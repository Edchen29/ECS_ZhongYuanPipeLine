using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 工序跟踪表
	/// </summary>
    [Table("step_trace_history")]
    public partial class StepTraceHistory : SysEntity
    {
        public StepTraceHistory()
        {
        }

        /// <summary>
	    /// 工单号
	    /// </summary>
        [Column("WONumber")]
        public string WONumber { get; set; }
        /// <summary>
	    /// 产品
	    /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 序列号
	    /// </summary>
        [Column("serialNumber")]
        public string SerialNumber { get; set; }
        /// <summary>
	    /// 线体
	    /// </summary>
        [Column("lineId")]
        public int? LineId { get; set; }
        /// <summary>
	    /// 当前工序
	    /// </summary>
        [Column("stepId")]
        public int? StepId { get; set; }
        /// <summary>
	    /// 当前工位
	    /// </summary>
        [Column("stationId")]
        public int? StationId { get; set; }
        /// <summary>
	    /// 下道工序
	    /// </summary>
        [Column("nextStepId")]
        public int? NextStepId { get; set; }
        /// <summary>
	    /// 是否不良
	    /// </summary>
        [Column("isNG")]
        public bool? IsNG { get; set; }
        /// <summary>
	    /// 不良代号
	    /// </summary>
        [Column("NGcode")]
        public string NGcode { get; set; }
        /// <summary>
	    /// 是否报废
	    /// </summary>
        [Column("isInvalid")]
        public bool? IsInvalid { get; set; }
        /// <summary>
	    /// 进站时间
	    /// </summary>
        [Column("stationInTime")]
        public System.DateTime? StationInTime { get; set; }
        /// <summary>
	    /// 出站时间
	    /// </summary>
        [Column("stationOutTime")]
        public System.DateTime? StationOutTime { get; set; }
        /// <summary>
	    /// 进线时间
	    /// </summary>
        [Column("lineInTime")]
        public System.DateTime? LineInTime { get; set; }
        /// <summary>
	    /// 出线时间
	    /// </summary>
        [Column("lineOutTime")]
        public System.DateTime? LineOutTime { get; set; }
    }
}