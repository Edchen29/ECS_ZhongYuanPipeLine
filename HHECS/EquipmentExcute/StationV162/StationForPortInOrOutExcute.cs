using System;
using System.Collections.Generic;
using System.Data;
using HHECS.Bll;
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
    /// 标准拣选口实现
    /// </summary>
    public class StationForPortInOrOutExcute : StationExcute
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
                    var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>(
                        $"where containerCode = '{barcode}' and taskStatus < {TaskEntityStatus.任务完成.GetIndexInt()} and deleted = 0");
                    if (taskResult.Success)
                    {
                        if (taskResult.Data.Count > 1)
                        {
                            return BllResultFactory.Error($"站台{station.Name}根据条码{barcode}查找到了多条任务，请检查数据");
                        }
                        else
                        {
                            var task = taskResult.Data[0];
                            //判断任务类型，整出性质的任务直接完成，需要回库性质的任务需要更换阶段
                            if (task.TaskType == TaskType.整盘出库.GetIndexInt()
                                || task.TaskType == TaskType.空容器出库.GetIndexInt()
                                || task.TaskType == TaskType.换站.GetIndexInt())
                            {
                                using (IDbConnection connection = AppSession.Dal.GetConnection())
                                {
                                    IDbTransaction transaction = null;
                                    try
                                    {
                                        connection.Open();
                                        transaction = connection.BeginTransaction();
                                        var result = AppSession.TaskService.CompleteTask(task.Id.Value, App.User.UserCode, connection, transaction);
                                        if (result.Success)
                                        {
                                            //回复位置到达
                                            result = SendAckToPlc(station, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveRealAddress.ToString()).Value, "0");
                                            if (result.Success)
                                            {
                                                transaction.Commit();
                                                //发送LED
                                                AppSession.BllService.SendLED(station, task);
                                                return BllResultFactory.Sucess($"响应站台{station.Name}位置到达成功,完成任务成功，条码{barcode},任务{task.Id}");
                                            }
                                            else
                                            {
                                                transaction.Rollback();
                                                return BllResultFactory.Error($"响应站台{station.Name}位置到达失败,条码{barcode},任务{task.Id}，详情：{result.Msg}");
                                            }
                                        }
                                        else
                                        {
                                            transaction.Rollback();
                                            return BllResultFactory.Error($"响应站台{station.Name}位置到达请求失败,完成任务失败，条码{barcode},任务{task.Id}，详情：{result.Msg}");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction?.Rollback();
                                        return BllResultFactory.Error($"响应站台{station.Name}位置到达请求失败,完成任务失败，条码{barcode},任务{task.Id}，详情：{ex.Message}");
                                    }
                                }
                            }
                            else
                            {
                                //其他
                                using (IDbConnection connection = AppSession.Dal.GetConnection())
                                {
                                    IDbTransaction transaction = null;
                                    try
                                    {
                                        connection.Open();
                                        transaction = connection.BeginTransaction();
                                        task.Stage = (int)TaskStageFlag.入;  //一定要转换任务阶段
                                        task.TaskStatus = (int)TaskEntityStatus.到达拣选站台;
                                        task.Gateway = station.Code;
                                        var result = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                        if (result.Success)
                                        {
                                            result = SendAckToPlc(station, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, arriveRealAddress, "0");
                                            if (result.Success)
                                            {
                                                transaction.Commit();
                                                return BllResultFactory.Sucess($"响应站台{station.Name}位置到达成功,更新任务状态成功，条码{barcode},任务{task.Id}");
                                            }
                                            else
                                            {
                                                transaction.Rollback();
                                                return BllResultFactory.Error($"响应站台{station.Name}位置到达失败，通知PLC失败，条码{barcode},任务{task.Id}");
                                            }
                                        }
                                        else
                                        {
                                            return BllResultFactory.Error($"响应站台{station.Name}位置到达请求失败,更新任务状态失败，条码{barcode},任务{task.Id}，" +
                                                $"详情：{result.Msg}");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction?.Rollback();
                                        return BllResultFactory.Error($"仓库{task.WarehouseCode},响应站台{station.Name}位置到达请求失败,更新任务状态失败，" +
                                            $"条码{barcode},任务{task.Id}，详情：{ex.ToString()}");
                                    }
                                }
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
                return BllResultFactory.Error($"站台{station.Name}位置到达失败");
            }
        }

        public override BllResult ExcuteRequest(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            var barcode = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestBarcode.ToString()).Value;
            var requestNumber = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestNumber.ToString()).Value;
            var height = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestHeight.ToString()).Value;
            var weight = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWeight.ToString()).Value;
            var length = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestLength.ToString()).Value;
            var width = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWidth.ToString()).Value;
            string goAddress = "";
            string backAddress = "";

            if (string.IsNullOrWhiteSpace(barcode))
            {
                SendBack(station, plc, barcode);
                return BllResultFactory.Error($"站台{station.Name}有地址请求但是没有条码信息");
            }
            else
            {
                //获取任务
                var taskResult = AppSession.Dal.GetCommonModelByConditionWithZero<TaskEntity>($"where containerCode = '{barcode}' and taskStatus < {TaskEntityStatus.任务完成.GetIndexInt()} and deleted = 0");
                if (taskResult.Success)
                {
                    if (taskResult.Data.Count > 1)
                    {
                        SendBack(station, plc, barcode);
                        return BllResultFactory.Error($"站台{station.Name}根据条码{barcode}查找到了多条任务，请检查数据");
                    }
                    if (taskResult.Data.Count == 0)
                    {
                        return BllResultFactory.Error($"站台{station.Name}根据条码{barcode}没有找到任务，请检查数据");
                    }
                    else
                    {
                        var task = taskResult.Data[0];
                        if (task.TaskType == TaskType.整盘出库.GetIndexInt() || task.TaskType == TaskType.空容器出库.GetIndexInt())
                        {
                            SendBack(station, plc, barcode);
                            return BllResultFactory.Error($"站台{station.Name}不接受整盘出库和空托盘出库任务");
                        }
                        //检测是否有去向库位，没有则请求
                        if (task.TaskType == TaskType.换站.GetIndexInt())
                        {
                            //如果是换站任务，则port不能为此站台，否则无意义
                            if (task.ToPort == station.Code)
                            {
                                SendBack(station, plc, barcode);
                                return BllResultFactory.Error($"托盘{task.ContainerCode}为换站任务，但是换站目的地与入口目的地相同");
                            }
                        }
                        goAddress = station.GoAddress;
                        if (string.IsNullOrWhiteSpace(goAddress))
                        {
                            // 检测是否有去向库位或去向站台   设计逻辑：优先考虑是否分配了目标区域，没有则判断是否分配了库位
                            if (string.IsNullOrWhiteSpace(task.DestinationArea))
                            {
                                if (string.IsNullOrWhiteSpace(task.ToLocationCode))
                                {
                                    //没有分配目标区域，也没有分配库位的情况  这个时候需要请求上游系统分配目标区域或库位
                                    //hack:这里默认进行去向库位获取，按需改成获取去向区域
                                    LocationAssignReqModelInfo locationRequest = new LocationAssignReqModelInfo();
                                    locationRequest.TaskNo = task.RemoteTaskNo.ToString();
                                    locationRequest.Height = height;
                                    locationRequest.Weight = weight;
                                    locationRequest.Length = length;
                                    locationRequest.Width = width;
                                    var result = AppSession.TaskService.GetDestinationLocationFromWMS(locationRequest);
                                    if (!result.Success)
                                    {
                                        SendBack(station, plc, barcode);
                                        return BllResultFactory.Error($"任务{task.Id}没有去向库位,并且请求上游系统库位失败：{result.Msg}");
                                    }
                                    Logger.Log($"任务{task.Id}请求上位系统去向库位成功，获取库位为：{result.Data.Code}", LogLevel.Success);
                                    task.DestinationArea = result.Data.DestinationArea;
                                    task.ToLocationCode = result.Data.Code;
                                }
                                else
                                {
                                    //没有分配目标区域，分配了库位的情况
                                    // 根据库位 查找巷道
                                    var temp2 = AppSession.LocationService.GetLocationByCode(task.ToLocationCode);
                                    if (!temp2.Success)
                                    {
                                        SendBack(station, plc, barcode);
                                        return BllResultFactory.Error($"任务{task.Id}获取去向库位{task.ToLocationCode}出错：{temp2.Msg}");
                                    }
                                    task.DestinationArea = temp2.Data.DestinationArea;
                                }
                                //将获取到的数据进行update
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            }
                            //根据目标区域查找前进地址
                            var temp3 = AppSession.ExcuteService.GetGoAddressByDestinationArea(task.DestinationArea, station.Code, App.WarehouseCode);
                            if (!temp3.Success)
                            {
                                SendBack(station, plc, barcode);
                                return BllResultFactory.Error($"托盘{task.ContainerCode}没有查找到对应请进地址，请核对信息是否录入维护错误");
                            }
                            //hack:这里默认取第一个，如果有项目存在多个选择，请加逻辑进行判断
                            if (string.IsNullOrWhiteSpace(temp3.Data[0].SelfAddress))
                            {
                                SendBack(station, plc, barcode);
                                return BllResultFactory.Error($"托盘{task.ContainerCode}查找设备{temp3.Data[0].Code}无目标地址，请核对设备信息是否配置正确");
                            }
                            goAddress = temp3.Data[0].SelfAddress;
                        }

                        int tempStatus = task.TaskStatus; //记录原始状态以便回滚
                        string tempArrivaEquipmentCode = task.Gateway;
                        task.TaskStatus = TaskEntityStatus.拣选台回库.GetIndexInt();
                        task.Gateway = station.Code;
                        task.ReqHeight = height;
                        task.ReqLength = length;
                        task.ReqWeight = weight;
                        task.ReqWidth = width;
                        var temp = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        if (temp.Success)
                        {
                            //回复地址请求
                            temp = SendAddressReplyToPlc(station, plc, StationMessageFlag.地址回复, StationLoadStatus.默认,
                                requestNumber, barcode, weight, length, width, height, goAddress, backAddress);
                            if (temp.Success)
                            {
                                //发送LED
                                AppSession.BllService.SendLED(station, task);
                                return BllResultFactory.Sucess($"响应站台{station.Name}地址请求成功，条码{barcode},任务{task.Id}");
                            }
                            else
                            {
                                task.TaskStatus = tempStatus;
                                task.Gateway = tempArrivaEquipmentCode;
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                SendBack(station, plc, barcode);
                                return BllResultFactory.Error($"响应站台{station.Name}地址请求失败，条码{barcode},任务{task.Id}，详情：{temp.Msg}，回滚任务状态");
                            }
                        }
                        else
                        {
                            SendBack(station, plc, barcode);
                            return BllResultFactory.Error($"站台{station.Name}，响应请求时更新任务{task.Id}状态失败。详情：{temp.Msg}");
                        }
                    }
                }
                else
                {
                    SendBack(station, plc, barcode);
                    return BllResultFactory.Error($"站台{station.Name}根据条码{barcode}没有查询到任务：{taskResult.Msg}");
                }
            }
        }

    }
}
