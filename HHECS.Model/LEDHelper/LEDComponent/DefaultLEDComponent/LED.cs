using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.LEDHelper.LEDComponent.DefaultLEDComponent
{
    //调用说明：该类是为BX_5MK2控制器设计的  展示立体仓库中该LED的IP地址为 192.168.10.110  port:5005
    //  LED显示调用的流程为： 第一步 初始化LEDHelper 
    //                         第二步：直接调用SendLEDInfo 即可
    public class LED
    {

        /// <summary>
        /// 记录IP
        /// </summary>
        public string LEDIP { get; set; }

        //led对应的IP地址
        private byte[] led_ip;
        //led对应的端口号
        private uint led_port;
        //句柄
        private uint hand;
        //超时时间
        private int timeout = 1;
        // 0:静态  1：动态
        private int mode = 1;

        public static int RIGHT = 0;
        public static int LEFT = 1;

        public static int RED = 0;
        public static int GREEN = 1;
        public static int YELLOW = 2;

        public static byte OPEN = 1;
        public static byte CLOSE = 2;

        public LED(string ledIp, uint port, int timesec)
        {
            LEDIP = ledIp;
            led_ip = Encoding.ASCII.GetBytes(ledIp);
            led_port = port;
            timeout = timesec;
            if (hand <= 0)
            {
                CreateClient();
            }
        }

        public int SendLedInfo(string sendText)
        {
            int result = 0;
            if (hand <= 0)
            {
                CreateClient();
            }
            LedInfo ledinfo = new LedInfo(sendText);
            result = Led5kSDK.SCREEN_SendDynamicArea(hand, ledinfo.bx_5k, (ushort)ledinfo.AreaText.Length, ledinfo.AreaText);
            return result;
        }

        public int SendLedInfoTest()
        {
            int result = -1;
            if (hand <= 0 && CreateClient() > 0)
            {
                string sendText = string.Format("任务类型:整盘入库\n托盘条码:P00010\n仓位:L-001-002-002-000\n物料:2001020401,10\n物料:2001020402,10\n物料:2001020403,100\n物料:2001020404,100");
                LedInfo ledinfo = new LedInfo(sendText);
                result = Led5kSDK.SCREEN_SendDynamicArea(hand, ledinfo.bx_5k, (ushort)ledinfo.AreaText.Length, ledinfo.AreaText);
            }

            return result;
        }


        public uint CreateClient()
        {
            Led5kSDK.InitSdk(2, 2);
            hand = Led5kSDK.CreateClient(led_ip, led_port, (Led5kSDK.bx_5k_card_type)0x53, timeout, mode, null);
            return hand;
        }

        public void CloseClient()
        {
            Led5kSDK.Destroy(hand);
        }

        public int CheckPing()
        {
            return Led5kSDK.CON_PING(hand);
        }

        public int setBrightness(byte BrigthnessTyp, byte CurrentBrigthn, byte[] BrigthnessValue)
        {
            int result = -1;
            Led5kSDK.SCREEN_SetBrightness(hand, BrigthnessTyp, CurrentBrigthn, BrigthnessValue);
            return result;
        }


        public int SCREEN_ForceOnOff(byte control)
        {
            int result = -1;
            result = Led5kSDK.SCREEN_ForceOnOff(hand, control);
            return result;
        }

        public int SCREEN_DelDynamicArea(byte DeleteAreaId)
        {
            int result = -1;
            result = Led5kSDK.SCREEN_DelDynamicArea(hand, DeleteAreaId);
            return result;
        }




        /// <summary>
        /// 获取控制器类型的对应16进制编码
        /// 任意
        ///BX_5K1
        ///BX_5K2
        ///BX_5MK1
        ///BX_5MK2
        ///BX_5K1Q_YY
        ///BX_6K1
        ///BX_6K2
        ///BX_6K3
        ///BX_6K1_YY
        ///BX_6K2_YY
        ///BX_6K3_YY
        /// </summary>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        private static byte getControllerTypeByName(string controllerType)
        {
            byte result = 0;
            switch (controllerType)
            {
                case "任意": { result = 0xFE; break; };
                case "BX_5K1": { result = 0x51; break; };
                case "BX_5K2": { result = 0x58; break; };
                case "BX_5MK1": { result = 0x54; break; };
                case "BX_5MK2": { result = 0x53; break; };
                case "BX_5K1Q_YY": { result = 0x5c; break; };
                case "BX_6K1": { result = 0x61; break; };
                case "BX_6K2": { result = 0x62; break; };
                case "BX_6K3": { result = 0x63; break; };
                case "BX_6K1_YY": { result = 0x64; break; };
                case "BX_6K2_YY": { result = 0x65; break; };
                case "BX_6K3_YY": { result = 0x66; break; };
                default: result = 0; break;
            }
            return result;
        }
    }
}
