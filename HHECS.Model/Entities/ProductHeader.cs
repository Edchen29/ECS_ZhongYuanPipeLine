using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 产品表
	/// </summary>
    [Table("product_header")]
    public partial class ProductHeader : SysEntity
    {
        public ProductHeader()
        {
        }

        /// <summary>
	    /// 产品代号
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 产品名称
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 类别
	    /// </summary>
        [Column("type")]
        public string Type { get; set; }
        /// <summary>
	    /// 说明
	    /// </summary>
        [Column("drawingNumber")]
        public string DrawingNumber { get; set; }
        /// <summary>
	    /// 外形尺寸
	    /// </summary>
        [Column("specification")]
        public string Specification { get; set; }
        /// <summary>
	    /// 重量
	    /// </summary>
        [Column("weight")]
        public decimal? Weight { get; set; }
        /// <summary>
	    /// 机型
	    /// </summary>
        [Column("machineType")]
        public string MachineType { get; set; }
        /// <summary>
	    /// 生产车间
	    /// </summary>
        [Column("workShop")]
        public string WorkShop { get; set; }
        /// <summary>
        /// WCS工件类型
        /// </summary>
        [Column("wcsProductType")]
        public int WcsProductType { get; set; }


        #region 新增产品有关的计算
        /// <summary>
        /// 摆放类型
        /// </summary>
        [NotMapped]
        public int PutType { get; set; }

        /// <summary>
        /// 摆放数量---随着摆放的产品类型而更改
        /// </summary>
        [NotMapped]
        public int PutNumber { get; set; }

        /// <summary>
        /// 摆放列数---随着摆放的产品类型区分
        /// </summary>
        [NotMapped]
        public int PutLine { get; set; }

        /// <summary>
        /// 摆放层数---随着摆放的产品类型而设置
        /// </summary>
        [NotMapped]
        public int PutLayer { get; set; }
        #endregion

    }
}