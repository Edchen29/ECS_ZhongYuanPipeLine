using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.View.Win;
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
    /// WinEquipmentType.xaml 的交互逻辑
    /// </summary>
    public partial class WinEquipmentType : HideCloseWindow
    {
        /// <summary>
        /// 用作详情页的VM
        /// </summary>
        public EquipmentType CurrentEquipmentType { get; set; } = new EquipmentType();
        public WinEquipmentType()
        {
            InitializeComponent();
            this.GridDetail.DataContext = CurrentEquipmentType;
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
            BllResult<List<EquipmentType>> result = AppSession.Dal.GetCommonModelByCondition<EquipmentType>(sql);
            if (result.Success)
            {
                DGMain.ItemsSource = result.Data;
                TIMain.IsSelected = true;
            }
            else
            {
                DGMain.ItemsSource = null;
                MessageBox.Show($"查询失败{result.Msg}");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinEquipmentTypeAddOrEdit win = new WinEquipmentTypeAddOrEdit(null);
            win.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
            }
            else
            {
                EquipmentType temp = (EquipmentType)DGMain.SelectedItem;
                WinEquipmentTypeAddOrEdit win = new WinEquipmentTypeAddOrEdit(temp.Id);
                win.ShowDialog();
                Query();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条数据！");
            }
            else
            {
                if (MessageBox.Show("是否确认删除？这可能导致程序异常！", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<EquipmentType> list = new List<EquipmentType>();
                    foreach (var item in DGMain.SelectedItems)
                    {
                        list.Add((EquipmentType)item);
                    }
                    BllResult result = AppSession.BllService.DeleteEuipmentTypeByIds(list.Select(t => t.Id.Value).ToList());
                    if (result.Success)
                    {
                        MessageBox.Show("删除成功");
                    }
                    else
                    {
                        MessageBox.Show($"删除失败:{result.Msg}");
                    }
                    Query();
                }
            }
        }

        private void BtnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEquipmentType.Id == null)
            {
                MessageBox.Show("请先选择主数据");
            }
            else
            {
                WinEquipmentTypeTemplateAddOrEdit win = new WinEquipmentTypeTemplateAddOrEdit(null, CurrentEquipmentType);
                win.ShowDialog();
            }
        }

        private void BtnEditDetial_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEquipmentType.Id == null)
            {
                MessageBox.Show("请先选择主数据");
            }else if (DGDetail.SelectedItem == null)
            {
                MessageBox.Show("请选择一行明细数据");
            }
            else
            {
                var a = (EquipmentTypeTemplate)DGDetail.SelectedItem;
                WinEquipmentTypeTemplateAddOrEdit win = new WinEquipmentTypeTemplateAddOrEdit(a.Id, CurrentEquipmentType);
                win.ShowDialog();
            }
        }

        private void BtnDeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            if (DGDetail.SelectedItems == null || DGDetail.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条属性明细");
            }
            else
            {
                if (MessageBox.Show("删除设备属性明细可能导致程序异常，是否继续？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<EquipmentTypeTemplate> list = new List<EquipmentTypeTemplate>();
                    foreach (var item in DGDetail.SelectedItems)
                    {
                        list.Add((EquipmentTypeTemplate)item);
                    }
                    BllResult result = AppSession.BllService.DeleteEquipmentTypeTemplateByIds(list.Select(t => t.Id.Value).ToList());
                    if (result.Success)
                    {
                        MessageBox.Show("删除成功");
                    }
                    else
                    {
                        MessageBox.Show($"删除失败:{result.Msg}");
                    }
                    QueryDetail(CurrentEquipmentType);
                }
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGridRow dgr = sender as DataGridRow;
                if (!dgr.IsNewItem)
                {
                    QueryDetail((EquipmentType)dgr.Item);
                    //事件不再向上传递，防止设置其他tabitem的isselected属性失败
                    e.Handled = true;
                    DGMain.SelectedItem = dgr.Item;
                }
            }
        }

        private void QueryDetail(EquipmentType item)
        {
            //重新查询一遍主数据，防止同步删除
            BllResult<List<EquipmentType>> result = AppSession.Dal.GetCommonModelByCondition<EquipmentType>($"where id ={item.Id}");
            if (result.Success)
            {
                var temp = result.Data[0];
                CurrentEquipmentType.Id = temp.Id;
                CurrentEquipmentType.Code = temp.Code;
                CurrentEquipmentType.Name = temp.Name;
                CurrentEquipmentType.Description = temp.Description;
                CurrentEquipmentType.CreateTime = temp.CreateTime;
                CurrentEquipmentType.CreateBy = temp.CreateBy;
                CurrentEquipmentType.CreateTime = temp.CreateTime;
                CurrentEquipmentType.UpdateBy = temp.UpdateBy;
                
                //查询属性模板
                var a = AppSession.Dal.GetCommonModelByCondition<EquipmentTypeTemplate>($"where equipmentTypeId = {CurrentEquipmentType.Id}");
                if (a.Success)
                {
                    DGDetail.ItemsSource = a.Data;
                }
                else
                {
                    DGDetail.ItemsSource = null;
                    MessageBox.Show($"查询当前设备类型的属性模板失败:{a.Msg}");
                    
                }
                TIDetail.IsSelected = true;
            }
            else
            {
                MessageBox.Show("未能查询到主数据，请刷新");
                TIMain.IsSelected = true;
            }
        }

        private void BtnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
            }
            else
            {
                QueryDetail((EquipmentType)DGMain.SelectedItem);
            }
        }

        private void BtnUpdateDetail_Click(object sender, RoutedEventArgs e)
        {
            QueryDetail(CurrentEquipmentType);
        }

        /// <summary>
        /// 复制一个设备类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
            }
            else
            {
                BllResult result = AppSession.BllService.CopyEquipment((EquipmentType)DGMain.SelectedItem);
                if (result.Success)
                {
                    MessageBox.Show("复制成功");
                }
                else
                {
                    MessageBox.Show($"复制失败：{result.Msg}");
                }
                Query();
            }
        }
    }
}
