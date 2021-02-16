using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Bll
{
    public class StepTraceService : BaseService
    {

        /// <summary>
        /// 人工确认，新增任务数据
        /// </summary>
        public BllResult HumanTasks(StepTrace stepTrace)
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
            //stepTraceLog.SrmCode = truss.Code;
            stepTraceLog.PassOrFail = "";
            stepTraceLog.IsNG = stepTrace.IsNG;
            stepTraceLog.NGcode = stepTrace.NGcode;
            stepTraceLog.StationInTime = DateTime.Now;
            //stepTraceLog.StationOutTime = stepTrace.StationOutTime;
            stepTraceLog.LineInTime = stepTrace.LineInTime;
            //stepTraceLog.LineOutTime = DateTime.Now;
            stepTraceLog.CreateBy = stepTrace.CreateBy;
            stepTraceLog.CreateTime = DateTime.Now;

            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                   var res1 = AppSession.Dal.InsertCommonModel<StepTrace>(stepTrace, connection, tran);
                    if (!res1.Success)
                    {
                        tran?.Rollback();
                        return BllResultFactory.Error($"插入工序跟踪表发生错误：{res1.Msg}");
                    }
                    var res2 = AppSession.Dal.InsertCommonModel<StepTraceLog>(stepTraceLog, connection, tran);
                    if (!res2.Success)
                    {
                        tran?.Rollback();
                        return BllResultFactory.Error($"插入工序跟踪日志表发生错误：{res1.Msg}");
                    }
                    tran.Commit();
                    stepTrace.Id = res1.Data;
                    stepTraceLog.Id = res2.Data;
                    return BllResultFactory.Sucess();
                }
                catch (Exception ex)
                {
                    tran?.Rollback();                    
                    return BllResultFactory.Error($"发生异常：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 空出处理
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public BllResult EmptyOutHandle(StepTrace task)
        {
            //List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>
            //    {
            //        new KeyValuePair<string, string>("taskId", task.Id.ToString())
            //    };
            //return AppSession.CommonService.FormPost(list, WMSUrls.HandleEmptyOut, client, urls);
            try
            {
                task.Status = (int)StepTraceStatus.异常结束;
                //task.IsEmptyOut = 1;
                return AppSession.Dal.UpdateCommonModel<StepTrace>(task);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(null, "更新失败：" + ex.ToString());
            }
        }

        public BllResult StepTraceExceptionOver(int taskId, string userCode, List<Station> stations, List<Step> steps)
        {
            var stepTraceResult = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id = {taskId}");
            if (!stepTraceResult.Success)
            {
                return BllResultFactory.Error(stepTraceResult.Msg);
            }
            return StepTraceExceptionOver(stepTraceResult.Data[0], userCode, stations, steps);
        }

        public BllResult StepTraceExceptionOver(StepTrace stepTrace, string userCode, List<Station> stations, List<Step> steps)
        {
            if (stepTrace.Status >= StepTraceStatus.任务完成.GetIndexInt())
            {
                return BllResultFactory.Error("任务已经完成，不能修改数据");
            }
            var oldStatus = stepTrace.Status;

            stepTrace.Status = StepTraceStatus.异常结束.GetIndexInt();
            stepTrace.UpdateTime = DateTime.Now;
            stepTrace.UpdateBy = userCode;

            var step = steps.Find(t => t.Id == stepTrace.NextStepId);
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    //也就是这里已经到了成品下线工序
                    if (step != null && step.StationType == "FinishedType")
                    {
                        if (stepTrace.WCSLayer != null && stepTrace.WCSLine != null && stepTrace.WCSLayer != "0" && stepTrace.WCSLine != "0")
                        {
                            //这里拿取对应的站台数据，更新库位数据
                            var res = updateLocation(stepTrace, stations);
                            if (!res.Success)
                            {
                                return BllResultFactory.Error($"异常结束任务时，更新库位数据{stepTrace.Id}失败：{res.Msg}");
                            }
                        }
                    }
                    var result = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace, connection, transaction);
                    if (!result.Success)
                    {
                        transaction.Rollback();
                        AppSession.LogService.LogContent(LogTitle.任务状态维护, $"任务号{stepTrace.Id},当前工序：从[{oldStatus}]更新至[{StepTraceStatus.异常结束.GetIndexString()}]成功", userCode, LogLevel.Success);
                        return BllResultFactory.Error($"异常结束任务{stepTrace.Id}失败：{result.Msg}");
                    }
                    else
                    {
                        transaction.Commit();
                        AppSession.LogService.LogContent(LogTitle.任务状态维护, $"任务号{stepTrace.Id},当前工序：从[{oldStatus}]更新至[{StepTraceStatus.异常结束.GetIndexString()}]失败，原因：{result.Msg}", userCode, LogLevel.Success);
                        return BllResultFactory.Sucess($"异常结束任务{stepTrace.Id}成功");
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    return BllResultFactory.Error($"异常结束任务{stepTrace.Id}出现异常：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 更新库位表数据源
        /// </summary>
        /// <param name="stepTrace"></param>
        /// <param name="stations"></param>
        /// <returns></returns>
        public BllResult updateLocation(StepTrace stepTrace, List<Station> stations)
        {
            try
            {
                //var temp = stations.Where(t => t.Code.Contains("StationForFinished")).ToList();
                //if (temp.Count == 0 || temp == null)
                //{
                //    return BllResultFactory.Error("没有查询到相对应的成品下线站台数据");
                //}
                var station = stations.Find(t => t.Id == stepTrace.NextStationId);
                if (station != null)
                {
                    var locationResult = AppSession.Dal.GetCommonModelByCondition<Location>($"where srmCode = '{station.Code}'");
                    if (locationResult.Success == false)
                    {
                        return BllResultFactory.Error($"根据{station.Code}查询库位数据失败");
                    }
                    if (locationResult.Data.Count != 1)
                    {
                        return BllResultFactory.Error($"根据{station.Code}查询库位数据不是1条，请核对库位基础数据");
                    }
                    var location = locationResult.Data[0];

                    if (location.Line < 1 || location.Layer < 1)
                    {
                        location.Row = 1;
                        location.Line = 1;
                        location.Layer = 1;
                        return AppSession.Dal.UpdateCommonModel<Location>(location);
                    }
                    if (location.Line == 1 && location.Layer == 1)
                    {
                        return BllResultFactory.Sucess();
                    }
                    if (location.Line == 1)
                    {
                        location.Line = 4;
                        location.Layer = (short)(location.Layer - 1);
                        return AppSession.Dal.UpdateCommonModel<Location>(location);
                    }
                    if (location.Line > 1)
                    {
                        location.Line = (short)(location.Line - 1);
                        return AppSession.Dal.UpdateCommonModel<Location>(location);
                    }
                    return BllResultFactory.Error("更新下线站台的列层失败，存在程序未处理的情况。");
                    //string sql_1 = $"update wcslocation set WCSLayer = @WCSLayer,WCSLine=@WCSLine where id = @id ";
                    //string sql_2 = $"update wcslocation set WCSLine=@WCSLine where id = @id ";
                    ////当前列数减一，发生异常情况实际未送过去的，
                    //int? newLine = location.Line - 1;
                    //if (newLine == 0)
                    //{
                    //    var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql_1, new { WCSLine = 1, WCSLayer = 1, id = location.Id });
                    //    if (!result.Success)
                    //    {
                    //        return BllResultFactory.Error($"更新库位表失败{location.Id}");
                    //    }
                    //}
                    //else
                    //{
                    //    var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql_2, new { WCSLine = newLine, id = location.Id });
                    //    if (!result.Success)
                    //    {
                    //        return BllResultFactory.Error($"更新库位表失败{location.Id}");
                    //    }

                    //}
                }
                return BllResultFactory.Error($"更新下线站台的列层失败，因为没有找到ID[{stepTrace.NextStationId}]对应的站台");
            }
            catch (Exception)
            {
                return BllResultFactory.Error();
            }
        }

        public BllResult StepTraceTaskStatus(int taskId, int status)
        {
            var taskResult = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id = {taskId}");
            if (!taskResult.Success)
            {
                return BllResultFactory.Error(taskResult.Msg);
            }
            var task = taskResult.Data[0];
            if (task.Status >= (int)StepTraceStatus.任务完成)
            {
                return BllResultFactory.Error("任务流程已经完成");
            }
            string sql = $"update step_trace set status  = {status}  where id = {taskId}";
            var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
            if (result.Success)
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error(result.Msg);
            }
        }


        public BllResult UpdateTrussRowIndex(int? trussId)
        {
            if (trussId < 0)
            {
                return BllResultFactory.Error("id号有任务");
            }
            var trussResult = AppSession.Dal.GetCommonModelByCondition<Equipment>($"where id = {trussId}");
            if (!trussResult.Success)
            {
                return BllResultFactory.Error(trussResult.Msg);
            }
            var truss = trussResult.Data[0];
            int rowIndex1 = 0;
            int rowIndex2 = 0;
            string sql = $"update equipment set rowIndex1 = {rowIndex1} , rowIndex2 = {rowIndex2}   where id = {trussId}";
            var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
            if (result.Success)
            {
                return BllResultFactory.Sucess();
            }
            else
            {
                return BllResultFactory.Error(result.Msg);
            }

        }
    }
}
