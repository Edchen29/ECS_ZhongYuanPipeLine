using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 产品对应线体表
	/// </summary>
    [Table("product_detail")]
    public partial class ProductDetail : SysEntity
    {
        public ProductDetail()
        {
        }

        /// <summary>
	    /// 产品标识
	    /// </summary>
        [Column("productHeaderId")]
        public int? ProductHeaderId { get; set; }
        /// <summary>
	    /// 线体标识
	    /// </summary>
        [Column("lineId")]
        public int? LineId { get; set; }
        /// <summary>
	    /// 线体代号
	    /// </summary>
        [Column("lineCode")]
        public string LineCode { get; set; }
        /// <summary>
	    /// 工序标识
	    /// </summary>
        [Column("stepId")]
        public int? StepId { get; set; }
        /// <summary>
	    /// 工序
	    /// </summary>
        [Column("stepCode")]
        public string StepCode { get; set; }
        /// <summary>
	    /// 工序程序
	    /// </summary>
        [Column("programCode")]
        public string ProgramCode { get; set; }
    }
}