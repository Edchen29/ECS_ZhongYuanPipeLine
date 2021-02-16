using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedIHelper
{
    public partial class LED : Form
    {
        LEDhelp.LEDHelper ledHelper;

        public LED()
        {
            InitializeComponent();
        }

        private void initLED()
        {
            string ip = textBox1.Text;
            int port = int.Parse(textBox2.Text);
            ledHelper = new LEDhelp.LEDHelper(ip, (uint)port, 1);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initLED();
            if(ledHelper == null)
            {
               MessageBox.Show("设置LED失败");
               return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            initLED();
            if(ledHelper == null)
            {
                MessageBox.Show("LED初始化失败");
                return;
            }
            string content = textBox3.Text;
            ledHelper.SendLedInfo(content);
            ledHelper.setBrightness(1, 10, null);    //0-15  数值越高越亮
        }
    }
}
