using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Controls;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Window = System.Windows.Window;

namespace HHECS.MarkingMachineServer
{
    /// <summary>
    /// WinMain.xaml 的交互逻辑
    /// </summary>
    public partial class WinMain : Window
    {
        /// <summary>
        /// 日志监控
        /// </summary>
        public LogInfo LogInfo { get; set; }

        //创建一个和客户端通信的套接字
        public static Socket SocketWatch = null;

        //定义一个集合，存储客户端信息
        public static Dictionary<string, Socket> ClientConnectionItems = new Dictionary<string, Socket> { };

        /// <summary>
        /// 创建一个线程
        /// </summary>
        /// <param name="o"></param>
        Socket socketSend;
        Dictionary<string, Socket> dicsocket = new Dictionary<string, Socket>();
        /// <summary>
        /// 次数
        /// </summary>
        int count = 0;
        /// <summary>
        /// 端口号（用来监听的）
        /// </summary>
        public static int port = 8080;

        public WinMain()
        {
            InitializeComponent();

            Init();

            //日志初始化
            Logger.LogWrite += Logger_LogWrite;
            LogInfo = new LogInfo(500d, 550d);
            this.LogPanel.Children.Add(LogInfo);

        }

        private void Init()
        {
            string strIp = "172.20.10.4";
            var task1 = Task.Run(() =>
            {
                IPAddress ip = IPAddress.Parse(strIp);

                //将IP地址和端口号绑定到网络节点point上  
                IPEndPoint ipe = new IPEndPoint(ip, port);
                //定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）  
                SocketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //监听绑定的网络节点  
                SocketWatch.Bind(ipe);
                //将套接字的监听队列长度限制为20  
                SocketWatch.Listen(1000);

                //负责监听客户端的线程:创建一个监听线程  
                Thread threadwatch = new Thread(WatchConnecting);
                //将窗体线程设置为与后台同步，随着主线程结束而结束  
                threadwatch.IsBackground = true;
                //启动线程     
                threadwatch.Start();

                string log = $"ECS-->[开始监听],时间{DateTime.Now}";
                Logger.Log($"{log}", LogLevel.Info);

                Thread th = new Thread(Listen);
                th.IsBackground = true;
                th.Start(SocketWatch);
                //SocketWatch.Close();

            });

            var task2 = Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    //var result = InstructionHandler.ReHandleMessage();
                    //if (!result.Success)
                    //{
                    //    Logger.Log(result.Msg);
                    //}
                }
            });
        }

        /// <summary>
        /// 监听客户端发来的请求 
        /// </summary>
        public static void WatchConnecting()
        {
            Socket connection = null;

            //持续不断监听客户端发来的请求     
            while (true)
            {
                try
                {
                    connection = SocketWatch.Accept();
                }
                catch (Exception ex)
                {
                    //提示套接字监听异常     
                    string exMsg = ex.Message;
                    Logger.Log($" 监听客户端发生异常：{exMsg} ", LogLevel.Exception);
                    break;
                }

                //客户端网络结点号  
                string remoteEndPoint = connection.RemoteEndPoint.ToString();
                //添加客户端信息  
                ClientConnectionItems.Add(remoteEndPoint, connection);
                //显示与客户端连接情况
                string logs = $" 客户端{remoteEndPoint}建立连接成功！ 客户端数量：{ClientConnectionItems.Count} ";
                Logger.Log($"{logs}", LogLevel.Info);

                //获取客户端的IP和端口号  
                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;

                //让客户显示"连接成功的"的信息  
                string sendmsg = $"clientIP：{clientIP},clientPort：{clientPort.ToString()}ServerConnectionSuccessful!";
                byte[] arrSendMsg = Encoding.ASCII.GetBytes(sendmsg);
                connection.Send(arrSendMsg);
                //创建一个通信线程      
                Thread thread = new Thread(Recv);
                //设置为后台线程，随着主线程退出而退出 
                thread.IsBackground = true;
                //启动线程     
                thread.Start(connection);
            }
        }
        /// <summary>
        /// 接收客户端发来的信息，客户端套接字对象
        /// </summary>
        /// <param name="socketclientpara"></param>    
        public static void Recv(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;
            while (true)
            {
                //创建一个内存缓冲区，其大小为1024*1024字节  即1M     
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                //将接收到的信息存入到内存缓冲区，并返回其字节数组的长度    
                try
                {
                    int length = socketServer.Receive(arrServerRecMsg);

                    //将机器接受到的字节数组转换为人可以读懂的字符串     
                    string strSRecMsg = Encoding.ASCII.GetString(arrServerRecMsg, 0, length);

                    Logger.Log($"接收客户端发来的数据{strSRecMsg}", LogLevel.Info);

                    var handMsg = HandClientRecsiveMsg(strSRecMsg);
                    if (handMsg.Success)
                    {
                        socketServer.Send(Encoding.ASCII.GetBytes($"{socketServer.RemoteEndPoint}" + strSRecMsg));
                    }
                    else
                    {
                        socketServer.Send(Encoding.ASCII.GetBytes($"{socketServer.RemoteEndPoint}" + strSRecMsg));
                    }

                    //Thread.Sleep(3000);
                    //发送客户端数据
                    if (ClientConnectionItems.Count > 0)
                    {
                        foreach (var socketTemp in ClientConnectionItems)
                        {
                            socketTemp.Value.Send(Encoding.ASCII.GetBytes("[" + socketServer.RemoteEndPoint + "]：" + strSRecMsg));
                        }
                    }
                }
                catch (Exception)
                {
                    ClientConnectionItems.Remove(socketServer.RemoteEndPoint.ToString());
                    //提示套接字监听异常  
                    string logEx = $"客户端{socketServer.RemoteEndPoint}已经中断连接！ 客户端数量：{ClientConnectionItems.Count}";
                    Logger.Log($"{logEx}", LogLevel.Exception);

                    //关闭之前accept出来的和客户端进行通信的套接字 
                    socketServer.Close();
                    break;
                }
            }
        }

        public void Listen(object o)
        {
            try
            {
                while (true)
                {
                    Socket socketWatch = o as Socket;
                    //不停的等待客户端的连接
                    socketSend = socketWatch.Accept();
                    Logger.Log($"客户端{socketSend.RemoteEndPoint}:连接成功:第{count}个", LogLevel.Info);

                    dicsocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend);//讲数据存入字典内
                    Logger.Log($"将数据存入字典内：{socketSend.RemoteEndPoint.ToString()}", LogLevel.Info);

                    try
                    {
                        string strPoint = socketSend.RemoteEndPoint.ToString();
                        //将ip地址 添加到下拉列表中

                        Dispatcher.Invoke(() => { txt_Ip.Text = strPoint; });
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($" 将数据存放入字典内错误：{ex.Message} ", LogLevel.Info);
                    }

                    Thread th = new Thread(Receive);
                    th.IsBackground = true;
                    th.Start(socketSend);
                    count++;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($" 异常：等待客户端连接 {ex.Message} ", LogLevel.Info);
            }
        }

        /// <summary>
        /// 用来接收客户端的消息
        /// </summary>
        /// <param name="obj"></param>
        public void Receive(object obj)
        {
            try
            {
                while (true)
                {
                    Socket socketSend = obj as Socket;
                    byte[] buffer = new byte[1024];
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    //如果有请求在这里解析并判断是否返回数据
                    string str = Encoding.ASCII.GetString(buffer, 0, r);

                    Logger.Log($"接收客户端{ socketSend.RemoteEndPoint}:消息内容:\r\n{str}", LogLevel.Info);

                    //处理消息，返回结果
                    var clientMsgResult = HandClientRecsiveMsg(str);
                    if (clientMsgResult.Success)
                    {
                        //处理成功放回ok，其他异常
                        string strOK = "OK;";
                        byte[] bufferOK = Encoding.ASCII.GetBytes(strOK);
                        List<byte> list = new List<byte>();
                        list.Add(0);
                        list.AddRange(bufferOK);
                        byte[] newbuffer = list.ToArray();
                        socketSend.Send(newbuffer);
                        Logger.Log($"服务端处理结果:{strOK};", LogLevel.Info);
                    }
                    else
                    {
                        string strFail = $"Fail:{clientMsgResult.Msg}";
                        byte[] bufferOK = Encoding.ASCII.GetBytes(strFail);
                        List<byte> list = new List<byte>();
                        list.Add(0);
                        list.AddRange(bufferOK);
                        byte[] newbuffer = list.ToArray();
                        socketSend.Send(newbuffer);
                        Logger.Log($"服务端处理结果:{strFail};", LogLevel.Info);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Log($"异常 ：接收客户端消息{socketSend.RemoteEndPoint }:{ex.ToString()}", LogLevel.Exception);
            }
        }

        /// <summary>
        /// 处理接收到客户端的消息 
        /// HACK：目前只返回成功
        /// </summary>
        /// <param name="RecsiveMsg">客户端消息</param>
        /// <returns></returns>
        public static BllResult HandClientRecsiveMsg(string clientRecsiveMsg)
        {
            return BllResultFactory.Sucess();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public BllResult SendMessage(string ip, byte[] buffer)
        {
            try
            {
                //获取客户端socket
                if (dicsocket.ContainsKey(ip))
                {
                    dicsocket[ip].Send(buffer);
                    return BllResultFactory.Sucess();
                }
                else
                {
                    Logger.Log($"找不到ip为 {ip} 客户端连接", LogLevel.Error);
                    return BllResultFactory.Error();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("异常" + ex.Message, LogLevel.Exception);
                return BllResultFactory.Error();
            }
        }

        #region 转换数据方法
        /// <summary>
        /// 转换数据方法
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="code"></param>
        /// <param name="user"></param>
        /// <param name="barcode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public BllResult<NeedDataModel> ConversionData(string cmd, string code, string user, string barcode, string count)
        {
            NeedDataModel needData = new NeedDataModel();

            string length = (cmd.Length + code.Length + user.Length + barcode.Length + count.Length).ToString().PadLeft(4, '0');
            string data = string.Format("{0};{1};{2};{3};{4};{5}", cmd, code, user, barcode, count, length);
            needData.DataSplitting = "STX" + data + "ETX";
            return BllResultFactory.Sucess<NeedDataModel>(needData, "");
        }

        public class NeedDataModel
        {
            /// <summary>
            /// 组合数据
            /// </summary>
            public string DataSplitting { get; set; }
        }
        #endregion

        #region 地址
        #region 服务配置
        private static async Task<SettingsModel> ReadSettinsAsync()
        {
            try
            {
                var opcConfigResult = AppSession.BllService.GetConfig(SysConst.ServerIP.ToString());
                if (opcConfigResult.Success)
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"读取配置错误：{ex.Message}；", LogLevel.Exception);
                return null;
            }
        }
        #endregion

        #region  服务地址
        /// <summary>
        /// 服务地址
        /// </summary>
        public class SettingsModel
        {
            public string ServerIP { get; set; }

            public int Port { get; set; }
        }
        #endregion
        #endregion

        #region 按钮事件
        /// <summary>
        /// 停止监听，Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            string strStop = $"停止监听，时间：{DateTime.Now}";
            Logger.Log($"{strStop}", LogLevel.Info);
            SocketWatch.Close();
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            string cmd = Txt_CMD.Text.Trim();
            string code = Txt_CODE.Text.Trim();
            string user = Txt_USER.Text.Trim();
            string barcode = Txt_BARCODE.Text.Trim();
            string count = Txt_COUNT.Text.Trim();
            var sendData = ConversionData(cmd, code, user, barcode, count);
            byte[] buffer = Encoding.ASCII.GetBytes(sendData.Data.DataSplitting);
            List<byte> list = new List<byte>();
            list.Add(0);
            list.AddRange(buffer);
            byte[] newbuffer = list.ToArray();
            //dicsocket.Add(ip, socketSend);
            var sendResult = SendMessage(txt_Ip.Text.Trim(), newbuffer);
            if (sendResult.Success)
            {
                Logger.Log($"发送正确{sendResult.Msg}", LogLevel.Info);
            }
            //else
            //{
            //    Logger.Log($"发送错误{sendResult.Msg}", LogLevel.Error);
            //}
        }

        #endregion

        #region HACK:暂时注释
        /// <summary>
        /// 发送数据
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
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8080);  /*IP地址以及端口号*/
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
                string message = Encoding.ASCII.GetString(buffer, 0, length);
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
        #endregion

        #region  日志组件

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Logger_LogWrite(object sender, LogEventArgs args)
        {
            Dispatcher.Invoke(() =>
            {
                switch (args.LogLevel)
                {
                    case LogLevel.Info:
                    case LogLevel.Error:
                    case LogLevel.Warning:
                    case LogLevel.Success:
                        //AppSession.LogService.WriteLog(args.LogLevel.ToString(), args.Content);
                        LogInfo.AddLogs(args.Content, args.LogLevel);
                        break;
                    case LogLevel.Exception:
                        //AppSession.LogService.WriteExceptionLog(args.Content, args.Exception);
                        LogInfo.AddLogs(args.Content, args.LogLevel);
                        break;
                    case LogLevel.PLC:
                        //AppSession.LogService.WriteLog(LogLevel.Success.ToString(), args.Content);
                        break;
                    default:
                        break;
                }
            });
        }
        #endregion
    }
}
