using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 测长下料缓存表
	/// </summary>
    [Table("LthMeasuring_Cache")]
    public partial class LthMeasuringCache : SysEntity
    {
        public LthMeasuringCache()
        {
        }

        /// <summary>
	    /// 缓存位编码
	    /// </summary>
        [Column("code")]
        public int Code { get; set; }

        /// <summary>
	    /// 管材标识
	    /// </summary>
        [Column("pipeId")]
        public int PipeId { get; set; }

        /// <summary>
	    /// 管材编码
	    /// </summary>
        [Column("pipeCode")]
        public int PipeCode { get; set; }

        /// <summary>
	    /// 材料
	    /// </summary>
        [Column("materialtype")]
        public int MaterialType { get; set; }

        /// <summary>
	    /// 直径
	    /// </summary>
        [Column("diameter")]
        public int Diameter { get; set; }

        /// <summary>
	    /// 壁厚
	    /// </summary>
        [Column("thickness")]
        public int Thickness { get; set; }

        /// <summary>
	    /// 原材料长度
	    /// </summary>
        [Column("MaterialLength")]
        public int MaterialLength { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [Column("createBy")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("updateTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创更新用户
        /// </summary>
        [Column("updateBy")]
        public string UpdateBy { get; set; }
    }
}
