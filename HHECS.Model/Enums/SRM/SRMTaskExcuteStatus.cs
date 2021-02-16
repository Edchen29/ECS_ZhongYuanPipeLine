using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.SRM
{
    /// <summary>
    /// 任务的执行状态
    /// </summary>
    public enum SRMTaskExcuteStatus
    {
        待机 = 1,
        任务执行中 = 2,
        任务完成 = 3,
        任务中断_出错=4,
        下发任务错误 = 5
    }
}
