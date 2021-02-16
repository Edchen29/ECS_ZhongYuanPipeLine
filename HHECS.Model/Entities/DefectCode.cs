using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 不良代码表
	/// </summary>
    [Table("defect_code")]
    public partial class DefectCode : SysEntity
    {
        public DefectCode()
        {
        }

        /// <summary>
	    /// 不良代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 不良名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 工位
	    /// </summary>
        [Column("stationId")]
        public int? StationId { get; set; }
        /// <summary>
	    /// 不良分类
	    /// </summary>
        [Column("type")]
        public string Type { get; set; }
    }
}