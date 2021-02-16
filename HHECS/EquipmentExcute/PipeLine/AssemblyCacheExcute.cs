using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Machine;
using HHECS.Model.Enums.PipeLine;
using HHECS.Model.Enums.Station;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HHECS.EquipmentExcute.PipeLine
{
    /// <summary>
    /// 组队缓存位
    /// </summary>
    public class AssemblyCacheExcute : PipeLineExcute
    {

        protected override BllResult ExcuteArrive(Equipment AssemblyCcache, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var ArriveTaskId = AssemblyCcache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.ArriveTaskId.ToString());
                if (string.IsNullOrWhiteSpace(ArriveTaskId.Value))
                {
                    Logger.Log($"处理工位位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}] 位置到达失败，原因：有位置到达但是没有任务号信息", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //var count = stepTraceList.Count(t => t.StationId == AssemblyCcache.StationId);
                //if (count > 1)
                //{
                //    Logger.Log($"处理工位[{AssemblyCcache.StationId}]设备[{AssemblyCcache.Name}]位置到达的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
                //    return BllResultFactory.Error();
                //}
                var number = AssemblyCcache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.ArriveRealAddress.ToString());
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == Convert.ToInt32(ArriveTaskId.Value));
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}]位置到达失败，找不到未完成的工序任务id[{ArriveTaskId.Value}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //更新数据
                stepTrace.StationId = stepTrace.NextStationId;
                stepTrace.NextStationId = 0;
                stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
                stepTrace.UpdateTime = DateTime.Now;
                stepTrace.UpdateBy = App.User.UserCode;

                //生成缓存记录并且插入数据库  
                StationCache stationCache = new StationCache();
                stationCache.stepTraceId = stepTrace.Id;
                stationCache.StationId = AssemblyCcache.StationId;
                stationCache.StationCode = AssemblyCcache.StationCode;
                stationCache.MaterialLength = stepTrace.PipeLength;
                stationCache.WcsProductType = stepTrace.PipeMaterial;
                stationCache.Thickness = stepTrace.PipeThickness;
                stationCache.Diameter = stepTrace.PipeDiameter;                
                stationCache.Status = StationCacheStatus.初始.GetIndexInt();
                stationCache.CreateBy = App.User.UserCode;
                stationCache.CreateTime = DateTime.Now;

                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction tran = null;
                    try
                    {
                        connection.Open();
                        tran = connection.BeginTransaction();
                        connection.Update<StepTrace>(stepTrace, transaction: tran);
                        connection.Insert<StationCache>(stationCache, transaction: tran);
                        BllResult plcResult = SendAckToPlc(AssemblyCcache, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, number.Value, stepTrace.Id.ToString(), "", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), stepTrace.WeldingNo);
                        if (plcResult.Success)
                        {
                            tran.Commit();
                            Logger.Log($"处理工位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}] 响应位置到达 成功，任务:{stepTrace.Id}", LogLevel.Success);
                            return BllResultFactory.Sucess();
                        }
                        else
                        {
                            tran?.Rollback();
                            Logger.Log($"处理工位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}] 响应位置到达的时候，写入PLC失败，任务:{stepTrace.Id}，原因：{plcResult.Msg}", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                    }
                    catch (Exception ex)
                    {
                        tran?.Rollback();
                        Logger.Log($"处理工位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}] 响应位置到达的时候，发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                        return BllResultFactory.Error();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{AssemblyCcache.StationId}]设备[{AssemblyCcache.Name}]位置到达时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }


        protected override BllResult ExcuteRequest(Equipment AssemblyCcache, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var RequestTaskId = AssemblyCcache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestTaskId.ToString());
                if (string.IsNullOrWhiteSpace(RequestTaskId.Value))
                {
                    Logger.Log($"处理工位位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}] 地址请求失败，原因：有地址请求但是没有任务号信息", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //var countResult = AppSession.Dal.GetCommonModelCount<StationCache>($"WHERE stationId='{AssemblyCcache.StationId}'");
                //if (countResult.Data < 1)
                //{
                //    Logger.Log($"处理工位位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}] 地址请求失败，原因：没有物料", LogLevel.Error);
                //    return BllResultFactory.Error();
                //}
                var convertResult = int.TryParse(RequestTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}]地址请求失败，工序任务的id[{RequestTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}]地址请求失败，找不到未完成的工序任务id[{RequestTaskId.Value}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var assembly = allEquipments.FirstOrDefault(t => t.SelfAddress == AssemblyCcache.GoAddress.ToString());
                //判断是否有上料请求  响应下料
                if (assembly == null)
                {
                    Logger.Log($"工位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}] 响应地址请求失败，原因：组队缓存位没有设置对应的下个设备", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //如果已经有了去下个设备的任务，那么就不能去，需要等待
                if (stepTraceList.Exists(t => t.NextStationId == assembly.StationId))
                {
                    return BllResultFactory.Error();
                }

                var ArriveMessage = assembly.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveMessage.ToString());
                var WCSACKMessage = assembly.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());

                //PLC有请求，但ECS没有，则ECS还没有响应
                //
                if (ArriveMessage.Value == MachineMessageFlag.PLC自动请求上料.GetIndexString() && WCSACKMessage.Value == MachineMessageFlag.默认.GetIndexString())
                {
                    //记录旧数据
                    var stepTraceClone = (StepTrace)stepTrace.DeepClone();
                    //更新数据
                    stepTrace.NextStationId = assembly.StationId;
                    stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
                    stepTrace.UpdateTime = DateTime.Now;
                    stepTrace.UpdateBy = App.User.UserCode;

                    var requestNumber = AssemblyCcache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestNumber.ToString());

                    var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                    if (updateResult.Success)
                    {
                        var sendResult = SendAddressReplyToPlc(AssemblyCcache, plc, StationMessageFlag.地址回复, StationLoadStatus.默认, requestNumber.Value, stepTrace.Id.ToString(), "", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), stepTrace.WeldingNo);
                        if (sendResult.Success)
                        {
                            Logger.Log($"处理工位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}] 响应地址请求 成功，任务:{stepTrace.Id}", LogLevel.Success);
                        }
                        else
                        {
                            //PLC写入失败，就把数据改回来
                            AppSession.Dal.UpdateCommonModel<StepTrace>(stepTraceClone);
                            Logger.Log($"处理工位[{AssemblyCcache.StationCode}]的设备[{AssemblyCcache.Name}] 响应地址请求的时候，写入PLC失败，任务:{stepTrace.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                        }
                        return sendResult;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{AssemblyCcache.StationId}]设备[{AssemblyCcache.Name}]地址请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
            }
            return BllResultFactory.Error();
        }





    }
}
