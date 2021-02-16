using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HH_TL.Mod
{
    public struct LMDataFromPCArray
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public LMDataFromPC[] fromPCs;
    }

    public struct LMDataFromPC
    {
        public Int16 GWNo; //(* 工件编号：该项目共8个工位即8根原材料管子，编号1～8 *)
        public Int16 MT;  //材料类型：0=Free ，1=CS ，2=SS，3=Alloy，4=Duplex
        public Int16 OD;  //外径
        public Int16 WT;  //壁厚
        public Int16 YLLth;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public Int16[] CutLth;
        public Int16 Busy;   //数据传送中为Ture，传送结束并且成功时为False
    }
}
