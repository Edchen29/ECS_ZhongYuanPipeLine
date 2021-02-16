using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 维修表
	/// </summary>
    [Table("repair")]
    public partial class Repair : SysEntity
    {
        public Repair()
        {
        }

        /// <summary>
	    /// 工单号
	    /// </summary>
        [Column("WONumber")]
        public string WONumber { get; set; }
        /// <summary>
	    /// 产品
	    /// </summary>
        [Column("itemCode")]
        public string ItemCode { get; set; }
        /// <summary>
	    /// 序列号
	    /// </summary>
        [Column("serialNumber")]
        public string SerialNumber { get; set; }
        /// <summary>
	    /// 不良部件
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 不良代号
	    /// </summary>
        [Column("NGcode")]
        public string NGcode { get; set; }
        /// <summary>
	    /// 维修代号
	    /// </summary>
        [Column("repairCode")]
        public string RepairCode { get; set; }
        /// <summary>
	    /// 修复状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
	    /// 打不良人员
	    /// </summary>
        [Column("NGMarkUser")]
        public string NGMarkUser { get; set; }
        /// <summary>
	    /// 维修人员
	    /// </summary>
        [Column("repairUser")]
        public string RepairUser { get; set; }
        /// <summary>
	    /// 维修意见
	    /// </summary>
        [Column("suggestion")]
        public string Suggestion { get; set; }
    }
}