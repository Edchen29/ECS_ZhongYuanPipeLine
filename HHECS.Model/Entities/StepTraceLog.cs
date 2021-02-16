using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 工序跟踪日志主表
	/// </summary>
    [Table("step_trace_log")]
    public partial class StepTraceLog : SysEntity
    {
        public StepTraceLog()
        {
        }
        /// <summary>
        /// 工序跟踪ID
        /// </summary>
        [Column("stepTraceId")]
        public int? StepTraceId { get; set; }
        /// <summary>
	    /// 工单号
	    /// </summary>
        [Column("WONumber")]
        public string WONumber { get; set; }
        /// <summary>
        /// 产品标识
        /// </summary>
        [Column("productId")]
        public int? ProductId { get; set; }
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
        /// 工位
        /// </summary>
        [Column("lastStationId")]
        public int? LastStationId { get; set; }
        /// <summary>
	    /// 工位
	    /// </summary>
        [Column("stationId")]
        public int? StationId { get; set; }
        /// <summary>
        /// 桁车编码
        /// </summary>
        [Column("srmCode")]
        public string SrmCode { get; set; }
        /// <summary>
	    /// 是否成功过站
	    /// </summary>
        [Column("passOrFail")]
        public string PassOrFail { get; set; }
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