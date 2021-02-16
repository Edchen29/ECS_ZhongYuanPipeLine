using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Task
{
    public enum TaskType
    {
        [Description("整盘入库")]
        整盘入库 = 100,
        [Description("空容器入库")]
        空容器入库 = 500,
        [Description("整盘出库")]
        整盘出库 = 300,
        [Description("空容器出库")]
        空容器出库 = 600,
        [Description("补充入库")]
        补充入库 = 200,
        [Description("分拣出库")]
        分拣出库 = 400,
        [Description("盘点")]
        盘点 = 700,
        [Description("出库查看")]
        出库查看 = 900,// 后续的质检任务  也是通过出库查看任务类型实现
        [Description("移库")]
        移库 = 800,
        [Description("移位")]
        移位 = 810,
        [Description("换站")]
        换站 = 1000,
        
    }
}
