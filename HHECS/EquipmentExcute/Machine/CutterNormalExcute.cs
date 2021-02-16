using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Car;
using HHECS.Model.Enums.Machine;
using HHECS.Model.Enums.PipeLine;
using HHECS.Model.Enums.Task;
using HHECS.Model.PLCHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HHECS.EquipmentExcute.Machine
{
    /// <summary>
    /// 切割机处理类
    /// </summary>
    public class CutterNormalExcute : MachineExcute
    {
        /// <summary>
        /// 执行上料完成
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="cutter"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteArrive(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                //var count = stepTraceList.Count(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == cutter.StationId);
                //if (count > 1)
                //{
                //    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]上料完成的时候，出现数据错误，站台有多个对应的任务", LogLevel.Error);
                //    return BllResultFactory.Error();
                //}
                //var stepTrace = stepTraceList.FirstOrDefault(t => t.Status == StepTraceStatus.响应放货完成.GetIndexInt() && t.StationId == cutter.StationId);
                var ArriveTaskId = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveTaskId.ToString());
                var convertResult = int.TryParse(ArriveTaskId.Value, out int stationCacheId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 上料请求失败，管材缓存标识[{ArriveTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stationCacheId < 1)
                {
                    Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 上料请求失败，管材缓存标识[{ArriveTaskId.Value}]不能小于1！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stationCacheResult = AppSession.Dal.GetCommonModelByCondition<StationCache>($" where id = '{stationCacheId}' ");
                if (!stationCacheResult.Success)
                {
                    Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 上料请求失败，根据管材缓存标识[{stationCacheId}]查询数据失败，原因：[{stationCacheResult.Msg}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stationCache = stationCacheResult.Data[0];
                ////记录旧数据
                //var stationCacheClone = (StationCache)stationCache.DeepClone();
                ////将缓存的管子标记为已用
                //stationCache.Status = StationCacheStatus.使用中.GetIndexInt();
                //stationCache.CreateTime = DateTime.Now;
                //stationCache.CreateBy = App.User.UserCode;
                //var updateResult = AppSession.Dal.UpdateCommonModel<StationCache>(stationCache);
                //if (updateResult.Success)
                //{
                var number = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveRealAddress.ToString());
                BllResult sendResult = SendAckToPlc(cutter, plc, MachineMessageFlag.WCS回复允许上料, number.Value, stationCache.Id.ToString(), "", "0", stationCache.WcsProductType.ToString(), stationCache.MaterialLength.ToString(), stationCache.Diameter.ToString(), stationCache.Thickness.ToString(), cutter.SelfAddress);
                if (sendResult.Success)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]上料请求成功，管材缓存标识[{stationCache.Id}]相关信息写入设备成功", LogLevel.Success);
                }
                else
                {
                    //AppSession.Dal.UpdateCommonModel<StationCache>(stationCacheClone);
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]上料请求失败，管材缓存标识[{stationCache.Id}]信息没写入设备失败，原因：{sendResult.Msg}", LogLevel.Error);
                }
                return sendResult;
                //}
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]上料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
            }
            return BllResultFactory.Error();
        }

        
        /// <summary>
        /// 处理定长切割请求
        /// </summary>
        /// <param name="cutter"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteCut(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var RequestCutTaskId = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestCutTaskId.ToString());
                var convertResult = int.TryParse(RequestCutTaskId.Value, out int stationCacheId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 自动请求切割失败，管材缓存标识[{RequestCutTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (stationCacheId < 1)
                {
                    Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 自动请求切割失败，管材缓存标识[{RequestCutTaskId.Value}]不能小于1！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //找出  套料计划
                var cutPlanResult = AppSession.Dal.GetCommonModelByConditionWithZero<CutPlan>($" where stationCacheId = {stationCacheId}");
                if (!cutPlanResult.Success)
                {
                    Logger.Log($"查询套料计划出错，原因：{cutPlanResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                if (cutPlanResult.Data.Count == 0)
                {
                    #region 找不到套料方案 ，返回给PLC没有套料方案
                    //没有套料计划，就告诉定长切割机 切完了
                    var sendResult = SendCutToPlc(cutter, plc, CutFlag.WCS回复没有套料方案, "0", "0", "0", "0", "0");
                    if (sendResult.Success)
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功，告诉设备 没有套料方案。", LogLevel.Success);
                    }
                    else
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功失败，写入PLC失败，原因：{sendResult.Msg}", LogLevel.Error);
                    }
                    return sendResult;
                    #endregion
                }
                var cutPlans = cutPlanResult.Data.Where(t => t.Status == CutPlanStatus.初始.GetIndexInt()).ToList();
                if (cutPlans.Count == 0)
                {
                    #region 如果整根管子的套料计划都切割完毕，返回给PLC切割结束
                    //var stationCacheResult = AppSession.Dal.GetCommonModelByCondition<StationCache>($" where id = '{stationCacheId}' ");
                    //if (!stationCacheResult.Success)
                    //{
                    //    Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 自动请求切割失败，查询缓存区失败，原因：[{stationCacheResult.Msg}]", LogLevel.Error);
                    //    return BllResultFactory.Error();
                    //}
                    //var sql = $"update station_cache set status = {StationCacheStatus.使用完毕.GetIndexInt()},updateBy='{App.User.UserCode}',updateTime='{DateTime.Now}' where id = {stationCacheId}";
                    //using (IDbConnection connection = AppSession.Dal.GetConnection())
                    //{
                    //    IDbTransaction tran = null;
                    //    try
                    //    {
                    //        connection.Open();
                    //        tran = connection.BeginTransaction();
                    //        connection.Execute(sql, transaction: tran);
                    //        //没有套料计划，就告诉定长切割机 切完了
                            var sendResult = SendCutToPlc(cutter, plc, CutFlag.WCS回复结束切割, "0", "0", "0", "0", "0");
                            if (sendResult.Success)
                            {
                                //tran.Commit();
                                Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功，告诉设备 结束切割。", LogLevel.Success);
                            }
                            else
                            {
                                //tran?.Rollback();
                                Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功失败，写入PLC失败，原因：{sendResult.Msg}", LogLevel.Error);
                            }
                            return sendResult;
                        //}
                        //catch (Exception ex)
                        //{
                        //    tran?.Rollback();
                        //    Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 自动请求切割的时候，删除管子缓存标识[{stationCacheId}]的数据候发生异常，原因：{ex.Message}", LogLevel.Exception, ex);
                        //}
                    //}
                    //return BllResultFactory.Error();
                    #endregion
                }
                var cutPlan = cutPlans[0];
                #region 如果未切割的套料计划，就发送切割机

                //根据产品找出前两个工序，第一个是组焊工序，第二个就是下个工序
                var stepResult = AppSession.Dal.GetCommonModelByCondition<Step>("");
                if (!stepResult.Success)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割时候，没有工序数据，原因：{stepResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var step = stepResult.Data.FirstOrDefault(t => t.Code == "Cutting");
                if (step == null)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割时候，没有检测到[Cutting]工序！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var nextStep = stepResult.Data.FirstOrDefault(t => t.Sequence > step.Sequence);
                if (nextStep == null)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割时候，没有检测到[Cutting]工序的下一道工序！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var productHeaderResult = AppSession.Dal.GetCommonModelByCondition<ProductHeader>($"where wcsProductType = {cutPlan.WcsProductType}");
                if (!productHeaderResult.Success)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割时候，没有检测到电器编号[{cutPlan.WcsProductType}]对应的产品，原因：{productHeaderResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }

                //向工序表插入一条记录
                StepTrace stepTrace = new StepTrace();
                stepTrace.WONumber = cutPlan.WONumber;
                stepTrace.SerialNumber = cutPlan.Id.ToString();
                stepTrace.ProductId = productHeaderResult.Data[0].Id.Value;
                stepTrace.ProductCode = productHeaderResult.Data[0].Code;
                stepTrace.WcsProductType = cutPlan.WcsProductType;
                stepTrace.LineId = cutter.LineId;
                stepTrace.StepId = step.Id.Value;
                stepTrace.NextStepId = nextStep.Id.Value;
                stepTrace.StationId = cutter.StationId;
                stepTrace.PipeMaterial = cutPlan.WcsProductType;
                stepTrace.PipeLength = cutPlan.Length;
                stepTrace.PipeDiameter = cutPlan.Diameter;
                stepTrace.PipeThickness = cutPlan.Thickness;
                stepTrace.NextStationId = 0;
                stepTrace.IsNG = false;
                stepTrace.NGcode = "";
                stepTrace.IsInvalid = false;
                stepTrace.WeldingNo = cutPlan.AssemblyStation.ToString();
                stepTrace.Status = StepTraceStatus.设备开始生产.GetIndexInt();
                stepTrace.StationInTime = DateTime.Now;
                stepTrace.LineInTime = DateTime.Now;
                stepTrace.CreateTime = DateTime.Now;
                stepTrace.CreateBy = App.User.UserCode;

                //修改套料状态为已经发送给CES
                cutPlan.Status = CutPlanStatus.发送定长.GetIndexInt();

                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction tran = null;
                    try
                    {
                        connection.Open();
                        tran = connection.BeginTransaction();
                        stepTrace.Id = connection.Insert<StepTrace>(stepTrace, transaction: tran);
                        connection.Update<CutPlan>(cutPlan, transaction: tran);
                        //如果是最后一个套料结果，那就可以把管子状态改为使用完毕
                        if (cutPlans.Count == 1)
                        {
                            var sql = $"update station_cache set status = {StationCacheStatus.使用完毕.GetIndexInt()},updateBy='{App.User.UserCode}',updateTime='{DateTime.Now}' where id = {stationCacheId}";
                            connection.Execute(sql:sql, transaction:tran);
                        }
                        var sendResult = SendCutToPlc(cutter, plc, CutFlag.WCS回复允许切割, stepTrace.Id.ToString(), stepTrace.PipeMaterial.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString());
                        if (sendResult.Success)
                        {
                            tran?.Commit();
                            Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功，返回给设备管子切割[{stepTrace.PipeLength}]长度。", LogLevel.Success);
                        }
                        else
                        {
                            tran?.Rollback();
                            Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割成功失败，写入PLC失败，原因：{sendResult.Msg}", LogLevel.Error);
                        }
                        return sendResult;
                    }
                    catch (Exception ex)
                    {
                        tran?.Rollback();
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]自动请求切割的时候，发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                        return BllResultFactory.Error();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]允许翻转信号时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
            }
            return BllResultFactory.Error();
        }

        /// <summary>
        /// 处理打标请求
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="allEquipments"></param>
        /// <param name="stepTraceList"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcutePint(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            var RequestPintTaskId = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestPintTaskId.ToString());
            var convertResult = int.TryParse(RequestPintTaskId.Value, out int stepTraceId);
            if (!convertResult)
            {
                Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 请求打印失败，PLC请求打印-任务号[{RequestPintTaskId.Value}]转化为整数失败！", LogLevel.Error);
                return BllResultFactory.Error();
            }
            if (stepTraceId < 1)
            {
                Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 请求打印失败，PLC请求打印-任务号[{RequestPintTaskId.Value}]不能小于1！", LogLevel.Error);
                return BllResultFactory.Error();
            }
            var stepTrace = stepTraceList.Find(t => t.Id == stepTraceId);
            if (stepTrace == null)
            {
                Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 请求打印失败，PLC请求打印-任务号[{RequestPintTaskId.Value}]找不到对应的任务！", LogLevel.Error);
                return BllResultFactory.Error();
            }
            var cutPlanResult = AppSession.Dal.GetCommonModelByCondition<CutPlan>($" where id = '{stepTrace.SerialNumber}'");
            if (!cutPlanResult.Success)
            {
                Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]请求打印的时候，数据错误：不存在条码[{stepTrace.WONumber}]对应的套料工单！，原因：{cutPlanResult.Msg}", LogLevel.Error);
                return BllResultFactory.Error();
            }
            var cutPlan = cutPlanResult.Data[0];
            if (cutPlan.Status == CutPlanStatus.打标完成.GetIndexInt())
            {
                // 发送回复确认打标成功
                var sendResult = SendPintToPlc(cutter, plc, PrintFlag.WCS回复打印);
                if (sendResult.Success)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]请求打印成功，回复打标完成信息写入PLC成功！", LogLevel.Success);
                }
                else
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]请求打印成功，回复打标完成信息写入PLC失败失败，原因：{sendResult.Msg}", LogLevel.Error);
                }
                return sendResult;
            }
            else if (cutPlan.Status < CutPlanStatus.发送打标机.GetIndexInt())
            {
                //记录旧数据
                var cutPlanClone = (CutPlan)cutPlan.DeepClone();
                //更新状态
                cutPlan.Status = CutPlanStatus.发送打标机.GetIndexInt();
                var updateResult = AppSession.Dal.UpdateCommonModel<CutPlan>(cutPlan);
                if (updateResult.Success)
                {
                    // 发送打标机进行打标
                    BllResult printResult = Marking.MarkingNormalExcute.PrintBarcode(stepTrace.WONumber, stepTrace.SerialNumber);
                    if (printResult.Success)
                    {
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]请求打印成功，给打标机发送条码[{stepTrace.WONumber}]成功！", LogLevel.Success);
                    }
                    else
                    {
                        AppSession.Dal.UpdateCommonModel<CutPlan>(cutPlanClone);
                        Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]请求打印失败，给打标机发送条码[{stepTrace.WONumber}]失败，原因：{printResult.Msg}", LogLevel.Error);
                    }
                    return printResult;
                }
                else
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]请求打印失败！修改数据库中套料状态失败！原因：{updateResult.Msg}", LogLevel.Error);
                }
                return updateResult;
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 执行下料请求
        /// 注意：allEquipments引用所有设备，此为共享应用
        /// </summary>
        /// <param name="cutter"></param>
        /// <param name="allEquipments"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        public override BllResult ExcuteRequest(Equipment cutter, List<Equipment> allEquipments, List<StepTrace> stepTraceList, IPLC plc)
        {
            try
            {
                var RequestTaskId = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestTaskId.ToString());
                var convertResult = int.TryParse(RequestTaskId.Value, out int stepTraceId);
                if (!convertResult)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]下料请求失败，工序任务的id[{RequestTaskId.Value}]转化为整数失败！", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                var stepTrace = stepTraceList.FirstOrDefault(t => t.Id == stepTraceId);
                if (stepTrace == null)
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]下料请求失败，找不到未完成的工序任务id[{RequestTaskId.Value}]", LogLevel.Error);
                    return BllResultFactory.Error();
                }

                //下个站台对应的设备
                Equipment nextEquipment = null;

                var stepStationsResult = AppSession.Dal.GetCommonModelByCondition<StepStation>($" where stepId = {stepTrace.NextStepId}");
                if (!stepStationsResult.Success )
                {
                    Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]下料请求时候，数据错误：不存在工序[{stepTrace.NextStepId}]对应的站台！，原因：{stepStationsResult.Msg}", LogLevel.Error);
                    return BllResultFactory.Error();
                }
                //找到工序对应的所有站台
                var stationList = cutter.StationList.Where(t => stepStationsResult.Data.Exists(x => x.StationId == t.Id)).ToList();
                // 排除已经被预定的站台
                stationList.RemoveAll(t => stepTraceList.Exists(x => x.NextStationId == t.Id));
                if (stationList.Count() == 0)    return BllResultFactory.Error();

                // 根据站台找到所有对应的设备
                var tempEquipments = allEquipments.Where(t => stationList.Exists(x => x.Id == t.StationId)).ToList();
                // 缓存位不能放长度超过自己的管子
                tempEquipments = tempEquipments.Where(t => t.ColumnIndex >= stepTrace.PipeLength).OrderBy(t => t.ColumnIndex).ToList();
                //如果管子长度小于1800，就不能放到中间的坡口缓存位，因为间隙大，管子会掉下来
                if (stepTrace.PipeLength < 1800)
                {
                      tempEquipments.RemoveAll(t => t.Code == "BevelCache2");
                }

                if (tempEquipments.Count() == 0)  return BllResultFactory.Error();

                foreach (var item in tempEquipments)
                {
                    var OperationModel = item.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachineProps.OperationModel.ToString());
                    var TotalError = item.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachineProps.TotalError.ToString());
                    if (OperationModel?.Value != OperationModelFlag.联机.GetIndexString() || TotalError?.Value != TotalErrorFlag.无故障.GetIndexString())
                    {
                        continue;
                    }
                    var ArriveMessage = item.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachineProps.ArriveMessage.ToString());
                    //如果站台请求上料 或者 输送线为空，才可以用 
                    var HasGoods = item.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == PipeLineProps.HasGoods.ToString());
                    if (ArriveMessage?.Value == MachineMessageFlag.PLC自动请求上料.GetIndexString() || HasGoods?.Value == PipeLineGoods.无货.GetIndexString())
                    {
                        //增加任务判断
                        var countResult = AppSession.Dal.GetCommonModelCount<StepTrace>($"where status >{StepTraceStatus.设备开始生产.GetIndexInt()} and status < {StepTraceStatus.任务完成.GetIndexInt()} and nextStationId = {item.StationId}");
                        if (countResult.Success && countResult.Data < 1)
                        {
                            nextEquipment = item;
                            break;
                        }
                    }
                }
                if (nextEquipment != null && nextEquipment.StationId != 0)
                {
                    var requestNumber = cutter.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == MachineProps.RequestNumber.ToString());

                    //就生成AGV任务并且插入数据库   小车任务的状态和类型需要重新定义？   
                    CarTask taskCar = new CarTask();
                    //taskCar.StepTraceId = Convert.ToInt32(Step_Trace_Id);
                    taskCar.StepTraceId = stepTrace.Id;
                    taskCar.CarNo = 1;
                    taskCar.Type = CarTaskType.取货和放货.GetIndexInt();
                    taskCar.FromLocation = cutter.Station.TrussTakeStationId.ToString();
                    taskCar.ToLocation = nextEquipment.Station.TrussPutStationId.ToString();
                    taskCar.ReSend = 0;
                    taskCar.Status = TaskCarStatus.Init.GetIndexInt();
                    taskCar.StartTime = DateTime.Now;
                    taskCar.EndTime = DateTime.Now;
                    taskCar.CreateTime = DateTime.Now;
                    taskCar.UpdateTime = DateTime.Now;

                    //taskCar.IsFlip = Convert.ToInt32(Allow_Flip);

                    //修改工序监控
                    stepTrace.NextStationId = nextEquipment.StationId;
                    stepTrace.Status = StepTraceStatus.等待任务执行.GetIndexInt();
                    stepTrace.UpdateTime = DateTime.Now;
                    stepTrace.UpdateBy = App.User.UserCode;

                    using (IDbConnection connection = AppSession.Dal.GetConnection())
                    {
                        IDbTransaction tran = null;
                        try
                        {
                            connection.Open();
                            tran = connection.BeginTransaction();
                            connection.Insert<CarTask>(taskCar, transaction: tran);
                            connection.Update<StepTrace>(stepTrace, transaction: tran);
                            //connection.Execute($"delete from step_trace where id < {stepTrace.Id} and stationId = {cutter.StationId} and status = {StepTraceStatus.设备开始生产.GetIndexInt()}");
                            var sendResult = SendAddressReplyToPlc(cutter, plc, MachineMessageFlag.WCS回复允许下料, requestNumber.Value, stepTrace.Id.ToString(), "", "0", stepTrace.WcsProductType.ToString(), stepTrace.PipeLength.ToString(), stepTrace.PipeDiameter.ToString(), stepTrace.PipeThickness.ToString(), cutter.GoAddress);
                            if (sendResult.Success)
                            {
                                tran.Commit();
                                Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 下料请求 成功，任务:{stepTrace.Id}", LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                tran?.Rollback();
                                Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 下料请求的时候，写入PLC失败，任务:{stepTrace.Id}，原因：{sendResult.Msg}", LogLevel.Error);
                                return BllResultFactory.Error();
                            }
                        }
                        catch (Exception ex)
                        {
                            tran?.Rollback();
                            Logger.Log($"处理工位[{cutter.StationCode}]的设备[{cutter.Name}] 下料请求的时候，发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                            return BllResultFactory.Error();
                        }
                    }
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                Logger.Log($"处理工位[{cutter.StationId}]设备[{cutter.Name}]下料请求时候，发生异常：{ex.Message}", LogLevel.Exception, ex);
            }
            return BllResultFactory.Error();
        }



    }
}
