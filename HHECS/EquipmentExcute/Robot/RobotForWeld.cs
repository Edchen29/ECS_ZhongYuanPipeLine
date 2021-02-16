//using Dapper;
//using HHECS.Bll;
//using HHECS.EquipmentExcute.Robot.Enums;
//using HHECS.Model.BllModel;
//using HHECS.Model.Common;
//using HHECS.Model.Entities;
//using HHECS.Model.Enums;
//using HHECS.Model.PLCHelper.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;

//namespace HHECS.EquipmentExcute.Robot
//{
//    class RobotForWeld : RobotExcute
//    {
//        #region 旧代码
//        ///// <summary>
//        ///// 焊接机器人的处理
//        ///// </summary>
//        ///// <param name="robot"></param>
//        ///// <param name="allEquipments"></param>
//        ///// <param name="plc"></param>
//        ///// <returns></returns>
//        //public override BllResult ExcuteRequest(List<Equipment> robots, List<Equipment> allEquipments, IPLC plc)
//        //{
//        //    try
//        //    {
//        //        //找出 桁车 未完成的任务
//        //        var stepTraceList = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where status < {StepTraceStatus.任务完成.GetIndexInt()}");
//        //        if (!stepTraceList.Success)
//        //        {
//        //            //如果没有数据
//        //            return BllResultFactory.Error(stepTraceList.Msg);
//        //        }
//        //        foreach (var robot in robots)
//        //        {
//        //            Excute(robot, allEquipments, stepTraceList.Data, plc);
//        //        }
//        //        return BllResultFactory.Sucess();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Logger.Log($"焊接机器人处理过程中出现异常：{ex.Message}", LogLevel.Exception);
//        //        return BllResultFactory.Error($"焊接机器人处理过程中出现异常：{ex.Message}");
//        //    }
//        //}


//        //private BllResult Excute(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
//        //{
//        //    var Ready_OK = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Ready_OK.ToString());
//        //    var Start_OK = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Start_OK.ToString());
//        //    var Start = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Start.ToString());

//        //    //下料准备完成信号
//        //    var Ready_Blank = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Ready_Blank.ToString());
//        //    //ECS回复允许下料信号
//        //    var Blank_Ready = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Blank_Ready.ToString());

//        //    // 上料准备完成信号
//        //    var Ready_Load = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Ready_Load.ToString());
//        //    //ECS回复允许上料信号
//        //    var Load_Ready = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Load_Ready.ToString());

//        //    //机器人请求下料信号 
//        //    var Request_Blank = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Request_Blank.ToString());

//        //    // 上料请求完成
//        //    var Task_OK = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Task_OK.ToString());
//        //    //ECS回复上料完成
//        //    var WCS_Allow_Load = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.WCS_Allow_Load.ToString());

//        //    //焊接机器人翻转请求
//        //    var Allow_Flip = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Allow_Flip.ToString());
//        //    // ECS允许焊接机器人翻转
//        //    var WCS_Allow_Flip = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.WCS_Allow_Flip.ToString());

//        //    if (Ready_OK == null || Start_OK == null || Start == null)
//        //    {
//        //        Logger.Log($"未找到{robot.Name}的 Ready_OK，Start_OK，Start 任务或属性", LogLevel.Error);
//        //        return BllResultFactory.Error();
//        //    }

//        //    //如果 下料准备完成，就直接回复允许
//        //    if (Ready_Blank.Value == "True" && Blank_Ready.Value == "False")
//        //    {
//        //        Blank_Ready.Value = "True";
//        //        BllResult plcResult = plc.Write(Blank_Ready);
//        //        if (plcResult.Success)
//        //        {
//        //            Logger.Log($"处理设备【{robot.Name}】下料准备完成 信号成功", LogLevel.Success);
//        //            return BllResultFactory.Sucess();
//        //        }
//        //        else
//        //        {
//        //            Logger.Log($"处理设备【{robot.Name}】下料准备完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
//        //            return BllResultFactory.Error();
//        //        }
//        //    }

//        //    //如果 上料准备完成，就直接回复允许
//        //    if (Ready_Load.Value == "True" && Load_Ready.Value == "False")
//        //    {
//        //        Load_Ready.Value = "True";
//        //        BllResult plcResult = plc.Write(Load_Ready);
//        //        if (plcResult.Success)
//        //        {
//        //            Logger.Log($"处理设备【{robot.Name}】上料准备完成 信号成功", LogLevel.Success);
//        //        }
//        //        else
//        //        {
//        //            Logger.Log($"处理设备【{robot.Name}】上料准备完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
//        //            return BllResultFactory.Error();
//        //        }
//        //    }

//        //    // 清除 ECS确认上料完成信号 
//        //    if (Task_OK.Value == "False" && WCS_Allow_Load.Value == "True")
//        //    {
//        //        WCS_Allow_Load.Value = "False";
//        //        BllResult plcResult = plc.Write(WCS_Allow_Load);
//        //        if (plcResult.Success)
//        //        {
//        //            Logger.Log($"清除设备【{robot.Name}】 ECS确认上料完成 信号成功", LogLevel.Success);
//        //        }
//        //        else
//        //        {
//        //            Logger.Log($"清除设备【{robot.Name}】 ECS确认上料完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
//        //            return BllResultFactory.Error();
//        //        }
//        //    }

//        //    // 清除 ECS允许翻转信号 
//        //    if (Allow_Flip.Value == "False" && WCS_Allow_Flip.Value == "True")
//        //    {
//        //        WCS_Allow_Flip.Value = "False";
//        //        BllResult plcResult = plc.Write(WCS_Allow_Flip);
//        //        if (plcResult.Success)
//        //        {
//        //            Logger.Log($"清除设备【{robot.Name}】ECS允许翻转信号成功", LogLevel.Success);
//        //            return BllResultFactory.Sucess();
//        //        }
//        //        else
//        //        {
//        //            Logger.Log($"清除设备【{robot.Name}】ECS允许翻转信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
//        //            return BllResultFactory.Error();
//        //        }
//        //    }

//        //    if (Ready_OK.Value == "True")
//        //    {
//        //        if (Start_OK.Value == "True")
//        //        {
//        //            #region 如果设备启动成功，就清除启动信号
//        //            if (Start.Value == "True")
//        //            {
//        //                Start.Value = "False";
//        //                var result = plc.Write(Start);
//        //                if (result.Success)
//        //                {
//        //                    return BllResultFactory.Sucess($"清除设备【{robot.Name}】启动信号成功");
//        //                }
//        //                else
//        //                {
//        //                    Logger.Log($"清除设备【{robot.Name}】启动信号失败", LogLevel.Error);
//        //                    return BllResultFactory.Error();
//        //                }
//        //            }
//        //            #endregion
//        //        }

//        //        #region 处理下料请求
//        //        try
//        //        {
//        //            if (Request_Blank.Value == "True")
//        //            {
//        //                //需要 设备没有翻转，才能处理下料请求。（设备翻转的时候不能上下料）
//        //                if (WCS_Allow_Flip.Value == "False")
//        //                {
//        //                    var Step_Trace_Id = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Step_Trace_Id.ToString());
//        //                    var convertResult = int.TryParse(Step_Trace_Id.Value, out int stepTraceId);
//        //                    if (!convertResult)
//        //                    {
//        //                        Logger.Log($"处理设备【{robot.Name}】下料请求失败，工序任务的id[{Step_Trace_Id.Value}]转化为整数失败！", LogLevel.Error);
//        //                        return BllResultFactory.Error();
//        //                    }
//        //                    if (stepTraceId <= 0)
//        //                    {
//        //                        var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == Convert.ToInt32(Step_Trace_Id.Value));
//        //                        if (stepTrace != null)
//        //                        {
//        //                            if (stepTrace.StationId != robot.StationId)
//        //                            {
//        //                                Logger.Log($"处理设备【{robot.Name}】下料请求失败，工序任务的id[{stepTrace.Id}]对应的站台是[{stepTrace.StationId}]", LogLevel.Error);
//        //                                return BllResultFactory.Error();
//        //                            }
//        //                            //向工序跟踪写入下料请求，等待桁车处理
//        //                            if (stepTrace.Status == StepTraceStatus.设备开始生产.GetIndexInt())
//        //                            {
//        //                                stepTrace.Status = StepTraceStatus.设备请求下料.GetIndexInt();
//        //                                stepTrace.UpdateTime = DateTime.Now;
//        //                                stepTrace.UpdateBy = App.User.UserCode;
//        //                                AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
//        //                                Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求成功，工序任务id[{Step_Trace_Id.Value}]的状态已修改为[{StepTraceStatus.设备请求下料}]", LogLevel.Success);
//        //                                return BllResultFactory.Sucess();
//        //                            }
//        //                            //清除站台的物料信息
//        //                            if (stepTrace.Status > StepTraceStatus.设备请求下料.GetIndexInt())
//        //                            {
//        //                                var TYPE = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.TYPE_Feedback.ToString());
//        //                                var Number = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Number.ToString());
//        //                                Step_Trace_Id.Value = "0";
//        //                                TYPE.Value = "0";
//        //                                Number.Value = "0";
//        //                                var propsToWriter = new List<EquipmentProp> { Step_Trace_Id, TYPE, Number };
//        //                                BllResult plcResult = plc.Writes(propsToWriter);
//        //                                if (plcResult.Success)
//        //                                {
//        //                                    Logger.Log($"清除设备[{robot.Name}]的站台[{robot.StationId}] 物料信息 成功！", LogLevel.Success);
//        //                                    return BllResultFactory.Sucess();
//        //                                }
//        //                                else
//        //                                {
//        //                                    Logger.Log($"清除设备[{robot.Name}]的站台[{robot.StationId}] 物料信息 失败，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
//        //                                    return BllResultFactory.Error();
//        //                                }
//        //                            }
//        //                        }
//        //                        else
//        //                        {
//        //                            Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求失败，找不到未完成的工序任务id[{Step_Trace_Id.Value}]", LogLevel.Error);
//        //                            return BllResultFactory.Error();
//        //                        }
//        //                    }
//        //                    else
//        //                    {
//        //                        // 如果不存在 已经处理过的任务，但是还在请求下线，说明是手动上的，或是id丢了
//        //                        if (!stepTraceList.Exists(t => t.StationId == robot.StationId && t.Status >= StepTraceStatus.设备请求下料.GetIndexInt()))
//        //                        {
//        //                            Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求失败，工序跟踪ID为0", LogLevel.Error);
//        //                            return BllResultFactory.Error();
//        //                        }
//        //                    }
//        //                }
//        //            }
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
//        //            return BllResultFactory.Error();
//        //        }
//        //        #endregion



//        //        #region 处理上料完成
//        //        try
//        //        {
//        //            //如果 设备请求上料任务完成，就修改任务信息，并且回复设备完成
//        //            if (Task_OK.Value == "True")
//        //            {
//        //                //需要 设备没有翻转，才能处理下料请求。（设备翻转的时候不能上下料）
//        //                if (WCS_Allow_Flip.Value == "False")
//        //                {

//        //                    var Step_Trace_Id = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Step_Trace_Id.ToString());
//        //                    var TYPE = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.TYPE_Feedback.ToString());
//        //                    var Number = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Number.ToString());
//        //                    var count = stepTraceList.Count(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == robot.StationId);
//        //                    if (count > 1)
//        //                    {
//        //                        Logger.Log($"处理设备[{robot.Name}]对应的站台[{robot.StationId}]上料完成的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
//        //                        return BllResultFactory.Error();
//        //                    }
//        //                    var stepTrace = stepTraceList.FirstOrDefault(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == robot.StationId);
//        //                    if (stepTrace != null)
//        //                    {
//        //                        //记录旧数据
//        //                        var updateTime = stepTrace.UpdateTime;
//        //                        var updateBy = stepTrace.UpdateBy;
//        //                        //更新数据
//        //                        stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
//        //                        stepTrace.UpdateTime = DateTime.Now;
//        //                        stepTrace.UpdateBy = App.User.UserCode;
//        //                        var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
//        //                        if (updateResult.Success)
//        //                        {
//        //                            Step_Trace_Id.Value = stepTrace.Id.ToString();
//        //                            TYPE.Value = stepTrace.ProductId.ToString();
//        //                            Number.Value = "1";
//        //                            WCS_Allow_Load.Value = "True";
//        //                            var propsToWriter = new List<EquipmentProp> { Step_Trace_Id, TYPE, Number, WCS_Allow_Load };
//        //                            BllResult plcResult = plc.Writes(propsToWriter);
//        //                            if (plcResult.Success)
//        //                            {
//        //                                Logger.Log($"处理设备【{robot.Name}】上料完成信号成功", LogLevel.Success);
//        //                                return BllResultFactory.Sucess();
//        //                            }
//        //                            else
//        //                            {
//        //                                stepTrace.Status = StepTraceStatus.响应放货完成.GetIndexInt();
//        //                                stepTrace.UpdateTime = updateTime;
//        //                                stepTrace.UpdateBy = updateBy;
//        //                                AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
//        //                                Logger.Log($"处理设备【{robot.Name}】上料完成信号失败，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
//        //                                return BllResultFactory.Error();
//        //                            }
//        //                        }
//        //                    }
//        //                }
//        //            }
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            Logger.Log($"处理设备【{robot.Name}】上料完成时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
//        //            return BllResultFactory.Error();
//        //        }
//        //        #endregion

//        //        #region 焊接机器人翻转信号处理
//        //        try
//        //        {
//        //            if (Allow_Flip.Value == "True")
//        //            {
//        //                //如果焊接机器人同时有 请求上料、请求下料，请求上料持续一段时间没反应，就会请求翻转改为请求下料
//        //                var stepTrace = stepTraceList.FirstOrDefault(t => t.NextStationId == robot.StationId);
//        //                if (stepTrace == null)
//        //                {
//        //                    WCS_Allow_Flip.Value = "True";
//        //                    BllResult plcResult = plc.Write(WCS_Allow_Flip);
//        //                    if (plcResult.Success)
//        //                    {
//        //                        Logger.Log($"处理设备【{robot.Name}】允许翻转信号成功", LogLevel.Success);
//        //                        return BllResultFactory.Sucess();
//        //                    }
//        //                    else
//        //                    {
//        //                        Logger.Log($"处理设备【{robot.Name}】允许翻转信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Success);
//        //                        return BllResultFactory.Sucess();
//        //                    }
//        //                }
//        //            }
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            Logger.Log($"处理设备【{robot.Name}】允许翻转信号时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
//        //            return BllResultFactory.Error();
//        //        }
//        //        #endregion
//        //    }
//        //    //else
//        //    //{
//        //    //    return BllResultFactory.Error($"设备【{robot.Name}】未准备就绪！");
//        //    //}
//        //    return BllResultFactory.Sucess();
//        //}
//        #endregion

//        /// <summary>
//        /// 处理设备上料准备完成
//        /// </summary>
//        /// <param name="robot"></param>
//        /// <param name="allEquipments"></param>
//        /// <param name="stepTraceList"></param>
//        /// <param name="plc"></param>
//        /// <returns></returns>
//        public override BllResult ExcuteLoadReady(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
//        {
//            return SendLoadReadyToPlc(true, robot, plc);
//        }

//        /// <summary>
//        /// 处理设备下料准备完成
//        /// </summary>
//        /// <param name="robot"></param>
//        /// <param name="allEquipments"></param>
//        /// <param name="stepTraceList"></param>
//        /// <param name="plc"></param>
//        /// <returns></returns>
//        public override BllResult ExcuteBlankReady(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
//        {
//            return SendBlankReadyToPlc(true, robot, plc);
//        }

//        /// <summary>
//        /// 处理设备下料请求
//        /// </summary>
//        /// <param name="robot"></param>
//        /// <param name="allEquipments"></param>
//        /// <param name="stepTraceList"></param>
//        /// <param name="plc"></param>
//        /// <returns></returns>
//        public override BllResult ExcuteRequest(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
//        {
//            try
//            {
//                var workStation = robot.StationList.Find(t => t.Id == robot.StationId);
//                if (workStation == null)
//                {
//                    Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求时候，不存在ID为[{robot.StationId}]的站台，请检查站台基础数据", LogLevel.Error);
//                    return BllResultFactory.Error();
//                }
//                var productTypeProp = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.TYPE_Feedback.ToString());
//                var result1 = int.TryParse(productTypeProp.Value, out int productType);
//                if (productType < 1)
//                {
//                    Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求失败，工件类型[{productType}]小于1", LogLevel.Error);
//                    return BllResultFactory.Error();
//                }
//                var Step_Trace_Id = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Step_Trace_Id.ToString());
//                var convertResult = int.TryParse(Step_Trace_Id.Value, out int stepTraceId);
//                if (!convertResult)
//                {
//                    Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求失败，工序任务的id[{Step_Trace_Id.Value}]转化为整数失败！", LogLevel.Error);
//                    return BllResultFactory.Error();
//                }
//                if (stepTraceId > 0)
//                {
//                    var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
//                    if (stepTrace != null)
//                    {
//                        if (stepTrace.StationId != robot.StationId)
//                        {
//                            Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求失败，工序任务的id[{stepTrace.Id}]对应的站台是[{stepTrace.StationId}]，请检查任务数据！", LogLevel.Error);
//                            return BllResultFactory.Error();
//                        }
//                        if (stepTrace.WcsProductType != productType)
//                        {
//                            return BllResultFactory.Error($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求失败，根据任务号[{stepTrace.Id}]查找到的任务工件类型为[{stepTrace.WcsProductType}]，设备给出的工件类型是[{productType}]，请检查任务数据");
//                        }
//                        //向工序跟踪写入下料请求，等待桁车处理
//                        if (stepTrace.Status == StepTraceStatus.设备开始生产.GetIndexInt())
//                        {
//                            stepTrace.Status = StepTraceStatus.设备请求下料.GetIndexInt();
//                            stepTrace.UpdateTime = DateTime.Now;
//                            stepTrace.UpdateBy = App.User.UserCode;
//                            AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
//                            Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求成功，工序任务id[{Step_Trace_Id.Value}]的状态已修改为[{StepTraceStatus.设备请求下料}]", LogLevel.Success);
//                            return BllResultFactory.Sucess();
//                        }
//                        //给桁架下发任务后，清除站台的物料信息
//                        if (stepTrace.Status > StepTraceStatus.设备请求下料.GetIndexInt())
//                        {
//                            BllResult plcResult = SendStepTraceToPlc("0", "0", "0", false, robot, plc);
//                            if (plcResult.Success)
//                            {
//                                Logger.Log($"清除设备【{robot.Name}】任务信息成功", LogLevel.Success);
//                            }
//                            else
//                            {
//                                Logger.Log($"清除设备【{robot.Name}】任务信息失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
//                            }
//                            return plcResult;
//                        }
//                    }
//                    else
//                    {
//                        Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求失败，找不到未完成的工序任务id[{Step_Trace_Id.Value}]", LogLevel.Error);
//                        return BllResultFactory.Error();
//                    }
//                }
//                else
//                {
//                    // 如果存在 已经处理过的任务，但是还在请求下线，就不处理
//                    if (stepTraceList.Exists(t => t.StationId == robot.StationId && t.Status >= StepTraceStatus.设备请求下料.GetIndexInt()))
//                    {
//                        return BllResultFactory.Sucess();
//                    }
//                    var createTypeFlagPro = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.ManualSign.ToString());
//                    var result = int.TryParse(createTypeFlagPro.Value, out int createTypeFlag);
//                    if (createTypeFlag == (int)CreateTypeFlag.手动任务)
//                    {
//                        var stepTraceCount = AppSession.Dal.GetCommonModelCount<StepTrace>($"where stationId={robot.StationId} and status < {StepTraceStatus.响应取货完成.GetIndexInt()}");
//                        if (!stepTraceCount.Success)
//                        {
//                            Logger.Log($"[{robot.Name}]的站台[{robot.StationId}]下料请求时候，生成人工确认任务失败，查询任务数据异常，原因：{stepTraceCount.Msg}", LogLevel.Error);
//                            return BllResultFactory.Error();
//                        }
//                        if (stepTraceCount.Data > 0)
//                        {
//                            Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}][{workStation.Name}]下料请求时候，生成人工确认任务失败，当前站台存在[{stepTraceCount.Data}]任务，请先把站台存在的任务完成掉", LogLevel.Error);
//                            return BllResultFactory.Error();
//                        }
//                        StepTrace stepTrace = new StepTrace();
//                        //获取产品信息
//                        var productHeaderResult = AppSession.Dal.GetCommonModelByCondition<ProductHeader>($"where wcsProductType = '{productType}'");
//                        if (!productHeaderResult.Success)
//                        {
//                            Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求时候，根据设备工件类型[{productType}]没有检测到对应的产品信息，原因：{productHeaderResult.Msg}，请检查产品基础数据", LogLevel.Error);
//                            return BllResultFactory.Error();
//                        }
//                        ////获取工位工序表--工位类型
//                        //var stepStationResult = AppSession.Dal.GetCommonModelByCondition<StepStation>($"where stationId = '{robot.StationId}'");
//                        //if (!stepStationResult.Success)
//                        //{
//                        //    Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求时候，根据站台[{robot.StationId}]没有检测到工序工位，原因：{stepStationResult.Msg}，请检查基础数据", LogLevel.Error);
//                        //    return BllResultFactory.Error();
//                        //}
//                        //获取工序表
//                        var stepResult = AppSession.Dal.GetCommonModelBySql<Step>($"select top 1 step.* from step inner join step_station on step.id = step_station.stepId and step.productId = '{productHeaderResult.Data[0].Id}' and step_station.stationId = {robot.StationId} ");
//                        if (!stepResult.Success)
//                        {
//                            Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求时候，根据产品标识[{productHeaderResult.Data[0].Id}]和工位标识[{robot.StationId}]没有查询到工序，原因：{stepResult.Msg}，请检查基础数据", LogLevel.Error);
//                            return BllResultFactory.Error();
//                        }
//                        var step = stepResult.Data[0];
//                        stepTrace.StepId = step.Id.Value;
//                        //工件型号
//                        stepTrace.WcsProductType = productType;
//                        stepTrace.StationId = robot.StationId;
//                        stepTrace.ProductCode = step.ProductCode;
//                        stepTrace.Status = StepTraceStatus.设备请求下料.GetIndexInt();
//                        stepTrace.LineId = robot.LineId;
//                        stepTrace.WONumber = "";
//                        stepTrace.SerialNumber = "";
//                        stepTrace.StationInTime = DateTime.Now;
//                        stepTrace.LineInTime = DateTime.Now;
//                        stepTrace.CreateType = CreateTypeFlag.手动任务.GetIndexInt();
//                        //人工确认任务起始点
//                        stepTrace.ManualStartPoint = robot.Name;
//                        stepTrace.CreateTime = DateTime.Now;
//                        stepTrace.CreateBy = "PLC";

//                        var resultHumanTasks = AppSession.StepTraceService.HumanTasks(stepTrace);
//                        if (!resultHumanTasks.Success)
//                        {
//                            Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求时候，人工确认任务失败，原因：{resultHumanTasks.Msg}", LogLevel.Error);
//                            return BllResultFactory.Error();
//                        }
//                        Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求时候，人工确认任务成功", LogLevel.Success);
//                        return BllResultFactory.Sucess();
//                    }
//                    else
//                    {
//                        Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求失败，工序跟踪ID[{stepTraceId}]小于1，也没有人工确认工件类型。", LogLevel.Error);
//                        return BllResultFactory.Error();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
//                return BllResultFactory.Error();
//            }
//            return BllResultFactory.Sucess();
//        }

//        /// <summary>
//        /// 处理设备上料完成
//        /// </summary>
//        /// <param name="robot"></param>
//        /// <param name="allEquipments"></param>
//        /// <param name="stepTraceList"></param>
//        /// <param name="plc"></param>
//        /// <returns></returns>
//        public override BllResult ExcuteArrive(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
//        {
//            try
//            {
//                var Step_Trace_Id = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Step_Trace_Id.ToString());
//                var count = stepTraceList.Count(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == robot.StationId);
//                if (count > 1)
//                {
//                    Logger.Log($"处理设备[{robot.Name}]对应的站台[{robot.StationId}]上料完成的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
//                    return BllResultFactory.Error();
//                }
//                var stepTrace = stepTraceList.FirstOrDefault(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == robot.StationId);
//                if (stepTrace != null)
//                {
//                    //记录旧数据
//                    var updateTime = stepTrace.UpdateTime;
//                    var updateBy = stepTrace.UpdateBy;
//                    //更新数据
//                    stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
//                    stepTrace.UpdateTime = DateTime.Now;
//                    stepTrace.UpdateBy = App.User.UserCode;
//                    var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
//                    if (updateResult.Success)
//                    {
//                        var plcResult = SendStepTraceToPlc(stepTrace.Id.ToString(), stepTrace.WcsProductType.ToString(), "1", true, robot, plc);
//                        if (plcResult.Success)
//                        {
//                            Logger.Log($"写入设备【{robot.Name}】任务[{stepTrace.Id}]的信息成功", LogLevel.Success);
//                        }
//                        else
//                        {
//                            stepTrace.Status = StepTraceStatus.响应放货完成.GetIndexInt();
//                            stepTrace.UpdateTime = updateTime;
//                            stepTrace.UpdateBy = updateBy;
//                            AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
//                            Logger.Log($"写入设备【{robot.Name}】任务[{stepTrace.Id}]的信息失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
//                        }
//                        return plcResult;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Log($"处理设备【{robot.Name}】上料完成时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
//                return BllResultFactory.Error();
//            }
//            return BllResultFactory.Sucess();
//        }

//        /// <summary>
//        /// 处理翻转请求
//        /// </summary>
//        /// <param name="robot"></param>
//        /// <param name="allEquipments"></param>
//        /// <param name="stepTraceList"></param>
//        /// <param name="plc"></param>
//        /// <returns></returns>
//        public override BllResult AllowFlip(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
//        {
//            try
//            {
//                //如果焊接机器人同时有 请求上料、请求下料，请求上料持续一段时间没反应，就会请求翻转改为请求下料
//                var stepTrace = stepTraceList.FirstOrDefault(t => t.NextStationId == robot.StationId);
//                if (stepTrace == null)
//                {
//                    return SendFlipToPlc(true, robot, plc);
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Log($"处理设备【{robot.Name}】允许翻转信号时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
//                return BllResultFactory.Error();
//            }
//            return BllResultFactory.Sucess();
//        }


//    }
//}
