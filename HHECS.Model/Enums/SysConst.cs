using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums
{
    /// <summary>
    /// 系统关键字典枚举
    /// </summary>
    public enum SysConst
    {

        /// <summary>
        /// 远程地址
        /// </summary>
        RemoteUrls,

        /// <summary>
        /// 上游系统与wcs port对应字典
        /// </summary>
        Port,

        /// <summary>
        /// 任务类型
        /// </summary>
        TaskType,

        /// <summary>
        /// 任务优先级
        /// </summary>
        TaskPriority,

        /// <summary>
        /// OPC服务器地址
        /// </summary>
        OPCServerIP,

        /// <summary>
        /// S7实现时，需要使用这个配置来确定PLCIP
        /// </summary>
        PLC,

        /// <summary>
        /// ModBus实现时，需要使用这个配置来确定
        /// </summary>
        ModBus,

        /// <summary>
        /// 只入口类型
        /// </summary>
        StationForPortIn,

        /// <summary>
        /// 分拣口
        /// </summary>
        StationForPortInOrOut,

        /// <summary>
        /// 只出口类型
        /// </summary>
        StationForPortOut,

        /// <summary>
        /// 堆垛机接入站台类型
        /// </summary>
        StationForStockerIn,

        /// <summary>
        /// 堆垛机接出站台类型
        /// </summary>
        StationForStockerOut,

        /// <summary>
        /// 堆垛机接出接入站台
        /// </summary>
        StationForStockerInOrOut,
        PLCHeartbeat,
        SingleForkSRMV132,

        /// <summary>
        /// 打标机服务端IP
        /// </summary>
        ServerIP,
    }
}
