using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 库位表
	/// </summary>
    [Table("location")]
    public partial class Location : SysEntity
    {
        public Location()
        {
        }

        /// <summary>
	    /// 库位
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 线体ID
	    /// </summary>
        [Column("lineId")]
        public int? LineId { get; set; }
        /// <summary>
	    /// 线体编码
	    /// </summary>
        [Column("lineCode")]
        public string LineCode { get; set; }
        /// <summary>
	    /// 行
	    /// </summary>
        [Column("row")]
        public short? Row { get; set; }
        /// <summary>
	    /// 列
	    /// </summary>
        [Column("line")]
        public short? Line { get; set; }
        /// <summary>
	    /// 层
	    /// </summary>
        [Column("layer")]
        public short? Layer { get; set; }
        /// <summary>
	    /// 格
	    /// </summary>
        [Column("grid")]
        public short? Grid { get; set; }
        /// <summary>
	    /// stepID
	    /// </summary>
        [Column("rowIndex1")]
        public int RowIndex1 { get; set; }
        /// <summary>
        /// stationID
        /// </summary>
        [Column("rowIndex2")]
        public int RowIndex2 { get; set; }
        /// <summary>
	    /// 堆垛机标记，与之相等，取rowindex1，反之取rowindex2
	    /// </summary>
        [Column("srmCode")]
        public string SrmCode { get; set; }
        /// <summary>
        /// 目标区域
        /// </summary>
        [Column("destinationArea")]
        public string DestinationArea { get; set; }
        /// <summary>
        /// 巷道
        /// </summary>
        [Column("roadWay")]
        public int? RoadWay { get; set; }
        /// <summary>
	    /// 库位类型
	    /// </summary>
        [Column("type")]
        public string Type { get; set; }
        /// <summary>
	    /// 高度上限
	    /// </summary>
        [Column("maxHeight")]
        public decimal? MaxHeight { get; set; }
        /// <summary>
	    /// 重量上限
	    /// </summary>
        [Column("maxWeight")]
        public decimal? MaxWeight { get; set; }
        /// <summary>
	    /// 是否禁用
	    /// </summary>
        [Column("isStop")]
        public bool? IsStop { get; set; }
        /// <summary>
	    /// 容器id号
	    /// </summary>
        [Column("containerId")]
        public int? ContainerId { get; set; }
        /// <summary>
	    /// 容器编码
	    /// </summary>
        [Column("containerCode")]
        public string ContainerCode { get; set; }
        /// <summary>
	    /// 状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        [Column("IsLock")]
        public short IsLock { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        [Column("zoneCode")]
        public string ZoneCode { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("warehouseCode")]
        public string WarehouseCode { get; set; }
        /// <summary>
	    /// 物料编号
	    /// </summary>
        [Column("goodsNo")]
        public string GoodsNo { get; set; }
        /// <summary>
	    /// 上次盘点日期
	    /// </summary>
        [Column("lastCycleCountDate")]
        public System.DateTime? LastCycleCountDate { get; set; }
        /// <summary>
        /// 摆放类型
        /// </summary>
        [Column("putType")]
        public int PutType { get; set; }
    }
}