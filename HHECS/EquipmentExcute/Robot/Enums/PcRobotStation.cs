using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.EquipmentExcute.Robot.Enums
{
    public enum PcRobotStation
    {
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
        /// 站台启动（一个焊接设备有2个站台，先启动设备，在启动站台）
        /// </summary>
        Partial_Start,
        /// <summary>
        /// WCS回复允许上料完成
        /// </summary>
        WCS_Allow_Load,
        /// <summary>
        /// WCS允许翻转
        /// </summary>
        WCS_Allow_Flip,

        /// <summary>
        /// 材料
        /// </summary>
        WCS_Pipe_Material,

        /// <summary>
        /// 长度
        /// </summary>
        WCS_Pipe_Length,

        /// <summary>
        /// 直径
        /// </summary>
        WCS_Pipe_Diameter,

        /// <summary>
        /// 壁厚
        /// </summary>
        WCS_Pipe_Thickness,
    }
}
