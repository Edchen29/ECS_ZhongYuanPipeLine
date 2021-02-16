using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 设计BOM主表
	/// </summary>
    [Table("ebom_header")]
    public partial class EbomHeader : SysEntity
    {
        public EbomHeader()
        {
        }

        /// <summary>
	    /// 产品代号
	    /// </summary>
        [Column("itemCode")]
        public string ItemCode { get; set; }
        /// <summary>
	    /// 子料
	    /// </summary>
        [Column("children")]
        public string Children { get; set; }
    }
}