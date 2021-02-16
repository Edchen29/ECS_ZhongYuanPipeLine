using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备保养记录表
	/// </summary>
    [Table("equipment_maintain_log")]
    public partial class EquipmentMaintainLog : SysEntity
    {
        public EquipmentMaintainLog()
        {
        }

        /// <summary>
	    /// 设备部件编号
	    /// </summary>
        [Column("equipmentItemId")]
        public int? EquipmentItemId { get; set; }
        /// <summary>
	    /// 保养人员
	    /// </summary>
        [Column("userCode")]
        public string UserCode { get; set; }
        /// <summary>
	    /// 开始时间
	    /// </summary>
        [Column("startTime")]
        public System.DateTime? StartTime { get; set; }
        /// <summary>
	    /// 结束时间
	    /// </summary>
        [Column("endTime")]
        public System.DateTime? EndTime { get; set; }
        /// <summary>
	    /// 备注
	    /// </summary>
        [Column("remark")]
        public string Remark { get; set; }
    }
}