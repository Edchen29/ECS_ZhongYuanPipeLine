using HHECS.Bll;
using HHECS.EquipmentExcute.Robot.Enums;
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
using System.Data;

namespace HHECS.EquipmentExcute.Robot
{
    /// <summary>
    /// 机器人顶层抽象类
    /// </summary>
    public abstract class RobotExcute
    {
        /// <summary>
        /// 用于标记设备的类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 用于可用存储设备列表
        /// </summary>
        public List<Equipment> Equipments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="robots"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public virtual BllResult Excute(List<Equipment> robots, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                if (robots.Count == 0)
                {
                    return BllResultFactory.Error($"没有【{this.EquipmentType.Name}】类型的设备，所以不执行处理程序。");
                }
                //// 如果机床有异常，就不上料，也不下料
                //for (var i = robots.Count - 1; i >= 0; i--)
                //{
                //    var TotalError = robots[i].EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.TotalError.ToString());

                //    if (TotalError.Value == "True")
                //    {
                //        var ArriveMessage = robots[i].EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveMessage.ToString());
                //        if (ArriveMessage != null)
                //        {
                //            ArriveMessage.Value = "False";
                //        }
                //        var Request_Blank = robots[i].EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestMessage.ToString());
                //        if (Request_Blank != null)
                //        {
                //            Request_Blank.Value = "False";
                //        }
                //    }
                //}
                //找出  未完成的任务
                var stepTraceResult = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where status < {StepTraceStatus.任务完成.GetIndexInt()}");
                if (!stepTraceResult.Success)
                {
                    Logger.Log($"查询【{this.EquipmentType.Name}】类型的设备的任务出错，原因：{stepTraceResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                foreach (var robot in robots)
                {
                    //机器人准备完成，才能处理，这个准备完成 是无故障，且在原点。
                    var ArriveMessage = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveMessage.ToString());
                    if (ArriveMessage.Value == true.ToString())
                    {
                        //处理 上料完成
                        var RequestMessage = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestMessage.ToString());
                        var ArriveResult = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveResult.ToString());
                        var WCSReplyMessage = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
                        var WCSACKMessage = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
                        if (RequestMessage != null && ArriveResult != null && WCSReplyMessage != null)
                        {
                            //PLC有位置到达，而WCSACK没有回复，则WCS还没有响应
                            if (RequestMessage.Value == MachineMessageFlag.PLC自动请求上料.GetIndexString() && ArriveResult.Value == MachineMessageFlag.PLC自动请求上料.GetIndexString() && WCSReplyMessage.Value == MachineMessageFlag.默认.GetIndexString())
                            {
                                ExcuteArrive(robot, allEquipments, stepTraceResult.Data, plc);
                            }
                            //PLC没位置到达，而WCSACK有回复，则PLC已经响应但还没有清除
                            if (WCSACKMessage.Value == MachineMessageFlag.WCS回复允许上料.GetIndexString())
                            {
                                SendLoadReadyToPlc(false, robot, plc);
                            }
                        }

                        //处理 下料                        
                        if (RequestMessage != null && WCSACKMessage != null)
                        {
                            //PLC有"下料请求"信号，但ECS没有确认信号，则ECS还没有响应（下料请求不用清除，等工件被取走后，机器人自己清除）
                            //PLC有请求，但ECS没有，则ECS还没有响应
                            if ((RequestMessage.Value == MachineMessageFlag.PLC自动请求下料.GetIndexString() || RequestMessage.Value == MachineMessageFlag.PLC人工请求下料.GetIndexString()) && WCSReplyMessage.Value == MachineMessageFlag.默认.GetIndexString())
                            {
                                 ExcuteRequest(robot, allEquipments, stepTraceResult.Data, plc);
                            }
                            if (WCSReplyMessage.Value == MachineMessageFlag.WCS回复允许下料.GetIndexString())
                            {
                                 SendBlankReadyToPlc(false , robot, plc);
                            }
                        }

                        
                    }
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"组队理过程中出现异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 处理上料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteArrive(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);

        /// <summary>
        /// 处理下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteRequest(Equipment robot, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);


        /// <summary>
        /// 写入或清除 ECS允许上料信号，True为写入，False为清除
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="robot"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendLoadReadyToPlc(bool load_Ready, Equipment robot, IPLC plc, int pipeMaterial, int pipeLength, decimal pipeDiameter, decimal pipeThickness)
        {
            var operate = load_Ready ? "写入" : "清除";
            var status = load_Ready ? MachineMessageFlag.WCS回复允许上料.GetIndexString() : MachineMessageFlag.默认.GetIndexString();
            var WCSACKMessage = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());

            var WCS_Pipe_Material = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMaterial.ToString());
            var WCS_Pipe_Length = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKLength.ToString());
            var WCS_Pipe_Diameter = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKDiameter.ToString());
            var WCS_Pipe_Thickness = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKThickness.ToString());
            WCS_Pipe_Material.Value = pipeMaterial.ToString();
            WCS_Pipe_Length.Value = pipeLength.ToString();
            WCS_Pipe_Diameter.Value = pipeDiameter.ToString();
            WCS_Pipe_Thickness.Value = pipeThickness.ToString();
            WCSACKMessage.Value = status.ToString();
            var propsToWriter = new List<EquipmentProp> { WCSACKMessage,  WCS_Pipe_Material, WCS_Pipe_Length, WCS_Pipe_Diameter, WCS_Pipe_Thickness };

            BllResult plcResult = plc.Writes(propsToWriter);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{robot.Name}】上料准备完成 信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{robot.Name}】上料准备完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }


        /// <summary>
        /// 写入或清除 ECS允许上料信号，True为写入，False为清除
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="robot"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendLoadReadyToPlc(bool load_Ready, Equipment robot, IPLC plc)
        {
            var operate = load_Ready ? "写入" : "清除";
            var status = load_Ready ? MachineMessageFlag.WCS回复允许上料.GetIndexString() : MachineMessageFlag.默认.GetIndexString();
            var WCSACKMessage = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
            WCSACKMessage.Value = status.ToString();

            BllResult plcResult = plc.Write(WCSACKMessage);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{robot.Name}】上料准备完成 信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{robot.Name}】上料准备完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }

        /// <summary>
        /// 写入或清除 ECS允许下料信号，True为写入，False为清除
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="robot"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendBlankReadyToPlc(bool blank_Ready, Equipment robot, IPLC plc)
        {
            var operate = blank_Ready ? "写入" : "清除";
            var status = blank_Ready ? MachineMessageFlag.WCS回复允许下料.GetIndexString() : MachineMessageFlag.默认.GetIndexString();
            var WCSReplyMessage = robot.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
            WCSReplyMessage.Value = status.ToString();
            BllResult plcResult = plc.Write(WCSReplyMessage);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{robot.Name}】下料准备完成 信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{robot.Name}】下料准备完成 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }
    }
}
