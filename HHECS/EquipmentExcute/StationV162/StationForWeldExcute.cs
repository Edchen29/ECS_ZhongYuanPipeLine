using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Station;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HHECS.EquipmentExcute.StationV162
{

    public class StationForWeldExcute : StationExcute
    {
        /// <summary>
        /// 到达
        /// </summary>
        /// <param name="station"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteArrive(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            var isSuccess = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveResult.ToString()).Value;
            var arriveRealAddress = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveRealAddress.ToString()).Value;
            if (isSuccess == StationArriveResult.成功.GetIndexString())
            {
                //获取条码
                var barcode = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveBarcode.ToString()).Value;
                //var arriveRealAddress = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveRealAddress.ToString()).Value;
                if (string.IsNullOrWhiteSpace(barcode))
                {
                    return BllResultFactory.Error($"站台{station.Code}{station.Name}有位置到达但是没有条码信息");
                }
                else
                {
                    var stepTraces = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id = {barcode} ");
                    if (stepTraces.Success)
                    {
                        if (stepTraces.Data.Count > 1)
                        {
                            return BllResultFactory.Error($"站台{station.Code}{station.Name}根据条码{barcode}查找到了多条任务，请检查任务数据");
                        }
                        else
                        {
                            var stepTrace = stepTraces.Data[0];                            
                            var backEquipment = allEquipments.Find(t => t.SelfAddress == station.BackAddress);
                            if (backEquipment == null)
                            {
                                Logger.Log($"站台{station.Code}{station.Name}的上一个地址 {station.BackAddress} 没有对应的设备，请检查设备数据", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            if (stepTrace.StationId != backEquipment.StationId)
                            {
                                var currentStation = station.StationList.Find(t => t.Id == stepTrace.StationId);
                                if (currentStation == null)
                                {
                                    Logger.Log($"站台{station.Code}{station.Name}根据id[{barcode}]查询到达任务的当前站台不存在，请检查任务数据", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                                Logger.Log($"站台{station.Code}{station.Name}根据id[{barcode}]查询到达任务，任务对应的站台是[{currentStation.Name}]，请检查任务数据", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            if (stepTrace.Status > StepTraceStatus.设备开始生产.GetIndexInt())
                            {
                                SendAckToPlcForInOrOut(station, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, arriveRealAddress, "0");
                                Logger.Log($"站台{station.Code}{station.Name}根据id[{barcode}]到达的任务状态大于[{StepTraceStatus.设备开始生产}]，直接回复到达", LogLevel.Success);
                                return BllResultFactory.Error();
                            }
                            try
                            {                                
                                //回复位置到达
                                var result = SendAckToPlcForInOrOut(station, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, arriveRealAddress, "0");
                                if (result.Success)
                                {
                                    return BllResultFactory.Sucess($"响应站台{station.Code}{station.Name}位置到达成功,完成任务成功，条码{barcode},任务{stepTrace.Id}");
                                }
                                else
                                {                                   
                                    return BllResultFactory.Error($"响应站台{station.Code}{station.Name}位置到达失败,条码{barcode},任务{stepTrace.Id}，详情：{result.Msg}");
                                }
                            }
                            catch (Exception ex)
                            {                                
                                return BllResultFactory.Error($"响应站台{station.Code}{station.Name}位置到达请求失败,完成任务失败，条码{barcode},任务{stepTrace.Id}，详情：{ex.Message}");
                            }
                                                                             
                        }
                    }
                    else
                    {
                        return BllResultFactory.Error($"站台{station.Code}{station.Name}根据条码{barcode}没有查询到任务：{stepTraces.Msg}");
                    }
                }
            }
            else
            {
                return BllResultFactory.Error($"站台{station.Name}位置到达失败");
            }
        }


        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="station"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteRequest(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            var barcode = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestBarcode.ToString()).Value;
            var requestNumber = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestNumber.ToString()).Value;
            var height = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestHeight.ToString()).Value;
            var weight = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWeight.ToString()).Value;
            var length = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestLength.ToString()).Value;
            var width = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWidth.ToString()).Value;
            string goAddress = "";
            string backAddress = "";

            if (string.IsNullOrWhiteSpace(barcode))
            {
                SendBack(station, plc, barcode);
                return BllResultFactory.Error($"站台{station.Code}{station.Name}有地址请求但是没有条码信息");
            }
            else
            {
                //获取任务
                var stepTraces = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where id = {barcode}");
                if (stepTraces.Success)
                {
                    if (stepTraces.Data.Count > 1)
                    {
                        SendBack(station, plc, barcode);
                        return BllResultFactory.Error($"站台{station.Code}{station.Name}根据条码{barcode}查找到了多条任务，请检查任务数据");
                    }
                    if (stepTraces.Data.Count == 0)
                    {
                        return BllResultFactory.Error($"站台{station.Code}{station.Name}根据条码{barcode}没有找到任务，请检查任务数据");
                    }
                    else
                    {
                        var stepTrace = stepTraces.Data[0];
                        goAddress = station.GoAddress;
                        if (string.IsNullOrWhiteSpace(goAddress))
                        {
                            return BllResultFactory.Error($"站台{station.Code}{station.Name}去向地址为空，请检查设备基础数据");
                        }
                        else
                        {
                            if (stepTrace.Status > StepTraceStatus.设备开始生产.GetIndexInt())
                            {
                                SendAddressReplyToPlc(station, plc, StationMessageFlag.地址回复, StationLoadStatus.默认, requestNumber, barcode, weight, length, width, height, goAddress, backAddress);
                                Logger.Log($"站台{station.Code}{station.Name}根据id[{barcode}]请求任务的任务状态大于 {StepTraceStatus.设备开始生产}，直接回复请求", LogLevel.Success);
                                return BllResultFactory.Error();
                            }
                            var nextEquipment = allEquipments.FirstOrDefault(t => t.SelfAddress == goAddress);
                            if (nextEquipment != null)
                            {
                                //var stationId = stepTrace.StationId;
                                //stepTrace.StationId = nextEquipment.StationId;
                                //var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                                //if (updateResult.Success)
                                //{
                                    var temp = SendAddressReplyToPlc(station, plc, StationMessageFlag.地址回复, StationLoadStatus.默认,
                                                                        requestNumber, barcode, weight, length, width, height, goAddress, backAddress);
                                    if (temp.Success)
                                    {
                                        return BllResultFactory.Sucess($"响应站台{station.Code}{station.Name}地址请求成功，条码{barcode},任务{stepTrace.Id}");
                                    }
                                    else
                                    {
                                        //stepTrace.StationId = stationId;
                                        //AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                                        return BllResultFactory.Error($"响应站台{station.Code}{station.Name}地址请求失败，条码{barcode},任务{stepTrace.Id}，写入PLC失败：{temp.Msg}");
                                    }
                                //}
                                //else
                                //{
                                //    return BllResultFactory.Error($"响应站台{station.Code}{station.Name}地址请求失败，更新当前站台失败,任务{stepTrace.Id}，原因：{updateResult.Msg}");
                                //}
                            }
                            else
                            {
                                return BllResultFactory.Error($"站台{station.Code}{station.Name}去向地址没有对应的设备，请检设备查数据");
                            }
                        }                          
                    }
                }
                else
                {
                    SendBack(station, plc, barcode);
                    return BllResultFactory.Error($"站台{station.Code}{station.Name}根据条码{barcode}没有查询到任务：{stepTraces.Msg}");
                }
            }
        }

    }
}
