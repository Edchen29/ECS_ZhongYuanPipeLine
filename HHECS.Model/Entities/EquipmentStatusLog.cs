using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备状态表
	/// </summary>
    [Table("equipment_status_log")]
    public partial class EquipmentStatusLog : SysEntity
    {
        public EquipmentStatusLog()
        {
        }

        /// <summary>
	    /// 设备编号
	    /// </summary>
        [Column("equipmentId")]
        public int? EquipmentId { get; set; }
        /// <summary>
	    /// 设备状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
	    /// 持续时间(min)
	    /// </summary>
        [Column("holdTime")]
        public int? HoldTime { get; set; }
    }
}