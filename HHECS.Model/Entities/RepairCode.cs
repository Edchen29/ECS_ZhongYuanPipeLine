using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 维修代码表
	/// </summary>
    [Table("repair_code")]
    public partial class RepairCode : SysEntity
    {
        public RepairCode()
        {
        }

        /// <summary>
	    /// 维修代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 维修名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 不良代号
	    /// </summary>
        [Column("NGCode")]
        public string NGCode { get; set; }
    }
}