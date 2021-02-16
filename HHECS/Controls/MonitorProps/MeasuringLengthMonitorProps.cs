using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Controls.MonitorProps
{
    public enum MeasuringLengthMonitorProps
    {
        /// <summary>
        ///操作模式 操作模式:1-维修; 2-手动; 3-机载操作；4-单机自动；5-联机
        /// </summary>
        OperationModel,

        /// <summary>
        /// 站台总故障
        /// </summary>
        TotalError,

        /// <summary>
        ///请求下料-报文
        /// </summary>
        RequestMessage,

        /// <summary>
        ///请求下料-电器编号
        /// </summary>
        RequestNumber,

        /// <summary>
        ///  请求下料-任务号
        /// </summary>
        RequestTaskId,

        /// <summary>
        ///   请求下料-条码
        /// </summary>
        RequestBarcode,

        /// <summary>
        ///  请求下料-工件类型
        /// </summary>
        RequestProductId,

        ///<summary>
        ///请求回复-货物材料
        /// </summary>
        RequestMaterial,

        ///<summary>
        ///请求回复-货物长度
        /// </summary>
        RequestLength,

        ///<summary>
        ///请求回复-货物直径
        /// </summary>
        RequestDiameter,

        ///<summary>
        ///请求回复-货物壁厚
        /// </summary>
        RequestThicknessl,

        /// <summary>
        ///   请求下料-备用
        /// </summary>
        RequestBackup,

        /// <summary>
        /// WCS请求下料回复-报文
        /// </summary>
        WCSReplyMessage,

        /// <summary>
        /// WCS请求下料回复-电器编码
        /// </summary>
        WCSReplyNumber,

        /// <summary>
        ///  WCS请求下料回复-条码
        /// </summary>
        WCSReplyBarcode,
        /// <summary>
        ///  WCS请求下料回复-目标地址
        /// </summary>
        WCSReplyAddress,
        /// <summary>
        /// WCS请求下料回复-工件类型
        /// </summary>
        WCSReplyProductId,
        /// <summary>
        ///  WCS请求下料回复-任务号
        /// </summary>
        WCSReplyTaskId,
        /// <summary>
        ///  WCS请求下料回复-备用
        /// </summary>
        WCSReplyBackup,

        ///<summary>
        ///WCS请求下料回复-货物材料
        /// </summary>
        WCSReplyMaterial,

        ///<summary>
        ///WCS请求下料回复-货物长度
        /// </summary>
        WCSReplyLength,

        ///<summary>
        ///WCS请求下料回复-货物直径
        /// </summary>
        WCSReplyDiameter,

        ///<summary>
        ///WCS请求下料回复-货物壁厚
        /// </summary>
        WCSReplyThickness,

    }
}
