using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.MachineTool
{
    /// <summary>
    ///ECS回复机床加工属性
    /// </summary>
    public enum EcsToMachineToolPorps
    {
        /// <summary>
        /// ECS回复上料请求
        /// </summary>
        WCS_Allow_Load,
        ///// <summary>
        ///// ECS回复下料请求
        ///// </summary>
        //WCS_Request_Blank,
        /// <summary>
        /// ECS回复开始生产
        /// </summary>
        WCS_Wroking,
        /// <summary>
        /// 任务id
        /// </summary>
        WCS_Step_Trace_Id,
        ///// <summary>
        ///// 工件类型
        ///// </summary>
        //WCS_TYPE,

    }
}
