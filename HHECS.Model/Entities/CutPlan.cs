using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 套料结果表
	/// </summary>
    [Table("cutplan")]
    [Serializable]
    public partial class CutPlan : SysEntity
    {
        public CutPlan()
        {
        }

        /// <summary>
        /// 工单编号
        /// </summary>
        [Column("WONumber")]
        public string WONumber { get; set; }

     //   /// <summary>
     //   /// 工位标识
     //   /// </summary>
     //   [Column("stationId")]
     //   public string StationId { get; set; }

     //   /// <summary>
	    ///// 工位编码
	    ///// </summary>
     //   [Column("stationCode")]
     //   public int? StationCode { get; set; }

        /// <summary>
	    /// 原材料缓存位ID
	    /// </summary>
        [Column("stationCacheId")]
        public int stationCacheId { get; set; }

        ///// <summary>
        ///// 原材料编码
        ///// </summary>
        //[Column("materialCode")]
        //public string MaterialCode { get; set; }

        ///// <summary>
        ///// 产品编码
        ///// </summary>
        //[Column("productCode")]
        //public string ProductCode { get; set; }

        /// <summary>
        /// PLC的材质
        /// </summary>
        [Column("wcsProductType")]
        public int WcsProductType { get; set; }

        /// <summary>
	    /// 原材料长度
	    /// </summary>
        [Column("materialLength")]
        public int MaterialLength { get; set; }

        /// <summary>
	    /// 直径
	    /// </summary>
        [Column("diameter")]
        public decimal Diameter { get; set; }

        /// <summary>
	    /// 壁厚
	    /// </summary>
        [Column("thickness")]
        public decimal Thickness { get; set; }

        /// <summary>
	    /// 要切的小段的长度
	    /// </summary>
        [Column("Length")]
        public int Length { get; set; }



        /// <summary>
        /// 状态 :  未发送给ECS：0，已发送给ECS：1
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 组对工位
        /// </summary>
        [Column("AssemblyStation")]
        public int AssemblyStation { get; set; }
    }
}
