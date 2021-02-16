using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 字典类型表
	/// </summary>
    [Table("sys_dict_type")]
    public partial class SysDictType : SysEntity
    {
        public SysDictType()
        {
        }

        /// <summary>
	    /// 字典名称
	    /// </summary>
        [Column("dictName")]
        public string DictName { get; set; }
        /// <summary>
	    /// 字典类型
	    /// </summary>
        [Column("dictType")]
        public string DictType { get; set; }
        /// <summary>
	    /// 备注
	    /// </summary>
        [Column("remark")]
        public string Remark { get; set; }

        //逻辑外键，对应明细
        private List<SysDictData> dictDetails;

        public List<SysDictData> DictDetails
        {
            get { if (dictDetails == null) { dictDetails = new List<SysDictData>(); } return dictDetails; }
            set { dictDetails = value; }
        }
    }
}