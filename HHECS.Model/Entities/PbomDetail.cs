using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 工艺BOM
	/// </summary>
    [Table("pbom_detail")]
    public partial class PbomDetail : SysEntity
    {
        public PbomDetail()
        {
        }

        /// <summary>
	    /// 工序标识
	    /// </summary>
        [Column("stepId")]
        public int? StepId { get; set; }
        /// <summary>
	    /// 工位标识
	    /// </summary>
        [Column("stationId")]
        public int? StationId { get; set; }
        /// <summary>
	    /// 父料号
	    /// </summary>
        [Column("parentMaterialCode")]
        public string ParentMaterialCode { get; set; }
        /// <summary>
	    /// 子料号
	    /// </summary>
        [Column("childMaterialCode")]
        public string ChildMaterialCode { get; set; }
        /// <summary>
	    /// 是否替代料
	    /// </summary>
        [Column("isReplace")]
        public bool? IsReplace { get; set; }
        /// <summary>
	    /// 倍用量
	    /// </summary>
        [Column("double")]
        public decimal? Double { get; set; }
        /// <summary>
	    /// 图号
	    /// </summary>
        [Column("drawingNumber")]
        public string DrawingNumber { get; set; }
        /// <summary>
	    /// 工时
	    /// </summary>
        [Column("man-hours")]
        public decimal? ManHours { get; set; }
        /// <summary>
	    /// 装配
	    /// </summary>
        [Column("assembleTips")]
        public string AssembleTips { get; set; }
    }
}