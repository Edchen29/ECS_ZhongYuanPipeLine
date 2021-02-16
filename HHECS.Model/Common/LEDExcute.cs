//using LEDhelp;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HHECS.Model.Common
//{
//    public class LEDExcute
//    {
//        LEDHelper LeftLED;
//        LEDHelper RightLED;
//        public Queue<string> LeftInfoQueue { get; set; } = new Queue<string>();
//        public Queue<string> RightInfoQueue { get; set; } = new Queue<string>();

//        public LEDExcute()
//        {
//            string leftLedIP = ConfigurationManager.AppSettings["LeftLedIP"];
//            string rightLedIP = ConfigurationManager.AppSettings["RightLedIP"];
//            int ledPort = Convert.ToInt32(ConfigurationManager.AppSettings["ledPort"]);
//            LeftLED = new LEDhelp.LEDHelper(leftLedIP, (uint)ledPort, 1);
//            RightLED = new LEDhelp.LEDHelper(rightLedIP, (uint)ledPort, 1);
//        }

//        public void BeginSendInfo()
//        {
//            Task.Run(async () =>
//            {
//                while (true)
//                {
//                    await Task.Delay(2000);
//                    if (LeftLED != null && LeftInfoQueue != null && LeftInfoQueue.Count > 0)
//                    {
//                        SCREEN_ForceOnOff(LEDhelp.LEDHelper.LEFT, LEDhelp.LEDHelper.OPEN);
//                        SCREEN_DelDynamicArea(LEDhelp.LEDHelper.LEFT, 0);
//                        sendLedInfo(LEDhelp.LEDHelper.LEFT, LEDhelp.LEDHelper.GREEN, LeftInfoQueue.Dequeue());
//                        setBrightness(LEDhelp.LEDHelper.LEFT, 10);
//                        //int i = leftLED.SendLedInfo(LeftInfoQueue.Dequeue());
//                        //Console.WriteLine("LED发送返回值:" + i);
//                        //if (i != 0)
//                        //{
//                        //    leftLED = new LEDHelper(AppSession.le, AppCommon.LedPort, AppCommon.LedTimer);
//                        //}
//                    }
//                    if (RightLED != null && RightInfoQueue != null && RightInfoQueue.Count > 0)
//                    {
//                        SCREEN_ForceOnOff(LEDhelp.LEDHelper.RIGHT, LEDhelp.LEDHelper.OPEN);
//                        SCREEN_DelDynamicArea(LEDhelp.LEDHelper.RIGHT, 0);
//                        sendLedInfo(LEDhelp.LEDHelper.RIGHT, LEDhelp.LEDHelper.RED, RightInfoQueue.Dequeue());
//                        setBrightness(LEDhelp.LEDHelper.RIGHT, 10);
//                    }
//                }
//            });
//        }

//        private void sendLedInfo(int direction, int color, String sendText)
//        {
//            if (color == LEDhelp.LEDHelper.GREEN)
//            {
//                sendText = "\\C2" + sendText;
//            }
//            else if (color == LEDhelp.LEDHelper.YELLOW)
//            {
//                sendText = "\\C3" + sendText;
//            }
//            else
//            {
//                sendText = "\\C1" + sendText;
//            }
//            if (direction == LEDhelp.LEDHelper.RIGHT)
//            {
//                RightLED.SendLedInfo(sendText);
//            }
//            else
//            {
//                LeftLED.SendLedInfo(sendText);
//            }

//        }

//        private void SCREEN_ForceOnOff(int direction, byte status)
//        {
//            if (direction == LEDhelp.LEDHelper.RIGHT)
//            {
//                RightLED.SCREEN_ForceOnOff(status);
//            }
//            else
//            {
//                LeftLED.SCREEN_ForceOnOff(status);
//            }
//        }

//        // value 0-15 值越大亮度越大
//        public void setBrightness(int direction, byte value)
//        {
//            if (direction == LEDhelp.LEDHelper.RIGHT)
//            {
//                RightLED.setBrightness(1, value, null);
//            }
//            else
//            {
//                LeftLED.setBrightness(1, value, null);
//            }
//        }

//        public void SCREEN_DelDynamicArea(int direction, byte DeleteAreaId)
//        {
//            if (direction == LEDhelp.LEDHelper.RIGHT)
//            {
//                RightLED.SCREEN_DelDynamicArea(DeleteAreaId);
//            }
//            else
//            {
//                LeftLED.SCREEN_DelDynamicArea(DeleteAreaId);
//            }
//        }
//    }
//}
