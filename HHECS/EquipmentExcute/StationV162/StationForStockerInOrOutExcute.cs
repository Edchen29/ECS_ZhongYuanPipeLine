using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.ApiModel;
using HHECS.Model.ApiModel.HHWMSApiModel;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Station;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;

namespace HHECS.EquipmentExcute.StationV162
{
    /// <summary>
    /// 入出的堆垛机接台
    /// </summary>
    public class StationForStockerInOrOutExcute : StationExcute
    {
        public override BllResult ExcuteRequest(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            //获取任务
            var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where  taskStatus= {TaskEntityStatus.响应堆垛机库外放货完成.GetIndexInt()} and gateway = '{station.Code}' and deleted = 0");
            if (taskResult.Success)
            {
                var tasks = taskResult.Data;
                if (taskResult.Data.Count > 1)
                {
                    return BllResultFactory.Error($"站台{station.Name}根据任务状态{TaskEntityStatus.响应堆垛机库外放货完成}和站台编码{station.Code}查找到了多条任务，请检查数据");
                }
                else
                {
                    var task = taskResult.Data[0];
                    if (task.TaskType == TaskType.整盘入库.GetIndexInt() || task.TaskType == TaskType.空容器入库.GetIndexInt())
                    {
                        return BllResultFactory.Error($"站台{station.Name}不接受整盘入库和空容器入库任务");
                    }
                    var requestNumber = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestNumber.ToString()).Value;
                    //***********************************************************************************************************************
                    // 根据托盘上一个入库性质的任务进行查找其长宽高重信息  这个其实不重要---so,what do you want?
                    var requestWeight = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWeight.ToString()).Value;
                    var requestLength = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestLength.ToString()).Value;
                    var requestWidth = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWidth.ToString()).Value;
                    var requestHeight = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestHeight.ToString()).Value;
                    var tempPreResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where containerCode='{task.ContainerCode}' and " +
                        $" taskStatus>={(int)task.TaskStatus}  order by Id desc");
                    if (tempPreResult.Success && tempPreResult.Data.Count > 0)
                    {
                        requestHeight = tempPreResult.Data[0].ReqHeight;
                        requestLength = tempPreResult.Data[0].ReqLength;
                        requestWeight = tempPreResult.Data[0].ReqWeight;
                        requestWidth = tempPreResult.Data[0].ReqWidth;
                    }
                    //***********************************************************************************************************************
                    //判断是否有去向地址，没有则根据任务的出口进行判断
                    string goAddress = station.GoAddress;
                    if (string.IsNullOrWhiteSpace(goAddress) || goAddress == "0")
                    {
                        var tempEquipmentResult = AppSession.Dal.GetCommonModelByCondition<Equipment>($"where code = '{task.ToPort}' and warehouseCode = '{station.WarehouseCode}'");
                        if (!tempEquipmentResult.Success)
                        {
                            return BllResultFactory.Error($"没有配置站台{station.Code}的去向地址，系统也找不到任务{task.Id}对应的{task.ToPort}地址");
                        }
                        else
                        {
                            goAddress = tempEquipmentResult.Data[0].SelfAddress;
                        }
                    }

                    int tempStatus = task.TaskStatus; //记录原始状态以便回滚
                    task.TaskStatus = TaskEntityStatus.响应接出口站台请求.GetIndexInt();
                    var temp = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    if (temp.Success)
                    {
                        //回复地址请求
                        temp = SendAddressReplyToPlc(station, plc, StationMessageFlag.地址回复, StationLoadStatus.默认, requestNumber, task.ContainerCode,
                            requestWeight, requestLength, requestWidth, requestHeight, goAddress, "0");
                        if (temp.Success)
                        {
                            return BllResultFactory.Sucess($"响应站台{station.Name}地址请求成功，条码{task.ContainerCode},任务{task.Id}");
                        }
                        else
                        {
                            task.TaskStatus = tempStatus;
                            AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            return BllResultFactory.Error($"响应站台{station.Name}地址请求失败，条码{task.ContainerCode},任务{task.Id}，回滚任务状态");
                        }
                    }
                    else
                    {
                        return BllResultFactory.Error($"站台{station.Name}，响应请求时更新任务{task.Id}状态失败。");
                    }
                }
            }
            else
            {
                return BllResultFactory.Error($"站台{station.Name}根据任务状态{TaskEntityStatus.响应堆垛机库外放货完成}没有查询到任务：{taskResult.Msg}");
            }
        }

        public override BllResult ExcuteArrive(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            var isSuccess = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveResult.ToString()).Value;
            if (isSuccess == StationArriveResult.成功.GetIndexString())
            {
                //获取条码
                var barcode = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveBarcode.ToString()).Value;
                var arriveRealAddress = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveRealAddress.ToString()).Value;
                if (string.IsNullOrWhiteSpace(barcode))
                {
                    return BllResultFactory.Error($"站台{station.Name}有位置到达但是没有条码信息");
                }
                else
                {
                    var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where containerCode = '{barcode}' and taskStatus < {TaskEntityStatus.任务完成.GetIndexInt()} and deleted = 0");
                    if (taskResult.Success)
                    {
                        if (taskResult.Data.Count > 1)
                        {
                            return BllResultFactory.Error($"站台{station.Name}根据条码{barcode}查找到了多条任务，请检查数据");
                        }
                        else
                        {
                            var task = taskResult.Data[0];
                            //校验任务类型
                            if (task.TaskType == TaskType.整盘出库.GetIndexInt() || task.TaskType == TaskType.空容器出库.GetIndexInt())
                            {
                                return BllResultFactory.Error($"站台{station.Name}不接受整盘出库和空托盘出库任务");
                            }
                            //判断任务是否有分配库位,没有则调用库位分配函数
                            if (string.IsNullOrWhiteSpace(task.ToLocationCode))
                            {
                                LocationAssignReqModelInfo info = new LocationAssignReqModelInfo();
                                info.TaskNo = task.RemoteTaskNo;
                                info.Height = task.ReqHeight;
                                info.Weight = task.ReqWeight;
                                info.Length = task.ReqLength;
                                info.Width = task.ReqWidth;
                                var temp4 = AppSession.TaskService.GetDestinationLocationFromWMS(info);
                                if (!temp4.Success)
                                {
                                    return BllResultFactory.Error($"站台{station.Name}请求没有去向地址，并且请求上游系统分配库位失败");
                                }
                                task.ToLocationCode = temp4.Data.Code;
                            }

                            int tempStatus = task.TaskStatus; //记录原始状态以便回滚
                            string tempArrivaEquipmentCode = task.Gateway;
                            task.TaskStatus = TaskEntityStatus.响应接入站台到达.GetIndexInt();
                            task.Gateway = station.Code; //记录站台
                            var temp = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            if (temp.Success)
                            {
                                //回复位置到达
                                temp = SendAckToPlc(station, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, arriveRealAddress, "0");
                                if (temp.Success)
                                {
                                    return BllResultFactory.Sucess($"响应站台{station.Name}位置到达成功，条码{barcode},任务{task.Id}");
                                }
                                else
                                {
                                    task.TaskStatus = tempStatus;
                                    task.Gateway = tempArrivaEquipmentCode;
                                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                    return BllResultFactory.Error($"响应站台{station.Name}位置到达失败，条码{barcode},任务{task.Id}，回滚任务状态");
                                }
                            }
                            else
                            {
                                task.TaskStatus = tempStatus;
                                task.Gateway = tempArrivaEquipmentCode;
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                return BllResultFactory.Error($"响应站台{station.Name}位置到达请求失败，条码{barcode},任务{task.Id}，回滚任务状态");
                            }
                        }

                    }
                    else
                    {
                        return BllResultFactory.Error($"站台{station.Name}根据条码{barcode}没有查询到任务：{taskResult.Msg}");
                    }
                }
            }
            else
            {
                return BllResultFactory.Error($"站台{station.Name}位置电控报告到达失败");
            }
        }
    }
}
