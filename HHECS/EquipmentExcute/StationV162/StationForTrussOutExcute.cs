using Dapper;
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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.StationV162
{

    public class StationForTrussOutExcute : StationExcute
    {
        public override BllResult ExcuteArrive(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            var isSuccess = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveResult.ToString()).Value;
            if (isSuccess == StationArriveResult.成功.GetIndexString())
            {
                var sql = string.Empty;
                StepTrace stepTrace = null;
                //获取条码
                var barcode = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveBarcode.ToString()).Value;
                var arriveRealAddress = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ArriveRealAddress.ToString()).Value;
                var productTypeProp = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ProductId.ToString());
                var result1 = int.TryParse(productTypeProp.Value, out int productType);
                if (productType < 1)
                {
                    Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求失败，工件类型[{productTypeProp.Value}]不是大于1的整数", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var workStation = station.StationList.Find(t => t.Id == station.StationId);
                if (workStation == null)
                {
                    Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求时候，不存在ID为[{station.StationId}]的站台，请检查站台基础数据", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var backEquipment = allEquipments.Find(t => t.SelfAddress == station.BackAddress);
                if (backEquipment == null)
                {
                    return BllResultFactory.Error($"站台{station.Code}{station.Name}的上一个地址 {station.BackAddress} 没有对应的设备，请检查设备基础数据");
                }

                var stepTraceCount = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where stationId={station.StationId} and status in ({StepTraceStatus.设备请求下料.GetIndexInt()},{StepTraceStatus.等待任务执行.GetIndexInt()})");
                if (!stepTraceCount.Success)
                {
                    Logger.Log($"[{station.Name}]的站台[{station.StationId}]下料请求时候，查询数据异常{stepTraceCount.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stepTraceCount.Data.Count > 0)
                {
                    string ids = $"存在任务号为:{string.Join(",", stepTraceCount.Data.Select(t => t.Id).ToArray())}的任务没有完成";
                    return BllResultFactory.Error($"处理设备[{station.Name}]的站台[{station.StationId}][{workStation.Name}]下料请求时候，" +
                                                  $"当前站台存在[{stepTraceCount.Data.Count}]条任务:{ids}，请先把站台【{station.Name}】存在的任务完成掉");
                }

                if (!string.IsNullOrWhiteSpace(barcode) && barcode != "0")
                {
                    var stepTracesResult = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id = '{barcode}'");
                    if (!stepTracesResult.Success || stepTracesResult.Data.Count == 0)
                    {
                        return BllResultFactory.Error($"站台{station.Code}{station.Name}根据id[{barcode}]查询不到任务");
                    }
                    if (stepTracesResult.Data.Count() > 1)
                    {
                        return BllResultFactory.Error($"站台{station.Code}{station.Name}根据id[{barcode}]查找到了多条任务，请检查数据");
                    }
                    else
                    {
                        //TODO 如果查询到任务与最初产品类型不一致，报错
                        if (stepTracesResult.Data[0].WcsProductType != productType)
                        {
                            return BllResultFactory.Error($"站台[{station.Code}]，[{station.Name}]，[{station.StationId}]根据ID[{stepTrace.Id}]查找到的任务工件类型为[{stepTrace.WcsProductType}]，站台给出的工件类型是[{productType}]，请检查任务数据");
                        }
                        stepTrace = stepTracesResult.Data.First();
                    }
                }
                else
                {
                    //return BllResultFactory.Error($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求失败，请求的ID为[{barcode}]，条码不能为空或0");
                    var createTypeFlagPro = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ManualSign.ToString());
                    if (createTypeFlagPro == null)
                    {
                        Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}][{workStation.Name}]下料请求时候,{createTypeFlagPro}为空，请核查基础数据配置", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    var result = int.TryParse(createTypeFlagPro.Value, out int createTypeFlag);
                    if (createTypeFlag == CreateTypeFlag.手动任务.GetIndexInt())
                    {
                        stepTrace = new StepTrace();
                        //获取产品信息
                        var productHeaderResult = AppSession.Dal.GetCommonModelByCondition<ProductHeader>($"where wcsProductType = '{productType}'");
                        if (!productHeaderResult.Success)
                        {
                            Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求时候，根据设备工件类型[{productType}]没有检测到对应的产品信息，原因：{productHeaderResult.Msg}，请检查[产品]基础数据", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                        ////获取工位工序表--工位类型
                        //var stepStationResult = AppSession.Dal.GetCommonModelByCondition<StepStation>($"where stationId = '{backEquipment.StationId}'");
                        //if (!stepStationResult.Success)
                        //{
                        //    Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求时候，根据站台[{backEquipment.StationId}]没有检测到工序工位，原因：{stepStationResult.Msg}，请检查[工序工位]基础数据", LogLevel.Error);
                        //    return BllResultFactory.Error();
                        //}
                        //获取工序表
                        var stepResult = AppSession.Dal.GetCommonModelBySql<Step>($"select top 1 step.* from step inner join step_station on step.id = step_station.stepId and step.productId = {productHeaderResult.Data[0].Id} and step_station.stationId = {station.StationId} ");
                        if (!stepResult.Success)
                        {
                            Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求时候，根据产品标识[{productHeaderResult.Data[0].Id}]和工位标识[{station.StationId}]没有查询到工序，原因：{stepResult.Msg}，请检查[工序]基础数据", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                        var step = stepResult.Data[0];
                        stepTrace.StepId = step.Id.Value;
                        //工件型号
                        stepTrace.WcsProductType = productType;
                        stepTrace.StationId = backEquipment.StationId;
                        stepTrace.ProductCode = step.ProductCode;
                        stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
                        stepTrace.LineId = station.LineId;
                        stepTrace.WONumber = "";
                        stepTrace.SerialNumber = "";
                        stepTrace.StationInTime = DateTime.Now;
                        stepTrace.LineInTime = DateTime.Now;
                        stepTrace.CreateType = CreateTypeFlag.手动任务.GetIndexInt();
                        //人工确认任务起始点
                        stepTrace.ManualStartPoint = station.Name;
                        stepTrace.CreateTime = DateTime.Now;
                        stepTrace.CreateBy = "PLC";

                        var resultHumanTasks = AppSession.StepTraceService.HumanTasks(stepTrace);
                        if (!resultHumanTasks.Success)
                        {
                            return BllResultFactory.Error($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求时候，人工确认任务失败，原因：{resultHumanTasks.Msg}");
                        }
                    }
                    else
                    {
                        return BllResultFactory.Error($"站台{station.Code}{station.Name}有请求，但是没有任务号，也没有人工确认工件类型。");
                    }
                }

                if (stepTrace.StationId != backEquipment.StationId)
                {
                    var stepTraceStation = station.StationList.Find(t => t.Id == stepTrace.StationId);
                    if (stepTraceStation == null)
                    {
                        Logger.Log($"站台{station.Code}{station.Name}根据id[{barcode}]查询到达任务的当前站台[{stepTrace.StationId}]不存在，请检查任务数据", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    Logger.Log($"站台{station.Code}{station.Name}根据id[{barcode}]查询到达任务，任务对应的站台是[{stepTraceStation.Name}]，请检查任务数据", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stepTrace.WcsProductType != productType)
                {
                    return BllResultFactory.Error($"站台[{station.Code}]，[{station.Name}]，[{station.StationId}]根据ID[{stepTrace.Id}]查找到的任务工件类型为[{stepTrace.WcsProductType}]，站台给出的工件类型是[{productType}]，请检查任务数据");
                }
                if (stepTrace.Status != StepTraceStatus.设备开始生产.GetIndexInt())
                {
                    //SendAckToPlc(station, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, arriveRealAddress, "0");
                    Logger.Log($"站台{station.Code}{station.Name}根据id[{barcode}]的任务状态不是[{StepTraceStatus.设备开始生产}]，不能处理！", LogLevel.Success);
                    return BllResultFactory.Error();
                }
                ////记录原始状态以便回滚
                //int tempStatus = stepTrace.Status;
                //var machining = true.ToString();
                //var finished = false.ToString();
                var machining = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.Machining_WCS.ToString()).Value;
                var finished = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.AGV_WCS.ToString()).Value;
                if (machining == true.ToString())
                {
                    //转到机加工生产
                    stepTrace.StationId = station.StationId;
                    stepTrace.Status = StepTraceStatus.设备请求下料.GetIndexInt();
                }
                else if (finished == true.ToString())
                {
                    //TODO: 根据已匹配到的任务产品编码，查询与实际成品下线站台的摆放类型
                    if (stepTrace.ProductCode == null)
                    {
                        Logger.Log($"站台{station.Code}{station.Name}根据任务{barcode}产品编码{stepTrace.ProductCode}为空,请检查数据", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    var productCodeResult = AppSession.Dal.GetCommonModelByCondition<ProductHeader>($"where Code={stepTrace.ProductCode}");
                    if (!productCodeResult.Success || productCodeResult.Data.Count == 0)
                    {
                        Logger.Log($"站台{station.Code}{station.Name}根据任务{barcode}产品编码{stepTrace.ProductCode}，查询产品表失败,请检查数据", LogLevel.Error);
                        return BllResultFactory.Error();
                    }


                    //直接下线
                    var steps = AppSession.Dal.GetCommonModelBySql<Step>($"select top 1 * from step where productCode = '{stepTrace.ProductCode}' and stepType='FinishedType' order by sequence");
                    if (!steps.Success || steps.Data.Count == 0)
                    {
                        Logger.Log($"站台{station.Code}{station.Name}根据产品编码{stepTrace.ProductCode}未找到成品下线工序，请检查数据", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    if (steps.Data.Count() > 1)
                    {
                        Logger.Log($"站台{station.Code}{station.Name}根据产品编码{stepTrace.ProductCode}找到多条成品下线工序，请检查数据", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    stepTrace.NextStepId = steps.Data.First().Id.Value;
                    stepTrace.StationId = station.StationId;
                    stepTrace.Status = StepTraceStatus.设备请求下料.GetIndexInt();
                }
                else
                {
                    Logger.Log($"响应站台{station.Code}{station.Name}位置到达失败，任务{stepTrace.Id}，任务既没有去机加工信号，也没有下线信号！", LogLevel.Error);
                    return BllResultFactory.Error();
                }

                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction tran = null;
                    try
                    {
                        connection.Open();
                        tran = connection.BeginTransaction();
                        connection.Update<StepTrace>(stepTrace, transaction: tran);

                        //回复位置到达
                        var temp = SendAckToPlc(station, plc, StationMessageFlag.WCSPLCACK, StationLoadStatus.回复到达, arriveRealAddress, "0");
                        if (temp.Success)
                        {
                            tran.Commit();
                            if (stepTrace.CreateType == CreateTypeFlag.手动任务.GetIndexInt())
                            {
                                return BllResultFactory.Sucess($"响应站台{station.Code}{station.Name}位置到达，人工确认任务成功，任务{stepTrace.Id}");
                            }
                            else
                            {
                                return BllResultFactory.Sucess($"响应站台{station.Code}{station.Name}位置到达成功，任务{stepTrace.Id}");
                            }
                        }
                        else
                        {
                            tran?.Rollback();
                            //如果响应PLC失败，就要删除人工确认的任务
                            if (stepTrace.CreateType == CreateTypeFlag.手动任务.GetIndexInt())
                            {
                                tran = connection.BeginTransaction();
                                connection.DeleteList<StepTrace>($"where id in @ids", new { ids = stepTrace.Id.Value }, tran);
                                connection.DeleteList<StepTraceLog>("where stepTraceId in @ids", new { ids = stepTrace.Id.Value }, tran);
                                tran.Commit();
                            }
                            return BllResultFactory.Error($"响应站台{station.Code}{station.Name}位置到达失败，任务{stepTrace.Id}，回滚任务状态");
                        }
                    }
                    catch (Exception ex)
                    {
                        tran?.Rollback();
                        Logger.Log($"站台{station.Code}{station.Name}处理到达响应，出现异常：{ex.Message}", LogLevel.Exception, ex);
                        return BllResultFactory.Error();
                    }
                }
            }
            else
            {
                Logger.Log($"站台{station.Code}{station.Name}位置电控报告到达失败", LogLevel.Error);
                return BllResultFactory.Error();
            }
        }

        public override BllResult ExcuteRequest(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {

            Logger.Log($"堆垛机接入站台{station.Code}{station.Name}不执行请求处理", LogLevel.Error);
            return BllResultFactory.Error();
        }


    }
}
