using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    /// <summary>
    /// 接口任务查询模型
    /// </summary>
    public class TaskQueryModel
    {
        /// <summary>
        /// 上游任务号
        /// </summary>
        public List<string> TaskNos { get; set; }

    }

    /// <summary>
    /// 接口任务查询模型
    /// </summary>
    public class TaskQueryModel1
    {
        /// <summary>
        /// 上游任务号
        /// </summary>
        public string TaskNo { get; set; }

    }
}
