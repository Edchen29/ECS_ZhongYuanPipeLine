using HHECS.API.Common;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HHECS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //注册全局过滤实例，用来包装返回值
            config.Filters.Add(new ApiResultAttribute());
            //注册全局异常处理，用来包装异常返回
            config.Filters.Add(new ApiErrorHandleAttribute());
            //注册特殊异常处理器
            config.MessageHandlers.Add(new CustomErrorMessageDelegatingHandler());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "API/WCS/V2/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
