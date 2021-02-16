using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 物料单位表
	/// </summary>
    [Table("material_unit")]
    public partial class MaterialUnit : SysEntity
    {
        public MaterialUnit()
        {
        }

        /// <summary>
	    /// 物料编号
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 单位代码
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 单位名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
    }
}