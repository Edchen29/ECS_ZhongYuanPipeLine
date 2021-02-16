using HslCommunication.Profinet.Siemens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.PLCHelper.PLCComponent.HslComponent
{
    /// <summary>
    /// 构建西门子PLCmodel类
    /// 端口默认102
    /// 请按如下配置传递solt和rack：S400:0,3;S1200:0,0;S300:0,2;S1500:0,0;其他按实际传递；
    /// </summary>
    public class SiemensPLCBuildModel
    {
        public SiemensPLCS SiemensPLCS { get; set; }

        public string IP { get; set; }

        public int Port { get; set; }

        public int Rack { get; set; }

        public int Slot { get; set; }
    }
}
