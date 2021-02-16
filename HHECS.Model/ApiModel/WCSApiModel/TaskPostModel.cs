using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    /// <summary>
    /// 任务回传model
    /// </summary>
    public class TaskPostModel
    {
        /// <summary>
        /// 上游任务号
        /// </summary>
        public string TaskNo { get; set; }

        /// <summary>
        /// 是否重入
        /// </summary>
        public int IsDoubleIn { get; set; }

        /// <summary>
        /// 是否空出
        /// </summary>
        public int IsEmptyOut { get; set; }

        /// <summary>
        /// 重入时，重新分配的库位
        /// </summary>
        public string RedirectionLocationCode { get; set; }

        /// <summary>
        /// 双伸位时，标记是否取货错
        /// </summary>
        public int IsForkError { get; set; }
    }
}
