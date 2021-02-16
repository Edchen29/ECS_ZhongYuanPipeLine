using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.LEDHelper.LEDComponent.DefaultLEDComponent
{
    public class LEDCreateOption
    {
        public string IP { get; set; }
        public uint Port { get; set; }
        
        /// <summary>
        /// 单位秒，一般写1即可
        /// </summary>
        public int Timesec { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }
    }
}
