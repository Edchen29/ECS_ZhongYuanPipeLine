using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Truss
{
    /// <summary>
    /// 桁车机动作类型
    /// 0=无任务，1=1号机械手，2=2号机械手，3=同时动作，4=行走任务
    /// </summary>
    public enum TrussForkAction
    {
        无任务 = 0,
        一号机械手 = 1,
        二号机械手 = 2,
        同时动作 = 3,
        行走任务 = 4,
    }
}
