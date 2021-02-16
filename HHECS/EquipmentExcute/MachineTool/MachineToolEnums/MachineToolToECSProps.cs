using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.MachineTool
{
    /// <summary>
    /// 机床加工请求ECS属性
    /// </summary>
    public  enum  MachineToolToECSProps
    {
        /// <summary>
        /// 请求上料
        /// </summary>
        Request_Load,
        /// <summary>
        /// 上料等待完成
        /// </summary>
        Task_OK,
        /// <summary>
        /// 机加请求生产
        /// </summary>
        Request_Wroking,
        /// <summary>
        /// 请求下料
        /// </summary>
        Request_Blank,
        /// <summary>
        /// 任务id
        /// </summary>
        Step_Trace_ID,
        /// <summary>
        /// 机床异常1
        /// </summary>
        Abnormal_1,
        /// <summary>
        /// 机床异常2
        /// </summary>
        Abnormal_2,
    }
}
