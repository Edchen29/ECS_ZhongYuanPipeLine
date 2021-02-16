using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 物料类别表
	/// </summary>
    [Table("material_type")]
    public partial class MaterialType : SysEntity
    {
        public MaterialType()
        {
        }

        /// <summary>
	    /// 类别代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 类别名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 品检
	    /// </summary>
        [Column("qcCheck")]
        public string QcCheck { get; set; }
    }
}