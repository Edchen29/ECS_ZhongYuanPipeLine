using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums.PipeLine
{
    public enum PipeLineProps
    {
        #region 状态

        /// <summary>
        /// PLC操作模式
        /// </summary>
        OperationModel,

        /// <summary>
        /// PLC站台总故障
        /// </summary>
        TotalError,

        /// <summary>
        /// PLC站台是否有货
        /// </summary>
        HasGoods,

        #endregion

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
        /// PLC地址请求-任务号
        /// </summary>
        RequestTaskId,

        /// <summary>
        /// PLC地址请求-条码
        /// </summary>
        RequestBarcode,

        /// <summary>
        /// PLC地址请求-工件类型
        /// </summary>
        RequestProductId,

        /// <summary>
        /// PLC地址请求-货物材料
        /// </summary>
        RequestMaterial,

        /// <summary>
        /// PLC地址请求-货物长度
        /// </summary>
        RequestLength,

        /// <summary>
        /// PLC地址请求-货物直径
        /// </summary>
        RequestDiameter,

        /// <summary>
        /// PLC地址请求-货物壁厚
        /// </summary>
        RequestThickness,

        /// <summary>
        /// 地址请求备用
        /// </summary>
        RequestBackup,
        #endregion

        #region 回复地址请求

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
        /// WCS回复请求-目标地址
        /// </summary>
        WCSReplyAddress,

        /// <summary>
        /// WCS回复请求-任务号
        /// </summary>
        WCSReplyTaskId,

        /// <summary>
        /// WCS回复请求-条码
        /// </summary>
        WCSReplyBarcode,

        /// <summary>
        /// WCS回复请求-工件类型
        /// </summary>
        WCSReplyProductId,

        /// <summary>
        /// WCS回复请求-货物材料
        /// </summary>
        WCSReplyMaterial,

        /// <summary>
        /// WCS回复请求-货物长度
        /// </summary>
        WCSReplyLength,

        /// <summary>
        /// WCS回复请求-货物直径
        /// </summary>
        WCSReplyDiameter,

        /// <summary>
        /// WCS回复请求-货物壁厚
        /// </summary>
        WCSReplyThickness,

        /// <summary>
        /// WCS地址回复-备用
        /// </summary>
        WCSReplyBackup,

        #endregion

        #region 位置到达

        /// <summary>
        /// PLC位置到达-报文
        /// </summary>
        ArriveMessage,

        /// <summary>
        /// PLC位置到达-结果
        /// </summary>
        ArriveResult,

        /// <summary>
        /// PLC位置到达-实际到达地址
        /// </summary>
        ArriveRealAddress,

        /// <summary>
        /// PLC位置到达-WCS分配地址
        /// </summary>
        ArriveAllocationAddress,

        /// <summary>
        /// PLC位置到达-任务号
        /// </summary>
        ArriveTaskId,

        /// <summary>
        /// PLC位置到达-条码
        /// </summary>
        ArriveBarcode,

        /// <summary>
        /// PLC位置到达-货物材料
        /// </summary>
        ArriveMaterial,

        /// <summary>
        /// PLC位置到达-货物长度
        /// </summary>
        ArriveLength,

        /// <summary>
        /// PLC位置到达-货物直径
        /// </summary>
        ArriveDiameter,

        /// <summary>
        /// PLC位置到达-货物壁厚
        /// </summary>
        ArriveThickness,

        /// <summary>
        /// PLC位置到达-备用
        /// </summary>

        #endregion

        #region 回复到达

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
        /// WCSACK回复到达-任务号
        /// </summary>
        WCSACKTaskId,
        /// <summary>
        /// WCSACK回复到达-条码
        /// </summary>
        WCSACKBarcode,
        /// <summary>
        /// WCSACK回复到达-工件类型
        /// </summary>
        WCSACKProductId,
        /// <summary>
        /// WCSACK回复到达-货物材料
        /// </summary>
        WCSACKMaterial,
        /// <summary>
        /// WCSACK回复到达-货物长度
        /// </summary>
        WCSACKLength,
        /// <summary>
        /// WCSACK回复到达-货物直径
        /// </summary>
        WCSACKDiameter,
        /// <summary>
        /// WCSACK回复到达-货物壁厚
        /// </summary>
        WCSACKThickness,
        /// <summary>
        /// WCSACK-备用
        /// </summary>
        WCSACKBackup


        #endregion

    }
}
