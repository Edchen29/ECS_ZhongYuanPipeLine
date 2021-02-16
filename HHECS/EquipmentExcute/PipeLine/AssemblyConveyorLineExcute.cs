using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
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
    /// 组队区输送线
    /// </summary>
    public class AssemblyConveyorLine : PipeLineExcute
    {
        protected override BllResult ExcuteArrive(Equipment assemblyLine, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var ArriveTaskId = assemblyLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.ArriveTaskId.ToString());
                var convertResult = int.TryParse(ArriveTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位位[{assemblyLine.StationCode}]的设备[{assemblyLine.Name}] 地址请求失败，工序任务的id[{ArriveTaskId.Value}]转化为整数失败", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //var count = stepTraceList.Count(t => t.StationId == assemblyLine.StationId);
                //if (count > 1)
                //{
                //    Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]位置到达的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
                //    return BllResultFactory.Error();
                //}
                var number = assemblyLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.ArriveRealAddress.ToString());
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{assemblyLine.StationId}]设备[{assemblyLine.Name}]的位置到达失败，找不到未完成的工序任务id[{ArriveTaskId.Value}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //记录旧数据
                var stepTraceClone = (StepTrace)stepTrace.DeepClone();
                //更新数据
                stepTrace.StationId = stepTrace.NextStationId;
                stepTrace.NextStationId = 0;
                stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
                stepTrace.UpdateTime = DateTime.Now;
                stepTrace.UpdateBy = App.User.UserCode;
                var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                if (updateResult.Success)
                {
                    BllResult plcResult = SendAckToPlc(assemblyLine, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, number.Value, stepTrace.Id.ToString(), "", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), assemblyLine.SelfAddress);
                    if (plcResult.Success)
                    {
                        Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]位置到达成功，对应的任务[{stepTrace.Id}]信息写入设备", LogLevel.Success);
                    }
                    else
                    {
                        //PLC写入失败，就把数据改回来
                        AppSession.Dal.UpdateCommonModel<StepTrace>(stepTraceClone);
                        Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]位置到达失败，对应的任务[{stepTrace.Id}]信息没写入设备，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                    }
                    return plcResult;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]位置到达时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }


        protected override BllResult ExcuteRequest(Equipment assemblyLine, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var RequestTaskId = assemblyLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestTaskId.ToString());
                var convertResult = int.TryParse(RequestTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]地址请求失败，工序任务的id[{RequestTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{assemblyLine.StationId}]设备[{assemblyLine.Name}]的地址请求失败，找不到未完成的工序任务id[{RequestTaskId.Value}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var currentStation = assemblyLine.StationList.FirstOrDefault(t => t.Id == stepTrace.StationId);
                if (currentStation == null)
                {
                    Logger.Log($"处理设备[{assemblyLine.Name}]的站台[{assemblyLine.StationId}]的时候，找不到Id为[{stepTrace.StationId}]的站台，不能处理工序跟踪记录Id为[{stepTrace.Id}]的下料请求！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (int.TryParse(stepTrace.WeldingNo, out int weldingNo) == false)
                {
                    Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]地址请求失败，查询目的缓存区失败，组队平台编号{stepTrace.WeldingNo}]不能转换为数字！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //下个组队平台对应的缓存区
                string nextEquipmentCode = $"AssembleCache{ (weldingNo - 1) / 2 + 1}";
                var nextEquipment = allEquipments.FirstOrDefault(t => t.Code == nextEquipmentCode);
                if (nextEquipment == null)
                {
                    Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]地址请求失败，查询目的缓存区失败，根据设备编码[AssembleCache{stepTrace.WeldingNo}]找不到设备！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //var stationCodes = allEquipments.Where(t => t.BackAddress == assemblyLine.SelfAddress.ToString()).Select(t=> t.StationCode).ToList();
                //var sql = $" select top 1 stationCode,count(*) count from station_cache where  status = {StationCacheStatus.初始.GetIndexInt()} and stationCode in ('{string.Join("', '", stationCodes)}') group by stationCode having count(*) <= 6 order by count";
                //对缓存表做判断，小于12条记录说明缓存区没满，可以下料
                var stationCacheInfo = AppSession.Dal.GetCommonModelCount<StationCache>($" where  status = { StationCacheStatus.初始.GetIndexInt()} and stationCode = '{nextEquipment.StationCode}'");
                if (!stationCacheInfo.Success)
                {
                    Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]地址请求失败，查询目的缓存区管材数量失败，原因：[{stationCacheInfo.Msg}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stationCacheInfo.Data > 6)
                {
                    Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]地址请求失败，缓存区已满，请等待缓存区腾出位置！", LogLevel.Warning);
                    return BllResultFactory.Error();
                }
                //var nextEquipment = allEquipments.FirstOrDefault(t => t.StationCode == stationCacheInfo.Data[0].StationCode);

                var requestNumber = assemblyLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestNumber.ToString());
                //记录旧数据
                var stepTraceClone = (StepTrace)stepTrace.DeepClone();
                //更新数据
                stepTrace.NextStationId = nextEquipment.StationId;
                stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
                stepTrace.UpdateTime = DateTime.Now;
                stepTrace.UpdateBy = App.User.UserCode;
                var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                if (updateResult.Success)
                {
                    var sendResult = SendAddressReplyToPlc(assemblyLine, plc, StationMessageFlag.地址回复, StationLoadStatus.默认, requestNumber.Value, stepTrace.Id.ToString(), "", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), stepTrace.WeldingNo);
                    if (sendResult.Success)
                    {
                        Logger.Log($"处理工位位[{assemblyLine.StationCode}]的设备[{assemblyLine.Name}]地址请求 成功", LogLevel.Success);
                    }
                    else
                    {
                        //PLC写入失败，就把数据改回来
                        AppSession.Dal.UpdateCommonModel<StepTrace>(stepTraceClone);
                        Logger.Log($"处理工位位[{assemblyLine.StationCode}]的设备[{assemblyLine.Name}]地址请求 失败，写入PLC失败：{sendResult.Msg}", LogLevel.Error);
                    }
                    return sendResult;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理站台[{assemblyLine.StationId}]的设备[{assemblyLine.Name}]地址请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
            }
            return BllResultFactory.Error();
        }









    }
}
