using Dapper;
using HHECS.Bll;
using HHECS.EquipmentExcute.Robot.Enums;
using HHECS.EquipmentExcute.StationV162;
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
using System.Text;

namespace HHECS.EquipmentExcute.Truss
{
    /// <summary>
    /// 标准桁车实现
    /// </summary>
    public class TrussNormalExcute : TrussExcute
    {
        /// <summary>
        /// 记录桁车当前距离
        /// </summary>
        public long CurrentDistance { get; private set; }

        /// <summary>
        /// 桁车 开启兼顾模式   0= 正常模式  , 1= 即一台堆垛机进行兼顾其他故障堆垛机的任务
        /// </summary>
        public bool CompatibleMode { get; private set; }

        /// <summary>
        /// 桁车 所辖的最小距离
        /// </summary>
        public long ManageSmallDistance { get; private set; }

        /// <summary>
        /// 桁车 所辖的最大距离
        /// </summary>
        public long ManageBigDistance { get; private set; }

        /// <summary>
        /// 桁架的安全距离是4000，不同的桁架对同一个点位有100的误差，所以改为4100
        /// </summary>
        public long SafetyDistance = 4100;

        //循环记录
        private static int excuteCount = 0;

        public override BllResult Excute(List<Equipment> trusses, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                if (trusses.Count == 0)
                {
                    Logger.Log($"没有【{this.EquipmentType.Name}】类型的设备，所以不执行处理程序。", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTraceResult = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where status >= {StepTraceStatus.设备开始生产.GetIndexInt()} and status < {StepTraceStatus.任务完成.GetIndexInt()}");
                if (!stepTraceResult.Success)
                {
                    Logger.Log($"查询【{this.EquipmentType.Name}】类型的设备的任务出错，原因：{stepTraceResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTraces = stepTraceResult.Data;

                #region 处理下料请求来生成任务
                foreach (var truss in trusses)
                {
                    InitTruss(truss);
                    //生成任务
                    CreateTask(truss, allEquipments, stepTraces, plc);
                    //心跳
                    Heartbeat(truss, plc);
                }
                #endregion

                #region 执行任务：1.按照正反轮流循环 2.找出组队和机加工优先 3.找出有货的。经过三次排序，后面的排序会在前面排序的基础上再排序一次，最终同时满足3个要求。
                IOrderedEnumerable<Equipment> trussesOrder = null;
                excuteCount++;
                // 避免一个桁架不停的执行任务，两边轮流执行
                if (excuteCount % 2 == 0)
                {
                    excuteCount = 0;
                    trussesOrder = trusses.OrderByDescending(t => t.Id);
                }
                else
                {
                    trussesOrder = trusses.OrderBy(t => t.Id);
                }

                var waitStepTraces = stepTraces.Where(x => x.Status >= StepTraceStatus.等待任务执行.GetIndexInt()).ToList();
                //找出有任务的组队和机加工位
                var priorityStations = trusses[0].StationList
                                                 .Where(t => t.StationType == "AssembleType" || t.StationType == "MachiningType")
                                                 .Where(t => waitStepTraces.Exists(x => x.StationId == t.Id || x.NextStationId == t.Id)).ToList();

                //如果存在有任务的组队和机加工位，那么对应的桁架优先执行
                if (priorityStations.Count() > 0)
                {
                    trussesOrder = trussesOrder.OrderByDescending(t => priorityStations.Exists(x => x.TransportNormal == t.Code));
                }

                //有货的优先放到前面处理
                var excuteTrusses = trussesOrder.OrderByDescending(t => t.EquipmentProps.Find(x => x.EquipmentTypeTemplateCode == TrussNormalProps.Fork1HasPallet.ToString()).Value == "True").ToList();
                foreach (var truss in excuteTrusses)
                {
                    InitTruss(truss);
                    //执行任务
                    ExcuteTask(truss, allEquipments, stepTraceResult.Data, plc);
                }
                #endregion

                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"桁车处理过程中出现异常：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 初始化桁架的参数
        /// </summary>
        /// <param name="truss"></param>
        private void InitTruss(Equipment truss)
        {
            this.CurrentDistance = Int64.Parse(truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.HorizontalDistance.ToString()).Value);
            this.CompatibleMode = bool.Parse(truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.CompatibleMode.ToString()).Value);
            this.ManageSmallDistance = Int64.Parse(truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.ManageSmallDistance.ToString()).Value);
            this.ManageBigDistance = Int64.Parse(truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.ManageBigDistance.ToString()).Value);

            if (truss.Code == "Truss-Car004" && this.CompatibleMode == false)
            {
                //桁架4能跑去补焊3的站台40090的最近距离，但是正常模式程序不能让桁架跑那么远，不然桁架3要去岛2焊接4上面避让
                this.ManageSmallDistance = 44000;
            }
        }

        /// <summary>
        /// 处理下料请求（每次生成一个任务，防止一个桁架任务把位置全部抢光），生成桁车任务
        /// </summary>
        /// <param name="truss"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult CreateTask(Equipment truss, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            var trussValidate = Validate(truss);
            if (trussValidate.Success)
            {
                //var Fork1TaskExcuteStatus = truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskExcuteStatus.ToString()).Value;
                //var WCSTaskFlag = truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskFlag.ToString()).Value;
                //var Fork1HasPallet = truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1HasPallet.ToString()).Value;
                //if (Fork1TaskExcuteStatus != SRMTaskExcuteStatus.待机.GetIndexString() || WCSTaskFlag != TrussTaskFlag.无任务.GetIndexString() || Fork1HasPallet == "True")
                //{
                //    return BllResultFactory.Sucess();
                //}
                foreach (var stepTrace in stepTraceList.Where(t => t.Status == StepTraceStatus.设备请求下料.GetIndexInt()).ToList())
                {
                    try
                    {
                        var currentStation = truss.StationList.FirstOrDefault(t => t.Id == stepTrace.StationId);
                        if (currentStation == null)
                        {
                            Logger.Log($"找不到Id为[{stepTrace.StationId}]的站台，不能处理工序跟踪记录Id为[{stepTrace.Id}]的下料请求！", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                        //设备对应的桁车
                        string trussCode = null;
                        if (CompatibleMode)
                        {
                            trussCode = currentStation.TransportCompatible;
                        }
                        else
                        {
                            trussCode = currentStation.TransportNormal;
                        }

                        if (trussCode == null)
                        {
                            var trussMode = CompatibleMode ? "兼容模式下" : "正常模式下";
                            Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，数据错误：{trussMode}，站台没有对应的桁架！", LogLevel.Error);
                            continue;
                        }

                        //只处理本桁车对应的设备
                        if (truss.Code == trussCode)
                        {
                            ////当前工序
                            Step step;
                            //下个工序
                            Step nextStep;
                            //所有的下个站台
                            List<StepStation> stepStations;
                            //下个站台对应的设备
                            Equipment nextEquipment = null;

                            using (IDbConnection connection = AppSession.Dal.GetConnection())
                            {
                                connection.Open();
                                // NextStepId为0，表示没有下到工序id，就需要去工序表查询下道工序
                                if (stepTrace.NextStepId == 0)
                                {
                                    step = connection.Get<Step>(stepTrace.StepId);
                                    nextStep = connection.QueryFirstOrDefault<Step>($"select top 1 * from step where productCode = '{stepTrace.ProductCode}' and sequence > {step.Sequence} order by sequence");
                                }
                                else
                                {
                                    nextStep = connection.Get<Step>(stepTrace.NextStepId);
                                }
                                if (nextStep == null)
                                {
                                    Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，数据错误：不存在产品[{stepTrace.ProductCode}]对应的下个工序！", LogLevel.Error);
                                    continue;
                                }
                                stepStations = connection.Query<StepStation>($"select * from step_station where stepId = '{nextStep.Id}'").ToList();
                            }

                            if (stepStations.Count < 1)
                            {
                                Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，数据错误：不存在工序[{nextStep.Id}][{nextStep.Code}]对应的站台！", LogLevel.Error);
                                continue;
                            }

                            //找到工序对应的所有站台
                            var stationList = truss.StationList.Where(t => stepStations.Exists(x => x.StationId == t.Id)).ToList();

                            // 筛选在桁车移动范围之内的设备
                            stationList = stationList.Where(t => t.Distance >= ManageSmallDistance && t.Distance <= ManageBigDistance).ToList();

                            // 如果是成品下线，和其他不一样，是判断下线的AGV库位是否有空位
                            if (nextStep.StationType == "FinishedType")
                            {
                                if (stationList.Exists(t => stepTraceList.Exists(x => x.NextStationId == t.Id)))
                                    continue;
                                var result = setStation(stationList, stepTrace, nextStep.Id.Value);
                                if (result.Success)
                                    return result;
                                else
                                    continue;
                            }

                            // 排除已经被预定的站台
                            stationList.RemoveAll(t => stepTraceList.Exists(x => x.NextStationId == t.Id));

                            if (stationList.Count() == 0) continue;

                            // 如果是机加工站台，同1个机加工的2个站台放一个产品类型
                            if (nextStep.StationType == "MachiningType")
                            {
                                //同1个机床的2个站台code只有最后一位不一样
                                var startsCode = stationList[0].Code.Substring(0, stationList[0].Code.Length - 2);
                                var stationIds = truss.StationList.Where(t => t.Code.StartsWith(startsCode)).Select(t => t.Id.Value).ToList();
                                //如果机加工站台的任务中，存在和当前不一样的工件型号，就不能去
                                if (stepTraceList.Exists(t => (stationIds.Contains(t.StationId.Value) || stationIds.Contains(t.NextStationId.Value)) && t.WcsProductType != stepTrace.WcsProductType))
                                {
                                    continue;
                                }
                            }

                            // 根据站台找到所有对应的设备
                            var tempEquipments = allEquipments.Where(t => stationList.Exists(x => x.Id == t.StationId)).ToList();

                            if (tempEquipments.Count() == 0) continue;

                            //如果是焊接设备，优先使用2个站台都为空的设备
                            if (nextStep.StationType == "AutoWeldType")
                            {
                                //找出准备就绪的目标设备
                                tempEquipments = tempEquipments.Where(t => t.EquipmentProps.Find(x => x.EquipmentTypeTemplateCode == RobotPcStation.Ready_OK.ToString()).Value == "True").ToList();

                                //焊接设备会翻转，如果ECS给了允许翻转信息，并且还没清除，那即使有请求信号也不能用。因为可能焊接设备收到了允许翻转信号，还没处理。
                                tempEquipments = tempEquipments.Where(t => t.EquipmentProps.Find(x => x.EquipmentTypeTemplateCode == PcRobotStation.WCS_Allow_Flip.ToString()).Value == "False").ToList();

                                //找出2个站台都为空的设备
                                var doubleEmptyEquipments = tempEquipments.Where(t => t.EquipmentProps.Find(x => x.EquipmentTypeTemplateCode == RobotPcStation.Double_Empty.ToString()).Value == "True").ToList();

                                //如果有2个站台为空的设备，就使用2个站台都为空的设备
                                if (doubleEmptyEquipments.Count() > 0)
                                {
                                    tempEquipments = doubleEmptyEquipments;
                                }
                            }

                            if (nextStep.StationType == "MWeldType")
                            {
                                //// 如果 是桁架3，就优先分配近的补焊站台，否则优先使用工件少的站台
                                //if (truss.Code == "Truss-Car003")
                                //{
                                //    tempEquipments = tempEquipments.OrderBy(t => Math.Abs(t.Station.Distance - currentStation.Distance)).ToList();
                                //}
                                //else
                                //{
                                tempEquipments = tempEquipments.OrderBy(t => t.EquipmentProps.Find(x => x.EquipmentTypeTemplateCode == StationProps.RequestGoodsCount.ToString()).Value).ToList();
                                //}
                            }
                            else
                            {
                                //将目标设备按照距离排序，近的优先
                                tempEquipments = tempEquipments.OrderBy(t => Math.Abs(t.Station.Distance - currentStation.Distance)).ToList();
                            }
                            foreach (var item in tempEquipments)
                            {
                                //如果站台请求上料，才可以用 
                                var Request_Load = item.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotPcStation.Request_Load.ToString());
                                if (Request_Load != null)
                                {
                                    if (Request_Load.Value == "True")
                                    {
                                        //增加任务判断
                                        var stepTraceResult = AppSession.Dal.GetCommonModelByConditionWithZero<StepTrace>($"where status >{StepTraceStatus.设备开始生产.GetIndexInt()} and status < {StepTraceStatus.任务完成.GetIndexInt()} and nextStationId = {item.StationId}");
                                        if (stepTraceResult.Data.Count < 1)
                                        {
                                            nextEquipment = item;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (nextEquipment != null && nextEquipment.StationId != 0)
                            {
                                stepTrace.NextStepId = nextStep.Id.Value;
                                stepTrace.NextStationId = nextEquipment.StationId;
                                stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
                                stepTrace.UpdateTime = DateTime.Now;
                                stepTrace.UpdateBy = App.User.UserCode;
                                AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                                return BllResultFactory.Sucess();
                            }
                            else if (stepTrace.NextStepId == 0)
                            {
                                stepTrace.NextStepId = nextStep.Id.Value;
                                stepTrace.UpdateTime = DateTime.Now;
                                stepTrace.UpdateBy = App.User.UserCode;
                                AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                                return BllResultFactory.Sucess();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
                        return BllResultFactory.Error();
                    }
                }
                return BllResultFactory.Sucess();
            }
            else
            {
                return trussValidate;
            }
        }

        /// <summary>
        /// 执行桁车任务
        /// </summary>
        /// <param name="truss"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public BllResult ExcuteTask(Equipment truss, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            var trussValidate = Validate(truss);
            if (trussValidate.Success)
            {
                #region 桁车任务执行判断
                if (truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务执行中.GetIndexString())
                {
                    //任务执行中就return
                    return BllResultFactory.Sucess();
                }
                if (truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务中断_出错.GetIndexString())
                {
                    //由人工处理，一般为空出和重入
                    return BllResultFactory.Sucess();
                }
                if (truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.下发任务错误.GetIndexString())
                {
                    //由人工处理，需要重新下发任务
                    return BllResultFactory.Sucess();
                }
                #endregion

                ////找到桁车可以到达的设备id
                //var stationIds = allEquipments.Where(t => t.RowIndex1 <= ManageBigDistance && t.RowIndex1 >= ManageSmallDistance && t.EquipmentTypeId != truss.EquipmentTypeId).Select(t => t.StationId).ToList();

                //桁车对应的设备工站ID
                IEnumerable<int> stationIds = null;
                //桁车普通模式对应TransportNormal，桁车兼容模式对应设备的TransportCompatible
                if (CompatibleMode)
                {
                    stationIds = allEquipments.Where(t => t.Station?.TransportCompatible == truss.Code).Select(t => t.StationId).ToList();
                }
                else
                {
                    stationIds = allEquipments.Where(t => t.Station?.TransportNormal == truss.Code).Select(t => t.StationId).ToList();
                }

                //匹配桁车可以执行的工序
                var stepTraces = stepTraceList.Where(t => t.Status >= StepTraceStatus.等待任务执行.GetIndexInt() && stationIds.Contains(t.StationId.Value)).ToList();

                //待机情况下
                if (truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.待机.GetIndexString())
                {
                    //货叉任务待机时，可执行放和取任务，同时当执行完成时，交互后货叉1会从任务完成更新为待机
                    EquipmentProp fork1TaskFlag = truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskFlag.ToString());
                    if (fork1TaskFlag.Value == TrussTaskFlag.任务完成.GetIndexString())
                    {
                        return ClearWCSData(truss, plc);
                    }
                    else if (fork1TaskFlag.Value == TrussTaskFlag.删除任务.GetIndexString())
                    {
                        return ClearWCSData(truss, plc);
                    }
                    //桁车无任务且货叉在高位
                    else if (truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskFlag.ToString()).Value == TrussTaskFlag.无任务.GetIndexString()
                          && truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1Center.ToString()).Value == "True")
                    {
                        #region 优先处理重新下发的任务,此处可按需去除有货无货的校验

                        //取货,要求货叉1无货
                        var taskForResend = stepTraces.FirstOrDefault(t => t.Status == StepTraceStatus.下发取货任务.GetIndexInt() && t.SendAgain == 1);
                        if (taskForResend != null && truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1HasPallet.ToString()).Value == "False")
                        {
                            //如果需要避让，先执行避让程序
                            if (Avoid(truss, allEquipments, plc).Success == false)
                            {
                                return BllResultFactory.Error();
                            }
                            if (Check(truss, allEquipments, taskForResend, plc).Success)
                            {
                                return ReSendTask(truss, plc, taskForResend, TrussTaskFlag.机械手取货, taskForResend.StationId.ToString());
                            }
                        }

                        //下发放货任务 放货时，任务标记已在当前桁车上，要求货叉有货
                        taskForResend = stepTraces.FirstOrDefault(t => t.Status == StepTraceStatus.下发放货任务.GetIndexInt() && t.SendAgain == 1 && t.SrmCode == truss.Code);
                        if (taskForResend != null && truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1HasPallet.ToString()).Value == "True")
                        {
                            if (Check(truss, allEquipments, taskForResend, plc).Success)
                            {
                                return ReSendTask(truss, plc, taskForResend, TrussTaskFlag.机械手放货, taskForResend.NextStationId.ToString());
                            }
                        }

                        #endregion

                        //判断桁车货叉上是不是有货，有货就只能接受放货任务，无货就可以接受取货任务
                        if (truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1HasPallet.ToString()).Value == "True")
                        {
                            //有货，则查找同巷道的取货任务完成状态的任务，同一个桁车，单叉情况下最多一条
                            #region 判断是否超出一条
                            if (stepTraces.Count(t => t.Status == StepTraceStatus.响应取货完成.GetIndexInt() && t.SrmCode == truss.Code) > 1)
                            {
                                Logger.Log($"桁车{truss.Name}显示货叉上有货，但是对应的任务超过1条，请检查状态为{StepTraceStatus.响应取货完成}的任务", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            #endregion

                            //添加任务号进行判断
                            int stepTraceId = int.Parse(truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskNo.ToString()).Value);
                            //先找响应取货完成的任务
                            var stepTrace = stepTraces.FirstOrDefault(t => t.Status == StepTraceStatus.响应取货完成.GetIndexInt() && t.SrmCode == truss.Code);
                            if (stepTrace == null)
                            {
                                //如果还是没有找到任务，那就说明的确没有这条任务，或是人为原因导致的，但此时又显示货叉有货，所以这里是有问题的，做个日志
                                Logger.Log($"桁车{truss.Name}显示货叉上有货，但是没有对应的任务", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            else
                            {
                                if (stepTrace.Id != stepTraceId)
                                {
                                    Logger.Log($"桁车{truss.Name}显示货叉上有货，并且找到对应的任务：{stepTrace.Id},但是和桁车对应的任务号：{stepTraceId},出现偏差请核对", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                                if (Check(truss, allEquipments, stepTrace, plc).Success)
                                {
                                    //下发放货任务
                                    return SendTaskToStation(truss, plc, stepTrace, StepTraceStatus.下发放货任务, TrussTaskFlag.机械手放货, stepTrace.NextStationId.ToString());
                                }
                            }
                        }
                        else
                        {
                            var excutingStepTrace = stepTraces.FirstOrDefault(t => t.Status == StepTraceStatus.下发取货任务.GetIndexInt() || t.Status == StepTraceStatus.响应取货完成.GetIndexInt() || t.Status == StepTraceStatus.下发放货任务.GetIndexInt());
                            if (excutingStepTrace != null)
                            {
                                Logger.Log($"桁车{truss.Name}有ID为{excutingStepTrace.Id}的桁架任务还在执行中，不能自动下发新任务！", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                            //如果需要避让，先执行避让程序
                            if (Avoid(truss, allEquipments, plc).Success == false)
                            {
                                return BllResultFactory.Error();
                            }
                            //组焊机器人的任务优先级第一
                            var aeesmblyStationIds = allEquipments.Where(t => t.EquipmentType.Code == "RobotForAeesmbly").Select(t => t.StationId);
                            var stepTrace = stepTraces.FirstOrDefault(t => t.Status == StepTraceStatus.等待任务执行.GetIndexInt() && aeesmblyStationIds.Contains(t.StationId.Value));
                            if (stepTrace == null)
                            {
                                //机床的任务优先级第二
                                var machiningStationIds = allEquipments.Where(t => t.EquipmentType.Code == "MachiningType").Select(t => t.StationId);
                                stepTrace = stepTraces.FirstOrDefault(t => t.Status == StepTraceStatus.等待任务执行.GetIndexInt() && (machiningStationIds.Contains(t.StationId.Value) || machiningStationIds.Contains(t.NextStationId.Value)));
                                if (stepTrace == null)
                                {
                                    stepTrace = stepTraces.FirstOrDefault(t => t.Status == StepTraceStatus.等待任务执行.GetIndexInt());
                                }
                            }
                            if (stepTrace != null)
                            {
                                if (Check(truss, allEquipments, stepTrace, plc).Success)
                                {
                                    //下发取货任务
                                    return SendTaskToStation(truss, plc, stepTrace, StepTraceStatus.下发取货任务, TrussTaskFlag.机械手取货, stepTrace.StationId.ToString());
                                }
                            }
                            //如果空闲无任务就去避让点
                            var CurrentTongs = truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.CurrentTongs.ToString()).Value;
                            if (CurrentTongs != truss.Station.TrussTakeStationId.ToString())
                            {
                                return ToAvoidStation(truss, allEquipments, plc);
                            }
                            //如果空闲无任务，且无货，但是路径还没删除，就删除掉
                            if (truss.RowIndex1 != 0 || truss.RowIndex2 != 0)
                            {
                                truss.RowIndex1 = 0;
                                truss.RowIndex2 = 0;
                                return AppSession.Dal.UpdateCommonModel<Equipment>(truss);
                            }
                        }
                    }
                }
                else if (truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskExcuteStatus.ToString()).Value == SRMTaskExcuteStatus.任务完成.GetIndexString()
                    && truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskFlag.ToString()).Value != TrussTaskFlag.任务完成.GetIndexString())
                {
                    //一共3种完成情况
                    //根据任务号和任务类型进行任务完成处理
                    int stepTraceId = int.Parse(truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskNo.ToString()).Value);
                    int forkType = int.Parse(truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskType.ToString()).Value);

                    var stepTrace = stepTraces.FirstOrDefault(t => t.Id == stepTraceId);
                    if (stepTrace == null || forkType == TrussTaskFlag.机械手行走.GetIndexInt())
                    {
                        var rowIndex1 = truss.RowIndex1;
                        var rowIndex2 = truss.RowIndex2;
                        truss.RowIndex1 = 0;
                        truss.RowIndex2 = 0;
                        using (IDbConnection connection = AppSession.Dal.GetConnection())
                        {
                            IDbTransaction tran = null;
                            try
                            {
                                connection.Open();
                                tran = connection.BeginTransaction();
                                connection.Update<Equipment>(truss, transaction: tran);
                                var prop = truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskFlag.ToString());
                                prop.Value = TrussTaskFlag.任务完成.GetIndexString();
                                //var sendResult = plc.Writes(new List<EquipmentProp> { prop });

                                //TODO: 效率高
                                var sendResult = plc.Write(prop);
                                if (sendResult.Success)
                                {
                                    tran.Commit();
                                    if (forkType == TrussTaskFlag.机械手行走.GetIndexInt())
                                    {
                                        Logger.Log($"桁车{truss.Name}完成机械手行走成功，任务:{stepTraceId}", LogLevel.Success);
                                    }
                                    else
                                    {
                                        Logger.Log($"根据桁车任务号{stepTraceId}未找到【机械手取货、机械手放货】任务，直接完成！", LogLevel.Warning);
                                    }
                                    return BllResultFactory.Sucess();
                                }
                                else
                                {
                                    tran?.Rollback();
                                    truss.RowIndex1 = rowIndex1;
                                    truss.RowIndex2 = rowIndex2;
                                    Logger.Log($"桁车{truss.Name}完成机械手行走写入PLC失败，原因：{sendResult.Msg}，任务:{stepTraceId}", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                            }
                            catch (Exception ex)
                            {
                                tran?.Rollback();
                                truss.RowIndex1 = rowIndex1;
                                truss.RowIndex2 = rowIndex2;
                                Logger.Log($"桁车{truss.Name}完成机械手行走失败，原因：{ex.Message}，任务:{stepTraceId}", LogLevel.Exception, ex);
                                return BllResultFactory.Error();
                            }
                        }
                    }
                    if (forkType == TrussTaskFlag.机械手取货.GetIndexInt())
                    {
                        //更新任务状态
                        stepTrace.Status = StepTraceStatus.响应取货完成.GetIndexInt();
                        //标记当前桁车到任务
                        stepTrace.SrmCode = truss.Code;
                        stepTrace.CreateBy = App.User.UserCode;
                        stepTrace.CreateTime = DateTime.Now;

                        using (IDbConnection connection = AppSession.Dal.GetConnection())
                        {
                            IDbTransaction tran = null;
                            try
                            {
                                connection.Open();
                                tran = connection.BeginTransaction();

                                connection.Update<StepTrace>(stepTrace, transaction: tran);

                                //标记交换区地址，任务完成10
                                var prop = truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskFlag.ToString());
                                prop.Value = TrussTaskFlag.任务完成.GetIndexString();

                                //var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                                //TODO: 效率高
                                var sendResult = plc.Write(prop);
                                if (sendResult.Success)
                                {
                                    tran.Commit();
                                    Logger.Log($"桁车{truss.Name}响应取货完成成功，任务:{stepTrace.Id}", LogLevel.Success);
                                    return BllResultFactory.Sucess();
                                }
                                else
                                {
                                    tran?.Rollback();
                                    Logger.Log($"桁车{truss.Name}响应取货完成失败，任务:{stepTrace.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                            }
                            catch (Exception ex)
                            {
                                tran?.Rollback();
                                Logger.Log($"桁车{truss.Name}响应取货完成{stepTrace.Id}时失败，原因：{ex.Message}", LogLevel.Exception, ex);
                                return BllResultFactory.Error($"桁车{truss.Name}响应取货完成{stepTrace.Id}时失败，原因：{ex.Message}");
                            }
                        }
                    }
                    else if (forkType == TrussTaskFlag.机械手放货.GetIndexInt())
                    {
                        //向工序跟踪日志表插入一条记录
                        StepTraceLog stepTraceLog = new StepTraceLog();
                        stepTraceLog.StepTraceId = stepTrace.Id;
                        stepTraceLog.WONumber = stepTrace.WONumber;
                        stepTraceLog.ProductId = stepTrace.ProductId;
                        stepTraceLog.ProductCode = stepTrace.ProductCode;
                        stepTraceLog.SerialNumber = stepTrace.SerialNumber;
                        stepTraceLog.LineId = stepTrace.LineId;
                        stepTraceLog.LastStationId = stepTrace.StationId;
                        stepTraceLog.StationId = stepTrace.NextStationId;
                        stepTraceLog.SrmCode = truss.Code;
                        stepTraceLog.PassOrFail = "";
                        stepTraceLog.IsNG = stepTrace.IsNG;
                        stepTraceLog.NGcode = stepTrace.NGcode;
                        stepTraceLog.StationInTime = DateTime.Now;
                        stepTraceLog.LineInTime = stepTrace.LineInTime;
                        stepTraceLog.CreateBy = App.User.UserCode;
                        stepTraceLog.CreateTime = DateTime.Now;

                        var nextStation = truss.StationList.Find(t => t.Id == stepTrace.NextStationId);
                        if (nextStation == null)
                        {
                            Logger.Log($"桁车{truss.Name} 响应放货完成 失败，任务:{stepTrace.Id}，原因：从站台基础数据中找不到id为[{stepTrace.NextStationId}]的站台", LogLevel.Error);
                            return BllResultFactory.Error();
                        }
                        //更新任务状态，如果是下线站台，就整个工件任务就完成了
                        if (nextStation.Code.Contains("StationForFinished"))
                        {
                            stepTrace.Status = StepTraceStatus.任务完成.GetIndexInt();
                            stepTrace.LineOutTime = DateTime.Now;
                        }
                        else
                        {
                            stepTrace.Status = StepTraceStatus.响应放货完成.GetIndexInt();
                        }
                        //放货完成就清除任务对应的桁车编码
                        stepTrace.SrmCode = "";
                        stepTrace.StationId = stepTrace.NextStationId;
                        stepTrace.StepId = stepTrace.NextStepId;
                        stepTrace.NextStationId = 0;
                        stepTrace.NextStepId = 0;
                        stepTrace.StationInTime = DateTime.Now;
                        stepTrace.UpdateBy = App.User.UserCode;
                        stepTrace.UpdateTime = DateTime.Now;

                        //修改设备属性表，删除桁架的路径
                        var rowIndex1 = truss.RowIndex1;
                        var rowIndex2 = truss.RowIndex2;
                        truss.RowIndex1 = 0;
                        truss.RowIndex2 = 0;

                        using (IDbConnection connection = AppSession.Dal.GetConnection())
                        {
                            IDbTransaction tran = null;
                            try
                            {
                                connection.Open();
                                tran = connection.BeginTransaction();

                                connection.Update<StepTrace>(stepTrace, transaction: tran);
                                connection.Insert<StepTraceLog>(stepTraceLog, transaction: tran);
                                connection.Update<Equipment>(truss, transaction: tran);

                                //标记交换区地址，任务完成10
                                var prop = truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskFlag.ToString());
                                prop.Value = TrussTaskFlag.任务完成.GetIndexString();
                                //var sendResult = plc.Writes(new List<EquipmentProp> { prop });
                                //TODO: 效率
                                var sendResult = plc.Write(prop);
                                if (sendResult.Success)
                                {
                                    tran.Commit();
                                    Logger.Log($"桁车{truss.Name} 响应放货完成 成功，任务:{stepTrace.Id}", LogLevel.Success);
                                    return BllResultFactory.Sucess();
                                }
                                else
                                {
                                    tran?.Rollback();
                                    truss.RowIndex1 = rowIndex1;
                                    truss.RowIndex2 = rowIndex2;
                                    Logger.Log($"桁车{truss.Name} 响应放货完成 失败，任务:{stepTrace.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                    return BllResultFactory.Error();
                                }
                            }
                            catch (Exception ex)
                            {
                                tran?.Rollback();
                                truss.RowIndex1 = rowIndex1;
                                truss.RowIndex2 = rowIndex2;
                                Logger.Log($"桁车{truss.Name}响应放货完成 发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                                return BllResultFactory.Error();
                            }
                        }
                    }
                    return BllResultFactory.Sucess();
                }
                else
                {
                    //未知情况
                    Logger.Log($"桁车{truss.Name}执行中，执行状态为：{truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskExcuteStatus.ToString()).Value}；可能由于ECS还未处理造成", LogLevel.Warning);
                }
                return BllResultFactory.Sucess();
            }
            else
            {
                return trussValidate;
            }
        }

        /// <summary>
        /// 检查桁车任务是否可以执行，并且生成其他桁车的避让
        /// </summary>
        /// <param name="truss"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTrace"></param>
        /// <returns></returns>
        private BllResult Check(Equipment truss, List<Equipment> allEquipments, StepTrace stepTrace, IPLC plc)
        {
            try
            {
                if (stepTrace.WcsProductType < 1)
                {
                    Logger.Log($"检查桁车{truss.Name}任务是否可以执行执行的时候，出现错误：任务id[{stepTrace.Id}]对应的工件类型不是大于1的整数！", LogLevel.Error);
                    return BllResultFactory.Error();
                }

                var beginStation = truss.StationList.FirstOrDefault(t => t.Id == stepTrace.StationId);
                var endStation = truss.StationList.FirstOrDefault(t => t.Id == stepTrace.NextStationId);
                if (beginStation == null)
                {
                    Logger.Log($"检查桁车{truss.Name}任务是否可以执行执行的时候，出现错误：找不到任务起始站台ID[{stepTrace.StationId}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (endStation == null)
                {
                    Logger.Log($"检查桁车{truss.Name}任务是否可以执行执行的时候，出现错误：找不到任务到达站台ID[{stepTrace.NextStationId}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }

                // 任务的最小距离
                long taskSmallDistance = beginStation.Distance;
                // 任务的最大距离
                long taskBigDistance = endStation.Distance;

                // 如果 最小距离 大于 最大距离，就要对调下
                if (taskSmallDistance > taskBigDistance)
                {
                    long temp = taskSmallDistance;
                    taskSmallDistance = taskBigDistance;
                    taskBigDistance = temp;
                }

                // 运行安全的最小距离
                long safetySmallDistance = taskSmallDistance - SafetyDistance;
                // 运行安全的最大距离
                long safetyBigDistance = taskBigDistance + SafetyDistance;

                //如果桁车的当前距离比任务的最小距离还小，就把运行距离拉大
                if (CurrentDistance < safetySmallDistance)
                {
                    taskSmallDistance = CurrentDistance;
                    safetySmallDistance = CurrentDistance;
                }
                //如果桁车的当前距离比任务的最大距离还大，就把运行距离拉大
                if (CurrentDistance > safetyBigDistance)
                {
                    taskBigDistance = CurrentDistance;
                    safetyBigDistance = CurrentDistance;
                }

                //找出其他的桁架
                List<Equipment> trusses = allEquipments.Where(t => t.EquipmentTypeId == truss.EquipmentTypeId && t.Id != truss.Id).ToList();

                #region  找出有避让任务的桁车，来判断是否会阻挡当前任务
                List<Equipment> avoidTrusses = trusses.Where(t => t.EquipmentProps.Find(x => x.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskNo.ToString()).Value == Int32.MaxValue.ToString()).ToList();
                foreach (var avoidTruss in avoidTrusses)
                {
                    long currentDistance = Int64.Parse(avoidTruss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.HorizontalDistance.ToString()).Value);
                    // 如果无任务的桁车阻挡了要执行的路劲，就调走该桁车
                    if (currentDistance >= safetySmallDistance && currentDistance <= safetyBigDistance)
                    {
                        //如果目标路径有任务了，就先不执行，等到任务完成了再执行本条任务
                        Logger.Log($"桁车{truss.Name}执行工序ID{stepTrace.Id}的任务需要运行{safetySmallDistance}到{safetyBigDistance}之间的路劲，该路径内有桁车{avoidTruss.Name}正在避让，需要等待！", LogLevel.Warning);
                        return BllResultFactory.Error();
                    }
                }
                #endregion
                #region 找出有任务的桁车（避让任务除外），来判断是否会阻挡当前任务
                List<Equipment> excuteTrusses = trusses.Where(t => t.RowIndex2 > 0 && !avoidTrusses.Exists(x => x.Id == t.Id)).ToList();

                // 找出任务的运行区间存在的桁车任务
                var excutingTruss = excuteTrusses.FirstOrDefault(t => (t.RowIndex1 >= safetySmallDistance && t.RowIndex1 <= safetyBigDistance)
                                                                     || (t.RowIndex2 >= safetySmallDistance && t.RowIndex2 <= safetyBigDistance)
                                                                     || (t.RowIndex1 < safetySmallDistance && t.RowIndex2 > safetyBigDistance));
                if (excutingTruss != null)
                {
                    //如果目标路径有任务了，就先不执行，等到任务完成了再执行本条任务
                    Logger.Log($"桁车{truss.Name}执行工序ID{stepTrace.Id}的任务需要运行{safetySmallDistance}到{safetyBigDistance}之间的路劲，该路径内有桁车{excutingTruss.Name}正在执行任务，需要等待！", LogLevel.Warning);
                    return BllResultFactory.Error();
                }
                #endregion
                #region 找出无任务的桁车，来判断是否会阻挡当前任务
                List<Equipment> freeTrusses = trusses.Where(t => t.RowIndex2 == 0).ToList();
                foreach (var freeTruss in freeTrusses)
                {
                    long currentDistance = Int64.Parse(freeTruss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.HorizontalDistance.ToString()).Value);
                    // 如果无任务的桁车阻挡了要执行的路劲，就调走该桁车
                    if (currentDistance >= safetySmallDistance && currentDistance <= safetyBigDistance)
                    {
                        #region 旧的根据距离来判断要去哪个点位
                        //Equipment tempEquipment = null;
                        //long tempSmallDistance = Int64.Parse(freeTruss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.ManageSmallDistance.ToString()).Value);
                        //long tempBigDistance = Int64.Parse(freeTruss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.ManageBigDistance.ToString()).Value);
                        //// 如果无任务的桁车的“最小运行距离”比当前桁车的“最小运行距离”小，就把无任务的桁车往 小距离 移动，否则就往大方向移动。 
                        //if (tempSmallDistance < ManageSmallDistance)
                        //{
                        //    tempEquipment = allEquipments.Where(t => t.RowIndex1 < safetySmallDistance && t.EquipmentTypeId != truss.EquipmentTypeId).OrderByDescending(t => t.RowIndex1).FirstOrDefault();
                        //}
                        //else
                        //{
                        //    tempEquipment = allEquipments.Where(t => t.RowIndex1 > safetyBigDistance && t.EquipmentTypeId != truss.EquipmentTypeId).OrderBy(t => t.RowIndex1).FirstOrDefault();
                        //}
                        #endregion
                        #region 固定的避让点位
                        //var station = truss.StationList.FirstOrDefault(t => t.Id == freeTruss.StationId);
                        //if (station == null)
                        //{
                        //    Logger.Log($"未查询到ID为{freeTruss.StationId}的站台", LogLevel.Error);
                        //    return BllResultFactory.Error();
                        //}
                        //freeTruss.RowIndex1 = currentDistance;
                        //freeTruss.RowIndex2 = freeTruss.Station.Distance;
                        //var updateBllResult = AppSession.Dal.UpdateCommonModel<Equipment>(truss);
                        //if (updateBllResult.Success)
                        //{
                        //    //下发桁车行走任务
                        //    BllResult sendResult = SendTaskToTruss(freeTruss, plc, TrussTaskFlag.机械手行走, "0", station.TrussTakeStationId.ToString(), "0", "0", "0");
                        //    if (sendResult.Success)
                        //    {
                        //        Logger.Log($"桁车{truss.Name}执行工序ID{stepTrace.Id}的任务需要运行{safetySmallDistance}到{safetyBigDistance}之间的路劲，下发任务移开{freeTruss.Code}桁车，写入PLC成功！", LogLevel.Success);
                        //    }
                        //    else
                        //    {
                        //        freeTruss.RowIndex1 = 0;
                        //        freeTruss.RowIndex2 = 0;
                        //        AppSession.Dal.UpdateCommonModel<Equipment>(truss);
                        //        Logger.Log($"桁车{truss.Name}执行工序ID{stepTrace.Id}的任务需要运行{safetySmallDistance}到{safetyBigDistance}之间的路劲，该路径内有桁车{excutingTruss.Name}空闲等待，下发任务移开该桁车时候，写入PLC失败，原因：{sendResult.Msg}！", LogLevel.Error);
                        //    }
                        //}
                        //else
                        //{
                        //    Logger.Log($"生成桁车{freeTruss.Name}的避让任务失败：更改数据库失败：{updateBllResult.Msg}", LogLevel.Error);
                        //}
                        #endregion
                        Logger.Log($"桁车[{truss.Name}]任务运行范围从{safetySmallDistance}到{safetyBigDistance}，给桁车[{freeTruss.Name}]下主动避让任务", LogLevel.Warning);
                        ToAvoidStation(freeTruss, allEquipments, plc);
                        return BllResultFactory.Error();
                    }
                }
                #endregion
                truss.RowIndex1 = taskSmallDistance;
                truss.RowIndex2 = taskBigDistance;
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"检查桁车[{truss.Name}]任务是否可以执行执行的时候，产生异常：${ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 检查本桁车是否需要避让其他桁车，如果需要就下发 机械手行走 来避让其他桁架，并且返回是否需要避让的判断
        /// </summary>
        /// <param name="truss"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTrace"></param>
        /// <param name="plc"></param>
        /// <returns>是否需要避让</returns>
        private BllResult Avoid(Equipment truss, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                // 找出其他的桁架
                List<Equipment> trusses = allEquipments.Where(t => t.EquipmentTypeId == truss.EquipmentTypeId && t.Id != truss.Id).ToList();
                // 找出有避让任务的桁车
                List<Equipment> avoidTrusses = trusses.Where(t => t.EquipmentProps.Find(x => x.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskNo.ToString()).Value == Int32.MaxValue.ToString()).ToList();
                // 找出有任务的桁车（避让任务除外）
                List<Equipment> excuteTrusses = trusses.Where(t => t.RowIndex2 > 0 && !avoidTrusses.Exists(x => x.Id == t.Id)).ToList();
                // 如果当前桁车挡住了其他桁车路劲，就去避让点等待
                var excuteTruss = excuteTrusses.FirstOrDefault(t => CurrentDistance >= t.RowIndex1 - SafetyDistance && CurrentDistance <= t.RowIndex2 + SafetyDistance);
                if (excuteTruss != null)
                {
                    Logger.Log($"桁车[{excuteTruss.Name}]任务运行范围从{excuteTruss.RowIndex1}到{excuteTruss.RowIndex2}，桁车[{truss.Name}]开始主动避让", LogLevel.Warning);
                    ToAvoidStation(truss, allEquipments, plc);
                    return BllResultFactory.Error();
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"桁车{truss.Name}执行避让程序的时候，产生异常：${ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 下发 桁架 到避让站台的任务
        /// </summary>
        /// <param name="truss"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTrace"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        private BllResult ToAvoidStation(Equipment truss, List<Equipment> allEquipments, IPLC plc)
        {
            try
            {
                var trussValidate = Validate(truss);
                if (!trussValidate.Success)
                {
                    Logger.Log($"{trussValidate.Msg}，不能生成避让任务移开，需等待！", LogLevel.Warning);
                    return trussValidate;
                }
                //判断桁车货叉上是不是有货，有货就不避让，无货才避让
                if (truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1HasPallet.ToString()).Value == "True")
                {
                    Logger.Log($"桁车{truss.Name}有货，不能生成避让任务移开，需等待该桁车任务完成！", LogLevel.Warning);
                    return BllResultFactory.Error();
                }
                //判断桁车状态不是“任务完成”、“删除任务” 才可以避让 （等待ECS确认任务完成得时候，桁车也是空闲）
                EquipmentProp fork1TaskFlag = truss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.WCSTaskFlag.ToString());
                if (fork1TaskFlag.Value == TrussTaskFlag.任务完成.GetIndexString())
                {
                    Logger.Log($"桁车{truss.Name}是{TrussTaskFlag.任务完成}状态，需要等待ECS确认，然后才能下发移车任务！", LogLevel.Warning);
                    return BllResultFactory.Error();
                }
                else if (fork1TaskFlag.Value == TrussTaskFlag.删除任务.GetIndexString())
                {
                    Logger.Log($"桁车{truss.Name}是{TrussTaskFlag.删除任务}状态，需要等待ECS确认，然后才能下发移车任务！", LogLevel.Warning);
                    return BllResultFactory.Error();
                }
                if (truss.Station == null)
                {
                    Logger.Log($"桁车[{truss.Name}]没有避让站台", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                // 正常模式下的避让点
                var avoidStation = truss.Station.TrussTakeStationId;
                #region 兼容模式的避让点
                //if (truss.Code == "Truss-Car001")
                //{
                //    var otherTruss = allEquipments.FirstOrDefault(t => t.Code == "Truss-Car002");
                //    if (otherTruss != null)
                //    {
                //        var otherTrussMode = bool.Parse(otherTruss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.CompatibleMode.ToString()).Value);
                //        if (otherTrussMode)
                //        {
                //            //如果桁架2是兼容模式，桁架1的避让点就是兼容模式的避让点
                //            avoidStation = truss.Station.TrussPutStationId;
                //        }
                //    }
                //}
                //if (truss.Code == "Truss-Car002")
                //{
                //    var otherTruss = allEquipments.FirstOrDefault(t => t.Code == "Truss-Car001");
                //    if (otherTruss != null)
                //    {
                //        var otherTrussMode = bool.Parse(otherTruss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.CompatibleMode.ToString()).Value);
                //        if (otherTrussMode)
                //        {
                //            //如果桁架2是兼容模式，桁架1的避让点就是兼容模式的避让点
                //            avoidStation = truss.Station.TrussPutStationId;
                //        }
                //    }
                //}
                //if (truss.Code == "Truss-Car003")
                //{
                //    var otherTruss = allEquipments.FirstOrDefault(t => t.Code == "Truss-Car004");
                //    if (otherTruss != null)
                //    {
                //        var otherTrussMode = bool.Parse(otherTruss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.CompatibleMode.ToString()).Value);
                //        if (otherTrussMode)
                //        {
                //            //如果桁架3是兼容模式，桁架4的避让点就是兼容模式的避让点
                //            avoidStation = truss.Station.TrussPutStationId;
                //        }
                //    }
                //}
                //if (truss.Code == "Truss-Car004")
                //{
                //    var otherTruss = allEquipments.FirstOrDefault(t => t.Code == "Truss-Car003");
                //    if (otherTruss != null)
                //    {
                //        var otherTrussMode = bool.Parse(otherTruss.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == TrussNormalProps.CompatibleMode.ToString()).Value);
                //        if (otherTrussMode)
                //        {
                //            //如果桁架4是兼容模式，桁架3的避让点就是兼容模式的避让点
                //            avoidStation = truss.Station.TrussPutStationId;
                //        }
                //    }
                //}
                #endregion
                truss.RowIndex1 = CurrentDistance;
                truss.RowIndex2 = truss.Station.Distance;
                var updateBllResult = AppSession.Dal.UpdateCommonModel<Equipment>(truss);
                if (updateBllResult.Success)
                {
                    //下发桁车行走任务
                    BllResult sendResult = SendTaskToTruss(truss, plc, TrussTaskFlag.机械手行走, "0", avoidStation.ToString(), "0", "0", Int32.MaxValue.ToString());
                    if (sendResult.Success)
                    {
                        Logger.Log($"桁车[{truss.Name}]下发去站台[{truss.Station.Name}]的[机械手行走]任务下发成功！", LogLevel.Success);
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        truss.RowIndex1 = 0;
                        truss.RowIndex2 = 0;
                        AppSession.Dal.UpdateCommonModel<Equipment>(truss);
                        Logger.Log($"桁车[{truss.Name}]下发去站台[{truss.Station.Name}]的[机械手行走]任务，写入PLC失败，原因：{sendResult.Msg}！", LogLevel.Error);
                    }
                }
                else
                {
                    Logger.Log($"桁车[{truss.Name}]下发去站台[{truss.Station.Name}]的[机械手行走]任务, 失败：更改数据库失败：{updateBllResult.Msg}", LogLevel.Error);
                }
                return BllResultFactory.Error();
            }
            catch (Exception ex)
            {
                Logger.Log($"桁车[{truss.Name}]下发去站台[{truss.Station.Name}]的[机械手行走]任务的时候，产生异常：${ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }
        }


        /// <summary>
        /// 产品下线分配 站台、列、层
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="stepTrace"></param>
        /// <returns></returns>
        public BllResult setStation(List<Station> stations, StepTrace stepTrace, int NextStepId)
        {
            if (stations.Count < 1)
            {
                Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，分配库位失败，没有目的站台", LogLevel.Error);
                return BllResultFactory.Error();
            }
            if (stepTrace == null)
            {
                Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，StepTrace类为NULL，系统内部错误", LogLevel.Error);
                return BllResultFactory.Error();
            }

            //TODO: 根据已匹配到的任务产品编码，查询与实际成品下线站台的摆放类型
            if (stepTrace.ProductCode == null)
            {
                Logger.Log($"桁车处理站台【{stepTrace.StationId}】;任务号{stepTrace.Id}{stepTrace.ProductCode}为空,请检查数据", LogLevel.Error);
                return BllResultFactory.Error();
            }
            //根据工序查询产品表
            var productCodeResult = AppSession.Dal.GetCommonModelByCondition<ProductHeader>($"where Code='{stepTrace.ProductCode}'");
            if (!productCodeResult.Success || productCodeResult.Data.Count == 0)
            {
                Logger.Log($"桁车处理站台【{stepTrace.StationId}】;任务号{stepTrace.Id}产品编码{stepTrace.ProductCode}，查询产品表失败,请检查数据", LogLevel.Error);
                return BllResultFactory.Error();
            }
            //TODO： int值默认为0,在这里我们不能处理
            var productHeader = productCodeResult.Data[0];
            if (productHeader.PutType == 0)
            {
                Logger.Log($"桁车处理站台【{stepTrace.StationId}】;任务号{stepTrace.Id}产品编码{stepTrace.ProductCode},摆放类型{productHeader.PutType}无效，查询产品表失败,请检查数据", LogLevel.Error);
                return BllResultFactory.Error();
            }
            if (productHeader.PutLayer == 0)
            {
                Logger.Log($"桁车处理站台【{stepTrace.StationId}】;任务号{stepTrace.Id}产品编码{stepTrace.ProductCode},摆放层数{productHeader.PutLayer}无效，查询产品表失败,请检查数据", LogLevel.Error);
                return BllResultFactory.Error();
            }
            if (productHeader.PutLine == 0)
            {
                Logger.Log($"桁车处理站台【{stepTrace.StationId}】;任务号{stepTrace.Id}产品编码{stepTrace.ProductCode},摆放列数{productHeader.PutLine}无效，查询产品表失败,请检查数据", LogLevel.Error);
                return BllResultFactory.Error();
            }

            #region 注释原先处理下线代码
            //StringBuilder sb = new StringBuilder();
            //foreach (var station in stations)
            //{
            //    sb.Append($"'{station.Code}',");
            //}
            //var srmCode = sb.ToString(0, sb.Length - 1);
            //connection.Open();

            ////下线是有位置就能去
            //var locations = connection.Query<Location>($"select top 1 * from location  where srmCode in ({srmCode}) and line < 5 and layer < 3 and isStop = 0 and isLock = 0 order by layer desc,line desc");
            //if (locations.Count() > 0)
            //{
            //    var location = locations.First();
            //    stepTrace.NextStepId = NextStepId;
            //    stepTrace.NextStationId = stations.Find(t => t.Code == location.SrmCode).Id.Value;
            //    stepTrace.WCSLine = location.Line.Value.ToString();
            //    stepTrace.WCSLayer = location.Layer.Value.ToString();
            //    stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
            //    stepTrace.UpdateTime = DateTime.Now;
            //    stepTrace.UpdateBy = App.User.UserCode;

            //    if (location.Line < 4)
            //    {
            //        location.Line++;
            //    }
            //    else
            //    {
            //        location.Layer++;
            //        location.Line = 1;
            //    }

            //    tran = connection.BeginTransaction();
            //    connection.Update<StepTrace>(stepTrace, transaction: tran);
            //    connection.Update<Location>(location, transaction: tran);
            //    tran.Commit();
            //    return BllResultFactory.Sucess();
            //}
            #endregion

            #region 
            //HACK: 如果存在小件产品，是不是存在放3
            //string sql = $"select * from location  where srmCode in ({srmCode}) and line < 5 and layer < 3 and isStop = 0 and isLock = 0 order by layer desc,line desc";
            //var resultToList = connection.GetList<Location>(sql, connection);
            //var ls = resultToList.ToList();
            //foreach (var item in ls)
            //{
            //    //如果位置上有货，该需要校验第一个数据；
            //    //如果位置两个位置上都有货，那就需要校验两个位置站台
            //    if (item.PutType == (int)PutTypeFlag.初始摆放类型值 || item.PutType == productHeader.PutType)
            //    {
            //        var location = item;
            //        stepTrace.NextStepId = NextStepId;
            //        stepTrace.NextStationId = stations.Find(t => t.Code == location.SrmCode).Id.Value;
            //        stepTrace.WCSLine = location.Line.Value.ToString();
            //        stepTrace.WCSLayer = location.Layer.Value.ToString();
            //        stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
            //        stepTrace.UpdateTime = DateTime.Now;
            //        stepTrace.UpdateBy = App.User.UserCode;

            //        if (location.Line == 1 && location.Layer == 1)
            //        {
            //            location.PutType = productHeader.PutType;
            //        }
            //        //TODO: 这里是否采用PutType区分是放8个还是3个
            //        //TODO：如果设计成配置式，那也就需要维护两个数据量，一是对应产品类型，已经该产品类型所能摆放的数据量。然后在进行下料的动态计算；否者不下料处理，后续维护以此为基准数据
            //        if (location.Line < 4)
            //        {
            //            location.Line++;
            //        }
            //        else
            //        {
            //            location.Layer++;
            //            location.Line = 1;
            //        }
            //        tran = connection.BeginTransaction();
            //        connection.Update<StepTrace>(stepTrace, transaction: tran);
            //        connection.Update<Location>(location, transaction: tran);
            //        tran.Commit();
            //        return BllResultFactory.Sucess();
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}
            #endregion

            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (var station in stations)
                {
                    sb.Append($"'{station.Code}',");
                }
                var srmCode = sb.ToString(0, sb.Length - 1);

                string conditions = $" where islock = 0 and StationCode in ({srmCode}) and putType in ({(int)PutTypeFlag.初始摆放类型值},{productHeader.PutType}) order by putNumber desc";
                var listResult = AppSession.Dal.GetCommonModelByCondition<ProductFinishedStation>(conditions);
                if (!listResult.Success)
                {
                    return BllResultFactory.Error("没有可用的成品下线工位");
                }
                var productFinishedStation = listResult.Data[0];
                //如果数量一致，就跳出
                if (productFinishedStation.PutNumber == productHeader.PutNumber)
                {
                    //或者生成一条回收空托盘的AGV任务
                    productFinishedStation.IsLock = 2;
                    AppSession.Dal.UpdateCommonModel<ProductFinishedStation>(productFinishedStation);
                    return BllResultFactory.Error($"站台[{productFinishedStation.Code}][{productFinishedStation.Name}]已经放满，修改工位状态为 放满锁定");
                }
                //用与获取到能存放的站台数据
                if (productFinishedStation.PutType == (int)PutTypeFlag.初始摆放类型值)
                {
                    productFinishedStation.PutType = productHeader.PutType;
                    productFinishedStation.ProductType = stepTrace.WcsProductType.Value;
                    //摆放结果，应该是跟随产品而来
                    productFinishedStation.PutLayer = productHeader.PutLayer;
                    productFinishedStation.PutLine = productHeader.PutLine;
                }

                stepTrace.NextStepId = NextStepId;
                stepTrace.NextStationId = stations.Find(t => t.Code == productFinishedStation.StationCode).Id.Value;
                stepTrace.WCSLine = productFinishedStation.Line.Value.ToString();
                stepTrace.WCSLayer = productFinishedStation.Layer.Value.ToString();
                stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
                stepTrace.UpdateTime = DateTime.Now;
                stepTrace.UpdateBy = App.User.UserCode;
                //能放的列数肯定是小于设定的列数；
                if (productFinishedStation.Line < productFinishedStation.PutLine)
                {
                    productFinishedStation.Line++;
                }
                else
                {
                    productFinishedStation.Layer++;
                    productFinishedStation.Line = 1;
                }
                //每执行一条任务往上加1.等于产品数量也就不能放了
                productFinishedStation.PutNumber++;
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction tran = null;
                    try
                    {
                        tran = connection.BeginTransaction();
                        connection.Update<StepTrace>(stepTrace, transaction: tran);
                        connection.Update<ProductFinishedStation>(productFinishedStation, transaction: tran);
                        tran.Commit();
                        return BllResultFactory.Sucess();
                    }
                    catch (Exception ex)
                    {
                        tran?.Rollback();
                        Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，发生异常，原因：{ex.Message}", LogLevel.Exception, ex);
                        return BllResultFactory.Error();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"桁车处理站台【{stepTrace.StationId}】下料请求时候，发生异常，原因：{ex.Message}", LogLevel.Exception, ex);
                return BllResultFactory.Error();
            }

        }
    }
}
