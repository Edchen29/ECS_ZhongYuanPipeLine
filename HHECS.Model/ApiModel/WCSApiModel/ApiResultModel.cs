using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    public class ApiResultModel<T>
    {
        public ApiResultModel(HttpStatusCode statusCode,T data,string msg)
        {
            this.Code = statusCode;
            this.Data = data;
            this.Msg = msg;
        }
        public HttpStatusCode Code { get; set; }
        public T Data { get; set; }
        public string Msg { get; set; }
    }

    public class ApiResultModel
    {
        public ApiResultModel()
        {

        }

        public ApiResultModel(HttpStatusCode statusCode, object data, string msg)
        {
            this.Code = statusCode;
            this.Data = data;
            this.Msg = msg;
        }
        public HttpStatusCode Code { get; set; }
        public object Data { get; set; }
        public string Msg { get; set; }
    }
}