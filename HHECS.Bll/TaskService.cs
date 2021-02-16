using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using HHECS.DAL;
using HHECS.Model.ApiModel;
using HHECS.Model.ApiModel.HHWMSApiModel;
using HHECS.Model.ApiModel.WCSApiModel;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;
using Newtonsoft.Json;

namespace HHECS.Bll
{
    public class TaskService : BaseService
    {
        #region 行车任务维护

        /// <summary>
        /// 任务 层
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newWCSLayer"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult ChangeTaskWCSLayer(int? id, string newWCSLayer, string userCode)
        {
            try
            {
                var temp = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id={id}");
                if (!temp.Success)
                {
                    return BllResultFactory.Error($"根据{id}查询任务失败!");
                }
                var a = AppSession.Dal.GetCommonModelByCondition<Step>("");
                var types = a.Data.Where(v => v.StationType.Contains("FinishedType")).ToList();
                var step = types.Find(t => t.Id == temp.Data[0].NextStepId);
                if (step == null)
                {
                    return BllResultFactory.Error($"根据{id},不是工件下线工序!");
                }
                string sql = $"UPDATE step_trace SET wcsLayer ='{newWCSLayer}' WHERE id='{id}'";
                var res = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                if (res.Success)
                {
                    AppSession.LogService.LogContent(LogTitle.成品下线层维护, $"任务号{id},当前工序：老{temp.Data[0].WCSLayer}更新至{newWCSLayer}失败", userCode, LogLevel.Error);
                    return BllResultFactory.Sucess();
                }
                else
                {
                    AppSession.LogService.LogContent(LogTitle.成品下线层维护, $"任务号{id},当前工序：老{temp.Data[0].WCSLayer}更新至{newWCSLayer}失败", userCode, LogLevel.Error);
                    return BllResultFactory.Error($"更新失败任务：{id};{res.Msg}");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }
        /// <summary>
        /// 修改任务列层
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newWCSLine"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult ChangeTaskWCSLineOrWcsLayer(int? id, string newWCSLine, string newWCSLayer, string userCode)
        {
            try
            {
                var temp = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id={id}");
                if (!temp.Success)
                {
                    return BllResultFactory.Error($"根据{id}查询任务失败!");
                }
                var stepResult = AppSession.Dal.GetCommonModelByCondition<Step>($"where id = '{temp.Data[0].NextStepId}' and stepType = 'FinishedType'");
                if (!stepResult.Success)
                {
                    return BllResultFactory.Error($"根据工序id[{id}]找到的工序,不是工件下线!");
                }
                var stationResult = AppSession.Dal.GetCommonModelByCondition<Station>($"where id = '{temp.Data[0].NextStationId}'");
                if (!stationResult.Success)
                {
                    return BllResultFactory.Error($"根据下个工位id[{temp.Data[0].NextStationId}]未找到对应的站台!");
                }
                var countResult = AppSession.Dal.GetCommonModelCount<Location>($"WHERE srmCode='{stationResult.Data[0].Code}'");
                if (countResult.Data != 1)
                {
                    return BllResultFactory.Error($"根据下个工位编码[{stationResult.Data[0].Code}]查询出来的库位不是1个!");
                }
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction transaction = null;
                    try
                    {
                        if (int.TryParse(newWCSLine, out int line) == false)
                        {
                            return BllResultFactory.Error($"列[{newWCSLine}]不能转化为数字!");
                        }
                        if (int.TryParse(newWCSLayer, out int layer) == false)
                        {
                            return BllResultFactory.Error($"层[{newWCSLayer}]不能转化为数字!");
                        }
                        if (line < 1 || layer < 1 || line > 4 || layer > 2)
                        {
                            return BllResultFactory.Error($"层必须在1-2之间，列必须在1-4之间!");
                        }
                        if (line < 4)
                        {
                            line = line + 1;
                        }
                        else
                        {
                            line = 1;
                            layer = layer + 1;
                        }
                        connection.Open();
                        transaction = connection.BeginTransaction();
                        string sql = $"UPDATE location SET line ='{line}',layer='{layer}' WHERE srmCode='{stationResult.Data[0].Code}'";
                        var res1 = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql, connection, transaction);
                        sql = $"UPDATE step_trace SET WCSLine ='{newWCSLine}',WCSLayer='{newWCSLayer}' WHERE id='{id}'";
                        var res2 = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql, connection, transaction);
                        if (res1.Success && res2.Success)
                        {
                            transaction.Commit();
                            AppSession.LogService.LogContent(LogTitle.成品下线维护, $"任务号{id},层：老{temp.Data[0].WCSLayer}更新至{newWCSLayer}和列： 老{temp.Data[0].WCSLine}更新至{newWCSLine}成功", userCode, LogLevel.Error);
                            return BllResultFactory.Sucess();
                        }
                        else
                        {
                            transaction.Rollback();
                            AppSession.LogService.LogContent(LogTitle.成品下线维护, $"任务号{id},层：老{temp.Data[0].WCSLayer}更新至{newWCSLayer}和列：老{temp.Data[0].WCSLine}更新至{newWCSLine}失败", userCode, LogLevel.Error);
                            return BllResultFactory.Error($"更新失败任务：任务id[{id}]，原因：{res1.Msg} {res2.Msg}");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction?.Rollback();
                        return BllResultFactory.Error($"任务{id}修改列层出现异常：{ex.ToString()}");
                    }
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(ex.Message);
            }
        }

        /// <summary>
        /// 任務狀態維護
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult ChangStepStatus(int id, int newStatus, int oldStatus, string userCode)
        {
            var stepTraceResult = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id = {id}");
            if (stepTraceResult.Success)
            {
                var stepTrace = stepTraceResult.Data[0];
                if (stepTrace.Status >= (int)StepTraceStatus.任务完成)
                {
                    return BllResultFactory.Error($"已完成任务无法进行状态维护");
                }
                else
                {
                    BllResult bllResult;
                    if (newStatus == StepTraceStatus.响应放货完成.GetIndexInt() && stepTrace.NextStationId > 0)
                    {
                        bllResult = trussPutOver(stepTrace, userCode);
                    }
                    else if (newStatus == StepTraceStatus.异常结束.GetIndexInt())
                    {
                        //工位表
                        var stationsResult = AppSession.Dal.GetCommonModelByCondition<Station>("");
                        if (!stationsResult.Success)
                        {
                            return BllResultFactory.Error($"修改任务状态失败，查询工位表出错，原因：{stationsResult.Msg}");
                        }
                        //工序表
                        var stepsResult = AppSession.Dal.GetCommonModelByCondition<Step>("");
                        if (!stepsResult.Success)
                        {
                            return BllResultFactory.Error($"修改任务状态失败，查询工序表出错，原因：{stepsResult.Msg}");
                        }
                        bllResult = AppSession.StepTraceService.StepTraceExceptionOver(stepTrace, userCode, stationsResult.Data, stepsResult.Data);
                    }
                    else
                    {
                        stepTrace.Status = newStatus;
                        bllResult = AppSession.Dal.UpdateCommonModel<StepTrace>(stepTrace);
                    }
                    if (bllResult.Success)
                    {
                        AppSession.LogService.LogContent(LogTitle.任务状态维护, $"任务号{id},当前工序：老{oldStatus}更新至{newStatus}成功", userCode, LogLevel.Success);
                        return BllResultFactory.Sucess("更改状态成功");
                    }
                    else
                    {
                        AppSession.LogService.LogContent(LogTitle.任务状态维护, $"任务号{id},当前工序：老{oldStatus}更新至{newStatus}失败", userCode, LogLevel.Error);
                        return BllResultFactory.Error($"更改状态失败：{bllResult.Msg}");
                    }
                }
            }
            else
            {
                return BllResultFactory.Error($"未能获取到任务：{id},请核对任务数据");
            }
        }


        /// <summary>
        /// 手动 响应放货完成
        /// </summary>
        /// <param name="stepTrace"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult trussPutOver(StepTrace stepTrace, string userCode)
        {
            var nextStationResult = AppSession.Dal.GetCommonModelByCondition<Station>($"Where id = {stepTrace.NextStationId}");
            if (!nextStationResult.Success)
            {
                Logger.Log($"手动修改任务ID:[{stepTrace.Id}]状态为[{StepTraceStatus.响应放货完成}]失败，原因：从站台基础数据中找不到id为[{stepTrace.NextStationId}]的站台", LogLevel.Error);
                return BllResultFactory.Error();
            }
            var nextStationCode = nextStationResult.Data[0].Code;
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
            stepTraceLog.SrmCode = stepTrace.SrmCode;
            stepTraceLog.PassOrFail = "";
            stepTraceLog.IsNG = stepTrace.IsNG;
            stepTraceLog.NGcode = stepTrace.NGcode;
            stepTraceLog.StationInTime = DateTime.Now;
            //stepTraceLog.StationOutTime = stepTrace.StationOutTime;
            stepTraceLog.LineInTime = stepTrace.LineInTime;
            //stepTraceLog.LineOutTime = DateTime.Now;
            stepTraceLog.CreateBy = userCode;
            stepTraceLog.CreateTime = DateTime.Now;

            //更新任务状态，如果是下线站台，就整个工件任务就完成了
            if (nextStationCode.Contains("StationForFinished"))
            {
                stepTrace.Status = StepTraceStatus.任务完成.GetIndexInt();
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
            //stepTrace.LineInTime = DateTime.Now;
            //stepTrace.StationOutTime = null;
            stepTrace.UpdateBy = userCode;
            stepTrace.UpdateTime = DateTime.Now;

            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    connection.Update<StepTrace>(stepTrace, transaction: tran);
                    connection.Insert<StepTraceLog>(stepTraceLog, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    Logger.Log($"手动修改任务ID:[{stepTrace.Id}]状态为[{StepTraceStatus.响应放货完成}] 发生异常，任务:{stepTrace.Id}，原因：{ex.Message}", LogLevel.Exception, ex);
                    return BllResultFactory.Error();
                }
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 当前工序维护
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldStepId">老工序，用于记录数据</param>
        /// <param name="newStepId">新工序id,用于更新数据</param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult ChangeTaskStepId(int? id, string oldStepId, string newStepId, string userCode)
        {
            try
            {
                string sql = $"UPDATE step_trace SET stepId ='{newStepId}' WHERE id='{id}'";
                var taskResult = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"Where id={id}");
                if (!taskResult.Success)
                {
                    return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
                }
                var tempTask = taskResult.Data[0];
                var res = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                if (res.Success)
                {
                    AppSession.LogService.LogContent(LogTitle.当前工序维护, $"任务号{id},当前工序：老{oldStepId}更新至{newStepId}失败", userCode, LogLevel.Error);
                    return BllResultFactory.Sucess();
                }
                else
                {
                    AppSession.LogService.LogContent(LogTitle.当前工序维护, $"任务号{id},当前工序：老{oldStepId}更新至{newStepId}失败", userCode, LogLevel.Error);
                    return BllResultFactory.Error($"更新失败任务：{id};{taskResult.Msg}");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"更改工序异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 下道工序
        /// </summary>
        /// <param name="id">任务号</param>
        /// <param name="nextStepId">下道工序</param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult ChangeTaskNextStationId(int? id, string nextStepId, string userCode)
        {
            try
            {
                string sql = $"UPDATE step_trace SET nextStepId ='{nextStepId}' WHERE id ='{id}'";
                var taskResult = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id = {id}");
                if (!taskResult.Success)
                {
                    return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
                }
                var tempTask = taskResult.Data[0];
                var res = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                if (!res.Success)
                {
                    AppSession.LogService.LogContent(LogTitle.下道工序维护, $"任务号{id},下道工位{tempTask.NextStepId}清零{nextStepId}失败", userCode, LogLevel.Error);
                    return BllResultFactory.Error($"更新失败任务：{id};{taskResult.Msg}");
                }
                else
                {
                    AppSession.LogService.LogContent(LogTitle.下道工序维护, $"任务号{id},下道工位{tempTask.NextStepId}清零{nextStepId}成功", userCode, LogLevel.Success);
                    return BllResultFactory.Sucess();
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"清零下道工序异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 下道工位
        /// 更新值 nextStepId 零；或者其他
        /// </summary>
        /// <param name="id">任务号</param>
        /// <param name="nextStationId">下道工位</param>
        /// <param name="userCode">操作者</param>
        /// <returns></returns>
        public BllResult ChangeTaskNextStepId(int? id, string nextStationId, string userCode)
        {
            try
            {
                string sql = $"UPDATE step_trace SET nextStationId ='{nextStationId}' WHERE id ='{id}'";
                var taskResult = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id = {id}");
                if (!taskResult.Success)
                {
                    return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
                }
                var tempTask = taskResult.Data[0];
                var res = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                if (!res.Success)
                {
                    AppSession.LogService.LogContent(LogTitle.下道工位维护, $"任务号{id},下道工位{tempTask.NextStationId}清零{nextStationId}失败", userCode, LogLevel.Error);
                    return BllResultFactory.Error($"更新失败任务：{id};{taskResult.Msg}");
                }
                else
                {
                    AppSession.LogService.LogContent(LogTitle.下道工位维护, $"任务号{id},下道工位{tempTask.NextStationId}清零{nextStationId}成功", userCode, LogLevel.Success);
                    return BllResultFactory.Sucess();
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"清零下道工序异常:{ex.Message}");
            }
        }
        #endregion

        #region Task

        /// <summary>
        ///更改任务当前设备维护 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gateway"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult ChangeTaskGeatWay(int id, string gateway, string userCode)
        {
            try
            {
                var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {id}");
                if (!taskResult.Success)
                {
                    return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
                }
                else
                {
                    var tempTask = taskResult.Data[0];
                    string sql = $"update wcstask set Gateway ='{gateway}' where id ={id}";
                    var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                    if (result.Success)
                    {
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更新任务阶段：任务号{id},原任务阶段{tempTask.Gateway}改为{gateway}成功", userCode, LogLevel.Success);
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更新任务阶段：任务号{id},原任务阶段{tempTask.Gateway}改为{gateway}失败", userCode, LogLevel.Error);
                        return BllResultFactory.Error($"更新任务{id}阶段失败：{result.Msg}");
                    }
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更新任务阶段异常：{ex.ToString()}", Accounts.WCS.ToString(), LogLevel.Exception);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 批量生成任务  一旦出现错误直接全部回滚
        /// </summary>
        /// <param name="createModels"></param>
        /// <returns></returns>
        public BllResult CreateTasks(List<TaskCreateModel> taskCreateModels)
        {
            if (taskCreateModels == null || taskCreateModels.Count == 0)
            {
                return BllResultFactory.Error("无数据，请重新提交");
            }
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction trans = null;
                try
                {
                    connection.Open();
                    trans = connection.BeginTransaction();
                    foreach (TaskCreateModel model in taskCreateModels)
                    {
                        var result = CreateTask(connection, trans, model);
                        if (result.Success)
                        {
                            continue;
                        }
                        else
                        {
                            trans.Rollback();
                            return BllResultFactory.Error(result.Msg);
                        }
                    }
                    trans.Commit();
                    return BllResultFactory.Sucess();
                }
                catch (Exception ex)
                {
                    trans?.Rollback();
                    return BllResultFactory.Error($"创建任务的时候出现异常{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 任务生成
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BllResult CreateTask(TaskCreateModel model)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction trans = null;
                try
                {
                    connection.Open();
                    trans = connection.BeginTransaction();
                    var result = CreateTask(connection, trans, model);
                    if (result.Success)
                    {
                        trans.Commit();
                        return BllResultFactory.Sucess(result.Msg);
                    }
                    else
                    {
                        trans.Rollback();
                        return BllResultFactory.Error(result.Msg);
                    }
                }
                catch (Exception ex)
                {
                    trans?.Rollback();
                    return BllResultFactory.Error($"创建任务的时候出现异常{ex.Message}");
                }
            }
        }


        /// <summary>
        /// 任务取消  任务取消必须是在任务未执行的情况下允许取消。任务已经执行只能通过冲销进行处理。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BllResult TaskCancels(TaskCancelModel model)
        {
            if (model == null || model.TaskNos.Count == 0)
            {
                return BllResultFactory.Error("未传递数据");
            }
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction trans = null;
                string message = "";
                string title = "TaskCancel";
                try
                {
                    connection.Open();
                    trans = connection.BeginTransaction();
                    foreach (string taskNo in model.TaskNos)
                    {
                        //step2: 判断模型中必要数据是否具备
                        var result = DeleteTaskByTaskNo(connection, trans, taskNo);
                        if (result.Success)
                        {
                            continue;
                        }
                        trans.Rollback();
                        return BllResultFactory.Error(result.Msg);
                    }
                    trans.Commit();
                    return BllResultFactory.Sucess();
                }
                catch (Exception ex)
                {
                    trans?.Rollback();
                    message = $" 取消任务的时候出现异常{ex.Message}";
                    AppSession.LogService.WriteLog(title, message);
                    return BllResultFactory.Error(message);
                }
            }

        }

        /// <summary>
        /// 任务取消  任务取消必须是在任务未执行的情况下允许取消。任务已经执行只能通过冲销进行处理。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BllResult TaskCancel(string taskNo)
        {
            //step1: 判断是否符合要求   
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction trans = null;
                //string title = "taskCancel";
                string message = "";
                try
                {
                    connection.Open();
                    trans = connection.BeginTransaction();
                    var result = DeleteTaskByTaskNo(connection, trans, taskNo);
                    if (result.Success)
                    {
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    trans?.Rollback();
                    message = $"取消异常：{ex.Message}";
                    AppSession.LogService.LogContent(LogTitle.任务数据处理, ex.ToString(), "WCS", LogLevel.Exception);
                    return BllResultFactory.Error(message);
                }
            }
        }

        /// <summary>
        /// 更改前置远程任务号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="preRemoteTaskNo"></param>
        /// <returns></returns>
        public BllResult ChangeTaskPreRemoteTaskNo(int? id, string preRemoteTaskNo, string userCode)
        {

            try
            {
                var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {id}");
                if (!taskResult.Success)
                {
                    return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
                }
                else
                {
                    var tempTask = taskResult.Data[0];
                    if (string.IsNullOrWhiteSpace(preRemoteTaskNo) || preRemoteTaskNo == "0")
                    {
                        //对前置任务进行清空
                        string sql = $"update wcstask set preRemoteTaskNo = '' ,preTaskId =0 where id = {id}";
                        var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                        if (result.Success)
                        {

                            AppSession.LogService.LogContent(LogTitle.任务维护操作, $"清空前置任务：任务号{id},原前置{tempTask.RemoteTaskNo}改为{preRemoteTaskNo}成功", userCode, LogLevel.Success);
                            return BllResultFactory.Sucess();
                        }
                        else
                        {

                            AppSession.LogService.LogContent(LogTitle.任务维护操作, $"清空前置任务：任务号{id},原前置{tempTask.RemoteTaskNo}改为{preRemoteTaskNo}失败：失败原因{result.Msg}", userCode, LogLevel.Success);
                            return BllResultFactory.Error(result.Msg);
                        }
                    }
                    else
                    {
                        //判断这个前置任务号是否存在
                        taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where remoteTaskNo = '{preRemoteTaskNo}'");
                        if (!taskResult.Success)
                        {
                            return BllResultFactory.Error($"前置任务号{preRemoteTaskNo}在当前任务列表中不存在，信息：{taskResult.Msg}");
                        }
                        else
                        {
                            //存在则获取这个前置任务
                            var temptask1 = taskResult.Data[0];
                            string sql = $"update wcstask set preRemoteTaskNo = '{preRemoteTaskNo}' ,preTaskId ={temptask1.Id} where id = {id}";
                            var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                            if (result.Success)
                            {
                                AppSession.LogService.LogContent(LogTitle.任务维护操作, $"修改前置任务：任务号{id},原前置{tempTask.RemoteTaskNo}改为{preRemoteTaskNo}成功", userCode, LogLevel.Success);
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                AppSession.LogService.LogContent(LogTitle.任务维护操作, $"修改前置任务：任务号{id},原前置{tempTask.RemoteTaskNo}改为{preRemoteTaskNo}失败：失败原因{result.Msg}", userCode, LogLevel.Success);
                                return BllResultFactory.Error(result.Msg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.LogContent(LogTitle.任务维护操作, $"修改前置任务异常{ex.ToString()}", Accounts.WCS.ToString(), LogLevel.Exception);
                return BllResultFactory.Error();
            }

        }

        /// <summary>
        /// 更改源库位或目标库位
        /// </summary>
        /// <param name="value"></param>
        /// <param name="locaiotnCode"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public BllResult ChangeTaskLocationCode(int id, string warehouseCode, string fromLocationCode, string toLocationCode, string userCode)
        {
            try
            {
                var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {id}");
                if (!taskResult.Success)
                {
                    return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
                }
                else
                {
                    var tempTask = taskResult.Data[0];
                    if (!string.IsNullOrWhiteSpace(fromLocationCode))
                    {
                        var result = CheckLocationInner(warehouseCode, fromLocationCode);
                        if (!result.Success)
                        {
                            return result;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(toLocationCode))
                    {
                        var result = CheckLocationInner(warehouseCode, toLocationCode);
                        if (!result.Success)
                        {
                            return result;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(fromLocationCode))
                    {
                        var taskFrom = updateFromLocationCode(id, fromLocationCode);
                        if (!taskFrom.Success)
                        {
                            return BllResultFactory.Error($"解锁库位表失败:{taskFrom.Msg}");
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(toLocationCode))
                    {
                        var taskid = updateToLocationCode(id, toLocationCode);
                        if (!taskid.Success)
                        {
                            return BllResultFactory.Error($"解锁库位表失败:{taskid.Msg}");
                        }
                    }


                    if (!string.IsNullOrWhiteSpace(fromLocationCode))
                    {
                        string sql = $"update wcstask set fromLocationCode = '{fromLocationCode}' where id = {id}";
                        var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                        if (!result.Success)
                        {
                            AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改源库位：任务号{id},原源库位{tempTask.FromLocationCode}改为{fromLocationCode}失败：失败原因{result.Msg}", userCode, LogLevel.Error);
                            return BllResultFactory.Error($"更改源库位失败：{result.Msg}");
                        }
                        else
                        {
                            AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改源库位：任务号{id},原源库位{tempTask.FromLocationCode}改为{fromLocationCode}成功", userCode, LogLevel.Success);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(toLocationCode))
                    {
                        string sql = $"update wcstask set toLocationCode = '{toLocationCode}' where id = {id}";
                        var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                        if (!result.Success)
                        {
                            AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改目标库位：任务号{id},原源库位{tempTask.ToLocationCode}改为{toLocationCode}失败：失败原因{result.Msg}", userCode, LogLevel.Error);
                            return BllResultFactory.Error($"更改目标库位失败：{result.Msg}");
                        }
                        else
                        {
                            AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改目标库位：任务号{id},原源库位{tempTask.ToLocationCode}改为{toLocationCode}成功", userCode, LogLevel.Success);
                        }
                    }
                    return BllResultFactory.Sucess();
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改库位异常：{ex.ToString()}", Accounts.WCS.ToString(), LogLevel.Exception);
                return BllResultFactory.Error();
            }
        }

        /// <summary>
        /// 更改源库位数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fromLocationCode"></param>
        /// <returns></returns>
        public BllResult updateFromLocationCode(int id, string fromLocationCode)
        {
            try
            {
                var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {id}");
                if (!taskResult.Success)
                {
                    return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
                }
                var temptask = taskResult.Data[0];
                var fromlocation = AppSession.Dal.GetCommonModelByCondition<Location>($"where code ='{temptask.FromLocation}'");
                if (!fromlocation.Success)
                {
                    return BllResultFactory.Error($"查询原源库位数据失败,{fromlocation.Msg}");
                }
                var temp_Rawfromlocation = fromlocation.Data[0];
                var locationResult = AppSession.Dal.GetCommonModelByCondition<Location>($"where Code='{fromLocationCode}'");
                if (!locationResult.Success)
                {
                    return BllResultFactory.Error($"查询源库位数据失败,{locationResult.Msg}");
                }
                var temp_NewLocation = locationResult.Data[0];
                if (temp_Rawfromlocation.ContainerCode != null)
                {
                    string sql = $"update location set isLock = {LocationLockStatus.空闲},containerCode='{null}' where code='{temptask.FromLocationCode}'";
                    var p = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                    if (!p.Success)
                    {
                        return BllResultFactory.Error($"更新失败");
                    }
                }
                else
                {
                    return BllResultFactory.Error($"原源库位容器编码为空");
                }
                if (temp_NewLocation.ContainerCode == null)
                {
                    string sql1 = $"update location set isLock = {LocationLockStatus.工作},containerCode='{temptask.ContainerCode}' where code='{fromLocationCode}'";
                    var a = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql1);
                    if (!a.Success)
                    {
                        return BllResultFactory.Error($"更新失败");
                    }
                }
                else
                {
                    return BllResultFactory.Error($"新源库位已有容器编码");
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"捕获异常：{ex.ToString()}");
            }

        }

        /// <summary>
        /// 更改目标库位
        /// 根据id，查询全部数据 
        /// 对库位表进行操作。
        /// </summary>
        /// <param name="id">任务id</param>
        /// <param name="toLocationCode">修改后的目标库位数据</param>
        /// <returns></returns>
        public BllResult updateToLocationCode(int id, string toLocationCode)
        {
            try
            {
                var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {id}");
                if (!taskResult.Success)
                {
                    return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
                }
                var temptask = taskResult.Data[0];
                //查询原目标库位数据
                var RawToLocation = AppSession.Dal.GetCommonModelByCondition<Location>($"where code ='{temptask.ToLocationCode}'").Data[0];

                //查询修改目标库位数据
                var AimlocationResult = AppSession.Dal.GetCommonModelByCondition<Location>($"where Code='{toLocationCode}'");
                if (!AimlocationResult.Success)
                {
                    return BllResultFactory.Error($"查询库位数据失败");
                }
                var temp_UpdateAimLocation = AimlocationResult.Data[0];
                //库表非占用，容器编码一致
                //入:任务，源库位有数据。
                //移库:源库位有数据，目标库位无数据。需删除源数据。然后目标库位加入数据。给源库位更改数据条件（状态占用，ContainerCode相等）。目标库位（ContainerCode为NULL）
                if (RawToLocation.IsLock == (short)LocationLockStatus.工作)
                {
                    if (temptask.ContainerCode == RawToLocation.ContainerCode)
                    {
                        //原有库位数据改空闲，库位为空。
                        string sqlContainer = $"update location set isLock ='{(int)LocationLockStatus.空闲}',containerCode='{null}' where code='{temptask.ToLocationCode}'";
                        var resSqlContainer = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sqlContainer);
                        if (!resSqlContainer.Success)
                        {
                            return BllResultFactory.Error($"解锁原库位数据失败:{resSqlContainer.Msg}");
                        }
                    }
                    else
                    {
                        return BllResultFactory.Error($"库位编码与任务容器编码不相等");
                    }
                }
                else
                {
                    return BllResultFactory.Error($"原目标库位非占用，请检查数据！");
                }
                if (temp_UpdateAimLocation.ContainerCode == null)
                {
                    if (temp_UpdateAimLocation.IsLock > (int)LocationLockStatus.空闲)
                    {
                        return BllResultFactory.Error($"更改库位{temp_UpdateAimLocation}非空闲");
                    }
                    //新的库位是占用,并且写入容器编号
                    string sqlLocation = $"update location set isLock ='{LocationLockStatus.工作}',containerCode='{temptask.ContainerCode}' where code='{toLocationCode}'";
                    var resSqlLocation = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sqlLocation);
                    if (!resSqlLocation.Success)
                    {
                        return BllResultFactory.Error($"更改库位表失败：{resSqlLocation.Msg}");
                    }
                }
                else
                {
                    return BllResultFactory.Error($"录入目标库位失败,原因当前所在位置已有容器编号");
                }
                return BllResultFactory.Sucess();
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"捕获异常:{ex.ToString()}");
            }

        }

        /// <summary>
        /// 更改入口
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromPort"></param>
        /// <param name="toPort"></param>
        /// <returns></returns>
        public BllResult ChangeTaskPort(int id, string fromPort, string toPort, string userCode)
        {
            var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {id}");
            if (!taskResult.Success)
            {
                return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
            }
            else
            {
                var tempTask = taskResult.Data[0];
                string sql = "";
                if (!string.IsNullOrWhiteSpace(fromPort))
                {
                    sql = $"update wcstask set fromPort = '{fromPort}' where id = {id}";
                    var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                    if (!result.Success)
                    {
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改入口：任务号{id},原入库口{tempTask.FromPort}改为{fromPort}失败", userCode, LogLevel.Failure);
                        return BllResultFactory.Error($"更改任务入口失败：{result.Msg}");
                    }
                    else
                    {
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改入口：任务号{id},原入库口{tempTask.FromPort}改为{fromPort}成功", userCode, LogLevel.Success);
                    }
                }
                if (!string.IsNullOrWhiteSpace(toPort))
                {
                    sql = $"update wcstask set toPort = '{toPort}' where id = {id}";
                    var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                    if (!result.Success)
                    {
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改出口：任务号{id},原出库口{tempTask.ToPort}改为{toPort}失败", userCode, LogLevel.Failure);
                        return BllResultFactory.Error($"更改任务出口失败：{result.Msg}");
                    }
                    else
                    {
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改出口：任务号{id},原出库口{tempTask.ToPort}改为{toPort}成功", userCode, LogLevel.Success);
                    }
                }
                return BllResultFactory.Sucess();
            }
        }

        /// <summary>
        /// 任务阶段维护
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        public BllResult ChangeTaskStage(int? id, int stage, string userCode)
        {
            try
            {
                var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {id}");
                if (!taskResult.Success)
                {
                    return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
                }
                else
                {
                    var tempTask = taskResult.Data[0];
                    string sql = $"update wcstask set stage ={stage} where id ={id}";
                    var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                    if (result.Success)
                    {
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更新任务阶段：任务号{id},原任务阶段{tempTask.Stage}改为{stage}成功", userCode, LogLevel.Success);
                        return BllResultFactory.Sucess();
                    }
                    else
                    {
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更新任务阶段：任务号{id},原任务阶段{tempTask.Stage}改为{stage}失败", userCode, LogLevel.Error);
                        return BllResultFactory.Error($"更新任务{id}阶段失败：{result.Msg}");
                    }
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更新任务阶段异常：{ex.ToString()}", Accounts.WCS.ToString(), LogLevel.Exception);
                return BllResultFactory.Error();
            }

        }

        private BllResult CheckLocationInner(string warehouseCode, string locationCode)
        {
            //检查库位是否符合
            var result = AppSession.Dal.GetCommonModelByCondition<Location>($"where code = '{locationCode}'");
            if (!result.Success)
            {
                return BllResultFactory.Error($"库位未找到：{result.Msg}");
            }
            var fromLocation = result.Data[0];
            if (fromLocation.WarehouseCode != warehouseCode)
            {
                return BllResultFactory.Error($"更改库位{locationCode}不在当前仓库{warehouseCode}中");
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 更改远程任务号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remoteTaskNo"></param>
        /// <returns></returns>
        public BllResult ChangeTaskRemoteTaskNo(int id, string remoteTaskNo, string userCode)
        {
            var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {id}");
            if (!taskResult.Success)
            {
                return BllResultFactory.Error($"未找到任务：{id};{taskResult.Msg}");
            }
            else
            {
                var task = taskResult.Data[0];
                var oldRemoteTaskNo = task.RemoteTaskNo;
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction transaction = null;
                    try
                    {
                        connection.Open();
                        transaction = connection.BeginTransaction();
                        string sql = $"update wcstask set remotetaskno = '{remoteTaskNo}' where id = {id}";
                        connection.Execute(sql);
                        if (!string.IsNullOrWhiteSpace(oldRemoteTaskNo) && !string.IsNullOrWhiteSpace(remoteTaskNo))
                        {
                            string sql2 = $"update wcstask set preRemoteTaskNo ='{remoteTaskNo}' where preRemoteTaskNo = '{oldRemoteTaskNo}' and id = {id}";
                            connection.Execute(sql2);
                        }
                        else if (string.IsNullOrWhiteSpace(oldRemoteTaskNo))
                        {
                            //清空
                        }
                        else if (string.IsNullOrWhiteSpace(remoteTaskNo))
                        {
                            string sql3 = $"update wcstask set preRemoteTaskNo = '',preTaskId = 0 where preRemoteTaskNo = '{oldRemoteTaskNo}' and id = {id}";
                            connection.Execute(sql3);
                        }
                        transaction.Commit();
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改远程任务：任务号{id},原远程任务{task.RemoteTaskNo}改为{remoteTaskNo}成功", userCode, LogLevel.Success);
                        return BllResultFactory.Sucess();
                    }
                    catch (Exception ex)
                    {
                        transaction?.Rollback();
                        AppSession.LogService.LogContent(LogTitle.任务维护操作, $"更改远程任务号异常：{ex.Message}", userCode, LogLevel.Exception);
                        return BllResultFactory.Error($"更新远程任务号{remoteTaskNo}出现异常：{ex.Message}");
                    }
                }

            }
        }

        public BllResult DeleteTaskById(int id)
        {
            IDbConnection connection = null;
            IDbTransaction trans = null;
            try
            {
                using (connection = AppSession.Dal.GetConnection())
                {
                    var tmp = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where Id={id} and deleted=0");
                    if (tmp.Success)
                    {
                        connection.Open();
                        trans = connection.BeginTransaction();
                        if (tmp.Data[0].TaskStatus != (int)TaskEntityStatus.任务创建)
                        {
                            trans.Rollback();
                            return BllResultFactory.Error($"非创建状态任务不能删除");
                        }
                        var result = DeleteTaskByEntity(connection, trans, tmp.Data[0]);
                        if (result.Success)
                        {
                            trans.Commit();
                            return BllResultFactory.Sucess();
                        }
                        else
                        {
                            trans.Rollback();
                            return BllResultFactory.Error();
                        }
                    }
                    return BllResultFactory.Error(tmp.Msg);
                }
            }
            catch (Exception ex)
            {
                trans?.Rollback();
                return BllResultFactory.Error(ex.Message);
            }
        }

        /// <summary>
        /// 维护任务状态
        /// </summary>
        /// <param name="value"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult ChangeTaskStatus(int value, int status, string userCode)
        {
            var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {value}");
            if (taskResult.Success)
            {
                var task = taskResult.Data[0];
                if (task.TaskStatus >= (int)TaskEntityStatus.任务完成)
                {
                    return BllResultFactory.Error($"已完成任务无法进行状态维护");
                }
                else
                {
                    task.TaskStatus = status;
                    task.Updated = DateTime.Now;
                    task.UpdatedBy = userCode;
                    var result = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    if (result.Success)
                    {
                        return BllResultFactory.Sucess("更改状态成功");
                    }
                    else
                    {
                        return BllResultFactory.Error($"更改状态失败：{result.Msg}");
                    }
                }
            }
            else
            {
                return BllResultFactory.Error($"未能获取到任务：{value},请核对任务数据");
            }
        }

        /// <summary>
        /// 任务查询接口
        /// </summary>
        /// <param name="taskQueryModel"></param>
        /// <returns></returns>
        public BllResult<List<TaskQueryResponseModel>> TaskQuery(TaskQueryModel taskQueryModel)
        {
            string title = "taskQuery";
            if (taskQueryModel == null && taskQueryModel.TaskNos.Count == 0)
            {
                return BllResultFactory<List<TaskQueryResponseModel>>.Error("未传递查询数据");
            }
            List<TaskQueryResponseModel> taskResponseQueries = new List<TaskQueryResponseModel>();
            try
            {
                var dicTaskDesc = AppSession.BllService.GetDictWithDetails("TaskStatus").Success ? AppSession.BllService.GetDictWithDetails("TaskStatus").Data : null;
                foreach (string remoteTaskNo in taskQueryModel.TaskNos)
                {
                    var result = TaskQueryByTaskNo(remoteTaskNo);
                    if (result.Success)
                    {
                        TaskEntity task = result.Data;
                        TaskQueryResponseModel item = new TaskQueryResponseModel();
                        item.TaskNo = task.RemoteTaskNo;
                        item.TaskStatus = task.TaskStatus;
                        item.TaskStatusDesc = dicTaskDesc?.DictDetails?.FirstOrDefault(t => t.Code == task.TaskStatus.ToString())?.Name;
                        taskResponseQueries.Add(item);
                    }
                }
                return BllResultFactory<List<TaskQueryResponseModel>>.Sucess(taskResponseQueries);
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<TaskQueryResponseModel>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 任务查询接口
        /// </summary>
        /// <param name="taskQueryModel"></param>
        /// <returns></returns>
        public BllResult<TaskQueryResponseModel> TaskQuery(string taskNo)
        {
            string title = "taskQuery";
            string message = "";
            try
            {
                var result = TaskQueryByTaskNo(taskNo);
                if (result.Success)
                {
                    var dicTaskDesc = AppSession.BllService.GetDictWithDetails("TaskStatus").Success ? AppSession.BllService.GetDictWithDetails("TaskStatus").Data : null;
                    TaskEntity task = result.Data;
                    TaskQueryResponseModel item = new TaskQueryResponseModel();
                    item.TaskNo = task.RemoteTaskNo;
                    item.TaskStatus = task.TaskStatus;
                    item.TaskStatusDesc = dicTaskDesc.DictDetails.FirstOrDefault(t => t.Code == task.TaskStatus.ToString()).Name;
                    return BllResultFactory<TaskQueryResponseModel>.Sucess(item);
                }

                message = result.Msg;
                AppSession.LogService.WriteLog(title, message);
                return BllResultFactory<TaskQueryResponseModel>.Error(message);
            }
            catch (Exception ex)
            {
                return BllResultFactory<TaskQueryResponseModel>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 任务查询接口
        /// </summary>
        /// <param name="stationInfos"></param>
        /// <returns></returns>
        public BllResult<List<StationResponseInfoModel>> StationInfo(StationRequestInfoModel stationInfos)
        {
            string title = "taskQuery";
            string message = "";
            List<StationResponseInfoModel> infoModels = new List<StationResponseInfoModel> { };
            try
            {
                if (stationInfos != null && stationInfos.stations.Count > 0)
                {
                    foreach (string code in stationInfos.stations)
                    {
                        var result = StationInfoByCode(code);
                        if (result.Success)
                        {
                            infoModels.Add(result.Data);
                        }
                        return BllResultFactory<List<StationResponseInfoModel>>.Error(result.Msg);
                    }
                    return BllResultFactory<List<StationResponseInfoModel>>.Sucess(infoModels);
                }
                message = "数据不对，没有执行";
                AppSession.LogService.WriteLog(title, message);
                return BllResultFactory<List<StationResponseInfoModel>>.Error("数据不对！！");
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<StationResponseInfoModel>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 任务查询接口
        /// </summary>
        /// <param name="taskQueryModel"></param>
        /// <returns></returns>
        public BllResult<StationResponseInfoModel> StationInfo(string stationCode)
        {
            string title = "stationInfo";
            string message = "";
            try
            {
                var result = StationInfoByCode(stationCode);
                if (result.Success)
                {
                    return BllResultFactory<StationResponseInfoModel>.Sucess(result.Data);
                }
                message = result.Msg;
                AppSession.LogService.WriteLog(title, message);
                return BllResultFactory<StationResponseInfoModel>.Error(message);
            }
            catch (Exception ex)
            {
                return BllResultFactory<StationResponseInfoModel>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 执行任务确认
        /// </summary>
        /// <returns></returns>
        public void ExecuteTaskFeedback()
        {
            var dicUrls = AppSession.BllService.GetDictWithDetails(SysConst.RemoteUrls.ToString()).Success ? AppSession.BllService.GetDictWithDetails(SysConst.RemoteUrls.ToString()).Data : null;
            string url = dicUrls.DictDetails.FirstOrDefault(t => t.Code == "TaskComplete").Value;
            if (url == null)
            {
                AppSession.LogService.WriteLog("taskConfrimException", "url地址没有获取到，请检查数据配置");
                return;
            }
            //获取任务完成的需要回传的

            try
            {
                TaskFeedback(url, TaskEntityStatus.任务完成);
                TaskFeedback(url, TaskEntityStatus.异常结束);
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteLog("taskConfrimException", ex.Message);
            }
        }

        /// <summary>
        /// 根据任务，获取LED显示内容。
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public string GetTaskDetailsForLED(TaskEntity task)
        {
            string taskType = Enum.GetName(typeof(TaskType), task.TaskType);
            string locationCode = "";
            if (!String.IsNullOrEmpty(task.FromLocationCode))
            {
                locationCode += task.FromLocationCode;
            }
            if (!string.IsNullOrEmpty(task.ToLocationCode))
            {
                if (string.IsNullOrEmpty(locationCode))
                {
                    locationCode += task.ToLocationCode;
                }
                else
                {
                    locationCode += "->" + task.ToLocationCode;
                }
            }
            string text = String.Format("任务号：{0}\\n任务类型：{1}\\n托盘条码：{2}\\n仓位：{3}\\n", task.Id, taskType, task.ContainerCode, locationCode);
            //这里加上明细
            var taskDetails = AppSession.Dal.GetCommonModelByCondition<TaskDetailEntity>($"where taskId = {task.Id}");
            if (taskDetails.Success)
            {
                text += $"物料：数量 [重量]\\n";
                foreach (var item in taskDetails.Data)
                {
                    text += $"{item.MaterialName}：{item.Qty} [{item.Weight}]\\n";
                }
            }
            return text;
        }


        /// <summary>
        /// 任务下发  是针对出库任务进行的
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <param name="urls"></param>
        /// <returns></returns>
        public BllResult SendTaskToWCS(int id, string userCode)
        {
            var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {id}");
            if (!taskResult.Success)
            {
                return BllResultFactory.Error(taskResult.Msg);
            }
            else
            {
                var task = taskResult.Data[0];
                if (task.TaskStatus != (int)TaskEntityStatus.任务创建)
                {
                    return BllResultFactory.Error($"只有创建状态的任务才能下发");
                }
                else
                {
                    task.TaskStatus = (int)TaskEntityStatus.下发任务;
                    task.Updated = DateTime.Now;
                    task.UpdatedBy = userCode;
                    var result = AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                    if (result.Success)
                    {
                        return BllResultFactory.Sucess($"下发任务{id}成功");
                    }
                    else
                    {
                        return BllResultFactory.Error($"下发任务{id}失败：{result.Msg}");
                    }
                }
            }
        }

        /// <summary>
        /// 核对入库库位是否正确，并判断是否可以进行入库任务创建
        /// 条件：目标库位存在且空闲无托盘
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="locationCode"></param>
        /// <param name="warehouseCode"></param>
        /// <returns></returns>
        public BllResult<Location> CheckLocationForCreateTaskIn(IDbConnection connection, IDbTransaction transaction, String locationCode, string warehouseCode)
        {
            //校验库位
            var result = AppSession.Dal.GetCommonModelByCondition<Location>($"where code = '{locationCode}' and warehouseCode = '{warehouseCode}'", connection, transaction);
            if (!result.Success)
            {
                return BllResultFactory<Location>.Error(null, $"没有找到库位:{locationCode}");
            }
            var dr = result.Data.First();
            if (dr.IsLock != (int)LocationLockStatus.空闲)
            {
                return BllResultFactory<Location>.Error(null, $"目标库位{locationCode}非空闲");
            }
            if (!String.IsNullOrWhiteSpace(dr.ContainerCode?.ToString()))
            {
                return BllResultFactory<Location>.Error(null, $"目标库位{locationCode}有存放托盘，请不要重入");
            }
            return BllResultFactory<Location>.Sucess(dr);
        }

        /// <summary>
        /// 空出处理
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public BllResult EmptyOutHandle(TaskEntity task)
        {
            //List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>
            //    {
            //        new KeyValuePair<string, string>("taskId", task.Id.ToString())
            //    };
            //return AppSession.CommonService.FormPost(list, WMSUrls.HandleEmptyOut, client, urls);
            try
            {
                task.TaskStatus = (int)TaskEntityStatus.异常结束;
                task.IsEmptyOut = 1;
                return AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(null, "更新失败：" + ex.ToString());
            }
        }

        /// <summary>
        /// 重入处理
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="location"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public BllResult TaskDoubleInHandle(string flag, string location, int taskId)
        {
            try
            {
                string sql = $"update task set isDoubleIn = {flag},secondDestinationLocation = '{location}' WHERE id = {taskId}";
                var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                if (!result.Success)
                {
                    return BllResultFactory.Error(null, "更新失败");
                }
                else
                {
                    AppSession.LocationService.UpdateLocationStatus(location, AppSession.WarehouseCode, LocationLockStatus.工作);
                    return BllResultFactory.Sucess(null, "更新成功");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(null, "更新失败：" + ex.ToString());
            }
        }

        /// <summary>
        /// 实现的取货错误处理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BllResult TaskForkErrorHandle(TaskEntity task)
        {
            try
            {
                task.TaskStatus = (int)TaskEntityStatus.异常结束;
                task.IsForkError = 1;
                return AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(null, "更新失败：" + ex.ToString());
            }
        }

        /// <summary>
        /// 分配库位
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="height"></param>
        /// <param name="client"></param>
        /// <param name="urls"></param>
        /// <returns></returns>
        public BllResult<T> GetDestinationLocation<T>(int id, int height, int length, int weight, int width, HttpClient client, List<DictDetail> urls)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("taskId", id.ToString()),
                    new KeyValuePair<string, string>("lenth", length.ToString()),
                    new KeyValuePair<string, string>("width", width.ToString()),
                    new KeyValuePair<string, string>("height", height.ToString()),
                    new KeyValuePair<string, string>("weight", weight.ToString())
                };
            //这里用同步的方式，因为我们是在定时器线程中调用的，不影响UI线程
            return AppSession.CommonService.FormPost<T>(list, WMSUrls.GetLocation, client, urls);
        }

        /// <summary>
        /// 向上游系统请求分配库位
        /// hack:按需重新实现此方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="client"></param>
        /// <param name="urls"></param>
        /// <returns></returns>
        public BllResult<Location> GetDestinationLocationFromWMS(LocationAssignReqModelInfo info)
        {
            var dicUrls = AppSession.BllService.GetDictWithDetails(SysConst.RemoteUrls.ToString()).Success ? AppSession.BllService.GetDictWithDetails(SysConst.RemoteUrls.ToString()).Data : null;
            string url = dicUrls.DictDetails.FirstOrDefault(t => t.Code == "GetLocation").Value;
            if (url == null)
            {
                AppSession.LogService.WriteLog("GetLocation", "url地址没有获取到，请检查数据配置");
                return BllResultFactory<Location>.Error("url地址没有获取到，请检查数据配置");
            }
            var result = AppSession.CommonService.PostJson<LocationAssignRspModelInfo>("getLocationInfo", url, info).Result;
            if (!result.Success)
            {
                return BllResultFactory<Location>.Error($"请求上位系统去向库位出错:{result.Msg}");
            }
            else
            {
                //校验库位是否OK
                var toLocationCode = result.Data.ToLocationCode;
                var locationResult = AppSession.Dal.GetCommonModelByCondition<Location>($"where code = '{toLocationCode}'");
                if (locationResult.Success)
                {
                    return BllResultFactory<Location>.Sucess(locationResult.Data[0]);
                }
                else
                {
                    return BllResultFactory<Location>.Error($"获取到的库位{toLocationCode}不在系统管制范围内：{locationResult.Msg}");
                }
            }
        }

        /// <summary>
        /// 向上游系统请求分配目标区域
        /// hack:按需重新实现此方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="client"></param>
        /// <param name="urls"></param>
        /// <returns></returns>
        public BllResult<DestinationAreaResponseModel> GetDestinationAreaFromWMS(DestinationAreaRequestModel info)
        {
            var dicUrls = AppSession.BllService.GetDictWithDetails(SysConst.RemoteUrls.ToString()).Success ? AppSession.BllService.GetDictWithDetails(SysConst.RemoteUrls.ToString()).Data : null;
            string url = dicUrls.DictDetails.FirstOrDefault(t => t.Code == "GetDestinationArea").Value;
            if (url == null)
            {
                AppSession.LogService.WriteLog("GetLocation", "url地址没有获取到，请检查数据配置");
                return BllResultFactory<DestinationAreaResponseModel>.Error("url地址没有获取到，请检查数据配置");
            }
            return AppSession.CommonService.PostJson<DestinationAreaResponseModel>("GetDestinationArea", url, info).Result;
        }

        /// <summary>
        /// 本地任务完成
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult CompleteTask(int taskId, string userCode)
        {
            //将任务状态更改为完成，分任务类型进行完成
            var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {taskId}");
            if (!taskResult.Success)
            {
                return BllResultFactory.Error(taskResult.Msg);
            }
            var task = taskResult.Data[0];
            if (task.TaskStatus >= (int)TaskEntityStatus.任务完成)
            {
                return BllResultFactory.Error("任务已经完成");
            }
            task.TaskStatus = (int)TaskEntityStatus.任务完成;
            task.Updated = DateTime.Now;
            task.UpdatedBy = userCode;
            string sqlStauts = $"update location set isLock = @status where code = @code ";
            string sqlContainer = $"update location set containerCode = @containerCode where code = @code";
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    var result = AppSession.Dal.UpdateCommonModel<TaskEntity>(task, connection, transaction);
                    if (!result.Success)
                    {
                        transaction.Rollback();
                        return BllResultFactory.Error($"完成任务{task.Id}失败：{result.Msg}");
                    }
                    else
                    {
                        switch ((TaskType)task.TaskType)
                        {
                            case TaskType.整盘出库:
                            case TaskType.空容器出库:
                                //删除源库位托盘，更新源库位为empty
                                connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.FromLocationCode }, transaction);
                                connection.Execute(sqlContainer, new { containerCode = "", code = task.FromLocationCode }, transaction);
                                transaction.Commit();
                                return BllResultFactory.Sucess($"完成任务{task.Id}成功");

                            case TaskType.整盘入库:
                            case TaskType.空容器入库:
                                //更新托盘到目标库位，更新目标库位为empty
                                connection.Execute(sqlContainer, new { containerCode = task.ContainerCode, code = task.ToLocationCode }, transaction);
                                connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.ToLocationCode }, transaction);
                                transaction.Commit();
                                return BllResultFactory.Sucess($"完成任务{task.Id}成功");

                            case TaskType.补充入库:
                            case TaskType.分拣出库:
                            case TaskType.盘点:
                            case TaskType.出库查看:
                                //更新源库位状态为empty
                                connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.FromLocationCode }, transaction);
                                transaction.Commit();
                                return BllResultFactory.Sucess($"完成任务{task.Id}成功");
                            case TaskType.移库:
                                //更新托盘到目标库位，删除源库位托盘，更新两个库位为empty
                                connection.Execute(sqlContainer, new { containerCode = task.ContainerCode, code = task.ToLocationCode }, transaction);
                                connection.Execute(sqlContainer, new { containerCode = "", code = task.FromLocationCode }, transaction);
                                connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.ToLocationCode }, transaction);
                                connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.FromLocationCode }, transaction);
                                transaction.Commit();
                                return BllResultFactory.Sucess($"完成任务{task.Id}成功");
                            case TaskType.换站:
                            case TaskType.移位:
                                transaction.Commit();
                                return BllResultFactory.Sucess($"完成任务{task.Id}成功");
                            default:
                                transaction.Rollback();
                                return BllResultFactory.Error($"任务{task.Id}为未识别的任务类型");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    return BllResultFactory.Error($"任务{task.Id}完成出现异常：{ex.ToString()}");
                }
            }

        }


        /// <summary>
        /// 本地任务完成，请在外围控制事物提交
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult CompleteTask(int taskId, string userCode, IDbConnection connection, IDbTransaction transaction)
        {
            //将任务状态更改为完成，分任务类型进行完成
            var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where id = {taskId}");
            if (!taskResult.Success)
            {
                return BllResultFactory.Error(taskResult.Msg);
            }
            var task = taskResult.Data[0];
            if (task.TaskStatus >= (int)TaskEntityStatus.任务完成)
            {
                return BllResultFactory.Error("任务已经完成");
            }
            task.TaskStatus = (int)TaskEntityStatus.任务完成;
            string sqlStauts = $"update location set isLock = @status where code = @code ";
            string sqlContainer = $"update location set containerCode = @containerCode where code = @code";

            try
            {
                var result = AppSession.Dal.UpdateCommonModel<TaskEntity>(task, connection, transaction);
                if (!result.Success)
                {
                    transaction.Rollback();
                    return BllResultFactory.Error($"完成任务{task.Id}失败：{result.Msg}");
                }
                else
                {
                    switch ((TaskType)task.TaskType)
                    {
                        case TaskType.整盘出库:
                        case TaskType.空容器出库:
                            //删除源库位托盘，更新源库位为empty
                            connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.FromLocationCode }, transaction);
                            connection.Execute(sqlContainer, new { containerCode = "", code = task.FromLocationCode }, transaction);
                            transaction.Commit();
                            return BllResultFactory.Sucess($"完成任务{task.Id}成功");

                        case TaskType.整盘入库:
                        case TaskType.空容器入库:
                            //更新托盘到目标库位，更新目标库位为empty
                            connection.Execute(sqlContainer, new { containerCode = task.ContainerCode, code = task.ToLocationCode }, transaction);
                            connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.ToLocationCode }, transaction);
                            transaction.Commit();
                            return BllResultFactory.Sucess($"完成任务{task.Id}成功");

                        case TaskType.补充入库:
                        case TaskType.分拣出库:
                        case TaskType.盘点:
                        case TaskType.出库查看:
                            //更新源库位状态为empty
                            connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.FromLocationCode }, transaction);
                            return BllResultFactory.Sucess($"完成任务{task.Id}成功");
                        case TaskType.移库:
                            //更新托盘到目标库位，删除源库位托盘，更新两个库位为empty
                            connection.Execute(sqlContainer, new { containerCode = task.ContainerCode, code = task.ToLocationCode }, transaction);
                            connection.Execute(sqlContainer, new { containerCode = "", code = task.FromLocationCode }, transaction);
                            connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.ToLocationCode }, transaction);
                            connection.Execute(sqlStauts, new { status = (int)LocationLockStatus.空闲, code = task.FromLocationCode }, transaction);
                            return BllResultFactory.Sucess($"完成任务{task.Id}成功");
                        case TaskType.换站:
                        case TaskType.移位:
                            return BllResultFactory.Sucess($"完成任务{task.Id}成功");
                        default:
                            return BllResultFactory.Error($"任务{task.Id}为未识别的任务类型");
                    }
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"任务{task.Id}完成出现异常：{ex.ToString()}");

            }

        }

        #endregion

        #region Task 私有方法


        /// <summary>
        /// 创建任务   要求进行锁定库位
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="trans"></param>
        /// <param name="remoteTaskNo"></param>
        /// <param name="palletCode"></param>
        /// <param name="toPort">设备编码</param>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <param name="priority"></param>
        /// <param name="userCode"></param>
        /// <param name="warehouseCode"></param>
        /// <returns></returns>
        private BllResult<TaskEntity> CreateTask(IDbConnection connection, IDbTransaction trans, string remoteTaskNo, string preTaskNo, string palletCode,
            string toPort, string fromPort, string fromLocation, string toLocation, int status, int type, int priority, int preTaskId,
            string userCode, string warehouseCode, string platform)
        {
            //区域信息
            string destinationArea = "";
            try
            {
                //不接受重复任务，你无法保证也没有必要做全字段检测
                CommitFlag commitFlag = CommitFlag.未提交;
                #region 远程任务校验，如果为空或者为0，则不校验
                if (!string.IsNullOrWhiteSpace(remoteTaskNo) && remoteTaskNo != "0")
                {
                    var tempResult = AppSession.Dal.GetCommonModelByConditionWithZero<TaskEntity>($" where remoteTaskNo='{remoteTaskNo}'" +
        $" and deleted=0 ", connection, trans);
                    if (!tempResult.Success)
                    {
                        return BllResultFactory<TaskEntity>.Error(tempResult.Data[0], $"查询{remoteTaskNo}本地任务失败");
                    }
                    else
                    {
                        if (tempResult.Data.Count > 0)
                        {
                            return BllResultFactory<TaskEntity>.Error($"任务{remoteTaskNo}已存在");
                        }
                    }
                }
                else
                {
                    commitFlag = CommitFlag.无需提交;
                }
                #endregion

                //判断托盘信息是否在执行任务中已经存在了，防止托盘码重复
                if (!String.IsNullOrWhiteSpace(palletCode))
                {
                    //校验托盘，查看托盘是否存在任务未完成
                    int result = connection.Query<int>(sql: $"SELECT count(*) from wcstask where deleted=0 and containerCode = @pallet and taskStatus<{(int)TaskEntityStatus.任务完成} and warehouseCode= @warehouseCode", param: new { pallet = palletCode, warehouseCode = warehouseCode }, transaction: trans).ToList()[0];
                    if (result > 0)
                    {
                        return BllResultFactory<TaskEntity>.Error(null, $"创建任务：{remoteTaskNo}失败，托盘{palletCode}已存在任务，请不要重复下发任务到此托盘");
                    }
                }
                else
                {
                    return BllResultFactory<TaskEntity>.Error(null, $"创建任务：{remoteTaskNo}失败，托盘号不能为空");
                }

                //待创建任务
                TaskEntity taskEntity = new TaskEntity();
                //默认阶段，库内
                taskEntity.Stage = (int)TaskStageFlag.库内;

                // 根据任务类型判断库位信息是否正确  
                //入库性质：整盘入库、空容器入库  移库（其他业务流程在入库口的时候可以在请求获取到具体的目标库位，移库必须是2个库位都给出）
                //hack: 如果入库的目标库位不限制，可以在此更改
                if (type == TaskType.整盘入库.GetIndexInt() || type == TaskType.空容器入库.GetIndexInt())
                {
                    if (String.IsNullOrWhiteSpace(toLocation))
                    {
                        return BllResultFactory<TaskEntity>.Error(null, $"任务：{remoteTaskNo}，对于任务类型:{((TaskType)type).ToDescriptionString()}目标库位不能为空");
                    }
                    else
                    {
                        //校验托盘，是否存放到库位中
                        var result = connection.Query<int>(sql: $"SELECT count(*) from location where  containerCode = @pallet and warehouseCode= @warehouseCode", param: new { pallet = palletCode, warehouseCode = warehouseCode }, transaction: trans).ToList()[0];
                        if (result > 0)
                        {
                            return BllResultFactory<TaskEntity>.Error(null, $"创建任务：{remoteTaskNo}失败，托盘{palletCode}已存在库位中");
                        }
                        //判断是否可以进行创建入库任务条件
                        var temp = CheckLocationForCreateTaskIn(connection, trans, toLocation, warehouseCode);
                        if (!temp.Success)
                        {
                            return BllResultFactory<TaskEntity>.Error(null, temp.Msg);
                        }
                        //入库性质，默认阶段为入
                        taskEntity.Stage = (int)TaskStageFlag.入;
                        destinationArea = temp.Data.DestinationArea;
                    }
                }
                // 出库性质的
                else if (type == TaskType.补充入库.GetIndexInt() || type == TaskType.整盘出库.GetIndexInt() || type == TaskType.分拣出库.GetIndexInt()
                    || type == TaskType.出库查看.GetIndexInt() || type == TaskType.空容器出库.GetIndexInt() || type == TaskType.盘点.GetIndexInt())
                {
                    if (String.IsNullOrWhiteSpace(fromLocation))
                    {
                        return BllResultFactory<TaskEntity>.Error(null, $"创建任务{remoteTaskNo}失败：对于补充入库、整盘出库、分拣出库、空盘出库、盘点源库位不能为空");
                    }
                    else
                    {
                        //校验库位
                        var temp = AppSession.LocationService.GetAllLocations(null, null, null, null, null, null, fromLocation, warehouseCode);
                        if (!temp.Success)
                        {
                            return BllResultFactory<TaskEntity>.Error(null, "未找到" + fromLocation);
                        }
                        var location = temp.Data[0];
                        if (location.IsLock != LocationLockStatus.空闲.GetIndexInt())
                        {
                            return BllResultFactory<TaskEntity>.Error(null, $"创建任务{remoteTaskNo}失败：{fromLocation}非空闲");
                        }
                        if (string.IsNullOrEmpty(location.ContainerCode))
                        {
                            return BllResultFactory<TaskEntity>.Error(null, $"创建任务{remoteTaskNo}失败：{fromLocation}无托盘");
                        }
                        if (palletCode != location.ContainerCode)
                        {
                            return BllResultFactory<TaskEntity>.Error(null, $"创建任务{remoteTaskNo}失败：" + fromLocation + "库位托盘为" + location.ContainerCode + "与录入的" + palletCode + "不一致");
                        }
                        //出库性质的任务默认阶段是出
                        taskEntity.Stage = (int)TaskStageFlag.出;

                        var result = AppSession.ExcuteService.GetPortFromDict(toPort);
                        if (!result.Success)
                        {
                            return BllResultFactory<TaskEntity>.Error(null, $"创建任务：{remoteTaskNo}失败：{result.Msg}");
                        }
                        destinationArea = location.DestinationArea;
                    }
                }

                else if (type == TaskType.移库.GetIndexInt() || type == TaskType.移位.GetIndexInt())
                {
                    if (String.IsNullOrWhiteSpace(fromLocation) || string.IsNullOrWhiteSpace(toLocation))
                    {
                        return BllResultFactory<TaskEntity>.Error(null, "对于移库源库位和去向库位不能为空");
                    }
                    var result = AppSession.Dal.GetCommonModelByCondition<Location>($"where code = {fromLocation}");
                    if (!result.Success)
                    {
                        return BllResultFactory<TaskEntity>.Error($"库位错误：{result.Msg}");
                    }
                    var result2 = AppSession.Dal.GetCommonModelByCondition<Location>($"where code = {toLocation}");
                    if (!result2.Success)
                    {
                        return BllResultFactory<TaskEntity>.Error($"库位错误：{result2.Msg}");
                    }
                    //校验源库位是否空闲且存在托盘
                    var from = result.Data[0];
                    var to = result2.Data[0];
                    if (string.IsNullOrWhiteSpace(from.ContainerCode) || from.IsLock != (int)LocationLockStatus.空闲)
                    {
                        return BllResultFactory<TaskEntity>.Error($"创建任务：{remoteTaskNo}失败：源库位不存在托盘或源库位非空闲");
                    }
                    //目标库位不存在托盘且目标库位空闲
                    if (!string.IsNullOrWhiteSpace(to.ContainerCode) || to.IsLock != (int)LocationLockStatus.空闲)
                    {
                        return BllResultFactory<TaskEntity>.Error($"创建任务：{remoteTaskNo}失败：目标库位存在托盘或者目标库位非空闲");
                    }
                    //对于移库（巷道内移库），默认阶段是库存
                    taskEntity.Stage = (int)TaskStageFlag.库内;
                }
                else if (type == TaskType.换站.GetIndexInt())
                {
                    //获取系统中Port列表
                    var result = AppSession.ExcuteService.GetPortFromDict(toPort);
                    if (!result.Success)
                    {
                        return BllResultFactory<TaskEntity>.Error($"创建任务：{remoteTaskNo}失败：{result.Msg}");
                    }
                    //对于换站，默认阶段是入
                    taskEntity.Stage = (int)TaskStageFlag.出;
                }
                else
                {
                    return BllResultFactory<TaskEntity>.Error(null, "未识别的任务类型");
                }

                taskEntity.RemoteTaskNo = remoteTaskNo;
                taskEntity.PreRemoteTaskNo = preTaskNo;
                taskEntity.PreTaskId = preTaskId;
                taskEntity.TaskType = type;
                taskEntity.ContainerCode = palletCode;
                taskEntity.FromLocationCode = fromLocation;
                taskEntity.ToLocationCode = toLocation;
                taskEntity.FromPort = fromPort;
                taskEntity.ToPort = toPort;
                taskEntity.TaskStatus = TaskEntityStatus.任务创建.GetIndexInt();
                taskEntity.Gateway = ""; //初始任务没有gateWay
                taskEntity.DestinationArea = destinationArea;
                taskEntity.Priority = priority;
                //提交标志位
                taskEntity.CommitFlag = (int)commitFlag;
                taskEntity.WarehouseCode = warehouseCode;
                taskEntity.Platform = platform; //来源平台
                taskEntity.Deleted = false;
                taskEntity.CreatedBy = userCode;
                taskEntity.Created = DateTime.Now;

                var tempSaveResult = AppSession.Dal.InsertCommonModel<TaskEntity>(taskEntity, connection, trans);
                if (tempSaveResult.Success)
                {
                    //更新货位状态为预定
                    if (type == (int)TaskType.整盘入库 || type == (int)TaskType.空容器入库 || type == (int)TaskType.移库)
                    {
                        string sql2 = "update location set isLock = @status where code = @code;";
                        var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql2, new { status = LocationLockStatus.工作, code = toLocation }, connection, trans);
                        if (!result.Success)
                        {
                            return BllResultFactory<TaskEntity>.Error(null, $"创建任务{remoteTaskNo}失败，更新货位状态失败：{result.Msg}");
                        }
                    }
                    if (type == (int)TaskType.补充入库 || type == (int)TaskType.整盘出库 || type == (int)TaskType.分拣出库
                     || type == (int)TaskType.出库查看 || type == (int)TaskType.空容器出库 || type == (int)TaskType.盘点 || type == (int)TaskType.移库)
                    {
                        string sql2 = "update location set isLock = @status where code = @code;";
                        var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql2, new { status = (short)LocationLockStatus.工作, code = fromLocation }, connection, trans);
                        if (!result.Success)
                        {
                            return BllResultFactory<TaskEntity>.Error(null, $"创建任务{remoteTaskNo}失败，更新货位状态失败：{result.Msg}");
                        }
                    }

                    return BllResultFactory<TaskEntity>.Sucess(null, $"创建任务{remoteTaskNo}成功");
                }
                else
                {
                    return BllResultFactory<TaskEntity>.Error(null, $"生成任务{remoteTaskNo}失败:{tempSaveResult.Msg}");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<TaskEntity>.Error(null, $"生成任务{remoteTaskNo}失败：{ex.Message}");
            }
        }

        private BllResult CreateTask(IDbConnection connection, IDbTransaction trans, TaskCreateModel model)
        {
            string title = "TaskCreate";
            string logText;
            try
            {
                if (model == null)
                {
                    logText = "TaskCreateModel模型为空";
                    AppSession.LogService.WriteLog(title, logText);
                    return BllResultFactory.Error(logText);
                }
                //任务号
                if (string.IsNullOrWhiteSpace(model.TaskNo))
                {
                    logText = "出入的数据不对,任务号出现为空或null";
                    AppSession.LogService.WriteLog(title, logText);
                    return BllResultFactory.Error(logText);
                }
                //容器
                if (string.IsNullOrWhiteSpace(model.ContainerCode))
                {
                    logText = $"创建任务{model.TaskNo}失败,容器条码出现为空";
                    AppSession.LogService.WriteLog(title, logText);
                    return BllResultFactory.Error(logText);
                }
                //任务类型
                if (model.TaskType == 0 || !CommonHelper.EnumListInt<TaskType>().Contains(model.TaskType))
                {
                    logText = $"创建任务{model.TaskNo}失败,任务类型:{model.TaskType}不在系统的管制范围内";
                    AppSession.LogService.WriteLog(title, logText);
                    return BllResultFactory.Error(logText);
                }
                //货位类型
                if (string.IsNullOrWhiteSpace(model.FromLocationCode) || string.IsNullOrWhiteSpace(model.ToLocationCode))
                {
                    logText = $"创建任务{model.TaskNo}失败,库位编码:from:{model.FromLocationCode},to:{model.ToLocationCode}不能为空";
                    AppSession.LogService.WriteLog(title, logText);
                    return BllResultFactory.Error(logText);
                }
                //站台
                //if (string.IsNullOrWhiteSpace(model.ToPort) || string.IsNullOrWhiteSpace(model.FromPort))
                //{
                //    logText = $"创建任务{model.TaskNo}失败,站台类型不能出现空，没有请赋值0";
                //    AppSession.LogService.WriteLog(title, logText);
                //    return BllResultFactory.Error(logText);
                //}
                if (string.IsNullOrWhiteSpace(model.WarehouseCode))
                {
                    return BllResultFactory.Error($"创建任务{model.TaskNo}失败,仓库编码不能为空");
                }

                var warehouseResult = AppSession.Dal.GetCommonModelByCondition<Warehouse>($"where code ='{model.WarehouseCode}'");
                if (!warehouseResult.Success)
                {
                    return BllResultFactory.Error($"创建任务{model.TaskNo}失败，仓库编码不存在:{warehouseResult.Msg}");
                }

                //前置任务
                if (!string.IsNullOrWhiteSpace(model.PreTaskNo) && model.PreTaskNo != "0")
                {
                    var tmp6 = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where remoteTaskNo='{model.PreTaskNo}' and deleted=0 ");
                    if (!tmp6.Success || tmp6.Data.Count != 1)
                    {
                        logText = $"创建任务{model.TaskNo}失败,前置任务:{model.PreTaskNo}没有查询到其相应任务";
                        AppSession.LogService.WriteLog(title, logText);
                        return BllResultFactory.Error(logText);
                    }
                    model.PreTaskId = (int)tmp6.Data[0].Id;
                }

                string fromPort = "";
                string toPort = "";
                var dicPortNames = AppSession.BllService.GetDictWithDetails(SysConst.Port.ToString()).Success ? AppSession.BllService.GetDictWithDetails("Port").Data : null;
                DictDetail portDict = dicPortNames?.DictDetails.FirstOrDefault(t => t.Code == model.ToPort);
                if (portDict == null)
                {
                    //对于出库口
                    if (model.TaskType == TaskType.整盘出库.GetIndexInt() || model.TaskType == (int)TaskType.空容器出库 || model.TaskType == (int)TaskType.补充入库 || model.TaskType == (int)TaskType.分拣出库 || model.TaskType == (int)TaskType.盘点 || model.TaskType == (int)TaskType.出库查看 || model.TaskType == (int)TaskType.换站)
                    {
                        logText = $"创建任务{model.TaskNo}失败,任务类型:{model.TaskType}，任务目的操作口:{model.ToPort}不在系统的管制范围内";
                        AppSession.LogService.WriteLog(title, logText);
                        return BllResultFactory.Error(logText);
                    }
                    toPort = "0";
                }
                else
                {
                    toPort = portDict.Value;
                }

                //hack:对于toport通过任务类型判断；对于fromport，在有输送线的情况下，如果需要进行指定口的路径入口，则需要重新考虑段代码是否需要加入限制
                DictDetail gateDict = dicPortNames?.DictDetails.FirstOrDefault(t => t.Code == model.FromPort);
                if (gateDict == null)
                {
                    fromPort = "0";
                }
                else
                {
                    fromPort = gateDict.Value;
                }


                var result =
                    CreateTask(connection, trans, model.TaskNo, model.PreTaskNo, palletCode: model.ContainerCode, toPort: toPort, fromPort: fromPort,
                    fromLocation: model.FromLocationCode, toLocation: model.ToLocationCode,
                    status: 1, type: model.TaskType, priority: model.Priority, platform: model.Platform, preTaskId: model.PreTaskId,
                    userCode: "WCS_Interface",
                    warehouseCode: model.WarehouseCode);
                if (result.Success)
                {
                    // 明细的是否成功对主业务不影响，这里因此没有强制
                    var temp = CreateTaskDetail(connection, trans, model);
                    return BllResultFactory.Sucess();
                }
                else
                {
                    logText = $"生成任务数据不对，任务号:{model.TaskNo},错误原因:{result.Msg}";
                    AppSession.LogService.WriteLog(title, logText);
                    return BllResultFactory.Error(logText);
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"创建任务的时候出现异常{ex.Message}");
            }
        }

        private BllResult DeleteTaskByTaskNo(IDbConnection connection, IDbTransaction trans, string taskNo)
        {
            string title = "TaskCancel";
            string message = "";
            try
            {
                TaskEntity taskEntity = null;
                //查询远程任务ID
                var taskTmp = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where remoteTaskNo='{taskNo}' and deleted=0", connection, trans);
                if (taskTmp.Success && taskTmp.Data.Count > 0)
                {
                    taskEntity = taskTmp.Data[0];
                }
                else
                {
                    message = $"任务取消失败，任务号:{taskNo}不存在系统中";
                    AppSession.LogService.WriteLog(title, message);
                    return BllResultFactory.Error(message);
                }
                int id = (int)taskEntity.Id;
                int status = taskEntity.TaskStatus;
                // todo: 这里需要判断后续其他业务是否也可以允许取消
                if (status == TaskEntityStatus.任务创建.GetIndexInt())
                {
                    var result = DeleteTaskByEntity(connection, trans, taskEntity);
                    if (result.Success)
                    {
                        return BllResultFactory.Sucess(result.Msg);
                    }
                    else
                    {
                        message = $"任务取消失败，任务号:{taskNo},错误原因:{result.Msg}";
                        AppSession.LogService.WriteLog(title, message);
                        return BllResultFactory.Error(message);
                    }
                }
                else
                {
                    message = $"任务取消失败，任务号:{taskNo},已经开始执行了";
                    AppSession.LogService.WriteLog(title, message);
                    return BllResultFactory.Error(message);
                }
            }
            catch (Exception ex)
            {
                message = $"任务取消失败，任务号:{taskNo},{ex.Message}";
                AppSession.LogService.WriteLog(title, message);
                return BllResultFactory.Error(message);
            }
        }

        private BllResult<TaskDetailEntity> CreateTaskDetail(IDbConnection connection, IDbTransaction trans, TaskCreateModel taskCreateModel)
        {
            string title = "taskCreate";
            string message = "";
            try
            {
                if (taskCreateModel.taskDetails != null && taskCreateModel.taskDetails.Count > 0)
                {
                    // 获取头表标识
                    int? taskHeaderId = 0;
                    var taskHeaderResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where RemoteTaskNo='{taskCreateModel.TaskNo}' and taskStatus={TaskEntityStatus.任务创建.GetIndexInt()}", connection, trans);
                    if (taskHeaderResult.Success)
                    {
                        taskHeaderId = taskHeaderResult.Data[0].Id;
                    }
                    else
                    {
                        message = $"根据上游任务号{taskCreateModel.TaskNo}没有查询到相应的主任务";
                        AppSession.LogService.WriteLog(title, message);
                        return BllResultFactory<TaskDetailEntity>.Error(message);
                    }
                    foreach (TaskDetailCreateModel model in taskCreateModel.taskDetails)
                    {
                        TaskDetailEntity taskDetail = new TaskDetailEntity();
                        taskDetail.TaskId = (int)taskHeaderId;
                        taskDetail.MaterialCode = model.materialCode;
                        taskDetail.MaterialName = model.materialName;
                        taskDetail.Qty = model.qty;
                        taskDetail.Unit = model.unit;
                        taskDetail.ReferLineId = model.referLineNo;
                        taskDetail.LastUpdated = DateTime.Now;
                        var result = AppSession.Dal.InsertCommonModel<TaskDetailEntity>(taskDetail, connection, trans);
                        if (!result.Success)
                        {
                            message = $"插入任务明细出错，信息:{result.Msg}";
                            AppSession.LogService.WriteLog(title, message);
                            return BllResultFactory<TaskDetailEntity>.Error(message);
                        }
                    }
                    return BllResultFactory<TaskDetailEntity>.Sucess();
                }
                else
                {
                    return BllResultFactory<TaskDetailEntity>.Error("数据有问题，函数没有执行");
                }
            }
            catch (Exception ex)
            {
                message = $"插入任务明细出现异常，信息:{ex.Message}";
                AppSession.LogService.WriteLog(title, message);
                return BllResultFactory<TaskDetailEntity>.Error(message);
            }

        }

        /// <summary>
        /// 根据任务ID进行任务删除，包括任务明细。 删除后记录日志
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private BllResult DeleteTaskByEntity(IDbConnection connection, IDbTransaction transaction, TaskEntity task)
        {
            //step1:  先插入任务明细到任务明细列历史表中
            var taskDetailResult = AppSession.Dal.GetCommonModelByCondition<TaskDetailEntity>($" WHERE taskId={task.Id} ");
            if (taskDetailResult.Success && taskDetailResult.Data.Count > 0)
            {
                List<int> ids = new List<int> { };
                foreach (TaskDetailEntity taskDetail in taskDetailResult.Data)
                {
                    TaskDetailEntityDeleted detailEntityDeleted = new TaskDetailEntityDeleted();
                    detailEntityDeleted.TaskId = taskDetail.TaskId;
                    detailEntityDeleted.BillCode = taskDetail.BillCode;
                    detailEntityDeleted.MaterialCode = taskDetail.MaterialCode;
                    detailEntityDeleted.MaterialName = taskDetail.MaterialName;
                    detailEntityDeleted.Qty = taskDetail.Qty;
                    detailEntityDeleted.ReferLineId = taskDetail.ReferLineId;
                    detailEntityDeleted.Unit = taskDetail.Unit;
                    detailEntityDeleted.Weight = taskDetail.Weight;
                    detailEntityDeleted.LastUpdated = DateTime.Now;

                    var result = AppSession.Dal.InsertCommonModel<TaskDetailEntityDeleted>(detailEntityDeleted, connection, transaction);
                    if (!result.Success)
                    {
                        return BllResultFactory.Error(result.Msg);
                    }
                    ids.Add(taskDetail.Id);
                }
                //step3:  删除任务明细
                var result3 = AppSession.Dal.DeleteCommonModelByIds<TaskDetailEntity>(ids, connection, transaction);
                if (!result3.Success)
                {
                    return BllResultFactory.Error(result3.Msg);
                }
            }
            //step2;  插入任务到任务历史表中
            TaskEntityDeleted taskEntityDeleted = new TaskEntityDeleted();
            taskEntityDeleted.RemoteTaskNo = task.RemoteTaskNo;
            taskEntityDeleted.TaskType = task.TaskType;
            taskEntityDeleted.ContainerCode = task.ContainerCode;
            taskEntityDeleted.Port = task.ToPort;
            taskEntityDeleted.FromLocationCode = task.FromLocationCode;
            taskEntityDeleted.ToLocationCode = task.ToLocationCode;
            taskEntityDeleted.WarehouseCode = task.WarehouseCode;
            taskEntityDeleted.TaskStatus = task.TaskStatus;
            taskEntityDeleted.Created = DateTime.Now;
            taskEntityDeleted.Updated = DateTime.Now;
            taskEntityDeleted.CreatedBy = task.CreatedBy;
            taskEntityDeleted.UpdatedBy = task.UpdatedBy;

            var result2 = AppSession.Dal.InsertCommonModel<TaskEntityDeleted>(taskEntityDeleted, connection, transaction);
            if (!result2.Success)
            {
                return BllResultFactory.Error(result2.Msg);
            }
            // 库位解锁
            //string sql = $"update location set status={(int)LocationStatus.空闲} where code in @codes";

            //库位解锁 现有表名
            string sql = $"update location set isLock =@IsLock where code in @codes";

            List<string> codes = new List<string>();
            if (string.IsNullOrWhiteSpace(task.FromLocationCode))
            {
                codes.Add(task.FromLocationCode);
            }
            if (string.IsNullOrWhiteSpace(task.ToLocationCode))
            {
                codes.Add(task.ToLocationCode);
            }
            var result5 = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql, new { IsLock = (int)LocationLockStatus.空闲, codes }, connection, transaction);
            if (!result5.Success)
            {
                return BllResultFactory.Error(result5.Msg);
            }
            //step4: 删除任务
            List<int> tids = new List<int> { };
            tids.Add((int)task.Id);
            var result4 = AppSession.Dal.DeleteCommonModelByIds<TaskEntity>(tids, connection, transaction);
            if (!result4.Success)
            {
                return BllResultFactory.Error(result4.Msg);
            }
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 查询任务信息
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="remoteTaskNo"></param>
        /// <returns></returns>
        private BllResult<TaskEntity> TaskQueryByTaskNo(string remoteTaskNo)
        {
            var taskResult = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where remoteTaskNo='{remoteTaskNo}' and deleted=0 ");
            if (taskResult.Success && taskResult.Data.Count > 0)
            {
                return BllResultFactory<TaskEntity>.Sucess(taskResult.Data[0]);
            }
            return BllResultFactory<TaskEntity>.Error();
        }

        /// <summary>
        /// 根据站台编码查询站台状态（出入口）
        /// </summary>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        private BllResult<StationResponseInfoModel> StationInfoByCode(string stationCode)
        {
            var dicPortNames = AppSession.BllService.GetDictWithDetails(SysConst.Port.ToString()) != null ? AppSession.BllService.GetDictWithDetails("Port").Data : null;
            DictDetail dict = dicPortNames?.DictDetails.FirstOrDefault(t => t.Code == stationCode);
            if (dict != null)
            {
                var result = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($" where port='{ stationCode}' and deleted=0 " +
                    $"and taskStaus >= {TaskEntityStatus.下发任务.GetIndexInt()} and taskStatus < {TaskEntityStatus.任务完成.GetIndexInt()} ");
                StationResponseInfoModel infoModel = new StationResponseInfoModel();
                infoModel.station = stationCode;
                infoModel.state = result.Success ? "1 " : "0";
            }
            return BllResultFactory<StationResponseInfoModel>.Error($"没有查询到该站台，输入站台:{stationCode}信息");
        }

        /// <summary>
        /// 任务回传
        /// </summary>
        /// <param name="url"></param>
        /// <param name="firstStatus"></param>
        private void TaskFeedback(string url, TaskEntityStatus firstStatus)
        {
            try
            {
                //获取任务完成的需要回传的,上游任务号大于0的才能进行回传，小于0的均为WCS产生的任务;
                //hack: 这里发生空出的任务不进行回传
                var temp = AppSession.Dal.GetCommonModelByCondition<TaskEntity>($"where deleted=0 and taskStatus={firstStatus.GetIndexInt()}" +
                    $" and id > 0 and (commitFlag = 0 or commitFlag = 3) ");
                if (temp.Success)
                {
                    List<TaskEntity> taskEntities = temp.Data;
                    foreach (TaskEntity task in taskEntities)
                    {
                        TaskPostModel req = TaskEntityToReqTaskConfirm(task);
                        if (req == null) { continue; }
                        BllResult<TaskPostModel> bll = AppSession.CommonService.PostJson<TaskPostModel>("taskConfirm", url, req).Result;
                        if (bll.Success)
                        {
                            AppSession.LogService.WriteLog("taskConfrim", $"任务号:{req.TaskNo } 回传成功");
                            task.CommitFlag = (int)CommitFlag.已提交;
                            AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        }
                        else
                        {
                            AppSession.LogService.WriteLog("taskConfrim", $"任务号:{req.TaskNo } 回传失败：{bll.Msg}");
                            task.CommitFlag = (int)CommitFlag.提交失败;
                            AppSession.Dal.UpdateCommonModel<TaskEntity>(task);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteExceptionLog("WcsApiService", ex);
            }
        }

        /// <summary>
        /// 类型转化 自定义的
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private TaskPostModel TaskEntityToReqTaskConfirm(TaskEntity task)
        {
            try
            {
                TaskPostModel req = new TaskPostModel();
                req.TaskNo = task.RemoteTaskNo;
                req.IsDoubleIn = task.IsDoubleIn;
                req.IsEmptyOut = task.IsEmptyOut;
                req.IsForkError = task.IsForkError;
                req.RedirectionLocationCode = task.DoubleInLocationCode;
                return req;
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteExceptionLog("WcsApiService", ex);
                return null;
            }
        }

        #endregion

        #region LthMeasuringTask 测长完成，生成一条缓存数据

        public BllResult<LthMeasuringCache> CreateLthMeasuringCache(int pipeid, int materialType, int diameter, int materialLength, int thickness )
        {
            LthMeasuringCache lthMeasuringCache = new LthMeasuringCache();
            lthMeasuringCache.PipeId = pipeid;
            lthMeasuringCache.MaterialType = materialType;
            lthMeasuringCache.Diameter = diameter;
            lthMeasuringCache.MaterialLength = materialLength;
            lthMeasuringCache.Thickness = thickness;
            var result = AppSession.Dal.InsertCommonModel<LthMeasuringCache>(lthMeasuringCache);
            if (result.Success)
            {
                lthMeasuringCache.Id = result.Data;
                return BllResultFactory<LthMeasuringCache>.Sucess(lthMeasuringCache);
            }
            else
            {
                return BllResultFactory<LthMeasuringCache>.Error(result.Msg);
            }
        }

        #endregion

    }
}
