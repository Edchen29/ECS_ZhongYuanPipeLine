using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HHECS.Bll;
using HHECS.Model;
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
    /// 标准只出站台
    /// </summary>
    public class StationForPortOutExcute : StationExcute
    {

        public override BllResult ExcuteArrive(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            var isSuccess = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveResult.ToString()).Value;
            if (isSuccess == StationArriveResult.成功.GetIndexString())
            {
                //获取条码
                var barcode = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveBarcode.ToString()).Value;
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
                            if (task.TaskType == TaskType.空容器出库.GetIndexInt() || task.TaskType == TaskType.整盘出库.GetIndexInt() || task.TaskType == TaskType.换站.GetIndexInt())
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
                                return BllResultFactory.Error($"响应站台{station.Name}位置到达请求失败,完成任务失败，条码{barcode},任务{task.Id}，详情：只出站台对应的任务类型有问题");
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
            return BllResultFactory.Error($"只出口{station.Name}不处理地址请求");
        }

    }
}
