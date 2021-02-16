using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums
{
    public enum AGVRollerStatus
    {

        请求上料 = 1,
        请求上料辊筒转动 = 2,
        上料完成 = 3,

        请求下料 = 4,
        请求下料辊筒转动 = 5,
        下料完成 = 6

    }
}
