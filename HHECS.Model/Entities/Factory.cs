using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 工厂表
	/// </summary>
    [Table("factory")]
    public partial class Factory : SysEntity
    {
        public Factory()
        {
        }

        /// <summary>
	    /// 工厂代号
	    /// </summary>
        [Column("factoryCode")]
        public string FactoryCode { get; set; }
        /// <summary>
	    /// 工厂名称
	    /// </summary>
        [Column("factoryName")]
        public string FactoryName { get; set; }
        /// <summary>
	    /// 地址
	    /// </summary>
        [Column("address")]
        public string Address { get; set; }
        /// <summary>
	    /// 电话
	    /// </summary>
        [Column("telephone")]
        public string Telephone { get; set; }
        /// <summary>
	    /// 集团
	    /// </summary>
        [Column("companyGroup")]
        public string CompanyGroup { get; set; }
    }
}