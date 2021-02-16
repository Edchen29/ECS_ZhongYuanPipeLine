using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums
{
    public enum MaterialCallStatus
    {
        呼叫配送 = 10,
        配送开始 = 50,
        配送到位 = 85,
        配送完成 = 90,
        配送投入使用 = 95,
        配送使用完毕 = 100,
    }
}
