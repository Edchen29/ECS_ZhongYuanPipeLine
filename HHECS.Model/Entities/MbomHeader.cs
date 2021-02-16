using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 制造BOM
	/// </summary>
    [Table("mbom_header")]
    public partial class MbomHeader : SysEntity
    {
        public MbomHeader()
        {
        }

        /// <summary>
	    /// 产品标识
	    /// </summary>
        [Column("productId")]
        public int? ProductId { get; set; }
        /// <summary>
	    /// 产品代号
	    /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 生产线标识
	    /// </summary>
        [Column("lineId")]
        public int? LineId { get; set; }
        /// <summary>
	    /// 图号
	    /// </summary>
        [Column("drawingNumber")]
        public string DrawingNumber { get; set; }
        /// <summary>
	    /// 变更单号
	    /// </summary>
        [Column("changeOrder")]
        public string ChangeOrder { get; set; }
        /// <summary>
	    /// 版本
	    /// </summary>
        [Column("version")]
        public string Version { get; set; }
        /// <summary>
	    /// 审核员
	    /// </summary>
        [Column("verifyer")]
        public string Verifyer { get; set; }
    }
}