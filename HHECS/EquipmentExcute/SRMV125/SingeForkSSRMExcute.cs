using System;
using System.Collections.Generic;
using System.Linq;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.SRM;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;

namespace HHECS.EquipmentExcute.SRMV125
{
    /// <summary>
    /// 单叉高速堆垛机标准实现  
    /// 当个任务实现 一次下发二个任务：取货任务+放货任务
    /// </summary>
    public class SingeForkSSRMExcute : SRMExcute
    {
        /// <summary>
        /// 记录堆垛机当前所在的列
        /// </summary>
        public int CurrentColumn { get; private set; }

        

        public override BllResult Excute(List<Equipment> stockers,List<Equipment> equipments, IPLC plc)
        {
            try
            {
                foreach (var stocker in stockers)
                {
                    CurrentColumn = int.Parse(stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "CurrentColumn").Value);
                    
                    ExcuteSingle(stocker, plc);
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
        /// 以任务号为依据，对堆垛机进行重新编写控制逻辑
        /// 1. 
        /// </summary>
        /// <param name="stocker"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteSingle(Equipment stocker, IPLC plc)
        {
            //联机、无故障
            if (Validate(stocker).Success)
            {
                #region 第一步：任务执行判断，用于手动情况或异常的处理
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
                #region 任务和货位

                //找出所有下发未完成的任务
                var tasksResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where taskStatus < {TaskEntityStatus.任务完成.GetIndexInt()} " +
                    $"and taskStatus>={TaskEntityStatus.下发任务.GetIndexInt()} and deleted = 0 and warehouseCode={AppSession.WarehouseCode}");
                if (!tasksResult.Success)
                {
                    //如果没有找到任务就直接返回
                    return BllResultFactory.Error(tasksResult.Msg);
                }
                //找出同巷道的库位
                //找出同巷道的库位，考虑到可能多个巷道移库，这里分别查询出所有库位和当前堆垛机所在的库位
                var locationsResult = AppSession.LocationService.GetAllLocations(null, null, null, null, null, null, null, stocker.WarehouseCode);
                if (!locationsResult.Success)
                {
                    return BllResultFactory.Error(locationsResult.Msg);
                }
                var locations = locationsResult.Data;
                var locationsRoadWay = locationsResult.Data.Where(t => t.RoadWay == stocker.RoadWay).ToList();

                //hack:这里筛选本巷道的任务，规则为起始或目标库位在本巷道中，所以，当将来出现跨巷道移库时，需要特别处理跨巷道移库任务
                var tasks = tasksResult.Data.Where(t => locationsRoadWay.Count(a => a.Code == t.FromLocationCode || a.Code == t.ToLocationCode) > 0).ToList();
                #endregion
                string Fork1TaskExecuteStatus = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1TaskExcuteStatus.ToString()).Value;
                //堆垛机待机情况下
                if ( Fork1TaskExecuteStatus== SRMTaskExcuteStatus.待机.GetIndexString())
                {
                    //货叉任务待机时，可执行放和取任务，同时当执行完成时，交互后堆垛机会从任务完成更新为待机  
                    //TODO:WCSFork1TaskFlag  WCSTaskAccount 用于高速堆垛机
                    string WcsFork1TaskFlag = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.WCSFork1TaskFlag.ToString()).Value;
                    if (WcsFork1TaskFlag == SRMForkTaskFlag.删除任务.GetIndexString())
                    {
                        return ClearWCSData(stocker, plc, TaskAccount.New);
                    }
                    else if(WcsFork1TaskFlag==SRMForkTaskFlag.任务完成.GetIndexString())
                    {
                        return ClearWCSData(stocker, plc, TaskAccount.New);
                    }
                    string WcsTaskAccount = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode ==SRMProps.WCSTaskAccount.ToString() ).Value;
                    if (WcsTaskAccount == SRMForkTaskFlag.任务完成.GetIndexString())
                    {
                        return ClearWCSData(stocker, plc,TaskAccount.New);
                    }
                    else if (WcsTaskAccount == SRMForkTaskFlag.无任务.GetIndexString())
                    {
                        #region 优先处理重新下发的任务
                        //todo://任务重发逻辑待完成
                        #endregion
                        //获取需要下发给堆垛机的任务 
                        if (stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1ForkHasPallet.ToString()).Value == "True")
                        {
                            Logger.Log($"堆垛机{stocker.Name}显示货叉上有货，高速堆垛机不应该出现有托盘的时候还出现待机无任务的情况", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                        else
                        {
                            //判断任务限制情况
                            var tempTasks = tasks;
                            //hack：关于拣选的限制暂时没有做
                            string taskLimit = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == SRMProps.TaskLimit.ToString()).Value;
                            if (taskLimit == SRMTaskLimit.限制出库.GetIndexString())
                            {
                                //限制出库，则不能执行下发状态的出库性质任务
                                var temp = tasks.Where(t => t.TaskStatus == TaskEntityStatus.下发任务.GetIndexInt())
                                    .Where(t => t.TaskType != TaskType.整盘出库.GetIndexInt() && t.TaskType != TaskType.空容器出库.GetIndexInt()&&t.TaskType!=TaskType.换站.GetIndexInt() 
                                        && t.TaskType != TaskType.盘点.GetIndexInt() && t.TaskType != TaskType.出库查看.GetIndexInt() && t.TaskType != TaskType.分拣出库.GetIndexInt()).ToList();
                                tempTasks = tasks.Where(t => temp.Count(a => a.Id == t.Id) == 0).ToList();
                            }
                            else if (taskLimit == SRMTaskLimit.限制入库.GetIndexString())
                            {
                                //限制入库，则不能响应待入状态的入库性质任务
                                tempTasks = tasks.Where(t => t.TaskStatus != TaskEntityStatus.响应接入站台到达.GetIndexInt() ).ToList();
                            }
                            if (tempTasks.Count == 0)
                            {
                                return BllResultFactory.Error();
                            }
                            else
                            {
                                // 这里不需要判断高速堆垛机的位置[一个入口，一个出口，循环]  根据任务依次执行即可
                                TaskEntity task= tempTasks.Where(t=>t.TaskStatus==TaskEntityStatus.下发任务.GetIndexInt()
                                    ||t.TaskStatus==TaskEntityStatus.响应接入站台到达.GetIndexInt() ).FirstOrDefault();
                                if (task != null)
                                {
                                    //出库性质的任务
                                    if (task.TaskStatus == TaskEntityStatus.下发任务.GetIndexInt() &&
                                            (task.TaskType == TaskType.整盘出库.GetIndexInt() || task.TaskType == TaskType.空容器出库.GetIndexInt()
                                                || task.TaskType == TaskType.补充入库.GetIndexInt() || task.TaskType == TaskType.分拣出库.GetIndexInt()
                                                || task.TaskType == TaskType.盘点.GetIndexInt() || task.TaskType == TaskType.出库查看.GetIndexInt()
                                                || task.TaskType == TaskType.移库.GetIndexInt()|| task.TaskType==TaskType.移位.GetIndexInt()))
                                    {
                                        //TODO：判断该出库口是否有任务未完成

                                        //解析任务并下发
                                        if (task.TaskType != TaskType.移库.GetIndexInt()||task.TaskType!=TaskType.移位.GetIndexInt())  //不是移库、移位的任务，都需要解析出库站台
                                        {
                                            Location from = locations.FirstOrDefault(t=>t.Code==task.FromLocationCode);
                                            Equipment station = Stations.FirstOrDefault(t => t.Code == task.ToPort);
                                            if (from != null && station != null)
                                            {
                                                task.TaskStatus = TaskEntityStatus.下发堆垛机库内取货.GetIndexInt();
                                                
                                                var res = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                                if (res.Success)
                                                {
                                                    var result = SendTaskToStocker(stocker,plc,SuperSRMTaskMode.完整任务,
                                                        SRMForkTaskFlag.库内取货,from.RowIndex1.ToString(),from.Line.ToString(),from.Layer.ToString(), "0",
                                                        SRMForkTaskFlag.库外放货,station.RowIndex1.ToString(),station.ColumnIndex.ToString(),station.LayerIndex.ToString(), station.StationIndex.ToString(),
                                                        task.Id.ToString(), TaskAccount.New);
                                                    if (result.Success)
                                                    {
                                                        Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{SRMForkTaskFlag.库内取货}成功,库位：{from.Code},出库口:{task.ToPort}", LogLevel.Info);
                                                        return BllResultFactory.Sucess();
                                                    }
                                                    else
                                                    {
                                                        Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{SRMForkTaskFlag.库内取货}失败:{result.Msg};回滚任务{task.Id}状态。", LogLevel.Error);
                                                        task.TaskStatus = TaskEntityStatus.下发任务.GetIndexInt();
                                                        AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                                        return BllResultFactory.Error();
                                                    }
                                                }
                                                else
                                                {
                                                    Logger.Log($"下发堆垛机{stocker.Name}库内取货时，更新任务{task.Id}状态失败：{res.Msg}", LogLevel.Error);
                                                    return BllResultFactory.Error($"下发堆垛机{stocker.Name}库内取货时，更新任务{task.Id}状态失败：{res.Msg}");
                                                }
                                            }
                                            return BllResultFactory.Error($"任务:{task.Id}对应的来源库位:{task.FromLocationCode}和去向站台:{task.ToPort}出现的了问题");
                                        }
                                        else 
                                        {
                                            Location from = locations.Where(t => t.Code == task.FromLocationCode).FirstOrDefault();
                                            Location to = locations.Where(t => t.Code == task.ToLocationCode).FirstOrDefault();
                                            if (from != null && to != null)
                                            {
                                                task.TaskStatus = TaskEntityStatus.下发堆垛机库内取货.GetIndexInt();

                                                var res = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                                if (res.Success)
                                                {
                                                    BllResult result;
                                                    if (task.TaskType == TaskType.移库.GetIndexInt())
                                                    {
                                                        result = SendTaskToStocker(stocker, plc, SuperSRMTaskMode.完整任务,
                                                          SRMForkTaskFlag.库内取货, from.RowIndex1.ToString(), from.Line.ToString(), from.Layer.ToString(), "0",
                                                          SRMForkTaskFlag.库内放货, to.RowIndex1.ToString(), to.Line.ToString(), to.Layer.ToString(), "0",
                                                          task.Id.ToString(), TaskAccount.New);
                                                    }
                                                    else
                                                    {
                                                        result = SendTaskToStocker(stocker, plc, SuperSRMTaskMode.无,
                                                         SRMForkTaskFlag.无任务, from.RowIndex1.ToString(), from.Line.ToString(), from.Layer.ToString(), "0",
                                                         SRMForkTaskFlag.无任务, to.RowIndex1.ToString(), to.Line.ToString(), to.Layer.ToString(), "0",
                                                         task.Id.ToString(), TaskAccount.New);
                                                    }
                                                    if (result.Success)
                                                    {
                                                        Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{SRMForkTaskFlag.库内取货}成功,库位：{from.Code},出库口:{task.ToPort}", LogLevel.Info);
                                                        return BllResultFactory.Sucess();
                                                    }
                                                    else
                                                    {
                                                        Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{SRMForkTaskFlag.库内取货}失败:{result.Msg};回滚任务{task.Id}状态。", LogLevel.Error);
                                                        task.TaskStatus = TaskEntityStatus.下发任务.GetIndexInt();
                                                        AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                                        return BllResultFactory.Error();
                                                    }
                                                }
                                                else
                                                {
                                                    Logger.Log($"下发堆垛机{stocker.Name}库内取货时，更新任务{task.Id}状态失败：{res.Msg}", LogLevel.Error);
                                                    return BllResultFactory.Error($"下发堆垛机{stocker.Name}库内取货时，更新任务{task.Id}状态失败：{res.Msg}");
                                                }
                                            }
                                            return BllResultFactory.Error($"任务类型:{task.TaskType}对应的来源库位:{task.FromLocationCode}和目标库位:{task.ToLocationCode}");
                                        }
                                    }
                                    //入库性质的任务
                                    if (task.TaskStatus == TaskEntityStatus.响应堆垛机库外取货完成.GetIndexInt() &&
                                                (task.TaskType == TaskType.整盘入库.GetIndexInt() || task.TaskType == TaskType.空容器入库.GetIndexInt()
                                                    || task.TaskType == TaskType.补充入库.GetIndexInt() || task.TaskType == TaskType.分拣出库.GetIndexInt()
                                                    || task.TaskType == TaskType.盘点.GetIndexInt() || task.TaskType == TaskType.出库查看.GetIndexInt() ))
                                    {
                                         Location to = locations.Where(t => t.Code == task.ToLocationCode).FirstOrDefault();
                                         Equipment station = Stations.Where(t => t.Code == task.Gateway).FirstOrDefault();
                                        if ( station != null&& to!=null)
                                        {
                                            task.TaskStatus = TaskEntityStatus.下发堆垛机库外取货.GetIndexInt();

                                            var res = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                            if (res.Success)
                                            {
                                                var result = SendTaskToStocker(stocker, plc, SuperSRMTaskMode.完整任务,
                                                      SRMForkTaskFlag.库外取货, station.RowIndex1.ToString(), station.ColumnIndex.ToString(), station.LayerIndex.ToString(), station.StationIndex.ToString(),
                                                      SRMForkTaskFlag.库内放货, to.RowIndex1.ToString(), to.Line.ToString(), to.Layer.ToString(), "0",
                                                        task.Id.ToString(), TaskAccount.New);
                                                if (result.Success)
                                                {
                                                    Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{SRMForkTaskFlag.库内取货}成功,库位：{to.Code},入库站台:{task.Gateway}", LogLevel.Info);
                                                    return BllResultFactory.Sucess();
                                                }
                                                else
                                                {
                                                    Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{SRMForkTaskFlag.库内取货}失败:{result.Msg};回滚任务{task.Id}状态。", LogLevel.Error);
                                                    task.TaskStatus = TaskEntityStatus.响应接入站台到达.GetIndexInt();
                                                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                                    return BllResultFactory.Error();
                                                }
                                            }
                                            else
                                            {
                                                Logger.Log($"下发堆垛机{stocker.Name}库外取货时，更新任务{task.Id}状态失败：{res.Msg}", LogLevel.Error);
                                                return BllResultFactory.Error($"下发堆垛机{stocker.Name}库内取货时，更新任务{task.Id}状态失败：{res.Msg}");
                                            }
                                        }
                                      
                                        return BllResultFactory.Error($"任务:{task.Id}对应的库位:{task.FromLocationCode},{task.ToLocationCode}和接入站台:{task.Gateway},{task.ToPort}出现的了问题");
                                    }

                                    //入库性质的换站任务
                                    if (task.TaskStatus == TaskEntityStatus.响应堆垛机库外取货完成.GetIndexInt() && task.TaskType == TaskType.换站.GetIndexInt())
                                    {
                                        Equipment station = Stations.Where(t => t.Code == task.Gateway).FirstOrDefault();
                                        Equipment to = Stations.Where(t => t.Code == task.ToPort).FirstOrDefault();
                                        if (station != null && to != null)
                                        {
                                            task.TaskStatus = TaskEntityStatus.下发堆垛机库外取货.GetIndexInt();

                                            var res = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                            if (res.Success)
                                            {
                                                var result = SendTaskToStocker(stocker, plc, SuperSRMTaskMode.完整任务,
                                                      SRMForkTaskFlag.库外取货, station.RowIndex1.ToString(), station.ColumnIndex.ToString(), station.LayerIndex.ToString(), station.StationIndex.ToString(),
                                                      SRMForkTaskFlag.库外放货, to.RowIndex1.ToString(), to.ColumnIndex.ToString(), to.LayerIndex.ToString(), to.StationIndex.ToString(),
                                                        task.Id.ToString(), TaskAccount.New);
                                                if (result.Success)
                                                {
                                                    Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{SRMForkTaskFlag.库内取货}成功,库位：{to.Code},入库站台:{task.Gateway}", LogLevel.Info);
                                                    return BllResultFactory.Sucess();
                                                }
                                                else
                                                {
                                                    Logger.Log($"下发堆垛机{stocker.Name},任务：{task.Id},任务类型：{task.TaskType},货叉类型：{SRMForkTaskFlag.库内取货}失败:{result.Msg};回滚任务{task.Id}状态。", LogLevel.Error);
                                                    task.TaskStatus = TaskEntityStatus.响应接入站台到达.GetIndexInt();
                                                    AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                                                    return BllResultFactory.Error();
                                                }
                                            }
                                            else
                                            {
                                                Logger.Log($"下发堆垛机{stocker.Name}库外取货时，更新任务{task.Id}状态失败：{res.Msg}", LogLevel.Error);
                                                return BllResultFactory.Error($"下发堆垛机{stocker.Name}库内取货时，更新任务{task.Id}状态失败：{res.Msg}");
                                            }
                                        }

                                        return BllResultFactory.Error($"任务:{task.Id}对应的接入站台:{task.Gateway},{task.ToPort}出现的了问题");
                                    }
                                    {
                                        Logger.Log($"堆垛机{stocker.Name}待机空闲，出现有任务需要执行，但是任务状态{task.TaskStatus}和任务类型{task.TaskTypeDesc}不匹配", LogLevel.Error);
                                        return BllResultFactory.Error();
                                    }
                                }
                                else
                                {
                                    var tmpTsk = tempTasks.Where(t => t.TaskStatus > TaskEntityStatus.下发任务.GetIndexInt());
                                    if (tmpTsk != null)
                                    {
                                        Logger.Log($"堆垛机{stocker.Name}待机空闲，出现有任务需要执行，但是根据条件没刷选出任何任务", LogLevel.Error);
                                        return BllResultFactory.Error();
                                    }
                                    return BllResultFactory.Sucess();
                                }
                            }

                        }
                    }
                    
                    else
                    {
                        //hack:其他情况1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址,暂时不做处理，这里也应不需要处理这些情况

                    }
                }
                else if (Fork1TaskExecuteStatus == SRMTaskExcuteStatus.任务完成.GetIndexString())
                {
                    //todo:响应任务完成
                    //一共4种完成情况  高速堆垛机只有2中情况会有完成信号  库外出库和库内放货
                    //根据任务号和货叉类型进行任务完成处理
                    int taskNo =int.Parse( stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "Fork1TaskNo").Value);
                    int forkType= int.Parse(stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "Fork1TaskType").Value);
                   
                    //库内放货完成
                     if (forkType == SRMForkTaskFlag.库内放货.GetIndexInt())
                    {
                        var task = tasks.FirstOrDefault(t => t.Id==taskNo && t.TaskStatus<TaskEntityStatus.任务完成.GetIndexInt()&& t.TaskStatus>=TaskEntityStatus.下发堆垛机库内取货.GetIndexInt()
                        &&locations.Count(a => a.Code == t.ToLocationCode) > 0);
                        if (task != null )
                        {
                            //调用WMS任务完成接口  ---- TODO:huhai20190416先注解

                            //var tempResult = AppSession.Dal.CompleteTask(task.Id.ToString(), AppSession.Client, AppSession.Urls);
                            //if (!tempResult.Success)
                            //{
                            //    Logger.Log($"任务{task.Id}状态修改失败!!", LogLevel.Error);
                            //}
                            task.TaskStatus = TaskEntityStatus.任务完成.GetIndexInt();
                            var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            if (tempResult.Success)
                            {
                                //标记交换区地址，任务完成10
                                //var prop = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1TaskFlag");
                                //prop.Value = "10";
                                var prop = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "WCSTaskAccount");
                                prop.Value = TaskAccount.OK.GetIndexString();
                                //var sendResult = S7Helper.PlcSplitWrite(plc, new List<EquipmentProp> { prop }, 20);
                                var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                                if (sendResult.Success)
                                {
                                    Logger.Log($"堆垛机{stocker.Name}完成库内放货成功，任务:{task.Id}", LogLevel.Success);
                                    return BllResultFactory.Sucess();
                                }
                                else
                                {
                                    //hack:这里如果更新PLC地址失败，WMS端并不会回滚，此时应考虑如何人工处理；
                                    Logger.Log($"堆垛机{stocker.Name}完成库内放货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                            }
                            else
                            {
                                Logger.Log($"完成堆垛机{stocker.Name}库内放货失败，任务{task.Id}请求WMS接口失败：{tempResult.Msg}", LogLevel.Error);
                                return BllResultFactory.Error($"完成堆垛机{stocker.Name}库内放货失败，任务{task.Id}请求WMS接口失败：{tempResult.Msg}");
                            }
                        }
                    }                   
                    //库外放货完成时
                    else if (forkType == SRMForkTaskFlag.库外放货.GetIndexInt())
                    {
                        var task = tasks.FirstOrDefault(t => t.Id==taskNo && t.TaskStatus < TaskEntityStatus.下发堆垛机库外放货.GetIndexInt() && t.TaskStatus>TaskEntityStatus.下发任务.GetIndexInt()
                        && locations.Count(a => a.Code == t.FromLocationCode) > 0);
                        if (task != null)
                        {
                            //更新任务状态
                            int preStatus = task.TaskStatus;
                            task.TaskStatus = TaskEntityStatus.响应堆垛机库外放货完成.GetIndexInt();
                            var tempResult = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                            if (tempResult.Success)
                            {
                                //标记交换区地址，任务完成10
                                //var prop = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "WCSFork1TaskFlag");
                                //prop.Value = "10";
                                var prop = stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "WCSTaskAccount");
                                prop.Value = "10";
                                //var sendResult = S7Helper.PlcSplitWrite(plc, new List<EquipmentProp> { prop }, 20);
                                var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                                if (sendResult.Success)
                                {
                                    Logger.Log($"堆垛机{stocker.Name}完成库外放货成功，任务:{task.Id}", LogLevel.Success);
                                    return BllResultFactory.Sucess();
                                }
                                else
                                {
                                    Logger.Log($"堆垛机{stocker.Name}完成库外放货失败，任务:{task.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                    task.TaskStatus = preStatus;
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
                    }
                    //未知情况
                    Logger.Log($"堆垛机{stocker.Name}的任务完成时候和系统对应的任务出现了异常：PLC：{taskNo}或 货叉的任务类型:{forkType} ", LogLevel.Warning);
                    return BllResultFactory.Error($"堆垛机{stocker.Name}的任务完成时候和系统对应的任务出现了异常：PLC：{taskNo}或 货叉的任务类型:{forkType} ");
                }
                else
                {
                    //Logger.Log($"堆垛机{stocker.Name}的任务完成时候和系统对应的任务出现了异常：PLC：{taskNo}或 货叉的任务类型:{forkType} ", LogLevel.Warning);
                    //未知情况
                    Logger.Log($"堆垛机{stocker.Name}出现了未知的执行状态：{stocker.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "Fork1TaskExcuteStatus").Value}", LogLevel.Warning);
                    return BllResultFactory.Sucess();
                }
            }
            return BllResultFactory.Sucess();
        }

    }
}
