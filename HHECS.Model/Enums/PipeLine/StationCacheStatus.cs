using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.PipeLine
{
     public enum StationCacheStatus
    {
        初始 = 0,
        已经套料 = 1,
        使用中 = 2,
        使用完毕 = 3,
    }

    public enum CutPlanStatus
    {
        初始 = 0,
        发送定长 = 1,
        发送打标机 = 2,
        打标完成 = 3,
    }


    public enum OrderStatus
    {
        初始 = 0,
        已经套料 = 1,
    }
}
