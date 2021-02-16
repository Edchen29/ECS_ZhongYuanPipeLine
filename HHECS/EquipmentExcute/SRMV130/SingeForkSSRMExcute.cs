using System;
using System.Collections.Generic;
using System.Linq;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.SRM;
using HHECS.Model.Enums.Station;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;

namespace HHECS.EquipmentExcute.SRMV130
{
    /// <summary>
    /// 单叉单任务堆垛机实现
    /// 此实现目前可只用于单叉单伸位高速堆垛机；不用于转轨堆垛机；
    /// </summary>
    public class SingeForkSSRMExcute : SRMExcute
    {
        /// <summary>
        /// 记录堆垛机当前所在的列
        /// </summary>
        public int CurrentColumn { get; private set; }

        public override BllResult Excute(List<Equipment> stockers, List<Equipment> equipments, IPLC plc)
        {
            try
            {
                foreach (var stocker in stockers)
                {
                    CurrentColumn = int.Parse(stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.CurrentColumn.ToString()).Value);

                    ExcuteSingle(stocker, equipments, plc);
                    //心跳
                    Heartbeat(stocker, plc);
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"堆垛机处理过程中出现异常：{ex.Message}", LogLevel.Exception);
                return BllResultFactory.Error($"堆垛机处理过程中出现异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 控制逻辑实现 
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteSingle(Equipment srm, List<Equipment> allEquipments, IPLC plc)
        {
            //联机、无故障
            if (Validate(srm).Success)
            {
                #region 对于单叉堆垛机，判断这个货叉有误故障

                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.Fork1TotalError.ToString()).Value == "True")
                {
                    //如果报有故障，则返回
                    return BllResultFactory.Error("货叉1故障");
                }

                #endregion

                #region 任务执行判断
                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务执行中.GetIndexString())
                {
                    //任务执行中就return
                    return BllResultFactory.Sucess();
                }
                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务中断_出错.GetIndexString())
                {
                    //由人工处理，一般为空出和重入
                    return BllResultFactory.Sucess();
                }
                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.下发任务错误.GetIndexString())
                {
                    //由人工处理，需要重新下发任务
                    return BllResultFactory.Sucess();
                }
                #endregion

                #region 任务,货位和本巷道的其他设备

                //找出所有未完成的任务
                var tasksResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where taskStatus < {TaskEntityStatus.任务完成.GetIndexInt()} " +
                    $"and taskStatus>={TaskEntityStatus.下发任务.GetIndexInt()} and deleted = 0 and warehouseCode='{AppSession.WarehouseCode}'");
                if (!tasksResult.Success)
                {
                    //如果没有找到任务就直接返回
                    return BllResultFactory.Error(tasksResult.Msg);
                }
                //找出同巷道的库位，考虑到可能多个巷道移库，这里分别查询出所有库位和当前堆垛机所在巷道的库位
                var locationsResult = AppSession.LocationService.GetAllLocations(null, null, null, null, null, null, null, srm.WarehouseCode);
                if (!locationsResult.Success)
                {
                    return BllResultFactory.Error(locationsResult.Msg);
                }
                //所有库位
                var allLocations = locationsResult.Data;
                //本巷道库位
                var locationsRoadWay = allLocations.Where(t => t.RoadWay == srm.RoadWay).ToList();
                //找出本巷道的所有设备
                var equipmentsRoadWay = allEquipments.Where(t => t.RoadWay == srm.RoadWay && t.WarehouseCode == srm.WarehouseCode).ToList();
                //可用的可出站台
                //hack:注意，此处针对每个巷道均需要配置StationStatusMonitor！如果没有配置，则可出站台为空集合；
                var stationOutStatusMonitor = equipmentsRoadWay.Find(a => a.EquipmentType.Code == "StationStatusMonitor");
                var availableOutStation = stationOutStatusMonitor == null ? new List<Equipment>() : equipmentsRoadWay.Where(t => t.EquipmentType.Code.Contains("Station")
                     && stationOutStatusMonitor.EquipmentProps.
                         Count(b => b.EquipmentTypeTemplateCode == t.StationIndex.ToString() && b.Value == StationTaskLimit.可出.GetIndexString()) > 0).ToList();

                //hack:这里筛选本巷道或关联到本巷道的任务，规则为起始或目标库位，所以，当将来出现跨巷道移库时，需要特别处理跨巷道移库任务
                var tasks = tasksResult.Data.Where(t => locationsRoadWay.Count(a => a.Code == t.FromLocationCode || a.Code == t.ToLocationCode) > 0).ToList();

                //筛选任务，任务的前置任务如果存在，则其前置任务需要大于完成状态
                tasks = tasks.Where(t =>
                {
                    if (t.PreTaskId != 0)
                    {
                        var tempResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {t.PreTaskId}");
                        if (tempResult.Success)
                        {
                            var innerTask = tempResult.Data[0];
                            if (innerTask.TaskStatus >= (int)TaskEntityStatus.任务完成)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            Logger.Log($"未找到任务{t.Id}，远程任务号：{t.RemoteTaskNo}的前置远程任务：{t.PreRemoteTaskNo}，前置内部任务号：{t.PreTaskId}，该任务不允许执行。如果要执行，请取消其前置任务号", LogLevel.Warning);
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }).ToList();

                #endregion

                //堆垛机待机情况下
                string Fork1TaskExecuteStatus = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.Fork1TaskExcuteStatus.ToString()).Value;
                if (Fork1TaskExecuteStatus == SRMTaskExcuteStatus.待机.GetIndexString())
                {
                    //货叉任务待机时，可执行放和取任务，同时当执行完成时，交互后堆垛机会从任务完成更新为待机  
                    //均判断put放货任务标志
                    //响应任务删除
                    string WcsFork1TaskFlag = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSPutFork1TaskFlag.ToString()).Value;
                    if (WcsFork1TaskFlag == SRMForkTaskFlag.删除任务.GetIndexString())
                    {
                        return ClearWCSDataS(srm, plc);
                    }
                    //响应任务完成
                    string WcsTaskAccount = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSTaskCompleteFlag.ToString()).Value;
                    if (WcsTaskAccount == SRMForkTaskFlag.任务完成.GetIndexString())
                    {
                        return ClearWCSDataS(srm, plc);
                    }
                    //响应无任务且货叉在中心
                    else if (WcsFork1TaskFlag == SRMForkTaskFlag.无任务.GetIndexString() && srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.Fork1Center.ToString()).Value == "True")
                    {
                        //hack:单任务由人员手动处理，不重发
                        //获取需要下发给堆垛机的任务 
                        if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.Fork1HasPallet.ToString()).Value == "True")
                        {
                            Logger.Log($"堆垛机{srm.Name}显示货叉上有货且无任务，状态错误", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                        else
                        {
                            //判断任务限制情况
                            var tempTasks = tasks;
                            //完全空闲的堆垛机实际只执行取货任务，则为库外取与库内取两种
                            //找出下发状态的待取的离堆垛机最近的任务,并且对应站台口需要可用
                            var task1s = tempTasks.Where(t => t.TaskStatus == TaskEntityStatus.下发任务.GetIndexInt()
                            && (t.TaskType == TaskType.出库查看.GetIndexInt()
                                || t.TaskType == TaskType.分拣出库.GetIndexInt()
                                || t.TaskType == TaskType.整盘出库.GetIndexInt()
                                || t.TaskType == TaskType.盘点.GetIndexInt()
                                || t.TaskType == TaskType.空容器出库.GetIndexInt()
                                || t.TaskType == TaskType.补充入库.GetIndexInt()
                                || t.TaskType == TaskType.移库.GetIndexInt())
                            && locationsRoadWay.Exists(a => a.Code == t.FromLocationCode)).ToList();
                            task1s.ForEach(t =>
                            {
                                t.FromLocation = locationsRoadWay.Find(a => a.Code == t.FromLocationCode);
                                t.ToLocation = allLocations.Find(a => a.Code == t.ToLocationCode);
                                t.ToPortEquipment = equipmentsRoadWay.FirstOrDefault(a => a.Code == t.ToPort);
                            });
                            //任务过滤条件，任务port对应的station要可用
                            task1s = task1s.Where(t => availableOutStation.Exists(a => AppSession.ExcuteService.GetOutStationByPort(srm.DestinationArea, t.ToPort,App.WarehouseCode).Data?.Exists(b => b.Code == a.Code) == true)).ToList();

                            //库内取货的任务
                            var task1 = task1s.OrderByDescending(t => t.Priority).ThenBy(t => Math.Abs((int)t.FromLocation.Line - CurrentColumn)).FirstOrDefault();

                            //本巷道接入口的任务
                            var task2s = tempTasks.Where(t => t.TaskStatus == TaskEntityStatus.响应接入站台到达.GetIndexInt() && equipmentsRoadWay.Exists(a => a.Code == t.Gateway)).ToList();
                            task2s.ForEach(t => { t.ArrivaEquipment = equipmentsRoadWay.Find(a => a.Code == t.Gateway); t.ToLocation = allLocations.Find(a => a.Code == t.ToLocationCode); });
                            //按优先级以及站台与现有堆垛机的距离优先做调度
                            var task2 = task2s.OrderByDescending(t => t.Priority).ThenBy(t => Math.Abs(t.ArrivaEquipment.ColumnIndex - CurrentColumn)).FirstOrDefault();
                            if (task1 == null && task2 == null)
                            {
                                //说明当前没有可以被执行的任务
                                return BllResultFactory.Sucess();
                            }
                            if (task2 != null)
                            {
                                //说明库外取货任务不为空，则下发接入任务
                                //库外取分为换站和其他，换站需要再放到库外，其他则放到库内
                                return OutGet(srm, plc, availableOutStation, task2);
                            }
                            if (task1 != null)
                            {
                                //说明库内取货任务不为空
                                //库内取分为两种，一种移到库外，一种重新到到库内另一个库位上
                                return InGet(srm, plc, locationsRoadWay, availableOutStation, task1);
                            }

                            //当两种任务均存在时，此处设置两种模式，出库优先与均衡模式（按堆垛机位置进行）
                            var configResult = AppSession.BllService.GetAllConfig();
                            var config = configResult.Data?.FirstOrDefault(t => t.Code == ConfigStrings.OutFirst.ToString());
                            if (config == null || config.Value != "1")
                            {
                                //均衡模式
                                var dis1 = Math.Abs((int)task1.FromLocation.Line - CurrentColumn);
                                var dis2 = Math.Abs(task2.ArrivaEquipment.RowIndex1 - CurrentColumn);
                                if (dis1 <= dis2)
                                {
                                    return InGet(srm, plc, locationsRoadWay, availableOutStation, task1);
                                }
                                else
                                {
                                    return OutGet(srm, plc, availableOutStation, task2);

                                }

                            }
                            else
                            {
                                //出库优先模式
                                return OutGet(srm, plc, availableOutStation, task2);

                            }

                        }
                    }

                    else
                    {
                        //hack:其他情况1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址,暂时不做处理，这里也应不需要处理这些情况

                    }
                }
                //当堆垛机有任务完成且wcs没有任务完成时，说明是堆垛机新发任务完成
                else if (Fork1TaskExecuteStatus == SRMTaskExcuteStatus.任务完成.GetIndexString() && srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSTaskCompleteFlag.ToString()).Value != SSRMTaskCompleteFlag.任务完成.GetIndexString())
                {
                    //todo:检查任务完成
                    //单叉单任务堆垛机只有2种情况会有完成信号  库外出库和库内放货
                    //根据任务号和货叉类型进行任务完成处理
                    int taskNo = int.Parse(srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.Fork1TaskNo.ToString()).Value);
                    int forkType = int.Parse(srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.Fork1TaskType.ToString()).Value);

                    //库内放货完成
                    if (forkType == SRMForkTaskFlag.库内放货.GetIndexInt())
                    {
                        var task = tasks.FirstOrDefault(t => t.Id == taskNo);
                        if (task != null)
                        {
                            if (!locationsRoadWay.Exists(a => a.Code == task.ToLocationCode))
                            {
                                Logger.Log($"堆垛机{srm.Code}，任务：{task.Id}，去向库位：{task.ToLocationCode}不在本巷道中", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            var tempStatus = task.TaskStatus;
                            task.TaskStatus = TaskEntityStatus.任务完成.GetIndexInt();
                            task.Updated = DateTime.Now;
                            task.UpdatedBy = Accounts.WCS.ToString();
                            var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            if (tempResult.Success)
                            {
                                //标记交换区地址，任务完成10
                                var prop = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSTaskCompleteFlag.ToString());
                                prop.Value = SSRMTaskCompleteFlag.任务完成.GetIndexString();
                                var sendResult = plc.Write(prop);
                                if (sendResult.Success)
                                {
                                    Logger.Log($"堆垛机{srm.Name}完成库内放货成功，任务:{task.Id}", LogLevel.Success);
                                    return BllResultFactory.Sucess();
                                }
                                else
                                {
                                    //回滚
                                    task.TaskStatus = tempStatus;
                                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                    Logger.Log($"堆垛机{srm.Name}完成库内放货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                            }
                            else
                            {
                                Logger.Log($"完成堆垛机{srm.Name}库内放货失败，任务{task.Id}请求WMS接口失败：{tempResult.Msg}", LogLevel.Error);
                                return BllResultFactory.Error($"完成堆垛机{srm.Name}库内放货失败，任务{task.Id}请求WMS接口失败：{tempResult.Msg}");
                            }
                        }
                        else
                        {
                            Logger.Log($"堆垛机{srm.Code}记录任务{task.Id}状态不对，请核查任务是否为本巷道任务，以及任务的状态{Enum.GetName(typeof(TaskEntityStatus), task.TaskStatus)}", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                    }
                    //库外放货完成时
                    else if (forkType == SRMForkTaskFlag.库外放货.GetIndexInt())
                    {
                        var task = tasks.FirstOrDefault(t => t.Id == taskNo);
                        if (task != null)
                        {
                            //更新任务状态
                            int preStatus = task.TaskStatus;
                            task.TaskStatus = TaskEntityStatus.响应堆垛机库外放货完成.GetIndexInt();
                            task.Updated = DateTime.Now;
                            task.UpdatedBy = Accounts.WCS.ToString();
                            var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            if (tempResult.Success)
                            {
                                //标记交换区地址，任务完成10
                                var prop = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SSRMProps.WCSTaskCompleteFlag.ToString());
                                prop.Value = SSRMTaskCompleteFlag.任务完成.GetIndexString();
                                var sendResult = plc.Write(prop);
                                if (sendResult.Success)
                                {
                                    Logger.Log($"堆垛机{srm.Name}完成库外放货成功，任务:{task.Id}", LogLevel.Success);
                                    return BllResultFactory.Sucess();
                                }
                                else
                                {
                                    Logger.Log($"堆垛机{srm.Name}完成库外放货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                    task.TaskStatus = preStatus;
                                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                    return BllResultFactory.Error();
                                }
                            }
                            else
                            {
                                Logger.Log($"完成堆垛机{srm.Name}库外放货时，更新任务{task.Id}状态失败：{tempResult.Msg}", LogLevel.Error);
                                return BllResultFactory.Error($"完成堆垛机{srm.Name}库外放货时，更新任务{task.Id}状态失败：{tempResult.Msg}");
                            }
                        }
                        else
                        {
                            Logger.Log($"堆垛机{srm.Code}记录任务{task.Id}状态不对，请核查任务是否为本巷道任务，以及任务的状态{Enum.GetName(typeof(TaskEntityStatus), task.TaskStatus)}", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                    }
                    //未知情况
                    Logger.Log($"堆垛机{srm.Name}的任务完成时候和系统对应的任务出现了异常：PLC：{taskNo}或 货叉的任务类型:{forkType} ", LogLevel.Warning);
                    return BllResultFactory.Error($"堆垛机{srm.Name}的任务完成时候和系统对应的任务出现了异常：PLC：{taskNo}或 货叉的任务类型:{forkType} ");
                }
                else
                {
                    //Logger.Log($"堆垛机{stocker.Name}的任务完成时候和系统对应的任务出现了异常：PLC：{taskNo}或 货叉的任务类型:{forkType} ", LogLevel.Warning);
                    //未知情况
                    Logger.Log($"堆垛机{srm.Name}处理中，执行状态为：{srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "Fork1TaskExcuteStatus").Value}", LogLevel.Warning);
                    return BllResultFactory.Sucess();
                }
            }
            return BllResultFactory.Sucess();
        }

        private BllResult InGet(Equipment srm, IPLC plc, List<Location> locationsRoadWay, List<Equipment> availableOutStation, TaskEntity task)
        {
            if (task.TaskType == TaskType.移库.GetIndexInt())
            {
                if (!locationsRoadWay.Exists(a => a.Code == task.ToLocationCode))
                {
                    //hack:暂不支持巷道间移库；如何要做，则下发到库外；
                    Logger.Log($"任务：{task.Id}对应去向货位{task.ToLocationCode}不在本巷道中，请检查任务数据", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                else
                {
                    //库内取放
                    var tempStatus = task.TaskStatus;
                    task.TaskStatus = (int)TaskEntityStatus.下发堆垛机库内移库;
                    task.Updated = DateTime.Now;
                    task.UpdatedBy = Accounts.WCS.ToString();
                    var result = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    if (result.Success)
                    {
                        result = SendTaskToSRM(srm, plc, SSRMNewTaskFlag.新任务, SRMForkTaskFlag.库内取货, task.FromLocation.RowIndex1.ToString(), task.FromLocation.Line.ToString(), task.FromLocation.Layer.ToString(), "0", SRMForkTaskFlag.库内放货, task.ToLocation.RowIndex1.ToString(), task.ToLocation.Line.ToString(), task.ToLocation.Layer.ToString(), "0", task.Id.ToString(), SSRMTaskCompleteFlag.无完成);
                        if (result.Success)
                        {
                            Logger.Log($"下发堆垛机{srm.Code}移库任务:{task.Id}成功", LogLevel.Success);
                            return BllResultFactory.Sucess();
                        }
                        else
                        {
                            //回滚
                            task.TaskStatus = tempStatus;
                            AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            Logger.Log($"下发堆垛机{srm.Code}移库任务:{task.Id}失败，写入地址失败：{result.Msg}", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                    }
                    else
                    {
                        Logger.Log($"下发堆垛机{srm.Code}移库任务时，更新任务{task.Id}状态失败：{result.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
            }
            else
            {
                //下发到库外
                //获取目标站台
                var stationsResult = AppSession.ExcuteService.GetOutStationByPort(srm.DestinationArea, task.ToPort,App.WarehouseCode);
                if (!stationsResult.Success)
                {
                    Logger.Log($"堆垛机：{srm.Code},任务：{task.Id}未找到可出站台：" + stationsResult.Msg, LogLevel.Error);
                    return BllResultFactory.Error(stationsResult.Msg);
                }
                var station = stationsResult.Data.Where(t => availableOutStation.Exists(a => a.Code == t.Code)).OrderBy(t => CurrentColumn - t.ColumnIndex).FirstOrDefault();
                if (station == null)
                {
                    Logger.Log($"堆垛机{srm.Name}当前没有可用的站台可以放货,任务：{task.Id}", LogLevel.Warning);
                    return BllResultFactory.Error();
                }
                else
                {
                    var tempStatus = task.TaskStatus;
                    task.TaskStatus = (int)TaskEntityStatus.下发堆垛机出库任务;
                    task.Updated = DateTime.Now;
                    task.UpdatedBy = Accounts.WCS.ToString();
                    var result = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    if (result.Success)
                    {

                        result = SendTaskToSRM(srm, plc, SSRMNewTaskFlag.新任务, SRMForkTaskFlag.库内取货, task.FromLocation.RowIndex1.ToString(), task.FromLocation.Line.ToString(), task.FromLocation.Layer.ToString(), "0", SRMForkTaskFlag.库外放货, station.RowIndex1.ToString(), "0", "0", station.StationIndex.ToString(), task.Id.ToString(), SSRMTaskCompleteFlag.无完成);
                        if (result.Success)
                        {
                            Logger.Log($"下发堆垛机{srm.Code}出库任务:{task.Id}成功", LogLevel.Success);
                            return BllResultFactory.Sucess();
                        }
                        else
                        {
                            //回滚
                            task.TaskStatus = tempStatus;
                            AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            Logger.Log($"下发堆垛机{srm.Code}出库任务:{task.Id}失败，写入地址失败：{result.Msg}", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                    }
                    else
                    {
                        Logger.Log($"下发堆垛机{srm.Code}出库任务时，更新任务{task.Id}状态失败：{result.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }

                }
            }
        }

        /// <summary>
        /// 库外取
        /// </summary>
        /// <param name="srm"></param>
        /// <param name="plc"></param>
        /// <param name="availableOutStation"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        private BllResult OutGet(Equipment srm, IPLC plc, List<Equipment> availableOutStation, TaskEntity task)
        {
            if (task.TaskType == TaskType.换站.GetIndexInt())
            {
                //获取目标站台
                var stationsResult = AppSession.ExcuteService.GetOutStationByPort(srm.DestinationArea,task.ToPort,App.WarehouseCode);
                if (!stationsResult.Success)
                {
                    Logger.Log($"堆垛机{srm.Code},任务：{task.Id},未找到可出站台：" + stationsResult.Msg, LogLevel.Error);
                    return BllResultFactory.Error(stationsResult.Msg);
                }
                var station = stationsResult.Data.Where(t => availableOutStation.Exists(a => a.Code == t.Code)).OrderBy(t => CurrentColumn - t.ColumnIndex).FirstOrDefault();
                if (station == null)
                {
                    Logger.Log($"堆垛机{srm.Name}当前没有可用的站台可以放货,任务：{task.Id}", LogLevel.Warning);
                    return BllResultFactory.Error();
                }
                else
                {
                    var tempStatus = task.TaskStatus;
                    task.TaskStatus = (int)TaskEntityStatus.下发堆垛机换站任务;
                    task.Updated = DateTime.Now;
                    task.UpdatedBy = Accounts.WCS.ToString();
                    var result = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    if (result.Success)
                    {
                        result = SendTaskToSRM(srm, plc, SSRMNewTaskFlag.新任务, SRMForkTaskFlag.库外取货, task.ArrivaEquipment.RowIndex1.ToString(), "0", "0", task.ArrivaEquipment.StationIndex.ToString(), SRMForkTaskFlag.库外放货, station.RowIndex1.ToString(), "0", "0", station.StationIndex.ToString(), task.Id.ToString(), SSRMTaskCompleteFlag.无完成); ;
                        if (result.Success)
                        {
                            Logger.Log($"下发堆垛机{srm.Code}换站任务:{task.Id}成功", LogLevel.Success);
                            return BllResultFactory.Sucess();
                        }
                        else
                        {
                            //回滚
                            task.TaskStatus = tempStatus;
                            AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            Logger.Log($"下发堆垛机{srm.Code}换站任务:{task.Id}失败，写入地址失败：{result.Msg}", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                    }
                    else
                    {
                        Logger.Log($"下发堆垛机{srm.Code}换站任务时，更新任务{task.Id}状态失败：{result.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
            }
            else
            {
                var tempStatus = task.TaskStatus;
                task.TaskStatus = (int)TaskEntityStatus.下发堆垛机入库任务;
                task.Updated = DateTime.Now;
                task.UpdatedBy = Accounts.WCS.ToString();
                var result = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                if (result.Success)
                {

                    result = SendTaskToSRM(srm, plc, SSRMNewTaskFlag.新任务, SRMForkTaskFlag.库外取货, task.ArrivaEquipment.RowIndex1.ToString(), "0", "0", task.ArrivaEquipment.StationIndex.ToString(), SRMForkTaskFlag.库内放货, task.ToLocation.RowIndex1.ToString(), task.ToLocation.Line.ToString(), task.ToLocation.Layer.ToString(), "0", task.Id.ToString(), SSRMTaskCompleteFlag.无完成);
                    if (result.Success)
                    {
                        Logger.Log($"下发堆垛机{srm.Code}入库任务:{task.Id}成功", LogLevel.Success);
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        //回滚
                        task.TaskStatus = tempStatus;
                        AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        Logger.Log($"下发堆垛机{srm.Code}入库任务:{task.Id}失败，写入地址失败：{result.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                }
                else
                {
                    Logger.Log($"下发堆垛机{srm.Code}入库任务时，更新任务{task.Id}状态失败：{result.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
            }
        }



    }
}
