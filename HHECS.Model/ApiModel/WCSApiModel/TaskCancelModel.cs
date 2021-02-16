using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    /// <summary>
    /// 任务取消
    /// </summary>
    public class TaskCancelModel
    {
        public List<string> TaskNos { get; set; }
    }

    public class TaskCancelModel1
    {
        public string TaskNo { get; set; }
    }
}
