using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 工位缓存表
	/// </summary>
    [Table("station_cache")]
    [Serializable]
    public partial class StationCache : SysEntity
    {
        public StationCache()
        {
        }

        //   /// <summary>
        ///// 缓存位编码
        ///// </summary>
        //   [Column("code")]
        //   public int Code { get; set; }

        //   /// <summary>
        ///// 管材标识
        ///// </summary>
        //   [Column("pipeId")]
        //   public int PipeId { get; set; }

        //   /// <summary>
        ///// 管材编码
        ///// </summary>
        //   [Column("pipeCode")]
        //   public int PipeCode { get; set; }

        /// <summary>
        /// 工序监控ID
        /// </summary>
        [Column("stepTraceId")]
        public int? stepTraceId { get; set; }

        /// <summary>
        /// 工位标识
        /// </summary>
        [Column("stationId")]
        public int? StationId { get; set; }

        /// <summary>
	    /// 工位编码
	    /// </summary>
        [Column("stationCode")]
        public string StationCode { get; set; }

        /// <summary>
        /// PLC的材质
        /// </summary>
        [Column("wcsProductType")]
        public int? WcsProductType { get; set; }

        /// <summary>
	    /// 原材料直径
	    /// </summary>
        [Column("diameter")]
        public decimal? Diameter { get; set; }

        /// <summary>
	    /// 原材料壁厚
	    /// </summary>
        [Column("thickness")]
        public decimal? Thickness { get; set; }

        /// <summary>
	    /// 原材料长度
	    /// </summary>
        [Column("MaterialLength")]
        public int? MaterialLength { get; set; }



        ///// <summary>
        ///// 物料编码
        ///// </summary>
        //[Column("materialCode")]
        //public string MaterialCode { get; set; }

        /// <summary>
        /// 状态 :  未套料：0，已经套料：1
        /// </summary>
        [Column("status")]
        public int? Status { get; set; }


        public Station StationCodeVM { get; set; }
    }
}
