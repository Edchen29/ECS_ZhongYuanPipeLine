using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 订单预警表
	/// </summary>
    [Table("order_alert")]
    public partial class OrderAlert : SysEntity
    {
        public OrderAlert()
        {
        }

        /// <summary>
	    /// 订单号
	    /// </summary>
        [Column("orderCode")]
        public string OrderCode { get; set; }
        /// <summary>
	    /// 产品代号
	    /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 产品料号
	    /// </summary>
        [Column("partMaterialCode")]
        public string PartMaterialCode { get; set; }
        /// <summary>
	    /// 序列号
	    /// </summary>
        [Column("serialNumber")]
        public string SerialNumber { get; set; }
        /// <summary>
	    /// 订单状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
	    /// 预警信息
	    /// </summary>
        [Column("AlertMsg")]
        public string AlertMsg { get; set; }
        /// <summary>
	    /// 标识符
	    /// </summary>
        [Column("Flag")]
        public int? Flag { get; set; }
        /// <summary>
	    /// 是否播报
	    /// </summary>
        [Column("IsSpeak")]
        public bool? IsSpeak { get; set; }
    }
}