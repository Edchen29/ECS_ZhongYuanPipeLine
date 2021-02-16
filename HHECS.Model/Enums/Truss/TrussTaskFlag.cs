using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Truss
{
    /// <summary>
    /// 用于写入W堆垛机CS交换区的任务标志
    /// 任务标志：0-无任务，1-机械手取货，2-机械手放货，3-机械手行走， 5重新分配入库地址, 6删除任务,10任务完成
    /// </summary>
    public enum TrussTaskFlag
    {
        无任务 = 0,
        机械手取货 = 1,
        机械手放货 = 2,
        机械手行走 = 3,
        重新分配放货地址 = 5,
        删除任务 = 6,
        任务完成 = 10
    }
}
