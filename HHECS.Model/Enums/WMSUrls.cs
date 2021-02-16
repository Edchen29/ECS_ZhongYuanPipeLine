using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Enums
{
    public enum WMSUrls
    {
        /// <summary>
        /// 创建
        /// </summary>
        TaskCreate,

        /// <summary>
        /// 下发
        /// </summary>
        TaskExecute,

        /// <summary>
        /// 完成
        /// </summary>
        TaskComplete,

        /// <summary>
        /// 登录
        /// </summary>
        Login,

        /// <summary>
        /// 心跳（定时刷新cookie）
        /// </summary>
        Heartbeat,

        /// <summary>
        /// 处理空出
        /// </summary>
        HandleEmptyOut,

        /// <summary>
        /// 获取入库目标库位
        /// </summary>
        GetLocation,

        /// <summary>
        /// 处理取货错误
        /// </summary>
        HandleForkError

    }
}
