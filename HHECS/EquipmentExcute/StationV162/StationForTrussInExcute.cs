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
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.StationV162
{

    public class StationForTrussInExcute : StationExcute
    {
        public override BllResult ExcuteRequest(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            StepTrace stepTrace = null;
            var workStation = station.StationList.Find(t => t.Id == station.StationId);
            if (workStation == null)
            {
                Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求时候，不存在ID为[{station.StationId}]的站台，请检查站台基础数据", LogLevel.Error);
                return BllResultFactory.Error();
            }
            var productTypeProp = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ProductId.ToString());
            var result1 = int.TryParse(productTypeProp.Value, out int productType);
            if (productType < 1)
            {
                Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求失败，工件类型[{productTypeProp.Value}]不是大于1的整数", LogLevel.Error);
                return BllResultFactory.Error();
            }
            //获取自动任务
            var stepTracesResult = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"WHERE status = {StepTraceStatus.响应放货完成.GetIndexInt()} and stationId = {station.StationId} ORDER BY stationInTime DESC");
            if (!stepTracesResult.Success)
            {
                Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求失败，查询数据库爆错：{stepTracesResult.Msg}", LogLevel.Error);
                return BllResultFactory.Error();
            }
            if (stepTracesResult.Data.Count > 0)
            {
                if (stepTracesResult.Data.Count > 1)
                {
                    string redundanceID = "";
                    for (var i = 0; i < stepTracesResult.Data.Count; i++)
                    {
                        redundanceID += stepTracesResult.Data[i].Id;
                        redundanceID += ";";
                        //if (i > 0)
                        //{
                        //    AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate($"UPDATE step_trace SET status = 3 WHERE id = {stepTraces.Data[i].Id}");
                        //}
                    }
                    return BllResultFactory.Error($"站台[{station.Code}]，[{station.Name}]，[{station.StationId}]查找到了多条任务[{redundanceID}]，请检查任务数据");
                }
                stepTrace = stepTracesResult.Data[0];
                if (stepTrace.WcsProductType != productType)
                {
                    return BllResultFactory.Error($"站台[{station.Code}]，[{station.Name}]，[{station.StationId}]查找到的任务工件类型为[{stepTrace.WcsProductType}]，站台给出的工件类型是[{productType}]，请检查任务数据");
                }
            }
            else
            {
                var excutingStepTraces = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where status = {StepTraceStatus.下发放货任务.GetIndexInt()} and nextStationId = {station.StationId}");
                if (excutingStepTraces.Success)
                {
                    //如果有 请求站台有下发放货任务任务的数据，不需要处理
                    return BllResultFactory.Sucess();
                }
                var createTypeFlagPro = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.ManualSign.ToString());
                if (createTypeFlagPro == null)
                {
                    Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}][{workStation.Name}]下料请求时候,{createTypeFlagPro}为空，请核查基础数据配置", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var result = int.TryParse(createTypeFlagPro.Value, out int createTypeFlag);
                if (createTypeFlag == (int)CreateTypeFlag.手动任务)
                {
                    //var stepTraceCount = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where stationId={station.StationId} and status = {StepTraceStatus.响应放货完成.GetIndexInt()}");
                    //if (!stepTraceCount.Success)
                    //{
                    //    Logger.Log($"[{station.Name}]的站台[{station.StationId}]下料请求时候，生成人工确认任务失败;查询数据异常{stepTraceCount.Msg}", LogLevel.Error);
                    //    return BllResultFactory.Error();
                    //}
                    //if (stepTraceCount.Data.Count > 0)
                    //{
                    //    string ids = $"存在任务号为:{string.Join(",", stepTraceCount.Data.Select(t => t.Id).ToArray())}的任务没有完成";
                    //    Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}][{workStation.Name}]下料请求时候，生成人工确认任务失败，" +
                    //               $"当前站台存在[{stepTraceCount.Data.Count}]条任务:{ids}，请先把站台【{station.Name}】存在的任务完成掉", LogLevel.Error);
                    //    return BllResultFactory.Error();
                    //}

                    stepTrace = new StepTrace();
                    //获取产品信息
                    var productHeaderResult = AppSession.Dal.GetCommonModelByCondition<ProductHeader>($"where wcsProductType = '{productType}'");
                    if (!productHeaderResult.Success)
                    {
                        Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求时候，根据设备工件类型[{productType}]没有检测到对应的产品信息，原因：{productHeaderResult.Msg}，请检查产品基础数据", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    ////获取工位工序表--工位类型
                    //var stepStationResult = AppSession.Dal.GetCommonModelByCondition<StepStation>($"where stationId = '{station.StationId}'");
                    //if (!stepStationResult.Success)
                    //{
                    //    Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求时候，根据站台[{station.StationId}]没有检测到工序工位，原因：{stepStationResult.Msg}，请检查 [工序工位] 基础数据", LogLevel.Error);
                    //    return BllResultFactory.Error();
                    //}
                    //获取工序表
                    var stepResult = AppSession.Dal.GetCommonModelBySql<Step>($"select top 1 step.* from step inner join step_station on step.id = step_station.stepId and step.productId = {productHeaderResult.Data[0].Id} and step_station.stationId = {station.StationId} ");
                    if (!stepResult.Success)
                    {
                        Logger.Log($"处理设备[{station.Name}]的站台[{station.StationId}]下料请求时候，根据产品标识[{productHeaderResult.Data[0].Id}]和工位标识[{station.StationId}]没有查询到工序，原因：{stepResult.Msg}，请检查 [工序] 基础数据", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    var step = stepResult.Data[0];
                    stepTrace.StepId = step.Id.Value;
                    //工件型号
                    stepTrace.WcsProductType = productType;
                    stepTrace.StationId = station.StationId;
                    stepTrace.ProductCode = step.ProductCode;
                    stepTrace.Status = StepTraceStatus.响应放货完成.GetIndexInt();
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

            var requestNumber = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestNumber.ToString()).Value;
            //***********************************************************************************************************************
            // 根据托盘上一个入库性质的任务进行查找其长宽高重信息  这个其实不重要---so,what do you want?
            var requestWeight = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWeight.ToString()).Value;
            var requestLength = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestLength.ToString()).Value;
            var requestWidth = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestWidth.ToString()).Value;
            var requestHeight = station.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == StationProps.RequestHeight.ToString()).Value;

            string goAddress = station.GoAddress;
            if (string.IsNullOrWhiteSpace(goAddress) || goAddress == "0")
            {
                return BllResultFactory.Error($"没有配置站台{station.Code}的去向地址，系统也找不到任务{stepTrace.Id}对应的去向地址");
            }
            //记录旧数据
            var updateTime = stepTrace.UpdateTime;
            var updateBy = stepTrace.UpdateBy;
            int tempStatus = stepTrace.Status;
            //更新数据
            stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
            stepTrace.UpdateTime = DateTime.Now;
            stepTrace.UpdateBy = App.User.UserCode;
            var updateResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
            if (updateResult.Success)
            {
                //回复地址请求
                var temp = SendAddressReplyToPlc(station, plc, StationMessageFlag.地址回复, StationLoadStatus.默认, requestNumber, stepTrace.Id.ToString(),
                                                 requestWeight, requestLength, requestWidth, requestHeight, goAddress, "0");
                if (temp.Success)
                {
                    if (stepTrace.CreateType == CreateTypeFlag.手动任务.GetIndexInt())
                    {
                        return BllResultFactory.Sucess($"响应站台{station.Code}{station.Name}地址请求，人工确认任务成功，任务{stepTrace.Id}");
                    }
                    else
                    {
                        return BllResultFactory.Sucess($"响应站台{station.Code}{station.Name}地址请求成功，任务{stepTrace.Id}");
                    }
                }
                else
                {
                    stepTrace.Status = tempStatus;
                    stepTrace.UpdateTime = updateTime;
                    stepTrace.UpdateBy = updateBy;
                    AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                    return BllResultFactory.Error($"响应站台{station.Code}{station.Name}地址请求失败，任务{stepTrace.Id}，写入PLC失败：{temp.Msg}，回滚任务状态");
                }
            }
            else
            {
                return BllResultFactory.Error($"站台{station.Code}{station.Name}，响应请求时更新任务{stepTrace.Id}状态失败，写入数据库失败：{updateResult.Msg}。");
            }
        }
        public override BllResult ExcuteArrive(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            return BllResultFactory.Error($"{station.Code}{station.Name}不执行到达处理");
        }

    }
}
