using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Robot.Enums
{
    public enum RobotPcInformation
    {
        /// <summary>
        /// 可接受输入信号
        /// </summary>
        Cmd_Enabled,
        /// <summary>
        /// 伺服准备就绪
        /// </summary>
        System_Ready,
        /// <summary>
        /// 程序运行中
        /// </summary>
        Prg_running,
        /// <summary>
        /// 暂停中
        /// </summary>
        Prg_paused,
        /// <summary>
        /// 按下HOLD键暂停中
        /// </summary>
        Motion_held,
        /// <summary>
        /// 报警
        /// </summary>
        Fault,
        /// <summary>
        /// 安全位置1
        /// </summary>
        SafePos_1,
        /// <summary>
        /// 安全位置2
        /// </summary>
        SafePos_2,
        /// <summary>
        /// 安全位置3
        /// </summary>
        SafePos_3,
        /// <summary>
        /// 当前运行的程序号
        /// </summary>
        Ack,
        /// <summary>
        /// 反馈的焊接电压
        /// </summary>
        V,
        /// <summary>
        /// 反馈的焊接电流
        /// </summary>
        I,


        /// <summary>
        /// 可接受输入信号
        /// </summary>
        Cmd_Enabled_2,
        /// <summary>
        /// 伺服准备就绪
        /// </summary>
        System_Ready_2,
        /// <summary>
        /// 程序运行中
        /// </summary>
        Prg_running_2,
        /// <summary>
        /// 暂停中
        /// </summary>
        Prg_paused_2,
        /// <summary>
        /// 按下HOLD键暂停中
        /// </summary>
        Motion_held_2,
        /// <summary>
        /// 报警
        /// </summary>
        Fault_2,
        /// <summary>
        /// 安全位置1
        /// </summary>
        SafePos_1_2,
        /// <summary>
        /// 安全位置2
        /// </summary>
        SafePos_2_2,
        /// <summary>
        /// 安全位置3
        /// </summary>
        SafePos_3_2,
        /// <summary>
        /// 当前运行的程序号
        /// </summary>
        Ack_2,
        /// <summary>
        /// 反馈的焊接电压
        /// </summary>
        V_2,
        /// <summary>
        /// 反馈的焊接电流
        /// </summary>
        I_2,

        #region 新增
        /// <summary>
        /// 错误
        /// </summary>
        Err,
        /// <summary>
        /// 工件型号
        /// </summary>
        TYPE_Feedback,
        /// <summary>
        /// 工件数量
        /// </summary>
        Num,
        /// <summary>
        /// 工序跟踪ID
        /// </summary>
        Step_Trace_Id,
        /// <summary>
        /// 焊接工位空闲
        /// </summary>
        Double_Empty,

        /// <summary>
        /// 任务完成信号
        /// </summary>
        Task_OK,
        #endregion
    }
}
