using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
    /// 任务明细实体，不继承basemodel
    /// </summary>
    [Table("wcstaskdetail_deleted")]
    public class TaskDetailEntityDeleted
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int ReferLineId { get; set; }
        public string BillCode { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public double Qty { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool Deleted { get; set; }

    }
}
