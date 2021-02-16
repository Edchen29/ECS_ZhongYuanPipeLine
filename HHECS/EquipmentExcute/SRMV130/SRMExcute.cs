using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.SRM;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.SRMV130
{
    public abstract class SRMExcute
    {
        /// <summary> 
        /// 对应的设备类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 执行堆垛机处理逻辑
        /// </summary>
        /// <param name="srms"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public abstract BllResult Excute(List<Equipment> srms, List<Equipment> allEquipments, IPLC plc);

        #region 双叉堆垛机

        #region 双叉单用多任务
        /// <summary>
        /// 下发数据到WCS交换区
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <param name="forkAction"></param>
        /// <param name="forkTaskFlag"></param>
        /// <param name="forkRow"></param>
        /// <param name="forkColumn"></param>
        /// <param name="forkLayer"></param>
        /// <param name="forkStation"></param>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public BllResult SendTaskToSRM(Equipment srm, IPLC plc, SRMForkAction forkAction, SRMForkTaskFlag forkTaskFlag, string forkRow, string forkColumn, string forkLayer, string forkStation, string taskNo)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = srm.EquipmentProps;
                var action = props.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSForkAction.ToString());
                action.Value = forkAction.GetIndexString();
                var taskFlag = props.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                taskFlag.Value = forkTaskFlag.GetIndexString();
                var row = props.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1Row.ToString());
                row.Value = forkRow;
                var column = props.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1Column.ToString());
                column.Value = forkColumn;
                var layer = props.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1Layer.ToString());
                layer.Value = forkLayer;
                var station = props.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1Station.ToString());
                station.Value = forkStation;
                var task = props.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1Task.ToString());
                task.Value = taskNo;
                propsToWriter.AddRange(new List<EquipmentProp>() { action, taskFlag, row, column, layer, station, task });
                return plc.Writes(propsToWriter);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"下发任务出现异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 清理WCS交换区
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ClearWCSData(Equipment srm, IPLC plc)
        {
            //表示堆垛机已经收到WCS发送给他的任务完成信号，此时WCS可以清除自己的交换区地址
            BllResult sendResult = SendTaskToSRM(srm, plc, SRMForkAction.无, SRMForkTaskFlag.无任务, "0", "0", "0", "0", "0");
            if (sendResult.Success)
            {
                Logger.Log($"任务完成后，清除堆垛机{srm.Name}交换区地址成功", LogLevel.Info);
                return BllResultFactory.Sucess();
            }
            else
            {
                Logger.Log($"任务完成后，清除堆垛机{srm.Name}交换区地址失败", LogLevel.Error);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 下发库内取货与放货任务 相对于库位来说
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <param name="task"></param>
        /// <param name="tempFirstStatus"></param>
        /// <param name="tempStatus"></param>
        /// <param name="tempGateway"></param>
        /// <param name="tempLocation"></param>
        /// <returns></returns>
        protected BllResult SendTaskToLocation(Equipment srm, IPLC plc, TaskEntity task, Location tempLocation, TaskEntityStatus taskEntityStatus, SRMForkTaskFlag forkTaskFlag, int tempStatus)
        {
            task.TaskStatus = taskEntityStatus.GetIndexInt();
            var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
            if (tempResult.Success)
            {
                // short rowIndex = srm.Code == tempLocation.SrmCode ? tempLocation.RowIndex1.Value : tempLocation.RowIndex2.Value;
                short rowIndex = 0;
                 BllResult sendResult = SendTaskToSRM(srm, plc, SRMForkAction.货叉1号, forkTaskFlag, rowIndex.ToString(), tempLocation.Line.ToString(), tempLocation.Layer.ToString(), "0", task.Id.ToString());
                if (sendResult.Success)
                {
                    Logger.Log($"下发堆垛机{srm.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}成功,目标库位:{tempLocation.Code}", LogLevel.Success);
                    return BllResultFactory.Sucess();
                }
                else
                {
                    Logger.Log($"下发堆垛机{srm.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}失败:{sendResult.Msg};回滚任务{task.Id}状态。", LogLevel.Error);
                    //回滚任务状态，记录日志
                    task.TaskStatus = tempStatus;
                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    return BllResultFactory.Error();
                }
            }
            else
            {
                Logger.Log($"下发堆垛机{srm.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}失败，更新任务{task.Id}状态失败：{tempResult.Msg}；任务未下发", LogLevel.Error);
                return BllResultFactory.Error($"下发堆垛机{srm.Name}{forkTaskFlag}，更新任务{task.Id}状态失败：{tempResult.Msg}");
            }
        }

        /// <summary>
        /// 下发堆垛机库外取放货任务  相对于站台来说的
        /// 注意，需要指定口和排索引
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <param name="task"></param>
        /// <param name="taskEntityStatus"></param>
        /// <param name="forkTaskFlag"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        protected BllResult SendTaskToStation(Equipment srm, IPLC plc, TaskEntity task, TaskEntityStatus taskEntityStatus, SRMForkTaskFlag forkTaskFlag, Equipment station, int tempStatus)
        {
            task.TaskStatus = taskEntityStatus.GetIndexInt();
            var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
            if (tempResult.Success)
            {
                BllResult sendResult = SendTaskToSRM(srm, plc, SRMForkAction.货叉1号, forkTaskFlag, station.RowIndex1.ToString(), "0", "0", station.StationIndex.ToString(), task.Id.ToString());

                if (sendResult.Success)
                {
                    Logger.Log($"下发堆垛机{srm.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}成功,接货站台为：{station.Name}", LogLevel.Success);
                    return BllResultFactory.Sucess();
                }
                else
                {
                    Logger.Log($"下发堆垛机{srm.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}失败:{sendResult.Msg};回滚任务{task.Id}状态。", LogLevel.Error);
                    //回滚任务状态，记录日志
                    task.TaskStatus = tempStatus;
                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    return BllResultFactory.Error();
                }
            }
            else
            {
                Logger.Log($"下发堆垛机{srm.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}失败，更新任务{task.Id}状态失败：{tempResult.Msg}；任务未下发", LogLevel.Error);
                return BllResultFactory.Error($"下发堆垛机{srm.Name}库外放货时，更新任务{task.Id}状态失败：{tempResult.Msg}");
            }
        }

        /// <summary>
        /// 重新下发堆垛机任务
        /// 重新下发任务限制：
        /// 取货：货叉在中心、联机、无货
        /// 放货：货叉在中心、联机、有货
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="layer"></param>
        /// <param name="forkStation">对应堆垛机的出入口，wcs的堆垛机出入站台；库内取放为0</param>
        /// <param name="taskForResend"></param>
        /// <param name="forkTaskFlag"></param>
        /// <returns></returns>
        protected BllResult ReSendTask(Equipment srm, IPLC plc, string row, string column, string layer, string forkStation, TaskEntity taskForResend, SRMForkTaskFlag forkTaskFlag)
        {
            BllResult sendResult = SendTaskToSRM(srm, plc, SRMForkAction.货叉1号, forkTaskFlag, row, column, layer, forkStation, taskForResend.Id.ToString());
            if (sendResult.Success)
            {
                taskForResend.SendAgain = 2;
                AppSession.Dal.UpdateCommonModel<TaskEntity>(taskForResend);
                Logger.Log($"重新下发堆垛机{srm.Name},任务：{taskForResend.Id},任务类型：{taskForResend.TaskType},货叉类型：{forkTaskFlag}成功,出库口:{taskForResend.ToPort}", LogLevel.Info);
                return BllResultFactory.Sucess();
            }
            else
            {
                Logger.Log($"重新下发堆垛机{srm.Name},任务：{taskForResend.Id},任务类型：{taskForResend.TaskType},货叉类型：{forkTaskFlag}失败:{sendResult.Msg};", LogLevel.Error);
                return BllResultFactory.Error();
            }
        }
        #endregion

        #region 双叉双用多任务

        #endregion

        #endregion

        #region 单叉整任务


        /// <summary>
        /// 单叉整任务下发
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <param name="newTaskFlag"></param>
        /// <param name="getForkTaskFlag"></param>
        /// <param name="getForkRow"></param>
        /// <param name="getForkColumn"></param>
        /// <param name="getForkLayer"></param>
        /// <param name="getForkStation"></param>
        /// <param name="putForkTaskFlag"></param>
        /// <param name="putForkRow"></param>
        /// <param name="putForkColumn"></param>
        /// <param name="putForkLayer"></param>
        /// <param name="putForkStation"></param>
        /// <param name="taskNo"></param>
        /// <param name="forkTaskFlag"></param>
        /// <returns></returns>
        public BllResult SendTaskToSRM(Equipment srm, IPLC plc, SSRMNewTaskFlag newTaskFlag,
            SRMForkTaskFlag getForkTaskFlag, string getForkRow, string getForkColumn, string getForkLayer, string getForkStation,
            SRMForkTaskFlag putForkTaskFlag, string putForkRow, string putForkColumn, string putForkLayer, string putForkStation,
            string taskNo, SSRMTaskCompleteFlag completeFlag)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = srm.EquipmentProps;

                var action = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSNewTask.ToString());
                action.Value = newTaskFlag.GetIndexString();

                //--

                var getTaskFlag = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSGetFork1TaskFlag.ToString());
                getTaskFlag.Value = getForkTaskFlag.GetIndexString();

                var getRow = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSGetRow.ToString());
                getRow.Value = getForkRow;

                var getStation = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSGetPort.ToString());
                getStation.Value = getForkStation;

                var getColumn = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSGetColumn.ToString());
                getColumn.Value = getForkColumn;

                var getLayer = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSGetLayer.ToString());
                getLayer.Value = getForkLayer;

                //-- 

                var putTaskFlag = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSPutFork1TaskFlag.ToString());
                putTaskFlag.Value = putForkTaskFlag.GetIndexString();

                var putRow = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSPutRow.ToString());
                putRow.Value = putForkRow;

                var putStation = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSPutPort.ToString());
                putStation.Value = putForkStation;

                var putColumn = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSPutColumn.ToString());
                putColumn.Value = putForkColumn;

                var putLayer = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSPutLayer.ToString());
                putLayer.Value = putForkLayer;

                // - 
                var taskNoProp = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSTaskNo.ToString());
                taskNoProp.Value = taskNo;

                // -
                var taskFlagProp = props.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSTaskCompleteFlag.ToString());
                taskFlagProp.Value = completeFlag.GetIndexString();


                propsToWriter.AddRange(new List<EquipmentProp>() { action, getTaskFlag, getRow, getColumn, getLayer, getStation, putTaskFlag, putRow, putStation, putColumn, putLayer, taskNoProp, taskFlagProp });
                return plc.Writes(propsToWriter);
            }
            catch (Exception ex)
            {
                Logger.Log($"PLC写入信息错误：" + ex.Message, LogLevel.Exception);
                return BllResultFactory.Error($"下发任务出现异常:{ex.Message}");
            }
        }


        /// <summary>
        /// 清理 单叉整任务 WCS交换区
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ClearWCSDataS(Equipment srm, IPLC plc)
        {
            //表示堆垛机已经收到WCS发送给他的任务完成信号，此时WCS可以清除自己的交换区地址
            BllResult sendResult = SendTaskToSRM(srm, plc, SSRMNewTaskFlag.无,
                SRMForkTaskFlag.无任务, "0", "0", "0", "0",
                SRMForkTaskFlag.无任务, "0", "0", "0", "0",
                "0", SSRMTaskCompleteFlag.无完成);
            if (sendResult.Success)
            {
                Logger.Log($"任务完成后，清除堆垛机{srm.Name}交换区地址成功", LogLevel.Info);
                return BllResultFactory.Sucess();
            }
            else
            {
                Logger.Log($"任务完成后，清除堆垛机{srm.Name}交换区地址失败", LogLevel.Error);
                return BllResultFactory.Error();
            }
        }

        #endregion

        #region 共用

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        public void Heartbeat(Equipment srm, IPLC plc)
        {
            var prop = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSHeartBeat.ToString());
            if (prop.Value == "1")
            {
                prop.Value = "0";
            }
            else
            {
                prop.Value = "1";
            }
            var result = plc.Writes(new List<EquipmentProp>() { prop });
            if (!result.Success)
            {
                Logger.Log($"发送堆垛机{srm.Name}心跳数据失败:{result.Msg}", LogLevel.Error);
            }
        }

        /// <summary>
        /// 通用条件验证
        /// 验证堆垛机联机且无故障
        /// </summary>
        /// <param name="srm"></param>
        /// <returns></returns>
        public BllResult Validate(Equipment srm)
        {
            //联机，无故障
            if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.OperationModel.ToString()).Value == SRMOperationModel.联机.GetIndexString() &&
                srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.TotalError.ToString()).Value == "False")
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error();
            }
        }

        #endregion

    }
}
