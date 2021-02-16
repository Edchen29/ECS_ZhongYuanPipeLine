using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Bevel.BevelEnums
{
    public   enum  ECSToBevelProps
    {
        /// <summary>
        /// ECS回复上料请求
        /// </summary>
        WCS_Allow_Load,
        ///// <summary>
        ///// ECS回复下料请求
        ///// </summary>
        WCS_Request_Blank,
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
        /// <summary>
        /// WCS允许翻转
        /// </summary>
        WCS_Allow_Flip,

        /// <summary>
        /// 材料
        /// </summary>
        WCS_Pipe_Material,

        /// <summary>
        /// 长度
        /// </summary>
        WCS_Pipe_Length,

        /// <summary>
        /// 直径
        /// </summary>
        WCS_Pipe_Diameter,

        /// <summary>
        /// 壁厚
        /// </summary>
        WCS_Pipe_Thickness,

    }
}
