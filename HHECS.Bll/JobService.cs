using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HHECS.DAL;
using HHECS.Model.ApiModel;
using HHECS.Model.ApiModel.WCSApiModel;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using Newtonsoft.Json;

namespace HHECS.Bll
{
    public class JobService : BaseService
    {

        public async Task<BllResult> ExcuteJobAsync(int value, HttpClient client)
        {
            try
            {
                var result = AppSession.Dal.GetCommonModelByCondition<JobEntity>($"where id = {value}");
                if (result.Success)
                {
                    var job = result.Data[0];
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new ApiBusModel() { Api = job.Api, Param = job.Param }));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync(job.Url, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        response.EnsureSuccessStatusCode();
                        return BllResultFactory.Sucess($"计划：{job.Name},触发成功,触发时间{DateTime.Now.ToString()}；服务端返回值：{response.Content.ReadAsStringAsync().Result};");
                    }
                    else
                    {
                        return BllResultFactory.Error($"计划：{job.Name},触发成功,触发时间{DateTime.Now.ToString()}；但请求接口失败：http返回状态码为{response.StatusCode}");
                    }
                }
                else
                {
                    return BllResultFactory.Error($"未查询到id为{value}的计划任务");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"执行计划出现异常：{ex.ToString()}");
            }
        }
    }
}
