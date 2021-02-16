using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备类型模板
	/// </summary>
    [Table("equipment_type_template")]
    public partial class EquipmentTypeTemplate : SysEntity
    {
        public EquipmentTypeTemplate()
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
	    /// 设备类型
	    /// </summary>
        [Column("equipmentTypeId")]
        public int EquipmentTypeId { get; set; }
        /// <summary>
	    /// 属性类型
	    /// </summary>
        [Column("propType")]
        public string PropType { get; set; }
        /// <summary>
	    /// 数据类型
	    /// </summary>
        [Column("dataType")]
        public string DataType { get; set; }
        /// <summary>
	    /// 地址
	    /// </summary>
        [Column("address")]
        public string Address { get; set; }
        /// <summary>
	    /// 偏移量
	    /// </summary>
        [Column("offset")]
        public string Offset { get; set; }
        /// <summary>
        /// 是否监控
        /// </summary>
        [Column("isMonitor")]
        public bool IsMonitor { get; set; }
        /// <summary>
	    /// 监控参照值
	    /// </summary>
        [Column("monitorCompareValue")]
        public string MonitorCompareValue { get; set; }
        /// <summary>
	    /// 正常的文本说明
	    /// </summary>
        [Column("monitorNormal")]
        public string MonitorNormal { get; set; }
        /// <summary>
	    /// 异常的文本说明
	    /// </summary>
        [Column("monitorFailure")]
        public string MonitorFailure { get; set; }

        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 额外记录所属设备类型编码
        /// </summary>
        [Editable(false)]
        public string HeadCode { get; set; }

        /// <summary>
        /// 额外记录所属设备类型名称
        /// </summary>
        [Editable(false)]
        public string HeadName { get; set; }
    }
}