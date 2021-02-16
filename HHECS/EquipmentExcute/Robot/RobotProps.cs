using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Robot
{
    public enum RobotProps
    {
        #region 地址请求
        /// <summary>
        /// 地址请求
        /// </summary>
        RequestMessage,

        /// <summary>
        /// 地址请求-装载状态
        /// </summary>
        RequestLoadStatus,

        /// <summary>
        /// 地址请求-读码器编号
        /// </summary>
        RequestNumber,

        /// <summary>
        /// 地址请求-条码
        /// </summary>
        RequestBarcode,

        /// <summary>
        /// 地址请求-重量
        /// </summary>
        RequestWeight,

        /// <summary>
        /// 地址请求-长度
        /// </summary>
        RequestLength,

        /// <summary>
        /// 地址请求-宽度
        /// </summary>
        RequestWidth,

        /// <summary>
        /// 地址请求-高度
        /// </summary>
        RequestHeight,

        /// <summary>
        /// 地址请求-RetCode
        /// </summary>
        RequestRetCode,

        /// <summary>
        /// 地址请求备用
        /// </summary>
        RequestBackup,
        #endregion

        #region 位置到达

        /// <summary>
        /// 位置到达
        /// </summary>
        ArriveMessage,

        /// <summary>
        /// 位置到达-结果
        /// </summary>
        ArriveResult,

        /// <summary>
        /// 位置到达-实际到达地址
        /// </summary>
        ArriveRealAddress,

        /// <summary>
        /// 位置到达-WCS分配地址
        /// </summary>
        ArriveAllocationAddress,

        /// <summary>
        /// 位置到达-条码
        /// </summary>
        ArriveBarcode,


        /// <summary>
        /// 位置到达-填充位
        /// </summary>
        ArrivePaddingBit,

        #endregion

        #region 控制指令

        /// <summary>
        /// 控制指令
        /// </summary>
        ControlMessage,

        /// <summary>
        /// 控制指令-类型
        /// </summary>
        ControlType,

        /// <summary>
        /// 控制指令-站台编号
        /// </summary>
        ControlNumber,

        /// <summary>
        /// 控制指令-备用
        /// </summary>
        ControlBackup,

        #endregion

        #region ACK

        /// <summary>
        /// ACK
        /// </summary>
        ACKMessage,

        /// <summary>
        /// ACK装载状态
        /// </summary>
        ACKLoadStatus,

        /// <summary>
        /// ACK-站台编码
        /// </summary>
        ACKNumber,

        /// <summary>
        /// ACK备用
        /// </summary>
        ACKBackup,

        #endregion

        #region 请求回复

        /// <summary>
        /// WCS地址回复
        /// </summary>
        WCSReplyMessage,

        /// <summary>
        /// WCS地址回复-装载状态
        /// </summary>
        WCSReplyLoadStatus,

        /// <summary>
        /// WCS地址回复-站台编码
        /// </summary>
        WCSReplyNumber,

        /// <summary>
        /// WCS地址回复-条码
        /// </summary>
        WCSReplyBarcode,

        /// <summary>
        /// WCS地址回复-重量
        /// </summary>
        WCSReplyWeight,

        /// <summary>
        /// WCS地址回复-长度
        /// </summary>
        WCSReplyLength,

        /// <summary>
        /// WCS地址回复-宽度
        /// </summary>
        WCSReplyWidth,

        /// <summary>
        /// WCS地址回复-高度
        /// </summary>
        WCSReplyHeight,

        /// <summary>
        /// WCS地址回复-目标地址
        /// </summary>
        WCSReplyAddress,

        /// <summary>
        /// WCS地址回复-备用
        /// </summary>
        WCSReplyBackup,

        #endregion

        #region 控制回复

        /// <summary>
        /// WCS控制指令
        /// </summary>
        WCSControlMessage,

        /// <summary>
        /// WCS控制指令-报文类型
        /// </summary>
        WCSControlType,

        /// <summary>
        /// WCS控制指令-站台编码
        /// </summary>
        WCSControlNumber,

        /// <summary>
        /// WCS控制指令-备用
        /// </summary>
        WCSControlBackup,

        #endregion

        #region WCSACK

        /// <summary>
        /// WCSACK报文
        /// </summary>
        WCSACKMessage,

        /// <summary>
        /// WCSACK-装载状态
        /// </summary>
        WCSACKLoadStatus,

        /// <summary>
        /// WCSACK-站台编码
        /// </summary>
        WCSACKNumber,

        /// <summary>
        /// WCSACK-备用
        /// </summary>
        WCSACKBackup,

        #endregion

    }
}
