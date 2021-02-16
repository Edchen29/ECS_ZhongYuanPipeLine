using HHECS.Model.Common;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.SocketUtility
{
    public class SocketClient
    {
        private Socket socket;
        private IPEndPoint endPoint;
        private byte[] buffer = new byte[1024];

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">连接服务器的IP</param>
        /// <param name="port">连接服务器的端口</param>
        public SocketClient(string ip, int port)
        {
            try
            {
                // 创建IP对象
                IPAddress address = IPAddress.Parse(ip);
                // 创建网络端口包括ip和端口
                this.endPoint = new IPEndPoint(address, port);
                // 实例化套接字(IP4寻址地址,流式传输,TCP协议)
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, LogLevel.Exception);
            }
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        public void connect()
        {
            try
            {
                this.socket.Connect(endPoint);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, LogLevel.Exception);
            }
        }

        /// <summary>
        /// 判断socket是否连接
        /// </summary>
        public bool connected()
        {
            bool connected = false;
            try
            {
                connected = this.socket.Connected;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, LogLevel.Exception);
            }
            return connected;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public string send(string content)
        {
            string result = "";
            try
            {
                // 向服务器发送消息
                byte[] data = Encoding.UTF8.GetBytes(content);
                socket.Send(data);
                // 接收数据
                System.Threading.Thread.Sleep(10);
                int length = socket.Receive(buffer);
                result = Encoding.UTF8.GetString(buffer, 0, length);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, LogLevel.Exception);
            }
            return result;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void close()
        {
            try
            {
                this.socket.Shutdown(SocketShutdown.Both);
                this.socket.Close();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, LogLevel.Exception);
            }
        }
    }
}
