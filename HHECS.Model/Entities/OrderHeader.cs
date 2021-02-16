using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 订单表
	/// </summary>
    [Table("order_header")]
    public partial class OrderHeader : SysEntity
    {
        public OrderHeader()
        {
        }

        /// <summary>
	    /// 工单号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 产品
	    /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 部件料号
	    /// </summary>
        [Column("partMaterialCode")]
        public string PartMaterialCode { get; set; }
        /// <summary>
	    /// 计划量
	    /// </summary>
        [Column("planQty")]
        public decimal? PlanQty { get; set; }
        /// <summary>
	    /// 完成量
	    /// </summary>
        [Column("completeQty")]
        public decimal? CompleteQty { get; set; }
        /// <summary>
	    /// 不良量
	    /// </summary>
        [Column("NGQty")]
        public decimal? NGQty { get; set; }
        /// <summary>
	    /// 工单状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
	    /// 工单类型
	    /// </summary>
        [Column("type")]
        public string Type { get; set; }
        /// <summary>
	    /// 线体
	    /// </summary>
        [Column("lineId")]
        public int? LineId { get; set; }
        /// <summary>
	    /// 批次号
	    /// </summary>
        [Column("lotNo")]
        public string LotNo { get; set; }
        /// <summary>
	    /// 计划开始时间
	    /// </summary>
        [Column("planStartTime")]
        public System.DateTime? PlanStartTime { get; set; }
        /// <summary>
	    /// 预计完成时间
	    /// </summary>
        [Column("planEndTime")]
        public System.DateTime? PlanEndTime { get; set; }
        /// <summary>
	    /// 实际开始时间
	    /// </summary>
        [Column("actualStartTime")]
        public System.DateTime? ActualStartTime { get; set; }
        /// <summary>
	    /// 实际结束时间
	    /// </summary>
        [Column("actualEndTime")]
        public System.DateTime? ActualEndTime { get; set; }
        /// <summary>
	    /// 预留号
	    /// </summary>
        [Column("reserveNo")]
        public int? ReserveNo { get; set; }
        /// <summary>
	    /// 预留行号
	    /// </summary>
        [Column("reserveRowNo")]
        public int? ReserveRowNo { get; set; }
        /// <summary>
	    /// 生产工厂
	    /// </summary>
        [Column("workFactory")]
        public string WorkFactory { get; set; }
    }
}