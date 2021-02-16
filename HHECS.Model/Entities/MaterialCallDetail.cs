using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 物料呼叫明细表
	/// </summary>
    [Table("material_call_detail")]
    public partial class MaterialCallDetail : SysEntity
    {
        public MaterialCallDetail()
        {
        }
        /// <summary>
	    /// 主表标识
	    /// </summary>
        [Column("callHeaderId")]
        public int? CallHeaderId { get; set; }
        /// <summary>
	    /// 订单号
	    /// </summary>
        [Column("orderCode")]
        public string OrderCode { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        [Column("productId")]
        public int ProductId { get; set; }
        /// <summary>
	    /// 产品编码
	    /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 产品序列号
	    /// </summary>
        [Column("serialNumber")]
        public string SerialNumber { get; set; }
        /// <summary>
	    /// 机型
	    /// </summary>
        [Column("machineType")]
        public string MachineType { get; set; }
        /// <summary>
        /// 是否已用
        /// </summary>
        [Column("used")]
        public bool? Used { get; set; }
    }
}