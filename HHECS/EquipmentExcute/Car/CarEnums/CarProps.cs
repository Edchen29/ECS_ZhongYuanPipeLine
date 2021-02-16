using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Car.CarEnums
{
    public enum CarProps
    {
        #region PLC输出
        /// <summary>
        /// 输出设备状态
        /// </summary>
        carStatus,
        /// <summary>
        /// 设备故障
        /// </summary>
        carError,
        /// <summary>
        /// 输出控制方式
        /// </summary>
        controlMode,
        /// <summary>
        /// 小车到达
        /// </summary>
        arriveMessage,
        /// <summary>
        /// 输出任务号
        /// </summary>
        TaskHeaderID,
        /// <summary>
        /// 输出子任务号
        /// </summary>
        TaskCarId,
        /// <summary>
        /// 输出作业类型
        /// </summary>
        actionType,
        /// <summary>
        /// 输出当前排
        /// </summary>
        row,
        /// <summary>
        /// 输出穿梭板上是否有货
        /// </summary>
        hasPallet,
        #endregion

        #region WCS响应
        /// <summary>
        /// 输入任务号
        /// </summary>
        wcsTaskHeaderId,
        /// <summary>
        /// 输入子任务号
        /// </summary>
        wcsTaskCarId,
        /// <summary>
        /// 输入作业类型
        /// </summary>
        wcsActionType,
        /// <summary>
        /// 输入取货排
        /// </summary>
        wcsStartRow,
        /// <summary>
        /// 输入放货排
        /// </summary>
        wcsDestinationRow,
        /// <summary>
        /// 输入开启信号
        /// </summary>
        wcsSwitch,
        /// <summary>
        /// 输入删除任务指令
        /// </summary>
        wcsDeleteCommand,
        /// <summary>
        /// 输入任务过账确认
        /// </summary>
        wcsConfirmTaskFinish,
        /// <summary>
        /// 输入确认小车到达信号
        /// </summary>
        wcsArriveMessage,
        /// <summary>
        /// 控制模式
        /// </summary>
        wcsControlMode,
        /// <summary>
        /// 输入复位信号
        /// </summary>
        wcsResetCommand,
        #endregion

        #region ERROR
        /// <summary>
        /// 空出，取货位置无货，无法取货
        /// </summary>
        Voidance,
        /// <summary>
        /// 满入，目的地有货，无法放货
        /// </summary>
        Full,
        /// <summary>
        /// 排错误
        /// </summary>
        RowError,
        /// <summary>
        /// 取货任务错误
        /// </summary>
        PickError,
        /// <summary>
        /// 放货任务错误
        /// </summary>
        PutError,
        /// <summary>
        /// 外部故障
        /// </summary>
        ExternalFault,
        /// <summary>
        /// 驶离超时
        /// </summary>
        LeaveTimeFault,
        /// <summary>
        /// 驶入超时
        /// </summary>
        EnterTimeFault,
        /// <summary>
        /// 升降传感器故障
        /// </summary>
        LiftSensorFault,
        /// <summary>
        /// 取货传感器故障
        /// </summary>
        PickSensorFault,
        /// <summary>
        /// 尾端取货，托盘位置错误
        /// </summary>
        RearPalletFault,
        /// <summary>
        /// 小车左侧驶离超限
        /// </summary>
        LeftLeaveOver,
        /// <summary>
        /// 小车右侧驶离超限
        /// </summary>
        RightLeaveOver,
        /// <summary>
        /// 小车左侧驶入超限
        /// </summary>
        LeftEnterOver,
        /// <summary>
        /// 小车右侧驶入超限
        /// </summary>
        RightEnterOver,
        /// <summary>
        /// 小车左侧回端头超限
        /// </summary>
        LeftBackOriginOver,
        /// <summary>
        /// 小车右侧回端头超限
        /// </summary>
        RightBackOriginOver,
        /// <summary>
        /// 端头托盘位置错误
        /// </summary>
        PalletPositionFault,
        /// <summary>
        /// 小车驶入检测不到堆垛机
        /// </summary>
        DetectionSRM_Fault,
        /// <summary>
        /// 小车超出巷道
        /// </summary>
        CarOverRoadway,
        /// <summary>
        /// 小车位置信息错误
        /// </summary>
        CarPositionFault,
        /// <summary>
        /// 小车运行错误，未与堆垛机对齐，驶出巷道
        /// </summary>
        CarRunFault,
        /// <summary>
        /// 小车左停止故障
        /// </summary>
        LeftStopSensorFault,
        /// <summary>
        /// 小车右停止故障
        /// </summary>
        RightStopSensorFault,
        /// <summary>
        /// 小车在堆垛机居中故障
        /// </summary>
        CarNoCenterFault,
        /// <summary>
        /// 编码器故障
        /// </summary>
        EncodeFault,
        /// <summary>
        /// 取货错误，检测不到货物
        /// </summary>
        PickEmptyPalletFaut,
        /// <summary>
        /// 放货任务与传感器检测不符
        /// </summary>
        PutPositiomFault,
        /// <summary>
        /// 货物检测传感器故障
        /// </summary>
        UnoccupiedPE_Fault,
        /// <summary>
        /// 运行超时
        /// </summary>
        RunTimeFault,
        #endregion
    }
}
