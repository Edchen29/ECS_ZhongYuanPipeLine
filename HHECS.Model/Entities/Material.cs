using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 物料表
	/// </summary>
    [Table("material")]
    public partial class Material : SysEntity
    {
        public Material()
        {
        }

        /// <summary>
	    /// 物料编号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 物料名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 物料类型
	    /// </summary>
        [Column("type")]
        public string Type { get; set; }
        /// <summary>
	    /// 图号
	    /// </summary>
        [Column("drawingNumber")]
        public string DrawingNumber { get; set; }
        /// <summary>
	    /// 功能类别
	    /// </summary>
        [Column("functionClass")]
        public string FunctionClass { get; set; }
        /// <summary>
	    /// 产品类别
	    /// </summary>
        [Column("buildClass")]
        public string BuildClass { get; set; }
        /// <summary>
	    /// 物料条码
	    /// </summary>
        [Column("barCode")]
        public string BarCode { get; set; }
        /// <summary>
	    /// 关联条码
	    /// </summary>
        [Column("barCode1")]
        public string BarCode1 { get; set; }
        /// <summary>
	    /// 品名规格
	    /// </summary>
        [Column("specification")]
        public string Specification { get; set; }
        /// <summary>
	    /// 重量
	    /// </summary>
        [Column("weight")]
        public decimal? Weight { get; set; }
        /// <summary>
	    /// 单位
	    /// </summary>
        [Column("unit")]
        public string Unit { get; set; }
        /// <summary>
	    /// ABC分类
	    /// </summary>
        [Column("classABC")]
        public string ClassABC { get; set; }
        /// <summary>
	    /// 品检
	    /// </summary>
        [Column("qcCheck")]
        public string QcCheck { get; set; }
        /// <summary>
	    /// 唯一标识码
	    /// </summary>
        [Column("uniqueMark")]
        public string UniqueMark { get; set; }
        /// <summary>
        /// 摆放最大行
        /// </summary>
        [Column("maxRow")]
        public short MaxRow { get; set; }
        /// <summary>
	    /// 摆放最大列
	    /// </summary>
        [Column("maxColumn")]
        public short MaxColumn { get; set; }
        /// <summary>
	    /// 摆放最大层
	    /// </summary>
        [Column("maxLayer")]
        public short MaxLayer { get; set; }
    }
}