using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 容器表
	/// </summary>
    [Table("container")]
    public partial class Container : SysEntity
    {
        public Container()
        {
        }

        /// <summary>
	    /// 容器编码
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 容器类型
	    /// </summary>
        [Column("type")]
        public string Type { get; set; }
        /// <summary>
	    /// 是否锁定
	    /// </summary>
        [Column("IsLock")]
        public short? IsLock { get; set; }
        /// <summary>
	    /// 库位id
	    /// </summary>
        [Column("locationId")]
        public int? LocationId { get; set; }
        /// <summary>
	    /// 库位编码
	    /// </summary>
        [Column("locationCode")]
        public string LocationCode { get; set; }
        /// <summary>
	    /// 状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
	    /// 物料高度
	    /// </summary>
        [Column("height")]
        public decimal? Height { get; set; }
        /// <summary>
	    /// 物料重量
	    /// </summary>
        [Column("weight")]
        public decimal? Weight { get; set; }
        /// <summary>
	    /// 打印次数
	    /// </summary>
        [Column("printCount")]
        public int? PrintCount { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("warehouseCode")]
        public string WarehouseCode { get; set; }
    }
}