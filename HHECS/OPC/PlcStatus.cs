using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.OPC
{
    public class PlcStatus
    {
        /// <summary>
        /// PLC的IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
