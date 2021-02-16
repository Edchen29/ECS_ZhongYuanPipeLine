using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.LengthMeasureing.LengthMeasuringEnums
{
    public enum ECSToMeasuringMachineProps
    {
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
        /// <summary>
        /// 原材料长度
        /// </summary>
        WCS_RequestLength,
        ///<summary>
        ///内径
        /// </summary>
        WCS_Diameter,
        /// <summary>
        /// 壁厚
        /// </summary>
        WCS_Thickness,
        /// <summary>
        /// 材料类型
        /// </summary>
        WCS_Material,
    }
}
