using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    /// <summary>
    /// 任务明细创建模型  这个主要是为了LED显示用途
    /// </summary>
    public class TaskDetailCreateModel
    {
        public int referLineNo;
        public string materialCode;
        public string materialName;
        public double qty;
        public string unit;
    }
}
