using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备部件表
	/// </summary>
    [Table("equipment_item")]
    public partial class EquipmentItem : SysEntity
    {
        public EquipmentItem()
        {
        }

        /// <summary>
	    /// 设备部件编号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 部件名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 设备
	    /// </summary>
        [Column("equipmentId")]
        public int? EquipmentId { get; set; }
        /// <summary>
	    /// 描述
	    /// </summary>
        [Column("description")]
        public string Description { get; set; }
        /// <summary>
	    /// 保养注意事项
	    /// </summary>
        [Column("maintainTips")]
        public string MaintainTips { get; set; }
        /// <summary>
	    /// 保养需要的工具
	    /// </summary>
        [Column("maintainTools")]
        public string MaintainTools { get; set; }
        /// <summary>
	    /// 链接的保养说明书
	    /// </summary>
        [Column("linkMaintainSop")]
        public string LinkMaintainSop { get; set; }
        /// <summary>
	    /// 上次保养时间
	    /// </summary>
        [Column("lastMaintainTime")]
        public int? LastMaintainTime { get; set; }
        /// <summary>
	    /// 上次保养距离
	    /// </summary>
        [Column("lastMaintainDistance")]
        public int? LastMaintainDistance { get; set; }
        /// <summary>
	    /// 上次保养次数
	    /// </summary>
        [Column("lastMaintainFrequence")]
        public int? LastMaintainFrequence { get; set; }
        /// <summary>
	    /// 下次保养时间
	    /// </summary>
        [Column("nextMaintainTime")]
        public int? NextMaintainTime { get; set; }
        /// <summary>
	    /// 下次保养距离
	    /// </summary>
        [Column("nextMaintainDistance")]
        public int? NextMaintainDistance { get; set; }
        /// <summary>
	    /// 下次保养次数
	    /// </summary>
        [Column("nextMaintainFrequence")]
        public int? NextMaintainFrequence { get; set; }
        /// <summary>
	    /// 当前保养时间
	    /// </summary>
        [Column("currentMaintainTime")]
        public int? CurrentMaintainTime { get; set; }
        /// <summary>
	    /// 当前保养距离
	    /// </summary>
        [Column("currentManitainDistance")]
        public int? CurrentManitainDistance { get; set; }
        /// <summary>
	    /// 当前保养次数
	    /// </summary>
        [Column("currentMaintainFrequence")]
        public int? CurrentMaintainFrequence { get; set; }
    }
}