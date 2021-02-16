using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    public class StationResponseInfoModel
    {
        /// <summary>
        /// 站台信息
        /// </summary>
        public string station { get; set; }

        /// <summary>
        /// 状态信息
        /// 1忙，0空闲
        /// </summary>
        public string state { get; set; }
    }
}
