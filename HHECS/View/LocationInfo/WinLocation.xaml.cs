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

namespace HHECS.View.LocationInfo
{
    /// <summary>
    /// WinLocation.xaml 的交互逻辑
    /// </summary>
    public partial class WinLocation : HideCloseWindow
    {
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        public WinLocation()
        {
            InitializeComponent();
            this.page.DataContext = PageInfo;
            AppSession.BllService.CheckPermission(App.MenuOperations, new List<UIElement>() { BtnAdd, BtnDelete, btn_ChangeStatus, btn_Query });
        }

        private void btn_Query_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            string sql = "where 1=1 ";
            if (!string.IsNullOrWhiteSpace(txt_Code.Text))
            {
                sql += $" and code like '{txt_Code.Text}%'";
            }
            if (!string.IsNullOrWhiteSpace(cbx_Status.SelectedValue?.ToString()))
            {
                sql += $" and status = '{cbx_Status.SelectedValue.ToString()}'";
            }
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<Location>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var temp = AppSession.Dal.GetCommonModeByPageCondition<Location>(PageInfo.PageIndex, PageInfo.PageSize, sql, null);
                dgv_1.ItemsSource = temp.Data;
            }
        }

        private void btn_ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_1.SelectedItems == null || dgv_1.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中任何数据");
            }
            else
            {
                List<Location> temp = new List<Location>();
                foreach (var item in dgv_1.SelectedItems)
                {
                    temp.Add((Location)item);
                }
                WinLocationStatusChange frm = new WinLocationStatusChange(temp.Select(t => t.Code).ToList());
                frm.ShowDialog();
                btn_Query_Click(null, null);
            }
        }

        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in dgv_1.Items)
            {
                DataGridTemplateColumn templeColumn = dgv_1.Columns[0] as DataGridTemplateColumn;
                FrameworkElement fwElement = dgv_1.Columns[0].GetCellContent(item);

                if (fwElement != null)
                {
                    CheckBox cBox = templeColumn.CellTemplate.FindName("cb", fwElement) as CheckBox;
                    if (cBox.IsChecked == true)
                    {
                        cBox.IsChecked = false;
                    }
                    else
                    {
                        if (cBox != null)
                        {
                            cBox.IsChecked = true;
                        }
                        else
                        {
                            cBox.IsChecked = false;
                        }
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = AppSession.BllService.GetDictWithDetails("LocationStatus");
            if (temp.Success)
            {
                var dictDetails = temp.Data.DictDetails.ToDictionary(t => t.Value, i => i.Name);
                dictDetails.Add("", "全部");
                cbx_Status.ItemsSource = dictDetails;
                cbx_Status.DisplayMemberPath = "Value";
                cbx_Status.SelectedValuePath = "Key";
            }
            else
            {
                MessageBox.Show("加载状态失败");
            }
        }

        private void page_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }

        /// <summary>
        /// todo:库位新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinLocationAdd winLocationAdd = new WinLocationAdd();
            winLocationAdd.ShowDialog();
        }

        /// <summary>
        /// todo:库位删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_1.SelectedItems != null && dgv_1.SelectedItems.Count > 0)
            {
                BllResult result = AppSession.LocationService.DeleteLocations(dgv_1.SelectedItems.Cast<Location>().Select(t => t.Code).ToList());
                MessageBox.Show(result.Msg);
                Query();
            }
            else
            {
                MessageBox.Show("请先选中数据");
            }
        }
    }
}
