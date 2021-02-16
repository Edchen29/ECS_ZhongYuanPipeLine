using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Controls;
using HHECS.Model.Entities;
using HHECS.View.Win;
using HHECS.ViewModel;
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

namespace HHECS.View.SystemInfo
{
    /// <summary>
    /// WinConfig.xaml 的交互逻辑
    /// </summary>
    public partial class WinConfig : HideCloseWindow
    {
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM()
        {
            PageIndex = 1,
            PageSize = 30,
            TotalCount = 0
        };
        public WinConfig()
        {
            InitializeComponent();
            this.page.DataContext = PageInfo;
            AppSession.BllService.CheckPermission(App.MenuOperations, WPMain.Children);
            AppSession.BllService.CheckPermission(App.MenuOperations, WPMain2.Children);
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            // string sql = $"where 1=1 and warehouseCode='{App.WarehouseCode}'";
            string sql = $"where 1=1 ";
            if (!String.IsNullOrWhiteSpace(TxtConfigCode.Text))
            {
                sql += $" and code like '{TxtConfigCode.Text}%' ";
            }
            if (!String.IsNullOrWhiteSpace(TxtConfigName.Text))
            {
                sql += $" and name like '{TxtConfigName.Text}%'";
            }

            BllResult<int> result = AppSession.Dal.GetCommonModelCount<Config>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var  a = AppSession.Dal.GetCommonModeByPageCondition<Config>(PageInfo.PageIndex, PageInfo.PageSize, sql, "");
                if (a.Success)
                {
                    DatagridMain.ItemsSource = a.Data;
                }
                else
                {
                    MessageBox.Show($"查询出错：{a.Msg}");
                }
            }
            else
            {
                MessageBox.Show($"查询出错{result.Msg}");
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            WinConfigAddOrEdit win = new WinConfigAddOrEdit(null);
            win.ShowDialog();
            Query();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
            }
            else
            {
                Config config = (Config)DatagridMain.SelectedItem;
                WinConfigAddOrEdit win = new WinConfigAddOrEdit(config.Id);
                win.ShowDialog();
                Query();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条数据！");
            }
            else
            {
                if (MessageBox.Show("是否确认删除？这可能导致程序异常！", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<Config> list = new List<Config>();
                    foreach (var item in DatagridMain.SelectedItems)
                    {
                        list.Add((Config)item);
                    }
                    BllResult result = AppSession.BllService.DeleteConfigByIds(list.Select(t => t.Id.Value).ToList());
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
            }
            
        }

        /// <summary>
        /// 分页完成时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataPager_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }

        /// <summary>
        /// 分页前处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PageChanging(object sender, PageChangingEventArgs e)
        {

        }

        private void Page_PageChanged(object sender, PageChangedEventArgs e)
        {

        }
    }
}
