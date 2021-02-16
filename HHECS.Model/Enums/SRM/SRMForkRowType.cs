using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.SRM
{
    /// <summary>
    /// 双伸位时，取货排的范围
    /// 1=左1，2=左2，3=右1，4=右2
    /// </summary>
    public enum SRMForkRowType
    {
        左2 = 2,
        左1 = 1,
        右1 = 3,
        右2 = 4
    }
}
