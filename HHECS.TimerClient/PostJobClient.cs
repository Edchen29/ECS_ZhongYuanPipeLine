using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Newtonsoft.Json;
using HHECS.Model.ApiModel;
using HHECS.Bll;
using System.Net.Http.Headers;
using HHECS.Model.Common;

namespace HHECS.TimerClient
{
    public class PostJobClient : IJob
    {
        //这里会被自动注入
        public int Id { get; set; }
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(async () =>
            {
                await Excute(context);
            });
        }

        private async Task Excute(IJobExecutionContext context)
        {
            var result = await AppSession.JobService.ExcuteJobAsync(Id, App.Client);
            Logger.Log(result.Msg, result.Success ? Model.Enums.LogLevel.Info : Model.Enums.LogLevel.Error);
        }
    }
}
