using Dapper;
using HHECS.Bll;
using HHECS.EquipmentExcute.Groove;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Car;
using HHECS.Model.Enums.Machine;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HHECS.EquipmentExcute.Bevel
{
    /// <summary>
    /// 切割机处理类
    /// </summary>
    public class CutterNormalExcute : CutterExcute
    {
        /// <summary>
        /// 执行上料完成
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="cutter"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteArrive(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var count = stepTraceList.Count(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == cutter.StationId);
                if (count > 1)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]上料完成的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == cutter.StationId);
                if (stepTrace != null)
                {
                    //记录旧数据
                    var status = stepTrace.Status;
                    var stationId = stepTrace.StationId;
                    var nextStationId = stepTrace.NextStationId = 0;
                    var updateTime = stepTrace.UpdateTime;
                    var updateBy = stepTrace.UpdateBy;
                    //更新数据
                    stepTrace.StationId = stepTrace.NextStationId;
                    stepTrace.NextStationId = 0;
                    stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
                    stepTrace.UpdateTime = DateTime.Now;
                    stepTrace.UpdateBy = App.User.UserCode;
                    var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                    if (updateResult.Success)
                    {

                        //管子数据
                        var pipeMaterial = stepTrace.PipeMaterial;
                        var pipeLength = stepTrace.PipeLength;
                        var pipeDiameter = stepTrace.PipeDiameter;
                        var pipeThickness = stepTrace.PipeThickness;

                        BllResult plcResult = SendStepTraceToPlc(plc, cutter, true , stepTrace.Id.ToString(), pipeMaterial, pipeLength, pipeDiameter, pipeThickness);
                        if (plcResult.Success)
                        {
                            Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]上料完成成功，对应的任务[{stepTrace.Id}]信息写入设备", LogLevel.Success);
                        }
                        else
                        {
                            stepTrace.StationId = stationId;
                            stepTrace.NextStationId = nextStationId;
                            stepTrace.Status = status;
                            stepTrace.UpdateTime = updateTime;
                            stepTrace.UpdateBy = updateBy;
                            AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                            Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]上料完成失败，对应的任务[{stepTrace.Id}]信息没写入设备，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                        }
                        return plcResult;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理设备【{cutter.Name}】上料完成时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }

        
        /// <summary>
        /// 处理切割请求
        /// </summary>
        /// <param name="cutter"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteCut(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                //找出  套料计划
                var cutPlanResult = AppSession.Dal.GetCommonModelBySqlWithZero<CutPlan>($" select top 1 * from cutPlan where status = 0");
                if (!cutPlanResult.Success)
                {
                    Logger.Log($"查询套料计划出错，原因：{cutPlanResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (cutPlanResult.Data.Count == 0)
                {
                    //没有套料计划，就告诉定长切割机 切完了
                    var WCSAllowCut = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSAllowCut.ToString());
                    WCSAllowCut.Value = CutFlag.回复结束切割.ToString();
                    var plcResult = SendCutToPlc(cutter, CutFlag.回复结束切割, plc);
                    if (plcResult.Success)
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功，告诉设备 结束切割。", LogLevel.Success);
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功失败，写入PLC失败，原因：{plcResult.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
                else
                {
                    var cutPlan = cutPlanResult.Data[0];

                    //根据产品找出前两个工序，第一个是组焊工序，第二个就是下个工序
                    var stepResult = AppSession.Dal.GetCommonModelBySql<Step>($"select top 1 * from step where  order by sequence");
                    if (!stepResult.Success)
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割时候，没有检测到工序信息，原因：{stepResult.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    //向工序表插入一条记录
                    StepTrace stepTrace = new StepTrace();
                    stepTrace.WONumber = cutPlan.WONumber;
                    stepTrace.SerialNumber = cutPlan.Id.ToString();
                    stepTrace.ProductId = cutPlan.Id.Value;
                    stepTrace.ProductCode = cutPlan.ProductCode;
                    stepTrace.LineId = cutter.LineId;
                    stepTrace.StepId = stepResult.Data[0].Id.Value;
                    stepTrace.StationId = cutter.StationId;
                    //stepTrace.NextStepId = stepResult.Data[1].Id.Value;
                    stepTrace.IsNG = false;
                    stepTrace.NGcode = "";
                    stepTrace.IsInvalid = false;
                    stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
                    stepTrace.StationInTime = DateTime.Now;
                    stepTrace.LineInTime = DateTime.Now;
                    stepTrace.CreateTime = DateTime.Now;
                    stepTrace.CreateBy = App.User.UserCode;

                    var insertResult = AppSession.Dal.InsertCommonModel<StepTrace>(stepTrace);
                    if (!insertResult.Success)
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割失败，向数据库插入工序监控失败，原因：{insertResult.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    var RequestTaskId = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestTaskId.ToString());
                    var plcResult = SendCutToPlc(cutter, CutFlag.回复允许切割, plc);
                    if (plcResult.Success)
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功，返回给设备切割数据。", LogLevel.Success);
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        AppSession.Dal.DeleteCommonModelByIds<StepTrace>(new List<int>() { stepTrace.Id.Value });
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功失败，写入PLC失败，原因：{plcResult.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]允许翻转信号时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 执行下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="cutter"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteRequest(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var RequestTaskId = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestTaskId.ToString());
                var convertResult = int.TryParse(RequestTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]下料请求失败，工序任务的id[{RequestTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stepTraceId > 0)
                {
                    var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                    if (stepTrace != null)
                    {
                        if (stepTrace.Status == StepTraceStatus.设备开始生产.GetIndexInt() || stepTrace.Status == StepTraceStatus.设备请求下料.GetIndexInt())
                        {
                            ////当前工序
                            Step step;
                            //下个工序
                            Step nextStep;
                            //所有的下个站台
                            List<StepStation> stepStations;
                            //下个站台对应的设备
                            Equipment nextEquipment = null;

                            using (IDbConnection connection = AppSession.Dal.GetConnection())
                            {
                                connection.Open();
                                // NextStepId为0，表示没有下到工序id，就需要去工序表查询下道工序
                                if (stepTrace.NextStepId == 0)
                                {
                                    step = connection.Get<Step>(stepTrace.StepId);
                                    nextStep = connection.QueryFirstOrDefault<Step>($"select top 1 * from step where and sequence > {step.Sequence} order by sequence");
                                    //更新下个工序
                                    stepTrace.NextStepId = nextStep.Id.Value;
                                    stepTrace.UpdateTime = DateTime.Now;
                                    stepTrace.UpdateBy = App.User.UserCode;
                                    connection.Update<StepTrace>(stepTrace);
                                    return BllResultFactory.Error();
                                }
                                else
                                {
                                    nextStep = connection.Get<Step>(stepTrace.NextStepId);
                                }
                                if (nextStep == null)
                                {
                                    Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，数据错误：不存在产品[{stepTrace.ProductCode}]对应的下个工序！", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                                stepStations = connection.Query<StepStation>($"select * from step_station where stepId = '{nextStep.Id}'").ToList();
                            }
                            if (stepStations.Count < 1)
                            {
                                Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，数据错误：不存在工序[{nextStep.Id}][{nextStep.Code}]对应的站台！", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            //找到工序对应的所有站台
                            var stationList = cutter.StationList.Where(t => stepStations.Exists(x => x.StationId == t.Id)).ToList();
                            // 排除已经被预定的站台
                            stationList.RemoveAll(t => stepTraceList.Exists(x => x.NextStationId == t.Id));
                            if (stationList.Count() == 0)    return BllResultFactory.Error();

                            // 根据站台找到所有对应的设备
                            var tempEquipments = allEquipments.Where(t => stationList.Exists(x => x.Id == t.StationId)).ToList();
                            if (tempEquipments.Count() == 0)  return BllResultFactory.Error();
                            foreach (var item in tempEquipments)
                            {
                                //如果站台请求上料，才可以用 
                                var ArriveResult = item.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveResult.ToString());
                                if (ArriveResult?.Value == MachineMessageFlag.自动请求上料.GetIndexString())
                                {
                                    //增加任务判断
                                    var countResult = AppSession.Dal.GetCommonModelCount<StepTrace>($"where status >{StepTraceStatus.设备开始生产.GetIndexInt()} and status < {StepTraceStatus.任务完成.GetIndexInt()} and nextStationId = {item.StationId}");
                                    if (countResult.Success && countResult.Data < 1)
                                    {
                                        nextEquipment = item;
                                        break;
                                    }
                                }
                            }
                            if (nextEquipment != null && nextEquipment.StationId != 0)
                            {
                                //就生成AGV任务并且插入数据库   小车任务的状态和类型需要重新定义？   
                                CarTask taskCar = new CarTask();
                                //taskCar.StepTraceId = Convert.ToInt32(Step_Trace_Id);
                                taskCar.StepTraceId = stepTrace.Id;
                                taskCar.CarNo = 0;
                                taskCar.Type = CarTaskType.取货和放货.GetIndexInt();
                                taskCar.FromLocation = cutter.SelfAddress.ToString();
                                taskCar.ToLocation = cutter.GoAddress.ToString();
                                taskCar.ReSend = 0;
                                taskCar.Status = TaskCarStatus.Executing.GetIndexInt();
                                //taskCar.IsFlip = Convert.ToInt32(Allow_Flip);

                                //修改工序监控
                                stepTrace.NextStepId = nextStep.Id.Value;
                                stepTrace.NextStationId = nextEquipment.StationId;
                                stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
                                stepTrace.UpdateTime = DateTime.Now;
                                stepTrace.UpdateBy = App.User.UserCode;

                                using (IDbConnection connection = AppSession.Dal.GetConnection())
                                {
                                    IDbTransaction tran = null;
                                    try
                                    {
                                        connection.Open();
                                        tran = connection.BeginTransaction();
                                        connection.Insert<CarTask>(taskCar, transaction: tran);
                                        connection.Update<StepTrace>(stepTrace, transaction: tran);

                                        var sendResult = SendRequestToPlc(plc, cutter, true);
                                        if (sendResult.Success)
                                        {
                                            tran.Commit();
                                            Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 下料请求 成功，任务:{stepTrace.Id}", LogLevel.Success);
                                            return BllResultFactory.Sucess();
                                        }
                                        else
                                        {
                                            tran?.Rollback();
                                            Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 下料请求的时候，写入PLC失败，任务:{stepTrace.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                            return BllResultFactory.Error();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        tran?.Rollback();
                                        Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 下料请求的时候，发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                                        return BllResultFactory.Error();
                                    }
                                }
                            }
                            return BllResultFactory.Sucess();
                        }
                        //if (stepTrace.Status > StepTraceStatus.设备请求下料.GetIndexInt())
                        //{
                        //    var WCS_Step_Trace_Id = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyTaskId.ToString());
                        //    WCS_Step_Trace_Id.Value = "0";
                        //    BllResult plcResult = plc.Write(WCS_Step_Trace_Id);
                        //    if (plcResult.Success)
                        //    {
                        //        Logger.Log($"清除工位[{cutter.StationId}]设备[{cutter.Name}]的任务号{stepTrace.Id}成功", LogLevel.Success);
                        //        return BllResultFactory.Sucess();
                        //    }
                        //    else
                        //    {
                        //        Logger.Log($"清除工位[{cutter.StationId}]设备[{cutter.Name}]的任务号{stepTrace.Id}失败，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                        //        return BllResultFactory.Error();
                        //    }
                        //}
                    }
                    else
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]下料请求失败，找不到未完成的工序任务id[{RequestTaskId.Value}]", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
                else
                {
                    // 如果不存在 已经处理过的任务，但是还在请求下线，说明是手动上的，或是id丢了
                    if (!stepTraceList.Exists(t => t.StationId == cutter.StationId && t.Status >= StepTraceStatus.设备请求下料.GetIndexInt()))
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]下料请求失败，工序跟踪ID为0", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }
    }
}
