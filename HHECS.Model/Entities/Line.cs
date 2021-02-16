using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 线体表
	/// </summary>
    [Table("line")]
    public partial class Line : SysEntity
    {
        public Line()
        {
        }

        /// <summary>
	    /// 车间ID
	    /// </summary>
        [Column("workshopId")]
        public int? WorkshopId { get; set; }
        /// <summary>
	    /// 车间代号
	    /// </summary>
        [Column("workshopCode")]
        public string WorkshopCode { get; set; }
        /// <summary>
	    /// 线体代号
	    /// </summary>
        [Column("lineCode")]
        public string LineCode { get; set; }
        /// <summary>
	    /// 线体名称
	    /// </summary>
        [Column("lineName")]
        public string LineName { get; set; }
    }
}