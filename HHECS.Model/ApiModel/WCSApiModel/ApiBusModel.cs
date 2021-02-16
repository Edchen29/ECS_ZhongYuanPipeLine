using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HHECS.Model.ApiModel.WCSApiModel
{
    /// <summary>
    /// API模型类
    /// </summary>
    public class ApiBusModel
    {
        //[JsonProperty("a")]
        public string Api { get; set; }
        //[JsonProperty("b")]
        public string Param { get; set; }
    }
}