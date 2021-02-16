using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 线边库表
	/// </summary>
    [Table("warehouse")]
    public partial class Warehouse : SysEntity
    {
        public Warehouse()
        {
        }

        /// <summary>
	    /// 仓库编号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 仓库名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 线体ID
	    /// </summary>
        [Column("lineId")]
        public int? LineId { get; set; }
        /// <summary>
	    /// 线体
	    /// </summary>
        [Column("lineCode")]
        public string LineCode { get; set; }
        /// <summary>
	    /// 车间标识
	    /// </summary>
        [Column("workshopId")]
        public int? WorkshopId { get; set; }
        /// <summary>
	    /// 车间
	    /// </summary>
        [Column("workshopCode")]
        public string WorkshopCode { get; set; }
    }
}