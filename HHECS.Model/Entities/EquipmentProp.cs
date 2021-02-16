using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备属性
	/// </summary>
    [Table("equipment_prop")]
    public partial class EquipmentProp : SysEntity
    {
        public EquipmentProp()
        {
        }

        /// <summary>
	    /// 设备
	    /// </summary>
        [Column("equipmentId")]
        public int? EquipmentId { get; set; }
        /// <summary>
	    /// 设备模板ID
	    /// </summary>
        [Column("equipmentTypeTemplateId")]
        public int? EquipmentTypeTemplateId { get; set; }
        /// <summary>
	    /// 设备模板代号
	    /// </summary>
        [Column("equipmentTypeTemplateCode")]
        public string EquipmentTypeTemplateCode { get; set; }
        /// <summary>
	    /// 服务器句柄
	    /// </summary>
        [Column("serverHandle")]
        public int ServerHandle { get; set; }
        /// <summary>
	    /// plc地址值
	    /// </summary>
        [Column("address")]
        public string Address { get; set; }
        /// <summary>
	    /// plc的数据值
	    /// </summary>
        [Column("value")]
        public string Value { get; set; }
        /// <summary>
	    /// 是否写入成功
	    /// </summary>
        [Column("plcFlag ")]
        public int? PlcFlag { get; set; }
        /// <summary>
	    /// 是否需要写入
	    /// </summary>
        [Column("writeFlag")]
        public bool? WriteFlag { get; set; }
        /// <summary>
	    /// 待写值
	    /// </summary>
        [Column("writeValue")]
        public string WriteValue { get; set; }
        /// <summary>
	    /// 备注
	    /// </summary>
        [Column("remark")]
        public string Remark { get; set; }

        #region 额外对应设备的属性

        /// <summary>
        /// 逻辑外键--设备实体
        /// </summary>
        public Equipment Equipment { get; set; }

        /// <summary>
        /// 额外对应属性模板，方便读取模板属性
        /// </summary>
        public EquipmentTypeTemplate EquipmentTypeTemplate { get; set; }

        #endregion
    }
}