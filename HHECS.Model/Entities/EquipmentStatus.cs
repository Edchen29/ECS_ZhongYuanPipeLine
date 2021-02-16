using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备状态表
	/// </summary>
    [Table("equipment_status")]
    public partial class EquipmentStatus : SysEntity
    {
        public EquipmentStatus()
        {
        }

        /// <summary>
	    /// 设备编号
	    /// </summary>
        [Column("equipmentId")]
        public int? EquipmentId { get; set; }
        /// <summary>
	    /// 是否就绪
	    /// </summary>
        [Column("beReady")]
        public int? BeReady { get; set; }
        /// <summary>
	    /// 设备状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
    }
}