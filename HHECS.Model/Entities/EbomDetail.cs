using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设计BOM明细表
	/// </summary>
    [Table("ebom_detail")]
    public partial class EbomDetail : SysEntity
    {
        public EbomDetail()
        {
        }

        /// <summary>
	    /// 主表ID
	    /// </summary>
        [Column("bomHeaderId")]
        public int? BomHeaderId { get; set; }
        /// <summary>
	    /// 物料编码
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 物料名称
	    /// </summary>
        [Column("materialName")]
        public string MaterialName { get; set; }
        /// <summary>
	    /// 品名规格
	    /// </summary>
        [Column("materialSpec")]
        public string MaterialSpec { get; set; }
        /// <summary>
	    /// 单位
	    /// </summary>
        [Column("unit")]
        public string Unit { get; set; }
        /// <summary>
	    /// 数量
	    /// </summary>
        [Column("qty")]
        public decimal? Qty { get; set; }
    }
}