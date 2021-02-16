using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.SRM
{
    /// <summary>
    /// 堆垛机货叉动作类型
    /// 0=无，1=1号货叉，2=2号货叉，3=同时动作
    /// </summary>
    public enum SRMForkAction
    {
        无 = 0,
        货叉1号 = 1,
        货叉2号 = 2,
        同时动作 = 3
    }
}
