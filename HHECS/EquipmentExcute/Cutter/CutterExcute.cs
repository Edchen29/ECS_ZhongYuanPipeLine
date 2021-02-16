using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Machine;
using HHECS.Model.Enums.Station;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;

namespace HHECS.EquipmentExcute.Groove
{
    /// <summary>
    /// 切割机抽象类
    /// </summary>
    public abstract class CutterExcute
    {
        /// <summary>
        /// 用于标记站台的类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 用于可用存储设备列表
        /// </summary>
        public List<Equipment> Equipments { get; set; }

        /// <summary>
        /// 具体的站台实现逻辑
        /// </summary>
        /// <param name="bevels"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public virtual BllResult Excute(List<Equipment> bevels, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                if (bevels.Count == 0)
                {
                    return BllResultFactory.Error($"没有连接到【{this.EquipmentType.Code}】设备，所以不执行处理程序。");
                }
                //找出  未完成的任务
                var stepTraceResult = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where status < {StepTraceStatus.任务完成.GetIndexInt()}");
                if (!stepTraceResult.Success)
                {
                    Logger.Log($"查询【{this.EquipmentType.Name}】类型的设备的任务出错，原因：{stepTraceResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                foreach (var cutter in bevels)
                {
                    var TotalError = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.TotalError.ToString());
                    //有故障就不处理，跳到下个设备
                    if (TotalError.Value == "True")
                    {
                        continue;
                    }

                    //处理 上料
                    var ArriveResult = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveResult.ToString());
                    var WCSACKMessage = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
                    if (ArriveResult != null && ArriveResult != null && WCSACKMessage != null)
                    {
                        //PLC有位置到达，而WCSACK没有回复，则WCS还没有响应
                        if (ArriveResult.Value == MachineMessageFlag.自动请求上料.GetIndexString() && WCSACKMessage.Value == MachineMessageFlag.默认.GetIndexString())
                        {
                            ExcuteArrive(cutter, allEquipments, stepTraceResult.Data, plc);
                        }
                        //PLC没位置到达，而WCSACK有回复，则PLC已经响应但还没有清除
                        if (ArriveResult.Value == MachineMessageFlag.默认.GetIndexString() && WCSACKMessage.Value == MachineMessageFlag.回复允许上料.GetIndexString())
                        {
                            SendArriveToPlc(plc, cutter, false);
                        }
                    }                    

                    //处理 切割请求
                    var AllowCut = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestCut.ToString());
                    var WCSAllowCut = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSAllowCut.ToString());
                    if (AllowCut != null && WCSAllowCut != null)
                    {
                        // PLC有"翻转请求"信号，但ECS没有确认信号，则ECS还没有响应
                        if (AllowCut.Value == CutFlag.自动请求切割.GetIndexString() && WCSAllowCut.Value == CutFlag.默认.GetIndexString())
                        {
                            ExcuteCut(cutter, allEquipments, stepTraceResult.Data, plc);
                        }
                        // PLC没有"翻转请求"信号，但ECS有确认信号，说明PLC已经清除而WCS没有清除 
                        if (AllowCut.Value == CutFlag.默认.GetIndexString() && WCSAllowCut.Value == CutFlag.回复允许切割.GetIndexString())
                        {
                            SendCutToPlc(cutter, CutFlag.默认, plc);
                        }
                    }

                    //处理 下料
                    var RequestMessage = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestMessage.ToString());
                    var WCSReplyMessage = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
                    if (RequestMessage != null&& WCSReplyMessage != null)
                    {
                        //PLC有请求，但ECS没有，则ECS还没有响应
                        if ((RequestMessage.Value == MachineMessageFlag.自动请求下料.GetIndexString() || RequestMessage.Value  == MachineMessageFlag.人工请求下料.GetIndexString()) && WCSReplyMessage.Value== MachineMessageFlag.默认.GetIndexString())
                        {
                            ExcuteRequest(cutter, allEquipments, stepTraceResult.Data, plc);
                        }
                        //PLC没有请求，但ECS有确认信号， 就清除 ECS确认下料完成信号 
                        if (RequestMessage.Value == MachineMessageFlag.默认.GetIndexString() && WCSReplyMessage.Value == MachineMessageFlag.回复允许下料.GetIndexString())
                        {
                            SendRequestToPlc(plc, cutter, false);
                        }
                    }
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"坡口理过程中出现异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 执行上料完成
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="cutter"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteArrive(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);

        

        /// <summary>
        /// 执行切割请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="cutter"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteCut(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);

        /// <summary>
        /// 执行下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="cutter"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteRequest(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);

        /// <summary>
        /// 写入或清除  上料完成 
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="cutter"></param>
        /// <param name="arrive">是否到达，true为到达,false为清除</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendArriveToPlc(IPLC plc, Equipment cutter, bool arrive)
        {
            var operate = arrive ? "写入" : "清除";
            var status= arrive ? StationMessageFlag.地址回复.GetIndexString() : StationMessageFlag.默认.GetIndexString();
            var WCSACKMessage = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
            WCSACKMessage.Value = status.ToString();
            BllResult plcResult = plc.Write(WCSACKMessage);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{cutter.Name}】 ECS确认上料完成 信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{cutter.Name}】 ECS确认上料完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }

        /// <summary>
        /// 写入或清除  下料完成 
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="cutter"></param>
        /// <param name="arrive">是否到达，true为到达,false为清除</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendRequestToPlc(IPLC plc, Equipment cutter, bool request)
        {
            var status = request ? StationMessageFlag.WCSPLCACK.GetIndexString() : StationMessageFlag.默认.GetIndexString();
            var operate = request ? "写入" : "清除";
            var WCSReplyMessage = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
            WCSReplyMessage.Value = status.ToString();
            BllResult plcResult = plc.Write(WCSReplyMessage);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{cutter.Name}】 ECS确认下料完成 信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{cutter.Name}】 ECS确认下料完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }

        /// <summary>
        /// 写入或清除 任务信息
        /// 到达处理
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="cutter"></param>
        /// <param name="product">是否生产，true为生产,false为清除</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendStepTraceToPlc(IPLC plc, Equipment cutter, bool allow, string wcsStepTraceId, int pipeMaterial , int pipeLength, int pipeDiameter, int pipeThickness)
        {
            var operate = allow ? "写入" : "清除";
            var status = allow ? StationMessageFlag.地址回复.GetIndexString() : StationMessageFlag.默认.GetIndexString();
            var WCSReplyMessage = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
            var WCS_Allow_Load = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
            var WCSACKTaskId = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKTaskId.ToString());
            var WCS_Pipe_Material = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMaterial.ToString());
            var WCS_Pipe_Length = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKLength.ToString());
            var WCS_Pipe_Diameter = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKDiameter.ToString());
            var WCS_Pipe_Thickness = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKThickness.ToString());
            WCS_Pipe_Material.Value = pipeMaterial.ToString ();
            WCS_Pipe_Length.Value = pipeLength.ToString();
            WCS_Pipe_Diameter.Value = pipeDiameter.ToString();
            WCS_Pipe_Thickness.Value = pipeThickness.ToString();

            WCSReplyMessage.Value = status.ToString();
            WCSACKTaskId.Value = wcsStepTraceId;
            var propsToWriter = new List<EquipmentProp> { WCS_Allow_Load, WCSACKTaskId, WCS_Pipe_Material, WCS_Pipe_Length, WCS_Pipe_Diameter, WCS_Pipe_Thickness };
            return plc.Writes(propsToWriter);
        }

        


        /// <summary>
        /// 写入或清除  ECS允许焊接机器人翻转，True为写入，False为清除
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="cutter">机器人</param>
        /// <param name="allow">是否允许翻转，true为允许,false为清除</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendCutToPlc(Equipment cutter, CutFlag cutFlag, IPLC plc)
        {
            var operate = cutFlag == CutFlag.默认 ? "清除" : "写入";
            var WCS_Allow_Flip = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSAllowFlip.ToString());
            WCS_Allow_Flip.Value = cutFlag.ToString();
            BllResult plcResult = plc.Write(WCS_Allow_Flip);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{cutter.Name}】ECS允许翻转信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{cutter.Name}】ECS允许翻转信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }

        

    }
}
