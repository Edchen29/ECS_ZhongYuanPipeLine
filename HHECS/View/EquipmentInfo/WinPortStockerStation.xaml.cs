using HHECS.Bll;
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

namespace HHECS.View.EquipmentInfo
{
    /// <summary>
    /// WinPortStockerStation.xaml 的交互逻辑
    /// </summary>
    public partial class WinPortStockerStation : HideCloseWindow
    {

        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();

        public WinPortStockerStation()
        {
            InitializeComponent();
            Init();
        }
        /// <summary>
        /// 数据初始化
        /// </summary>
        private void Init()
        {
            this.page.DataContext = PageInfo;
            AppSession.BllService.CheckPermission(App.MenuOperations, new List<UIElement>() { BtnAdd, BtnDelete, btn_Edit, btn_Query });
            

        }
        private void btn_Query_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }
        private void Query()
        {
            string sql = "where 1=1 ";
            if (!string.IsNullOrWhiteSpace(txt_Port.Text))
            {
                sql += $" and portCode like '{txt_Port.Text}%'";
            }
            if (!string.IsNullOrWhiteSpace(txt_Roadway.Text))
            {
                sql += $" and roadway like '{txt_Roadway.Text}%'";
            }

            BllResult<int> result = AppSession.Dal.GetCommonModelCount<PortSRMStationRelative>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var temp = AppSession.Dal.GetCommonModeByPageCondition<PortSRMStationRelative>(PageInfo.PageIndex, PageInfo.PageSize, sql, null);
                dgv_1.ItemsSource = temp.Data;
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_1.SelectedItem == null)
            {
                MessageBox.Show("请选择一条数据");
            }
            else
            {
                PortSRMStationRelative temp = (PortSRMStationRelative)dgv_1.SelectedItem;
                WinPortStockerStationAddOrEdit win = new WinPortStockerStationAddOrEdit(temp.Id);
                win.ShowDialog();
                Query();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinPortStockerStationAddOrEdit win = new WinPortStockerStationAddOrEdit();
            win.ShowDialog();
            Query();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条数据！");
            }
            else
            {
                if (MessageBox.Show("是否确认删除？这可能导致程序异常！", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var list = dgv_1.SelectedItems.Cast<PortSRMStationRelative>().ToList();
                    BllResult result = AppSession.Dal.DeleteCommonModelByIds<PortSRMStationRelative>(list.Select(t => t.Id.Value).ToList());
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

        private void page_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }
    }
}
