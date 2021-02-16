using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.Entities
{
    /// <summary>
    ///出入口和堆垛机以及出入站台的映射关系维护表
    ///当标记为出时，以当前DestinationArea，到目标port，选取stationcode，这可能是多个，特定情况下需要做判断
    ///当标记为入时，以当前站台portcode，到目标DestinationArea，选取stationcode，这可能是多个，特定情况下需要做判断
    /// </summary>
    [Table("wcsportsrmstation")]
    public class PortSRMStationRelative : BaseModel
    {
        public string DestinationArea { get; set; }

        public string WarehouseCode { get; set; }

        /// <summary>
        /// 对应设备Code
        /// </summary>
        public string PortCode { get; set; }

        /// <summary>
        /// 对应设备Code
        /// </summary>
        public string StationCode { get; set; }

        /// <summary>
        /// 1,表示出，2，表示入
        /// </summary>
        public int InOutFlag { get; set; }

        public string Remark { get; set; }
    }
}
