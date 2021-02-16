using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
	/// 系统配置表
	/// </summary>
    [Table("wcsconfig")]
    public partial class WcsConfig : SysEntity
    {
        public WcsConfig()
        {
        }

        /// <summary>
	    /// 
	    /// </summary>
        [Column("warehouseCode")]
        public string WarehouseCode { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        [Column("value")]
        public string Value { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        [Column("remark")]
        public string Remark { get; set; }
        
    }
}
