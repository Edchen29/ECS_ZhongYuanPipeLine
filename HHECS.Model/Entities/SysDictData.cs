using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 字典数据表
	/// </summary>
    [Table("sys_dict_data")]
    public partial class SysDictData : SysEntity
    {
        public SysDictData()
        {
        }

        /// <summary>
	    /// 头表id
	    /// </summary>
        [Column("headerId")]
        public int? HeaderId { get; set; }
        /// <summary>
	    /// 字典排序
	    /// </summary>
        [Column("dictSort")]
        public int? DictSort { get; set; }
        /// <summary>
	    /// 字典标签
	    /// </summary>
        [Column("dictLabel")]
        public string DictLabel { get; set; }
        /// <summary>
	    /// 字典键值
	    /// </summary>
        [Column("dictValue")]
        public string DictValue { get; set; }
        /// <summary>
	    /// 字典类型
	    /// </summary>
        [Column("dictType")]
        public string DictType { get; set; }
        /// <summary>
	    /// 样式属性
	    /// </summary>
        [Column("cssClass")]
        public string CssClass { get; set; }
        /// <summary>
	    /// 回显样式
	    /// </summary>
        [Column("listClass")]
        public string ListClass { get; set; }
        /// <summary>
	    /// 是否默认（Y是 N否）
	    /// </summary>
        [Column("isDefault")]
        public string IsDefault { get; set; }
        /// <summary>
	    /// 备注
	    /// </summary>
        [Column("remark")]
        public string Remark { get; set; }
    }
}