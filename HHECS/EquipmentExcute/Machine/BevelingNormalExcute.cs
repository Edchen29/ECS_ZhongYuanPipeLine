using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Car;
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
    /// 坡口机处理类
    /// </summary>
    public class BevelingNormalExcute : MachineExcute
    {
        /// <summary>
        /// 处理 上料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="bevel"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteArrive(Equipment bevel, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                //var count = stepTraceList.Count(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == bevel.StationId);
                //if (count > 1)
                //{
                //    Logger.Log($"处理设备[{bevel.Name}]对应的站台[{bevel.StationId}]上料完成的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
                //    return BllResultFactory.Error();
                //}
                //var stepTrace = stepTraceList.FirstOrDefault(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == bevel.StationId);
                var ArriveTaskId = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveTaskId.ToString());
                var convertResult = int.TryParse(ArriveTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 下料请求失败，工序任务的id[{ArriveTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stepTraceId == 0)
                {
                    Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 下料请求失败，工序跟踪ID为0", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 下料请求失败，找不到未完成的工序任务id[{stepTraceId}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var number = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveRealAddress.ToString());
                //记录旧数据
                var stepTraceClone = (StepTrace)stepTrace.DeepClone();
                //更新数据
                if (stepTrace.NextStepId != 0)
                {
                    stepTrace.StepId = stepTrace.NextStepId;
                    stepTrace.NextStepId = 0;
                }
                stepTrace.StationId = stepTrace.NextStationId;
                stepTrace.NextStationId = 0;
                stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
                stepTrace.UpdateTime = DateTime.Now;
                stepTrace.UpdateBy = App.User.UserCode;
                var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                if (updateResult.Success)
                {
                    BllResult plcResult = SendAckToPlc(bevel, plc, MachineMessageFlag.WCS回复允许上料, number.Value, stepTrace.Id.ToString(), "", "0", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), bevel.GoAddress);
                    if (plcResult.Success)
                    {
                        Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 上料请求成功，对应的任务[{stepTrace.Id}]信息写入设备", LogLevel.Success);
                    }
                    else
                    {
                        //PLC写入失败，就把数据改回来
                        AppSession.Dal.UpdateCommonModel<StepTrace>(stepTraceClone);
                        Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 上料请求失败，对应的任务[{stepTrace.Id}]信息写入PLC失败，原因：{plcResult.Msg}", LogLevel.Error);
                    }
                    return plcResult;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 上料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }


        /// <summary>
        /// 执行下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="bevel"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteRequest(Equipment bevel, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var RequestTaskId = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestTaskId.ToString());
                var convertResult = int.TryParse(RequestTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 下料请求失败，工序任务的id[{RequestTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 下料请求失败，找不到未完成的工序任务id[{RequestTaskId.Value}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var nextEquipment = allEquipments.FirstOrDefault(t => t.SelfAddress == bevel.GoAddress.ToString());
                //判断是否有上料请求  响应下料
                if (nextEquipment == null)
                {
                    Logger.Log($"工位[{bevel.StationCode}]的设备[{bevel.Name}] 响应地址请求失败，原因：组队缓存位没有设置对应的下个设备", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //组队区输送线没货才能去
                var HasGoods = nextEquipment.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == PipeLineProps.HasGoods.ToString());
                if (HasGoods.Value != PipeLineGoods.无货.GetIndexString())
                {
                    return BllResultFactory.Error();
                }
                //如果已经有了去下个设备的任务，那么就不能去，需要等待
                if (stepTraceList.Exists(t => t.NextStationId == nextEquipment.StationId))
                {
                    return BllResultFactory.Error();
                }
                //就生成AGV任务并且插入数据库   小车任务的状态和类型需要重新定义？   
                CarTask taskCar = new CarTask();
                //taskCar.StepTraceId = Convert.ToInt32(Step_Trace_Id);
                taskCar.StepTraceId = stepTrace.Id;
                taskCar.CarNo = 1;
                taskCar.Type = CarTaskType.取货和放货.GetIndexInt();
                taskCar.FromLocation = bevel.Station.TrussTakeStationId.ToString();
                taskCar.ToLocation = nextEquipment.Station.TrussPutStationId.ToString();
                taskCar.ReSend = 0;
                taskCar.Status = TaskCarStatus.Init.GetIndexInt();
                taskCar.CreateTime = DateTime.Now;
                taskCar.EndTime = DateTime.Now;
                taskCar.StartTime = DateTime.Now;
                taskCar.UpdateTime = DateTime.Now;

                //修改工序监控
                stepTrace.NextStationId = nextEquipment.StationId;
                stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
                stepTrace.UpdateTime = DateTime.Now;
                stepTrace.UpdateBy = App.User.UserCode;

                var requestNumber = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestNumber.ToString());

                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction tran = null;
                    try
                    {
                        connection.Open();
                        tran = connection.BeginTransaction();
                        var step = connection.Get<Step>(stepTrace.StepId,transaction:tran);
                        var nextStep = connection.QueryFirstOrDefault<Step>($"select top 1 * from step where  sequence > {step.Sequence} order by sequence",transaction:tran);
                        stepTrace.NextStepId = nextStep.Id.Value;
                        connection.Insert<CarTask>(taskCar, transaction: tran);
                        connection.Update<StepTrace>(stepTrace, transaction: tran);

                        var sendResult = SendAddressReplyToPlc(bevel, plc, MachineMessageFlag.WCS回复允许下料, requestNumber.Value, stepTrace.Id.ToString(), "", "0", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), bevel.GoAddress);
                        if (sendResult.Success)
                        {
                            tran.Commit();
                            Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 下料请求 成功，任务:{stepTrace.Id}", LogLevel.Success);
                            return BllResultFactory.Sucess();
                        }
                        else
                        {
                            tran?.Rollback();
                            Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 下料请求的时候，写入PLC失败，任务:{stepTrace.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                    }
                    catch (Exception ex)
                    {
                        tran?.Rollback();
                        Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 下料请求的时候，发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                        return BllResultFactory.Error();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理设备【{bevel.Name}】下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }


        /// <summary>
        /// 处理翻转请求
        /// </summary>
        /// <param name="bevel"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteFlip(Equipment bevel, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var RequestTaskId = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestTaskId.ToString());
                var convertResult = int.TryParse(RequestTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 翻转信号失败，工序任务的id[{RequestTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stepTraceId == 0)
                {
                    Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 翻转信号失败，工序跟踪ID为0", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 翻转信号失败，找不到未完成的工序任务id[{stepTraceId}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //就生成AGV任务并且插入数据库   小车任务的状态和类型需要重新定义？   
                CarTask taskCar = new CarTask();
                taskCar.StepTraceId = stepTrace.Id;
                taskCar.CarNo = 1;
                taskCar.Type = CarTaskType.翻转.GetIndexInt();
                taskCar.FromLocation = bevel.Station.TrussTakeStationId.ToString();
                taskCar.ToLocation = bevel.Station.TrussTakeStationId.ToString();
                taskCar.ReSend = 0;
                taskCar.Status = TaskCarStatus.Init.GetIndexInt();
                taskCar.CreateTime = DateTime.Now;
                taskCar.EndTime = DateTime.Now;
                taskCar.StartTime = DateTime.Now;
                taskCar.UpdateTime = DateTime.Now;
                //taskCar.IsFlip = Convert.ToInt32(Allow_Flip);
                var updateResult = AppSession.Dal.InsertCommonModel<CarTask>(taskCar);
                if (updateResult.Success)
                {
                    var sendResult = SendFlipToPlc(bevel, plc, FlipFlag.WCS回复允许翻转);
                    if (sendResult.Success)
                    {
                        Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 翻转请求成功，对应的任务[{stepTrace.Id}]信息写入设备", LogLevel.Success);
                    }
                    else
                    {
                        //PLC写入失败，就把数据改回来
                        AppSession.Dal.DeleteCommonModelByIds<CarTask>(new List<int>() { taskCar.Id.Value });
                        Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 翻转请求失败，对应的任务[{stepTrace.Id}]信息写入PLC失败，原因：{sendResult.Msg}", LogLevel.Error);
                    }
                    return sendResult;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{bevel.StationCode}]的设备[{bevel.Name}] 翻转信号时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }
    }
}
