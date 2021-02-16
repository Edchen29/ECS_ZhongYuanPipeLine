using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Machine;
using HHECS.Model.Enums.PipeLine;
using HHECS.Model.Enums.Station;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HHECS.EquipmentExcute.PipeLine
{
    /// <summary>
    /// 测长缓存位处理类
    /// </summary>
    public class LengthMeasuringCacheExcute : PipeLineExcute
    {
        /// <summary>
        /// 测长缓存区位置到达：
        /// 小车把管子从测长设备取走，放到测长缓存区后，PLC发送位置到达信号。
        /// ECS响应到达，并且修改工序监控种的当前工位。
        /// </summary>
        /// <param name="lengthMeasuringCache"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        protected override BllResult ExcuteArrive(Equipment lengthMeasuringCache, List<Equipment> allEquipments, List<StepTrace> data, IPLC plc)
        {
            try
            {
                var ArriveTaskId = lengthMeasuringCache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.ArriveTaskId.ToString());
                if (string.IsNullOrWhiteSpace(ArriveTaskId.Value))
                {
                    Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}] 位置到达失败，原因：有地址请求但是没有任务号信息", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stationCachesResult = AppSession.Dal.GetCommonModelByCondition<StationCache>($" where id = {ArriveTaskId.Value}");
                if (!stationCachesResult.Success)
                {
                    Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]位置到达失败，根据管子缓存标识[{ArriveTaskId.Value}]查找缓存记录失败，原因：{stationCachesResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stationCache = stationCachesResult.Data[0];
                //记录旧数据
                var stationCacheClone = (StationCache)stationCache.DeepClone();
                //更新数据
                stationCache.StationId = lengthMeasuringCache.StationId;
                stationCache.StationCode = lengthMeasuringCache.StationCode;
                stationCache.UpdateBy = App.User.UserCode;
                stationCache.UpdateTime = DateTime.Now;
                var updateResult = AppSession.Dal.UpdateCommonModel<StationCache>(stationCache);
                if (!updateResult.Success)
                {
                    Logger.Log($"处理工位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}] 位置到达的时候，编辑管材信息到数据库失败，原因：{updateResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var number = lengthMeasuringCache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.ArriveRealAddress.ToString());
                //直接做一个简单回复
                BllResult plcResult = SendAckToPlc(lengthMeasuringCache, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, number.Value, stationCache.Id.ToString(), "", stationCache.WcsProductType.ToString(), stationCache.MaterialLength.ToString(), stationCache.Diameter.ToString(), stationCache.Thickness.ToString(), lengthMeasuringCache.SelfAddress);
                if (plcResult.Success)
                {
                    Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]位置到达 成功，对应的管子缓存标识[{stationCache.Id}]信息写入设备", LogLevel.Success);
                    return plcResult;
                }
                else
                {
                    //PLC写入失败，就把数据改回来
                    AppSession.Dal.UpdateCommonModel<StationCache>(stationCacheClone);
                    Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]位置到达 失败，对应的任务[{stationCache.Id}]信息没写入设备，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]位置到达 时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 测长缓存区地址请求：
        /// 测长缓存区域只要有物料，PLC就发送请求，ECS判断如果定长切割设备请求上料，就响应请求。
        /// PLC收到响应就从调度设备从缓存料架取走一根管子放到输送线上。
        /// </summary>
        /// <param name="lengthMeasuringCache"></param>
        /// <param name="allEquipments"></param>
        /// <param name="data"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        protected override BllResult ExcuteRequest(Equipment lengthMeasuringCache, List<Equipment> allEquipments, List<StepTrace> data, IPLC plc)
        {
            try
            {
                var RequestTaskId = lengthMeasuringCache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestTaskId.ToString());
                if (string.IsNullOrWhiteSpace(RequestTaskId.Value))
                {
                    Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}] 地址请求失败，原因：有地址请求但是没有任务号信息", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var cutter = allEquipments.FirstOrDefault(t => t.SelfAddress == lengthMeasuringCache.GoAddress.ToString());
                //找到下个设备(定长切割)
                if (cutter == null)
                {
                    Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}] 地址请求失败，原因：测长缓存位没有设置对应的下个设备", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var ArriveMessage = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveMessage.ToString());
                var WCSACKMessage = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
                var requestNumber = lengthMeasuringCache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestNumber.ToString());
                if (ArriveMessage?.Value == MachineMessageFlag.PLC自动请求上料.GetIndexString() && WCSACKMessage?.Value == MachineMessageFlag.默认.GetIndexString())
                {
                    var stationCacheCountResult = AppSession.Dal.GetCommonModelCount<StationCache>($"where status = {StationCacheStatus.使用中.GetIndexInt()}");
                    if (!stationCacheCountResult.Success)
                    {
                        Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]地址请求 失败，统计正在切割中的管子数量失败，原因：{stationCacheCountResult.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    if (stationCacheCountResult.Data > 0)
                    {
                        Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]地址请求 失败，存在还未切割完毕的管子，请等管子切割完毕。", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    var stationCachesResult = AppSession.Dal.GetCommonModelByCondition<StationCache>($"where id = {RequestTaskId.Value}");
                    if (!stationCachesResult.Success)
                    {
                        Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]地址请求 失败，根据缓存ID[{RequestTaskId.Value}]查找缓存记录失败，原因：{stationCachesResult.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    var stationCache = stationCachesResult.Data[0];
                    if (stationCache.Status == StationCacheStatus.初始.GetIndexInt())
                    {
                        Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]地址请求 失败，管子缓存标识[{RequestTaskId.Value}]对应的管子还没有套料", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    if (stationCache.Status > StationCacheStatus.已经套料.GetIndexInt())
                    {
                        Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]地址请求 失败，管子缓存标识[{RequestTaskId.Value}]对应的管子已经使用过了", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    //将缓存的管子标记为已用
                    stationCache.Status = StationCacheStatus.使用中.GetIndexInt();
                    stationCache.CreateTime = DateTime.Now;
                    stationCache.CreateBy = App.User.UserCode;

                    using (IDbConnection connection = AppSession.Dal.GetConnection())
                    {
                        IDbTransaction tran = null;
                        try
                        {
                            connection.Open();
                            tran = connection.BeginTransaction();
                            connection.Update<StationCache>(stationCache, tran);
                            BllResult plcResult = SendAddressReplyToPlc(lengthMeasuringCache, plc, StationMessageFlag.地址回复, StationLoadStatus.默认, requestNumber.Value, stationCache.Id.ToString(), "", stationCache.WcsProductType.ToString(), stationCache.MaterialLength.ToString(), stationCache.Diameter.ToString(), stationCache.Thickness.ToString(), lengthMeasuringCache.GoAddress);
                            if (plcResult.Success)
                            {
                                tran?.Commit();
                                Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]地址请求 成功", LogLevel.Success);
                            }
                            else
                            {
                                tran?.Rollback();
                                Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]地址请求 失败，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                            }
                            return plcResult;
                        }
                        catch (Exception ex)
                        {
                            tran?.Rollback();
                            Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}]地址请求的时候，发生异常，缓存标识:[{RequestTaskId.Value}]，原因：{ex.Message}", LogLevel.Exception, ex);
                            return BllResultFactory.Error();
                        }
                    }
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位位[{lengthMeasuringCache.StationCode}]的设备[{lengthMeasuringCache.Name}] 地址请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }
    }
}
