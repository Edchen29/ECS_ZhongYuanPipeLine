using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    /// <summary>
    /// 接口任务创建模型
    /// </summary>
    public class TaskCreateModel
    {
        /// <summary>
        /// 上游任务号
        /// </summary>
        public string TaskNo { get; set; }

        /// <summary>
        /// 前置上游任务号
        /// 不存在时，请写"0"
        /// </summary>
        public string PreTaskNo { get; set; }

        /// <summary>
        /// 前置上游任务号对应的ID
        /// </summary>
        public int PreTaskId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 托盘编码
        /// </summary>
         public string ContainerCode { get; set; }

        /// <summary>
        /// 库内出性质任务必填项
        /// </summary>
        public string FromLocationCode { get; set; }

        /// <summary>
        /// 入库内性质任务必填项
        /// </summary>
        public string ToLocationCode { get; set; }

        /// <summary>
        /// 出库或拣选的目标口编码
        /// </summary>
        public string ToPort { get; set; }

        /// <summary>
        /// 入库口来源位置口编码
        /// </summary>
        public string FromPort { get; set; }

        /// <summary>
        /// 优先级，约定数值越大优先级越高
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 上游平台标识
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 携带的备注
        /// </summary>
        public string Remark { get; set; }

        public string WarehouseCode { get; set; }

        /// <summary>
        /// 任务模型
        /// </summary>
        public List<TaskDetailCreateModel> taskDetails;
    }
}
