using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Robot.Enums
{
    public enum RobotPcStation
    {
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
        /// 手动确认信号
        /// </summary>
        ManualSign,
        /// <summary>
        /// 工件数量
        /// </summary>
        Num,
        /// <summary>
        /// 站台启动成功（一个焊接设备有2个站台，先启动设备，在启动站台）
        /// </summary>
        Partial_OK,
        /// <summary>
        /// 请求翻转
        /// </summary>
        Allow_Flip,
        /// <summary>
        /// 焊接工位是否为空
        /// </summary>
        Double_Empty,
        /// <summary>
        /// 从请求上料开始有信号，回复WCS_Allow_Load信号后结束
        /// </summary>
        Task_OK

    }
}
