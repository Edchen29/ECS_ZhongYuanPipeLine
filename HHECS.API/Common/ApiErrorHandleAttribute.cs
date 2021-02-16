using HHECS.Model.ApiModel;
using HHECS.Model.ApiModel.WCSApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace HHECS.API.Common
{
    public class ApiErrorHandleAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            // 取得发生例外时的错误讯息
            var errorMessage = actionExecutedContext.Exception.Message;

            var result = new ApiResultModel()
            {
                Code = HttpStatusCode.BadRequest,
                Msg = errorMessage
            };

            // 重新打包回传的讯息
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result.Code, result);
        }
    }
}