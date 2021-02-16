using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    public class StationRequestInfoModel
    {
        /// <summary>
        /// 站台状态查询
        /// </summary>
        public List<string> stations { get; set; }
    }

    public class StationRequestInfoModel1
    {
        /// <summary>
        /// 站台状态查询
        /// </summary>
        public string station { get; set; }
    }
}
