using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 制造BOM
	/// </summary>
    [Table("mbom_detail")]
    public partial class MbomDetail : SysEntity
    {
        public MbomDetail()
        {
        }

        /// <summary>
	    /// 主表标识
	    /// </summary>
        [Column("mbomHeaderId")]
        public int? MbomHeaderId { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 工序标识
        /// </summary>
        [Column("stepId")]
        public int? StepId { get; set; }
        /// <summary>
	    /// 物料
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 图号
	    /// </summary>
        [Column("drawingNumber")]
        public string DrawingNumber { get; set; }
        /// <summary>
	    /// 基数数量
	    /// </summary>
        [Column("baseQty")]
        public decimal? BaseQty { get; set; }
        /// <summary>
	    /// 需要质检
	    /// </summary>
        [Column("isCheck")]
        public bool? IsCheck { get; set; }
    }
}