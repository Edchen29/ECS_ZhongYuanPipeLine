using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.LengthMeasureing.LengthMeasuringEnums
{
    public enum MeasuringMachineToECSProps
    {
        ///<summary>
        ///请求上料
        /// </summary>
        Request_Load,
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
        /// <summary>
        /// 原材料长度
        /// </summary>
        Length,
        ///<summary>
        ///内径
        /// </summary>
        Diameter,
        /// <summary>
        /// 壁厚
        /// </summary>
        Thickness,
        /// <summary>
        /// 材料类型
        /// </summary>
        Material,

    }
}
