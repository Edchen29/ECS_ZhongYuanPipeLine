using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 工艺BOM
	/// </summary>
    [Table("pbom_header")]
    public partial class PbomHeader : SysEntity
    {
        public PbomHeader()
        {
        }

        /// <summary>
	    /// 物料编码
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 类型
	    /// </summary>
        [Column("type")]
        public string Type { get; set; }
    }
}