using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 工位表
	/// </summary>
    [Table("station")]
    [Serializable]
    public partial class Station : SysEntity
    {
        public Station()
        {
        }

        /// <summary>
	    /// 线体ID
	    /// </summary>
        [Column("lineId")]
        public int? LineId { get; set; }
        /// <summary>
	    /// 线体代号
	    /// </summary>
        [Column("lineCode")]
        public string LineCode { get; set; }
        /// <summary>
	    /// 工位代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 工位名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 顺序
	    /// </summary>
        [Column("sequence")]
        public int? Sequence { get; set; }
        /// <summary>
	    /// 工位属性
	    /// </summary>
        [Column("attribute")]
        public string Attribute { get; set; }
        /// <summary>
	    /// 是否有效
	    /// </summary>
        [Column("enable")]
        public bool? Enable { get; set; }
        /// <summary>
        /// 桁车取货点位ID
        /// </summary>
        [Column("trussTakeStationId")]
        public int TrussTakeStationId { get; set; }
        /// <summary>
        /// 桁车放货点位ID
        /// </summary>
        [Column("trussPutStationId")]
        public int TrussPutStationId { get; set; }
        /// <summary>
        /// 站台X坐标距离
        /// </summary>
        [Column("distance")]
        public int Distance { get; set; }
        /// <summary>
        /// 工位对应正常模式的桁车编码
        /// </summary>
        [Column("transportNormal")]
        public string TransportNormal { get; set; }
        /// <summary>
        /// 工位对应兼容模式的桁车编码
        /// </summary>
        [Column("transportCompatible")]
        public string TransportCompatible { get; set; }
        /// <summary>
        /// 工位类型
        /// </summary>
        [Column("stationType")]
        public string StationType { get; set; }
    }
   
}