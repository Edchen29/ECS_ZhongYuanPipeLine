using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Task
{
    public enum DistributeStatus
    {
        任务生成 = 1,
        小车响应 = 10,
        回收料框开始 = 20,
        回收料框到位 = 30,
        回收料框完成 = 40,
        配送开始 = 50,
        配送装料完成 = 60,
        配送到达检测点 = 70,
        配送检测失败 = 75,
        配送检测通过 = 80,
        配送到位 = 85,
        配送完成 = 90,
        配送投入使用 = 95,
        配送使用完毕 = 100,
        取工件开始 = 200,
        取工件到达 = 210,
        取工件完成 = 220,
        放料车开始 = 250,
        放料车到达 = 260,
        放料车完成 = 270,
    }
}
