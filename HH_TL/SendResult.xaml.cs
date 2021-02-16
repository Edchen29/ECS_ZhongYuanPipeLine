using HH_TL.Mod;
using HH_TL.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace HH_TL
{
    /// <summary>
    /// SendResult.xaml 的交互逻辑
    /// </summary>
    public partial class SendResult : Window
    {
        List<OutPutValue> outPutValues = new List<OutPutValue>();
        ObservableCollection<ResultGrid> resultlist = new ObservableCollection<ResultGrid>();
        private ObservableCollection<OutPutValue> newlist;
        private CSVFile cSVFile = new CSVFile();
        /// <summary>
        /// 缓存的管子数量
        /// </summary>
        public Int16 papeQty = Int16.Parse(ConfigurationManager.AppSettings["papeQty"]);
        /// <summary>
        /// 每个管子切的段数
        /// </summary>
        public Int16 cutNum = Int16.Parse(ConfigurationManager.AppSettings["cutNum"]);
        //初始化表格
        public SendResult(List<OutPutValue> list,string plcID)
        {
            InitializeComponent();
            newlist = new ObservableCollection<OutPutValue>(list);
            this.grid2.ItemsSource = newlist;
            this.NetId = plcID;
        }

        #region 导入文件
        private void Button_Click_Me(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 8; i++)
            {
                resultlist.Add(new ResultGrid() { id = i });
            }
            
            foreach (var value in resultlist)
            {
                value.clear();
            }

            grid2.ItemsSource = null;

            var dateresult = cSVFile.OpenCSVFile();
            if (dateresult == null)
            {
                MessageBox.Show("未能成功打开CSV文件");
                return;
            }
            if (dateresult.Columns.Count != 21)
            {
                MessageBox.Show("CSV文件列数不正确，无法导入");
                return;
            }
            for (int i = 0; i < dateresult.Rows.Count; i++)
            {
                int[] length = new int[15];
                length[i] = 0;

                string GWNo = dateresult.Rows[i][0].ToString().Replace(" ", "");
                string MT = dateresult.Rows[i][1].ToString().Replace(" ", "");
                string OD = dateresult.Rows[i][2].ToString().Replace(" ", "");
                string WT = dateresult.Rows[i][3].ToString().Replace(" ", "");
                string YLLth = dateresult.Rows[i][4].ToString().Replace(" ", "");
                string length1 = dateresult.Rows[i][5].ToString().Replace(" ", "");
                //string length2 = dateresult.Rows[i][6].ToString().Replace(" ", "");
                //string length3 = dateresult.Rows[i][7].ToString().Replace(" ", "");
                //string length4 = dateresult.Rows[i][8].ToString().Replace(" ", "");
                //string length5 = dateresult.Rows[i][9].ToString().Replace(" ", "");
                //string length6 = dateresult.Rows[i][10].ToString().Replace(" ", "");
                //string length7 = dateresult.Rows[i][11].ToString().Replace(" ", "");
                //string length8 = dateresult.Rows[i][12].ToString().Replace(" ", "");
                //string length9 = dateresult.Rows[i][13].ToString().Replace(" ", "");
                //string length10 = dateresult.Rows[i][14].ToString().Replace(" ", "");
                //string length11 = dateresult.Rows[i][15].ToString().Replace(" ", "");
                //string length12 = dateresult.Rows[i][16].ToString().Replace(" ", "");
                //string length13 = dateresult.Rows[i][17].ToString().Replace(" ", "");
                //string length14 = dateresult.Rows[i][18].ToString().Replace(" ", "");
                //string length15 = dateresult.Rows[i][19].ToString().Replace(" ", "");
                string IsBusy = dateresult.Rows[i][20].ToString().Replace(" ", "");

                if (string.IsNullOrWhiteSpace(GWNo))
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(MT))
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(OD))
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(WT))
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(YLLth))
                {
                    continue;
                }
                for (int j = 0; j < 15; j++)
                {
                    if (!int.TryParse(length1, out length[j]))
                    {
                        continue;
                    }
                }

                resultlist[i].GWNo = GWNo;
                resultlist[i].MT = MT;
                resultlist[i].OD = OD;
                resultlist[i].WT = WT;
                resultlist[i].YLLth = YLLth;
                resultlist[i].length1 = length[0];
                resultlist[i].length2 = length[1];
                resultlist[i].length3 = length[2];
                resultlist[i].length4 = length[3];
                resultlist[i].length5 = length[4];
                resultlist[i].length6 = length[5];
                resultlist[i].length7 = length[6];
                resultlist[i].length8 = length[7];
                resultlist[i].length9 = length[8];
                resultlist[i].length10 = length[9];
                resultlist[i].length11 = length[10];
                resultlist[i].length12 = length[11];
                resultlist[i].length13 = length[12];
                resultlist[i].length14 = length[13];
                resultlist[i].length15 = length[14];
                resultlist[i].Busy = IsBusy;
            }
            grid2.ItemsSource = resultlist;
            int realcount = resultlist.Count(a => !string.IsNullOrWhiteSpace(a.GWNo));
            if (realcount != dateresult.Rows.Count)
            {
                MessageBox.Show("未能全部导入文件内容，部分数据为空或格式不正确，实际导入" + realcount + "条数据");
            }
        }

        #endregion

        #region 发送数据去给PLC
       


        public  string NetId = "5.18.191.254.1.1"; //PLC 控制地址 5.18.191.254.1.1
        public TwinCAT.Ads.TcAdsClient adsClient = null;
        public int readStructIndex;

        // 连接PLC
        public bool AdsConnect()
        { 
            try
            {
                adsClient = new TwinCAT.Ads.TcAdsClient();
                adsClient.Connect(NetId, 801);
                return true;
            }
            catch (Exception ex)
            {
                string EInfo = string.Format("Ads Connect Error\n{0}", ex.Message);
                MessageBox.Show($"AdsConnect: " + EInfo);
                return false;
            }
        }

        //初始化变量，此例中【LMDataFromPC】为plc提供的变量地址，类型为结构体。读取写入类型为其他时同样处理。            
        public bool AdsInit()
        {
            try
            {
                readStructIndex = adsClient.CreateVariableHandle("MAIN.LMDataFromPC_BG1");
                //var val2 = (LMDataFromPCArray)adsClient.ReadAny(readStructIndex, typeof(LMDataFromPCArray));
                return true;
            }
            catch (Exception ex)
            {
                string Emessage = string.Format("Ads Add Struct Error\n{0}", ex.Message);
                MessageBox.Show($"AdsInit:" + Emessage);
                return false;
            }
        }

        //从plc读取变量结构体，ReadStruct 为结构体，通过初始化时得到的 readStructIndex索引，进行读取
        public bool ReadAds()
        {
            try
            {
                //ReadStruct 为结构体，通过初始化时得到的 readStructIndex索引，进行读取
                var value = (LMDataFromPCArray)adsClient.ReadAny(readStructIndex, typeof(LMDataFromPCArray));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ReadAds error:" + ex.Message);
                return false;
            }
        }

        //写变量的值给PLC，结构体写入时，同样通过writeStructIndex索引，直接写入结构体。
        public bool WriteAds(List<ResultGrid> choseList)
        {
            if (choseList == null || choseList.Count == 0 )
            {
                MessageBox.Show("套料结果为空，无法发送给PLC");
                return false;
            }


            LMDataFromPCArray lMDataFrom = (LMDataFromPCArray)adsClient.ReadAny(readStructIndex, typeof(LMDataFromPCArray)) ;

            try
            {
               // 预写入数据
                for (int index = 0; index < 8; index++)
                {
                    if (index < choseList.Count)
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
                adsClient.WriteAny(readStructIndex, lMDataFrom);
                // 数据可以读取操作了
                System.Threading.Thread.Sleep(500);
                for (int i = 0; i< 8; i++)
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
                MessageBox.Show($"WriteAds error:" + ex.Message);
                return false;
            }
        }

        private void Button_OutPut_Plc(object sender, RoutedEventArgs e)
        {
            var list = resultlist.ToList();
            //var vLst = list.FindAll(a => a.ischeck);
            //if (vLst.Count == 0)
            //{
            //    MessageBox.Show("未选择任何需要导出的行");
            //    return;
            //}
            //if (vLst.Count > 8)
            //{
            //    MessageBox.Show("切割机最大支持导出8行数据，请勿选择大于8条原材料");
            //    return;
            //}
            //ReadStruct result = new ReadStruct();
            //result.CutLth[1] = list.length1;


            if (AdsConnect() && AdsInit() && ReadAds() && WriteAds(list))
            {
                MessageBox.Show("PLC 写入成功");
            }
            else
            {
                MessageBox.Show("PLC 连接失败");
            }
        }

        #endregion

        private void Button_Instruction(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("请按照如下步骤操作发送数据到PLC：" +
                "1.从本程序内部导出套料结果，再点击导入外部套料方案，写入结果，发送到PLC。  " +
                "2.直接导入外部套料方案，发送到PLC");
        }
    }
}
