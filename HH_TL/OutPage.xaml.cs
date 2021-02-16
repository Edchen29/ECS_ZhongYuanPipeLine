using Dapper;
using HH_TL.Mod;
using HH_TL.Server;
using HHECS.Bll;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums.PipeLine;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace HH_TL
{
    /// <summary>
    /// OutPage.xaml 的交互逻辑
    /// </summary>
    public partial class OutPage : Window
    {
        private CSVFile cSVFile = new CSVFile();
        private ObservableCollection<OutPutValue> newlist;
        public OutPage(List<OutPutValue> list)
        {
            InitializeComponent();
            newlist = new ObservableCollection<OutPutValue>(list);
            this.grid1.ItemsSource = newlist;
        }

        public void savefile(List<OutPutValue> outlist)
        {
            if (outlist == null || outlist.Count == 0)
            {
                MessageBox.Show("切割尺寸为空，无法导出");
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("等级", Type.GetType("System.String"));
            dt.Columns.Add("材质", Type.GetType("System.String"));
            dt.Columns.Add("管径", Type.GetType("System.String"));
            dt.Columns.Add("壁厚", Type.GetType("System.String"));
            dt.Columns.Add("原材料长", Type.GetType("System.Int32"));
            var count = outlist.Select(a => a.productList.Count).ToList().Max();
            for (int index = 1; index <= 15; index++)
            {
                dt.Columns.Add("段长" + index, Type.GetType("System.Int32"));
            }
            dt.Columns.Add("END", Type.GetType("System.String"));

            foreach (OutPutValue outPut in outlist)
            {
                DataRow newRow = dt.NewRow();
                newRow["等级"] = outPut.material.Dengji;
                newRow["材质"] = outPut.material.CaiZhi;
                newRow["管径"] = outPut.material.WaiJing;
                newRow["壁厚"] = outPut.material.BiHou;
                newRow["原材料长"] = outPut.material.length;
                for (int j = 0; j < outPut.productList.Count; j++)
                {
                    int index = j + 1;
                    newRow["段长" + index] = outPut.productList[j].length;
                }
                dt.Rows.Add(newRow);
            }

            string defaultName = "切割计划PLC数据 " + DateTime.Now.ToString("yyyy-MM-dd HH-mm");

            SaveFileDialog sfd = new SaveFileDialog(); //设置文件类型
            sfd.Filter = "导出数据（*.csv）|*.csv";
            sfd.FileName = defaultName;
            //设置默认文件类型显示顺序
            //sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录
            sfd.RestoreDirectory = true;

            //点了保存按钮进入
            if (sfd.ShowDialog() == true)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径+文件名
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                                                                                                   //获取文件路径，不带文件名
                string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                //保存
                cSVFile.ExportToSvc(dt, FilePath, fileNameExt);

            }
        }

        public void savefile_User(List<OutPutValue> outlist)
        {
            if (outlist == null || outlist.Count == 0)
            {
                MessageBox.Show("切割尺寸为空，无法导出");
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("原材料外径", Type.GetType("System.String"));
            dt.Columns.Add("原材料壁厚", Type.GetType("System.String"));
            dt.Columns.Add("原材料材质", Type.GetType("System.String"));
            dt.Columns.Add("原材料等级", Type.GetType("System.String"));
            dt.Columns.Add("原材料长度", Type.GetType("System.Int32"));
            dt.Columns.Add("原材料炉号", Type.GetType("System.String"));
            dt.Columns.Add("原材料物料号", Type.GetType("System.String"));
            dt.Columns.Add("->", Type.GetType("System.String"));
            dt.Columns.Add("下料材质", Type.GetType("System.String"));
            dt.Columns.Add("下料等级", Type.GetType("System.String"));
            dt.Columns.Add("下料外径", Type.GetType("System.String"));
            dt.Columns.Add("下料壁厚", Type.GetType("System.String"));
            dt.Columns.Add("管件编号", Type.GetType("System.String"));
            dt.Columns.Add("项目名称", Type.GetType("System.String"));
            dt.Columns.Add("下料编号", Type.GetType("System.String"));
            dt.Columns.Add("图纸页码", Type.GetType("System.String"));
            dt.Columns.Add("切断长", Type.GetType("System.String"));
            dt.Columns.Add("首端坡口形式", Type.GetType("System.String"));
            dt.Columns.Add("末端坡口形式", Type.GetType("System.String"));
            dt.Columns.Add("首端内镗T值", Type.GetType("System.String"));
            dt.Columns.Add("末端内镗T值", Type.GetType("System.String"));
            dt.Columns.Add("物料编码", Type.GetType("System.String"));
            dt.Columns.Add("流向", Type.GetType("System.String"));

            for (int index = 0; index < outlist.Count; index++)
            {
                for (int j = 0; j < outlist[index].productList.Count; j++)
                {
                    DataRow newRow = dt.NewRow();
                    var product = outlist[index].productList[j];
                    if (j == 0)
                    {
                        newRow["原材料外径"] = outlist[index].material.WaiJing;
                        newRow["原材料壁厚"] = outlist[index].material.BiHou;
                        newRow["原材料材质"] = outlist[index].material.CaiZhi;
                        newRow["原材料等级"] = outlist[index].material.Dengji;
                        newRow["原材料长度"] = outlist[index].material.length;
                        newRow["原材料炉号"] = outlist[index].material.LuPiHao;
                        newRow["原材料物料号"] = outlist[index].material.WuLiaoHao;
                    }
                    newRow["下料材质"] = product.CaiZhi;
                    newRow["下料等级"] = product.Dengji;
                    newRow["下料外径"] = product.WaiJing;
                    newRow["下料壁厚"] = product.BiHou;
                    newRow["管件编号"] = product.GuanJianBianHao;
                    newRow["项目名称"] = product.XiangMuMingCheng;
                    newRow["下料编号"] = product.XiaLiaoBianHao;
                    newRow["图纸页码"] = product.TuZhi;
                    newRow["切断长"] = product.length;
                    newRow["首端坡口形式"] = product.ShouDuanPoKou;
                    newRow["末端坡口形式"] = product.MoDuanPoKou;
                    newRow["首端内镗T值"] = product.ShouDuanNeiTang;
                    newRow["末端内镗T值"] = product.MoDuanNeiTang;
                    newRow["物料编码"] = product.WuLiaoBianMa;
                    newRow["流向"] = product.LiuXiang;
                    dt.Rows.Add(newRow);
                }

                DataRow nullRow = dt.NewRow();
                dt.Rows.Add(nullRow);
            }

            string defaultName = "切割计划用户数据 " + DateTime.Now.ToString("yyyy-MM-dd HH-mm");

            SaveFileDialog sfd = new SaveFileDialog(); //设置文件类型
            sfd.Filter = "导出数据（*.csv）|*.csv";
            sfd.FileName = defaultName;
            //设置默认文件类型显示顺序
            //sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录
            sfd.RestoreDirectory = true;

            //点了保存按钮进入
            if (sfd.ShowDialog() == true)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径+文件名
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                                                                                                   //获取文件路径，不带文件名
                string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                //保存
                cSVFile.ExportToSvc(dt, FilePath, fileNameExt);

            }
        }

        public void send_DataBase(List<OutPutValue> outlist)
        {
            if (outlist == null || outlist.Count == 0)
            {
                MessageBox.Show("切割尺寸为空，无法导出");
                return;
            }

            //套料结果
            List<CutPlan> cutPlanList = new List<CutPlan>();
            //工单表更新语句
            List<string> pipeOrderUpdate = new List<string>();
            //物料缓存表更新语句
            List<string> stationCacheUpdate = new List<string>();
            for (int index = 0; index < outlist.Count; index++)
            {
                if (!int.TryParse(outlist[index].material.WuLiaoHao, out int stationCacheId))
                {
                    MessageBox.Show("物料号转化数字失败，不能存入数据库");
                    return;
                }
                if (!decimal.TryParse(outlist[index].material.WaiJing, out decimal diameter))
                {
                    MessageBox.Show("外径转化数字失败，不能存入数据库");
                    return;
                }
                if (!decimal.TryParse(outlist[index].material.BiHou, out decimal thickness))
                {
                    MessageBox.Show("壁厚转化数字失败，不能存入数据库");
                    return;
                }
                if (!int.TryParse(outlist[index].material.CaiZhi, out int wcsProductType))
                {
                    MessageBox.Show("材质编号转化数字失败，不能存入数据库");
                    return;
                }
                //if (!int.TryParse(outlist[index].material.ZuDui, out int AssemblyStation))//cf
                //{
                //    MessageBox.Show("组队工位转化数字失败，不能存入数据库");
                //    return;
                //}
                for (int j = 0; j < outlist[index].productList.Count; j++)
                {
                    if (!int.TryParse(outlist[index].productList[j].GuanJianBianHao, out int WONumber))
                    {
                        MessageBox.Show("管件编号转化数字失败，不能存入数据库");
                        return;
                    }

                    pipeOrderUpdate.Add($" UPDATE pipe_order SET status = {StationCacheStatus.已经套料.GetIndexString()} WHERE id = {WONumber} ");

                    CutPlan cutPlan = new CutPlan();
                    //cutPlan.WONumber = WONumber.ToString();
                    
                    cutPlan.stationCacheId = stationCacheId;
                    cutPlan.WONumber = outlist[index].productList[j].Code;//cf
                    cutPlan.AssemblyStation = outlist[index].productList[j].AssemblyStation;//cf
                    cutPlan.WcsProductType = wcsProductType;
                    cutPlan.MaterialLength = outlist[index].material.length;
                    cutPlan.Diameter = diameter;
                    cutPlan.Thickness = thickness;
                    cutPlan.Length = outlist[index].productList[j].length;
                    cutPlan.Status = CutPlanStatus.初始.GetIndexInt();
                    cutPlanList.Add(cutPlan);                    
                }
                stationCacheUpdate.Add($"UPDATE station_cache SET status = { StationCacheStatus.已经套料.GetIndexString()} WHERE id = { stationCacheId }");
            }

            string where = " where WONumber IN ('" + string.Join(" ','", cutPlanList.Select(t => t.WONumber).ToList()) + "')";
            var countResult = AppSession.Dal.GetCommonModelCount<CutPlan>(where);
            if (!countResult.Success)
            {
                MessageBox.Show($"发送套料结果到数据库的时候，查询数据库发生错误，原因：{countResult.Msg}");
                return;
            }
            if (countResult.Data > 0)
            {
                MessageBox.Show($"套料结果在数据库中已存在，不要反复保存！");
                return;
            }
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    foreach (var cutPlan in cutPlanList)
                    {
                        connection.Insert<CutPlan>(cutPlan, transaction: tran);
                    }
                    foreach (var item in pipeOrderUpdate)
                    {
                        connection.Execute(item, transaction: tran);
                    }
                    foreach (var item in stationCacheUpdate)
                    {
                        connection.Execute(item, transaction: tran);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    MessageBox.Show($"发送套料结果到数据库的时候，发生异常，原因：{ex.Message}");
                    return;
                }
            }
            MessageBox.Show("套料结果发送到数据库成功！");
        }

        private void Button_OutPut_User(object sender, RoutedEventArgs e)
        {

            var list = newlist.ToList();
            var vLst = list.FindAll(a => a.ischeck);
            if (vLst.Count == 0)
            {
                MessageBox.Show("未选择任何需要导出的行");
                return;
            }
            //if (vLst.Count > 8)
            //{
            //    MessageBox.Show("切割机最大支持导出8行数据，请勿选择大于8条原材料");
            //    return;
            //}
            savefile_User(vLst);
        }

        private void Button_OutPut_Plc(object sender, RoutedEventArgs e)
        {

            var list = newlist.ToList();
            var vLst = list.FindAll(a => a.ischeck);
            if (vLst.Count == 0)
            {
                MessageBox.Show("未选择任何需要导出的行");
                return;
            }
            if (vLst.Count > 8)
            {
                MessageBox.Show("切割机最大支持导出8行数据，请勿选择大于8条原材料");
                return;
            }
            savefile(vLst);
        }
  
        private void Button_Send_DB(object sender, RoutedEventArgs e)
        {
            var list = newlist.ToList();
            var vLst = list.FindAll(a => a.ischeck);
            if (vLst.Count == 0)
            {
                MessageBox.Show("未选择任何需要导出的行");
                return;
            }
            if (vLst.Count > 11)
            {
                MessageBox.Show("切割机最大支持导出11行数据，请勿选择大于11条原材料");
                return;
            }
            //todo://没有实现
            send_DataBase(vLst);
        }

        private void Button_Send_Plc(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    DataTable dataTable = GetDataTabelByOutPutValue();
            //    if (dataTable == null)
            //    {
            //         MessageBox.Show("没有选择数据");
            //    }
            //    string NetId = ConfigurationManager.AppSettings["plcID"]; //PLC 控制地址 5.18.191.254.1.1
            //    TwinCAT.Ads.TcAdsClient adsClient = null;
            //    adsClient = new TwinCAT.Ads.TcAdsClient();
            //    adsClient.Connect(NetId, 801);
            //    int readStructIndex = adsClient.CreateVariableHandle("MAIN.LMDataFromPC_BG1");
            //    LMDataFromPCArray lMDataFrom = (LMDataFromPCArray)adsClient.ReadAny(readStructIndex, typeof(LMDataFromPCArray));

            //    // 预写入数据
            //    for (int index = 0; index < 8; index++)
            //    {
            //        if (index < dataTable.Rows.Count)
            //        {
            //            lMDataFrom.fromPCs[index].GWNo = short.Parse(dataTable.Rows[index][0].ToString());
            //            lMDataFrom.fromPCs[index].MT = 1; //材质处理
            //            lMDataFrom.fromPCs[index].OD = short.Parse(dataTable.Rows[index][2].ToString());
            //            lMDataFrom.fromPCs[index].WT = short.Parse(dataTable.Rows[index][3].ToString()); 
            //            lMDataFrom.fromPCs[index].YLLth = short.Parse(dataTable.Rows[index][4].ToString()); 
            //            lMDataFrom.fromPCs[index].CutLth[0] = short.Parse(dataTable.Rows[index][5].ToString()); 
            //            lMDataFrom.fromPCs[index].CutLth[1] = short.Parse(dataTable.Rows[index][6].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[2] = short.Parse(dataTable.Rows[index][7].ToString()); 
            //            lMDataFrom.fromPCs[index].CutLth[3] = short.Parse(dataTable.Rows[index][8].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[4] = short.Parse(dataTable.Rows[index][9].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[5] = short.Parse(dataTable.Rows[index][10].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[6] = short.Parse(dataTable.Rows[index][11].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[7] = short.Parse(dataTable.Rows[index][12].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[8] = short.Parse(dataTable.Rows[index][13].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[9] = short.Parse(dataTable.Rows[index][14].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[10] = short.Parse(dataTable.Rows[index][15].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[11] = short.Parse(dataTable.Rows[index][16].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[12] = short.Parse(dataTable.Rows[index][17].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[13] = short.Parse(dataTable.Rows[index][18].ToString());
            //            lMDataFrom.fromPCs[index].CutLth[14] = short.Parse(dataTable.Rows[index][19].ToString());
            //            lMDataFrom.fromPCs[index].Busy = 1;
            //        }
            //        else
            //        {
            //            lMDataFrom.fromPCs[index].GWNo = 0;
            //            lMDataFrom.fromPCs[index].MT = 0;
            //            lMDataFrom.fromPCs[index].OD = 0;
            //            lMDataFrom.fromPCs[index].WT = 0;
            //            lMDataFrom.fromPCs[index].YLLth = 0;
            //            lMDataFrom.fromPCs[index].CutLth[0] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[1] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[2] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[3] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[4] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[5] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[6] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[7] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[8] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[9] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[10] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[11] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[12] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[13] = 0;
            //            lMDataFrom.fromPCs[index].CutLth[14] = 0;
            //            lMDataFrom.fromPCs[index].Busy = 1;
            //        }
            //    }
            //    adsClient.WriteAny(readStructIndex, lMDataFrom);
            //    // 数据可以读取操作了
            //    System.Threading.Thread.Sleep(500);
            //    for (int i = 0; i < 8; i++)
            //    {
            //        lMDataFrom.fromPCs[i].Busy = 0;
            //    }
            //    //Array.Copy(choseList.CutLth, complexStruct.CutLth, complexStruct.CutLth.Length);
            //    //结构体写入时，同样通过readStructIndex索引，直接写入结构体。
            //    adsClient.WriteAny(readStructIndex, lMDataFrom);
            //    MessageBox.Show("PLC数据写入成功");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"WriteAds error:" + ex.Message);
            //}

        }

        public Int16 getCaiZhiByXingHao(string strXingHao)
        {
            Int16 result = 0;
            switch (strXingHao)
            {
                case "A828-S31803":
                    {
                        result = 1;
                        break;
                    }


                default:
                    {
                        result = 0;
                        break;
                    }
            }
            return result;
        }
    }
}
