using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
    /// 成品下线站台
    /// </summary>

    [Table("product_Finished_Station")]
    public class ProductFinishedStation : SysEntity
    {
        public ProductFinishedStation()
        {
        }

        /// <summary>
	    /// 站台设备编码
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// 站台名字
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        [Column("line")]
        public short? Line { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        [Column("layer")]
        public short? Layer { get; set; }

        /// <summary>
        /// 站台类型
        /// </summary>
        [Column("type")]
        public string Type { get; set; }

        /// <summary>
        /// 对应设备Code
        /// </summary>
        [Column("stationCode")]
        public string StationCode { get; set; }

        /// <summary>
        /// 锁定 
        /// 0：解锁状态
        /// 1：设备离开的锁定
        /// 2：已经放满的锁定
        /// 3：其他锁定
        /// </summary>
        [Column("islock")]
        public int IsLock { get; set; }

        [Column("isStop")]
        public bool IsStop { get; set; }

        #region 新增产品有关的计算
        /// <summary>
        /// 工件类型
        /// </summary>
        [Column("productType")]
        public int ProductType { get; set; }

        /// <summary>
        /// 摆放类型
        /// </summary>
        [Column("putType")]
        public int PutType { get; set; }

        /// <summary>
        /// 摆放数量---随着摆放的产品类型而更改
        /// </summary>
        [Column("putNumber")]
        public int PutNumber { get; set; }

        /// <summary>
        /// 摆放列数---随着摆放的产品类型区分
        /// </summary>
        [Column("putLine")]
        public int PutLine { get; set; }

        /// <summary>
        /// 摆放层数---随着摆放的产品类型而设置
        /// </summary>
        [Column("putLayer")]
        public int PutLayer { get; set; }
        #endregion
    }
}
