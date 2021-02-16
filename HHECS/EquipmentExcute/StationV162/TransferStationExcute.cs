using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HHECS.Bll;
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
    /// 中转站台的实现类
    /// 此类的具体作用请参考台光项目，其他情况请勿使用
    /// </summary>
    public class TransferStationExcute : StationExcute
    {
        public override BllResult ExcuteRequest(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            //获取任务
            var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where  taskStatus= {TaskEntityStatus.响应堆垛机库外放货完成.GetIndexInt()} " +
                $"and gateway = '{station.SelfAddress}' and warehouseCode = '{station.WarehouseCode}'");
            if (taskResult.Success)
            {
                var tasks = taskResult.Data;
                if (tasks.Count == 0)
                {
                    return BllResultFactory.Error($"站台{station.Name}根据任务状态{TaskEntityStatus.响应堆垛机库外放货完成}没有找到任务数据，请检查数据");

                }
                if (tasks.Count > 1)
                {
                    return BllResultFactory.Error($"站台{station.Name}根据任务状态{TaskEntityStatus.响应堆垛机库外放货完成}查找到了多条任务，请检查数据");
                }
                else
                {
                    var task = taskResult.Data[0];
                    //if (task.Type == TaskType.整盘入库.GetIndexInt() || task.Type == TaskType.空容器入库.GetIndexInt())
                    //{
                    //    Logger.Log($"站台{station.Name}不接受整盘入库和空容器入库任务", LogLevel.Error);
                    //    return;
                    //}
                    if (station.GoAddress.ToString() == "")
                    {
                        return BllResultFactory.Error($"站台{station.Name}没有设置去向地址");
                    }

                    int tempStatus = task.TaskStatus; //记录原始状态以便回滚
                    string tempGateWay = task.Gateway;

                    task.TaskStatus = TaskEntityStatus.响应接出口站台请求.GetIndexInt();
                    task.Gateway = station.Code;
                    //task.Roadway = station.RoadWay;
                    var temp = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    if (temp.Success)
                    {
                        String number = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "RequestNumber").Value;
                        String weight = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "RequestWeight").Value;
                        String length = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "RequestLength").Value;
                        String width = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "RequestWidth").Value;
                        String height = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "RequestHeight").Value;
                        temp = SendAddressReplyToPlc(station, plc, StationMessageFlag.地址回复, StationLoadStatus.回复到达,
                            number, task.ContainerCode, weight, length, width, height, station.GoAddress.ToString(), "");
                        if (temp.Success)
                        {
                            return BllResultFactory.Sucess($"响应站台{station.Name}地址请求成功，条码{task.ContainerCode},任务{task.Id}");
                        }
                        else
                        {
                            task.TaskStatus = tempStatus;
                            task.Gateway = tempGateWay;
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
                return BllResultFactory.Error($"站台{station.Name}未能获取到任务状态为'{TaskEntityStatus.响应堆垛机库外放货完成}'的任务，请检查数据。");
            }
        }

        public override BllResult ExcuteArrive(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            var isSuccess = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "ArriveResult").Value;
            if (isSuccess == "1")
            {
                //获取条码
                var barcode = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "ArriveBarcode").Value;
                if (string.IsNullOrWhiteSpace(barcode))
                {
                    return BllResultFactory.Error($"站台{station.Name}有位置到达但是没有条码信息");
                }
                else
                {
                    var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where containerCode = '{barcode}' " +
                        $"and taskStatus < {TaskEntityStatus.任务完成.GetIndexInt()}");
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
                            //if (task.Type == TaskType.整盘出库.GetIndexInt() || task.Type == TaskType.空容器出库.GetIndexInt())
                            //{
                            //    Logger.Log($"站台{station.Name}不接受整盘出库和空托盘出库任务", LogLevel.Error);
                            //    return;
                            //}
                            int tempStatus = task.TaskStatus; //记录原始状态以便回滚
                            string tempGateWay = task.Gateway;


                            task.TaskStatus = TaskEntityStatus.响应接入站台到达.GetIndexInt();
                            task.Gateway = station.Code;//记录到达的站台
                            var temp = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            if (temp.Success)
                            {
                                //回复位置到达
                                string arriveRealAddress = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "ArriveRealAddress").Value;
                                temp = SendAckToPlc(station, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, arriveRealAddress, "0");
                                if (temp.Success)
                                {
                                    return BllResultFactory.Error($"响应站台{station.Name}位置到达成功，条码{barcode},任务{task.Id}");
                                }
                                else
                                {
                                    Logger.Log($"响应站台{station.Name}位置到达失败，条码{barcode},任务{task.Id}，回滚任务状态", LogLevel.Error);
                                    task.TaskStatus = tempStatus;
                                    task.Gateway = tempGateWay;
                                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                    return BllResultFactory.Error($"响应站台{station.Name}位置到达失败，条码{barcode},任务{task.Id}，回滚任务状态");
                                }
                            }
                            else
                            {
                                task.TaskStatus = tempStatus;
                                task.Gateway = tempGateWay;
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                return BllResultFactory.Error($"响应站台{station.Name}位置到达请求失败，条码{barcode},任务{task.Id}，回滚任务状态");
                            }
                        }

                    }
                    else
                    {
                        return BllResultFactory.Error($"站台{station.Name}根据条码{barcode}没有查询到任务。");

                    }
                }
            }
            else
            {
                return BllResultFactory.Error($"站台{station.Name}位置到达失败");
            }
        }

    }
}
