using HHECS.Bll;
using HHECS.Model.BllModel;
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
    /// WinStation.xaml 的交互逻辑
    /// </summary>
    public partial class WinStation : HideCloseWindow
    {
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        public WinStation()
        {
            InitializeComponent();
            this.page.DataContext = PageInfo;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Query_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            string sql = $"where 1=1";
            if (!String.IsNullOrWhiteSpace(txt_Code.Text.Trim()))
            {
                sql += $"and lineCode like {txt_Code.Text.Trim()}";
            }
            BllResult<int> countResult = AppSession.Dal.GetCommonModelCount<Station>(sql);
            if (countResult.Success)
            {
                PageInfo.TotalCount = countResult.Data;
                var res = AppSession.Dal.GetCommonModelByCondition<Station>(sql);
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
                MessageBox.Show($"查询记录数出错{countResult.Msg}");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Change_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_1.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
            }
            else
            {
                Station id = (Station)dgv_1.SelectedItem;
                WinStationAddOrEdit winStationAddOrEdit = new WinStationAddOrEdit(id.Id);
                winStationAddOrEdit.ShowDialog();
                Query();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinStationAddOrEdit winStationAddOrEdit = new WinStationAddOrEdit(null);
            winStationAddOrEdit.ShowDialog();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_1.SelectedItem == null)
            {
                MessageBox.Show("请选中一条任务后继续");
                return;
            }
            if (MessageBox.Show($"是否删除数据？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                List<Station> list = new List<Station>();
                foreach (var item in dgv_1.SelectedItems)
                {
                    list.Add((Station)item);
                }
                var Delids = list.Select(t => t.Id.Value).ToList();

                var DelRes = AppSession.StationService.StationDeleteId(Delids, App.User.UserCode);
                if (DelRes.Success)
                {
                    MessageBox.Show($"删除成功");
                }
                else
                {
                    MessageBox.Show($"删除失败{DelRes.Msg}");
                }
            }
        }

        private void page_PageChanged(object sender, Model.Controls.PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }
    }
}
