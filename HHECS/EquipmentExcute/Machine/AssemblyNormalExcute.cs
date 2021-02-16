using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Machine;
using HHECS.Model.Enums.PipeLine;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HHECS.EquipmentExcute.Machine
{
    /// <summary>
    /// 组队
    /// </summary>
    public class AssemblyNormalExcute : MachineExcute
    {
        /// <summary>
        /// 处理设备上料完成
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteArrive(Equipment assembly, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            //ECS修改工序监控的当前位置，然后响应上料到达
            try
            {
                //var count = stepTraceList.Count(t => t.StationId == assembly.StationId);
                //if (count > 1)
                //{
                //    Logger.Log($"处理设备[{assembly.Name}]对应的站台[{assembly.StationId}]上料完成的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
                //    return BllResultFactory.Error();
                //}
                //var stepTrace = stepTraceList.FirstOrDefault(t => t.StationId == assembly.StationId);
                var ArriveTaskId = assembly.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveTaskId.ToString());
                var convertResult = int.TryParse(ArriveTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 下料请求失败，工序任务的id[{ArriveTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stepTraceId == 0)
                {
                    Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 下料请求失败，工序跟踪ID为0", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 下料请求失败，找不到未完成的工序任务id[{stepTraceId}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var number = assembly.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveRealAddress.ToString());

                //记录旧数据
                var stepTraceClone = (StepTrace)stepTrace.DeepClone();
                //更新数据
                stepTrace.StationId = stepTrace.NextStationId;
                stepTrace.NextStationId = 0;
                stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
                stepTrace.UpdateTime = DateTime.Now;
                stepTrace.UpdateBy = App.User.UserCode;

                var sql = $"update station_cache set status = {StationCacheStatus.使用完毕.GetIndexInt()},updateBy='{App.User.UserCode}',updateTime='{DateTime.Now}' where stepTraceId = {stepTrace.Id} ";

                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction tran = null;
                    try
                    {
                        connection.Open();
                        tran = connection.BeginTransaction();
                        connection.Update<StepTrace>(stepTrace, transaction: tran);
                        connection.Execute(sql, transaction: tran);

                        BllResult plcResult = SendAckToPlc(assembly, plc, MachineMessageFlag.WCS回复允许上料, number.Value, stepTrace.Id.ToString(), "", "0", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), assembly.SelfAddress);
                        if (plcResult.Success)
                        {
                            tran.Commit();
                            Logger.Log($"处理设备【{assembly.Name}】上料完成成功，对应的任务[{stepTrace.Id}]信息写入设备", LogLevel.Success);
                        }
                        else
                        {
                            tran?.Rollback();
                            Logger.Log($"处理设备【{assembly.Name}】上料完成失败，对应的任务[{stepTrace.Id}]信息没写入设备，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                        }
                        return plcResult;
                    }
                    catch (Exception ex)
                    {
                        tran?.Rollback();
                        Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 响应地址请求的时候，发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                        return BllResultFactory.Error();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理设备【{assembly.Name}】上料完成时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 处理设备请求下线
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteRequest(Equipment assembly, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var RequestTaskId = assembly.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestTaskId.ToString());
                var convertResult = int.TryParse(RequestTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 下料请求失败，工序任务的id[{RequestTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stepTraceId == 0)
                {
                    Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 下料请求失败，工序跟踪ID为0", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 下料请求失败，找不到未完成的工序任务id[{stepTraceId}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var requestNumber = assembly.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestNumber.ToString());

                ////记录旧数据
                //var stepTraceClone = (StepTrace)stepTrace.DeepClone();

                //修改工序跟踪
                stepTrace.Status = StepTraceStatus.任务完成.GetIndexInt();
                stepTrace.UpdateTime = DateTime.Now;
                stepTrace.UpdateBy = App.User.UserCode;
                //var result = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                //if (result.Success)
                //{
                //}
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction tran = null;
                    try
                    {
                        connection.Open();
                        tran = connection.BeginTransaction();
                        connection.Update<StepTrace>(stepTrace);
                        var sendResult = SendAddressReplyToPlc(assembly, plc, MachineMessageFlag.WCS回复允许下料, requestNumber.Value, stepTrace.Id.ToString(), "", "0", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), assembly.GoAddress);
                        if (sendResult.Success)
                        {
                            tran?.Commit();
                            Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 下料请求 成功，任务:{stepTrace.Id}", LogLevel.Success);
                        }
                        else
                        {
                            //PLC写入失败，就把数据改回来
                            tran?.Rollback();
                            Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 下料请求的时候，写入PLC失败，任务:{stepTrace.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                        }
                        return sendResult;
                    }
                    catch (Exception ex)
                    {
                        tran?.Rollback();
                        Logger.Log($"处理工位[{assembly.StationCode}]的设备[{assembly.Name}] 下料请求的时候，发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                        return BllResultFactory.Error();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理设备【{assembly.Name}】下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }    
        }


    }
}
