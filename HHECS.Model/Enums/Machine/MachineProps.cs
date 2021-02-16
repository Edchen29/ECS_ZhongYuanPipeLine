using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.Machine
{
    public enum MachineProps
    {
        #region 状态
        /// <summary>
        /// 机器操作模式
        /// </summary>
        OperationModel,
        /// <summary>
        /// 机器总故障
        /// </summary>
        TotalError,
        #endregion

        #region PLC请求下料
        /// <summary>
        /// PLC请求下料-报文
        /// </summary>
        RequestMessage,

        /// <summary>
        /// PLC请求下料-电器编号
        /// </summary>
        RequestLoadStatus,

        /// <summary>
        /// PLC请求下料-电器编码
        /// </summary>
        RequestNumber,

        /// <summary>
        /// PLC请求下料-任务号
        /// </summary>
        RequestTaskId,

        /// <summary>
        /// PLC请求下料-条码
        /// </summary>
        RequestBarcode,

        /// <summary>
        /// PLC请求下料-工件类型
        /// </summary>
        RequestProductId,

        /// <summary>
        /// PLC请求下料-货物材料
        /// </summary>
        RequestMaterial,

        /// <summary>
        /// PLC请求下料-货物长度
        /// </summary>
        RequestLength,

        /// <summary>
        /// PLC请求下料-货物直径
        /// </summary>
        RequestDiameter,

        /// <summary>
        /// PLC请求下料-货物壁厚
        /// </summary>
        RequestThickness,

        /// <summary>
        /// PLC请求下料-备用
        /// </summary>
        RequestBackup,
        #endregion

        #region WCS回复允许下料

        /// <summary>
        /// WCS回复允许下料-报文
        /// </summary>
        WCSReplyMessage,

        /// <summary>
        ///   WCS回复允许下料-电器编码
        /// </summary>
        WCSReplyNumber,

        /// <summary>
        ///   WCS回复允许下料-任务号
        /// </summary>
        WCSReplyTaskId,

        /// <summary>
        ///  WCS回复允许下料-条码
        /// </summary>
        WCSReplyBarcode,

        /// <summary>
        ///  WCS回复允许下料-目标地址
        /// </summary>
        WCSReplyAddress,

        /// <summary>
        ///    WCS回复允许下料-工件类型
        /// </summary>
        WCSReplyProductId,

        /// <summary>
        ///     WCS回复允许下料-货物材料
        /// </summary>
        WCSReplyMaterial,

        /// <summary>
        ///   WCS回复允许下料-货物长度
        /// </summary>
        WCSReplyLength,

        /// <summary>
        ///     WCS回复允许下料-货物直径
        /// </summary>
        WCSReplyDiameter,

        /// <summary>
        ///    WCS回复允许下料-货物壁厚
        /// </summary>
        WCSReplyThickness,

        /// <summary>
        ///   WCS回复允许下料-备用
        /// </summary>
        WCSReplyBackup,

        #endregion

        #region PLC请求上料

        /// <summary>
        /// PLC请求上料-报文 
        /// </summary>
        ArriveMessage,

        /// <summary>
        /// PLC请求上料-结果  
        /// </summary>
        ArriveResult,

        /// <summary>
        /// PLC请求上料-实际到达地址  
        /// </summary>
        ArriveRealAddress,

        /// <summary>
        /// PLC位置到达-任务号 
        /// </summary>
        ArriveTaskId,

        /// <summary>
        /// PLC位置到达-条码  
        /// </summary>
        ArriveBarcode,

        /// <summary>
        /// PLC请求上料-备用  
        /// </summary>
        ArriveBackup,

        #endregion

        #region WCS回复允许上料

        /// <summary>
        /// WCS回复允许上料-报文 
        /// </summary>
        WCSACKMessage,

        /// <summary>
        /// WCS回复允许上料-电器编码
        /// </summary>
        WCSACKNumber,

        /// <summary>
        /// WCS回复允许上料-任务号   
        /// </summary>
        WCSACKTaskId,

        /// <summary>
        /// WCS回复允许上料-条码 
        /// </summary>
        WCSACKBarcode,

        /// <summary>
        /// WCS回复允许上料-工件类型  
        /// </summary>
        WCSACKProductId,

        /// <summary>
        /// WCS回复允许上料-货物材料
        /// </summary>
        WCSACKMaterial,

        /// <summary>
        /// WCS回复允许上料-货物长度  
        /// </summary>
        WCSACKLength,

        /// <summary>
        /// WCS回复允许上料-货物直径 
        /// </summary>
        WCSACKDiameter,

        /// <summary>
        /// WCS回复允许上料-货物壁厚  
        /// </summary>
        WCSACKThickness,

        /// <summary>
        /// WCS回复允许上料-备用 
        /// </summary>
        WCSACKBackup,

        #endregion

        #region 翻转
        /// <summary>
        /// PLC请求翻转
        /// </summary>
        RequestFlip,

        /// <summary>
        /// WCS允许翻转
        /// </summary>
        WCSAllowFlip,
        #endregion

        #region 切割
        /// <summary>
        /// PLC请求切割-报文
        /// </summary>
        RequestCut,

        /// <summary>
        /// PLC请求切割-任务号
        /// </summary>
        RequestCutTaskId,

        /// <summary>
        /// WCS回复切割-结果
        /// </summary>
        WCSAllowCut,

        /// <summary>
        /// WCS回复切割-管段任务号
        /// </summary>
        WCSCutTaskId,

        /// <summary>
        /// WCS回复切割-管段材料
        /// </summary>
        WCSCutMaterial,

        /// <summary>
        /// WCS回复切割-管段长度
        /// </summary>
        WCSCutLength,

        /// <summary>
        /// WCS回复切割-管段直径
        /// </summary>
        WCSCutDiameter,

        /// <summary>
        /// WCS回复切割-管段壁厚
        /// </summary>
        WCSCutThickness,

        /// <summary>
        /// PLC请求打印-报文
        /// </summary>
        RequestPint,

        /// <summary>
        /// PLC请求打印-任务号
        /// </summary>
        RequestPintTaskId,

        /// <summary>
        /// WCS回复打印-结果
        /// </summary>
        WCSPint,



        #endregion


    }
}
