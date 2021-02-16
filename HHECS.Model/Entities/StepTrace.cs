using HHECS.Model.Common;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 工序跟踪表
	/// </summary>
    [Table("step_trace")]
    [Serializable]
    public partial class StepTrace : SysEntity
    {
        public StepTrace()
        {
        }

        /// <summary>
	    /// 工单号
	    /// </summary>
        [Column("WONumber")]
        public string WONumber { get; set; }
        /// <summary>
        /// WCS工件类型
        /// </summary>
        [Column("wcsProductType")]
        public int? WcsProductType { get; set; }
        /// <summary>
        /// 产品标识
        /// </summary>
        [Column("productId")]
        public int? ProductId { get; set; }
        /// <summary>
	    /// 产品编码
	    /// </summary>
        [Column("productCode")]
        public string ProductCode { get; set; }
        /// <summary>
	    /// 序列号
	    /// </summary>
        [Column("serialNumber")]
        public string SerialNumber { get; set; }
        /// <summary>
	    /// 线体
	    /// </summary>
        [Column("lineId")]
        public int? LineId { get; set; }
        /// <summary>
	    /// 当前工序
	    /// </summary>
        [Column("stepId")]
        public int? StepId { get; set; }
        /// <summary>
	    /// 当前工位
	    /// </summary>
        [Column("stationId")]
        public int? StationId { get; set; }
        /// <summary>
	    /// 下道工序
	    /// </summary>
        [Column("nextStepId")]
        public int? NextStepId { get; set; }
        /// <summary>
        /// 下道工位
        /// </summary>
        [Column("nextStationId")]
        public int? NextStationId { get; set; }
        /// <summary>
	    /// 是否不良
	    /// </summary>
        [Column("isNG")]
        public bool? IsNG { get; set; }
        /// <summary>
	    /// 不良代号
	    /// </summary>
        [Column("NGcode")]
        public string NGcode { get; set; }
        /// <summary>
	    /// 是否报废
	    /// </summary>
        [Column("isInvalid")]
        public bool? IsInvalid { get; set; }
        /// <summary>
	    /// 进站时间
	    /// </summary>
        [Column("stationInTime")]
        public System.DateTime? StationInTime { get; set; }
        /// <summary>
	    /// 出站时间
	    /// </summary>
        [Column("stationOutTime")]
        public System.DateTime? StationOutTime { get; set; }
        /// <summary>
	    /// 进线时间
	    /// </summary>
        [Column("lineInTime")]
        public System.DateTime? LineInTime { get; set; }
        /// <summary>
	    /// 出线时间
	    /// </summary>
        [Column("lineOutTime")]
        public System.DateTime? LineOutTime { get; set; }
        /// <summary>
	    /// 状态
	    /// </summary>
        [Column("status")]
        public int Status { get; set; }
        /// <summary>
        /// 桁车编码
        /// </summary>
        [Column("srmCode")]
        public string SrmCode { get; set; }
        /// <summary>
        /// 是否重发任务
        /// </summary>
        [Column("sendAgain")]
        public int SendAgain { get; set; }

        /// <summary>
        /// 列编码
        /// </summary>
        [Column("wcsLine")]
        public string WCSLine { get; set; }

        /// <summary>
        /// 层编码
        /// </summary>
        [Column("wcsLayer")]
        public string WCSLayer { get; set; }

        /// <summary>
        /// 组队平台编码（1-4）
        /// </summary>
        [Column("weldingNo")]
        public string WeldingNo { get; set; }

        ///// <summary>
        ///// 工位编码
        ///// </summary>
        //[Column("isChange")]
        //public int IsChange { get; set; }


        /// <summary>
        /// 状态描述
        /// </summary>
        [Editable(false)]
        public string StatusDesc
        {
            get { return typeof(StepTraceStatus).GetDescriptionString(Status); }
        }


        public Step StepIdVM { get; set; }

        public Station StationIdVM { get; set; }

        public Step NextStepIdVM { get; set; }

        public Station NextStationIdVM { get; set; }


        #region 新增

        /// <summary>
        /// 任务生成方式
        /// 默认为 0 自动任务，为 1 手动任务
        /// </summary>
        [Column("createType")]
        public int CreateType { get; set; }

        /// <summary>
        /// 人工确认起始点
        /// </summary>
        [Column("manualStartPoint")]
        public string ManualStartPoint { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        [Column("errCode")]
        public string ErrCode { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        [Column("pipeLength")]
        public int? PipeLength { get; set; }

        /// <summary>
        /// 材料
        /// </summary>
        [Column("pipeMaterial")]
        public int? PipeMaterial { get; set; }

        /// <summary>
        /// 壁厚
        /// </summary>
        [Column("pipeThickness")]
        public decimal? PipeThickness { get; set; }

        /// <summary>
        /// 内径
        /// </summary>
        [Column("pipeDiameter")]
        public decimal? PipeDiameter { get; set; }

        #endregion
    }
}