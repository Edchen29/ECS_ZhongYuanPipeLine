using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备类型表
	/// </summary>
    [Table("equipment_type")]
    public partial class EquipmentType : SysEntity
    {
        public EquipmentType()
        {
        }

        /// <summary>
	    /// 设备类型代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 类型名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 描述
	    /// </summary>
        [Column("description")]
        public string Description { get; set; }
        /// <summary>
	    /// 是否生效
	    /// </summary>
        [Column("enable")]
        public bool? Enable { get; set; }
    }
}