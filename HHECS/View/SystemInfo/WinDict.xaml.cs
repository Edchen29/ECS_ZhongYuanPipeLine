using HHECS.Bll;
using HHECS.Controls;
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
    /// WinDict.xaml 的交互逻辑
    /// </summary>
    public partial class WinDict : HideCloseWindow
    {
        /// <summary>
        /// 用于明细页绑定
        /// </summary>
        public Dict CurrentDict { get; set; } = new Dict();
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        public WinDict()
        {
            InitializeComponent();
            this.page.DataContext = PageInfo;
            this.GridDetail.DataContext = CurrentDict;
            AppSession.BllService.CheckPermission(App.MenuOperations, SPMain.Children);
            AppSession.BllService.CheckPermission(App.MenuOperations, SPDetail.Children);
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            string sql = "where 1=1 ";
            if (!String.IsNullOrWhiteSpace(TxtCode.Text))
            {
                sql += $" and code like '{TxtCode.Text}%'";
            }
            if (!String.IsNullOrWhiteSpace(TxtName.Text))
            {
                sql += $" and name like '{TxtName.Text}%'";
            }
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<Dict>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                BllResult<List<Dict>> a = AppSession.Dal.GetCommonModeByPageCondition<Dict>(PageInfo.PageIndex, PageInfo.PageSize, sql, "");
                if (a.Success)
                {
                    DGMain.ItemsSource = a.Data;
                    TIMain.IsSelected = true;
                    DGDetail.ItemsSource = null;
                }
                else
                {
                    MessageBox.Show($"查询失败{a.Msg}");
                }
            }
            else
            {
                MessageBox.Show($"查询失败{result.Msg}");
            }
        }

        private void QueryDetail(Dict dict)
        {
            BllResult<List<DictDetail>> result = AppSession.Dal.GetCommonModelByCondition<DictDetail>($"where headId ={dict.Id}");
            if (result.Success)
            {
                var temp = result.Data;
                temp.ForEach(t => { t.HeadCode = dict.Code; t.HeadName = dict.Name; });
                DGDetail.ItemsSource = temp;
                TIDetail.IsSelected = true;
                CurrentDict = dict;
                GridDetail.DataContext = CurrentDict;
            }
            else
            {
                CurrentDict = dict;
                GridDetail.DataContext = CurrentDict;
                TIDetail.IsSelected = true;
                MessageBox.Show($"查询明细失败{result.Msg}");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinDictAddOrEdit win = new WinDictAddOrEdit(null);
            win.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据");
            }
            else
            {
                Dict temp = (Dict)DGMain.SelectedItem;
                WinDictAddOrEdit win = new WinDictAddOrEdit(temp.Id);
                win.ShowDialog();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItems == null || DGMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条数据");
            }
            else
            {
                if (MessageBox.Show("删除数据字典可能导致程序异常，是否继续？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<Dict> list = new List<Dict>();
                    foreach (var item in DGMain.SelectedItems)
                    {
                        list.Add((Dict)item);
                    }
                    BllResult result = AppSession.BllService.DeleteDictByIds(list.Select(t => t.Id.Value).ToList());
                    if (result.Success)
                    {
                        MessageBox.Show("删除成功");
                    }
                    else
                    {
                        MessageBox.Show($"删除失败：{result.Msg}");
                    }
                    Query();
                }
            }
        }

        private void BtnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDict.Id.HasValue)
            {
                WinDictDetialAddOrEdit win = new WinDictDetialAddOrEdit(null, CurrentDict.Id.Value);
                win.ShowDialog();
            }
            else
            {
                MessageBox.Show("无主数据");
            }
        }

        private void BtnEditDetial_Click(object sender, RoutedEventArgs e)
        {
            if (DGDetail.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条字典明细数据，再行编辑");
            }
            else
            {
                DictDetail detail = (DictDetail)DGDetail.SelectedItem;
                WinDictDetialAddOrEdit win = new WinDictDetialAddOrEdit(detail.Id, detail.HeadId);
                win.ShowDialog();
            }
        }

        private void BtnDeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            if (DGDetail.SelectedItems == null || DGDetail.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条字典明细");
            }
            else
            {
                if (MessageBox.Show("删除数据字典明细可能导致程序异常，是否继续？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<DictDetail> list = new List<DictDetail>();
                    foreach (var item in DGDetail.SelectedItems)
                    {
                        list.Add((DictDetail)item);
                    }
                    BllResult result = AppSession.Dal.DeleteCommonModelByIds<DictDetail>(list.Select(t => t.Id.Value).ToList());
                    if (result.Success)
                    {
                        MessageBox.Show("删除成功");
                    }
                    else
                    {
                        MessageBox.Show($"删除失败：{result.Msg}");
                    }
                    //这里重新按照headid来查询下
                    BllResult<List<Dict>> a = AppSession.Dal.GetCommonModelByCondition<Dict>($"where id = {list[0].HeadId}");
                    if (a.Success)
                    {
                        QueryDetail(a.Data[0]);
                    }
                    else
                    {
                        DGDetail.ItemsSource = null;
                    }
                }
            }
        }

        /// <summary>
        /// 主表双击事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGridRow dgr = sender as DataGridRow;
                if (!dgr.IsNewItem)
                {
                    QueryDetail((Dict)dgr.Item);
                    //事件不再向上传递，防止设置其他tabitem的isselected属性失败
                    e.Handled = true;
                    DGMain.SelectedItem = dgr.Item;
                }
            }
        }

        private void BtnQueryDetail_Click(object sender, RoutedEventArgs e)
        {
            QueryDetail(CurrentDict);
        }

        private void page_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageSize = e.CurrentPageIndex;
            Query();
        }
    }
}
