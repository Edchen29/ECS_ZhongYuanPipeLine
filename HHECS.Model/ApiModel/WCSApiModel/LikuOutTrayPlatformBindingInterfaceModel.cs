using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    /// <summary>
    /// 立库出库托盘站台绑定接口
    /// </summary>
    public class LikuOutTrayPlatformBindingInterfaceModel
    {
        /// <summary>
        /// 容器编码
        /// </summary>
        public string containerCode { get; set; }
        /// <summary>
        /// 出库口编码
        /// </summary>
        public string outPortCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 平台
        /// </summary>
        public string platform { get; set; }
    }
}
