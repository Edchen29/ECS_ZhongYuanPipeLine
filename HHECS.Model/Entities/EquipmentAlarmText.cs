using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备报警警示文档表
	/// </summary>
    [Table("equipment_alarm_text")]
    public partial class EquipmentAlarmText : SysEntity
    {
        public EquipmentAlarmText()
        {
        }

        /// <summary>
	    /// 设备编号
	    /// </summary>
        [Column("equipmentId")]
        public int? EquipmentId { get; set; }
        /// <summary>
	    /// 警示代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 警示语句
	    /// </summary>
        [Column("text")]
        public string Text { get; set; }
    }
}