using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 
	/// </summary>
    [Table("product_barcode_rule")]
    public partial class ProductBarcodeRule : SysEntity
    {
        public ProductBarcodeRule()
        {
        }

        /// <summary>
	    /// 产品ID
	    /// </summary>
        [Column("productId")]
        public int? ProductId { get; set; }
        /// <summary>
	    /// 产品
	    /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 规则代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 条码长度
	    /// </summary>
        [Column("barcodeLength")]
        public int? BarcodeLength { get; set; }
        /// <summary>
	    /// 固定字符
	    /// </summary>
        [Column("partCode")]
        public string PartCode { get; set; }
        /// <summary>
	    /// 字符长度
	    /// </summary>
        [Column("partCodeLength")]
        public int? PartCodeLength { get; set; }
        /// <summary>
	    /// 起始位置
	    /// </summary>
        [Column("partCodeStart")]
        public int? PartCodeStart { get; set; }
    }
}