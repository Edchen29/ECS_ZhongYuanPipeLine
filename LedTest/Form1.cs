using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedHelper
{
    public partial class Form1 : Form
    {
        LEDhelp.LEDHelper leftLED;
        LEDhelp.LEDHelper rightLED;
        public Form1()
        {
            InitializeComponent();
        }

        private void initLED()
        {
            string leftLedIP = ConfigurationManager.AppSettings["LeftLedIP"];
            string rightLedIP = ConfigurationManager.AppSettings["RightLedIP"];
            int ledPort = Convert.ToInt32(ConfigurationManager.AppSettings["ledPort"]);
            leftLED = new LEDhelp.LEDHelper(leftLedIP, (uint)ledPort, 1);
            rightLED = new LEDhelp.LEDHelper(rightLedIP, (uint)ledPort, 1);
        }

        private void sendLedInfo(int direction, int color, String sendText)
        {
            if (color == LEDhelp.LEDHelper.GREEN)
            {
                sendText = "\\C2" + sendText;
            }
            else if (color == LEDhelp.LEDHelper.YELLOW)
            {
                sendText = "\\C3" + sendText;
            }
            else
            {
                sendText = "\\C1" + sendText;
            }
            if (direction == LEDhelp.LEDHelper.RIGHT)
            {
                rightLED.SendLedInfo(sendText);
            }
            else
            {
                leftLED.SendLedInfo(sendText);
            }

        }


        private void SCREEN_ForceOnOff(int direction, byte status)
        {
            if (direction == LEDhelp.LEDHelper.RIGHT)
            {
                rightLED.SCREEN_ForceOnOff(status);
            }
            else
            {
                leftLED.SCREEN_ForceOnOff(status);
            }
        }

        // value 0-15 值越大亮度越大
        public void setBrightness(int direction, byte value)
        {
            if (direction == LEDhelp.LEDHelper.RIGHT)
            {
                rightLED.setBrightness(1, value, null);
            }
            else
            {
                leftLED.setBrightness(1, value, null);
            }
        }

        public void SCREEN_DelDynamicArea(int direction, byte DeleteAreaId)
        {
            if (direction == LEDhelp.LEDHelper.RIGHT)
            {
                rightLED.SCREEN_DelDynamicArea(DeleteAreaId);
            }
            else
            {
                leftLED.SCREEN_DelDynamicArea(DeleteAreaId);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string sendText = string.Format("任务类型:整盘入库\\n托盘条码:P00010\\n仓位:L-001-002-002-000\\n物料:2001020401,10\\n物料:2001020402,10\\n物料:2001020403,100\\n物料:2001020404,100");
            SCREEN_ForceOnOff(LEDhelp.LEDHelper.LEFT, LEDhelp.LEDHelper.OPEN);
            SCREEN_DelDynamicArea(LEDhelp.LEDHelper.LEFT, 0);
            sendLedInfo(LEDhelp.LEDHelper.LEFT, LEDhelp.LEDHelper.GREEN, sendText);
            setBrightness(LEDhelp.LEDHelper.LEFT, 10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sendText = string.Format("任务类型:整盘入库\\n托盘条码:P00010\\n仓位:L-001-002-002-000\\n物料:2001020401,10\\n物料:2001020402,10\\n物料:2001020403,100\\n物料:2001020404,100");
            SCREEN_ForceOnOff(LEDhelp.LEDHelper.RIGHT, LEDhelp.LEDHelper.OPEN);
            SCREEN_DelDynamicArea(LEDhelp.LEDHelper.RIGHT, 0);
            sendLedInfo(LEDhelp.LEDHelper.RIGHT, LEDhelp.LEDHelper.RED, sendText);
            setBrightness(LEDhelp.LEDHelper.RIGHT, 5);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
