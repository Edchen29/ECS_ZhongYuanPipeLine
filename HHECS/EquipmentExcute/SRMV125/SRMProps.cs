using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.SRMV125
{
    /// <summary>
    /// 堆垛机属性列表 对应版本8.4
    /// </summary>
    public enum SRMProps
    {
        /// <summary>
        /// 堆垛机编号
        /// </summary>
        Number,

        /// <summary>
        /// 操作模式
        /// </summary>
        OperationModel,

        /// <summary>
        /// 拓展模式  用于转轨堆垛机 0：正常  1：兼容故障堆垛机的执行任务
        /// </summary>
        ExpendMode,

        /// <summary>
        /// 拓展模式  最小列
        /// </summary>
        ManageSmallColumn,

        /// <summary>
        /// 拓展模式  最大列
        /// </summary>
        ManageBigColumn,
        /// <summary>
        /// 心跳
        /// </summary>
        HeartBeat,

        /// <summary>
        /// 任务限制
        /// </summary>
        TaskLimit,

        /// <summary>
        /// 水平测距
        /// </summary>
        HorizontalDistance,

        /// <summary>
        /// 起升测距
        /// </summary>
        VerticalDistance,

        /// <summary>
        /// 货叉1伸叉测距
        /// </summary>
        Fork1Distance,

        /// <summary>
        /// 货叉2伸叉测距
        /// </summary>
        Fork2Distance,

        /// <summary>
        /// 当前列
        /// </summary>
        CurrentColumn,

        /// <summary>
        /// 当前层
        /// </summary>
        CurrentLayer,

        /// <summary>
        /// 当前出/入口
        /// </summary>
        CurrentStation,

        #region 货叉1

        /// <summary>
        /// 货叉1_是否货物前超
        /// </summary>
        Fork1FrontOut,

        /// <summary>
        /// 货叉1_是否货物后超
        /// </summary>
        Fork1BehindOut,

        /// <summary>
        /// 货叉1_是否左侧外形超限
        /// </summary>
        Fork1LeftForkOut,

        /// <summary>
        /// 货叉1_是否右侧外形超限
        /// </summary>
        Fork1RightForkOut,

        /// <summary>
        /// 货叉1_超高1
        /// </summary>
        Fork1OverHeight1,

        /// <summary>
        /// 货叉1_超高2
        /// </summary>
        Fork1OverHeight2,

        /// <summary>
        /// 货叉1_超高3
        /// </summary>
        Fork1OverHeight3,

        /// <summary>
        /// 货叉1_货物超高
        /// </summary>
        Fork1OverHeight,

        /// <summary>
        /// 货叉1_货叉超时
        /// </summary>
        Fork1PalletForkTimeout,

        /// <summary>
        /// 货叉1_是否左侧极限报警
        /// </summary>
        Fork1LeftLimitAlarm,

        /// <summary>
        /// 货叉1_是否右侧极限报警
        /// </summary>
        Fork1RightLimitAlarm,

        /// <summary>
        /// 货叉1_货叉变频器故障
        /// </summary>
        Fork1ForkUuivertor,

        /// <summary>
        /// 货叉1_货叉断路器/接触器故障
        /// </summary>
        Fork1ForkBreakerOrCocontactor,

        /// <summary>
        /// 货叉1_是否货物检测传感器故障
        /// </summary>
        Fork1GoodsInspectionSensor,

        /// <summary>
        /// 货叉1_是否货叉定位传感器故障
        /// </summary>
        Fork1ForkAlignmentSensor,

        /// <summary>
        /// 货叉1_是否运行方向错误
        /// </summary>
        Fork1DirectionError,

        /// <summary>
        /// 0-无故障； 1-X 轴、 Y 轴、 货叉执行动作错误
        /// </summary>
        Fork1XYForkExcute,

        /// <summary>
        /// 货叉1_是否设定值错误
        /// </summary>
        Fork1SetValueError,

        /// <summary>
        /// 0-无故障； 1-取货任务错误（取左 2 右 2 时，左 1 右1 有货）
        /// </summary>
        Fork1PickupTaskError,

        /// <summary>
        /// 货叉1_Spare3
        /// </summary>
        Fork1Spare3,

        /// <summary>
        /// 货叉1_双重入库
        /// </summary>
        Fork1DoubleIn,

        /// <summary>
        /// 货叉1_是否空货位出库
        /// </summary>
        Fork1EmptyOut,

        /// <summary>
        /// 货叉1_是否货叉有货
        /// </summary>
        Fork1ForkHasPallet,

        /// <summary>
        /// 货叉1_货叉总故障
        /// </summary>
        Fork1ForkError,

        /// <summary>
        /// 货叉1_Spare4
        /// </summary>
        Fork1Spare4,


        #endregion

        #region 货叉2

        /// <summary>
        /// 货叉2_是否货物前超
        /// </summary>
        Fork2FrontOut,

        /// <summary>
        /// 货叉2_是否货物后超
        /// </summary>
        Fork2BehindOut,

        /// <summary>
        /// 货叉2_是否左侧外形超限
        /// </summary>
        Fork2LeftForkOut,

        /// <summary>
        /// 货叉2_是否右侧外形超限
        /// </summary>
        Fork2RightForkOut,

        /// <summary>
        /// 货叉2_超高1
        /// </summary>
        Fork2OverHeight1,

        /// <summary>
        /// 货叉2_超高2
        /// </summary>
        Fork2OverHeight2,

        /// <summary>
        /// 货叉2_超高3
        /// </summary>
        Fork2OverHeight3,

        /// <summary>
        /// 货叉2_货物超高
        /// </summary>
        Fork2OverHeight,

        /// <summary>
        /// 货叉2_货叉超时
        /// </summary>
        Fork2PalletForkTimeout,

        /// <summary>
        /// 货叉2_是否左侧极限报警
        /// </summary>
        Fork2LeftLimitAlarm,

        /// <summary>
        /// 货叉2_是否右侧极限报警
        /// </summary>
        Fork2RightLimitAlarm,

        /// <summary>
        /// 货叉2_货叉变频器故障
        /// </summary>
        Fork2ForkUuivertor,

        /// <summary>
        /// 货叉2_货叉断路器/接触器故障
        /// </summary>
        Fork2ForkBreakerOrCocontactor,

        /// <summary>
        /// 货叉2_是否货物检测传感器故障
        /// </summary>
        Fork2GoodsInspectionSensor,

        /// <summary>
        /// 货叉2_是否货叉定位传感器故障
        /// </summary>
        Fork2ForkAlignmentSensor,

        /// <summary>
        /// 货叉2_是否运行方向错误
        /// </summary>
        Fork2DirectionError,

        /// <summary>
        /// 0-无故障； 1-X 轴、 Y 轴、 货叉执行动作错误
        /// </summary>
        Fork2XYForkExcute,

        /// <summary>
        /// 货叉2_是否设定值错误
        /// </summary>
        Fork2SetValueError,

        /// <summary>
        /// 0-无故障； 1-取货任务错误（取左 2 右 2 时，左 1 右1 有货）
        /// </summary>
        Fork2PickupTaskError,

        /// <summary>
        /// 货叉2_Spare3
        /// </summary>
        Fork2Spare3,

        /// <summary>
        /// 货叉2_双重入库
        /// </summary>
        Fork2DoubleIn,

        /// <summary>
        /// 货叉2_是否空货位出库
        /// </summary>
        Fork2EmptyOut,

        /// <summary>
        /// 货叉2_是否货叉有货
        /// </summary>
        Fork2ForkHasPallet,

        /// <summary>
        /// 货叉2_货叉总故障
        /// </summary>
        Fork2ForkError,

        /// <summary>
        /// 货叉2_Spare4
        /// </summary>
        Fork2Spare4,


        #endregion

        /// <summary>
        /// 是否过载
        /// </summary>
        Overload,

        /// <summary>
        /// 是否松绳
        /// </summary>
        Rope,

        /// <summary>
        /// 是否行走变频器报警
        /// </summary>
        RunningUuivertorAlarm,

        /// <summary>
        /// 是否起升变频器报警
        /// </summary>
        RaisingUuivertorAlarm,

        /// <summary>
        /// 是否运行超时
        /// </summary>
        RunningTimeout,

        /// <summary>
        /// 是否起升超时
        /// </summary>
        RaisingTimeout,

        /// <summary>
        /// 是否水平激光数据错误
        /// </summary>
        HorizontalLaserDataError,

        /// <summary>
        /// 起升条码数据错误
        /// </summary>
        RaisingBarcodeDataError,

        /// <summary>
        /// 是否地址错
        /// </summary>
        AdressError,

        /// <summary>
        /// 主接触器断开
        /// </summary>
        MainCocontactorInterrupt,

        /// <summary>
        /// 水平断路器/制动器跳闸
        /// </summary>
        HorizontalBreakerOrBrakeInterrupt,

        /// <summary>
        /// 是否起升断路器/制动器跳闸
        /// </summary>
        RaisingBreakerOrBrakeInterrupt,

        /// <summary>
        /// 是否水平前端超限（前进终点）
        /// </summary>
        HorizontalLeadingendOut,

        /// <summary>
        /// 是否水平后端超限（后退终点）
        /// </summary>
        HorizontalTrailingendOut,

        /// <summary>
        /// 是否垂直上端超限（上升终点）
        /// </summary>
        VerticalHorizontalLeadingendOut,

        /// <summary>
        /// 垂直下端超限（下降终点）
        /// </summary>
        VerticalHorizontalTrailingendOut,

        /// <summary>
        /// 0-无过载； 1-X 轴变频器未准备就绪
        /// </summary>
        OverloadError,

        /// <summary>
        /// 0-无松绳； 1-Y 轴变频器未准备就绪
        /// </summary>
        LooseRope,

        /// <summary>
        /// 0-无急停； 1-操作盒急停被按下
        /// </summary>
        Stop1,

        /// <summary>
        /// 0-无急停； 1-车载控制柜急停被按下
        /// </summary>
        Stop2,

        /// <summary>
        /// 0-无故障； 1-前安全门被打开
        /// </summary>
        Breakdown1,

        /// <summary>
        /// 0-无故障； 1-后安全门被打开
        /// </summary>
        Breakdown2,

        /// <summary>
        /// 0-无故障； 1-红外通信故障
        /// </summary>
        InfraredError,

        /// <summary>
        /// 货叉1_任务执行
        /// </summary>
        Fork1TaskExcuteStatus,

        /// <summary>
        /// 货叉1_任务号
        /// </summary>
        Fork1TaskNo,

        /// <summary>
        /// 货叉1_任务类型
        /// </summary>
        Fork1TaskType,


        /// <summary>
        /// 货叉2_任务执行
        /// </summary>
        Fork2TaskExcuteStatus,

        /// <summary>
        /// 货叉2_任务号
        /// </summary>
        Fork2TaskNo,

        /// <summary>
        /// 货叉2_任务类型
        /// </summary>
        Fork2TaskType,

        /// <summary>
        /// 货叉1 0-无错误； 1-WCS 下发取货任务时，货叉有货
        /// </summary>
        Fork1Error1,

        /// <summary>
        /// 货叉1 0-无错误； 1-WCS 下发放货任务时，货叉无货
        /// </summary>
        Fork1Error2,

        /// <summary>
        /// 货叉1 0-无错误； 1-WCS 下发任务时，任务排号错误
        /// </summary>
        Fork1Error3,

        /// <summary>
        /// 货叉1 0-无错误； 1-WCS 下发任务时，任务层数错误
        /// </summary>
        Fork1Error4,

        /// <summary>
        /// 货叉1 0-无错误； 1-WCS 下发任务时，任务列数错误
        /// </summary>
        Fork1Error5,

        /// <summary>
        /// 货叉1 0-无错误； 1-WCS 下发任务时，任务出入口错误
        /// </summary>
        Fork1Error6,

        /// <summary>
        /// 货叉1 0-无错误； 1-库内放货，货物高度与货架高度不匹配
        /// </summary>
        Fork1Error7,

        /// <summary>
        /// 货叉2 0-无错误； 1-WCS 下发取货任务时，货叉有货
        /// </summary>
        Fork2Error1,

        /// <summary>
        /// 货叉2 0-无错误； 1-WCS 下发放货任务时，货叉无货
        /// </summary>
        Fork2Error2,

        /// <summary>
        /// 货叉2 0-无错误； 1-WCS 下发任务时，任务排号错误
        /// </summary>
        Fork2Error3,

        /// <summary>
        /// 货叉2 0-无错误； 1-WCS 下发任务时，任务层数错误
        /// </summary>
        Fork2Error4,

        /// <summary>
        /// 货叉2 0-无错误； 1-WCS 下发任务时，任务列数错误
        /// </summary>
        Fork2Error5,

        /// <summary>
        /// 货叉2 0-无错误； 1-WCS 下发任务时，任务出入口错误
        /// </summary>
        Fork2Error6,

        /// <summary>
        /// 货叉2 0-无错误； 1-库内放货，货物高度与货架高度不匹配
        /// </summary>
        Fork2Error7,

        /// <summary>
        /// 货叉1故障代码
        /// </summary>
        Fork1ErrorCode,

        /// <summary>
        /// 货叉2故障代码
        /// </summary>
        Fork2ErrorCode,

        /// <summary>
        /// 行走轴故障代码
        /// </summary>
        WalkErrorCode,

        /// <summary>
        /// 升降轴故障代码
        /// </summary>
        RaiseErrorCode,


        #region WCS

        /// <summary>
        /// 货叉动作类型
        /// </summary>
        WCSForkAction,

        /// <summary>
        /// 货叉1_任务标志
        /// </summary>
        WCSFork1TaskFlag,

        /// <summary>
        /// 高速堆垛机  二个任务同时下发时用于任务过账确认的
        /// </summary>
        WCSTaskAccount,

        /// <summary>
        /// 货叉1_取放货地址:  行
        /// </summary>
        WCSFork1Row,

        /// <summary>
        /// 货叉1_取放货列
        /// </summary>
        WCSFork1Column,

        /// <summary>
        /// 货叉1_取放货层
        /// </summary>
        WCSFork1Layer,

        /// <summary>
        /// 货叉1_取放货出入口
        /// </summary>
        WCSFork1Station,

        /// <summary>
        /// 货叉1_任务号
        /// </summary>
        WCSFork1Task,

        /// <summary>
        /// 货叉2_任务标志
        /// </summary>
        WCSFork2TaskFlag,

        /// <summary>
        /// 货叉2_取放货地址:  行
        /// </summary>
        WCSFork2Row,

        /// <summary>
        /// 货叉2_取放货列
        /// </summary>
        WCSFork2Column,

        /// <summary>
        /// 货叉2_取放货层
        /// </summary>
        WCSFork2Layer,

        /// <summary>
        /// 货叉2_取放货出入口
        /// </summary>
        WCSFork2Station,

        /// <summary>
        /// 货叉2_任务号
        /// </summary>
        WCSFork2Task,

        WCSHeartBeat,

        #endregion



    }
}
