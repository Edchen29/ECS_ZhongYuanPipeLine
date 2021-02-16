using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
    /// 小车任务表
    /// </summary>
    [Table("car_task")]
    [Serializable]
    public class CarTask : SysEntity
    {
        public CarTask()
        {

        }
        /// <summary>
        /// 任务监控标识
        /// </summary>
        [Column("stepTraceId")] 
        public int? StepTraceId { get; set; }
        /// <summary>
        /// 小车编码
        /// </summary>
        [Column("carNo")]
        public int CarNo { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [Column("type")]
        public int Type { get; set; }
        /// <summary>
        /// 源位置
        /// </summary>
        [Column("fromLocation")]
        public string FromLocation { get; set; }
        /// <summary>
        /// 目标位置
        /// </summary>
        [Column("toLocation")]
        public string ToLocation { get; set; }
        /// <summary>
        /// 是否重新下发
        /// </summary>
        [Column("reSend")]
        public int ReSend { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
        /// <summary>
        /// 是否翻转
        /// </summary>
        [Column("isFlip")]
        public int IsFlip { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("startTime")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Column("endTime")]
        public DateTime EndTime { get; set; }
    }
}
