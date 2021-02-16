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
    /// 单叉堆垛机标准实现
    /// </summary>
    public class SingeForkSRMExcute : SRMExcute
    {
        /// <summary>
        /// 记录堆垛机当前所在的列
        /// </summary>
        public int CurrentColumn { get; private set; }

        public override BllResult Excute(List<Equipment> srms, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                foreach (var srm in srms)
                {

                    CurrentColumn = int.Parse(srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.CurrentColumn.ToString()).Value);

                    ExcuteSingle(srm, allEquipments, plc);
                    //心跳
                    Heartbeat(srm, plc);

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
        /// 以堆垛机状态驱动，重写堆垛机控制逻辑
        /// hack:1.注意此处改动：堆垛机任务完成不再以状态去查找任务，以堆垛机携带的任务号为准;
        /// 2.标准实现中不支持跨巷道移库
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

                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TotalError.ToString()).Value == "True")
                {
                    //如果报有故障，则返回
                    return BllResultFactory.Error("货叉1故障");
                }

                #endregion

                #region 堆垛机任务执行判断
                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务执行中.GetIndexString())
                {
                    //任务执行中就return
                    return BllResultFactory.Sucess();
                }
                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务中断_出错.GetIndexString())
                {
                    //由人工处理，一般为空出和重入
                    return BllResultFactory.Sucess();
                }
                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.下发任务错误.GetIndexString())
                {
                    //由人工处理，需要重新下发任务
                    return BllResultFactory.Sucess();
                }
                #endregion

                #region 任务,货位和本巷道的其他设备

                //找出所有未完成的任务
                var tasksResult = AppSession.Dal.GetCommonModelByConditionWithZero<TaskEntity>($"where taskStatus < {TaskEntityStatus.任务完成.GetIndexInt()} " +
                    $"and taskStatus>={TaskEntityStatus.下发任务.GetIndexInt()} and deleted = 0 and warehouseCode='{AppSession.WarehouseCode}'");
                if (!tasksResult.Success)
                {
                    //如果查找发生错误
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

                //获取当前堆垛机可以到达的列
                var minProp = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.ManageSmallColumn.ToString());
                var maxProp = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.ManageBigColumn.ToString());
                int minColumn = Convert.ToInt32(minProp.Value);
                int maxColumn = Convert.ToInt32(maxProp.Value);
                //可用的可出站台
                //hack:注意，此处针对每个巷道均需要配置StationStatusMonitor！如果没有配置，则可出站台为空集合；
                var stationOutStatusMonitor = equipmentsRoadWay.Find(a => a.EquipmentType.Code == "StationStatusMonitor");
                var availableOutStation = stationOutStatusMonitor == null ? new List<Equipment>() : equipmentsRoadWay.Where(t => t.EquipmentType.Code.Contains("Station")
                     && stationOutStatusMonitor.EquipmentProps.
                         Count(b => b.EquipmentTypeTemplateCode == t.StationIndex.ToString() && b.Value == StationTaskLimit.可出.GetIndexString()) > 0).ToList();
                //筛选可出站台，可出站台得在堆垛机的最大列和最小列之间且目标区域与堆垛机
                //hack:这里按需选择是否判断站台的区域与堆垛机的区域是否相同
                availableOutStation = availableOutStation.Where(t => t.ColumnIndex >= minColumn && t.ColumnIndex <= maxColumn).ToList();

                //筛选任务，任务的范围得在堆垛机的最大列和最小列之间
                var tasks = tasksResult.Data.Where(t =>
                {
                    if (t.TaskType != (int)TaskType.换站)
                    {
                        //hack:这里筛选本巷道或关联到本巷道的任务，规则为起始或目标库位，所以，当将来出现跨巷道移库时，需要特别处理跨巷道移库任务
                        //兼容转轨，这里需要对两个库位是否在当前堆垛机可以到达的列做个判断，如果超出，则不执行
                        var tempLocations = locationsRoadWay.Where(a => a.Code == t.FromLocationCode || a.Code == t.ToLocationCode).ToList();
                        if (tempLocations.Count > 0)
                        {
                            return tempLocations.Exists(a =>
                            {
                                return a.RoadWay == srm.RoadWay && (a.Line >= minColumn && a.Line <= maxColumn);
                            });
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //如果是换站，则toPort必须在本巷道中
                        var temp = AppSession.ExcuteService.GetOutStationByPort(srm.DestinationArea, t.ToPort, App.WarehouseCode);
                        if (temp.Success)
                        {
                            if (temp.Data.Exists(a => a.RoadWay != srm.RoadWay))
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                }).ToList();

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

                //货叉1待机情况下
                if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.待机.GetIndexString())
                {
                    //货叉任务待机时，可执行放和取任务，同时当执行完成时，交互后货叉1会从任务完成更新为待机
                    EquipmentProp fork1TaskFlag = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                    if (fork1TaskFlag.Value == SRMForkTaskFlag.任务完成.GetIndexString())
                    {
                        return ClearWCSData(srm, plc);
                    }
                    else if (fork1TaskFlag.Value == SRMForkTaskFlag.删除任务.GetIndexString())
                    {
                        return ClearWCSData(srm, plc);
                    }
                    //货叉无任务且货叉在中心
                    else if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString()).Value == SRMForkTaskFlag.无任务.GetIndexString() && srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1Center.ToString()).Value == "True")
                    {
                        #region 优先处理重新下发的任务,此处可按需去除有货无货的校验

                        //库内取,要求货叉1无货
                        var taskForResend = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.下发堆垛机库内取货.GetIndexInt() && locationsRoadWay.Count(a => a.Code == t.FromLocationCode) > 0 && t.SendAgain == 1);
                        if (taskForResend != null && srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1HasPallet.ToString()).Value == "False")
                        {
                            var locationForResend = locationsRoadWay.Find(t => t.Code == taskForResend.FromLocationCode);
                            return ReSendTask(srm, plc, locationForResend.SrmCode == srm.Code ? locationForResend.RowIndex1.ToString() : locationForResend.RowIndex2.ToString(), locationForResend.Line.ToString(), locationForResend.Layer.ToString(), "0", taskForResend, SRMForkTaskFlag.库内取货);
                        }

                        //库内放货时，任务标记已在当前堆垛机上，校验之,要求货叉有货
                        taskForResend = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.下发堆垛机库内放货.GetIndexInt() && locationsRoadWay.Count(a => a.Code == t.ToLocationCode) > 0 && t.SendAgain == 1 && t.Gateway == srm.Code);
                        if (taskForResend != null && srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1HasPallet.ToString()).Value == "True")
                        {
                            var locationForResend = locationsRoadWay.Find(t => t.Code == taskForResend.ToLocationCode);
                            return ReSendTask(srm, plc, locationForResend.SrmCode == srm.Code ? locationForResend.RowIndex1.ToString() : locationForResend.RowIndex2.ToString(), locationForResend.Line.ToString(), locationForResend.Layer.ToString(), "0", taskForResend, SRMForkTaskFlag.库内放货);
                        }

                        //库外取货时，此站台得在本巷道内，要求货叉无货
                        //hack:一般情况下不存在同个巷道多个重新下发的任务，如果存在，此处需要特别处理
                        taskForResend = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.下发堆垛机库外取货.GetIndexInt() &&
                                                                  equipmentsRoadWay.Count(a => a.Code == t.Gateway) > 0 &&
                                                                  t.SendAgain == 1);
                        if (taskForResend != null && srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1HasPallet.ToString()).Value == "False")
                        {
                            //使用task的ArrivaEquipmentCode来记录task对应托盘当前或上一次所在的口，则此处通过ArrivaEquipmentCode来查找当前task所在的口
                            var stationResult = equipmentsRoadWay.First(t => t.Code == taskForResend.Gateway);
                            return ReSendTask(srm, plc, stationResult.RowIndex1.ToString(), stationResult.RowIndex1.ToString(), "0", stationResult.StationIndex.ToString(), taskForResend, SRMForkTaskFlag.库外取货);

                        }
                        //库外放货，要求货叉有货，要求站台可出
                        taskForResend = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.下发堆垛机库外放货.GetIndexInt() && t.Gateway == srm.Code && t.SendAgain == 1);
                        if (taskForResend != null && srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1HasPallet.ToString()).Value == "True")
                        {
                            var stationsResult = AppSession.ExcuteService.GetOutStationByPort(srm.DestinationArea, taskForResend.ToPort, App.WarehouseCode);
                            if (!stationsResult.Success)
                            {
                                return BllResultFactory.Error($"重新下发任务出错：{stationsResult.Msg}");
                            }
                            //这些站台要在可用的站台列表中并选取离堆垛机最近的一个站台
                            var station = stationsResult.Data.Where(t => availableOutStation.Exists(a => a.Code == t.Code)).OrderBy(t => CurrentColumn - t.ColumnIndex).FirstOrDefault();
                            if (station == null)
                            {
                                Logger.Log($"堆垛机{srm.Name}当前没有可用的站台可以放货,任务：{taskForResend.Id}", LogLevel.Warning);
                                return BllResultFactory.Error();
                            }
                            else
                            {
                                return ReSendTask(srm, plc, station.RowIndex1.ToString(), "0", "0", station.StationIndex.ToString(), taskForResend, SRMForkTaskFlag.库外放货);
                            }
                        }

                        #endregion

                        //判断堆垛机货叉上是不是有货，有货就只能接受放货任务，无货就可以接受取货任务
                        if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1HasPallet.ToString()).Value == "True")
                        {
                            //有货，则查找同巷道的取货任务完成状态的任务，同一个堆垛机，单叉情况下最多一条
                            #region 判断是否超出一条
                            if (tasks.Count(t => (t.TaskStatus == TaskEntityStatus.响应堆垛机库内取货完成.GetIndexInt() || t.TaskStatus == TaskEntityStatus.响应堆垛机库外取货完成.GetIndexInt()) && t.Gateway == srm.Code) > 1)
                            {
                                Logger.Log($"堆垛机{srm.Name}显示货叉上有货，但是对应的任务超过1条，请检查状态为{TaskEntityStatus.响应堆垛机库内取货完成}" +
                                    $"和{TaskEntityStatus.响应堆垛机库外取货完成}的任务", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            #endregion

                            //添加任务号进行判断
                            int taskNo = int.Parse(srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskNo.ToString()).Value);
                            //先找库内取货完成的任务
                            var task = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.响应堆垛机库内取货完成.GetIndexInt() && t.Gateway == srm.Code);
                            if (task == null)
                            {
                                //如果没有就找库外取货完成的任务,此任务以ArrivaEquipmentCode去查找堆垛机任务
                                task = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.响应堆垛机库外取货完成.GetIndexInt() && t.Gateway == srm.Code);
                            }
                            if (task == null)
                            {
                                //如果还是没有找到任务，那就说明的确没有这条任务，或是人为原因导致的，但此时又显示货叉有货，所以这里是有问题的，做个日志
                                Logger.Log($"堆垛机{srm.Name}显示货叉上有货，但是没有对应的任务", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            else
                            {
                                if (task.Id != taskNo)
                                {
                                    Logger.Log($"堆垛机{srm.Name}显示货叉上有货，并且找到对应的任务：{task.Id},但是和堆垛机对应的任务号：{taskNo},出现偏差请核对", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                                //判断任务类型
                                //移库
                                if (task.TaskType == TaskType.移库.GetIndexInt())
                                {
                                    //是否同巷道
                                    var tempLocation = locationsRoadWay.FirstOrDefault(t => t.Code == task.ToLocationCode);
                                    if (tempLocation != null)
                                    {
                                        //同一个巷道，则直接下发库内放货
                                        return SendTaskToLocation(srm, plc, task, tempLocation, TaskEntityStatus.下发堆垛机库内放货, SRMForkTaskFlag.库内放货, task.TaskStatus);
                                    }
                                    else
                                    {
                                        //hack:默认不支持巷道间的移库，如需要则请按实际情况自行实现
                                        Logger.Log($"任务：{task.Id}对应去向货位{task.ToLocationCode}不在本巷道中，请检查任务数据", LogLevel.Error);
                                        return BllResultFactory.Error();
                                    }
                                }
                                else if (task.TaskType == TaskType.整盘出库.GetIndexInt() || task.TaskType == TaskType.空容器出库.GetIndexInt() || task.TaskType == TaskType.换站.GetIndexInt())
                                {
                                    //整出任务，空盘出库和换站，都是奔着port去的：
                                    var stationsResult = AppSession.ExcuteService.GetOutStationByPort(srm.DestinationArea, task.ToPort, App.WarehouseCode);
                                    if (!stationsResult.Success)
                                    {
                                        Logger.Log($"未找到可出站台：" + stationsResult.Msg, LogLevel.Error);
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
                                        return SendTaskToStation(srm, plc, task, TaskEntityStatus.下发堆垛机库外放货, SRMForkTaskFlag.库外放货, station, task.TaskStatus);
                                    }
                                }
                                else if (task.TaskType == TaskType.整盘入库.GetIndexInt() || task.TaskType == TaskType.空容器入库.GetIndexInt())
                                {
                                    //整盘入库、空盘入库：当前目的货位与堆垛机是否在同一个巷道，在，则下发库内放货任务；不在，报错；
                                    var tempLocation = locationsRoadWay.FirstOrDefault(t => t.Code == task.ToLocationCode);
                                    if (tempLocation != null)
                                    {
                                        //同一个巷道，则直接下发库内放货
                                        return SendTaskToLocation(srm, plc, task, tempLocation, TaskEntityStatus.下发堆垛机库内放货, SRMForkTaskFlag.库内放货, task.TaskStatus);
                                    }
                                    else
                                    {
                                        Logger.Log($"任务：{task.Id}对应去向货位{task.ToLocationCode}不在本巷道中，请检查任务数据", LogLevel.Error);
                                        return BllResultFactory.Error();
                                    }
                                }
                                else if (task.TaskType == TaskType.出库查看.GetIndexInt() || task.TaskType == TaskType.分拣出库.GetIndexInt()
                                            || task.TaskType == TaskType.盘点.GetIndexInt() || task.TaskType == TaskType.补充入库.GetIndexInt())
                                {
                                    //补充入库、分拣出库、盘点、出库查看：需要判断任务阶段来决定是出还是入
                                    //判断任务阶段
                                    if (task.Stage == TaskStageFlag.入.GetIndexInt())
                                    {
                                        //判断目标货位是否在当前巷道
                                        var tempLocation = locationsRoadWay.FirstOrDefault(t => t.Code == task.ToLocationCode);
                                        if (tempLocation != null)
                                        {
                                            //同一个巷道，则直接下发库内放货
                                            return SendTaskToLocation(srm, plc, task, tempLocation, TaskEntityStatus.下发堆垛机库内放货, SRMForkTaskFlag.库内放货, task.TaskStatus);
                                        }
                                        else
                                        {
                                            Logger.Log($"任务：{task.Id}对应去向货位{task.ToLocationCode}不在本巷道中，请检查任务数据", LogLevel.Error);
                                            return BllResultFactory.Error();
                                        }
                                    }
                                    else
                                    {
                                        //出
                                        var stationsResult = AppSession.ExcuteService.GetOutStationByPort(srm.DestinationArea, task.ToPort, App.WarehouseCode);
                                        if (!stationsResult.Success)
                                        {
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
                                            return SendTaskToStation(srm, plc, task, TaskEntityStatus.下发堆垛机库外放货, SRMForkTaskFlag.库外放货, station, task.TaskStatus);
                                        }
                                    }
                                }
                                else
                                {
                                    //报警，未知的任务类型
                                    Logger.Log($"堆垛机{srm.Name}对应的任务{task.Id}为未知的任务类型", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                            }
                        }
                        else
                        {
                            //无货,说明完全处在空闲状态
                            var tempTasks = tasks;
                            if (tempTasks.Count == 0)
                            {
                                return BllResultFactory.Error();
                            }
                            else
                            {
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
                                    t.FromLocation = locationsRoadWay.First(a => a.Code == t.FromLocationCode);
                                    t.ToPortEquipment = equipmentsRoadWay.FirstOrDefault(a => a.Code == t.ToPort);
                                });
                                //任务过滤条件，任务port对应的station要可用
                                task1s = task1s.Where(t => t.TaskType == (int)TaskType.移库 || (availableOutStation.Exists(a => AppSession.ExcuteService.GetOutStationByPort(srm.DestinationArea, t.ToPort, App.WarehouseCode).Data?.Exists(b => b.Code == a.Code) == true))).ToList();


                                //库内取货的任务
                                var task1 = task1s.OrderByDescending(t => t.Priority).ThenBy(t => Math.Abs((int)t.FromLocation.Line - CurrentColumn)).FirstOrDefault();

                                //本巷道接入口的任务
                                var task2s = tempTasks.Where(t => t.TaskStatus == TaskEntityStatus.响应接入站台到达.GetIndexInt() && equipmentsRoadWay.Exists(a => a.Code == t.Gateway)).ToList();
                                task2s.ForEach(t => t.ArrivaEquipment = equipmentsRoadWay.Find(a => a.Code == t.Gateway));
                                //按优先级以及站台与现有堆垛机的距离优先做调度，注意，接入的站台必须要在堆垛机可以到达的列之间
                                var task2 = task2s.Where(t => t.ArrivaEquipment.ColumnIndex >= minColumn && t.ArrivaEquipment.ColumnIndex <= maxColumn).OrderByDescending(t => t.Priority).ThenBy(t => Math.Abs(t.ArrivaEquipment.ColumnIndex - CurrentColumn)).FirstOrDefault();
                                if (task1 == null && task2 == null)
                                {
                                    //说明当前没有可以被执行的任务
                                    return BllResultFactory.Sucess();
                                }
                                if (task2 != null)
                                {
                                    //说明库外取货任务不为空，则下发接入任务
                                    return SendTaskToStation(srm, plc, task2, TaskEntityStatus.下发堆垛机库外取货, SRMForkTaskFlag.库外取货, equipmentsRoadWay.Find(t => t.Code == task2.Gateway), task2.TaskStatus);
                                }
                                if (task1 != null)
                                {
                                    //说明库内取货任务不为空
                                    return SendTaskToLocation(srm, plc, task1, task1.FromLocation, TaskEntityStatus.下发堆垛机库内取货, SRMForkTaskFlag.库内取货, task1.TaskStatus);
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
                                        return SendTaskToLocation(srm, plc, task1, task1.FromLocation, TaskEntityStatus.下发堆垛机库内取货, SRMForkTaskFlag.库内取货, task1.TaskStatus);
                                    }
                                    else
                                    {
                                        return SendTaskToStation(srm, plc, task2, TaskEntityStatus.下发堆垛机库外取货, SRMForkTaskFlag.库外取货, equipmentsRoadWay.First(t => t.Code == task2.Gateway), task2.TaskStatus);
                                    }

                                }
                                else
                                {
                                    //出库优先模式
                                    return SendTaskToLocation(srm, plc, task1, task1.FromLocation, TaskEntityStatus.下发堆垛机库内取货, SRMForkTaskFlag.库内取货, task1.TaskStatus);
                                }

                            }
                        }
                    }
                    else
                    {
                        //hack:其他情况1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址,暂时不做处理，这里也应不需要处理这些情况

                    }
                }
                else if (srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务完成.GetIndexString()
                    && srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString()).Value != SRMForkTaskFlag.任务完成.GetIndexString())
                {
                    //一共4种完成情况
                    //根据任务号和货叉类型进行任务完成处理
                    int taskNo = int.Parse(srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskNo.ToString()).Value);
                    int forkType = int.Parse(srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskType.ToString()).Value);
                    var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {taskNo}");
                    if (!taskResult.Success)
                    {
                        Logger.Log($"根据堆垛机任务号{taskNo}未找到任务：{taskResult.Msg}", LogLevel.Error);
                        return BllResultFactory.Error();
                    }
                    var task = taskResult.Data[0];
                    var tempStatus = task.TaskStatus;
                    var tempGateWay = task.Gateway;
                    //库内取货完成
                    if (forkType == SRMForkTaskFlag.库内取货.GetIndexInt())
                    {
                        //更新任务状态
                        task.TaskStatus = TaskEntityStatus.响应堆垛机库内取货完成.GetIndexInt();
                        task.Gateway = srm.Code; //标记当前堆垛机到任务
                        var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        if (tempResult.Success)
                        {
                            //标记交换区地址，任务完成10
                            var prop = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                            prop.Value = SRMForkTaskFlag.任务完成.GetIndexString();
                            var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                            if (sendResult.Success)
                            {
                                Logger.Log($"堆垛机{srm.Name}完成库内取货成功，任务:{task.Id}", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"堆垛机{srm.Name}完成库内取货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                task.TaskStatus = tempStatus;
                                task.Gateway = tempGateWay;
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                return BllResultFactory.Error();
                            }
                        }
                        else
                        {
                            Logger.Log($"完成堆垛机{srm.Name}库内取货时，更新任务{task.Id}状态失败：{tempResult.Msg}", LogLevel.Error);
                            return BllResultFactory.Error($"完成堆垛机{srm.Name}库内取货时，更新任务{task.Id}状态失败：{tempResult.Msg}");
                        }
                    }

                    //库内放货完成
                    else if (forkType == SRMForkTaskFlag.库内放货.GetIndexInt())
                    {
                        //本地完成任务，然后进行回传
                        var tempResult = AppSession.TaskService.CompleteTask(task.Id.Value, App.User.UserCode);
                        if (!tempResult.Success)
                        {
                            Logger.Log($"完成堆垛机{srm.Name}库内放货失败：{tempResult.Msg}", LogLevel.Error);
                            return BllResultFactory.Error($"完成堆垛机{srm.Name}库内放货失败：{tempResult.Msg}");
                        }
                        else
                        {
                            //标记交换区地址，任务完成10
                            var prop = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                            prop.Value = SRMForkTaskFlag.任务完成.GetIndexString();
                            var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                            if (sendResult.Success)
                            {
                                Logger.Log($"堆垛机{srm.Name}完成库内放货成功，任务:{task.Id}", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"堆垛机{srm.Name}完成库内放货失败，请人工处理任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                        }
                    }
                    //库外取货完成时
                    else if (forkType == SRMForkTaskFlag.库外取货.GetIndexInt())
                    {
                        //更新任务状态
                        task.TaskStatus = TaskEntityStatus.响应堆垛机库外取货完成.GetIndexInt();
                        task.Gateway = srm.Code; //标记当前堆垛机到任务
                        var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        if (tempResult.Success)
                        {
                            //标记交换区地址，任务完成10
                            var prop = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                            prop.Value = SRMForkTaskFlag.任务完成.GetIndexString();
                            var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                            if (sendResult.Success)
                            {
                                Logger.Log($"堆垛机{srm.Name}完成库外取货成功，任务:{task.Id}", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"堆垛机{srm.Name}完成库外取货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                task.TaskStatus = tempStatus;
                                task.Gateway = tempGateWay;
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                return BllResultFactory.Error();
                            }
                        }
                        else
                        {
                            Logger.Log($"完成堆垛机{srm.Name}库外取货时，更新任务{task.Id}状态失败：{tempResult.Msg}", LogLevel.Error);
                            return BllResultFactory.Error($"完成堆垛机{srm.Name}库外取货时，更新任务{task.Id}状态失败：{tempResult.Msg}");
                        }
                    }
                    //库外放货完成时
                    else if (forkType == SRMForkTaskFlag.库外放货.GetIndexInt())
                    {

                        //库外放货时，堆垛机会携带站台Index，找到放货站台并更新到任务；
                        var currentStation = srm.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == SRMProps.CurrentStation.ToString());
                        if (currentStation == null)
                        {
                            Logger.Log($"未找到堆垛机{srm.Name}对应的出入口属性", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                        var station = equipmentsRoadWay.FirstOrDefault(t => t.StationIndex?.ToString() == currentStation.Value && t.WarehouseCode == srm.WarehouseCode && t.DestinationArea == srm.DestinationArea);
                        if (station == null)
                        {
                            Logger.Log($"未找到堆垛机{srm.Name}对应的出入口{currentStation.Value}", LogLevel.Error);
                            return BllResultFactory.Error();
                        }

                        //更新任务状态
                        task.TaskStatus = TaskEntityStatus.响应堆垛机库外放货完成.GetIndexInt();
                        //注意此处对应放货的具体接出站台
                        task.Gateway = station.Code;
                        var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        if (tempResult.Success)
                        {
                            //标记交换区地址，任务完成10
                            var prop = srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                            prop.Value = SRMForkTaskFlag.任务完成.GetIndexString();
                            var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                            if (sendResult.Success)
                            {
                                Logger.Log($"堆垛机{srm.Name}完成库外放货成功，任务:{task.Id}", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"堆垛机{srm.Name}完成库外放货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                task.TaskStatus = tempStatus;
                                task.Gateway = tempGateWay;
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
                    return BllResultFactory.Sucess();
                }
                else
                {
                    //未知情况
                    Logger.Log($"堆垛机{srm.Name}执行中，执行状态为：{srm.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value}；可能由于WCS还未处理造成", LogLevel.Warning);
                    return BllResultFactory.Sucess();
                }
            }
            return BllResultFactory.Sucess();
        }

    }
}
