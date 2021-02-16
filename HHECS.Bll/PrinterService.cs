using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HHECS.Bll
{
    public class PrinterService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="stx">开始符</param>
        /// <param name="data">数据</param>
        /// <param name="etx">结束符</param>
        /// <returns></returns>
        public BllResult SendPrinterData(string ip, string data)
        {
            try
            {
                string stx = "02";
                string etx = "03";
                var dataRes = Encoding.ASCII.GetBytes(stx + data + etx);

                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 56164);  /*IP地址以及端口号*/
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ManualResetEvent timeout = new ManualResetEvent(false);
                client.BeginConnect(remoteEP, (e) =>
                {
                    try
                    {
                        Socket socket = (Socket)e.AsyncState;
                        socket.EndConnect(e);
                        timeout.Set();
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Log($"连接IP为{ip}的打印机异常：{ex.ToString()}", LogLevel.Exception, ex);
                        System.Console.WriteLine(ex.ToString());
                    }
                }, client);
                //timeout.WaitOne();
                if (!timeout.WaitOne(1000))
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }
                    client.Close();
                    return BllResultFactory.Error($"连接IP为{ip}的照相机超时");
                }
                client.SendTimeout = 400;
                client.ReceiveTimeout = 5000;       /*则需要等待，这里设置5秒时间；*/
                client.Send(dataRes);
                byte[] buffer = new byte[1024];
                int length = client.Receive(buffer);
                string message = Encoding.UTF8.GetString(buffer, 0, length);
                //处理回复消息
                string code = message;

                client.Shutdown(SocketShutdown.Both);
                client.Close();
                return BllResultFactory.Sucess("成功");
            }
            catch (System.Exception ex)
            {
                //client.Shutdown(SocketShutdown.Both);
                //client.Close();
                return BllResultFactory.Error($"打印机处理出现异常{ex.Message}");
            }
        }

        /// <summary>
        /// 字符串转ASCII码
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public static byte[] str2ASCII(String xmlStr)
        {
            return Encoding.ASCII.GetBytes(xmlStr);
        }

        /// <summary>
        /// 组合数据
        /// </summary>
        /// <param name="cmd">01-发送  10-打印</param>
        /// <param name="code">0001 -1号设备</param>
        /// <param name="user">0011 -11号工人</param>
        /// <param name="barcode">最大长度20？,可变的需要添加分隔符; ASCII 3B</param>
        /// <param name="count">次数，是否可以理解为打印次数</param>
        /// <returns></returns>
        public BllResult<NeedDataModel> ConversionData(string cmd, string code, string user, string barcode, string count)
        {
            NeedDataModel needData = new NeedDataModel();

            string length = (cmd.Length + code.Length + user.Length + barcode.Length + count.Length).ToString().PadLeft(4, '0');
            string data = string.Format("{0};{1};{2};{3};{4};{5}", cmd, code, user, barcode, count, length);
            needData.DataSplitting = data;
            return BllResultFactory.Sucess<NeedDataModel>(needData, "");
        }

        public class NeedDataModel
        {
            /// <summary>
            /// 组合数据
            /// </summary>
            public string  DataSplitting { get; set; }
        }
    }
}
