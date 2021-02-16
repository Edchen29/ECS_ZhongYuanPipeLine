using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 物料配送任务表
	/// </summary>
    [Table("material_distribute_task_header")]
    public partial class MaterialDistributeTaskHeader : SysEntity
    {
        public MaterialDistributeTaskHeader()
        {
        }

        /// <summary>
	    /// AGV任务号
	    /// </summary>
        [Column("taskNo")]
        public string TaskNo { get; set; }
        /// <summary>
	    /// 叫料需求标识
	    /// </summary>
        [Column("materialCallId")]
        public int? MaterialCallId { get; set; }
        /// <summary>
	    /// 小车编号
	    /// </summary>
        [Column("carNo")]
        public string CarNo { get; set; }
        /// <summary>
	    /// 料框编码
	    /// </summary>
        [Column("containerCode")]
        public string ContainerCode { get; set; }
        /// <summary>
        /// 产品代码
        /// </summary>
        [Column("productId")]
        public int ProductId { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }        
        /// <summary>
	    /// 配送操作员
	    /// </summary>
        [Column("userCode")]
        public string UserCode { get; set; }
        /// <summary>
	    /// 需求工位
	    /// </summary>
        [Column("needStation")]
        public string NeedStation { get; set; }
        /// <summary>
        /// 需求缓存位
        /// </summary>
        [Column("locationCode")]
        public string LocationCode { get; set; }
        /// <summary>
	    /// 需求时间
	    /// </summary>
        [Column("needTime")]
        public System.DateTime? NeedTime { get; set; }
        /// <summary>
	    /// 响应需求时间
	    /// </summary>
        [Column("responseTime")]
        public System.DateTime? ResponseTime { get; set; }
        /// <summary>
	    /// 结束时间
	    /// </summary>
        [Column("finishTime")]
        public System.DateTime? FinishTime { get; set; }
        /// <summary>
	    /// 状态
	    /// </summary>
        [Column("status")]
        public int? Status { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("materialConfirm")]
        public int? MaterialConfirm { get; set; }
        /// <summary>
        /// 逻辑外键实体-配送明细
        /// </summary>
        public List<MaterialDistributeTaskDetail> distributeDetails { get; set; } = new List<MaterialDistributeTaskDetail>();
    }
}