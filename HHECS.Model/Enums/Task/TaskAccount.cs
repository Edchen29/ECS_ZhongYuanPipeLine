using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Task
{
    /// <summary>
    /// 任务是否过账   0 ：任务新建   10：任务过账OK
    /// </summary>
    public enum TaskAccount
    {
        New = 0,
        OK = 10
    }
}
