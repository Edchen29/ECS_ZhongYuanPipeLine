using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Controls.MonitorProps
{
    public enum CarMonitorProps
    {
        /// <summary>
        /// 输入任务号
        /// </summary>
        wcsTaskHeaderId ,
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
        /// <summary>
        /// 输出设备号
        /// </summary>
        carNo,
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
        /// <summary>
        /// 报警
        /// </summary>
        Voidance,
        /// <summary>
        /// 报警
        /// </summary>
        Full,
        /// <summary>
        /// 报警
        /// </summary>
        RowError,
        /// <summary>
        /// 报警
        /// </summary>
        PickError,
        /// <summary>
        /// 报警
        /// </summary>
        PutError,
        /// <summary>
        /// 报警
        /// </summary>
        ExternalFault,
        /// <summary>
        /// 报警
        /// </summary>
        LeaveTimeFault,
        /// <summary>
        /// 报警
        /// </summary>
        EnterTimeFault,
        /// <summary>
        /// 报警
        /// </summary>
        LiftSensorFault,
        /// <summary>
        /// 报警
        /// </summary>
        PickSensorFault,
        /// <summary>
        /// 报警
        /// </summary>
        RearPalletFault,
        /// <summary>
        /// 报警
        /// </summary>
        LeftLeaveOver,
        /// <summary>
        /// 报警
        /// </summary>
        RightLeaveOver,
        /// <summary>
        /// 报警
        /// </summary>
        LeftEnterOver,
        /// <summary>
        /// 报警
        /// </summary>
        RightEnterOver,
        /// <summary>
        /// 报警
        /// </summary>
        LeftBackOriginOver,
        /// <summary>
        /// 报警
        /// </summary>
        RightBackOriginOver,
        /// <summary>
        /// 报警
        /// </summary>
        PalletPositionFault,
        /// <summary>
        /// 报警
        /// </summary>
        DetectionSRM_Fault,
        /// <summary>
        /// 报警
        /// </summary>
        CarOverRoadway,
        /// <summary>
        /// 报警
        /// </summary>
        CarPositionFault,
        /// <summary>
        /// 报警
        /// </summary>
        CarRunFault,
        /// <summary>
        /// 报警
        /// </summary>
        LeftStopSensorFault,
        /// <summary>
        /// 报警
        /// </summary>
        RightStopSensorFault,
        /// <summary>
        /// 报警
        /// </summary>
        CarNoCenterFault,
        /// <summary>
        /// 报警
        /// </summary>
        EncodeFault,
        /// <summary>
        /// 报警
        /// </summary>
        PickEmptyPalletFaut,
        /// <summary>
        /// 报警
        /// </summary>
        PutPositiomFault,
        /// <summary>
        /// 报警
        /// </summary>
        //LiftSensorFault,
        /// <summary>
        /// 报警
        /// </summary>
        UnoccupiedPE_Fault,
        /// <summary>
        /// 报警
        /// </summary>
        RunTimeFault,


    }
}
