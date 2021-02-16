using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 物料需求表
	/// </summary>
    [Table("material_demand")]
    public partial class MaterialDemand : SysEntity
    {
        public MaterialDemand()
        {
        }

        /// <summary>
	    /// 订单号
	    /// </summary>
        [Column("orderCode")]
        public string OrderCode { get; set; }
        /// <summary>
	    /// 物料编码
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 需求数量
	    /// </summary>
        [Column("damandQty")]
        public decimal? DamandQty { get; set; }
        /// <summary>
	    /// 配送数量
	    /// </summary>
        [Column("distributeQty")]
        public decimal? DistributeQty { get; set; }
        /// <summary>
	    /// 上线数量
	    /// </summary>
        [Column("onlineQty")]
        public decimal? OnlineQty { get; set; }
        /// <summary>
	    /// 下线数量
	    /// </summary>
        [Column("offlineQty")]
        public decimal? OfflineQty { get; set; }
        /// <summary>
	    /// 状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
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
    }
}