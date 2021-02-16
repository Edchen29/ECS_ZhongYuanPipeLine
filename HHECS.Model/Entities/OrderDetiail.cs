using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 订单明细表
	/// </summary>
    [Table("order_detiail")]
    public partial class OrderDetiail : SysEntity
    {
        public OrderDetiail()
        {
        }

        /// <summary>
	    /// 工单号
	    /// </summary>
        [Column("orderCode")]
        public string OrderCode { get; set; }
        /// <summary>
	    /// 关联头表
	    /// </summary>
        [Column("orderHeaderId")]
        public int? OrderHeaderId { get; set; }
        /// <summary>
	    /// 产品图号
	    /// </summary>
        [Column("drawingNumber")]
        public string DrawingNumber { get; set; }
        /// <summary>
	    /// 序列号
	    /// </summary>
        [Column("serialNumber")]
        public string SerialNumber { get; set; }
        /// <summary>
	    /// 执行状态
	    /// </summary>
        [Column("executeStatus")]
        public string ExecuteStatus { get; set; }
        /// <summary>
	    /// 质量状态
	    /// </summary>
        [Column("qualityStatus")]
        public string QualityStatus { get; set; }
        /// <summary>
	    /// 跟踪工位
	    /// </summary>
        [Column("stationTraceId")]
        public int? StationTraceId { get; set; }
        /// <summary>
	    /// 开始时间
	    /// </summary>
        [Column("startTime")]
        public System.DateTime? StartTime { get; set; }
        /// <summary>
	    /// 结束时间
	    /// </summary>
        [Column("endTime")]
        public System.DateTime? EndTime { get; set; }
        /// <summary>
        /// 打印次数
        /// </summary>
        [Column("printCount")]
        public int? PrintCount { get; set; }
    }
}