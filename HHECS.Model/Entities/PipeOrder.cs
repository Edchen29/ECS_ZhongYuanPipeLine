using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 切割机下料工单表
	/// </summary>
    [Table("pipe_order")]
    public partial class PipeOrder : SysEntity
    {
        public PipeOrder()
        {
        }

        /// <summary>
        /// 工单编码
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

        ///// <summary>
        ///// 工位编码
        ///// </summary>
        //[Column("stationCode")]
        //public int? StationCode { get; set; }

        /// <summary>
        /// 原材料编码
        /// </summary>
        [Column("materialCode")]
        public int MaterialCode { get; set; }

        /// <summary>
	    /// 材料编码（材料类型）
	    /// </summary>
        [Column("productCode")]
        public int ProductCode { get; set; }

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
	    /// 长度
	    /// </summary>
        [Column("length")]
        public int Length { get; set; }


        /// <summary>
        /// 状态 :  未套料：0，已经套料：1
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
