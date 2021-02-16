using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.AGVWCSModel
{
    /// <summary>
    /// AGV光栅交互
    /// </summary>
    public class Call_Request_Raster
    {
        public AGVRasterModel call_request_raster;

    }
    public class AGVRasterModel
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string order_id { get; set; }


        /// <summary>
        /// agv编号
        /// </summary>
        public int agv_id { get; set; }

        /// <summary>
        /// 库位编号
        /// </summary>
        public string loc_id { get; set; }

        /// <summary>
        /// 托盘编号
        /// </summary>
        public string pallet_no { get; set; }

        /// <summary>
        /// AGV进入:1请求进入 2已经进入
        ///AGV驶出:3请求出来 4已经出来
        ///todo:用枚举封装
        /// </summary>
        public int? status { get; set; }


        /// <summary>
        /// 预留参数1
        /// </summary>
        public string custom_param1 { get; set; }

        /// <summary>
        /// 预留参数2
        /// </summary>
        public string custom_param2 { get; set; }
    }
}

