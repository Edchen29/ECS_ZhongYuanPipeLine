using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Station
{
    /// <summary>
    /// 站台控制指令发送的报文类型
    /// </summary>
    public enum StationMessageType
    {
        空托盘组补给 = 1,
        周转箱组补给 = 11,
        站台启用关闭 = 3,
        拼托完成信号 = 6,
        托盘码盘完毕 = 12,
        料箱码完 = 7
    }
}
