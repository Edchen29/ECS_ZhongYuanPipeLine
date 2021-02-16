using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Controls
{
    /// <summary>
    /// MachiningType机加工设备类监控类
    /// </summary>
    public enum MachiningTypeMonitorProps
    {
        /// <summary>
        /// WCS回复允许上料完成
        /// </summary>
        WCS_Allow_Load,

        /// <summary>
        /// wcs准备回复加工
        /// </summary>
        WCS_Wroking,
        
        /// <summary>
        /// 工序任务ID
        /// </summary>
        Step_Trace_ID,
        
        /// <summary>
        ///请求上料
        /// </summary>
        Request_Load,
        
        /// <summary>
        /// 上料等待完成
        /// </summary>
        Task_OK,
        
        /// <summary>
        /// 请求下料
        /// </summary>
        Request_Blank,

        /// <summary>
        /// WCS写入任务ID
        /// </summary>
        WCS_Step_Trace_Id,
        
        /// <summary>
        ///机加请求生产
        /// </summary>
        Request_Wroking,

        /// <summary>
        /// 机床异常1
        /// </summary>
        Abnormal_1,
        /// <summary>
        /// 机床异常2
        /// </summary>
        Abnormal_2,

        ///// <summary>
        ///// 工件类型
        ///// </summary>
        //TYPE,

        ///// <summary>
        ///// 工件类型
        ///// </summary>
        //TYPE_FeedBack,
    }
}
