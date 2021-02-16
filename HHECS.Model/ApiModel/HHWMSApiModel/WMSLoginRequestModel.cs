using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.ApiModel.HHWMSApiModel
{
    /// <summary>
    /// WMS的登录模型
    /// hack:按需修改
    /// </summary>
    public class WMSLoginRequestModel
    {
        [JsonProperty("username")]
        public string Username { get; set; } = "wcs";

        [JsonProperty("password")]
        public string Password { get; set; } = "123456";

        [JsonProperty("warehouseId")]
        public string WarehouseId { get; set; } = "1";

        [JsonProperty("warehouseCode")]
        public string WarehouseCode { get; set; } = "HC0001";
    }
}
