using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 库存表
	/// </summary>
    [Table("inventory")]
    public partial class Inventory : SysEntity
    {
        public Inventory()
        {
        }

        /// <summary>
	    /// 
	    /// </summary>
        [Column("factoryId")]
        public int? FactoryId { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        [Column("factoryCode")]
        public string FactoryCode { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        [Column("lineId")]
        public int? LineId { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        [Column("lineCode")]
        public string LineCode { get; set; }
        /// <summary>
	    /// 仓库类型
	    /// </summary>
        [Column("warehouseCode")]
        public string WarehouseCode { get; set; }
        /// <summary>
	    /// 库位id
	    /// </summary>
        [Column("locationId")]
        public int? LocationId { get; set; }
        /// <summary>
	    /// 库位编号
	    /// </summary>
        [Column("locationCode")]
        public string LocationCode { get; set; }
        /// <summary>
	    /// 容器编码
	    /// </summary>
        [Column("containerCode")]
        public string ContainerCode { get; set; }
        /// <summary>
	    /// 上游系统单号
	    /// </summary>
        [Column("sourceCode")]
        public string SourceCode { get; set; }
        /// <summary>
	    /// 物料Id
	    /// </summary>
        [Column("materialId")]
        public int? MaterialId { get; set; }
        /// <summary>
	    /// 物料编码
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 批次
	    /// </summary>
        [Column("batch")]
        public string Batch { get; set; }
        /// <summary>
	    /// 批号
	    /// </summary>
        [Column("lot")]
        public string Lot { get; set; }
        /// <summary>
	    /// 图号
	    /// </summary>
        [Column("drawingCode")]
        public string DrawingCode { get; set; }
        /// <summary>
	    /// 生产日期
	    /// </summary>
        [Column("manufactureDate")]
        public System.DateTime? ManufactureDate { get; set; }
        /// <summary>
	    /// 失效日期
	    /// </summary>
        [Column("expirationDate")]
        public System.DateTime? ExpirationDate { get; set; }
        /// <summary>
	    /// 库存状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
	    /// 容器状态
	    /// </summary>
        [Column("containerStatus")]
        public string ContainerStatus { get; set; }
        /// <summary>
	    /// 数量
	    /// </summary>
        [Column("qty")]
        public decimal? Qty { get; set; }
    }
}