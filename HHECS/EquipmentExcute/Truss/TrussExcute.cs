using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.SRM;
using HHECS.Model.Enums.Task;
using HHECS.Model.Enums.Truss;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HHECS.EquipmentExcute.Truss
{
    public abstract class TrussExcute
    {
        /// <summary> 
        /// 对应的设备类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 执行桁车处理逻辑
        /// </summary>
        /// <param name="srms"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public abstract BllResult Excute(List<Equipment> trusses, List<Equipment> allEquipments, IPLC plc);


        /// <summary>
        /// 下发数据到桁车
        /// </summary>
        /// <param name="truss"></param>
        /// <param name="plc"></param>
        /// <param name="taskFlag"></param>
        /// <param name="productId"></param>
        /// <param name="position"></param>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public BllResult SendTaskToTruss(Equipment truss, IPLC plc, TrussTaskFlag taskFlag, string productId, string stationId, string wcsLine, string wcsLayer, string taskNo)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = truss.EquipmentProps;
                var WCSProductId = props.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSProductId.ToString());
                WCSProductId.Value = productId;
                var WCSStationId = props.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSStationId.ToString());
                WCSStationId.Value = stationId;
                var WCSLine = props.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSLine.ToString());
                WCSLine.Value = wcsLine;
                var WCSLayer = props.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSLayer.ToString());
                WCSLayer.Value = wcsLayer;
                var WCSTaskNo = props.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskNo.ToString());
                WCSTaskNo.Value = taskNo;
                var WCSForkAction = props.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSForkAction.ToString());
                WCSForkAction.Value = TrussForkAction.一号机械手.GetIndexString();
                var WCSTaskFlag = props.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskFlag.ToString());
                WCSTaskFlag.Value = taskFlag.GetIndexString();
                propsToWriter.AddRange(new List<EquipmentProp>() { WCSProductId, WCSStationId, WCSLine, WCSLayer, WCSTaskNo, WCSForkAction, WCSTaskFlag });
                return plc.Writes(propsToWriter);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"写入plc失败，下发任务出现异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 清理WCS交换区
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ClearWCSData(Equipment truss, IPLC plc)
        {
            //var rowIndex1 = truss.RowIndex1;
            //var rowIndex2 = truss.RowIndex2;

            //truss.RowIndex1 = 0;
            //truss.RowIndex2 = 0;
            //var updateResult = AppSession.Dal.UpdateCommonModel<Equipment>(truss);
            //if (updateResult.Success)
            //{
            //表示桁车已经收到WCS发送给他的任务完成信号，此时WCS可以清除自己的交换区地址
            BllResult sendResult = SendTaskToTruss(truss, plc, TrussTaskFlag.无任务, "0", "0", "0", "0", "0");
            if (sendResult.Success)
            {
                Logger.Log($"任务完成后，清除桁车{truss.Name}交换区地址成功", LogLevel.Info);
                return BllResultFactory.Sucess();
            }
            else
            {
                //truss.RowIndex1 = rowIndex1;
                //truss.RowIndex2 = rowIndex2;
                //AppSession.Dal.UpdateCommonModel<Equipment>(truss);
                Logger.Log($"任务完成后，清除桁车{truss.Name}交换区地址失败, 原因：${sendResult.Msg}", LogLevel.Error);
                return BllResultFactory.Error();
            }
            //}
            //Logger.Log($"任务完成后，清除桁车{truss.Name}交换区地址失败，更新桁车路径数据失败，原因{updateResult.Msg}", LogLevel.Error);
            //return BllResultFactory.Error();
        }

        /// <summary>
        /// 发送到站台的任务
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <param name="stepTrace"></param>
        /// <param name="taskEntityStatus"></param>
        /// <param name="taskFlag"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        protected BllResult SendTaskToStation(Equipment truss, IPLC plc, StepTrace stepTrace, StepTraceStatus taskEntityStatus, TrussTaskFlag taskFlag, string stationId)
        {
            var station = truss.StationList.FirstOrDefault(t => t.Id.ToString() == stationId);
            if (station == null)
            {
                Logger.Log($"未查询到ID为{stationId}的站台", LogLevel.Error);
                return BllResultFactory.Error();
            }
            var tempStatus = stepTrace.Status;
            int trussStationId = 0;
            string wcsLine = "0";
            string wcsLayer = "0";
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    if (taskFlag == TrussTaskFlag.机械手取货)
                    {
                        trussStationId = int.Parse(station.TrussTakeStationId.ToString());
                    }
                    else if (taskFlag == TrussTaskFlag.机械手放货)
                    {
                        trussStationId = int.Parse(station.TrussPutStationId.ToString());
                        wcsLine = string.IsNullOrEmpty(stepTrace.WCSLine) ? "0" : stepTrace.WCSLine;
                        wcsLayer = string.IsNullOrEmpty(stepTrace.WCSLayer) ? "0" : stepTrace.WCSLayer;
                    }
                    if (trussStationId == 0)
                    {
                        Logger.Log($"站台[{station.Id}][{station.Name}]没有对应的桁架点位数据，请补齐数据", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    stepTrace.Status = taskEntityStatus.GetIndexInt();
                    stepTrace.SrmCode = truss.Code;

                    connection.Open();
                    tran = connection.BeginTransaction();

                    connection.Update<StepTrace>(stepTrace, tran);
                    connection.Update<Equipment>(truss, tran);

                    BllResult sendResult = SendTaskToTruss(truss, plc, taskFlag, stepTrace.WcsProductType.ToString(), trussStationId.ToString(), wcsLine, wcsLayer, stepTrace.Id.ToString());
                    if (sendResult.Success)
                    {
                        tran.Commit();
                        Logger.Log($"下发桁车{truss.Name}任务,任务id：{stepTrace.Id},操作类型：{taskFlag},目的站台:{stationId} {station.Name}，桁车点位{trussStationId}，成功", LogLevel.Success);
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        tran?.Rollback();
                        stepTrace.Status = tempStatus;
                        truss.RowIndex1 = 0;
                        truss.RowIndex2 = 0;
                        Logger.Log($"下发桁车{truss.Name}任务,任务id：{stepTrace.Id},操作类型：{taskFlag},目的站台:{stationId} {station.Name}，桁车点位{trussStationId}，失败:{sendResult.Msg}，回滚任务{stepTrace.Id}状态。", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    stepTrace.Status = tempStatus;
                    truss.RowIndex1 = 0;
                    truss.RowIndex2 = 0;
                    Logger.Log($"下发桁车{truss.Name}任务,任务id：{stepTrace.Id},操作类型：{taskFlag},目的站台:{stationId} {station.Name}，桁车点位{trussStationId}，失败：{ex.Message}，回滚任务{stepTrace.Id}状态。", LogLevel.Exception, ex);
                    return BllResultFactory.Error();
                }
            }
        }

        /// <summary>
        /// 重新下发桁车任务
        /// 重新下发任务限制：
        /// 取货：货叉在中心、联机、无货
        /// 放货：货叉在中心、联机、有货
        /// </summary>
        /// <param name="truss">桁架</param>
        /// <param name="plc"></param>
        /// <param name="stepTrace"></param>
        /// <param name="taskFlag">货叉标志</param>
        /// <param name="stationId">站台点位</param>
        /// <returns></returns>
        protected BllResult ReSendTask(Equipment truss, IPLC plc, StepTrace stepTrace, TrussTaskFlag taskFlag, string stationId)
        {
            var station = truss.StationList.FirstOrDefault(t => t.Id.ToString() == stationId);
            if (station == null)
            {
                Logger.Log($"未查询到ID为{stationId}的站台", LogLevel.Error);
                return BllResultFactory.Error();
            }
            int trussStationId = 0;
            string wcsLine = "0";
            string wcsLayer = "0";
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    if (taskFlag == TrussTaskFlag.机械手取货)
                    {
                        trussStationId = int.Parse(station.TrussTakeStationId.ToString());
                    }
                    else if (taskFlag == TrussTaskFlag.机械手放货)
                    {
                        trussStationId = int.Parse(station.TrussPutStationId.ToString());
                        wcsLine = string.IsNullOrEmpty(stepTrace.WCSLine) ? "0" : stepTrace.WCSLine;
                        wcsLayer = string.IsNullOrEmpty(stepTrace.WCSLayer) ? "0" : stepTrace.WCSLayer;
                    }
                    if (trussStationId == 0)
                    {
                        Logger.Log($"站台[{station.Id}][{station.Name}]没有对应的桁架点位数据，请补齐数据", LogLevel.Error);
                        return BllResultFactory.Error();
                    }

                    stepTrace.SendAgain = 2;
                    stepTrace.SrmCode = truss.Code;

                    connection.Open();
                    tran = connection.BeginTransaction();

                    connection.Update<StepTrace>(stepTrace, tran);
                    //TODO： 这里这个更新有用吗？？？？？？？
                    connection.Update<Equipment>(truss, tran);

                    BllResult sendResult = SendTaskToTruss(truss, plc, taskFlag, stepTrace.WcsProductType.ToString(), trussStationId.ToString(), wcsLine, wcsLayer, stepTrace.Id.ToString());
                    if (sendResult.Success)
                    {
                        tran?.Commit();
                        Logger.Log($"重新下发桁车{truss.Name}任务，任务id：{stepTrace.Id},操作类型：{taskFlag},目的站台:{stationId} {station.Name}，桁车点位{trussStationId}，成功", LogLevel.Info);
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        tran?.Rollback();
                        stepTrace.SendAgain = 1;
                        Logger.Log($"重新下发桁车{truss.Name}任务,任务id：{stepTrace.Id},操作类型：{taskFlag},目的站台:{stationId} {station.Name}，桁车点位{trussStationId}，失败:{sendResult.Msg};", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    stepTrace.SendAgain = 1;
                    Logger.Log($"重新下发桁车{truss.Name}任务,任务id：{stepTrace.Id},操作类型：{taskFlag},目的站台:{stationId} {station.Name}，桁车点位{trussStationId}，失败：{ex.Message}，回滚任务{stepTrace.Id}状态。", LogLevel.Exception, ex);
                    return BllResultFactory.Error();
                }
            }
        }



        #region 共用

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        public BllResult Heartbeat(Equipment srm, IPLC plc)
        {
            var prop = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSHeartBeat.ToString());
            if (prop.Value == "1")
            {
                prop.Value = "0";
            }
            else
            {
                prop.Value = "1";
            }
            var result = plc.Writes(new List<EquipmentProp>() { prop });
            if (result.Success)
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                Logger.Log($"发送桁车{srm.Name}心跳数据失败:{result.Msg}", LogLevel.Error);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 通用条件验证
        /// 验证桁车联机且无故障
        /// </summary>
        /// <param name="srm"></param>
        /// <returns></returns>
        public BllResult Validate(Equipment srm)
        {
            //联机，无故障
            if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.OperationModel.ToString()).Value != SRMOperationModel.联机.GetIndexString())
            {
                return BllResultFactory.Error($"桁车 {srm.Name} 不是联机");
            }
            if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.TotalError.ToString()).Value == "True")
            {
                return BllResultFactory.Error($"桁车 {srm.Name} 有故障");
            }
            return BllResultFactory.Sucess();
        }

        #endregion

    }
}
