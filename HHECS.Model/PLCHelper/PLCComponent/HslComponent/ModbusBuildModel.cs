using HslCommunication.Profinet.Siemens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.PLCComponent.HslComponent
{
    /// <summary>
    /// 构建 Modbus 类
    /// 端口默认502
    /// 站台默认0x01
    /// </summary>
    public class ModbusBuildModel
    {
        public string IP { get; set; }

        public int Port { get; set; } = 502;

        public byte Station { get; set; } = 0x01;
    }
}
