using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Controls;
using HHECS.Model.Entities;
using HHECS.Model.LEDHelper.LEDComponent.DefaultLEDComponent;
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
    /// WinLed.xaml 的交互逻辑
    /// </summary>
    public partial class WinLed : HideCloseWindow
    {
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        public WinLed()
        {
            InitializeComponent();
            this.page.DataContext = PageInfo;
            AppSession.BllService.CheckPermission(App.MenuOperations, new List<UIElement>() { BtnAdd, BtnDelete, btn_Change, btn_Query });
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
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<LEDEntity>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var temp = AppSession.Dal.GetCommonModeByPageCondition<LEDEntity>(PageInfo.PageIndex, PageInfo.PageSize, sql, null);
                dgv_1.ItemsSource = temp.Data;
            }
        }

        private void btn_Change_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_1.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
            }
            else
            {
                LEDEntity led = (LEDEntity)dgv_1.SelectedItem;
                WinLedAdd winLedAdd = new WinLedAdd(led.Id);
                winLedAdd.ShowDialog();
                Query();
            }
        }

        private void page_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }

        /// <summary>
        /// todo:新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinLedAdd winLedAdd = new WinLedAdd(null);
            winLedAdd.ShowDialog();
        }

        /// <summary>
        /// todo:删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    List<LEDEntity> list = new List<LEDEntity>();
                    foreach (var item in dgv_1.SelectedItems)
                    {
                        list.Add((LEDEntity)item);
                    }
                    BllResult result = AppSession.BllService.DeleteLEDS(list.Select(t => t.Id.Value).ToList());
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

    }        
 }
            

