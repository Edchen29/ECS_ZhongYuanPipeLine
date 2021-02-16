using HHECS.Bll;
using HHECS.EquipmentExcute.Truss;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HHECS.EquipmentExcute.StationV162
{
    /// <summary>
    /// AGV站台
    /// </summary>
    public class StationForFinished : StationExcute
    {
        private static DateTime excuteDateTime = DateTime.MinValue;

        /// <summary>
        /// 具体的站台实现逻辑
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public override void Excute(List<Equipment> stations, List<Equipment> allEquipments, IPLC plc)
        {
            //每隔3秒处理一次到达和离开
            if (DateTime.Now.Subtract(excuteDateTime).Seconds > 3)
            {
                excuteDateTime = DateTime.Now;
            }
            else
            {
                return;
            }
            var locations = AppSession.Dal.GetCommonModelByCondition<ProductFinishedStation>($"where 1=1");
            if (locations.Success)
            {
                foreach (var station in stations)
                {
                    BllResult result = null;
                    try
                    {
                        var location = locations.Data.FirstOrDefault(t => t.Code == station.Code);
                        if (station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.AGV_Arrive_WCS.ToString())?.Value == true.ToString())
                        {
                            //如果AGV到达，并且库位是停用的，就改为可以用
                            if (location.IsStop == true)
                            {
                                location.IsStop = false;
                                AppSession.Dal.UpdateCommonModel<ProductFinishedStation>(location);
                                result = BllResultFactory.Sucess($"AGV站台[{station.Code}][{station.Name}]到达，对应的库位{location.Code}更改为可用");
                            }
                        }
                        if (station.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationProps.AGV_Leave_WCS.ToString())?.Value == true.ToString())
                        {
                            //如果AGV不是到达，并且库位未锁定，就改为停用，恢复为1层1列
                            if (location.IsStop == false)
                            {
                                var stepTraceResult = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where status < {StepTraceStatus.下发放货任务.GetIndexInt()} and nextStationId = '{station.StationId}'");
                                if (stepTraceResult.Success)
                                {
                                    foreach (var stepTrace in stepTraceResult.Data.Where(t => t.Status < StepTraceStatus.下发取货任务.GetIndexInt()))
                                    {
                                        stepTrace.NextStationId = 0;
                                        stepTrace.WCSLayer = null;
                                        stepTrace.WCSLine = null;
                                        stepTrace.Status = StepTraceStatus.设备请求下料.GetIndexInt();
                                        AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                                    }
                                    foreach (var stepTrace in stepTraceResult.Data.Where(t => t.Status == StepTraceStatus.下发取货任务.GetIndexInt()))
                                    {
                                        var stationCode = string.Empty;

                                        switch (station.Code)
                                        {
                                            case "StationForFinished1":
                                                stationCode = "StationForFinished2";
                                                break;
                                            case "StationForFinished2":
                                                stationCode = "StationForFinished1";
                                                break;
                                            case "StationForFinished3":
                                                stationCode = "StationForFinished4";
                                                break;
                                            case "StationForFinished4":
                                                stationCode = "StationForFinished3";
                                                break;
                                        }
                                        var otherStation = stations.Find(t => t.Code == stationCode);
                                        if (otherStation != null)
                                        {
                                            List<Station> stationList = station.StationList.Where(t => t.Id == otherStation.StationId).ToList();
                                            TrussNormalExcute trussNormalExcutea = new TrussNormalExcute();
                                            trussNormalExcutea.setStation(stationList, stepTrace, stepTrace.NextStepId.Value);
                                        }
                                    }
                                }
                                location.PutType = (int)PutTypeFlag.初始摆放类型值;
                                location.Line = 1;
                                location.Layer = 1;
                                location.IsStop = true;
                                AppSession.Dal.UpdateCommonModel<ProductFinishedStation>(location);
                                result = BllResultFactory.Sucess($"AGV站台[{station.Code}][{station.Name}]离开，对应的库位{location.Code}更改为停用，并且恢复到1列1层");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result = BllResultFactory.Error($"AGV站台[{station.Code}][{station.Name}]处理发生异常：{ex.Message}");
                    }
                    LogResult(result);
                }
            }

        }


        public override BllResult ExcuteRequest(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            return BllResultFactory.Error($"AGV站台{station.Name}不处理请求");
        }

        public override BllResult ExcuteArrive(Equipment station, List<Equipment> allEquipments, IPLC plc)
        {
            return BllResultFactory.Error($"AGV站台{station.Name}不处理到达");
        }

    }
}
