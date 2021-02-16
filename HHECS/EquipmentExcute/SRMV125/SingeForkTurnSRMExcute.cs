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

namespace HHECS.EquipmentExcute.SRMV125
{
    /// <summary>
    /// 单叉转轨堆垛机标准实现
    /// 在标准的堆垛机中加入 拓展模式和所辖的最小列和最大列
    /// 这个主要针对转轨堆垛机由于故障的原因，会被推放在一旁会占用部分库位，或转轨堆垛机本身的原因不能全部兼顾所有库位
    /// 转轨堆垛机约定： 这两台堆垛机涉及的库位为一个巷道， 库位的列根据行进行分割，举例说明：
    /// 一共有4排货架，每排货架62列，15层，一共2台堆垛机。  第一排和第2排货架之间的堆垛机为1号 SRM1  第三排和第四排之间为 SRM2
    /// 那么正常情况下 SRM1：管理的最小列为1，最大列62； SRM2： 管理的最小列：63，最大列124；
    /// 如果其中SRM1故障，那么SRM2开启兼顾模式，那么管理的最小列可能就是 3，最大列124；
    /// 如果其中是SRM2故障，那么可能就是最小列1，最大列118 
    /// 这里需要特别注意的就是换轨后，对应的货叉索引会由于换轨而变化。
    /// </summary>
    public class SingeForkTurnSRMExcute : SRMExcute
    {
        /// <summary>
        /// 记录堆垛机当前所在的列
        /// </summary>
        public int CurrentColumn { get; private set; }

        /// <summary>
        /// 堆垛机转轨 开启兼顾模式   0= 正常模式  , 1= 即一台堆垛机进行兼顾其他故障堆垛机的任务
        /// </summary>
        public int ExpandMode { get; private set; }

        /// <summary>
        /// 堆垛机转轨 所辖的最小列
        /// </summary>
        public int ManageSmallColumn { get; private set; }

        /// <summary>
        /// 堆垛机转轨 所辖的最大列
        /// </summary>
        public int ManageBigColumn { get; private set; }

        public override BllResult Excute(List<Equipment> stockers, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                foreach (var stocker in stockers)
                {
                    ParseTask(stocker.WarehouseCode, stocker.RoadWay);

                    CurrentColumn = int.Parse(stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.CurrentColumn.ToString()).Value);
                    ExpandMode=int.Parse(stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.ExpendMode.ToString()).Value);
                    ManageSmallColumn=int.Parse(stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.ManageSmallColumn.ToString()).Value);
                    ManageBigColumn=int.Parse(stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.ManageSmallColumn.ToString()).Value);

                    ExcuteSingle(stocker, allEquipments, plc);
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
        /// 以堆垛机状态驱动，重写堆垛机控制逻辑
        /// hack:1.注意此处改动：堆垛机任务完成不再以状态去查找任务，以堆垛机携带的任务号为准;2.标准实现中不支持跨巷道移库
        /// </summary>
        /// <param name="stocker"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteSingle(Equipment stocker, List<Equipment> allEquipments, IPLC plc)
        {
            //联机、无故障
            if (Validate(stocker).Success)
            {
                #region 堆垛机任务执行判断
                if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务执行中.GetIndexString())
                {
                    //任务执行中就return
                    return BllResultFactory.Sucess();
                }
                if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务中断_出错.GetIndexString())
                {
                    //由人工处理，一般为空出和重入
                    return BllResultFactory.Sucess();
                }
                if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.下发任务错误.GetIndexString())
                {
                    //由人工处理，需要重新下发任务
                    return BllResultFactory.Sucess();
                }
                #endregion

                #region 任务,货位和本巷道的其他设备

                //找出所有未完成的任务
                var tasksResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where taskStatus < {TaskEntityStatus.任务完成.GetIndexInt()} " +
                    $"and taskStatus>={TaskEntityStatus.下发任务.GetIndexInt()} and deleted = 0 and warehouseCode={AppSession.WarehouseCode}");
                if (!tasksResult.Success)
                {
                    //如果没有找到任务就直接返回
                    return BllResultFactory.Error(tasksResult.Msg);
                }
                //找出同巷道的库位，考虑到可能多个巷道移库，这里分别查询出所有库位和当前堆垛机所在的库位
                var locationsResult = AppSession.LocationService.GetAllLocationsByColumnSpanAndRoadway(ManageSmallColumn,ManageBigColumn,stocker.RoadWay,stocker.WarehouseCode);
                if (!locationsResult.Success)
                {
                    return BllResultFactory.Error(locationsResult.Msg);
                }
                var locationsRoadWay = locationsResult.Data;
               
                //找出本巷道的所有设备
                var tempEquipments = allEquipments.Where(t => t.RoadWay == stocker.RoadWay && t.WarehouseCode == stocker.WarehouseCode).ToList();
                //可用站台
                //var availableStation = AppSession.ExcuteService.GetAvailableStation(tempEquipments);
                //hack:注意，此处需要配置StationStatusMonitor！
                var availableStation = tempEquipments.Where(t => t.EquipmentType.Code.Contains("Station") 
                && tempEquipments.First(a => a.EquipmentType.Code == "StationStatusMonitor").EquipmentProps.
                    Count(b => b.EquipmentTypeTemplateCode == t.StationIndex.ToString() && b.Value == StationTaskLimit.可出.GetIndexString()) > 0).ToList();

                //hack:这里筛选本巷道或关联到本巷道的任务，规则为起始或目标库位，所以，当将来出现跨巷道移库时，需要特别处理跨巷道移库任务
                var tasks = tasksResult.Data.Where(t => locationsRoadWay.Count(a => a.Code == t.FromLocationCode || a.Code == t.ToLocationCode) > 0).ToList();

                #endregion

                //堆垛机待机情况下
                if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.待机.GetIndexString())
                {
                    //货叉任务待机时，可执行放和取任务，同时当执行完成时，交互后堆垛机会从任务完成更新为待机
                    if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString()).Value == SRMForkTaskFlag.任务完成.GetIndexString())
                    {
                        return ClearWCSData(stocker, plc);
                    }
                    else if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString()).Value == SRMForkTaskFlag.无任务.GetIndexString())
                    {
                        #region 优先处理重新下发的任务

                        var taskForResend = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.下发堆垛机库内取货.GetIndexInt() && locationsRoadWay.Count(a => a.Code == t.FromLocationCode) > 0 && t.SendAgain == 1);
                        if (taskForResend != null)
                        {
                            var locationForResend = locationsRoadWay.Find(t => t.Code == taskForResend.FromLocationCode);
                            // 根据任务的来源库位和堆垛机编号进行判断，如果相同则1，不同用2
                            var rowIndex = stocker.Code == locationForResend.SrmCode ? locationForResend.RowIndex1 : locationForResend.RowIndex2;
                            return ReSendTask(stocker, plc, rowIndex.ToString(), locationForResend.Line.ToString(), locationForResend.Layer.ToString(), "0", taskForResend, SRMForkTaskFlag.库内取货);
                        }

                        taskForResend = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.下发堆垛机库内放货.GetIndexInt() && locationsRoadWay.Count(a => a.Code == t.ToLocationCode) > 0 && t.SendAgain == 1);
                        if (taskForResend != null)
                        {
                            var locationForResend = locationsRoadWay.Find(t => t.Code == taskForResend.ToLocationCode);
                            // 根据任务的来源库位和堆垛机编号进行判断，如果相同则1，不同用2
                            var rowIndex = stocker.Code == locationForResend.SrmCode ? locationForResend.RowIndex1 : locationForResend.RowIndex2;
                            return ReSendTask(stocker, plc, rowIndex.ToString(), locationForResend.Line.ToString(), locationForResend.Layer.ToString(), "0", taskForResend, SRMForkTaskFlag.库内放货);
                        }

                        //取货时，此站台得在本巷道内
                        //hack:一般情况下不存在同个巷道多个重新下发的任务，如果存在，此处需要特别处理
                        taskForResend = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.下发堆垛机库外取货.GetIndexInt() &&
                                                                  tempEquipments.Count(a => a.Code == t.Gateway) > 0 &&
                                                                  t.SendAgain == 1);
                        if (taskForResend != null)
                        {
                            //使用task的ArrivaEquipmentCode来记录task对应托盘当前或上一次所在的口，则此处通过ArrivaEquipmentCode来查找当前task所在的口
                            var stationResult = tempEquipments.First(t => t.Code == taskForResend.Gateway);
                            return ReSendTask(stocker, plc, stationResult.RowIndex1.ToString(), stationResult.ColumnIndex.ToString(), stationResult.LayerIndex.ToString(), stationResult.StationIndex.ToString(), taskForResend, SRMForkTaskFlag.库外取货);
                        }

                        taskForResend = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.下发堆垛机库外放货.GetIndexInt() && t.Gateway == stocker.Code && t.SendAgain == 1);
                        if (taskForResend != null)
                        {
                            var stationsResult = AppSession.ExcuteService.GetOutStationByPort(stocker.DestinationArea,taskForResend.ToPort,App.WarehouseCode);
                            if (!stationsResult.Success)
                            {
                                return BllResultFactory.Error($"重新下发任务出错：{stationsResult.Msg}");
                            }
                            //这些站台要在可用的站台列表中并选取离堆垛机最近的一个站台
                            //var station = stationsResult.Data.Where(t => availableStation.Count(a => a.Code == t.Code) > 0).OrderBy(t => CurrentColumn - t.ColumnIndex).FirstOrDefault();
                            var station = availableStation.Find(t => t.Code == stationsResult.Data[0].Code);
                            if (station == null)
                            {
                                Logger.Log($"堆垛机{stocker.Name}当前没有可用的站台可以放货,任务：{taskForResend.Id}", LogLevel.Warning);
                                return BllResultFactory.Error();
                            }
                            else
                            {
                                return ReSendTask(stocker, plc, station.RowIndex1.ToString(), station.ColumnIndex.ToString(), station.LayerIndex.ToString(), station.StationIndex.ToString(), taskForResend, SRMForkTaskFlag.库外放货);
                            }
                        }

                        #endregion

                        //判断堆垛机货叉上是不是有货，有货就只能接受放货任务，无货就可以接受取货任务
                        if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1ForkHasPallet.ToString()).Value == "True")
                        {
                            //有货，则查找同巷道的取货任务完成状态的任务，同一个堆垛机，单叉情况下最多一条
                            #region 判断是否超出一条
                            if (tasks.Count(t => (t.TaskStatus == TaskEntityStatus.响应堆垛机库内取货完成.GetIndexInt() || t.TaskStatus == TaskEntityStatus.响应堆垛机库外取货完成.GetIndexInt()) && t.Gateway == stocker.Code) > 1)
                            {
                                Logger.Log($"堆垛机{stocker.Name}显示货叉上有货，但是对应的任务超过1条，请检查状态为{TaskEntityStatus.响应堆垛机库内取货完成}和{TaskEntityStatus.响应堆垛机库外取货完成}的任务", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            #endregion
                            //先找库内取货完成的任务
                            var task = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.响应堆垛机库内取货完成.GetIndexInt() && t.Gateway == stocker.Code);
                            if (task == null)
                            {
                                //如果没有就找库外取货完成的任务,此任务以ArrivaEquipmentCode去查找堆垛机任务
                                task = tasks.FirstOrDefault(t => t.TaskStatus == TaskEntityStatus.响应堆垛机库外取货完成.GetIndexInt() && t.Gateway == stocker.Code);
                            }
                            if (task == null)
                            {
                                //如果还是没有找到任务，那就说明的确没有这条任务，或是人为原因导致的，但此时又显示货叉有货，所以这里是有问题的，做个日志
                                Logger.Log($"堆垛机{stocker.Name}显示货叉上有货，但是没有对应的任务", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            else
                            {
                                //判断任务类型
                                //移库
                                if (task.TaskType == TaskType.移库.GetIndexInt())
                                {
                                    //是否同巷道
                                    var tempLocation = locationsRoadWay.FirstOrDefault(t => t.Code == task.ToLocationCode);
                                    if (tempLocation != null)
                                    {
                                        //同一个巷道，则直接下发库内放货
                                        return SendTaskToLocation(stocker, plc, task, tempLocation, TaskEntityStatus.下发堆垛机库内放货, SRMForkTaskFlag.库内放货, task.TaskStatus);
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
                                    var stationsResult = AppSession.ExcuteService.GetOutStationByPort(stocker.DestinationArea,taskForResend.ToPort,App.WarehouseCode);
                                    if (!stationsResult.Success)
                                    {
                                        Logger.Log(stationsResult.Msg, LogLevel.Error);
                                        return BllResultFactory.Error(stationsResult.Msg);
                                    }
                                    var station = stationsResult.Data.Where(t => availableStation.Count(a => a.Code == t.Code) > 0).OrderBy(t => CurrentColumn - t.ColumnIndex).FirstOrDefault();
                                    if (station == null)
                                    {
                                        Logger.Log($"堆垛机{stocker.Name}当前没有可用的站台可以放货,任务：{task.Id}", LogLevel.Warning);
                                        return BllResultFactory.Error();
                                    }
                                    else
                                    {
                                        return SendTaskToStation(stocker, plc, task, TaskEntityStatus.下发堆垛机库外放货, SRMForkTaskFlag.库外放货, station, task.TaskStatus);
                                    }
                                }
                                else if (task.TaskType == TaskType.整盘入库.GetIndexInt() || task.TaskType == TaskType.空容器入库.GetIndexInt())
                                {
                                    //整盘入库、空盘入库：当前目的货位与堆垛机是否在同一个巷道，在，则下发库内放货任务；不在，报错；
                                    var tempLocation = locationsRoadWay.FirstOrDefault(t => t.Code == task.ToLocationCode);
                                    if (tempLocation != null)
                                    {
                                        //同一个巷道，则直接下发库内放货
                                        return SendTaskToLocation(stocker, plc, task, tempLocation, TaskEntityStatus.下发堆垛机库内放货, SRMForkTaskFlag.库内放货, task.TaskStatus);
                                    }
                                    else
                                    {
                                        Logger.Log($"任务：{task.Id}对应去向货位{task.ToLocationCode}不在本巷道中，请检查任务数据", LogLevel.Error);
                                        return BllResultFactory.Error();
                                    }
                                }
                                else if (task.TaskType == TaskType.出库查看.GetIndexInt() || task.TaskType == TaskType.分拣出库.GetIndexInt() || task.TaskType == TaskType.盘点.GetIndexInt() || task.TaskType == TaskType.补充入库.GetIndexInt())
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
                                            return SendTaskToLocation(stocker, plc, task, tempLocation, TaskEntityStatus.下发堆垛机库内放货, SRMForkTaskFlag.库内放货, task.TaskStatus);
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
                                        var stationsResult = AppSession.ExcuteService.GetOutStationByPort(stocker.DestinationArea,taskForResend.ToPort,App.WarehouseCode);
                                        if (!stationsResult.Success)
                                        {
                                            return BllResultFactory.Error(stationsResult.Msg);
                                        }
                                        var station = stationsResult.Data.Where(t => availableStation.Count(a => a.Code == t.Code) > 0).OrderBy(t => CurrentColumn - t.ColumnIndex).FirstOrDefault();
                                        if (station == null)
                                        {
                                            Logger.Log($"堆垛机{stocker.Name}当前没有可用的站台可以放货,任务：{task.Id}", LogLevel.Warning);
                                            return BllResultFactory.Error();
                                        }
                                        else
                                        {
                                            return SendTaskToStation(stocker, plc, task, TaskEntityStatus.下发堆垛机库外放货, SRMForkTaskFlag.库外放货, station, task.TaskStatus);
                                        }
                                    }
                                }
                                else
                                {
                                    //报警，未知的任务类型
                                    Logger.Log($"堆垛机{stocker.Name}对应的任务{task.Id}为未知的任务类型", LogLevel.Error);
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
                                var task1s = tempTasks.Where(t => t.TaskStatus == TaskEntityStatus.下发任务.GetIndexInt() && (t.TaskType == TaskType.出库查看.GetIndexInt() || t.TaskType == TaskType.分拣出库.GetIndexInt() || t.TaskType == TaskType.整盘出库.GetIndexInt() || t.TaskType == TaskType.盘点.GetIndexInt() || t.TaskType == TaskType.空容器出库.GetIndexInt() || t.TaskType == TaskType.补充入库.GetIndexInt() || t.TaskType == TaskType.移库.GetIndexInt()) && locationsRoadWay.Exists(a => a.Code == t.FromLocationCode)).ToList();
                                task1s.ForEach(t =>
                                {
                                    t.FromLocation = locationsRoadWay.First(a => a.Code == t.FromLocationCode);
                                    t.ToPortEquipment = tempEquipments.FirstOrDefault(a => a.Code == t.ToPort);
                                });
                                //任务过滤条件，任务port对应的station要可用
                                task1s = task1s.Where(t => availableStation.Exists(a => AppSession.ExcuteService.GetOutStationByPort(stocker.DestinationArea,t.ToPort,App.WarehouseCode).Data?.Exists(b => b.Code == a.Code) == true)).ToList();

                                //库内取货的任务
                                var task1 = task1s.OrderByDescending(t => t.Priority).ThenBy(t => Math.Abs((int)t.FromLocation.Line - CurrentColumn)).FirstOrDefault();

                                //本巷道接入口的任务
                                var task2 = tempTasks.Where(t => t.TaskStatus == TaskEntityStatus.响应接入站台到达.GetIndexInt() && tempEquipments.Exists(a => a.Code == t.Gateway)).OrderByDescending(t => t.Priority).ThenBy(t => Math.Abs((int)t.FromLocation.Line - CurrentColumn)).FirstOrDefault();
                                if (task1 == null && task2 == null)
                                {
                                    //说明当前没有可以被执行的任务
                                    return BllResultFactory.Error();
                                }
                                if (task1 == null)
                                {
                                    //说明库外取货任务不为空，则下发接入任务
                                    return SendTaskToStation(stocker, plc, task2, TaskEntityStatus.下发堆垛机库外取货, SRMForkTaskFlag.库外取货, tempEquipments.First(t => t.Code == task2.Gateway), task2.TaskStatus);
                                }
                                if (task2 == null)
                                {
                                    //说明库内取货任务不为空
                                    return SendTaskToLocation(stocker, plc, task1, task1.FromLocation, TaskEntityStatus.下发堆垛机库内取货, SRMForkTaskFlag.库内取货, task1.TaskStatus);
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
                                        return SendTaskToLocation(stocker, plc, task1, task1.FromLocation, TaskEntityStatus.下发堆垛机库内取货, SRMForkTaskFlag.库内取货, task1.TaskStatus);
                                    }
                                    else
                                    {
                                        return SendTaskToStation(stocker, plc, task2, TaskEntityStatus.下发堆垛机库外取货, SRMForkTaskFlag.库外取货, tempEquipments.First(t => t.Code == task2.Gateway), task2.TaskStatus);
                                    }

                                }
                                else
                                {
                                    //出库优先模式
                                    return SendTaskToLocation(stocker, plc, task1, task1.FromLocation, TaskEntityStatus.下发堆垛机库内取货, SRMForkTaskFlag.库内取货, task1.TaskStatus);
                                }

                            }
                        }
                    }
                    else if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1TaskFlag").Value == SRMForkTaskFlag.删除任务.GetIndexString())
                    {
                        return ClearWCSData(stocker, plc);
                    }
                    else
                    {
                        //hack:其他情况1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址,暂时不做处理，这里也应不需要处理这些情况

                    }
                }
                else if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务完成.GetIndexString() && stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString()).Value != SRMForkTaskFlag.任务完成.GetIndexString())
                {
                    //一共4种完成情况
                    //根据任务号和货叉类型进行任务完成处理
                    int taskNo = int.Parse(stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskNo.ToString()).Value);
                    int forkType = int.Parse(stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskType.ToString()).Value);
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
                        task.Gateway = stocker.Code;
                        var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        if (tempResult.Success)
                        {
                            //标记交换区地址，任务完成10
                            var prop = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                            prop.Value = SRMForkTaskFlag.任务完成.GetIndexString();
                            var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                            if (sendResult.Success)
                            {
                                Logger.Log($"堆垛机{stocker.Name}完成库内取货成功，任务:{task.Id}", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"堆垛机{stocker.Name}完成库内取货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                task.TaskStatus = tempStatus;
                                task.Gateway = tempGateWay;
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                return BllResultFactory.Error();
                            }
                        }
                        else
                        {
                            Logger.Log($"完成堆垛机{stocker.Name}库内取货时，更新任务{task.Id}状态失败：{tempResult.Msg}", LogLevel.Error);
                            return BllResultFactory.Error($"完成堆垛机{stocker.Name}库内取货时，更新任务{task.Id}状态失败：{tempResult.Msg}");
                        }
                    }

                    //库内放货完成
                    else if (forkType == SRMForkTaskFlag.库内放货.GetIndexInt())
                    {
                        //本地完成任务，然后进行回传
                        var tempResult = AppSession.TaskService.CompleteTask(task.Id.Value, App.User.UserCode);
                        if (!tempResult.Success)
                        {
                            Logger.Log($"完成堆垛机{stocker.Name}库内放货失败：{tempResult.Msg}", LogLevel.Error);
                            return BllResultFactory.Error($"完成堆垛机{stocker.Name}库内放货失败：{tempResult.Msg}");
                        }
                        else
                        {
                            //标记交换区地址，任务完成10
                            var prop = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                            prop.Value = SRMForkTaskFlag.任务完成.GetIndexString();
                            var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                            if (sendResult.Success)
                            {
                                Logger.Log($"堆垛机{stocker.Name}完成库内放货成功，任务:{task.Id}", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"堆垛机{stocker.Name}完成库内放货失败，请人工处理任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                        }
                    }
                    //库外取货完成时
                    else if (forkType == SRMForkTaskFlag.库外取货.GetIndexInt())
                    {
                        //更新任务状态
                        task.TaskStatus = TaskEntityStatus.响应堆垛机库外取货完成.GetIndexInt();
                        task.Gateway = stocker.Code;
                        var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        if (tempResult.Success)
                        {
                            //标记交换区地址，任务完成10
                            var prop = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                            prop.Value = SRMForkTaskFlag.任务完成.GetIndexString();
                            var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                            if (sendResult.Success)
                            {
                                Logger.Log($"堆垛机{stocker.Name}完成库外取货成功，任务:{task.Id}", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"堆垛机{stocker.Name}完成库外取货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                task.TaskStatus = tempStatus;
                                task.Gateway = tempGateWay;
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                return BllResultFactory.Error();
                            }
                        }
                        else
                        {
                            Logger.Log($"完成堆垛机{stocker.Name}库外取货时，更新任务{task.Id}状态失败：{tempResult.Msg}", LogLevel.Error);
                            return BllResultFactory.Error($"完成堆垛机{stocker.Name}库外取货时，更新任务{task.Id}状态失败：{tempResult.Msg}");
                        }
                    }
                    //库外放货完成时
                    else if (forkType == SRMForkTaskFlag.库外放货.GetIndexInt())
                    {

                        //库外放货时，堆垛机会携带站台Index，找到放货站台并更新到任务；
                        var currentStation = stocker.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == SRMProps.CurrentStation.ToString());
                        if (currentStation == null)
                        {
                            Logger.Log($"未找到堆垛机{stocker.Name}对应的出入口属性", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                        var station = tempEquipments.FirstOrDefault(t => t.StationIndex?.ToString() == currentStation.Value);
                        if (station == null)
                        {
                            Logger.Log($"未找到堆垛机{stocker.Name}对应的出入口{currentStation.Value}", LogLevel.Error);
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
                            var prop = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString());
                            prop.Value = SRMForkTaskFlag.任务完成.GetIndexString();
                            var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                            if (sendResult.Success)
                            {
                                Logger.Log($"堆垛机{stocker.Name}完成库外放货成功，任务:{task.Id}", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                Logger.Log($"堆垛机{stocker.Name}完成库外放货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                task.TaskStatus = tempStatus;
                                task.Gateway = tempGateWay;
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                return BllResultFactory.Error();
                            }
                        }
                        else
                        {
                            Logger.Log($"完成堆垛机{stocker.Name}库外放货时，更新任务{task.Id}状态失败：{tempResult.Msg}", LogLevel.Error);
                            return BllResultFactory.Error($"完成堆垛机{stocker.Name}库外放货时，更新任务{task.Id}状态失败：{tempResult.Msg}");
                        }
                    }
                    return BllResultFactory.Sucess();
                }
                else
                {
                    //未知情况
                    Logger.Log($"堆垛机{stocker.Name}出现了未知的执行状态：{stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value}", LogLevel.Warning);
                    return BllResultFactory.Sucess();
                }
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 解析任务下发
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="roadway"></param>
        public void ParseTask(string warehouseCode,int roadway)
        {
            try
            {
                var tmpTaskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where warehouseCode={warehouseCode} and deleted=0 and roadway={roadway}" +
                    $" and taskStatus={TaskEntityStatus.任务创建.GetIndexInt()}");
                if (tmpTaskResult.Success&&tmpTaskResult.Data.Count>0)
                {
                    foreach (TaskEntity task in tmpTaskResult.Data)
                    {
                        if (task.PreTaskId == 0)
                        {
                            task.TaskStatus = TaskEntityStatus.下发任务.GetIndexInt();
                            AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        }
                        var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where Id={task.PreTaskId} and deleted=0");
                        if (taskResult.Success && taskResult.Data.Count == 1)
                        {
                            TaskEntity entity = taskResult.Data[0];
                            if (entity.TaskStatus > TaskEntityStatus.下发堆垛机库内取货.GetIndexInt())
                            {
                                task.TaskStatus = TaskEntityStatus.下发任务.GetIndexInt();
                                AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
