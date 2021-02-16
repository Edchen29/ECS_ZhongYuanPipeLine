using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 工序表
	/// </summary>
    [Table("step")]
    [Serializable]
    public partial class Step : SysEntity
    {
        public Step()
        {
        }

        /// <summary>
	    /// 产品标识
	    /// </summary>
        [Column("productId")]
        public int? ProductId { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 机型
	    /// </summary>
        [Column("machineType")]
        public string MachineType { get; set; }
        /// <summary>
	    /// 产品类别
	    /// </summary>
        [Column("productType")]
        public string ProductType { get; set; }
        /// <summary>
	    /// 物料ID
	    /// </summary>
        [Column("materialId")]
        public int? MaterialId { get; set; }
        /// <summary>
	    /// 物料编码
	    /// </summary>
        [Column("materialCode")]
        public string MaterialCode { get; set; }
        /// <summary>
	    /// 工序代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 工序名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// 工位类型
        /// </summary>
        [Column("stationType")]
        public string StationType { get; set; }
        /// <summary>
	    /// 周期
	    /// </summary>
        [Column("cycleTime")]
        public string CycleTime { get; set; }
        /// <summary>
	    /// 工序序号
	    /// </summary>
        [Column("sequence")]
        public int? Sequence { get; set; }
        /// <summary>
	    /// 计划开始时间
	    /// </summary>
        [Column("planStartTime")]
        public System.DateTime? PlanStartTime { get; set; }
        /// <summary>
	    /// 计划结束时间
	    /// </summary>
        [Column("planEndTime")]
        public System.DateTime? PlanEndTime { get; set; }
        /// <summary>
	    /// 实际开始时间
	    /// </summary>
        [Column("actualStartTime")]
        public System.DateTime? ActualStartTime { get; set; }
        /// <summary>
	    /// 实际结束时间
	    /// </summary>
        [Column("actualEndTime")]
        public System.DateTime? ActualEndTime { get; set; }
        /// <summary>
	    /// 作业指导书
	    /// </summary>
        [Column("linkSop")]
        public string LinkSop { get; set; }
        /// <summary>
	    /// 控制码
	    /// </summary>
        [Column("ctrlCode")]
        public string CtrlCode { get; set; }
        /// <summary>
	    /// MES顺序号
	    /// </summary>
        [Column("mesSeqence")]
        public int? MesSeqence { get; set; }
        /// <summary>
        /// 工序程序
        /// </summary>
        [Column("programCode")]
        public string ProgramCode { get; set; }
        /// <summary>
	    /// 工作中心
	    /// </summary>
        [Column("workCenter")]
        public string WorkCenter { get; set; }
    }
}