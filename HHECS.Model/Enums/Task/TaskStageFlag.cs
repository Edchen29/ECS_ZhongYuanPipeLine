using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Task
{
    /// <summary>
    /// 标记任务的出入阶段
    /// 对于补充入库、分拣出库、盘点、出库查看等有进有出的任务，不再强制依靠任务状态判断出和入，使用出入阶段进行判断；
    /// 其他有明确方向性的任务不使用此标记
    /// </summary>
    public enum TaskStageFlag
    {
        库内 = 0,
        出 = 1,
        入 = 2
    }
}
