using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Machine;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HHECS.EquipmentExcute.Robot
{
    /// <summary>
    /// 组焊机器人
    /// </summary>
    public class RobotForAssembly : RobotExcute
    {
        #region 旧代码
        ///// <summary>
        ///// 组焊机器人的处理
        ///// </summary>
        ///// <param name="robot"></param>
        ///// <param name="allEquipments"></param>
        ///// <param name="plc"></param>
        ///// <returns></returns>
        //public override BllResult ExcuteRequest(List<Equipment> robots, List<Equipment> allEquipments, IPLC plc)
        //{
        //    try
        //    {
        //        //找出 桁车 未完成的任务
        //        var stepTraceList = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where status < {StepTraceStatus.任务完成.GetIndexInt()}");
        //        foreach (var robot in robots)
        //        {
        //            Excute(robot, allEquipments, stepTraceList.Data, plc);
        //        }
        //        return BllResultFactory.Sucess();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log($"组焊机器人处理过程中出现异常：{ex.Message}", LogLevel.Exception, ex);
        //        return BllResultFactory.Error($"组焊机器人处理过程中出现异常：{ex.Message}");
        //    }
        //}


        //private BllResult Excute(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        //{
        //    ////读取所有地址值
        //    //var readResult = plc.Reads(robot.EquipmentProps.Where(t => t.EquipmentTypeTemplate.PropType == EquipmentPropType.PLC_DelayRead.ToString()).ToList());
        //    //if (!readResult.Success)
        //    //{
        //    //    Logger.Log($"组焊机器人处理过程中出现异常：读取地址错误：{readResult.Msg},请检查网络配置", LogLevel.Error);
        //    //    return BllResultFactory.Error($"组焊机器人处理过程中出现异常：读取地址错误：{readResult.Msg},请检查网络配置");
        //    //}

        //    var ready_OK = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Ready_OK.ToString());
        //    var start_OK = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Start_OK.ToString());
        //    //var prg_running = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcInformation.Prg_running.ToString());
        //    var start = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Start.ToString());

        //    //下料准备完成信号
        //    var Ready_Blank = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Ready_Blank.ToString());
        //    //ECS回复允许下料信号
        //    var Blank_Ready = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcRobotStation.Blank_Ready.ToString());
        //    //下料请求信号
        //    var Request_Blank = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.Request_Blank.ToString());

        //    if (ready_OK == null || start_OK == null || start == null)
        //    {
        //        return BllResultFactory.Error($"未找到{robot.Name}的任务或属性");
        //    }

        //    //如果 下料准备完成，就直接回复允许
        //    if (Ready_Blank.Value == "True" && Blank_Ready.Value == "False")
        //    {
        //        Blank_Ready.Value = "True";
        //        BllResult plcResult = plc.Write(Blank_Ready);
        //        if (plcResult.Success)
        //        {
        //            Logger.Log($"处理设备【{robot.Name}】下料准备完成 信号成功", LogLevel.Success);
        //            return BllResultFactory.Sucess();
        //        }
        //        else
        //        {
        //            Logger.Log($"处理设备【{robot.Name}】下料准备完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
        //            return BllResultFactory.Error();
        //        }
        //    }

        //    //// 清除 ECS允许下料信号
        //    //if (Ready_Blank.Value == "False" && Blank_Ready.Value == "True")
        //    //{
        //    //    Blank_Ready.Value = "False";
        //    //    BllResult plcResult = plc.Write(Blank_Ready);
        //    //    if (plcResult.Success)
        //    //    {
        //    //        Logger.Log($"清除设备【{robot.Name}】允许下料信号成功", LogLevel.Success);
        //    //        return BllResultFactory.Sucess();
        //    //    }
        //    //    else
        //    //    {
        //    //        Logger.Log($"清除设备【{robot.Name}】允许下料信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
        //    //        return BllResultFactory.Error();
        //    //    }
        //    //}

        //    if (ready_OK.Value == "True")
        //    {
        //        if (start_OK.Value == "True")
        //        {
        //            #region 如果设备启动成功，就清除启动信号，清除16个物料缓存区物料信息
        //            //if (start.Value == "True")
        //            //{
        //            //    List<EquipmentProp> equipmentProps = new List<EquipmentProp>();
        //            //    start.Value = "False";
        //            //    var A_Type = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.A_Type.ToString());
        //            //    A_Type.Value = "0";
        //            //    var A_Cache_Area = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.A_Cache_Area.ToString());
        //            //    A_Cache_Area.Value = "0";
        //            //    equipmentProps.Add(start);
        //            //    equipmentProps.Add(A_Type);
        //            //    equipmentProps.Add(A_Cache_Area);
        //            //    for (var i = 1; i < 17; i++)
        //            //    {
        //            //        var A_Tier = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Tier" + i.ToString());
        //            //        A_Tier.Value = "0";
        //            //        var A_Row = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Row" + i.ToString());
        //            //        A_Row.Value = "0";
        //            //        var A_Line = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Line" + i.ToString());
        //            //        A_Line.Value = "0";
        //            //        var A_Num = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Num" + i.ToString());
        //            //        A_Num.Value = "0";
        //            //        var B_Tier = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Tier" + i.ToString());
        //            //        B_Tier.Value = "0";
        //            //        var B_Row = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Row" + i.ToString());
        //            //        B_Row.Value = "0";
        //            //        var B_Line = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Line" + i.ToString());
        //            //        B_Line.Value = "0";
        //            //        var B_Num = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Num" + i.ToString());
        //            //        B_Num.Value = "0";
        //            //        var C_Tier = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Tier" + i.ToString());
        //            //        C_Tier.Value = "0";
        //            //        var C_Row = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Row" + i.ToString());
        //            //        C_Row.Value = "0";
        //            //        var C_Line = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Line" + i.ToString());
        //            //        C_Line.Value = "0";
        //            //        var C_Num = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Num" + i.ToString());
        //            //        C_Num.Value = "0";
        //            //        equipmentProps.Add(A_Tier);
        //            //        equipmentProps.Add(A_Row);
        //            //        equipmentProps.Add(A_Line);
        //            //        equipmentProps.Add(A_Num);
        //            //        equipmentProps.Add(B_Tier);
        //            //        equipmentProps.Add(B_Row);
        //            //        equipmentProps.Add(B_Line);
        //            //        equipmentProps.Add(B_Num);
        //            //        equipmentProps.Add(C_Tier);
        //            //        equipmentProps.Add(C_Row);
        //            //        equipmentProps.Add(C_Line);
        //            //        equipmentProps.Add(C_Num);
        //            //    }
        //            //    var plcResult = plc.Writes(equipmentProps);
        //            //    if (plcResult.Success)
        //            //    {
        //            //        return BllResultFactory.Sucess($"清除设备【{robot.Name}】启动信号成功");
        //            //    }
        //            //    else
        //            //    {
        //            //        Logger.Log($"清除设备【{robot.Name}】启动信号失败", LogLevel.Error);
        //            //        return BllResultFactory.Error($"清除设备【{robot.Name}】启动信号失败");
        //            //    }
        //            //}
        //            #endregion
        //        }

        //        #region AGV上料处理
        //        //找到工站对应的缓存位
        //        var locationResult = AppSession.Dal.GetCommonModelByCondition<Location>($"where srmCode = '{robot.Code}'");
        //        if (locationResult.Success)
        //        {
        //            #region 物料呼叫，工位必须有一个未用完的呼叫，如果没有就新增
        //            var callHeaderResult = AppSession.Dal.GetCommonModelByCondition<MaterialCallHeader>($" where needStation = '{robot.StationCode}' and status != '{MaterialCallStatus.deplete.ToString()}'");
        //            foreach (var item in locationResult.Data)
        //            {
        //                //如果呼叫过，就跳过
        //                if (callHeaderResult.Success)
        //                {
        //                    if (callHeaderResult.Data.Exists(t => t.LocationCode == item.Code))
        //                    {
        //                        continue;
        //                    }
        //                }
        //                //新增一条呼叫
        //                MaterialCallHeader materialCallHeader = new MaterialCallHeader();
        //                materialCallHeader.LineId = robot.LineId;
        //                materialCallHeader.LineCode = robot.LineCode;
        //                materialCallHeader.NeedStation = robot.StationCode;
        //                materialCallHeader.LocationCode = item.Code;
        //                materialCallHeader.CallTime = DateTime.Now;
        //                materialCallHeader.Status = MaterialCallStatus.ready.ToString();
        //                materialCallHeader.Mode = "auto";
        //                materialCallHeader.FromPlatform = "PLC";
        //                materialCallHeader.UserCode = App.User.UserCode;
        //                materialCallHeader.CreateTime = DateTime.Now;
        //                materialCallHeader.CreateBy = App.User.UserCode;
        //                materialCallHeader.UpdateTime = DateTime.Now;
        //                materialCallHeader.UpdateBy = App.User.UserCode;
        //                var result = AppSession.Dal.InsertCommonModel<MaterialCallHeader>(materialCallHeader);
        //                if (result.Success == false)
        //                {
        //                    return BllResultFactory.Error(result.Msg);
        //                }
        //            }
        //            #endregion

        //            #region 将配送完成的物料写入PLC

        //            //var A_Incoming = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.A_Incoming.ToString());
        //            //var B_Incoming = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.B_Incoming.ToString());
        //            //var C_Incoming = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.C_Incoming.ToString());

        //            //if (A_Incoming.Value == "False")
        //            //{
        //            //    var distributeHeader = materialDistribute(locationResult.Data, "A");
        //            //    if (distributeHeader.Success)
        //            //    {
        //            //        //修改配送主表的状态为"投入使用"
        //            //        var excuteResult = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送投入使用} where id={distributeHeader.Data.Id}");
        //            //        if (excuteResult.Success)
        //            //        {
        //            //            #region 把物料写入料框类型A
        //            //            List<EquipmentProp> equipmentProps = new List<EquipmentProp>();
        //            //            A_Incoming.Value = "True";
        //            //            var A_Type = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.A_Type.ToString());
        //            //            A_Type.Value = distributeHeader.Data.ProductId.ToString();
        //            //            var A_Cache_Area = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.A_Cache_Area.ToString());
        //            //            A_Cache_Area.Value = distributeHeader.Data.LocationCode;
        //            //            equipmentProps.Add(A_Incoming);
        //            //            equipmentProps.Add(A_Type);
        //            //            equipmentProps.Add(A_Cache_Area);
        //            //            for (var i = 0; i < distributeHeader.Data.distributeDetails.Count; i++)
        //            //            {
        //            //                var A_Tier = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Tier" + (i + 1).ToString());
        //            //                A_Tier.Value = distributeHeader.Data.distributeDetails[i].Layer.ToString();
        //            //                var A_Row = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Row" + (i + 1).ToString());
        //            //                A_Row.Value = distributeHeader.Data.distributeDetails[i].Row.ToString();
        //            //                var A_Line = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Line" + (i + 1).ToString());
        //            //                A_Line.Value = distributeHeader.Data.distributeDetails[i].Column.ToString();
        //            //                var A_Num = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Num" + (i + 1).ToString());
        //            //                A_Num.Value = distributeHeader.Data.distributeDetails[i].BomNum.ToString();
        //            //                equipmentProps.Add(A_Tier);
        //            //                equipmentProps.Add(A_Row);
        //            //                equipmentProps.Add(A_Line);
        //            //                equipmentProps.Add(A_Num);
        //            //            }
        //            //            var plcResult = plc.Writes(equipmentProps);
        //            //            if (!plcResult.Success)
        //            //            {
        //            //                //如果写入PLC失败，就把状态回滚为"配送完成"
        //            //                AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送完成} where id={distributeHeader.Data.Id}");
        //            //            }
        //            //            #endregion
        //            //        }
        //            //    }
        //            //    else
        //            //    {
        //            //        return BllResultFactory.Error($"处理设备【{robot.Name}】对应物料缓存失败，原因:{distributeHeader.Msg}！");
        //            //    }
        //            //}
        //            //if (B_Incoming.Value == "False")
        //            //{
        //            //    var distributeHeader = materialDistribute(locationResult.Data, "B");
        //            //    if (distributeHeader.Success)
        //            //    {
        //            //        //修改配送主表的状态为"投入使用"
        //            //        var excuteResult = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送投入使用} where id={distributeHeader.Data.Id}");
        //            //        if (excuteResult.Success)
        //            //        {
        //            //            #region 把物料写入料框类型B
        //            //            List<EquipmentProp> equipmentProps = new List<EquipmentProp>();
        //            //            B_Incoming.Value = "True";
        //            //            var B_Type = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.B_Type.ToString());
        //            //            B_Type.Value = distributeHeader.Data.ProductId.ToString();
        //            //            var B_Cache_Area = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.B_Cache_Area.ToString());
        //            //            B_Cache_Area.Value = distributeHeader.Data.LocationCode;
        //            //            equipmentProps.Add(B_Incoming);
        //            //            equipmentProps.Add(B_Type);
        //            //            equipmentProps.Add(B_Cache_Area);
        //            //            for (var i = 0; i < distributeHeader.Data.distributeDetails.Count; i++)
        //            //            {
        //            //                var B_Tier = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Tier" + (i + 1).ToString());
        //            //                B_Tier.Value = distributeHeader.Data.distributeDetails[i].Layer.ToString();
        //            //                var B_Row = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Row" + (i + 1).ToString());
        //            //                B_Row.Value = distributeHeader.Data.distributeDetails[i].Row.ToString();
        //            //                var B_Line = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Line" + (i + 1).ToString());
        //            //                B_Line.Value = distributeHeader.Data.distributeDetails[i].Column.ToString();
        //            //                var B_Num = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Num" + (i + 1).ToString());
        //            //                B_Num.Value = distributeHeader.Data.distributeDetails[i].BomNum.ToString();
        //            //                equipmentProps.Add(B_Tier);
        //            //                equipmentProps.Add(B_Row);
        //            //                equipmentProps.Add(B_Line);
        //            //                equipmentProps.Add(B_Num);
        //            //            }
        //            //            var plcResult = plc.Writes(equipmentProps);
        //            //            if (!plcResult.Success)
        //            //            {
        //            //                //如果写入PLC失败，就把状态回滚为"配送完成"
        //            //                AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送完成} where id={distributeHeader.Data.Id}");
        //            //            }
        //            //            #endregion
        //            //        }
        //            //    }
        //            //    else
        //            //    {
        //            //        return BllResultFactory.Error($"处理设备【{robot.Name}】对应物料缓存失败，原因:{distributeHeader.Msg}！");
        //            //    }
        //            //}
        //            //if (C_Incoming.Value == "False")
        //            //{
        //            //    var distributeHeader = materialDistribute(locationResult.Data, "C");
        //            //    if (distributeHeader.Success)
        //            //    {
        //            //        //修改配送主表的状态为"投入使用"
        //            //        var excuteResult = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送投入使用} where id={distributeHeader.Data.Id}");
        //            //        if (excuteResult.Success)
        //            //        {
        //            //            #region 把物料写入料框类型C
        //            //            List<EquipmentProp> equipmentProps = new List<EquipmentProp>();
        //            //            C_Incoming.Value = "True";
        //            //            var C_Type = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.C_Type.ToString());
        //            //            C_Type.Value = distributeHeader.Data.ProductId.ToString();
        //            //            var C_Cache_Area = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.C_Cache_Area.ToString());
        //            //            C_Cache_Area.Value = distributeHeader.Data.LocationCode;
        //            //            equipmentProps.Add(C_Incoming);
        //            //            equipmentProps.Add(C_Type);
        //            //            equipmentProps.Add(C_Cache_Area);
        //            //            for (var i = 0; i < distributeHeader.Data.distributeDetails.Count; i++)
        //            //            {
        //            //                var C_Tier = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Tier" + (i + 1).ToString());
        //            //                C_Tier.Value = distributeHeader.Data.distributeDetails[i].Layer.ToString();
        //            //                var C_Row = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Row" + (i + 1).ToString());
        //            //                C_Row.Value = distributeHeader.Data.distributeDetails[i].Row.ToString();
        //            //                var C_Line = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Line" + (i + 1).ToString());
        //            //                C_Line.Value = distributeHeader.Data.distributeDetails[i].Column.ToString();
        //            //                var C_Num = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Num" + (i + 1).ToString());
        //            //                C_Num.Value = distributeHeader.Data.distributeDetails[i].BomNum.ToString();
        //            //                equipmentProps.Add(C_Tier);
        //            //                equipmentProps.Add(C_Row);
        //            //                equipmentProps.Add(C_Line);
        //            //                equipmentProps.Add(C_Num);
        //            //            }
        //            //            var plcResult = plc.Writes(equipmentProps);
        //            //            if (!plcResult.Success)
        //            //            {
        //            //                //如果写入PLC失败，就把状态回滚为"配送完成"
        //            //                AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送完成} where id={distributeHeader.Data.Id}");
        //            //            }
        //            //            #endregion
        //            //        }
        //            //    }
        //            //    else
        //            //    {
        //            //        return BllResultFactory.Error($"处理设备【{robot.Name}】对应物料缓存失败，原因:{distributeHeader.Msg}！");
        //            //    }
        //            //}
        //            #endregion

        //            #region 如果A、B、C框都到齐了，就可以生产了

        //            //if (A_Incoming.Value == "True" && B_Incoming.Value == "True" && C_Incoming.Value == "True")
        //            //{
        //            //    start.Value = "True";
        //            //    var result = plc.Write(start);
        //            //    if (result.Success)
        //            //    {
        //            //        return BllResultFactory.Sucess($"写入设备【{robot.Name}】启动信号成功");
        //            //    }
        //            //    else
        //            //    {
        //            //        Logger.Log($"写入设备【{robot.Name}】启动信号失败", LogLevel.Error);
        //            //        return BllResultFactory.Error($"写入设备【{robot.Name}】启动信号失败");
        //            //    }
        //            //}
        //            #endregion
        //        }
        //        #endregion

        //        #region 处理下料请求

        //        if (Request_Blank.Value == "True")
        //        {
        //            var TYPE_Feedback = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == RobotPcStation.TYPE_Feedback.ToString()).Value;
        //            var convertResult = int.TryParse(TYPE_Feedback, out int productId);                   
        //            if (!convertResult || productId < 1)
        //            {
        //                Logger.Log($"处理设备【{robot.Name}】下料请求失败，读取工件类型错误，工件类型[{TYPE_Feedback}]不是大于1的整数", LogLevel.Error);
        //                return BllResultFactory.Error();
        //            }
        //            var stepTrace = stepTraceList.FirstOrDefault(t => t.StationId == robot.StationId);
        //            // 如果不存在 本站台的任务，但是还在请求下线，就新增一条任务
        //            if (stepTrace == null)
        //            {
        //                var insertResult = insertStepTrace(robot);
        //                if (insertResult.Success)
        //                {
        //                    Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求成功", LogLevel.Error);
        //                }
        //                return insertResult;
        //            }
        //        }

        //        #endregion
        //    }
        //    return BllResultFactory.Sucess();

        //}
        #endregion

        ///// <summary>
        ///// 处理设备上料准备完成
        ///// </summary>
        ///// <param name="robot"></param>
        ///// <param name="allEquipments"></param>
        ///// <param name="stepTraceList"></param>
        ///// <param name="plc"></param>
        ///// <returns></returns>
        //public override BllResult ExcuteLoadReady(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        //{

        //    //return SendLoadReadyToPlc(true, robot, plc);
        //    #region 已注释的代码 
        //    ////找到工站对应的缓存位
        //    //var locationResult = AppSession.Dal.GetCommonModelByCondition<Location>($"where srmCode = '{robot.Code}' ");
        //    //if (!locationResult.Success)
        //    //{
        //    //    Logger.Log($"处理设备[{robot.Name}]工位[{robot.StationId}]下料准备完成 失败，获取设备对应的上料点位失败", LogLevel.Error);
        //    //    return BllResultFactory.Error();
        //    //}

        //    //#region 如果A、B、C框都到齐了，就可以生产，需要从PLC取值判断，因为C料框是人工配送的

        //    //var A_Incoming = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.A_Incoming.ToString());
        //    //var B_Incoming = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.B_Incoming.ToString());
        //    //var C_Incoming = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.C_Incoming.ToString());

        //    //if (A_Incoming.Value == "True" && B_Incoming.Value == "True" && C_Incoming.Value == "True")
        //    //{
        //    //    #region 如果有"使用中"的呼叫，并且有未完成明细，就根据明细生成任务，然后把任务号和允许抓料信号写入设备，然后返回。
        //    //    //return SendBlankReadyToPlc(true, robot, plc);
        //    //    #endregion
        //    //    #region 如果没有"使用中"的呼叫，或者没有未使用明细，就把呼叫头表状态改为"完成"，并且清除掉写给PLC的料框给定信息。
        //    //    //return SendBlankReadyToPlc(true, robot, plc);
        //    //    #endregion
        //    //}
        //    //#endregion

        //    //#region 判断设备是否有4个有效呼叫（AABB，C料框是手动上料），如果没有就新增呼叫头表，保持一个设备有4个有效呼叫
        //    //var callHeaderResult = AppSession.Dal.GetCommonModelByCondition<MaterialCallHeader>($" where needStation = '{robot.StationCode}' and status < {MaterialCallStatus.物料使用完成.GetIndexString()}");
        //    //foreach (var item in locationResult.Data)
        //    //{
        //    //    //如果呼叫过，就跳过
        //    //    if (callHeaderResult.Success)
        //    //    {
        //    //        if (callHeaderResult.Data.Exists(t => t.LocationCode == item.Code))
        //    //        {
        //    //            continue;
        //    //        }
        //    //    }
        //    //    //新增一条呼叫
        //    //    MaterialCallHeader materialCallHeader = new MaterialCallHeader();
        //    //    materialCallHeader.LineId = robot.LineId;
        //    //    materialCallHeader.LineCode = robot.LineCode;
        //    //    materialCallHeader.NeedStation = robot.StationCode;
        //    //    materialCallHeader.LocationCode = item.Code;
        //    //    materialCallHeader.CallTime = DateTime.Now;
        //    //    materialCallHeader.Status = MaterialCallStatus.ready.ToString();
        //    //    materialCallHeader.Mode = "auto";
        //    //    materialCallHeader.FromPlatform = "PLC";
        //    //    materialCallHeader.UserCode = App.User.UserCode;
        //    //    materialCallHeader.CreateTime = DateTime.Now;
        //    //    materialCallHeader.CreateBy = App.User.UserCode;
        //    //    materialCallHeader.UpdateTime = DateTime.Now;
        //    //    materialCallHeader.UpdateBy = App.User.UserCode;
        //    //    var result = AppSession.Dal.InsertCommonModel<MaterialCallHeader>(materialCallHeader);
        //    //    if (result.Success == false)
        //    //    {
        //    //        return BllResultFactory.Error(result.Msg);
        //    //    }
        //    //}
        //    //#endregion

        //    //#region 如果有"已到达"的呼叫头表，并且料框类型不是"使用中"的料框类型，就把对应的物料信息写入PLC，并且把呼叫头表状态改为"使用中"
        //    //if (A_Incoming.Value == "False")
        //    //{
        //    //    var distributeHeader = materialDistribute(locationResult.Data, "A");
        //    //    if (distributeHeader.Success)
        //    //    {
        //    //        //修改配送主表的状态为"投入使用"
        //    //        var excuteResult = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送投入使用} where id={distributeHeader.Data.Id}");
        //    //        if (excuteResult.Success)
        //    //        {
        //    //            #region 把物料写入料框类型A
        //    //            List<EquipmentProp> equipmentProps = new List<EquipmentProp>();
        //    //            A_Incoming.Value = "True";
        //    //            var A_Type = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.A_Type.ToString());
        //    //            A_Type.Value = distributeHeader.Data.ProductId.ToString();
        //    //            var A_Cache_Area = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.A_Cache_Area.ToString());
        //    //            A_Cache_Area.Value = distributeHeader.Data.LocationCode;
        //    //            equipmentProps.Add(A_Incoming);
        //    //            equipmentProps.Add(A_Type);
        //    //            equipmentProps.Add(A_Cache_Area);
        //    //            for (var i = 0; i < distributeHeader.Data.distributeDetails.Count; i++)
        //    //            {
        //    //                var A_Tier = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Tier" + (i + 1).ToString());
        //    //                A_Tier.Value = distributeHeader.Data.distributeDetails[i].Layer.ToString();
        //    //                var A_Row = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Row" + (i + 1).ToString());
        //    //                A_Row.Value = distributeHeader.Data.distributeDetails[i].Row.ToString();
        //    //                var A_Line = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Line" + (i + 1).ToString());
        //    //                A_Line.Value = distributeHeader.Data.distributeDetails[i].Column.ToString();
        //    //                var A_Num = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "A_Num" + (i + 1).ToString());
        //    //                A_Num.Value = distributeHeader.Data.distributeDetails[i].BomNum.ToString();
        //    //                equipmentProps.Add(A_Tier);
        //    //                equipmentProps.Add(A_Row);
        //    //                equipmentProps.Add(A_Line);
        //    //                equipmentProps.Add(A_Num);
        //    //            }
        //    //            var plcResult = plc.Writes(equipmentProps);
        //    //            if (!plcResult.Success)
        //    //            {
        //    //                //如果写入PLC失败，就把状态回滚为"配送完成"
        //    //                AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送完成} where id={distributeHeader.Data.Id}");
        //    //            }
        //    //            #endregion
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        return BllResultFactory.Error($"处理设备【{robot.Name}】对应物料缓存失败，原因:{distributeHeader.Msg}！");
        //    //    }
        //    //}
        //    //if (B_Incoming.Value == "False")
        //    //{
        //    //    var distributeHeader = materialDistribute(locationResult.Data, "B");
        //    //    if (distributeHeader.Success)
        //    //    {
        //    //        //修改配送主表的状态为"投入使用"
        //    //        var excuteResult = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送投入使用} where id={distributeHeader.Data.Id}");
        //    //        if (excuteResult.Success)
        //    //        {
        //    //            #region 把物料写入料框类型B
        //    //            List<EquipmentProp> equipmentProps = new List<EquipmentProp>();
        //    //            B_Incoming.Value = "True";
        //    //            var B_Type = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.B_Type.ToString());
        //    //            B_Type.Value = distributeHeader.Data.ProductId.ToString();
        //    //            var B_Cache_Area = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.B_Cache_Area.ToString());
        //    //            B_Cache_Area.Value = distributeHeader.Data.LocationCode;
        //    //            equipmentProps.Add(B_Incoming);
        //    //            equipmentProps.Add(B_Type);
        //    //            equipmentProps.Add(B_Cache_Area);
        //    //            for (var i = 0; i < distributeHeader.Data.distributeDetails.Count; i++)
        //    //            {
        //    //                var B_Tier = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Tier" + (i + 1).ToString());
        //    //                B_Tier.Value = distributeHeader.Data.distributeDetails[i].Layer.ToString();
        //    //                var B_Row = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Row" + (i + 1).ToString());
        //    //                B_Row.Value = distributeHeader.Data.distributeDetails[i].Row.ToString();
        //    //                var B_Line = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Line" + (i + 1).ToString());
        //    //                B_Line.Value = distributeHeader.Data.distributeDetails[i].Column.ToString();
        //    //                var B_Num = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "B_Num" + (i + 1).ToString());
        //    //                B_Num.Value = distributeHeader.Data.distributeDetails[i].BomNum.ToString();
        //    //                equipmentProps.Add(B_Tier);
        //    //                equipmentProps.Add(B_Row);
        //    //                equipmentProps.Add(B_Line);
        //    //                equipmentProps.Add(B_Num);
        //    //            }
        //    //            var plcResult = plc.Writes(equipmentProps);
        //    //            if (!plcResult.Success)
        //    //            {
        //    //                //如果写入PLC失败，就把状态回滚为"配送完成"
        //    //                AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送完成} where id={distributeHeader.Data.Id}");
        //    //            }
        //    //            #endregion
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        return BllResultFactory.Error($"处理设备【{robot.Name}】对应物料缓存失败，原因:{distributeHeader.Msg}！");
        //    //    }
        //    //}
        //    //if (C_Incoming.Value == "False")
        //    //{
        //    //    var distributeHeader = materialDistribute(locationResult.Data, "C");
        //    //    if (distributeHeader.Success)
        //    //    {
        //    //        //修改配送主表的状态为"投入使用"
        //    //        var excuteResult = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送投入使用} where id={distributeHeader.Data.Id}");
        //    //        if (excuteResult.Success)
        //    //        {
        //    //            #region 把物料写入料框类型C
        //    //            List<EquipmentProp> equipmentProps = new List<EquipmentProp>();
        //    //            C_Incoming.Value = "True";
        //    //            var C_Type = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.C_Type.ToString());
        //    //            C_Type.Value = distributeHeader.Data.ProductId.ToString();
        //    //            var C_Cache_Area = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PcPlcAgv.C_Cache_Area.ToString());
        //    //            C_Cache_Area.Value = distributeHeader.Data.LocationCode;
        //    //            equipmentProps.Add(C_Incoming);
        //    //            equipmentProps.Add(C_Type);
        //    //            equipmentProps.Add(C_Cache_Area);
        //    //            for (var i = 0; i < distributeHeader.Data.distributeDetails.Count; i++)
        //    //            {
        //    //                var C_Tier = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Tier" + (i + 1).ToString());
        //    //                C_Tier.Value = distributeHeader.Data.distributeDetails[i].Layer.ToString();
        //    //                var C_Row = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Row" + (i + 1).ToString());
        //    //                C_Row.Value = distributeHeader.Data.distributeDetails[i].Row.ToString();
        //    //                var C_Line = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Line" + (i + 1).ToString());
        //    //                C_Line.Value = distributeHeader.Data.distributeDetails[i].Column.ToString();
        //    //                var C_Num = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "C_Num" + (i + 1).ToString());
        //    //                C_Num.Value = distributeHeader.Data.distributeDetails[i].BomNum.ToString();
        //    //                equipmentProps.Add(C_Tier);
        //    //                equipmentProps.Add(C_Row);
        //    //                equipmentProps.Add(C_Line);
        //    //                equipmentProps.Add(C_Num);
        //    //            }
        //    //            var plcResult = plc.Writes(equipmentProps);
        //    //            if (!plcResult.Success)
        //    //            {
        //    //                //如果写入PLC失败，就把状态回滚为"配送完成"
        //    //                AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"update material_distribute_task_header set status={DistributeStatus.配送完成} where id={distributeHeader.Data.Id}");
        //    //            }
        //    //            #endregion
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        return BllResultFactory.Error($"处理设备【{robot.Name}】对应物料缓存失败，原因:{distributeHeader.Msg}！");
        //    //    }
        //    //}
        //    //#endregion

        //    //return BllResultFactory.Error();
        //    #endregion
        //}

        /// <summary>
        /// 处理设备上料完成
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteArrive(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            //ECS修改工序监控的当前位置，然后响应上料到达
            try
            {
                var count = stepTraceList.Count(t => t.StationId == robot.StationId);
                if (count > 1)
                {
                    Logger.Log($"处理设备[{robot.Name}]对应的站台[{robot.StationId}]上料完成的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.StationId == robot.StationId);
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
                        //管子数据
                        var pipeMaterial = stepTrace.PipeMaterial;
                        var pipeLength = stepTrace.PipeLength;
                        var pipeDiameter = stepTrace.PipeDiameter;
                        var pipeThickness = stepTrace.PipeThickness;

                        BllResult plcResult = SendLoadReadyToPlc(true, robot, plc, pipeMaterial.Value, pipeLength.Value, pipeDiameter.Value, pipeThickness.Value);
                        if (plcResult.Success)
                        {
                            Logger.Log($"处理设备【{robot.Name}】上料完成成功，对应的任务[{stepTrace.Id}]信息写入设备", LogLevel.Success);
                        }
                        else
                        {
                            stepTrace.Status = StepTraceStatus.响应放货完成.GetIndexInt();
                            stepTrace.UpdateTime = updateTime;
                            stepTrace.UpdateBy = updateBy;
                            AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                            Logger.Log($"处理设备【{robot.Name}】上料完成失败，对应的任务[{stepTrace.Id}]信息没写入设备，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                        }
                        return plcResult;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理设备【{robot.Name}】上料完成时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
        }


        ///// <summary>
        ///// 处理设备下料准备完成
        ///// </summary>
        ///// <param name="robot"></param>
        ///// <param name="allEquipments"></param>
        ///// <param name="stepTraceList"></param>
        ///// <param name="plc"></param>
        ///// <returns></returns>
        //public override BllResult ExcuteBlankReady(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        //{
        //    return SendBlankReadyToPlc(true, robot, plc);
        //}

        /// <summary>
        /// 处理设备请求下线
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteRequest(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var Step_Trace_Id = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestTaskId.ToString());
                var convertResult = int.TryParse(Step_Trace_Id.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理设备【{robot.Name}】下料请求失败，工序任务的id[{Step_Trace_Id.Value}]转化为整数失败！", LogLevel.Error);
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
                            var result = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                            if (result.Success)
                            {
                                //回复允许下料
                                return SendBlankReadyToPlc(true, robot, plc);
                            }
                            return BllResultFactory.Sucess();
                        }
                        if (stepTrace.Status > StepTraceStatus.设备请求下料.GetIndexInt())
                        {
                            var WCS_Step_Trace_Id = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyTaskId.ToString());
                            WCS_Step_Trace_Id.Value = "0";
                            BllResult plcResult = plc.Write(WCS_Step_Trace_Id);
                            if (plcResult.Success)
                            {
                                Logger.Log($"清除设备【{robot.Name}】的任务号{stepTrace.Id}成功", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"清除设备【{robot.Name}】的任务号{stepTrace.Id}失败，写入PLC失败：{plcResult.Msg}", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                        }
                    }
                    else
                    {
                        Logger.Log($"处理设备【{robot.Name}】下料请求失败，找不到未完成的工序任务id[{Step_Trace_Id.Value}]", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
                else
                {
                    // 如果不存在 已经处理过的任务，但是还在请求下线，说明是手动上的，或是id丢了
                    if (!stepTraceList.Exists(t => t.StationId == robot.StationId && t.Status >= StepTraceStatus.设备请求下料.GetIndexInt()))
                    {
                        Logger.Log($"处理设备[{robot.Name}]的站台[{robot.StationId}]下料请求失败，工序跟踪ID为0", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"处理设备【{robot.Name}】下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
            return BllResultFactory.Sucess();
            
            
        }



        ///// <summary>
        ///// 处理翻转请求
        ///// </summary>
        ///// <param name="robot"></param>
        ///// <param name="allEquipments"></param>
        ///// <param name="stepTraceList"></param>
        ///// <param name="plc"></param>
        ///// <returns></returns>
        //public override BllResult AllowFlip(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        //{
        //    return BllResultFactory.Error($"设备[{robot.Name}][{robot.Code}]不处理翻转");
        //}


    }
}
