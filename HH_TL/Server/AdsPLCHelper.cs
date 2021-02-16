using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HH_TL.Server
{
    // 倍福plc 读写操作实现类
    /// <summary>
    /// 1/ 需要查看防火墙是否已经开启了相应端口或关闭
    /// 2、 路由映射是否已经配置
    /// 3、 对应的结构是否匹配
    /// 4、 变量是否要求是
    /// </summary>
    public class AdsPLCHelper
    {
        /// <summary>
        /// 定义实体
        /// </summary>
        public TwinCAT.Ads.TcAdsClient adsClient = null;
        /// <summary>
        /// 网络ID 用于倍福PLC对应唯一标示  
        /// </summary>
        private string NetID = "";
        public AdsPLCHelper(string netId)
        {
            this.NetID = netId;
        }

        



        // 连接PLC
        public bool AdsConnect()
        {
            try
            {
                adsClient = new TwinCAT.Ads.TcAdsClient();
                adsClient.Connect(NetID, 801);
                return true;
            }
            catch (Exception ex)
            {
                string EInfo = string.Format("Ads Connect Error\n{0}", ex.Message);
                return false;
            }
        }

        //初始化变量，此例中【LMDataFromPC】为plc提供的变量地址，类型为结构体。读取写入类型为其他时同样处理。            
        public int AdsInit()
        {
            try
            {
                return adsClient.CreateVariableHandle("MAIN.LMDataFromPC_BG1");
            }
            catch (Exception ex)
            {
                string Emessage = string.Format("Ads Add Struct Error\n{0}", ex.Message);
                return 0;
            }
        }

        //从plc读取变量结构体，ReadStruct 为结构体，通过初始化时得到的 readStructIndex索引，进行读取
        public bool ReadAds(int readStructIndex)
        {
            try
            {
                //ReadStruct 为结构体，通过初始化时得到的 readStructIndex索引，进行读取
                var value = (LMDataFromPCArray)adsClient.ReadAny(readStructIndex, typeof(LMDataFromPCArray));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //写变量的值给PLC，结构体写入时，同样通过writeStructIndex索引，直接写入结构体。
        public bool WriteAds(List<ResultGrid> choseList)
        {
            if (choseList == null || choseList.Count == 0)
            {
                MessageBox.Show("套料结果为空，无法发送给PLC");
                return false;
            }


            LMDataFromPCArray lMDataFrom = (LMDataFromPCArray)adsClient.ReadAny(readStructIndex, typeof(LMDataFromPCArray));

            try
            {
                // 预写入数据
                for (int index = 0; index < 8; index++)
                {
                    if (index <= choseList.Count)
                    {
                        Int16.TryParse(choseList[index].GWNo, out lMDataFrom.fromPCs[index].GWNo);
                        Int16.TryParse(choseList[index].MT, out lMDataFrom.fromPCs[index].MT);
                        Int16.TryParse(choseList[index].OD, out lMDataFrom.fromPCs[index].OD);
                        Int16.TryParse(choseList[index].WT, out lMDataFrom.fromPCs[index].WT);
                        Int16.TryParse(choseList[index].YLLth, out lMDataFrom.fromPCs[index].YLLth);
                        lMDataFrom.fromPCs[index].CutLth[0] = (short)choseList[index].length1;
                        lMDataFrom.fromPCs[index].CutLth[1] = (short)choseList[index].length2;
                        lMDataFrom.fromPCs[index].CutLth[2] = (short)choseList[index].length3;
                        lMDataFrom.fromPCs[index].CutLth[3] = (short)choseList[index].length4;
                        lMDataFrom.fromPCs[index].CutLth[4] = (short)choseList[index].length5;
                        lMDataFrom.fromPCs[index].CutLth[5] = (short)choseList[index].length6;
                        lMDataFrom.fromPCs[index].CutLth[6] = (short)choseList[index].length7;
                        lMDataFrom.fromPCs[index].CutLth[7] = (short)choseList[index].length8;
                        lMDataFrom.fromPCs[index].CutLth[8] = (short)choseList[index].length9;
                        lMDataFrom.fromPCs[index].CutLth[9] = (short)choseList[index].length10;
                        lMDataFrom.fromPCs[index].CutLth[10] = (short)choseList[index].length11;
                        lMDataFrom.fromPCs[index].CutLth[11] = (short)choseList[index].length12;
                        lMDataFrom.fromPCs[index].CutLth[12] = (short)choseList[index].length13;
                        lMDataFrom.fromPCs[index].CutLth[13] = (short)choseList[index].length14;
                        lMDataFrom.fromPCs[index].CutLth[14] = (short)choseList[index].length15;
                        lMDataFrom.fromPCs[index].Busy = 1;
                    }
                    else
                    {
                        lMDataFrom.fromPCs[index].GWNo = 0;
                        lMDataFrom.fromPCs[index].MT = 0;
                        lMDataFrom.fromPCs[index].OD = 0;
                        lMDataFrom.fromPCs[index].WT = 0;
                        lMDataFrom.fromPCs[index].YLLth = 0;
                        lMDataFrom.fromPCs[index].CutLth[0] = 0;
                        lMDataFrom.fromPCs[index].CutLth[1] = 0;
                        lMDataFrom.fromPCs[index].CutLth[2] = 0;
                        lMDataFrom.fromPCs[index].CutLth[3] = 0;
                        lMDataFrom.fromPCs[index].CutLth[4] = 0;
                        lMDataFrom.fromPCs[index].CutLth[5] = 0;
                        lMDataFrom.fromPCs[index].CutLth[6] = 0;
                        lMDataFrom.fromPCs[index].CutLth[7] = 0;
                        lMDataFrom.fromPCs[index].CutLth[8] = 0;
                        lMDataFrom.fromPCs[index].CutLth[9] = 0;
                        lMDataFrom.fromPCs[index].CutLth[10] = 0;
                        lMDataFrom.fromPCs[index].CutLth[11] = 0;
                        lMDataFrom.fromPCs[index].CutLth[12] = 0;
                        lMDataFrom.fromPCs[index].CutLth[13] = 0;
                        lMDataFrom.fromPCs[index].CutLth[14] = 0;
                        lMDataFrom.fromPCs[index].Busy = 1;
                    }
                }
                // 数据可以读取操作了
                System.Threading.Thread.Sleep(500);
                for (int i = 0; i < 8; i++)
                {
                    lMDataFrom.fromPCs[i].Busy = 0;
                }
                //Array.Copy(choseList.CutLth, complexStruct.CutLth, complexStruct.CutLth.Length);
                //结构体写入时，同样通过readStructIndex索引，直接写入结构体。
                adsClient.WriteAny(readStructIndex, lMDataFrom);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    #region 结构体的定义  根据实际情况可以修改
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
    #endregion

    
}
