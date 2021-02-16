using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Model.LEDHelper.LEDComponent.DefaultLEDComponent
{
  public  class LedInfo
    {
        public  Led5kSDK.bx_5k_area_header bx_5k;
        public  byte[] AreaText;
        public  int length;
        // led 显示信息格式：  192*96
        //  类型：整盘入库
        //  条码：
        //  仓位：
        //  M:         Q:
        //  M:         Q:
        //  M：        Q:

        public LedInfo (string sendText)
        {
            int flag = 0;
            bx_5k.AreaType = 0x06;
            bx_5k.AreaX = 0;
            bx_5k.AreaY = 0;
            bx_5k.AreaWidth = 192; //根据屏幕设定 
            bx_5k.AreaWidth /= 8;

            bx_5k.AreaHeight = 192;// 根据屏幕设定
           
            bx_5k.Lines_sizes = 0;

            bx_5k.Reserved1 = 0;
            bx_5k.Reserved2 = 0;
            bx_5k.Reserved3 = 0;

            bx_5k.RunMode = 0;//0：自动循环显示   1：完成后停在最后一页   2：超时未完成删除该信息
            bx_5k.Timeout = 2;
            bx_5k.SingleLine = 0x02; //01:单行显示   02：多行显示
            bx_5k.NewLine = 0x02;  //01:手动换行  02:自动换行

            bx_5k.DisplayMode = 0x02;//01静止显示 02快速打出  03向左移动 04向右移动 05向上移动 06向下移动

            bx_5k.ExitMode = 0x00;
            bx_5k.Speed = 2; //运行速度
            bx_5k.StayTime = 10;//停留时间

         

            AreaText = System.Text.Encoding.Default.GetBytes(sendText);
            bx_5k.DataLen = AreaText.Length;

        }


    }
}
