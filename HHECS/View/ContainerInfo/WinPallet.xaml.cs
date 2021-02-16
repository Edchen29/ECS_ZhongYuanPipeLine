using FastReport;
using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Controls;
using HHECS.Model.Entities;
using HHECS.View.Win;
using HHECS.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HHECS.View.ContainerInfo
{

    /// <summary>
    /// Frm_Pallet.xaml 的交互逻辑
    /// </summary>
    public partial class WinPallet : HideCloseWindow
    {
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        public WinPallet()
        {
            InitializeComponent();
            this.page.DataContext = PageInfo;
            AppSession.BllService.CheckPermission(App.MenuOperations, new List<UIElement>() { btn_Add, BtnDelete, btn_print, btn_query });
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgv_1.SelectedItems == null || dgv_1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("未选中数据");
                    return;
                }
                bool isDoublePrint = cb_IsDoublePrint.IsChecked == true;
                Report report = new Report();
                report.Load("Content/FastReport/" + App.Report);
                List<Container> temps = new List<Container>();
                foreach (var item in dgv_1.SelectedItems)
                {
                    temps.Add((Container)item);
                }
                foreach (Container item in temps)
                {
                    report.SetParameterValue("Barcode", item.Code);
                    //report.Prepare();
                    //report.Show();
                    report.PrintSettings.Printer = App.PrinterName;
                    report.PrintSettings.ShowDialog = false;
                    report.Print();
                    if (isDoublePrint)
                    {
                        report.Print();
                    }
                    //更新打印次数
                    if (isDoublePrint)
                    {
                        item.PrintCount += 2;
                    }
                    else
                    {
                        item.PrintCount += 1;
                    }
                    AppSession.Dal.UpdateCommonModel<Container>(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印出现异常：" + ex.ToString());
            }
        }

        private void btn_query_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            string sql = $"where 1=1 ";
            if (!String.IsNullOrEmpty(txt_code.Text))
            {
                sql += $" and code =  '{txt_code.Text}' ";
            }
            if (cb_isInWarehouse.IsChecked == true)
            {
                sql += $" and PrintCount > 0";
            }
            if (cb_isPrint.IsChecked == true)
            {
                sql += $" and EXISTS(SELECT * from wcslocation where containerCode = '{txt_code.Text}');";
            }
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<Container>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var res = AppSession.Dal.GetCommonModeByPageCondition<Container>(PageInfo.PageIndex, PageInfo.PageSize, sql, null);
                if (res.Success)
                {
                    dgv_1.ItemsSource = res.Data;
                }
                else
                {
                    MessageBox.Show($"查询出错{res.Msg}");
                }
            }
            else
            {
                MessageBox.Show($"查询出错{result.Msg}");
            }
        }

        /// <summary>
        /// todo:实现容器新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            WinPalletAdd frm_PalletAdd = new WinPalletAdd();
            frm_PalletAdd.ShowDialog();
        }

        private void page_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }

        /// <summary>
        /// todo:实现容器删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中一条数据");
                return;
            }
            else
            {
                if (MessageBox.Show("是否确认删除？这可能导致程序异常？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<Container> list = new List<Container>();
                    foreach (var item in dgv_1.SelectedItems)
                    {
                        list.Add((Container)item);
                    }
                    var ids = list.Select(t => t.Id.Value).ToList();
                    BllResult result = AppSession.Dal.DeleteContainer(ids);
                    if (result.Success)
                    {
                        MessageBox.Show("删除成功");
                    }
                    else
                    {
                        MessageBox.Show("删除失败");
                    }
                    Query();
                }
                else
                {
                    MessageBox.Show("错误");
                }

            }
        }
    }
}
