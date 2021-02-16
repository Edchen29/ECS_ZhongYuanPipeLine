using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 车间表
	/// </summary>
    [Table("workshop")]
    public partial class Workshop : SysEntity
    {
        public Workshop()
        {
        }

        /// <summary>
	    /// 车间代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 车间名
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 工厂
	    /// </summary>
        [Column("factoryId")]
        public string FactoryId { get; set; }
    }
}