using HHECS.Model.Common;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HHECS.Model.Entities;
using SanNiuSignal;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HHECS.Model.Controls;
using HHECS.Bll;
using HHECS.Model.BllModel;

namespace HHECS.MarkingClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 日志监控
        /// </summary>
        public LogInfo LogInfo { get; set; }

        private ITxClient TxClient = null;

        private ITxClient TxClient1 = null;

        private ITxClient TxClient2 = null;

        public int port1 = 10020;

        public int port2 = 10020;

        public int port3 = 10020;
        public MainWindow()
        {
            InitializeComponent();

            init1();

            //init2();

            //init3();

            //日志初始化
            Logger.LogWrite += Logger_LogWrite;
            LogInfo = new LogInfo(500d, 550d);
            this.LogPanel.Children.Add(LogInfo);
        }

        #region 客户端连接

        /// <summary>
        /// 连接192.168.1.191
        /// </summary>
        private void init1()
        {
            string ip = "127.0.0.1";
            try
            {
                var task1 = Task.Run(() =>
                {
                    TxClient = TxStart.startClient(ip, port1);
                    TxClient.AcceptString += new TxDelegate<IPEndPoint, string>(accptString);//当收到文本数据的时候
                    //TxClient.AcceptByte += new TxDelegate<IPEndPoint, byte[]>(accptByte);//当收到文本数据的时候

                    TxClient.dateSuccess += new TxDelegate<IPEndPoint>(sendSuccess);//当对方已经成功收到我数据的时候
                    TxClient.EngineClose += new TxDelegate(engineClose);//当客户端引擎完全关闭释放资源的时候
                    TxClient.EngineLost += new TxDelegate<string>(engineLost);//当客户端非正常原因断开的时候
                    TxClient.ReconnectionStart += new TxDelegate(reconnectionStart);//当自动重连开始的时候
                    TxClient.StartResult += new TxDelegate<bool, string>(startResult);//当登录完成的时候
                    //TxClient.BufferSize = 12048;//这里大小自己设置，默认为1KB，也就是1024个字节
                    TxClient.StartEngine();
                });

                //处理任务数据
                Task.Run(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(200);
                        var res = ProcessingTask();
                        if (res.Success)
                        {
                            //数据组合后，发送数据
                            string sql = $"update print_Middle set commintFlag={(int)PrintCommintFlag.已发送} where id ={res.Data.PrintID}";
                            AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                            Logger.Log($"发送数据至IP{ip}：{res.Data.DataSplitting}", LogLevel.Info);
                            TxClient.sendMessage(res.Data.DataSplitting);
                        }
                        else
                        {
                            Logger.Log($"异常：发送数据至IP{ip}：{res.Data.DataSplitting}", LogLevel.Error);
                        }
                    }
                });
            }
            catch (Exception Ex)
            {
                Logger.Log($"连接{ip}异常{Ex.Message}", LogLevel.Exception);
            }
        }

        /// <summary>
        /// 连接192.168.1.192
        /// </summary>
        private void init2()
        {
            string ip = "";
            try
            {
                var task2 = Task.Run(() =>
                {
                    TxClient2 = TxStart.startClient(ip, port2);
                    TxClient2.AcceptString += new TxDelegate<IPEndPoint, string>(accptString);//当收到文本数据的时候
                    TxClient2.dateSuccess += new TxDelegate<IPEndPoint>(sendSuccess);//当对方已经成功收到我数据的时候
                    TxClient2.EngineClose += new TxDelegate(engineClose);//当客户端引擎完全关闭释放资源的时候
                    TxClient2.EngineLost += new TxDelegate<string>(engineLost);//当客户端非正常原因断开的时候
                    TxClient2.ReconnectionStart += new TxDelegate(reconnectionStart);//当自动重连开始的时候
                    TxClient2.StartResult += new TxDelegate<bool, string>(startResult);//当登录完成的时候
                    //TxClient.BufferSize = 12048;//这里大小自己设置，默认为1KB，也就是1024个字节
                    TxClient2.StartEngine();
                });
            }
            catch (Exception ex)
            {
                Logger.Log($"连接{ip}异常{ex.Message}", LogLevel.Error);
            }
        }

        /// <summary>
        /// 连接192.168.1.193
        /// </summary>
        private void init3()
        {

            string ip = "127.0.0.1";
            try
            {
                var task3 = Task.Run(() =>
                {
                    TxClient1 = TxStart.startClient(ip, port3);
                    TxClient1.AcceptString += new TxDelegate<IPEndPoint, string>(accptString);//当收到文本数据的时候
                    TxClient1.dateSuccess += new TxDelegate<IPEndPoint>(sendSuccess);//当对方已经成功收到我数据的时候
                    TxClient1.EngineClose += new TxDelegate(engineClose);//当客户端引擎完全关闭释放资源的时候
                    TxClient1.EngineLost += new TxDelegate<string>(engineLost);//当客户端非正常原因断开的时候
                    TxClient1.ReconnectionStart += new TxDelegate(reconnectionStart);//当自动重连开始的时候
                    TxClient1.StartResult += new TxDelegate<bool, string>(startResult);//当登录完成的时候

                    //TxClient.BufferSize = 12048;//这里大小自己设置，默认为1KB，也就是1024个字节
                    TxClient1.StartEngine();
                });
            }
            catch (Exception ex)
            {
                Logger.Log($"连接{ip}异常{ex.Message}", LogLevel.Error);
            }

        }

        #endregion

        #region TCP客户端区

        /// <summary>
        /// 处理任务数据
        /// </summary>
        public BllResult<NeedDataModel> ProcessingTask()
        {
            var resPrintData = AppSession.Dal.GetCommonModelByCondition<PrintMiddleModel>($"");
            if (resPrintData.Success)
            {
                var temp = resPrintData.Data[0];
                var combinedData = ConversionData(temp.Cmd, temp.Code, temp.User, temp.Barcode, temp.Count, temp.Id.ToString());
                if (combinedData.Success)
                {
                    Logger.Log($"{temp.Id}组合数据{combinedData.Data.DataSplitting}", LogLevel.Error);
                    return BllResultFactory<NeedDataModel>.Sucess(combinedData.Data);
                }
                return BllResultFactory<NeedDataModel>.Error("组合数据失败");
            }
            else
            {
                Logger.Log($"", LogLevel.Error);
                return BllResultFactory<NeedDataModel>.Error("查询失败");
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
        public BllResult<NeedDataModel> ConversionData(string cmd, string code, string user, string barcode, string count, string id)
        {
            NeedDataModel needData = new NeedDataModel();
            char stx = (char)0x02;
            char etx = (char)0x03;
            string length = (cmd.Length + code.Length + user.Length + barcode.Length + count.Length).ToString().PadLeft(4, '0');
            string data = string.Format("{0};{1};{2};{3};{4};{5};{6}", cmd, code, user, barcode, count, length, id);
            needData.DataSplitting = stx + data + etx;
            //needData.DataSplitting = data;
            return BllResultFactory.Sucess<NeedDataModel>(needData, "");
        }
        public class NeedDataModel
        {
            /// <summary>
            /// 根据此id更新数据
            /// </summary>
            public string PrintID { get; set; }
            /// <summary>
            /// 组合数据
            /// </summary>
            public string DataSplitting { get; set; }
        }
        #endregion

        /// <summary>
        /// 接收到文本数据的时候 以string处理
        /// </summary>
        /// <param name="str"></param>
        private void accptString(IPEndPoint end, string message)
        {
            Logger.Log($"收到IP:{end.Address}端口：{end.Port}接收到数据：{message}", LogLevel.Info);
            string code = message;
            int index = code.LastIndexOf(";");
            string codeId = code.Substring(index + 1);
            //string sendSuccessMsg = $"{codeId};06";
            //string sendFailMsg = $"{codeId};15";

            var res = AppSession.Dal.GetCommonModelByCondition<PrintMiddleModel>($"WHERE id = {codeId}");
            if (res.Success)
            {
                string sql = $"update print_Middle set commintFlag={(int)PrintCommintFlag.已打印} where id={codeId}";
                AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
            }
            else
            {
                Logger.Log($"查询打印数据失败{codeId}，信息：{res.Msg}", LogLevel.Error);
            }
        }

        /// <summary>
        /// 接收到文本数据 以Byte处理
        /// </summary>
        /// <param name="end"></param>
        /// <param name="bytes"></param>
        private void accptByte(IPEndPoint end, byte[] bytes)
        {
            Logger.Log($"地址{end.Address}端口：{end.Port}接收到数据：{bytes}", LogLevel.Info);
        }

        /// <summary>
        /// 当数据发送成功的时候
        /// </summary>
        private void sendSuccess(IPEndPoint end)
        {
            Logger.Log($"发送数据{end.Address},端口{end.Port}", LogLevel.Info);
        }
        /// <summary>
        /// 当客户端引擎完全关闭的时候
        /// </summary>
        private void engineClose()
        {

        }
        /// <summary>
        /// 当客户端突然断开的时候
        /// </summary>
        /// <param name="str">断开原因</param>
        private void engineLost(string str)
        {
            Logger.Log($"客户端断开{str}", LogLevel.Warning);
        }
        /// <summary>
        /// 当自动重连开始的时候
        /// </summary>
        private void reconnectionStart()
        {
            Logger.Log("自动重连开始", LogLevel.Warning);
        }
        /// <summary>
        /// 当登录有结果的时候
        /// </summary>
        /// <param name="b">是否成功</param>
        /// <param name="str">失败或成功原因</param>
        private void startResult(bool b, string str)
        {
            if (b == true)
            {
                Logger.Log($"登录成功：原因{str}", LogLevel.Info);
            }
            else
            {
                Logger.Log($"登录失败：原因{str}", LogLevel.Error);
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

        #region 测试按钮

        /// <summary>
        /// 测试发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {

            if (TxClient == null)
            {
                Logger.Log($"异常{TxClient}为null,自动重连中", LogLevel.Error);
                reconnectionStart();
                return;
            }
            string cmd = Txt_CMD.Text.Trim();
            string code = Txt_CODE.Text.Trim();
            string user = Txt_USER.Text.Trim();
            string barcode = Txt_BARCODE.Text.Trim();
            string count = Txt_COUNT.Text.Trim();

            string id = txt_Id.Text.Trim();

            var sendData = ConversionData(cmd, code, user, barcode, count, id);

            Logger.Log($"发送的数据{sendData.Data.DataSplitting}", LogLevel.Info);

            //int count1 = send.Length;

            //string strs = Encoding.ASCII.GetString(send, count1);

            //Logger.Log($"转换的数据源{strs}", LogLevel.Info);
            TxClient.sendMessage(sendData.Data.DataSplitting);
        }

        private void BtnTest1_Click(object sender, RoutedEventArgs e)
        {
            string code = "123;567;910";
            int length = code.Length;

            int index = code.LastIndexOf(";");



            /*截取第一个字符","前面所有的字符 */
            string codeData = code.Substring(index + 1);

            Logger.Log($"{codeData}", LogLevel.Info);
        }

        private void BtnTest_Click2(object sender, RoutedEventArgs e)
        {
            //string cmd = Txt_CMD.Text.Trim();
            //string code = Txt_CODE.Text.Trim();
            //string user = Txt_USER.Text.Trim();
            //string barcode = Txt_BARCODE.Text.Trim();
            //string count = Txt_COUNT.Text.Trim();
            //var sendData = ConversionData(cmd, code, user, barcode, count);
            //byte[] buffer = Encoding.ASCII.GetBytes(sendData.Data.DataSplitting);
            //List<byte> list = new List<byte>();
            //list.Add(0);
            //list.AddRange(buffer);
            //byte[] newbuffer = list.ToArray();

            string newbuffer = "TxClient2";
            TxClient2.sendMessage(newbuffer);
        }

        #endregion


        public enum PrintCommintFlag
        {
            未发送 = 0,
            已发送 = 1,
            发送失败 = 2,
            需要重新发送 = 3,
            发送并回复 = 4,
            已打印 = 10,
        }
    }
}
