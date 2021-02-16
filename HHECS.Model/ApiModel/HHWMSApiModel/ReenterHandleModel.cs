using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.HHWMSApiModel
{
    /// <summary>
    /// 1.	当发生重入时，需要WCS手动重新提供一个库位号，WCS会将托盘送往指定的库位，然后机械动作完成后再调用任务完成接口，并上传WMS；
    /// 2.	当发生重入时，需要WMS 分配一个库位给WCS，这个时候也是调用这个接口，区别是rediretionLocationCode为0
    /// </summary>
    public class ReenterHandleModel
    {
        /// <summary>
        /// 任务号
        /// </summary>
        public string TaskNo { get; set; }
        /// <summary>
        /// 目的位置编码
        /// </summary>
        public string ToLocationCode { get; set; }
        /// <summary>
        /// 重入的库位编码，不为0说明wcs内部选取好库位，wms需要进行处理维护数据，当0为的时候，wms需要选取一个同区域，同类型地新库位下发，
        /// </summary>
        public string RedirectionLocationCode { get; set; }
    }
}
