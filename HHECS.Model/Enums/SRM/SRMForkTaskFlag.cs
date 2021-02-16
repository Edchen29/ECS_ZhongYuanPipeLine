using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.SRM
{
    /// <summary>
    /// 用于写入W堆垛机CS交换区的任务标志
    /// 任务标志：0-无任务，1-库内取货，2-库内放货，3-库外入库，4库外出库， 5重新分配入库地址, 6删除任务,10任务完成
    /// </summary>
    public enum SRMForkTaskFlag
    {
        无任务 = 0,
        库内取货 = 1,
        库内放货 = 2,
        库外取货 = 3,
        库外放货 = 4,
        重新分配入库地址 = 5,
        删除任务 = 6,
        任务完成 = 10
    }
}
