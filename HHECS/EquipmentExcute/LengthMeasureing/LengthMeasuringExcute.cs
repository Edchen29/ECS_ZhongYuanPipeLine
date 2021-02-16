using HHECS.EquipmentExcute.LengthMeasureing.LengthMeasuringEnums;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Machine;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;

namespace HHECS.EquipmentExcute.LengthMeasureing
{
    /// <summary>
    /// 测长站台测量完成，发出请求:  材料(int)+直径(int)+ 壁厚(int)+ 原材料长度(int)
    /// 发出请求之后回复，并且在缓存表中生成一条数据
    /// 设备请求下料：
    /// ECS读取物料和测量信息，然后写入工位缓存表，并且响应请求
    /// </summary>
    public abstract class LengthMeasureingExcute
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
        /// <param name="LengthMeasuringMachines"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public virtual BllResult Excute(List<Equipment> LengthMeasuringMachines, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                if (LengthMeasuringMachines.Count == 0)
                {
                    return BllResultFactory.Error($"没有连接到【{this.EquipmentType.Code}】设备，所以不执行处理程序。");
                }

                foreach (var LengthMeasuringMachine in LengthMeasuringMachines)
                {
                    var TotalError = LengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.TotalError.ToString());
                    //有故障就不处理，跳到下个设备
                    if (TotalError.Value == "True")
                    {
                        continue;
                    }
                    //处理 PLC下料请求
                    var RequestMessage = LengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestMessage.ToString());
                    var WCSReplyMessage = LengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.WCSReplyMessage.ToString());
                    //PLC有请求，但ECS没有，则ECS还没有响应
                    if ((RequestMessage?.Value == MachineMessageFlag.自动请求下料.ToString() || RequestMessage?.Value == MachineMessageFlag.人工请求下料.ToString()) && WCSReplyMessage?.Value == MachineMessageFlag.默认.ToString())
                    {
                        ExcuteRequest(LengthMeasuringMachine, allEquipments, allEquipments, plc);
                    }
                    //PLC没有请求，但ECS有确认信号， 就清除 ECS确认下料完成信号 
                    if (RequestMessage?.Value == MachineMessageFlag.默认.ToString() && WCSReplyMessage?.Value == MachineMessageFlag.回复允许下料.ToString())
                    {
                        SendRequestToPlc(plc, LengthMeasuringMachine, false);
                    }
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 执行下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="lengthMeasuringMachine"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteRequest(Equipment lengthMeasuringMachine, List<Equipment> allEquipments, List<Equipment> lthMeasuringCaches, IPLC plc);

        /// <summary>
        /// 写入或清除  允许下料 
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="bevel"></param>
        /// <param name="arrive">是否写入，true为写入,false为清除</param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected BllResult SendRequestToPlc(IPLC plc, Equipment LengthMeasuringMachine, bool request)
        {
            var operate = request ? "写入" : "清除";
            var WCS_Request_Blank = LengthMeasuringMachine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode ==ECSToMeasuringMachineProps.WCS_Request_Blank.ToString());
            WCS_Request_Blank.Value = request ? MachineMessageFlag.回复允许下料.GetIndexString() : MachineMessageFlag.默认.GetIndexString(); ;
            BllResult plcResult = plc.Write(WCS_Request_Blank);
            if (plcResult.Success)
            {
                Logger.Log($"{operate}设备【{LengthMeasuringMachine.Name}】 ECS确认下料 信号成功", LogLevel.Success);
            }
            else
            {
                Logger.Log($"{operate}设备【{LengthMeasuringMachine.Name}】 ECS确认下料 信号失败，写入PLC失败：原因：{plcResult.Msg}", LogLevel.Error);
            }
            return plcResult;
        }


    }
}
