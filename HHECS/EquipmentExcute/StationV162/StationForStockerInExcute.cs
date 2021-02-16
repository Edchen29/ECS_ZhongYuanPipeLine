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
    /// 标准堆垛机接入站台处理
    /// 这里只处理位置到达
    /// 设计逻辑： 
    /// 1. 根据电气给的到达信息的条码和到达结果进行数据查询获取到相应任务
    /// 2. 判断任务是否有分配去向库位，没有的向上游系统请求
    /// 3. 更新任务状态为响应入库站台请求
    /// </summary>
    public class StationForStockerInExcute : StationExcute
    {
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
                                    return BllResultFactory.Error($"站台{station.Name}请求没有去向地址，并且请求上游系统分配库位失败：{temp4.Msg}");
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

        public override BllResult ExcuteRequest(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            return BllResultFactory.Error($"堆垛机接入站台{station.Name}不执行请求处理");
        }
    }
}
