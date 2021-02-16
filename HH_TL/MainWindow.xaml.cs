using HH_TL.Mod;
using HH_TL.Server;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums.PipeLine;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HH_TL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private ObservableCollection<MaterialGrid> inputlist = new ObservableCollection<MaterialGrid>();
        private ObservableCollection<ProductGrid> outputlist = new ObservableCollection<ProductGrid>();
        private ObservableCollection<ResultGrid> resutlist = new ObservableCollection<ResultGrid>();
        private List<ResultGrid> resultress = new List<ResultGrid>();
        private List<OutPutValue> outPutValues = new List<OutPutValue>();
        private List<MaterialGrid> materialress = new List<MaterialGrid>();
        private List<ProductGrid> productress = new List<ProductGrid>();

        private DispatcherTimer ShowTimer;
        private CSVFile cSVFile = new CSVFile();
        private Defult defult = new Defult();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 初始化设置损耗率

            App.loss = 0;
            string lossstring = ConfigurationManager.AppSettings["loss"];
            bool isint = int.TryParse(lossstring, out App.loss);
            if (!isint)
            {
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfa.AppSettings.Settings["loss"].Value = App.loss.ToString();
                cfa.Save();
            }

            FuncSettingClosed();

            #endregion 初始化设置损耗率

            #region 初始化设置单元格

            for (int i = 1; i <= 500; i++)
            {
                inputlist.Add(new MaterialGrid() { id = i });
            }
            for (int i = 1; i <= 500; i++)
            {
                outputlist.Add(new ProductGrid() { id = i });
            }
            inputgrid.ItemsSource = inputlist;
            outputgrid.ItemsSource = outputlist;

            #endregion 初始化设置单元格

            #region 设置右上角当前时间

            ShowTime();    //在这里窗体加载的时候不执行文本框赋值，窗体上不会及时的把时间显示出来，而是等待了片刻才显示了出来

            ShowTimer = new System.Windows.Threading.DispatcherTimer();

            ShowTimer.Tick += new EventHandler(ShowCurTimer);//起个Timer一直获取当前时间

            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);

            ShowTimer.Start();

            #endregion 设置右上角当前时间
        }

        #region 界面事件

        private void Materialgrid_CellEditEnding(object sender, DataGridBeginningEditEventArgs e)
        {
            //try
            //{
            //    DataGrid dg = sender as DataGrid;
            //    var cell = dg.CurrentCell;
            //    MaterialGrid item = cell.Item as MaterialGrid;
            //    if (item != null)
            //    {
            //        if (item.name == null)
            //        {
            //            item.name = "#" + item.num;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void Productgrid_CellEditEnding(object sender, DataGridBeginningEditEventArgs e)
        {
            //try
            //{
            //    DataGrid dg = sender as DataGrid;
            //    var cell = dg.CurrentCell;
            //    ProductGrid item = cell.Item as ProductGrid;
            //    if (item != null)
            //    {
            //        if (item.name == null)
            //        {
            //            item.name = "#" + item.num;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            foreach (var value in inputlist)
            {
                value.clear();
            }
            foreach (var value in outputlist)
            {
                value.clear();
            }
            outPutValues = new List<OutPutValue>();
            productress = new List<ProductGrid>();
            materialress = new List<MaterialGrid>();
            grid1.ItemsSource = null;
            inputgrid1.ItemsSource = null;
            outputgrid1.ItemsSource = null;
        }

        public void ShowCurTimer(object sender, EventArgs e)
        {
            ShowTime();
        }

        private void ShowTime()
        {
            this.tbDateText.Text = "当前时间：" + DateTime.Now.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 计算套料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Calculation(object sender, RoutedEventArgs e)
        {
            try
            {
                //导入数据库工单
                var importMaterialResult = importDataBaseMaterial();
                if (!importMaterialResult.Success)
                {
                    MessageBox.Show(importMaterialResult.Msg);
                }
                //导入数据库原材料
                var importOrderResult = importDataBaseOrder();
                if (!importOrderResult.Success)
                {
                    MessageBox.Show(importOrderResult.Msg);
                }

                List<MaterialGrid> materialGrids_default = defult.DeepCopyByReflection<List<MaterialGrid>>(inputlist.Where(a => a.length != 0).ToList());
                List<ProductGrid> productGrids_default = defult.DeepCopyByReflection(outputlist.Where(a => a.length != 0).ToList());
                //计算下料
                List<MaterialGrid> materialGrids = defult.DeepCopyByReflection(inputlist.Where(a => a.length != 0).ToList());
                List<ProductGrid> productGrids = defult.DeepCopyByReflection(outputlist.Where(a => a.length != 0).ToList());

                outPutValues = Calculation(materialGrids, productGrids, App.loss);

                materialress = MaterialSurplus(materialGrids_default, outPutValues);
                productress = ProductSurplus(productGrids_default, outPutValues.Select(a => a.productList).ToList());

                if (productress.Count != 0)
                {
                    MessageBox.Show("提示：当前计划存在未切割的下料尺寸！" +
                        "\r\n1:原材料可用量是否足够？" +
                        "\r\n2:未切割下料尺寸是否大于原材料尺寸？" +
                        "\r\n请在右侧列表查看相关数据");
                }

                //绑定前端切割计划界面
                grid1.ItemsSource = outPutValues;
                //绑定剩余原材料界面
                inputgrid1.ItemsSource = materialress;
                //绑定剩余未切割界面
                outputgrid1.ItemsSource = productress;
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生计算错误，请联系管理员。错误内容：" + ex.Message);
            }
        }

        #endregion 界面事件

        #region 计算切割方案

        /// <summary>
        /// 计算切割方案
        /// </summary>
        /// <param name="materialGrids"></param>
        /// <param name="productGrids"></param>
        /// <param name="loss"></param>
        /// <returns></returns>
        private List<OutPutValue> Calculation(List<MaterialGrid> materialGrids, List<ProductGrid> productGrids, int loss)
        {
            List<OutPutValue> outPutValues = new List<OutPutValue>();

            //按长度升序排列
            productGrids = productGrids.OrderBy(a => a.length).ToList();

            //先筛除相同尺寸的材料(不需要加入损耗)
            for (int i = productGrids.Count - 1; i >= 0; i--)
            {
                //加入匹配
                var haveindex = materialGrids.FirstOrDefault(a => a.length == productGrids[i].length
                    && a.WaiJing == productGrids[i].WaiJing
                    && a.BiHou == productGrids[i].BiHou
                    && a.CaiZhi == productGrids[i].CaiZhi
                    && a.Dengji == productGrids[i].Dengji);
                if (haveindex != null)
                {
                    OutPutValue outPut = new OutPutValue();
                    outPut.material = haveindex;
                    outPut.Percentage = 1;
                    outPut.SumSun = 0;
                    outPut.productList.Add(productGrids[i]);

                    outPutValues.Add(outPut);

                    //移除第一个匹配项
                    materialGrids.Remove(haveindex);
                    productGrids.Remove(productGrids[i]);
                }
            }

            //在计算利用率最大的（需要计入损耗）
            for (int i = productGrids.Count - 1; i >= 0; i--)
            {
                if (productGrids.Count - 1 < i)
                {
                    continue;
                }
                //获取需要计算的出料的所有组合
                var y_temp = productGrids;
                var y_temp_index = y_temp[i];
                y_temp.Remove(productGrids[i]);
                //加入匹配
                var composelist = GetAllCompose(y_temp, y_temp_index);

                //原材料去重
                //var x_distinct = materialGrids.Distinct().OrderBy(a => a).ToList();
                var x_distinct = materialGrids.GroupBy(c => c.length).Select(c => c.First());

                float temppercent = 0;
                OutPutValue tempoutput = null;

                //遍历所有出料的组合
                foreach (OutPutValue outPut_temp in composelist)
                {
                    int tempsum = outPut_temp.SumProduct();
                    //加入匹配
                    var tempmu = x_distinct.FirstOrDefault(a => a.length >= tempsum
                        && a.WaiJing == outPut_temp.productList[0].WaiJing
                        && a.BiHou == outPut_temp.productList[0].BiHou
                        && a.CaiZhi == outPut_temp.productList[0].CaiZhi
                        && a.Dengji == outPut_temp.productList[0].Dengji);
                    //如果原材料为0，则未找到对应匹配的原材料，不进行处理
                    if (tempmu != null)
                    {
                        //计算利用率
                        int sun1 = loss * (outPut_temp.productList.Count() - 1);
                        int sun2 = loss * outPut_temp.productList.Count();
                        if (tempsum + sun1 == tempmu.length)
                        {
                            //如果总和长度与原材料相同（不需要计入最后一次损耗，并且直接输出）

                            temppercent = 1;

                            tempoutput = outPut_temp;
                            tempoutput.material = tempmu;
                            tempoutput.Percentage = temppercent;
                            tempoutput.SumSun = sun1;
                            break;
                        }
                        else
                        {
                            //如果不相同，则需要计算利用率
                            tempsum = tempsum + sun2;
                            float temppercent_t = (float)tempsum / tempmu.length;
                            if (temppercent_t > temppercent && temppercent_t <= 1)
                            {
                                temppercent = temppercent_t;

                                tempoutput = outPut_temp;
                                tempoutput.material = tempmu;
                                tempoutput.Percentage = temppercent;
                                tempoutput.SumSun = sun2;
                            }
                        }
                    }
                }

                if (tempoutput != null)
                {
                    outPutValues.Add(tempoutput);
                    materialGrids.Remove(tempoutput.material);
                    foreach (var tempy in tempoutput.productList)
                    {
                        productGrids.Remove(tempy);
                    }
                }
            }
            return outPutValues;
        }

        /// <summary>
        /// 获取所有的组合方案
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private List<OutPutValue> GetAllCompose(List<ProductGrid> list, ProductGrid index)
        {
            List<OutPutValue> newlist = new List<OutPutValue>();
            //先增加它本身
            OutPutValue outPutValue_self = new OutPutValue();
            outPutValue_self.productList.Add(index);
            newlist.Add(outPutValue_self);

            for (int y = 0; y < list.Count; y++)
            {
                List<ProductGrid> tempvalue = new List<ProductGrid>();
                tempvalue.Add(index);
                for (int t = y; t < list.Count; t++)
                {
                    if (list[t].WaiJing == index.WaiJing && list[t].BiHou == index.BiHou
                        && list[t].CaiZhi == index.CaiZhi && list[t].Dengji == index.Dengji)
                    {
                        tempvalue.Add(list[t]);
                        OutPutValue outPutValue = new OutPutValue();
                        foreach (var tempproduct in tempvalue)
                        {
                            outPutValue.productList.Add(tempproduct);
                        }
                        newlist.Add(outPutValue);
                    }
                }
            }
            return newlist;
        }

        /// <summary>
        /// 计算剩余原材料
        /// </summary>
        /// <param name="materialGrids"></param>
        /// <param name="outPutValues"></param>
        /// <returns></returns>
        private List<MaterialGrid> MaterialSurplus(List<MaterialGrid> materialGrids, List<OutPutValue> outPutValues)
        {
            foreach (OutPutValue outPut in outPutValues)
            {
                var findmaterial = materialGrids.FirstOrDefault(a => a.id == outPut.material.id);
                if (findmaterial != null)
                {
                    if (findmaterial.length == outPut.SumProduct())
                    {
                        materialGrids.Remove(findmaterial);
                    }
                    else
                    {
                        findmaterial.length = findmaterial.length - outPut.SumProduct();
                    }
                }
            }
            return materialGrids;
        }

        /// <summary>
        /// 计算未切割的下料尺寸
        /// </summary>
        /// <param name="productGrids"></param>
        /// <param name="productGridsUses"></param>
        /// <returns></returns>
        private List<ProductGrid> ProductSurplus(List<ProductGrid> productGrids, List<List<ProductGrid>> productGridsUses)
        {
            foreach (List<ProductGrid> productGridUse in productGridsUses)
            {
                foreach (ProductGrid productGrid in productGridUse)
                {
                    var findproduct = productGrids.FirstOrDefault(a => a.id == productGrid.id);
                    if (findproduct != null)
                    {
                        if (findproduct.length == productGrid.length)
                        {
                            bool rebool = productGrids.Remove(findproduct);
                        }
                        else
                        {
                            findproduct.length = findproduct.length - productGrid.length;
                        }
                    }
                }
            }
            return productGrids;
        }

        #endregion 计算切割方案

        #region 菜单栏操作

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("华恒套料计算工具 v2.2" +
                        "\r\n2020年5月27日");
        }

        private void MenuItem_Click_Sun(object sender, RoutedEventArgs e)
        {
            SunPage sunPage = new SunPage();
            sunPage.ChangeTextEvent += new ChangeTextHandler(FuncSettingClosed);
            sunPage.ShowDialog();
        }

        private void FuncSettingClosed()
        {
            tbSHText.Text = "当前设置损耗率为：" + App.loss + "毫米";
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("请确认是否退出程序？\n退出后将无法存储当前计划!",
               "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        #endregion 菜单栏操作

        #region 保存切割计划的文件

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Out(object sender, RoutedEventArgs e)
        {
            if (outPutValues == null || outPutValues.Count == 0)
            {
                MessageBox.Show("切割尺寸为空，无法导出");
                return;
            }
            OutPage outPage = new OutPage(outPutValues);
            outPage.ShowDialog();
        }

        private void Button_Click_Out1(object sender, RoutedEventArgs e)
        {
            if (materialress == null || materialress.Count == 0)
            {
                MessageBox.Show("剩余原材料数据为空，无法导出");
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("外径", Type.GetType("System.String"));
            dt.Columns.Add("壁厚", Type.GetType("System.String"));
            dt.Columns.Add("材质", Type.GetType("System.String"));
            dt.Columns.Add("等级", Type.GetType("System.String"));
            dt.Columns.Add("长度", Type.GetType("System.Int32"));
            dt.Columns.Add("炉批号", Type.GetType("System.String"));
            dt.Columns.Add("物料号", Type.GetType("System.String"));

            foreach (var outPut in materialress)
            {
                DataRow newRow = dt.NewRow();
                newRow["外径"] = outPut.WaiJing;
                newRow["壁厚"] = outPut.BiHou;
                newRow["材质"] = outPut.CaiZhi;
                newRow["等级"] = outPut.Dengji;
                newRow["长度"] = outPut.length;
                newRow["炉批号"] = outPut.LuPiHao;
                newRow["物料号"] = outPut.WuLiaoHao;
                dt.Rows.Add(newRow);
            }

            string defaultName = "剩余原材料数据 " + DateTime.Now.ToString("yyyy-MM-dd HH-mm");

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

        private void Button_Click_Out2(object sender, RoutedEventArgs e)
        {
            if (productress == null || productress.Count == 0)
            {
                MessageBox.Show("未切割下料为空，无法导出");
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("下料材质", Type.GetType("System.String"));
            dt.Columns.Add("下料等级", Type.GetType("System.String"));
            dt.Columns.Add("下料外径", Type.GetType("System.String"));
            dt.Columns.Add("下料壁厚", Type.GetType("System.String"));
            dt.Columns.Add("管件编号", Type.GetType("System.String"));
            dt.Columns.Add("项目名称", Type.GetType("System.String"));
            dt.Columns.Add("下料编号", Type.GetType("System.String"));
            dt.Columns.Add("图纸页码", Type.GetType("System.String"));
            dt.Columns.Add("切断长", Type.GetType("System.Int32"));
            dt.Columns.Add("首端坡口形式", Type.GetType("System.String"));
            dt.Columns.Add("末端坡口形式", Type.GetType("System.String"));
            dt.Columns.Add("首端内镗T值", Type.GetType("System.String"));
            dt.Columns.Add("末端内镗T值", Type.GetType("System.String"));
            dt.Columns.Add("物料编码", Type.GetType("System.String"));
            dt.Columns.Add("流向", Type.GetType("System.String"));

            foreach (var product in productress)
            {
                DataRow newRow = dt.NewRow();
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

            string defaultName = "未切割的下料数据 " + DateTime.Now.ToString("yyyy-MM-dd HH-mm");

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

        #endregion 保存切割计划的文件

        #region 导入Excel文件、数据库

        private void Button_Click_Me(object sender, RoutedEventArgs e)
        {
            var result = importExcelMaterial();
            if (!result.Success)
            {
                MessageBox.Show(result.Msg);
            }
        }

        private void Button_Click_Pro(object sender, RoutedEventArgs e)
        {
            var result = importExcelOrder();
            if (!result.Success)
            {
                MessageBox.Show(result.Msg);
            }
        }

        private void Button_Click_GetDB(object sender, RoutedEventArgs e)
        {
            var result = importDataBaseMaterial();
            if (!result.Success)
            {
                MessageBox.Show(result.Msg);
            }
        }

        private void Button_Click_GetOrder(object sender, RoutedEventArgs e)
        {
            var result = importDataBaseOrder();
            if (!result.Success)
            {
                MessageBox.Show(result.Msg);
            }
        }

        /// <summary>
        /// 导入Excel原材料
        /// </summary>
        private BllResult importExcelMaterial()
        {
            try
            {
                foreach (var value in inputlist)
                {
                    value.clear();
                }

                inputgrid1.ItemsSource = null;

                var dateresult = cSVFile.OpenCSVFile();
                if (dateresult == null)
                {
                    return BllResultFactory.Error("未能成功打开CSV文件");
                }
                if (dateresult.Columns.Count != 7)
                {
                    return BllResultFactory.Error("CSV文件列数不正确，无法导入");
                }
                for (int i = 0; i < dateresult.Rows.Count; i++)
                {
                    int fl = 0;

                    string WaiJing = dateresult.Rows[i][0].ToString().Replace(" ", "");
                    string BiHou = dateresult.Rows[i][1].ToString().Replace(" ", "");
                    string CaiZhi = dateresult.Rows[i][2].ToString().Replace(" ", "");
                    string Dengji = dateresult.Rows[i][3].ToString().Replace(" ", "");
                    string length = dateresult.Rows[i][4].ToString().Replace(" ", "");
                    string LuPiHao = dateresult.Rows[i][5].ToString().Replace(" ", "");
                    string WuLiaoHao = dateresult.Rows[i][6].ToString().Replace(" ", "");

                    if (string.IsNullOrWhiteSpace(WaiJing))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(BiHou))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(CaiZhi))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(Dengji))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(LuPiHao))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(WuLiaoHao))
                    {
                        continue;
                    }
                    if (!int.TryParse(length, out fl))
                    {
                        continue;
                    }

                    inputlist[i].WaiJing = WaiJing;
                    inputlist[i].BiHou = BiHou;
                    inputlist[i].CaiZhi = CaiZhi;
                    inputlist[i].Dengji = Dengji;
                    inputlist[i].length = fl;
                    inputlist[i].LuPiHao = LuPiHao;
                    inputlist[i].WuLiaoHao = WuLiaoHao;
                }

                int realcount = inputlist.Count(a => !string.IsNullOrWhiteSpace(a.WaiJing));
                if (realcount != dateresult.Rows.Count)
                {
                    return BllResultFactory.Error("未能全部导入文件内容，部分数据为空或格式不正确，实际导入" + realcount + "条数据");
                }
                else
                {
                    return BllResultFactory.Sucess("导入Excel原材料成功！");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"导入Excel原材料发生异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 导入Excel工单
        /// </summary>
        private BllResult importExcelOrder()
        {
            try
            {
                var countResult = AppSession.Dal.GetCommonModelCount<PipeOrder>($" where status = { StationCacheStatus.初始.GetIndexString()}");
                if (!countResult.Success)
                {
                    return BllResultFactory.Error($"在数据库中查询未套料工单的时候，查询数据库发生错误，原因：{countResult.Msg}");
                }
                //if (countResult.Data > 0)
                //{
                //    return BllResultFactory.Error($"数据库中存在未套料工单，不要反复保存！");
                //}
                foreach (var value in outputlist)
                {
                    value.clear();
                }
                outputgrid1.ItemsSource = null;

                var dateresult = cSVFile.OpenCSVFile();
                if (dateresult == null)
                {
                    return BllResultFactory.Error("未能成功打开CSV文件");
                }

                if (dateresult.Columns.Count != 7)
                {
                    return BllResultFactory.Error("CSV文件列数不正确，无法导入");
                }
                for (int i = 0; i < dateresult.Rows.Count; i++)
                {
                    string ZuDuiCheck = dateresult.Rows[i][6].ToString().Replace(" ", "");//对应组队工位列
                    if (ZuDuiCheck != "1" && ZuDuiCheck != "2" && ZuDuiCheck != "3" && ZuDuiCheck != "4")
                    {
                        return BllResultFactory.Error("模板中组对平台值错误,平台值只能为1、2、3、4");
                    }
                    string CodeCheck = dateresult.Rows[i][5].ToString().Replace(" ", "");//对应条码列
                    if (string.IsNullOrEmpty(CodeCheck))
                    {
                        return BllResultFactory.Error("模板中套料信息条码号不能为空");
                    }
                    //DataView myDataView = new DataView(dateresult);
                    //if (myDataView.ToTable(true, "条码").Rows.Count < dateresult.Rows.Count)//是否允许导入工单中存在重复条码，若允许则删除此判断条件
                    //{
                    //    return BllResultFactory.Error("条码重复,请检查");
                    //}
                }

                for (int i = 0; i < dateresult.Rows.Count; i++)
                {
                    //外径
                    string WaiJing = dateresult.Rows[i][1].ToString().Replace(" ", "");
                    //壁厚
                    string BiHou = dateresult.Rows[i][2].ToString().Replace(" ", "");
                    //材质
                    string CaiZhi = dateresult.Rows[i][0].ToString().Replace(" ", "");
                    //等级
                    string Dengji = dateresult.Rows[i][4].ToString().Replace(" ", "");
                    //长度
                    string Length = dateresult.Rows[i][3].ToString().Replace(" ", "");
                    //条码
                    string Code = dateresult.Rows[i][5].ToString().Replace(" ", "");//cf
                    //组对工位
                    string ZuDui = dateresult.Rows[i][6].ToString().Replace(" ", "");

                    if (!decimal.TryParse(WaiJing, out decimal diameter))
                    {
                        continue;
                    }
                    if (!decimal.TryParse(BiHou, out decimal thickness))
                    {
                        continue;
                    }
                    if (!int.TryParse(CaiZhi, out int productCode))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(Dengji))
                    {
                        continue;
                    }
                    if (!int.TryParse(Length, out int lengthInt))
                    {
                        continue;
                    }
                    if (!int.TryParse(ZuDui, out int AssemblyStation))
                    {
                        continue;
                    }

                    //var transfer = AppSession.Dal.GetCommonModelByCondition<ProductHeader>($"where name = '{CaiZhi}'");
                    //if(!transfer.Success)
                    //{
                    //    MessageBox.Show($"查找材料对应信息失败，请检查字典是否录入相关材料信息");
                    //    return;
                    //}

                    //将表格中内容写入数据库
                    PipeOrder pipeOrder = new PipeOrder();
                    pipeOrder.Diameter = diameter;
                    pipeOrder.Thickness = thickness;
                    pipeOrder.ProductCode = productCode;
                    pipeOrder.Length = lengthInt;
                    pipeOrder.Status = StationCacheStatus.初始.GetIndexInt();
                    pipeOrder.Code = Code;
                    pipeOrder.AssemblyStation = AssemblyStation;//cf

                    //判断条码是否存在
                    //var codeCount = AppSession.Dal.GetCommonModelByCondition<PipeOrder>($" where Code='{Code}'");
                    //if (codeCount.Data.Count>0)
                    //{
                    //    continue;//如果数据库中已经存在该条码，则跳过
                    //}
                    //pipeOrder.StationCode = 1;
                    var insertResult = AppSession.Dal.InsertCommonModel<PipeOrder>(pipeOrder);//插入数据库
                    if (!insertResult.Success)
                    {
                        continue;
                    }
                    else
                    {
                        pipeOrder.Id = insertResult.Data;
                    }
                    outputlist[i].WaiJing = WaiJing;
                    outputlist[i].BiHou = BiHou;
                    outputlist[i].CaiZhi = CaiZhi;
                    outputlist[i].Dengji = Dengji;
                    outputlist[i].length = lengthInt;
                    outputlist[i].GuanJianBianHao = pipeOrder.Id.ToString();
                    outputlist[i].Code = Code;//cf
                    outputlist[i].AssemblyStation = AssemblyStation;
                }
                int realcount = outputlist.Count(a => !string.IsNullOrWhiteSpace(a.WaiJing));
                if (realcount != dateresult.Rows.Count)
                {
                    return BllResultFactory.Error("未能全部导入文件内容，部分数据为空或格式不正确，实际导入" + realcount + "条数据");
                }
                else
                {
                    return BllResultFactory.Sucess("导入Excel工单成功！");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"导入Excel工单发生异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 导入数据库原材料
        /// </summary>
        private BllResult importDataBaseMaterial()
        {
            try
            {
                foreach (var value in inputlist)
                {
                    value.clear();
                }

                inputgrid1.ItemsSource = null;
                var stationCacheResult = AppSession.Dal.GetCommonModelByConditionWithZero<StationCache>($" where status = 0 ");
                if (!stationCacheResult.Success)
                {
                    return BllResultFactory.Error($"导入数据库原材料时候，读取数据库出错，原因：{stationCacheResult.Msg}！");
                }
                //缓存区没有新管子
                if (stationCacheResult.Data.Count == 0)
                {
                    return BllResultFactory.Error("数据库中没有未套料得管子信息！");
                }
                if (stationCacheResult.Data.Count > 11)
                {
                    return BllResultFactory.Error("获取到超过11个原材料信息，请检查数据库！");
                }

                for (int i = 0; i < stationCacheResult.Data.Count; i++)
                {
                    //string ID = material.Data[i].Id.ToString().Replace(" ", "");
                    string WaiJing = stationCacheResult.Data[i].Diameter.ToString().Replace(" ", "");//外径
                    string BiHou = stationCacheResult.Data[i].Thickness.ToString().Replace(" ", "");//壁厚
                    string CaiZhi = stationCacheResult.Data[i].WcsProductType.ToString().Replace(" ", "");//材质
                    string Dengji = "0";
                    string MaterialLength = stationCacheResult.Data[i].MaterialLength.ToString().Replace(" ", "");
                    string LuPiHao = "0";
                    string WuLiaoHao = stationCacheResult.Data[i].Id.ToString().Replace(" ", "");

                    if (string.IsNullOrWhiteSpace(WaiJing))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(BiHou))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(CaiZhi))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(LuPiHao))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(WuLiaoHao))
                    {
                        continue;
                    }
                    if (!int.TryParse(MaterialLength, out int Length))
                    {
                        continue;
                    }

                    inputlist[i].WaiJing = WaiJing;
                    inputlist[i].BiHou = BiHou;
                    inputlist[i].CaiZhi = CaiZhi;
                    inputlist[i].Dengji = Dengji;
                    inputlist[i].length = Length;
                    inputlist[i].LuPiHao = LuPiHao;
                    inputlist[i].WuLiaoHao = WuLiaoHao;
                }
                return BllResultFactory.Sucess("导入数据库原材料成功！");
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"导入数据库原材料发生异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 导入数据库工单
        /// </summary>
        private BllResult importDataBaseOrder()
        {
            try
            {
                foreach (var value in outputlist)
                {
                    value.clear();
                }
                outputgrid1.ItemsSource = null;
                var pipeOrderResult = AppSession.Dal.GetCommonModelByConditionWithZero<PipeOrder>($" where status = '0' ");
                if (!pipeOrderResult.Success)
                {
                    return BllResultFactory.Error($"导入数据库工单时候，读取数据库出错，原因：{pipeOrderResult.Msg}！");
                }
                //没有新工单的就返回
                if (pipeOrderResult.Data.Count == 0)
                {
                    return BllResultFactory.Error("数据库中没有未套料的工单信息！");
                }

                for (int i = 0; i < pipeOrderResult.Data.Count; i++)
                {
                    //string pipeId = product.Data[i].Id.ToString().Replace(" ", "");
                    //外径
                    string WaiJing = pipeOrderResult.Data[i].Diameter.ToString().Replace(" ", "");
                    //壁厚
                    string BiHou = pipeOrderResult.Data[i].Thickness.ToString().Replace(" ", "");
                    //材质
                    string CaiZhi = pipeOrderResult.Data[i].ProductCode.ToString().Replace(" ", "");
                    //长度
                    string MaterialLength = pipeOrderResult.Data[i].Length.ToString().Replace(" ", "");
                    //物料号
                    string WuLiaoHao = pipeOrderResult.Data[i].Diameter.ToString().Replace(" ", "");
                    //管件编号
                    string GuanJianBianHao = pipeOrderResult.Data[i].Id.ToString().Replace(" ", "");
                    //等级
                    string Dengji = "0";

                    string LuPiHao = "0";
                    if (string.IsNullOrWhiteSpace(WaiJing))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(BiHou))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(CaiZhi))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(LuPiHao))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(WuLiaoHao))
                    {
                        continue;
                    }
                    if (!int.TryParse(MaterialLength, out int Length))
                    {
                        continue;
                    }

                    outputlist[i].WaiJing = WaiJing;
                    outputlist[i].BiHou = BiHou;
                    outputlist[i].CaiZhi = CaiZhi;
                    outputlist[i].Dengji = Dengji;
                    outputlist[i].length = Length;
                    outputlist[i].GuanJianBianHao = GuanJianBianHao;
                    //outputlist[i].WuLiaoHao = WuLiaoHao;
                    //outputlist[i].LuPiHao = LuPiHao;
                }
                return BllResultFactory.Sucess("导入数据库工单成功！");
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"导入数据库工单发生异常：{ex.Message}");
            }
        }

        #endregion 导入Excel文件、数据库

        #region 读取外界切割方案并发送到PLC

        public void Button_Click_SendToPLC(object sender, RoutedEventArgs e)
        {
            string plcID = ConfigurationManager.AppSettings["plcID"];
            SendResult sendResult = new SendResult(outPutValues, plcID);
            sendResult.ShowDialog();
        }

        #endregion 读取外界切割方案并发送到PLC

    }
}

