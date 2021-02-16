using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Car;
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
    /// 坡口缓存位
    /// </summary>
    public class BevelCacheExcute: PipeLineExcute
    {

        protected override BllResult ExcuteArrive(Equipment bevelCcache, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var ArriveTaskId = bevelCcache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.ArriveTaskId.ToString());
                if (string.IsNullOrWhiteSpace(ArriveTaskId.Value))
                {
                    Logger.Log($"处理工位位[{bevelCcache.StationCode}]的设备[{bevelCcache.Name}] 地址请求失败，原因：有地址请求但是没有任务号信息", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //var count = stepTraceList.Count(t => t.StationId == bevelCcache.StationId);
                //if (count > 1)
                //{
                //    Logger.Log($"处理工位[{bevelCcache.StationId}]设备[{bevelCcache.Name}]位置到达的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
                //    return BllResultFactory.Error();
                //}
                var number = bevelCcache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.ArriveRealAddress.ToString());
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == Convert.ToInt32(ArriveTaskId.Value));
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{bevelCcache.StationId}]设备[{bevelCcache.Name}]的地址请求失败，找不到未完成的工序任务id[{ArriveTaskId.Value}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //记录旧数据
                var stepTraceClone = (StepTrace)stepTrace.DeepClone();

                //更新数据
                stepTrace.StepId = stepTrace.NextStepId;
                stepTrace.NextStepId = 0;
                stepTrace.StationId = stepTrace.NextStationId;
                stepTrace.NextStationId = 0;
                stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
                stepTrace.UpdateTime = DateTime.Now;
                stepTrace.UpdateBy = App.User.UserCode;
                var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                if (updateResult.Success)
                {
                    BllResult plcResult = SendAckToPlc(bevelCcache, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, number.Value, stepTrace.Id.ToString(), "", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), bevelCcache.SelfAddress);
                    if (plcResult.Success)
                    {
                        Logger.Log($"处理工位[{bevelCcache.StationId}]设备[{bevelCcache.Name}]位置到达成功，对应的任务[{stepTrace.Id}]信息写入设备", LogLevel.Success);
                    }
                    else
                    {
                        //PLC写入失败，就把数据改回来
                        AppSession.Dal.UpdateCommonModel<StepTrace>(stepTraceClone);
                        Logger.Log($"处理工位[{bevelCcache.StationId}]设备[{bevelCcache.Name}]位置到达失败，对应的任务[{stepTrace.Id}]信息没写入设备，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                    }
                    return plcResult;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{bevelCcache.StationId}]设备[{bevelCcache.Name}]位置到达时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }

        protected override BllResult ExcuteRequest(Equipment bevelCcache, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var RequestTaskId = bevelCcache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestTaskId.ToString());
                var convertResult = int.TryParse(RequestTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{bevelCcache.StationId}]设备[{bevelCcache.Name}]地址请求失败，工序任务的id[{RequestTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{bevelCcache.StationId}]设备[{bevelCcache.Name}]的地址请求失败，找不到未完成的工序任务id[{RequestTaskId.Value}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var bevel = allEquipments.FirstOrDefault(t => t.SelfAddress == bevelCcache.GoAddress.ToString());

                //找到下个设备
                if (bevel == null)
                {
                    Logger.Log($"处理工位位[{bevelCcache.StationCode}]的设备[{bevelCcache.Name}] 地址请求失败，原因：坡口缓存位没有设置对应的下个设备", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //如果已经有了去下个设备的任务，那么就不能去，需要等待
                if (stepTraceList.Exists(t => t.NextStationId == bevel.StationId))
                {
                    return BllResultFactory.Error();
                }

                var ArriveMessage = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveMessage.ToString());
                var WCSACKMessage = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());

                if (ArriveMessage.Value == MachineMessageFlag.PLC自动请求上料.GetIndexString() && WCSACKMessage.Value == MachineMessageFlag.默认.GetIndexString())
                {
                    return  ExcuteRequest(bevelCcache, bevel, stepTrace, plc);
                }
                return BllResultFactory.Sucess();           
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{bevelCcache.StationId}]设备[{bevelCcache.Name}]地址请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }


        private BllResult ExcuteRequest(Equipment bevelCcache, Equipment bevel, StepTrace stepTrace, IPLC plc)
        {
            //就生成AGV任务并且插入数据库   小车任务的状态和类型需要重新定义？   
            CarTask taskCar = new CarTask();
            taskCar.StepTraceId = stepTrace.Id;
            taskCar.CarNo = 1;
            taskCar.Type = CarTaskType.取货和放货.GetIndexInt();
            taskCar.FromLocation = bevelCcache.Station.TrussTakeStationId.ToString();
            taskCar.ToLocation = bevel.Station.TrussPutStationId.ToString();
            taskCar.ReSend = 0;
            taskCar.Status = TaskCarStatus.Init.GetIndexInt();
            //taskCar.IsFlip = CarTaskType.取货和放货.GetIndexInt();
            taskCar.CreateTime = DateTime.Now;
            taskCar.EndTime = DateTime.Now;
            taskCar.StartTime = DateTime.Now;
            taskCar.UpdateTime = DateTime.Now;

            //修改工序跟踪
            stepTrace.NextStationId = bevel.StationId;
            stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
            stepTrace.UpdateTime = DateTime.Now;
            stepTrace.UpdateBy = App.User.UserCode;
            stepTrace.CreateTime = DateTime.Now;
            stepTrace.LineInTime = DateTime.Now;//
            stepTrace.LineOutTime = DateTime.Now;//
            stepTrace.StationInTime = DateTime.Now;
            stepTrace.StationOutTime = DateTime.Now;

            var requestNumber = bevelCcache.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestNumber.ToString());

            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();

                    connection.Insert<CarTask>(taskCar, transaction: tran);
                    connection.Update<StepTrace>(stepTrace, transaction: tran);

                    var sendResult = SendAddressReplyToPlc(bevelCcache, plc, StationMessageFlag.地址回复, StationLoadStatus.默认, requestNumber.Value, stepTrace.Id.ToString(), "", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), bevelCcache.GoAddress);
                    if (sendResult.Success)
                    {
                        tran.Commit();
                        Logger.Log($"处理工位[{bevelCcache.StationCode}]的设备[{bevelCcache.Name}] 地址请求 成功，任务:{stepTrace.Id}", LogLevel.Success);
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        tran?.Rollback();
                        Logger.Log($"处理工位[{bevelCcache.StationCode}]的设备[{bevelCcache.Name}] 地址请求的时候，写入PLC失败，任务:{stepTrace.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    Logger.Log($"处理工位[{bevelCcache.StationCode}]的设备[{bevelCcache.Name}] 地址请求的时候，发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                    return BllResultFactory.Error();
                }
            }
        }






    }
}
