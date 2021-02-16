using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.BllModel
{
    public class ResponseModel<T>
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("msg")]
        public string Msg { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
