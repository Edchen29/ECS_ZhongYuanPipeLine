using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 物料呼叫主表
	/// </summary>
    [Table("material_call_header")]
    public partial class MaterialCallHeader : SysEntity
    {
        public MaterialCallHeader()
        {
        }

        /// <summary>
        /// 线别
        /// </summary>
        [Column("lineId")]
        public int LineId { get; set; }
        /// <summary>
	    /// 线别
	    /// </summary>
        [Column("lineCode")]
        public string LineCode { get; set; }
        /// <summary>
	    /// 需求工位
	    /// </summary>
        [Column("needStation")]
        public string NeedStation { get; set; }
        /// <summary>
        /// 需求位置
        /// </summary>
        [Column("locationCode")]
        public string LocationCode { get; set; }
        /// <summary>
	    /// 叫料时间
	    /// </summary>
        [Column("callTime")]
        public System.DateTime? CallTime { get; set; }
        /// <summary>
	    /// 状态
	    /// </summary>
        [Column("status")]
        public string Status { get; set; }
        /// <summary>
	    /// 模式
	    /// </summary>
        [Column("mode")]
        public string Mode { get; set; }
        /// <summary>
	    /// 来源平台
	    /// </summary>
        [Column("fromPlatform")]
        public string FromPlatform { get; set; }
        /// <summary>
	    /// 叫料操作员
	    /// </summary>
        [Column("userCode")]
        public string UserCode { get; set; }
    }
}