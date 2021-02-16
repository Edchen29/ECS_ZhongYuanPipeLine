using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace HHECS.TimerClient
{
    public class TestJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run( ()=> Console.WriteLine("定时任务调度。。"));
        }
    }
}
