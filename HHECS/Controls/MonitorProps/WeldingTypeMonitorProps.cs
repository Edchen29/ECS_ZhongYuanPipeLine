using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Controls.MonitorProps
{
    /// <summary>
    /// 焊接机器人监控属性
    /// </summary>
    public enum WeldingTypeMonitorProps
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
        /// 启动成功
        /// </summary>
        Start_OK,
        /// <summary>
        /// 焊丝预警
        /// </summary>
        Wire,
        /// <summary>
        /// 错误
        /// </summary>
        Err,
        /// <summary>
        /// 准备完成
        /// </summary>
        Ready_OK,
        /// <summary>
        /// 下料准备完成
        /// </summary>
        Ready_Blank,
        /// <summary>
        /// 请求下料
        /// </summary>
        Request_Blank,
        /// <summary>
        /// 确认下料完成
        /// </summary>
        CompleteBlank_OK,
        /// <summary>
        /// 上料准备完成
        /// </summary>
        Ready_Load,
        /// <summary>
        /// 请求上料
        /// </summary>
        Request_Load,
        /// <summary>
        /// 确认上料完成
        /// </summary>
        CompleteLoad_OK,
        /// <summary>
        /// 工件型号
        /// </summary>
        TYPE_Feedback,
        /// <summary>
        /// 工件数量
        /// </summary>
        Num,
        /// <summary>
        /// 启动
        /// </summary>
        Start,
        /// <summary>
        /// 停止
        /// </summary>
        Stop,
        /// <summary>
        /// 复位
        /// </summary>
        Reset,
        /// <summary>
        /// 绗架允许下料
        /// </summary>
        Blank_Ready,
        /// <summary>
        /// 绗架下料中
        /// </summary>
        Allow_Blank,
        /// <summary>
        /// 绗架允许上料
        /// </summary>
        Load_Ready,
        /// <summary>
        /// 绗架上料中
        /// </summary>
        Allow_Load,
        /// <summary>
        /// 上料工件型号
        /// </summary>
        TYPE,
        /// <summary>
        /// 上料工件数量
        /// </summary>
        Number,
        /// <summary>
        /// 上料完成
        /// </summary>
        Load_Compelte,
        /// <summary>
        /// 下料完成
        /// </summary>
        Blank_Complete,
        /// <summary>
        /// 工序跟踪ID
        /// </summary>
        Step_Trace_Id,
        /// <summary>
        /// WCS回复上料请求
        /// </summary>
        WCS_Allow_Load,
        /// <summary>
        /// 请求翻转
        /// </summary>
        Allow_Flip,
        /// <summary>
        /// WCS回复允许翻转信号
        /// </summary>
        WCS_Allow_Flip,
        /// <summary>
        /// 焊接工位空闲
        /// </summary>
        Double_Empty,
        /// <summary>
        /// 任务完成信号
        /// </summary>
        Task_OK,
        /// <summary>
        /// 手动确认
        /// </summary>
        ManualSign,
    }
}
