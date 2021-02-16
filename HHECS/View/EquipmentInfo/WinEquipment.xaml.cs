using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Controls;
using HHECS.Model.Entities;
using HHECS.View.Win;
using HHECS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HHECS.View.EquipmentInfo
{
    /// <summary>
    /// WinEquipment.xaml 的交互逻辑
    /// </summary>
    public partial class WinEquipment : HideCloseWindow
    {
        /// <summary>
        /// 用于属性页数据绑定
        /// </summary>
        public Equipment CurrentEquipment { get; set; } = new Equipment();

        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();


        public WinEquipment()
        {
            InitializeComponent();
            this.GridDetail.DataContext = CurrentEquipment;
            this.page.DataContext = PageInfo;
            AppSession.BllService.CheckPermission(App.MenuOperations, SPEquipment.Children);
            AppSession.BllService.CheckPermission(App.MenuOperations, SPEquipmentProps.Children);

            // 填充设备类型
            var equipmentTypes = AppSession.Dal.GetCommonModelByCondition<EquipmentType>("  ").Data;
            equipmentTypes.Insert(0, new EquipmentType());
            this.ComoboxType.ItemsSource = equipmentTypes;
            this.ComoboxType.DisplayMemberPath = "Name";
            this.ComoboxType.SelectedValuePath = "Id";

            //var temp = AppSession.BllService.GetDictWithDetails("WrokNo");
            //if (temp.Success)
            //{
            //    var dictDetails = temp.Data.DictDetails.ToDictionary(t => t.Value, i => i.Name);
            //   // dictDetails.Add("", "全部");
            //    WrokNo.ItemsSource = dictDetails;
            //    WrokNo.DisplayMemberPath = "Value";
            //    WrokNo.SelectedValuePath = "Key";
            //}
             var lines = AppSession.Dal.GetCommonModelByCondition<Line>("  ").Data;
            lines.Insert(0, new Line());
            this.WrokNo.ItemsSource = lines;
            this.WrokNo.DisplayMemberPath = "LineName";
            this.WrokNo.SelectedValuePath = "Id";
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            string sql = " where 1=1 ";
            if (!String.IsNullOrWhiteSpace(TxtCode.Text))
            {
                sql += $" and code like '%{TxtCode.Text}%'";
            }
            if (!String.IsNullOrWhiteSpace(TxtName.Text))
            {
                sql += $" and name like '%{TxtName.Text}%'";
            }
            if (!String.IsNullOrWhiteSpace(ComoboxType.Text))
            {
                sql += $" and equipmentTypeId = '{ComoboxType.SelectedValue.ToString()}'";
            }
            if (!String.IsNullOrWhiteSpace(WrokNo.Text))
            {
                sql += $" and lineId = {int.Parse(WrokNo.SelectedValue.ToString())}";
            }
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<Equipment>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var a = AppSession.Dal.GetCommonModeByPageCondition<Equipment>(PageInfo.PageIndex, PageInfo.PageSize, sql, "");
                if (a.Success)
                {
                    var equipments = a.Data;
                    var bllResult = AppSession.Dal.GetCommonModelByCondition<EquipmentType>("");
                    var typeList = bllResult.Data;
                    equipments.ForEach(t =>
                    {
                        var temp = typeList?.FirstOrDefault(i => i.Id == t.EquipmentTypeId);
                        if (temp != null)
                        {
                            t.EquipmentType = temp;
                        }
                    });

                    DGMain.ItemsSource = equipments;
                    TIMain.IsSelected = true;
                }
                else
                {
                    MessageBox.Show($"查询出错：{a.Msg}");
                }
            }
            else
            {
                MessageBox.Show($"查询失败{result.Msg}");
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
                QueryDetail((Equipment)DGMain.SelectedItem);
            }
        }

        private void QueryDetail(Equipment equipment)
        {
            //重新查询一遍主数据，防止同步删除
            BllResult<List<Equipment>> result = AppSession.Dal.GetCommonModelByCondition<Equipment>($"where id ={equipment.Id}");
            if (result.Success)
            {
                var temp = result.Data[0];
                CurrentEquipment = temp;
                //CurrentEquipment.Id = temp.Id;
                //CurrentEquipment.Code = temp.Code;
                //CurrentEquipment.Name = temp.Name;
                //CurrentEquipment.EquipmentTypeId = temp.EquipmentTypeId;
                //CurrentEquipment.IP = temp.IP;
                //CurrentEquipment.ConnectName = temp.ConnectName;
                //CurrentEquipment.GroupName = temp.GroupName;
                //CurrentEquipment.BasePlcDB = temp.BasePlcDB;
                //CurrentEquipment.GoAddress = temp.GoAddress;
                //CurrentEquipment.BackAddress = temp.BackAddress;
                //CurrentEquipment.ColumnIndex = temp.ColumnIndex;
                //CurrentEquipment.Description = temp.Description;
                //CurrentEquipment.Created = temp.Created;
                //CurrentEquipment.CreatedBy = temp.CreatedBy;
                //CurrentEquipment.Updated = temp.Updated;
                //CurrentEquipment.UpdatedBy = temp.UpdatedBy;
                //CurrentEquipment.Disable = temp.Disable;
                //查询对应属性
                var a = AppSession.Dal.GetCommonModelByCondition<EquipmentProp>($"where equipmentId = {CurrentEquipment.Id}");
                if (a.Success)
                {
                    DGDetail.ItemsSource = a.Data;
                }
                else
                {
                    DGDetail.ItemsSource = null;
                    MessageBox.Show($"查询当前设备的属性失败:{a.Msg}");

                }
                TIDetail.IsSelected = true;

            }
            else
            {
                MessageBox.Show("查询出错");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinEquipmentAddOrEdit win = new WinEquipmentAddOrEdit(null);
            win.ShowDialog();
            Query();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
            }
            else
            {
                Equipment temp = (Equipment)DGMain.SelectedItem;
                WinEquipmentAddOrEdit win = new WinEquipmentAddOrEdit(temp.Id);
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
                    List<Equipment> list = new List<Equipment>();
                    foreach (var item in DGMain.SelectedItems)
                    {
                        list.Add((Equipment)item);
                    }
                    BllResult result = AppSession.BllService.DeleteEuipmentByIds(list.Select(t => t.Id.Value).ToList());
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

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGridRow dgr = sender as DataGridRow;
                if (!dgr.IsNewItem)
                {
                    QueryDetail((Equipment)dgr.Item);
                    //事件不再向上传递，防止设置其他tabitem的isselected属性失败
                    e.Handled = true;
                    DGMain.SelectedItem = dgr.Item;
                }
            }
        }

        private void BtnSaveProp_Click(object sender, RoutedEventArgs e)
        {
            if (DGDetail.ItemsSource != null)
            {
                foreach (var item in DGDetail.ItemsSource)
                {
                    EquipmentProp equipmentProp = (EquipmentProp)item;
                    var result = AppSession.Dal.UpdateCommonModel<EquipmentProp>(equipmentProp);
                    if (!result.Success)
                    {
                        MessageBox.Show($"属性{equipmentProp.EquipmentTypeTemplateCode}更新失败，操作中止");
                        return;
                    }
                }
                MessageBox.Show("更新成功");
            }
            else
            {
                MessageBox.Show("无可更新项");
            }
        }

        private void BtnUpdateDetail_Click(object sender, RoutedEventArgs e)
        {
            QueryDetail(CurrentEquipment);
        }

        private void BtnSync_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("同步将会使得当前设备的属性与属性模板同步，可能会导致属性增加或减少。是否继续？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BllResult result = AppSession.BllService.SyncEquipmentProp(CurrentEquipment.Id, CurrentEquipment.BasePlcDB);
                MessageBox.Show(result.Success ? "同步成功" : $"同步失败：{result.Msg}");
                QueryDetail(CurrentEquipment);
            }
        }

        private void BtnDeleteProp_Click(object sender, RoutedEventArgs e)
        {
            if (DGDetail.SelectedItems == null)
            {
                MessageBox.Show("未选中数据");
            }
            else
            {
                if (MessageBox.Show("删除可能导致程序异常，是否继续?", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<int> ids = new List<int>();
                    foreach (var item in DGDetail.SelectedItems)
                    {
                        ids.Add(((EquipmentProp)item).Id.Value);
                    }
                    var result = AppSession.Dal.DeleteCommonModelByIds<EquipmentProp>(ids);
                    if (result.Success)
                    {
                        MessageBox.Show("删除成功。");
                        QueryDetail((Equipment)DGMain.SelectedItem);
                    }
                    else
                    {
                        MessageBox.Show($"删除失败：{result.Msg}");
                    }
                }
            }
        }

        private void page_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }

        /// <summary>
        /// 复制一个设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一个设备后再继续复制操作");
            }
            else
            {
                Equipment equipment = (Equipment)DGMain.SelectedItem;
                var result = AppSession.BllService.CopyEquipment(equipment.Id.Value);
                if (result.Success)
                {
                    MessageBox.Show("复制成功");
                    Query();
                }
                else
                {
                    MessageBox.Show($"复制失败：{result.Msg}");
                }
            }

        }
    }
}
