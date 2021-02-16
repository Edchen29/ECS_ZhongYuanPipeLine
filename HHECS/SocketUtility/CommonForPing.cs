using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.SocketUtility
{
    public class CommonForPing
    {
        public bool PingTest(string IP)
        {
            bool returnValue = false;
            try
            {
                //ping实例化
                Ping ping = new Ping();
                //发送ip ，返回PingReply
                PingReply pingStatus = ping.Send(IPAddress.Parse(IP), 100);
                //判断是否成功。
                if (pingStatus.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Logger.Log($"PING[{IP}]的时候，发生错误：{ex.Message}", LogLevel.Exception);
            }
            return returnValue;
        }
    }
}
