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

namespace HHECS.EquipmentExcute.Machine
{
    /// <summary>
    /// 机器处理基类
    /// </summary>
    public abstract class MachineExcute
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
        /// 具体的机器处理实现逻辑
        /// </summary>
        /// <param name="machines"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public virtual BllResult Excute(List<Equipment> machines, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                if (machines.Count == 0)
                {
                    return BllResultFactory.Error($"没有连接到[{this.EquipmentType.Name}]类型设备，所以不执行处理程序。");
                }

                //找出  未完成的任务
                var stepTraceResult = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where status < {StepTraceStatus.任务完成.GetIndexInt()}");
                if (!stepTraceResult.Success)
                {
                    Logger.Log($"查询[{this.EquipmentType.Name}]类型设备的任务出错，原因：{stepTraceResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                foreach (var machine in machines)
                {
                    var OperationModel = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.OperationModel.ToString());
                    var TotalError = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.TotalError.ToString());
                    //不是 故障且联机，就不处理，跳到下个设备
                    if (OperationModel?.Value != OperationModelFlag.联机.GetIndexString() || TotalError?.Value != TotalErrorFlag.无故障.GetIndexString())
                    {
                        continue;
                    }

                    #region 处理 上料请求
                    var ArriveMessage = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveMessage.ToString());
                    var ArriveResult = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveResult.ToString());
                    var WCSACKMessage = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
                    //PLC有位置到达，而WCSACK没有回复，则WCS还没有响应
                    if (ArriveMessage?.Value == MachineMessageFlag.PLC自动请求上料.GetIndexString() && ArriveResult?.Value == MachineResultFlag.已到达.GetIndexString() && WCSACKMessage?.Value == MachineMessageFlag.默认.GetIndexString())
                    {
                        ExcuteArrive(machine, allEquipments, stepTraceResult.Data, plc);
                    }
                    //PLC没位置到达，而WCSACK有回复，则PLC已经响应但还没有清除
                    if (ArriveMessage?.Value == MachineMessageFlag.默认.GetIndexString() && WCSACKMessage?.Value == MachineMessageFlag.WCS回复允许上料.GetIndexString())
                    {
                        ExcuteArriveClear(machine, plc);
                    }
                    #endregion

                    #region 处理 下料请求
                    var RequestMessage = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestMessage.ToString());
                    var WCSReplyMessage = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
                    //PLC有请求，但ECS没有，则ECS还没有响应
                    if ((RequestMessage?.Value == MachineMessageFlag.PLC自动请求下料.GetIndexString() || RequestMessage?.Value == MachineMessageFlag.PLC人工请求下料.GetIndexString()) && WCSReplyMessage?.Value == MachineMessageFlag.默认.GetIndexString())
                    {
                        ExcuteRequest(machine, allEquipments, stepTraceResult.Data, plc);
                    }
                    //PLC没有请求，但ECS有确认信号， 就清除 ECS确认下料完成信号 
                    if (RequestMessage?.Value == MachineMessageFlag.默认.GetIndexString() && WCSReplyMessage?.Value == MachineMessageFlag.WCS回复允许下料.GetIndexString())
                    {
                        ExcuteRequestClear(machine, plc);
                    }
                    #endregion

                    #region 处理 定长切割请求
                    var RequestCut = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestCut.ToString());
                    var WCSAllowCut = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSAllowCut.ToString());
                    // PLC有"翻转请求"信号，但ECS没有确认信号，则ECS还没有响应
                    if (RequestCut?.Value == CutFlag.PLC自动请求切割.GetIndexString() && WCSAllowCut?.Value == CutFlag.默认.GetIndexString())
                    {
                        ExcuteCut(machine, allEquipments, stepTraceResult.Data, plc);
                    }
                    // PLC没有"翻转请求"信号，但ECS有确认信号，说明PLC已经清除而WCS没有清除 
                    if (RequestCut?.Value == CutFlag.默认.GetIndexString() && (WCSAllowCut?.Value == CutFlag.WCS回复允许切割.GetIndexString() || WCSAllowCut?.Value == CutFlag.WCS回复结束切割.GetIndexString()))
                    {
                        ExcuteCutClear(machine, plc);
                    }
                    #endregion

                    #region 处理 打标请求
                    var RequestPint = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestPint.ToString());
                    var WCSPint = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSPint.ToString());
                    // PLC有"翻转请求"信号，但ECS没有确认信号，则ECS还没有响应
                    if (RequestPint?.Value == PrintFlag.PLC请求打印.GetIndexString() && WCSPint?.Value == PrintFlag.默认.GetIndexString())
                    {
                        ExcutePint(machine, allEquipments, stepTraceResult.Data, plc);
                    }
                    // PLC没有"翻转请求"信号，但ECS有确认信号，说明PLC已经清除而WCS没有清除 
                    if (RequestPint?.Value == PrintFlag.默认.GetIndexString() && WCSPint?.Value == PrintFlag.WCS回复打印.GetIndexString())
                    {
                        ExcutePintClear(machine, plc);
                    }
                    #endregion

                    # region 处理 翻转请求
                    var Allow_Flip = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestFlip.ToString());
                    var WCS_Allow_Flip = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSAllowFlip.ToString());
                    // PLC有"翻转请求"信号，但ECS没有确认信号，则ECS还没有响应
                    if (Allow_Flip?.Value == FlipFlag.PLC自动请求翻转.GetIndexString() && WCS_Allow_Flip?.Value == FlipFlag.默认.GetIndexString())
                    {
                        ExcuteFlip(machine, allEquipments, stepTraceResult.Data, plc);
                    }
                    // PLC没有"翻转请求"信号，但ECS有确认信号，说明PLC已经清除而WCS没有清除 
                    if (Allow_Flip?.Value == FlipFlag.默认.GetIndexString() && WCS_Allow_Flip?.Value == FlipFlag.WCS回复允许翻转.GetIndexString())
                    {
                        ExcuteFlipClear(machine, plc);
                    }
                    #endregion

                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"处理类型[{EquipmentType.Name}]设备过程中出现异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 处理 上料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public virtual BllResult ExcuteArrive(Equipment machine, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            Logger.Log($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]上料请求失败，子类没有重写处理", LogLevel.Error);
            return BllResultFactory.Error();
        }

        /// <summary>
        /// 处理 下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public virtual BllResult ExcuteRequest(Equipment machine, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            Logger.Log($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]下料请求失败，子类没有重写处理", LogLevel.Error);
            return BllResultFactory.Error();
        }

        /// <summary>
        /// 处理 定长切割请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public virtual BllResult ExcuteCut(Equipment machine, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            Logger.Log($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]切割请求失败，子类没有重写处理", LogLevel.Error);
            return BllResultFactory.Error();
        }

        /// <summary>
        /// 处理 打标请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public virtual BllResult ExcutePint(Equipment machine, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            Logger.Log($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]打标请求失败，子类没有重写处理", LogLevel.Error);
            return BllResultFactory.Error();
        }

        /// <summary>
        /// 处理 翻转请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public virtual BllResult ExcuteFlip(Equipment machine, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            Logger.Log($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]翻转请求失败，子类没有重写处理", LogLevel.Error);
            return BllResultFactory.Error();
        }


        /// <summary>
        /// 位置到达回复
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="plc"></param>
        /// <param name="message"></param>
        /// <param name="loadStatus"></param>
        /// <param name="number"></param>
        /// <param name="backup"></param>
        /// <returns></returns>
        public BllResult SendAckToPlc(Equipment machine, IPLC plc, MachineMessageFlag messageFlag,  string number, string taskId, string barcode, string requestProductId, string pipeMaterial, string pipeLength, string pipeDiameter, string pipeThickness, string address)
        {
            pipeDiameter = (Convert.ToInt32(decimal.Parse(pipeDiameter) * 10)).ToString();
            pipeThickness = (Convert.ToInt32(decimal.Parse(pipeThickness) * 10)).ToString();

            List<EquipmentProp> props = null;
            var prop1 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
            prop1.Value = messageFlag.GetIndexString();
            var prop2 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKNumber.ToString());
            prop2.Value = number;
            var prop3 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKTaskId.ToString());
            prop3.Value = taskId;
            var prop4 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKBarcode.ToString());
            prop4.Value = barcode;
            var prop5 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKProductId.ToString());
            prop5.Value = requestProductId;
            var prop6 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMaterial.ToString());
            prop6.Value = pipeMaterial;
            var prop7 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKLength.ToString());
            prop7.Value = pipeLength;
            var prop8 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKDiameter.ToString());
            prop8.Value = pipeDiameter;
            var prop9 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKThickness.ToString());
            prop9.Value = pipeThickness;
            var prop10 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyAddress.ToString());
            prop10.Value = address;
            if (messageFlag == MachineMessageFlag.默认)
            {
                props = new List<EquipmentProp>() { prop1, prop2, prop3, prop4, prop5, prop6, prop7, prop8, prop9, prop10 };
            }
            else
            {
                props = new List<EquipmentProp>() { prop2, prop3, prop4, prop5, prop6, prop7, prop8, prop9, prop10, prop1 };
            }
            return plc.Writes(props);
        }


        /// <summary>
        /// 位置到达清除
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="machine"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private BllResult ExcuteArriveClear(Equipment machine, IPLC plc)
        {
            var result = SendAckToPlc(machine, plc, MachineMessageFlag.默认, "0", "0", "", "0", "0", "0", "0", "0", "0");
            if (result.Success)
            {
                return BllResultFactory.Sucess($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应位置到达完成后，清除WCS地址区成功");
            }
            else
            {
                return BllResultFactory.Error($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应位置到达完成后，清除WCS地址区失败");
            }
        }


        /// <summary>
        /// 地址请求回复
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="plc"></param>
        /// <param name="message"></param>
        /// <param name="loadStatus"></param>
        /// <param name="number"></param>
        /// <param name="barcode"></param>
        /// <param name="weight"></param>
        /// <param name="lenght"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="address"></param>
        /// <param name="backup"></param>
        /// <returns></returns>
        public BllResult SendAddressReplyToPlc(Equipment machine, IPLC plc, MachineMessageFlag messageFlag, string number, string taskId, string barcode, string requestProductId, string pipeMaterial, string pipeLength, string pipeDiameter, string pipeThickness, string address)
        {
            pipeDiameter = (Convert.ToInt32(decimal.Parse(pipeDiameter) * 10)).ToString();
            pipeThickness = (Convert.ToInt32(decimal.Parse(pipeThickness) * 10)).ToString();

            List<EquipmentProp> props = null;
            var prop1 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
            prop1.Value = messageFlag.GetIndexString();
            var prop2 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyNumber.ToString());
            prop2.Value = number;
            var prop3 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyTaskId.ToString());
            prop3.Value = taskId;
            var prop4 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyBarcode.ToString());
            prop4.Value = barcode;
            var prop5 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyProductId.ToString());
            prop5.Value = requestProductId;
            var prop6 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMaterial.ToString());
            prop6.Value = pipeMaterial;
            var prop7 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyLength.ToString());
            prop7.Value = pipeLength;
            var prop8 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyDiameter.ToString());
            prop8.Value = pipeDiameter;
            var prop9 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyThickness.ToString());
            prop9.Value = pipeThickness;
            var prop10 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyAddress.ToString());
            prop10.Value = address;

            if (messageFlag == MachineMessageFlag.默认)
            {
                props = new List<EquipmentProp> { prop1, prop2, prop3, prop4, prop5, prop6, prop7, prop8, prop9, prop10 };
            }
            else
            {
                props = new List<EquipmentProp> { prop2, prop3, prop4, prop5, prop6, prop7, prop8, prop9, prop10, prop1 };
            }
            return plc.Writes(props);
        }


        /// <summary>
        /// 地址请求清除
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteRequestClear(Equipment machine, IPLC plc)
        {
            var result = SendAddressReplyToPlc(machine, plc, MachineMessageFlag.默认, "0", "0", "", "0", "0", "0", "0", "0", "0");
            if (result.Success)
            {
                return BllResultFactory.Sucess($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应地址请求完成后，清除WCS地址区成功");
            }
            else
            {
                return BllResultFactory.Error($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应地址请求完成后，清除WCS地址区失败");
            }
        }


        /// <summary>
        /// 切割请求处理
        /// </summary>
        /// <param name="machine">切割机</param>
        /// <param name="cutFlag">是否允许切割</param>
        /// <param name="wcsCutTaskId">管段任务号</param>
        /// <param name="wcsCutMaterial">管段材料</param>
        /// <param name="wcsCutLength">管段长度</param>
        /// <param name="wcsCutDiameter">管段直径</param>
        /// <param name="wcsCutThickness">管段壁厚</param>
        /// <param name="plc"></param>
        /// <returns></returns>
        protected BllResult SendCutToPlc(Equipment machine, IPLC plc, CutFlag cutFlag, string wcsCutTaskId, string wcsCutMaterial, string wcsCutLength, string wcsCutDiameter, string wcsCutThickness)
        {
            wcsCutDiameter = (Convert.ToInt32(decimal.Parse(wcsCutDiameter) * 10)).ToString();
            wcsCutThickness = (Convert.ToInt32(decimal.Parse(wcsCutThickness) * 10)).ToString();

            List<EquipmentProp> props = null;
            var prop1 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSAllowCut.ToString());
            prop1.Value = cutFlag.GetIndexString();
            var prop2 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSCutTaskId.ToString());
            prop2.Value = wcsCutTaskId;
            var prop3 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSCutMaterial.ToString());
            prop3.Value = wcsCutMaterial;
            var prop4 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSCutLength.ToString());
            prop4.Value = wcsCutLength;
            var prop5 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSCutDiameter.ToString());
            prop5.Value = wcsCutDiameter;
            var prop6 = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSCutThickness.ToString());
            prop6.Value = wcsCutThickness;

            if (cutFlag == CutFlag.默认)
            {
                props = new List<EquipmentProp> { prop1, prop2, prop3, prop4, prop5, prop6 };
            }
            else
            {
                props = new List<EquipmentProp> { prop2, prop3, prop4, prop5, prop6, prop1 };
            }
            BllResult plcResult = plc.Writes(props);
            return plcResult;
        }

        /// <summary>
        /// 切割请求清除
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteCutClear(Equipment machine, IPLC plc)
        {
            var result = SendCutToPlc(machine, plc, CutFlag.默认, "0", "0", "0", "0", "0");
            if (result.Success)
            {
                return BllResultFactory.Sucess($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应切割请求完成后，清除WCS响应切割信息成功");
            }
            else
            {
                return BllResultFactory.Error($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应切割请求完成后，清除WCS响应切割信息失败");
            }
        }

        /// <summary>
        /// 打标请求处理
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="flipFlag"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        protected BllResult SendPintToPlc(Equipment machine, IPLC plc, PrintFlag wcsPint)
        {
            var WCSPint = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSPint.ToString());
            WCSPint.Value = wcsPint.GetIndexString();
            BllResult plcResult = plc.Write(WCSPint);
            return plcResult;
        }

        /// <summary>
        /// 打标请求清除
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcutePintClear(Equipment machine, IPLC plc)
        {
            var result = SendPintToPlc(machine, plc, PrintFlag.默认);
            if (result.Success)
            {
                return BllResultFactory.Sucess($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应打标请求完成后，清除WCS响应打标信息成功");
            }
            else
            {
                return BllResultFactory.Error($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应打标请求完成后，清除WCS响应打标信息失败");
            }
        }


        /// <summary>
        /// 翻转请求处理
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="flipFlag"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        protected BllResult SendFlipToPlc(Equipment machine, IPLC plc, FlipFlag wcsAllowFlip)
        {
            var WCSAllowFlip = machine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSAllowFlip.ToString());
            WCSAllowFlip.Value = wcsAllowFlip.GetIndexString();
            BllResult plcResult = plc.Write(WCSAllowFlip);
            return plcResult;
        }

        /// <summary>
        /// 翻转请求清除
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteFlipClear(Equipment machine, IPLC plc)
        {
            var result = SendFlipToPlc(machine, plc, FlipFlag.默认);
            if (result.Success)
            {
                return BllResultFactory.Sucess($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应翻转请求完成后，清除WCS响应翻转信息成功");
            }
            else
            {
                return BllResultFactory.Error($"处理工位[{machine.StationCode}]对应的设备[{machine.Name}]响应翻转请求完成后，清除WCS响应翻转信息失败");
            }
        }


    }
}
