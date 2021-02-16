using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备报警表
	/// </summary>
    [Table("equipment_alarm_log")]
    public partial class EquipmentAlarmLog : SysEntity
    {
        public EquipmentAlarmLog()
        {
        }

        /// <summary>
	    /// 设备编号
	    /// </summary>
        [Column("equipmentId")]
        public int? EquipmentId { get; set; }
        /// <summary>
	    /// 报警代号
	    /// </summary>
        [Column("equipmentAlarmTextCode")]
        public string EquipmentAlarmTextCode { get; set; }
        /// <summary>
	    /// 持续时间(min)
	    /// </summary>
        [Column("holdTime")]
        public int? HoldTime { get; set; }
    }
}