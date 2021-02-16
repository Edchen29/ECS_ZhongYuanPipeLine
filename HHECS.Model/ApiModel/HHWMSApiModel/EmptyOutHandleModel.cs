using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.HHWMSApiModel
{
    /// <summary>
    /// 空出处理接口
    /// 1.当发生空出时，WCS会调用此接口，调用成功后，WCS会删除此任务，不会再调用任务完成接口；
    /// </summary>
    public class EmptyOutHandleModel
    {
        public string TaskNo { get; set; }
    }
}
