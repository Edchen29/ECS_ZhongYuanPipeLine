using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 库存预警表
	/// </summary>
    [Table("inventory_Alert")]
    public partial class InventoryAlert : SysEntity
    {
        public InventoryAlert()
        {
        }

        /// <summary>
	    /// 物料编码
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 物料类型
	    /// </summary>
        [Column("type")]
        public string Type { get; set; }
        /// <summary>
	    /// 物料名称
	    /// </summary>
        [Column("materialName")]
        public string MaterialName { get; set; }
        /// <summary>
	    /// 库存数量
	    /// </summary>
        [Column("inventoryNum")]
        public int? InventoryNum { get; set; }
        /// <summary>
	    /// 预警数量
	    /// </summary>
        [Column("alertNum")]
        public int? AlertNum { get; set; }
    }
}