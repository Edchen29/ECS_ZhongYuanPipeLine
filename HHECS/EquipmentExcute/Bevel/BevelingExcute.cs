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

namespace HHECS.EquipmentExcute.Bevel
{
    /// <summary>
    /// 坡口
    /// </summary>
    public abstract class BevelingExcute
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
                    return BllResultFactory.Error($"没有连接到[{this.EquipmentType.Name}]类型设备，所以不执行处理程序。");
                }

                //找出  未完成的任务
                var stepTraceResult = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where status < {StepTraceStatus.任务完成.GetIndexInt()}");
                if (!stepTraceResult.Success)
                {
                    Logger.Log($"查询[{this.EquipmentType.Name}]类型设备的任务出错，原因：{stepTraceResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                foreach (var bevel in bevels)
                {
                    var TotalError = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.TotalError.ToString());
                    //有故障就不处理，跳到下个设备
                    if (TotalError.Value == "True")
                    {
                        continue;
                    }

                    //处理 上料请求
                    var ArriveResult = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveResult.ToString());
                    var WCSACKMessage = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
                    //PLC有位置到达，而WCSACK没有回复，则WCS还没有响应
                    if (ArriveResult?.Value == MachineMessageFlag.自动请求上料.GetIndexString() && WCSACKMessage?.Value == MachineMessageFlag.默认.GetIndexString())
                    {
                        ExcuteArrive(bevel, allEquipments, stepTraceResult.Data, plc);
                    }
                    //PLC没位置到达，而WCSACK有回复，则PLC已经响应但还没有清除
                    if (ArriveResult?.Value == MachineMessageFlag.默认.GetIndexString() && WCSACKMessage?.Value == MachineMessageFlag.回复允许上料.GetIndexString())
                    {
                        ExcuteArriveClear(bevel, plc);
                    }

                    //处理 下料请求
                    var RequestMessage = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestMessage.ToString());
                    var WCSReplyMessage = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
                    //PLC有请求，但ECS没有，则ECS还没有响应
                    if ((RequestMessage?.Value == MachineMessageFlag.自动请求下料.GetIndexString() || RequestMessage?.Value == MachineMessageFlag.人工请求下料.GetIndexString()) && WCSReplyMessage?.Value == MachineMessageFlag.默认.GetIndexString())
                    {
                        ExcuteRequest(bevel, allEquipments, stepTraceResult.Data, plc);
                    }
                    //PLC没有请求，但ECS有确认信号， 就清除 ECS确认下料完成信号 
                    if (RequestMessage?.Value == MachineMessageFlag.默认.GetIndexString() && WCSReplyMessage?.Value == MachineMessageFlag.回复允许下料.GetIndexString())
                    {
                        ExcuteArriveClear(bevel, plc);
                    }

                    //处理 翻转请求
                    var Allow_Flip = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestFlip.ToString());
                    var WCS_Allow_Flip = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSAllowFlip.ToString());
                    // PLC有"翻转请求"信号，但ECS没有确认信号，则ECS还没有响应
                    if (Allow_Flip?.Value == FlipFlag.自动请求翻转.GetIndexString() && WCS_Allow_Flip?.Value == FlipFlag.默认.GetIndexString())
                    {
                        ExcuteFlip(bevel, allEquipments, stepTraceResult.Data, plc);
                    }
                    // PLC没有"翻转请求"信号，但ECS有确认信号，说明PLC已经清除而WCS没有清除 
                    if (Allow_Flip?.Value == FlipFlag.默认.GetIndexString() && WCS_Allow_Flip?.Value == FlipFlag.回复允许翻转.GetIndexString())
                    {
                        SendFlipToPlc(bevel, false, plc);
                    }
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
        /// <param name="bevel"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteArrive(Equipment bevel, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);

        /// <summary>
        /// 处理 下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="bevel"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteRequest(Equipment bevel, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);

        /// <summary>
        /// 处理 翻转请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="bevel"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteFlip(Equipment bevel, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc);


        /// <summary>
        /// 位置到达回复
        /// </summary>
        /// <param name="bevel"></param>
        /// <param name="plc"></param>
        /// <param name="message"></param>
        /// <param name="loadStatus"></param>
        /// <param name="number"></param>
        /// <param name="backup"></param>
        /// <returns></returns>
        public BllResult SendAckToPlc(Equipment bevel, IPLC plc, MachineMessageFlag messageFlag,  string number, string taskId, string barcode, string requestProductId, string pipeMaterial, string pipeLength, string pipeDiameter, string pipeThickness, string address)
        {
            var prop1 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMessage.ToString());
            prop1.Value = messageFlag.GetIndexString();
            var prop2 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKNumber.ToString());
            prop2.Value = number;
            var prop3 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKTaskId.ToString());
            prop3.Value = taskId;
            var prop4 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKBarcode.ToString());
            prop4.Value = barcode;
            var prop5 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKProductId.ToString());
            prop5.Value = requestProductId;
            var prop6 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKMaterial.ToString());
            prop6.Value = pipeMaterial;
            var prop7 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKLength.ToString());
            prop7.Value = pipeLength;
            var prop8 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKDiameter.ToString());
            prop8.Value = pipeDiameter;
            var prop9 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSACKThickness.ToString());
            prop9.Value = pipeThickness;
            var prop10 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyAddress.ToString());
            prop10.Value = address;
            List<EquipmentProp> props = new List<EquipmentProp>() { prop2, prop3, prop4, prop5, prop6, prop7, prop8, prop9, prop10, prop1 };
            return plc.Writes(props);
        }


        /// <summary>
        /// 位置到达清除
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="bevel"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private BllResult ExcuteArriveClear(Equipment bevel, IPLC plc)
        {
            var result = SendAckToPlc(bevel, plc, MachineMessageFlag.默认, "0", "0", "", "0", "0", "0", "0", "0", "0");
            if (result.Success)
            {
                return BllResultFactory.Sucess($"处理工位[{bevel.StationCode}]对应的线体[{bevel.Name}]响应位置到达完成后，清除WCS地址区成功");
            }
            else
            {
                return BllResultFactory.Error($"处理工位[{bevel.StationCode}]对应的线体[{bevel.Name}]响应位置到达完成后，清除WCS地址区失败");
            }
        }


        /// <summary>
        /// 地址请求回复
        /// </summary>
        /// <param name="bevel"></param>
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
        public BllResult SendAddressReplyToPlc(Equipment bevel, IPLC plc, MachineMessageFlag messageFlag, string number, string taskId, string barcode, string requestProductId, string pipeMaterial, string pipeLength, string pipeDiameter, string pipeThickness, string address)
        {
            var prop1 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
            prop1.Value = messageFlag.GetIndexString();
            var prop2 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyNumber.ToString());
            prop2.Value = number;
            var prop3 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyTaskId.ToString());
            prop3.Value = taskId;
            var prop4 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyBarcode.ToString());
            prop4.Value = barcode;
            var prop5 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyProductId.ToString());
            prop5.Value = requestProductId;
            var prop6 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMaterial.ToString());
            prop6.Value = pipeMaterial;
            var prop7 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyLength.ToString());
            prop7.Value = pipeLength;
            var prop8 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyDiameter.ToString());
            prop8.Value = pipeDiameter;
            var prop9 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyThickness.ToString());
            prop9.Value = pipeThickness;
            var prop10 = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyAddress.ToString());
            prop10.Value = address;
            List<EquipmentProp> props = new List<EquipmentProp> { prop2, prop3, prop4, prop5, prop6, prop7, prop8, prop9, prop10, prop1 };
            return plc.Writes(props);
        }


        /// <summary>
        /// 地址请求清除
        /// </summary>
        /// <param name="bevel"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteRequestClear(Equipment bevel, IPLC plc)
        {
            var result = SendAddressReplyToPlc(bevel, plc, MachineMessageFlag.默认, "0", "0", "", "0", "0", "0", "0", "0", "0");
            if (result.Success)
            {
                return BllResultFactory.Sucess($"处理工位[{bevel.StationCode}]对应的线体[{bevel.Name}]响应地址请求完成后，清除WCS地址区成功");
            }
            else
            {
                return BllResultFactory.Error($"处理工位[{bevel.StationCode}]对应的线体[{bevel.Name}]响应地址请求完成后，清除WCS地址区失败");
            }
        }




        /// <summary>
        /// 写入或清除  ECS允许焊接机器人翻转，True为写入，False为清除
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="bevel">机器人</param>
        /// <param name="allow">是否允许翻转，true为允许,false为清除</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendFlipToPlc(Equipment bevel, bool allow, IPLC plc)
        {
            var operate = allow ? "写入" : "清除";
            var status = allow ? FlipFlag.回复允许翻转.GetIndexString() : FlipFlag.默认.GetIndexString();
            var WCS_Allow_Flip = bevel.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSAllowFlip.ToString());
            WCS_Allow_Flip.Value = status.ToString();
            BllResult plcResult = plc.Write(WCS_Allow_Flip);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{bevel.Name}】ECS允许翻转信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{bevel.Name}】ECS允许翻转信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }

        

    }
}
