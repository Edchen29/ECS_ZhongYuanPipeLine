using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设备工作状况
	/// </summary>
    [Table("equipmentmonitor")]
    public partial class EquipmentMonitor : SysEntity
    {
        public EquipmentMonitor()
        {
        }

        /// <summary>
	    /// 设备编号
	    /// </summary>
        [Column("equipmentCode")]
        public string EquipmentCode { get; set; }
        /// <summary>
	    /// 设备名称
	    /// </summary>
        [Column("equipmentName")]
        public string EquipmentName { get; set; }
        /// <summary>
	    /// 生产工厂
	    /// </summary>
        [Column("factoryName")]
        public string FactoryName { get; set; }
        /// <summary>
	    /// 车间代号
	    /// </summary>
        [Column("workshopCode")]
        public string WorkshopCode { get; set; }
        /// <summary>
	    /// 生产车间
	    /// </summary>
        [Column("workshopName")]
        public string WorkshopName { get; set; }
        /// <summary>
	    /// 线体
	    /// </summary>
        [Column("lineName")]
        public string LineName { get; set; }
        /// <summary>
	    /// 工序
	    /// </summary>
        [Column("stepName")]
        public string StepName { get; set; }
        /// <summary>
	    /// 工位
	    /// </summary>
        [Column("stationName")]
        public string StationName { get; set; }
        /// <summary>
	    /// IP
	    /// </summary>
        [Column("IP")]
        public string IP { get; set; }
        /// <summary>
	    /// 是否生效
	    /// </summary>
        [Column("enable")]
        public string Enable { get; set; }
        /// <summary>
	    /// 设备类型
	    /// </summary>
        [Column("typeName")]
        public string TypeName { get; set; }
        /// <summary>
	    /// 设备状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
	    /// 是否就绪
	    /// </summary>
        [Column("beReady")]
        public string BeReady { get; set; }
        /// <summary>
	    /// 工单号
	    /// </summary>
        [Column("WONumber")]
        public string WONumber { get; set; }
        /// <summary>
	    /// 产品编号
	    /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 产品唯一序列号
	    /// </summary>
        [Column("serialNumber")]
        public string SerialNumber { get; set; }
    }
}