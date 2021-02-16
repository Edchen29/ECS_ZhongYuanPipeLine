using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 序列号表
	/// </summary>
    [Table("serial_number")]
    public partial class SerialNumber : SysEntity
    {
        public SerialNumber()
        {
        }

        /// <summary>
	    /// 订单号
	    /// </summary>
        [Column("orderCode")]
        public string OrderCode { get; set; }
        /// <summary>
	    /// 产品
	    /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 序列号
	    /// </summary>
        [Column("serialNumber")]
        public string SerialNumberMember { get; set; }
        /// <summary>
	    /// 条码规则
	    /// </summary>
        [Column("serialRule")]
        public string SerialRule { get; set; }
        /// <summary>
	    /// 打印次数
	    /// </summary>
        [Column("printCount")]
        public int? PrintCount { get; set; }
    }
}