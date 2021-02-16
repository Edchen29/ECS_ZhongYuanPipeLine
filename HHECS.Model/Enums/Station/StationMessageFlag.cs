using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Station
{
    /// <summary>
    /// 站台报文名称
    /// </summary>
    public enum StationMessageFlag
    {
        默认 = 0,

        地址请求 = 1,
        分拣报告 = 2,

        /// <summary>
        /// PLC发出的控制指令
        /// </summary>
        PLC控制指令 = 3,

        /// <summary>
        /// PLC-->WCSACK
        /// </summary>
        PLCWCSACK = 5,
        地址回复 = 6,

        /// <summary>
        /// WCS回复的控制指令
        /// </summary>
        WCS控制指令 = 7,

        /// <summary>
        /// WCS-->PLCACK
        /// </summary>
        WCSPLCACK = 8

    }


}
