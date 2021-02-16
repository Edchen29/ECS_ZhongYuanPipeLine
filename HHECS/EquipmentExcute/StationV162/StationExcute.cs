using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Station;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.StationV162
{
    /// <summary>
    /// 站台顶层抽象类
    /// </summary>
    public abstract class StationExcute
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
        /// <param name="stations"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public virtual void Excute(List<Equipment> stations, List<Equipment> allEquipments, IPLC plc)
        {
            foreach (var station in stations)
            {
                BllResult result = null;
                //判断请求
                //PLC有请求，但WCS没有，则WCS还没有响应
                if (station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.RequestMessage.ToString())?.Value == StationMessageFlag.地址请求.GetIndexString()
                    && station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyMessage.ToString())?.Value == StationMessageFlag.默认.GetIndexString())
                {
                    result = ExcuteRequest(station, allEquipments, plc);
                }

                //PLC没有，WCS有，说明PLC已经清除而WCS没有清除
                if (station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.RequestMessage.ToString())?.Value == StationMessageFlag.默认.GetIndexString()
                    && station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyMessage.ToString())?.Value == StationMessageFlag.地址回复.GetIndexString())
                {
                    result = ExcuteRequestClear(station, plc);
                }

                //判断位置到达
                //PLC有位置到达，而WCSACK没有回复，则WCS还没有响应
                if (station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.ArriveMessage.ToString())?.Value == StationMessageFlag.分拣报告.GetIndexString()
                    && station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKMessage.ToString())?.Value == StationMessageFlag.默认.GetIndexString())
                {
                    result = ExcuteArrive(station, allEquipments, plc);
                }

                //PLC没位置到达，而WCSACK有回复，则PLC已经响应但还没有清除
                if (station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.ArriveMessage.ToString())?.Value == StationMessageFlag.默认.GetIndexString()
                    && station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKMessage.ToString())?.Value == StationMessageFlag.WCSPLCACK.GetIndexString()
                    && station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKLoadStatus.ToString())?.Value == StationLoadStatus.回复到达.GetIndexString())
                {
                    result = ExcuteArriveClear(plc, station);
                }

                LogResult(result);
            }
        }

        /// <summary>
        /// 执行请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="station"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteRequest(Equipment station, List<Equipment> allEquipments, IPLC plc);

        /// <summary>
        /// 执行清除请求
        /// </summary>
        /// <param name="station"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteRequestClear(Equipment station, IPLC plc)
        {
            var result = SendAddressReplyToPlc(station, plc, StationMessageFlag.默认, StationLoadStatus.默认, "0", "0", "0", "0", "0", "0", "0", "0");
            if (result.Success)
            {
                return BllResultFactory.Sucess($"站台{station.Name}响应地址请求完成后，清除WCS地址区成功");
            }
            else
            {
                return BllResultFactory.Error($"站台{station.Name}响应地址请求完成后，清除WCS地址区失败");
            }
        }

        /// <summary>
        /// 执行到达
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="station"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public abstract BllResult ExcuteArrive(Equipment station, List<Equipment> allEquipments, IPLC plc);

        /// <summary>
        /// 实现到达清除
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="station"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private BllResult ExcuteArriveClear(IPLC plc, Equipment station)
        {
            var result = SendAckToPlcForInOrOut(station, plc, StationMessageFlag.默认, StationLoadStatus.默认, "0", "0");
            if (result.Success)
            {
                return BllResultFactory.Sucess($"站台{station.Name}响应位置到达完成后，清除WCS地址区成功");
            }
            else
            {
                return BllResultFactory.Error($"站台{station.Name}响应位置到达完成后，清除WCS地址区失败");
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="result"></param>
        protected void LogResult(BllResult result)
        {
            if (result == null)
            {
                return;
            }
            if (result.Success)
            {
                Logger.Log(result.Msg, LogLevel.Success);
                AppSession.LogService.WriteLog("StationExecuteSuccess", result.Msg);
            }
            else
            {
                //空消息也不显示
                if (result.Msg == "")
                {
                    return;
                }
                Logger.Log(result.Msg, LogLevel.Error);
                AppSession.LogService.WriteLog("StationExecuteError", result.Msg);
            }
        }

        protected string getEquipmentInfo(Equipment station, string logMessage)
        {
            return $"设备[{station.Id}][{station.Code}][{station.Name}]对应的工位[{station.Station.Id}][{station.Station.Code}][{station.Station.Name}] {logMessage}";
        }

        /// <summary>
        /// 实现地址回复
        /// </summary>
        /// <param name="station"></param>
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
        public BllResult SendAddressReplyToPlc(Equipment station, IPLC plc, StationMessageFlag messageFlag, StationLoadStatus loadStatus, string number, string barcode, string weight, string lenght, string width, string height, string address, string backup)
        {
            var prop1 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyMessage.ToString());
            prop1.Value = messageFlag.GetIndexString();
            var prop2 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyLoadStatus.ToString());
            prop2.Value = loadStatus.GetIndexString();
            var prop3 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyNumber.ToString());
            prop3.Value = number;
            var prop4 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyBarcode.ToString());
            prop4.Value = barcode;
            var prop5 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyWeight.ToString());
            prop5.Value = weight;
            var prop6 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyLength.ToString());
            prop6.Value = lenght;
            var prop7 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyWidth.ToString());
            prop7.Value = width;
            var prop8 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyHeight.ToString());
            prop8.Value = height;
            var prop9 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyAddress.ToString());
            prop9.Value = address;
            //var prop10 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSReplyBackup.ToString());
            //prop10.Value = backup;
            List<EquipmentProp> props = new List<EquipmentProp> { prop2, prop3, prop4, prop5, prop6, prop7, prop8, prop9, prop1 };
            return plc.Writes(props);
        }

        /// <summary>
        /// 实现ACK回复，包括地址到达和控制指令
        /// </summary>
        /// <param name="station"></param>
        /// <param name="plc"></param>
        /// <param name="message"></param>
        /// <param name="loadStatus"></param>
        /// <param name="number"></param>
        /// <param name="backup"></param>
        /// <returns></returns>
        public BllResult SendAckToPlc(Equipment station, IPLC plc, StationMessageFlag messageFlag, StationLoadStatus loadStatus, string number, string backup)
        {
            var prop1 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKMessage.ToString());
            prop1.Value = messageFlag.GetIndexString();
            var prop2 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKLoadStatus.ToString());
            prop2.Value = loadStatus.GetIndexString();
            var prop3 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKNumber.ToString());
            prop3.Value = number;
            //var prop4 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKBackup.ToString());
            //prop4.Value = backup;
            List<EquipmentProp> props = new List<EquipmentProp>() { prop2, prop3, prop1 };
            return plc.Writes(props);
        }
        public BllResult SendAckToPlcForInOrOut(Equipment station, IPLC plc, StationMessageFlag messageFlag, StationLoadStatus loadStatus, string number, string backup)
        {
            var prop1 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKMessage.ToString());
            prop1.Value = messageFlag.GetIndexString();
            var prop2 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKLoadStatus.ToString());
            prop2.Value = loadStatus.GetIndexString();
            var prop3 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKNumber.ToString());
            prop3.Value = number;
            //var prop4 = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.WCSACKBackup.ToString());
            //prop4.Value = backup;
            List<EquipmentProp> props = new List<EquipmentProp>() { prop2, prop3, prop1 };
            return plc.Writes(props);
        }
        /// <summary>
        /// 地址请求时，回退
        /// </summary>
        /// <param name="station"></param>
        /// <param name="plc"></param>
        /// <param name="barcode"></param>
        public void SendBack(Equipment station, IPLC plc, string barcode)
        {
            var result = SendAddressReplyToPlc(station, plc, StationMessageFlag.地址回复, StationLoadStatus.默认,
                                                station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestNumber.ToString()).Value,
                                                barcode,
                                                station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWeight.ToString()).Value,
                                                station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestLength.ToString()).Value,
                                                station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWidth.ToString()).Value,
                                                station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestHeight.ToString()).Value,
                                                station.BackAddress, "");
            LogResult(result);
        }



    }
}
