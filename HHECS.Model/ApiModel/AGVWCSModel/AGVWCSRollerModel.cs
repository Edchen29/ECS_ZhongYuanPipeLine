using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.AGVWCSModel
{
    /// <summary>
    /// 辊筒交互接口
    /// URL:http://127.0.0.1:20000/agv2roller
    /// </summary>
    public class Call_Request_Roller
    {
        public AGVWCSRollerModel call_request_roller;
        
    }
    public class AGVWCSRollerModel
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
        /// Agv上料:1请求上料 2请求上料辊筒转动 3上料完成
        ///Agv下料 :4请求下料 5请求下料辊筒转动 6下料完成
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
