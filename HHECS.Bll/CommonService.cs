using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HHECS.DAL;
using HHECS.Model.ApiModel;
using HHECS.Model.ApiModel.WCSApiModel;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using Newtonsoft.Json;

namespace HHECS.Bll
{
    public class CommonService : BaseService
    {

        /// <summary>
        /// 这里用同步的方式，因为我们是在定时器线程中调用的，不影响UI线程
        /// </summary>
        /// <param name="list"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public BllResult FormPost(List<KeyValuePair<string, string>> list, WMSUrls url, HttpClient client, List<DictDetail> urls)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var ret = client.PostAsync(urls.First(t => t.Code == url.ToString()).Value, GetFormUrlEncodedContent(list)).Result;
                if (ret.IsSuccessStatusCode)
                {
                    ret.EnsureSuccessStatusCode();
                    string temp = ret.Content.ReadAsStringAsync().Result;
                    AppSession.LogService.WriteInfoLog($"请求WMS接口{url.ToString()}成功，返回值：{temp}");
                    var b = JsonConvert.DeserializeObject<ResponseModel<Object>>(temp);
                    if (b.Code == "200")
                    {
                        return BllResultFactory.Sucess(null, b.Msg);
                    }
                    else
                    {
                        return BllResultFactory.Error(null, b.Msg);
                    }
                }
                else
                {
                    return BllResultFactory.Error(null, $"请求WMS失败，请检查网络连接,详情：{ret.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(null, $"请求WMS失败，请检查网络连接或者重启本程序,详情：{ex.ToString()}");
            }
        }

        /// <summary>
        /// 这里用同步的方式，因为我们是在定时器线程中调用的，不影响UI线程
        /// </summary>
        /// <param name="list"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public BllResult<T> FormPost<T>(List<KeyValuePair<string, string>> list, WMSUrls url, HttpClient client, List<DictDetail> urls)
        {
            try
            {
                var ret = client.PostAsync(urls.First(t => t.Code == url.ToString()).Value, GetFormUrlEncodedContent(list)).Result;
                if (ret.IsSuccessStatusCode)
                {
                    ret.EnsureSuccessStatusCode();
                    string temp = ret.Content.ReadAsStringAsync().Result;
                    var b = JsonConvert.DeserializeObject<ResponseModel<T>>(temp);
                    if (b.Code == "200")
                    {
                        return BllResultFactory<T>.Sucess(b.Data, b.Msg);
                    }
                    else
                    {
                        return BllResultFactory<T>.Error(default(T), b.Msg);
                    }
                }
                else
                {
                    return BllResultFactory<T>.Error(default(T), "请求WMS失败，请检查网络连接");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<T>.Error(default(T), "请求WMS失败，请检查网络连接或者重启本程序");
            }
        }

        /// <summary>
        /// 这里用异步的方式，UI线程中请使用这个方法
        /// </summary>
        /// <param name="list"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<BllResult> FormPostAsync(List<KeyValuePair<string, string>> list, WMSUrls url, HttpClient client, List<DictDetail> urls)
        {
            try
            {
                var ret = await client.PostAsync(urls.First(t => t.Code == url.ToString()).Value, GetFormUrlEncodedContent(list));
                if (ret.IsSuccessStatusCode)
                {
                    ret.EnsureSuccessStatusCode();
                    string temp = ret.Content.ReadAsStringAsync().Result;
                    var b = JsonConvert.DeserializeObject<ResponseModel<Object>>(temp);
                    if (b.Code == "200")
                    {
                        return BllResultFactory.Sucess(null, b.Msg);
                    }
                    else
                    {
                        return BllResultFactory.Error(null, b.Msg);
                    }
                }
                else
                {
                    return BllResultFactory.Error(null, "请求WMS失败，请检查网络连接");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error(null, "请求WMS失败，请检查网络连接或者重启本程序");
            }
        }

        public HttpContent GetFormUrlEncodedContent(List<KeyValuePair<string, string>> keyValuePair)
        {
            HttpContent content = new FormUrlEncodedContent(keyValuePair);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            return content;
        }

        /// <summary>
        /// 根据类型T 进行Post Json 提交
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<BllResult<T>> PostJson<T>(string title, string url, Object obj)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string postJsonString = JsonConvert.SerializeObject(obj);
                    StringContent content = new StringContent(postJsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();//用来抛异常的
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var temp = JsonConvert.DeserializeObject<ApiResultModel<T>>(responseBody);
                    if (temp.Code == System.Net.HttpStatusCode.OK)//成功
                    {
                        AppSession.LogService.LogInterface(title, postJsonString, responseBody, LogLevel.Success, "", "");
                        return BllResultFactory<T>.Sucess(temp.Data);
                    }
                    else
                    {
                        AppSession.LogService.LogInterface(title, postJsonString, responseBody, LogLevel.Failure, "", "");
                        return BllResultFactory<T>.Error(temp.Data, temp.Msg);
                    }
                }
                catch (Exception ex)
                {
                    AppSession.LogService.WriteExceptionLog(title, ex);
                    return BllResultFactory<T>.Error(ex.Message); ;
                }
            }
        }
    }
}
