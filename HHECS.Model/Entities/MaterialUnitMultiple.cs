using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 物料单位换算表
	/// </summary>
    [Table("material_unit_multiple")]
    public partial class MaterialUnitMultiple : SysEntity
    {
        public MaterialUnitMultiple()
        {
        }

        /// <summary>
	    /// 物料编号
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 物料单位
	    /// </summary>
        [Column("materialUnitCode1")]
        public string MaterialUnitCode1 { get; set; }
        /// <summary>
	    /// 物料换算单位
	    /// </summary>
        [Column("materialUnitCode2")]
        public string MaterialUnitCode2 { get; set; }
        /// <summary>
	    /// 换算倍率
	    /// </summary>
        [Column("multiple")]
        public string Multiple { get; set; }
    }
}