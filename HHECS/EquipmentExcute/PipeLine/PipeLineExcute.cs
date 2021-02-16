using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Machine;
using HHECS.Model.Enums.PipeLine;
using HHECS.Model.Enums.Station;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HHECS.EquipmentExcute.PipeLine
{
    /// <summary>
    /// 输送线和缓存基类
    /// </summary>
    public abstract class PipeLineExcute
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
        public virtual BllResult Excute(List<Equipment> pipeLines, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                if (pipeLines.Count == 0)
                {
                    return BllResultFactory.Error($"没有连接到类型[{EquipmentType.Name}]的设备，所以不执行处理程序。");
                }
                List<StepTrace> stepTraceList = new List<StepTrace>();
                if (EquipmentType.Code != "LengthMeasuringCache")
                {
                    //找出  未完成的任务
                    var stepTraceResult = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where status < {StepTraceStatus.任务完成.GetIndexInt()}");
                    if (!stepTraceResult.Success)
                    {
                        Logger.Log($"查询类型[{EquipmentType.Name}]的设备的任务出错，原因：{stepTraceResult.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    else
                    {
                        stepTraceList = stepTraceResult.Data;
                    }
                }
                foreach (var pipeLine in pipeLines)
                {
                    var OperationModel = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.OperationModel.ToString());
                    var TotalError = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.TotalError.ToString());
                    //不是 故障且联机，就不处理，跳到下个设备
                    if (OperationModel?.Value != OperationModelFlag.联机.GetIndexString() || TotalError?.Value != TotalErrorFlag.无故障.GetIndexString())
                    {
                        continue;
                    }

                    //处理位置到达
                    var ArriveMessage = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.ArriveMessage.ToString());
                    var WCSACKMessage = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKMessage.ToString());
                    var WCSACKLoadStatus = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKLoadStatus.ToString());
                    //PLC有位置到达，而WCSACK没有回复，则WCS还没有响应
                    if (ArriveMessage?.Value == StationMessageFlag.分拣报告.GetIndexString() && WCSACKMessage?.Value == StationMessageFlag.默认.GetIndexString())
                    {
                        ExcuteArrive(pipeLine, allEquipments, stepTraceList, plc);
                    }
                    //PLC没位置到达，而WCSACK有回复，则PLC已经响应但还没有清除
                    if (ArriveMessage?.Value == StationMessageFlag.默认.GetIndexString() && WCSACKMessage?.Value == StationMessageFlag.WCSPLCACK.GetIndexString() && WCSACKLoadStatus?.Value == StationLoadStatus.回复到达.GetIndexString())
                    {
                        ExcuteArriveClear(pipeLine, plc);
                    }

                    //处理地址请求
                    var RequestMessage = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.RequestMessage.ToString());
                    var WCSReplyMessage = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyMessage.ToString());
                    //PLC有请求，但ECS没有，则ECS还没有响应
                    if (RequestMessage?.Value == StationMessageFlag.地址请求.GetIndexString() && WCSReplyMessage?.Value == StationMessageFlag.默认.GetIndexString())
                    {
                        ExcuteRequest(pipeLine, allEquipments, stepTraceList, plc);
                    }
                    //PLC没有请求，但ECS有确认信号， 就清除 ECS确认下料完成信号 
                    if (RequestMessage?.Value == StationMessageFlag.默认.GetIndexString() && WCSReplyMessage?.Value == StationMessageFlag.地址回复.GetIndexString())
                    {
                        ExcuteRequestClear(pipeLine, plc);
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
        /// 处理位置到达
        /// </summary>
        /// <param name="pipeLine"></param>
        /// <param name="allEquipments"></param>
        /// <param name="data"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        protected abstract BllResult ExcuteArrive(Equipment pipeLine, List<Equipment> allEquipments, List<StepTrace> data, IPLC plc);

        /// <summary>
        /// 处理地址请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="pipeLine"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        protected abstract BllResult ExcuteRequest(Equipment pipeLine, List<Equipment> allEquipments, List<StepTrace> data, IPLC plc);


        /// <summary>
        /// 位置到达回复
        /// </summary>
        /// <param name="pipeLine"></param>
        /// <param name="plc"></param>
        /// <param name="message"></param>
        /// <param name="loadStatus"></param>
        /// <param name="number"></param>
        /// <param name="backup"></param>
        /// <returns></returns>
        public BllResult SendAckToPlc(Equipment pipeLine, IPLC plc, StationMessageFlag messageFlag, StationLoadStatus loadStatus, string number, string taskId, string barcode, string pipeMaterial, string pipeLength, string pipeDiameter, string pipeThickness, string address)
        {
            pipeDiameter = (Convert.ToInt32(decimal.Parse(pipeDiameter) * 10)).ToString();
            pipeThickness = (Convert.ToInt32(decimal.Parse(pipeThickness) * 10)).ToString();

            List<EquipmentProp> props = null;
            var prop1 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKMessage.ToString());
            prop1.Value = messageFlag.GetIndexString();
            var prop2 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKLoadStatus.ToString());
            prop2.Value = loadStatus.GetIndexString();
            var prop3 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKNumber.ToString());
            prop3.Value = number;
            var prop4 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKTaskId.ToString());
            prop4.Value = taskId;
            var prop5 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKBarcode.ToString());
            prop5.Value = barcode;
            var prop6 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKMaterial.ToString());
            prop6.Value = pipeMaterial;
            var prop7 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKLength.ToString());
            prop7.Value = pipeLength;
            var prop8 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKDiameter.ToString());
            prop8.Value = pipeDiameter;
            var prop9 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSACKThickness.ToString());
            prop9.Value = pipeThickness;
            var prop10 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyAddress.ToString());
            prop10.Value = address;
            if (messageFlag == StationMessageFlag.默认)
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
        /// <param name="pipeLine"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private BllResult ExcuteArriveClear(Equipment pipeLine, IPLC plc)
        {
            var result = SendAckToPlc(pipeLine, plc, StationMessageFlag.默认, StationLoadStatus.默认, "0", "0", "", "0", "0", "0", "0", "0");
            if (result.Success)
            {
                return BllResultFactory.Sucess($"处理工位[{pipeLine.StationCode}]对应的线体[{pipeLine.Name}]响应位置到达完成后，清除WCS地址区成功");
            }
            else
            {
                return BllResultFactory.Error($"处理工位[{pipeLine.StationCode}]对应的线体[{pipeLine.Name}]响应位置到达完成后，清除WCS地址区失败");
            }
        }


        /// <summary>
        /// 地址请求回复
        /// </summary>
        /// <param name="pipeLine"></param>
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
        public BllResult SendAddressReplyToPlc(Equipment pipeLine, IPLC plc, StationMessageFlag messageFlag, StationLoadStatus loadStatus, string number, string taskId, string barcode, string pipeMaterial, string pipeLength, string pipeDiameter, string pipeThickness, string address)
        {
            pipeDiameter = (Convert.ToInt32(decimal.Parse(pipeDiameter) * 10)).ToString();
            pipeThickness = (Convert.ToInt32(decimal.Parse(pipeThickness) * 10)).ToString();

            List<EquipmentProp> props = null;
            var prop1 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyMessage.ToString());
            prop1.Value = messageFlag.GetIndexString();
            var prop2 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyLoadStatus.ToString());
            prop2.Value = loadStatus.GetIndexString();
            var prop3 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyNumber.ToString());
            prop3.Value = number;
            var prop4 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyTaskId.ToString());
            prop4.Value = taskId;
            var prop5 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyBarcode.ToString());
            prop5.Value = barcode;
            var prop6 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyMaterial.ToString());
            prop6.Value = pipeMaterial;
            var prop7 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyLength.ToString());
            prop7.Value = pipeLength;
            var prop8 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyDiameter.ToString());
            prop8.Value = pipeDiameter;
            var prop9 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyThickness.ToString());
            prop9.Value = pipeThickness;
            var prop10 = pipeLine.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == PipeLineProps.WCSReplyAddress.ToString());
            prop10.Value = address;
            if (messageFlag == StationMessageFlag.默认)
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
        /// <param name="pipeLine"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteRequestClear(Equipment pipeLine, IPLC plc)
        {
            var result = SendAddressReplyToPlc(pipeLine, plc, StationMessageFlag.默认, StationLoadStatus.默认, "0", "0", "", "0", "0", "0", "0", "0");
            if (result.Success)
            {
                return BllResultFactory.Sucess($"处理工位[{pipeLine.StationCode}]对应的线体[{pipeLine.Name}]响应地址请求完成后，清除WCS地址区成功");
            }
            else
            {
                return BllResultFactory.Error($"处理工位[{pipeLine.StationCode}]对应的线体[{pipeLine.Name}]响应地址请求完成后，清除WCS地址区失败");
            }
        }







    }
}
