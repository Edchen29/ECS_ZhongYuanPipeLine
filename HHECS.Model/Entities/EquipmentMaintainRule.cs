using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 部件保养规则表
	/// </summary>
    [Table("equipment_maintain_rule")]
    public partial class EquipmentMaintainRule : SysEntity
    {
        public EquipmentMaintainRule()
        {
        }

        /// <summary>
	    /// 设备部件编号
	    /// </summary>
        [Column("equipmentItemId")]
        public int? EquipmentItemId { get; set; }
        /// <summary>
	    /// 时间（天）
	    /// </summary>
        [Column("times ")]
        public int? Times { get; set; }
        /// <summary>
	    /// 距离（km）
	    /// </summary>
        [Column("distance")]
        public int? Distance { get; set; }
        /// <summary>
	    /// 次数（千次）
	    /// </summary>
        [Column("frequence")]
        public int? Frequence { get; set; }
    }
}