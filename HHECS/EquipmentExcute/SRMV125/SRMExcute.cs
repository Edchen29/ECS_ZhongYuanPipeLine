using HHECS.Bll;
using HHECS.Model;
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

namespace HHECS.EquipmentExcute.SRMV125
{
    public abstract class SRMExcute
    {
        /// <summary> 
        /// 对应的设备类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 对应的站台信息
        /// </summary>
        public List<Equipment> Stations { get; set; }
        /// <summary>
        /// 执行堆垛机处理逻辑
        /// </summary>
        /// <param name="stockers"></param>
        /// <param name="plcs"></param>
        /// <returns></returns>
        public abstract BllResult Excute(List<Equipment> stockers, List<Equipment> allEquipments, IPLC plc);

        /// <summary>
        /// 下发数据到WCS交换区
        /// </summary>
        /// <param name="stocker"></param>
        /// <param name="plc"></param>
        /// <param name="forkAction"></param>
        /// <param name="forkTaskFlag"></param>
        /// <param name="forkRow"></param>
        /// <param name="forkColumn"></param>
        /// <param name="forkLayer"></param>
        /// <param name="forkStation"></param>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public BllResult SendTaskToStocker(Equipment stocker, IPLC plc, SRMForkAction forkAction, SRMForkTaskFlag forkTaskFlag, string forkRow, string forkColumn, string forkLayer, string forkStation, string taskNo)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = stocker.EquipmentProps;
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
        /// 高速堆垛机的下发给PLC的调用函数
        /// 由于只有一个叉，动作类型暂时保留固定写单叉动作。 
        /// </summary>
        /// <param name="stocker">堆垛机编码</param>
        /// <param name="plc">对应写入PLC的OPC连接</param>
        /// <param name="taskMode">任务模型</param>
        /// <param name="forkTaskFlag">取货任务的任务类型</param>
        /// <param name="forkRow">取货的行（2,1，3,4）</param>
        /// <param name="forkColumn">取货的列</param>
        /// <param name="forkLayer">取货的层</param>
        /// <param name="forkStation">取货的站台</param>
        /// <param name="forkTaskFlag2">放货的任务类型</param>
        /// <param name="forkRow2">放货的行（2,1，3,4）</param>
        /// <param name="forkColumn2">放货的列</param>
        /// <param name="forkLayer2">放货的层</param>
        /// <param name="forkStation2">放货的站台</param>
        /// <param name="taskNo">任务号</param>
        /// <param name="taskAccount">任务过账 </param>
        /// <returns></returns>
        public BllResult SendTaskToStocker(Equipment stocker, IPLC plc, SuperSRMTaskMode taskMode,
            SRMForkTaskFlag forkTaskFlag, string forkRow, string forkColumn, string forkLayer, string forkStation ,
            SRMForkTaskFlag forkTaskFlag2, string forkRow2, string forkColumn2, string forkLayer2, string forkStation2, 
            string taskNo, TaskAccount taskAccount)
        {
            try
            {
                List<EquipmentProp> propsToWriter = new List<EquipmentProp>();
                var props = stocker.EquipmentProps;
                var action = props.Find(t => t.EquipmentTypeTemplateCode == "WCSForkTaskMode");
                action.Value = taskMode.GetIndexString();
                var taskFlag = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1TaskFlag");
                taskFlag.Value = forkTaskFlag.GetIndexString();
                var row = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1Row");
                row.Value = forkRow;
                var column = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1Column");
                column.Value = forkColumn;
                var layer = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1Layer");
                layer.Value = forkLayer;
                var station = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1Station");
                station.Value = forkStation;
                var taskFlag2 = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1TaskFlag2");
                taskFlag2.Value = forkTaskFlag2.GetIndexString();
                var row2 = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1Row2");
                row2.Value = forkRow2;
                var column2 = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1Column2");
                column2.Value = forkColumn2;
                var layer2 = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1Layer2");
                layer2.Value = forkLayer2;
                var station2 = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1Station2");
                station2.Value = forkStation2;
                var task = props.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1Task");
                task.Value = taskNo;
                var account = props.Find(t => t.EquipmentTypeTemplateCode == "WCSTaskAccount");
                account.Value = taskAccount.GetIndexString();
                propsToWriter.AddRange(new List<EquipmentProp>() { action, taskFlag, row, column, layer, station, taskFlag2, row2, column2, layer2, station2, task, account });
                return plc.Writes(propsToWriter);
            }
            catch (Exception ex)
            {
                Logger.Log($"PLC写入信息错误：" + ex.Message, LogLevel.Exception);
                return BllResultFactory.Error($"下发任务出现异常:{ex.Message}");
            }
        }
        
        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="stocker"></param>
        /// <param name="plc"></param>
        public void Heartbeat(Equipment stocker, IPLC plc)
        {
            var prop = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSHeartBeat.ToString());
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
                Logger.Log($"发送堆垛机{stocker.Name}心跳数据失败:{result.Msg}", LogLevel.Error);
            }
        }

        /// <summary>
        /// 清理WCS交换区
        /// </summary>
        /// <param name="stocker"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ClearWCSData(Equipment stocker, IPLC plc)
        {
            //表示堆垛机已经收到WCS发送给他的任务完成信号，此时WCS可以清除自己的交换区地址
            BllResult sendResult = SendTaskToStocker(stocker, plc, SRMForkAction.无, SRMForkTaskFlag.无任务, "0", "0", "0", "0", "0");
            if (sendResult.Success)
            {
                Logger.Log($"任务完成后，清除堆垛机{stocker.Name}交换区地址成功", LogLevel.Info);
                return BllResultFactory.Sucess();
            }
            else
            {
                Logger.Log($"任务完成后，清除堆垛机{stocker.Name}交换区地址失败", LogLevel.Error);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 清理WCS交换区
        /// </summary>
        /// <param name="stocker"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ClearWCSData(Equipment stocker, IPLC plc, TaskAccount account)
        {
            //表示堆垛机已经收到WCS发送给他的任务完成信号，此时WCS可以清除自己的交换区地址
            BllResult sendResult = SendTaskToStocker(stocker, plc, SuperSRMTaskMode.无,
                SRMForkTaskFlag.无任务, "0", "0", "0", "0",
                SRMForkTaskFlag.无任务, "0", "0", "0", "0",
                "0", account);
            if (sendResult.Success)
            {
                Logger.Log($"任务完成后，清除堆垛机{stocker.Name}交换区地址成功", LogLevel.Info);
                return BllResultFactory.Sucess();
            }
            else
            {
                Logger.Log($"任务完成后，清除堆垛机{stocker.Name}交换区地址失败", LogLevel.Error);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 通用条件验证
        /// </summary>
        /// <param name="stocker"></param>
        /// <returns></returns>
        public BllResult Validate(Equipment stocker)
        {
            //联机，无故障
            if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.OperationModel.ToString()).Value == SRMOperationModel.联机.GetIndexString() &&
                stocker.EquipmentProps.Where(t => t.EquipmentTypeTemplate.IsMonitor == true).Count(t => t.Value != t.EquipmentTypeTemplate.MonitorCompareValue) == 0)
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 下发库内取货与放货任务 相对于库位来说
        /// </summary>
        /// <param name="stocker"></param>
        /// <param name="plc"></param>
        /// <param name="task"></param>
        /// <param name="tempFirstStatus"></param>
        /// <param name="tempStatus"></param>
        /// <param name="tempGateway"></param>
        /// <param name="tempLocation"></param>
        /// <returns></returns>
        protected BllResult SendTaskToLocation(Equipment stocker, IPLC plc, TaskEntity task, Location tempLocation, TaskEntityStatus taskEntityStatus, SRMForkTaskFlag forkTaskFlag, int tempStatus)
        {
            task.TaskStatus = taskEntityStatus.GetIndexInt();
            //task.ArriveEquipmentCode = stocker.Code;
            var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
            if (tempResult.Success)
            {
                var rowIndex = stocker.Code == tempLocation.SrmCode ? tempLocation.RowIndex1 : tempLocation.RowIndex2;
                BllResult sendResult = SendTaskToStocker(stocker, plc, SRMForkAction.货叉1号, forkTaskFlag, rowIndex.ToString(), tempLocation.Line.ToString(), tempLocation.Layer.ToString(), "0", task.Id.ToString());
                if (sendResult.Success)
                {
                    Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}成功,目标库位:{tempLocation.Code}", LogLevel.Success);
                    return BllResultFactory.Sucess();
                }
                else
                {
                    Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}失败:{sendResult.Msg};回滚任务{task.Id}状态。", LogLevel.Error);
                    //回滚任务状态，记录日志
                    task.TaskStatus = tempStatus;
                    //task.ArriveEquipmentCode = tempGateway;
                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    return BllResultFactory.Error();
                }
            }
            else
            {
                Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}失败，更新任务{task.Id}状态失败：{tempResult.Msg}；任务未下发", LogLevel.Error);
                return BllResultFactory.Error($"下发堆垛机{stocker.Name}{forkTaskFlag}，更新任务{task.Id}状态失败：{tempResult.Msg}");
            }
        }

        /// <summary>
        /// 下发堆垛机库外取放货任务  相对于站台来说的
        /// </summary>
        /// <param name="stocker"></param>
        /// <param name="plc"></param>
        /// <param name="task"></param>
        /// <param name="taskEntityStatus"></param>
        /// <param name="forkTaskFlag"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        protected BllResult SendTaskToStation(Equipment stocker, IPLC plc, TaskEntity task, TaskEntityStatus taskEntityStatus, SRMForkTaskFlag forkTaskFlag, Equipment station, int tempStatus)
        {
            task.TaskStatus = taskEntityStatus.GetIndexInt();
            //task.ArrivaEquipmentCode = stocker.Code;
            var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
            if (tempResult.Success)
            {
                BllResult sendResult = SendTaskToStocker(stocker, plc, SRMForkAction.货叉1号, forkTaskFlag, "0", "0", "0", station.StationIndex.ToString(), task.Id.ToString());

                if (sendResult.Success)
                {
                    Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}成功,接货站台为：{station.Name}", LogLevel.Success);
                    return BllResultFactory.Sucess();
                }
                else
                {
                    Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}失败:{sendResult.Msg};回滚任务{task.Id}状态。", LogLevel.Error);
                    //回滚任务状态，记录日志
                    task.TaskStatus = tempStatus;
                    //task.ArriveEquipmentCode = tempGateway;
                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    return BllResultFactory.Error();
                }
            }
            else
            {
                Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{forkTaskFlag}失败，更新任务{task.Id}状态失败：{tempResult.Msg}；任务未下发", LogLevel.Error);
                return BllResultFactory.Error($"下发堆垛机{stocker.Name}库外放货时，更新任务{task.Id}状态失败：{tempResult.Msg}");
            }
        }

        /// <summary>
        /// 重新下发堆垛机任务
        /// </summary>
        /// <param name="stocker"></param>
        /// <param name="plc"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="layer"></param>
        /// <param name="forkStation"></param>
        /// <param name="taskForResend"></param>
        /// <param name="forkTaskFlag"></param>
        /// <returns></returns>
        protected BllResult ReSendTask(Equipment stocker, IPLC plc, string row, string column, string layer, string forkStation, TaskEntity taskForResend, SRMForkTaskFlag forkTaskFlag)
        {
            BllResult sendResult = SendTaskToStocker(stocker, plc, SRMForkAction.货叉1号, forkTaskFlag, row, column, layer, forkStation, taskForResend.Id.ToString());
            if (sendResult.Success)
            {
                taskForResend.SendAgain = 2;
                AppSession.Dal.UpdateCommonModel<TaskEntity>(taskForResend);
                Logger.Log($"重新下发堆垛机{stocker.Name},任务：{taskForResend.Id},任务类型：{taskForResend.TaskType},货叉类型：{forkTaskFlag}成功,出库口:{taskForResend.ToPort}", LogLevel.Info);
                return BllResultFactory.Sucess();
            }
            else
            {
                Logger.Log($"重新下发堆垛机{stocker.Name},任务：{taskForResend.Id},任务类型：{taskForResend.TaskType},货叉类型：{forkTaskFlag}失败:{sendResult.Msg};", LogLevel.Error);
                return BllResultFactory.Error();
            }
        }
    }
}
