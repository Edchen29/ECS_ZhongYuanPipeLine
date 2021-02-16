using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.MachineTool
{
    /// <summary>
    /// 机床处理类
    /// </summary>
     public  class MachineToolNormalExcute : MachineToolExcute
    {
        #region 旧代码
        //public override BllResult ExcuteRequest(List<Equipment> machineTools, List<Equipment> allEquipments, IPLC plc)
        //{
        //    try
        //    {
        //        //找出 桁车 未完成的任务
        //        var stepTraceList = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where status < {StepTraceStatus.任务完成.GetIndexInt()}");
        //        foreach (var machineTool in machineTools)
        //        {
        //            Excute(machineTool, allEquipments, stepTraceList.Data, plc);
        //        }
        //        return BllResultFactory.Sucess();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log($"机床处理过程中出现异常：{ex.Message}", LogLevel.Exception);
        //        return BllResultFactory.Error();
        //    }
        //}
        //private BllResult Excute(Equipment machineTool, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        //{
        //    var Request_Load = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Request_Load.ToString());
        //    var Task_OK = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Task_OK.ToString());
        //    var WCS_Allow_Load = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Allow_Load.ToString());
        //    var Request_Product = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Request_Wroking.ToString());
        //    var WCS_Allow_Product = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Wroking.ToString());
        //    var Request_Blank = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Request_Blank.ToString());
        //    //var WCS_Request_Blank = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Request_Blank.ToString());


        //    #region   清除ECS写入的信号                  

        //    if (Task_OK.Value == "False" && WCS_Allow_Load.Value == "True")
        //    {
        //        WCS_Allow_Load.Value = "False";
        //        BllResult plcResult = plc.Write(WCS_Allow_Load);
        //        if (plcResult.Success)
        //        {
        //            Logger.Log($"清除设备【{machineTool.Name}】允许请求上料信号成功", LogLevel.Success);
        //        }
        //        else
        //        {
        //            Logger.Log($"清除设备【{machineTool.Name}】允许请求上料信号失败，写入PLC失败：{plcResult.Msg}", LogLevel.Success);
        //        }
        //    }

        //    if (Request_Product.Value == "False" && WCS_Allow_Product.Value == "True")
        //    {
        //        WCS_Allow_Product.Value = "False";
        //        BllResult plcResult = plc.Write(WCS_Allow_Product);
        //        if (plcResult.Success)
        //        {
        //            Logger.Log($"清除设备【{machineTool.Name}】允许请求生产信号成功", LogLevel.Success);
        //        }
        //        else
        //        {
        //            Logger.Log($"清除设备【{machineTool.Name}】允许请求上料信号失败，写入PLC失败：{plcResult.Msg}", LogLevel.Success);
        //        }
        //    }

        //    #endregion

        //    #region 处理上料完成确认
        //    try
        //    {               
        //        if (Task_OK.Value == "True")
        //        {
        //            //var TYPE = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_TYPE.ToString());
        //            var count = stepTraceList.Count(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == machineTool.StationId);
        //            //if (count == 0)
        //            //{
        //            //    Logger.Log($"处理设备[{machineTool.Name}]对应的站台[{machineTool.StationId}]上料完成的时候，出现数据错误，站台没有对应的任务", LogLevel.Error);
        //            //    return BllResultFactory.Error();
        //            //}
        //            if (count > 1)
        //            {
        //                Logger.Log($"处理设备[{machineTool.Name}]对应的站台[{machineTool.StationId}]上料完成的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
        //                return BllResultFactory.Error();
        //            }
        //            var stepTrace = stepTraceList.FirstOrDefault(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == machineTool.StationId);
        //            if (stepTrace != null)
        //            {
        //                //记录旧数据
        //                var updateTime = stepTrace.UpdateTime;
        //                var updateBy = stepTrace.UpdateBy;
        //                //更新数据
        //                stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
        //                stepTrace.UpdateTime = DateTime.Now;
        //                stepTrace.UpdateBy = App.User.UserCode;
        //                var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
        //                if (updateResult.Success)
        //                {
        //                    //TYPE.Value = stepTrace.ProductId.ToString();
        //                    var WCS_Step_Trace_Id = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Step_Trace_Id.ToString());
        //                    WCS_Allow_Load.Value = "True";
        //                    WCS_Step_Trace_Id.Value = stepTrace.Id.ToString();
        //                    var propsToWriter = new List<EquipmentProp> { WCS_Allow_Load, WCS_Step_Trace_Id };
        //                    BllResult plcResult = plc.Writes(propsToWriter);
        //                    if (plcResult.Success)
        //                    {
        //                        Logger.Log($"处理设备【{machineTool.Name}】上料请求，对应的任务{stepTrace.Id}成功", LogLevel.Success);
        //                        return BllResultFactory.Sucess();
        //                    }
        //                    else
        //                    {
        //                        stepTrace.Status = StepTraceStatus.响应放货完成.GetIndexInt();
        //                        stepTrace.UpdateTime = updateTime;
        //                        stepTrace.UpdateBy = updateBy;
        //                        AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
        //                        Logger.Log($"处理设备【{machineTool.Name}】上料请求，对应的任务{stepTrace.Id}失败，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
        //                        return BllResultFactory.Error();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log($"处理设备【{machineTool.Name}】上料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
        //        return BllResultFactory.Error();
        //    }
        //    #endregion

        //    #region 给加机下达生产指令
        //    try
        //    {
        //        if (Request_Product.Value == "True" && WCS_Allow_Product.Value == "False")
        //        {
        //           //同一个机床的设备code只有最有一位不一样
        //            var startsCode = machineTool.Code.Substring(0, machineTool.Code.Length - 1);
        //            //获取同一个机床的2个站台
        //            var stationIds = allEquipments.Where(t => t.Code.StartsWith(startsCode)).Select(t => t.StationId).ToList();
        //            var readyCount = stepTraceList.Count(t => t.Status == StepTraceStatus.设备开始生产.GetIndexInt() && stationIds.Contains(t.StationId));
        //            var waitCount = stepTraceList.Count(t => stationIds.Contains(t.NextStationId));
        //            //如果机架里面有2个工件，或是没有下一个要来机加的工件（至少有1个工件，才会请求生产），就生产
        //            if (readyCount >= 2 || waitCount == 0)
        //            {                        
        //                WCS_Allow_Product.Value = "True";
        //                BllResult plcResult = plc.Write(WCS_Allow_Product);
        //                if (plcResult.Success)
        //                {
        //                    Logger.Log($"处理设备【{machineTool.Name}】 请求生产 成功", LogLevel.Success);
        //                    return BllResultFactory.Sucess();
        //                }
        //                else
        //                {
        //                    Logger.Log($"处理设备【{machineTool.Name}】 请求生产 失败，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
        //                    return BllResultFactory.Error();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log($"处理设备【{machineTool.Name}】机加工时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
        //        return BllResultFactory.Error();
        //    }
        //    #endregion

        //    #region 处理下料请求
        //    try
        //    {
        //        if (Request_Blank.Value == "True")
        //        {

        //            //var TYPE = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_TYPE.ToString());
        //            //var count = stepTraceList.Count(t => t.Status == StepTraceStatus.设备开始生产.GetIndexInt() && t.StationId == machineTool.StationId);
        //            //if (count == 0)
        //            //{
        //            //    Logger.Log($"处理设备[{machineTool.Name}]对应的站台[{machineTool.StationId}]下料请求的时候，出现数据错误，站台没有对应的任务", LogLevel.Error);
        //            //    return BllResultFactory.Error();
        //            //}
        //            //if (count > 1)
        //            //{
        //            //    Logger.Log($"处理设备[{machineTool.Name}]对应的站台[{machineTool.StationId}]下料请求的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
        //            //    return BllResultFactory.Error();
        //            //}
        //            //var stepTrace = stepTraceList.FirstOrDefault(t => t.StationId == machineTool.StationId);
        //            var Step_Trace_Id = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Step_Trace_ID.ToString());
        //            var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == Convert.ToInt32(Step_Trace_Id.Value));
        //            if (stepTrace != null)
        //            {
        //                if (stepTrace.Status == StepTraceStatus.设备开始生产.GetIndexInt())
        //                {
        //                    //修改工序跟踪
        //                    stepTrace.Status = StepTraceStatus.设备请求下料.GetIndexInt();
        //                    stepTrace.UpdateTime = DateTime.Now;
        //                    stepTrace.UpdateBy = App.User.UserCode;
        //                    var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
        //                    if (updateResult.Success)
        //                    {
        //                        var WCS_Step_Trace_Id = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Step_Trace_Id.ToString());
        //                        WCS_Step_Trace_Id.Value = "0";
        //                        BllResult plcResult = plc.Write(WCS_Step_Trace_Id);
        //                        if (plcResult.Success)
        //                        {
        //                            Logger.Log($"处理设备【{machineTool.Name}】下料请求对应的任务{stepTrace.Id}成功", LogLevel.Success);
        //                            return BllResultFactory.Sucess();
        //                        }
        //                        else
        //                        {
        //                            stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
        //                            stepTrace.UpdateTime = DateTime.Now;
        //                            stepTrace.UpdateBy = App.User.UserCode;
        //                            AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Logger.Log($"处理设备【{machineTool.Name}】下料请求失败，找不到未完成的工序任务id[{Step_Trace_Id.Value}]", LogLevel.Error);
        //                //Logger.Log($"处理设备【{machineTool.Name}】下料请求失败，找不到未完成的机加任务", LogLevel.Error);
        //                return BllResultFactory.Error();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log($"处理设备【{machineTool.Name}】下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
        //        return BllResultFactory.Error();
        //    }
        //    #endregion

        //    return BllResultFactory.Sucess();
        //}

        #endregion

        /// <summary>
        /// 执行上料完成
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machineTool"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteArrive(Equipment machineTool, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var count = stepTraceList.Count(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == machineTool.StationId);
                if (count > 1)
                {
                    Logger.Log($"处理设备[{machineTool.Name}]对应的站台[{machineTool.StationId}]上料完成的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == machineTool.StationId);
                if (stepTrace != null)
                {
                    //记录旧数据
                    var updateTime = stepTrace.UpdateTime;
                    var updateBy = stepTrace.UpdateBy;
                    //更新数据
                    stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
                    stepTrace.UpdateTime = DateTime.Now;
                    stepTrace.UpdateBy = App.User.UserCode;
                    var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                    if (updateResult.Success)
                    {
                        BllResult plcResult = SendStepTraceToPlc(plc, machineTool, true, stepTrace.Id.ToString());
                        if (plcResult.Success)
                        {
                            Logger.Log($"处理设备【{machineTool.Name}】上料完成成功，对应的任务[{stepTrace.Id}]信息写入设备", LogLevel.Success);
                        }
                        else
                        {
                            stepTrace.Status = StepTraceStatus.响应放货完成.GetIndexInt();
                            stepTrace.UpdateTime = updateTime;
                            stepTrace.UpdateBy = updateBy;
                            AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                            Logger.Log($"处理设备【{machineTool.Name}】上料完成失败，对应的任务[{stepTrace.Id}]信息没写入设备，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                        }
                        return plcResult;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理设备【{machineTool.Name}】上料完成时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 执行生产请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machineTool"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteProduct(Equipment machineTool, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                //同一个机床的设备code只有最有一位不一样
                var startsCode = machineTool.Code.Substring(0, machineTool.Code.Length - 1);
                //获取同一个机床的2个站台
                var stationIds = allEquipments.Where(t => t.Code.StartsWith(startsCode)).Select(t => t.StationId).ToList();
                // 如果还有不是 设备开始生产 状态的就等待
                if (stepTraceList.Exists(t => t.Status > StepTraceStatus.设备开始生产.GetIndexInt() && stationIds.Contains(t.StationId)))
                {
                    return BllResultFactory.Sucess();
                }
                var readyCount = stepTraceList.Count(t => t.Status == StepTraceStatus.设备开始生产.GetIndexInt() && stationIds.Contains(t.StationId));
                var waitCount = stepTraceList.Count(t => stationIds.Contains(t.NextStationId));
                //如果机架里面有2个工件，或是没有下一个要来机加的工件（至少有1个工件，才会请求生产），就生产
                if (readyCount >= 2 || waitCount == 0)
                {
                   return SendProductToPlc(plc, machineTool, true);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理设备【{machineTool.Name}】机加工生产请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 执行下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machineTool"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteRequest(Equipment machineTool, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var Step_Trace_Id = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineToolToECSProps.Step_Trace_ID.ToString());
                var convertResult = int.TryParse(Step_Trace_Id.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理设备【{machineTool.Name}】下料请求失败，工序任务的id[{Step_Trace_Id.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stepTraceId > 0)
                {
                    var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == Convert.ToInt32(Step_Trace_Id.Value));
                    if (stepTrace != null)
                    {
                        if (stepTrace.Status == StepTraceStatus.设备开始生产.GetIndexInt())
                        {
                            //修改工序跟踪
                            stepTrace.Status = StepTraceStatus.设备请求下料.GetIndexInt();
                            stepTrace.UpdateTime = DateTime.Now;
                            stepTrace.UpdateBy = App.User.UserCode;
                            AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                            return BllResultFactory.Sucess();
                        }
                        if (stepTrace.Status > StepTraceStatus.设备请求下料.GetIndexInt())
                        {
                            var WCS_Step_Trace_Id = machineTool.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == EcsToMachineToolPorps.WCS_Step_Trace_Id.ToString());
                            WCS_Step_Trace_Id.Value = "0";
                            BllResult plcResult = plc.Write(WCS_Step_Trace_Id);
                            if (plcResult.Success)
                            {
                                Logger.Log($"清除设备【{machineTool.Name}】的任务号{stepTrace.Id}成功", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"清除设备【{machineTool.Name}】的任务号{stepTrace.Id}失败，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                        }
                    }
                    else
                    {
                        Logger.Log($"处理设备【{machineTool.Name}】下料请求失败，找不到未完成的工序任务id[{Step_Trace_Id.Value}]", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
                else
                {
                    // 如果不存在 已经处理过的任务，但是还在请求下线，说明是手动上的，或是id丢了
                    if (!stepTraceList.Exists(t => t.StationId == machineTool.StationId && t.Status >= StepTraceStatus.设备请求下料.GetIndexInt()))
                    {
                        Logger.Log($"处理设备[{machineTool.Name}]的站台[{machineTool.StationId}]下料请求失败，工序跟踪ID为0", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理设备【{machineTool.Name}】下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }











    }
}
