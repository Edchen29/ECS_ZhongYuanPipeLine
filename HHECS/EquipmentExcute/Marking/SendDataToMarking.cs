using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HHECS.EquipmentExcute.Marking
{
    internal class SendDataToMarking
    {
        private static Socket socketSend;
        private static Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        public static string mark = "";
        private static int count = 0;
        private static string ipForStart = "";
        private static string barcodeForDel = "";
        static System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
        public static void Start()
        {
            try
            {
                //当点击开始监听的时候，在服务器端创建一个负责监听IP地址跟端口号的Socket
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Any;
                // IPAddress ip = IPAddress.Parse(txt1.Text);
                //创建端口号对象
                IPEndPoint point = new IPEndPoint(ip, 5050);
                socketWatch.Bind(point);
                //ShowMsg("监听成功");
                socketWatch.Listen(10);

                Thread th = new Thread(Listen);
                th.IsBackground = true;
                th.Start(socketWatch);//把负责监听的socketWatch传进去
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public static void Listen(object o)
        {
            try
            {
                Socket socketWatch = o as Socket;
                while (true)
                {
                    //等待客户端的连接，并创建一个负责通讯的socket
                    socketSend = socketWatch.Accept();//由负责监听的socket通过调用accept方法（accept方法指一直等待客户端的响应，没有回应就一直等待）
                                                      // ，去创建一个负责通讯的socket
                    dicSocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend); //将远程的客户端的ip地址和socket存入集合
                                                                                     //comboBox1.Items.Add(socketSend.RemoteEndPoint.ToString());//填充下拉框
                                                                                     //ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功");//172.16.35.127:连接成功
                                                                                     //开启一个新线程去接收客户端发来的消息
                    Thread th = new Thread(Recive);
                    th.IsBackground = true;
                    th.Start(socketSend);
                }
            }
            catch { }
        }

        public static void Recive(object o)
        {
            try
            {
                string str = "";
                Socket socketSend = o as Socket;
                while (true)
                {
                    //客户端接收消息
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    if (Encoding.ASCII.GetString(buffer, 0, r - 1) == "newmark")
                    {
                        str = Encoding.ASCII.GetString(buffer, 0, r);
                        mark = str;
                    }
                    else
                    {
                        str = Encoding.ASCII.GetString(buffer, 0, r);
                    }

                    //txt1.AppendText(str + "\r\n");
                    //ShowMsg(socketSend.RemoteEndPoint + ":" + str);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 发送要打印的条码
        /// </summary>
        /// <param name="barcode"></param>
        public static void SendData(string barcode)
        {
            try
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                string str1 = "new TxMark7PiContent_Text";
                byte[] buffer1 = asciiEncoding.GetBytes(str1);

                string str3 = "set Pos=54,62";
                byte[] buffer3 = asciiEncoding.GetBytes(str3);

                string str4 = "set content ="+barcode;
                byte[] buffer4 = asciiEncoding.GetBytes(str4);
                barcodeForDel = barcode;
                string str7 = "set Size=30,13";
                byte[] buffer7 = asciiEncoding.GetBytes(str7);

                string str8 = "setpen 0,500,95,20000,280,80";
                byte[] buffer8 = asciiEncoding.GetBytes(str8);

                List<byte> list1 = new List<byte>();
                List<byte> list3 = new List<byte>();
                List<byte> list4 = new List<byte>();
                List<byte> list7 = new List<byte>();
                List<byte> list8 = new List<byte>();

                list1.AddRange(buffer1);
                list3.AddRange(buffer3);
                list4.AddRange(buffer4);
                list7.AddRange(buffer7);
                list8.AddRange(buffer8);

                //将泛型集合转为数组
                byte[] newBuffer1 = list1.ToArray();
                byte[] newBuffer3 = list3.ToArray();
                byte[] newBuffer4 = list4.ToArray();
                byte[] newBuffer7 = list7.ToArray();
                byte[] newBuffer8 = list8.ToArray();

                string ip = socketSend.RemoteEndPoint.ToString();
                ipForStart = ip;
                dicSocket[ip].Send(newBuffer1);
                Thread.Sleep(1000);
                dicSocket[ip].Send(newBuffer3);
                Thread.Sleep(1000);
                dicSocket[ip].Send(newBuffer4);
                Thread.Sleep(1000);
                dicSocket[ip].Send(newBuffer7);
                Thread.Sleep(1000);
                dicSocket[ip].Send(newBuffer8);
                Thread.Sleep(1000);
                ++count;

                ////打印
                //string str6 = "start";
                //byte[] buffer6 = asciiEncoding.GetBytes(str6);
                //List<byte> list6 = new List<byte>();
                //list6.AddRange(buffer6);
                //byte[] newBuffer6 = list6.ToArray();
                //dicSocket[ip].Send(newBuffer6);
                //Thread.Sleep(1000*35);

                ////删除标志
                //string str5 = "del newmark" + count;
                //byte[] buffer5 = asciiEncoding.GetBytes(str5);
                //List<byte> list5 = new List<byte>();
                //list5.AddRange(buffer5);
                //byte[] newBuffer5 = list5.ToArray();
                //dicSocket[ip].Send(newBuffer5);
            }
            catch
            {
            }
        }
       
        /// <summary>
        /// 发送打印命令
        /// </summary>
        public static void Print()
        {
            string str6 = "start";
            byte[] buffer6 = asciiEncoding.GetBytes(str6);
            List<byte> list6 = new List<byte>();
            list6.AddRange(buffer6);
            byte[] newBuffer6 = list6.ToArray();
            dicSocket[ipForStart].Send(newBuffer6);
            int length = barcodeForDel.Length;       
            Thread.Sleep(2400 * length+2000);
        }

        /// <summary>
        /// 清除打印信息
        /// </summary>
        public static void DeleMark()
        {
            //删除标志
            string str5 = "del newmark" + count;
            byte[] buffer5 = asciiEncoding.GetBytes(str5);
            List<byte> list5 = new List<byte>();
            list5.AddRange(buffer5);
            byte[] newBuffer5 = list5.ToArray();
            dicSocket[ipForStart].Send(newBuffer5);       
        }
    }
}
