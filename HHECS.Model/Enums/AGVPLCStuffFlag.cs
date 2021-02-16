using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums
{
    /// <summary>
    /// 上下料对接
    /// </summary>
    public enum AGVPLCStuffFlag
    {
        允许上料 = 10,
        上料辊筒转动 = 20,
        上料完成 = 30,
        允许下料 = 40,
        下料辊筒转动 = 50,
        下料完成 = 60,
    }
}
