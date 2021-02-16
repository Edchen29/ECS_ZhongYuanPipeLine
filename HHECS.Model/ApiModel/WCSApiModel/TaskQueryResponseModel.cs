using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    /// <summary>
    /// 任务查询返回值
    /// </summary>
    public class TaskQueryResponseModel
    {
        public string TaskNo { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int TaskStatus { get; set; }

        /// <summary>
        /// 任务状态的描述
        /// </summary>
        public string TaskStatusDesc { get; set; }

        /// <summary>
        /// 当前所在设备（如果有）
        /// </summary>
        public string CurrentEquipmentName { get; set; }
    }
}
