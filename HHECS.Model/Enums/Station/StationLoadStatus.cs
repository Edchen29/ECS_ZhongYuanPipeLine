using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Station
{
    /// <summary>
    /// 站台装载状态
    /// </summary>
    public enum StationLoadStatus
    {
        默认 = 0,
        回复到达 = 1,
        回复控制指令 = 2
    }
}
