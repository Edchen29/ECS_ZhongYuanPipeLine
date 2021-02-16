using HHECS.Bll;
using HHECS.Model.ApiModel;
using HHECS.Model.BllModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using HHECS.Model.Common;
using HHECS.Model.ApiModel.WCSApiModel;

namespace HHECS.API.Common
{
    public class ApiResultAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // 若发生异常则不在这边处理
            if (actionExecutedContext.Exception != null)
                return;
            base.OnActionExecuted(actionExecutedContext);

            string requestString = "";
            var stream = actionExecutedContext.Request.Content.ReadAsStreamAsync().Result;
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                requestString = reader.ReadToEnd();
            }

            ApiResultModel result = new ApiResultModel();

            var bllResult = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<BllResult>().Result;
            if (bllResult.Success)
            {
                // 取得由 API 返回的状态代码
                result.Code = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.Code = System.Net.HttpStatusCode.BadRequest;
            }

            // 取得由 API 返回的状态代码
            //result.Status = actionExecutedContext.ActionContext.Response.StatusCode;
            result.Msg = bllResult.Msg;
            // 取得由 API 返回的资料
            result.Data = bllResult.Data;

            AppSession.LogService.LogInterface(actionExecutedContext.ActionContext.ActionDescriptor.ActionName, requestString, JsonConvert.SerializeObject(result), result.Code == System.Net.HttpStatusCode.OK ? Model.Enums.LogLevel.Success : Model.Enums.LogLevel.Error, "", "");

            // 重新封装回传格式
            //本次请求是成功的，业务操作是否成功应看result.code
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
    }
}