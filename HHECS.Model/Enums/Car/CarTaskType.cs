using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HHECS.Model.Enums.Car
{
    public  enum CarTaskType
    {
        [Description("初始")]
        初始 = 0,
        [Description("取货")]
        取货 = 1,
        [Description("放货")]
        放货 = 2,
        [Description("取货和放货")]
        取货和放货 = 3,
        [Description("翻转")]
        翻转 = 4
    }
}
