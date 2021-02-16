using HHECS.Model.ApiModel;
using HHECS.Model.ApiModel.WCSApiModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HHECS.API.Common
{
    /// <summary>
    /// 消息代理处理，用来捕获特殊的异常信息(这些异常不会被全局的异常filter捕获，这些异常应是在异常filter之前就已经发生并处理了)
    /// </summary>
    public class CustomErrorMessageDelegatingHandler: DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>((responseToCompleteTask) =>
            {
                HttpResponseMessage response = responseToCompleteTask.Result;
                HttpError error = null;
                if (response.TryGetContentValue<HttpError>(out error))
                {
                    //添加自定义错误处理
                    //error.Message = "Your Customized Error Message";
                }

                if (error != null)
                {
                    var setting = new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    };
                    //获取抛出自定义异常，有拦截器统一解析
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        //封装处理异常信息，返回指定JSON对象

                        Content = new StringContent(JsonConvert.SerializeObject(new ApiResultModel(HttpStatusCode.NotFound, null, error.Message), Formatting.None, setting)),
                        ReasonPhrase = "Exception"
                    });
                }
                else
                {
                    return response;
                }
            });
        }
    }
}