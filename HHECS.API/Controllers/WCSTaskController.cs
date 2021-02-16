using HHECS.Model.ApiModel;
using HHECS.Model.BllModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using HHECS.Bll;
using HHECS.Model.ApiModel.WCSApiModel;

namespace HHECS.API.Controllers
{
    public class WCSTaskController : ApiController
    {

        /// <summary>
        /// 任务创建接口  传入多个任务的时候，其中有一个有问题后面的任务将全部不执行。
        /// </summary>
        /// <param name="taskCreateModels"></param>
        /// <returns></returns>
        [HttpPost]
        public BllResult TaskAssign([FromBody] TaskCreateModel taskCreateModel)
        {
            try
            {
                return AppSession.TaskService.CreateTask(taskCreateModel);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"创建任务失败:{ex.Message}");
            }

        }

        /// <summary>
        /// 任务创建接口  传入多个任务的时候，其中有一个有问题后面的任务将全部不执行。
        /// </summary>
        /// <param name="taskCreateModels"></param>
        /// <returns></returns>
        [HttpPost]
        public BllResult TaskAssigns([FromBody] List<TaskCreateModel> taskCreateModels)
        {
            return AppSession.TaskService.CreateTasks(taskCreateModels);
        }

        /// <summary>
        /// 任务取消
        /// </summary>
        /// <param name="taskCancelModel"></param>
        /// <returns></returns>
        [HttpPost]
        public BllResult TaskCancels([FromBody]TaskCancelModel taskCancelModel)
        {
            return AppSession.TaskService.TaskCancels(taskCancelModel);
        }

        /// <summary>
        /// 任务取消
        /// </summary>
        /// <param name="taskCancelModel"></param>
        /// <returns></returns>
        [HttpPost]
        public BllResult TaskCancel([FromBody]TaskCancelModel1 taskNo)
        {
            try
            {
                TaskService service = new TaskService();
                return service.TaskCancel(taskNo.TaskNo);
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"任务取消的时候出现异常{ex.Message}");
            }
        }

        /// <summary>
        /// 任务查询
        /// </summary>
        /// <param name="taskQueryModel"></param>
        /// <returns></returns>
        [HttpPost]
        public BllResult<List<TaskQueryResponseModel>> TaskInfos([FromBody]TaskQueryModel taskQueryModel)
        {
            return AppSession.TaskService.TaskQuery(taskQueryModel);
        }

        [HttpPost]
        public BllResult<TaskQueryResponseModel> TaskInfo([FromBody] TaskQueryModel1 taskNo)
        {
            try
            {
                return AppSession.TaskService.TaskQuery(taskNo.TaskNo);
            }
            catch (Exception ex)
            {
                return BllResultFactory<TaskQueryResponseModel>.Error($"任务取消的时候出现异常{ex.Message}");
            }
        }

        /// <summary>
        /// 执行站台信息查询
        /// </summary>
        /// <param name="stations"></param>
        /// <returns></returns>
        [HttpPost]
        public BllResult<List<StationResponseInfoModel>> StationInfos([FromBody] StationRequestInfoModel stations)
        {
            return AppSession.TaskService.StationInfo(stations);
        }
        [HttpPost]
        public BllResult<StationResponseInfoModel> StationInfo([FromBody] StationRequestInfoModel1 code)
        {
            return AppSession.TaskService.StationInfo(code.station);
        }

        #region 任务完成回传

        /// <summary>
        /// 任务确认 -- 回传上游系统
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void TaskConfirm()
        {
            AppSession.TaskService.ExecuteTaskFeedback();
        }

        #endregion

    }
}