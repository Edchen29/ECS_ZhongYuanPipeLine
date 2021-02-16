using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Car
{
    public  enum CarToECS
    {
        /// <summary>
        /// 输出设备状态
        /// </summary>
        carStatus,
        /// <summary>
        /// 设备故障
        /// </summary>
        carError,
        /// <summary>
        /// 输出控制方式
        /// </summary>
        controlMode,
        /// <summary>
        /// 小车到达
        /// </summary>
        arriveMessage,
        /// <summary>
        /// 输出任务号
        /// </summary>
        TaskHeaderID,
        /// <summary>
        /// 输出子任务号
        /// </summary>
        TaskCarId,
        /// <summary>
        /// 输出作业类型
        /// </summary>
        actionType,
        /// <summary>
        /// 输出当前排
        /// </summary>
        row,
        /// <summary>
        /// 输出穿梭板上是否有货
        /// </summary>
        hasPallet,
        

    }
}
