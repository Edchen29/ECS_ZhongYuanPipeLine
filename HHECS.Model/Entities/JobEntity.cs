using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    [Table("job")]
    public class JobEntity : BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Api { get; set; }
        public string Param { get; set; }
        public string Corn { get; set; }
        public string ExcludesCorn { get; set; }

        /// <summary>
        /// 最后一次执行状态
        /// </summary>
        public string ExcuteStatus { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
    }
}
