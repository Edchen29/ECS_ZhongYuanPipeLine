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
    /// EquipmentAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinEquipmentAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }
        //public Equipment CurrentEquipment { get; set; } = new Equipment();



        public Equipment CurrentEquipment
        {
            get { return (Equipment)GetValue(CurrentEquipmentProperty); }
            set { SetValue(CurrentEquipmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentEquipment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEquipmentProperty =
            DependencyProperty.Register("CurrentEquipment", typeof(Equipment), typeof(WinEquipmentAddOrEdit), new PropertyMetadata(new Equipment()));



        public WinEquipmentAddOrEdit(int? id)
        {
            InitializeComponent();
            this.Id = id;
            Init();
            this.Title = id == null ? "新增" : "编辑";
            this.GridMain.DataContext = CurrentEquipment;
        }

        private void Init()
        {

            if (Id == null)
            {
                //新增
                CurrentEquipment.WarehouseCode = App.WarehouseCode;
            }
            else
            {
                //编辑
                BllResult<List<Equipment>> result = AppSession.Dal.GetCommonModelByCondition<Equipment>($"where id ={Id}");
                if (result.Success)
                {
                    var temp = result.Data[0];
                    CurrentEquipment = temp;
                }
                else
                {
                    MessageBox.Show($"查询设备详情失败:{result.Msg}");
                }
            }
            var a = AppSession.Dal.GetCommonModelByCondition<EquipmentType>("");
            if (a.Success)
            {
                var temp = a.Data;
                CBEquipmentType.ItemsSource = temp;
                CBEquipmentType.DisplayMemberPath = "Name";
                CBEquipmentType.SelectedValuePath = "Id";
                CBEquipmentType.SelectedIndex = 0;
                
            }
            else
            {
                MessageBox.Show("获取设备类型数据失败。");
            }

            var temp1 = AppSession.BllService.GetDictWithDetails("WrokNo");
            if (temp1.Success)
            {
                var dictDetails = temp1.Data.DictDetails.ToDictionary(t => t.Value, i => i.Name);
                // dictDetails.Add("", "全部");
                WrokNo.ItemsSource = dictDetails;
                WrokNo.DisplayMemberPath = "Value";
                WrokNo.SelectedValuePath = "Key";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CBEquipmentType.SelectedIndex == -1)
            {
                MessageBox.Show("未选择设备类型");
                return;
            }
            if (CurrentEquipment.Id==null)
            {
                //新增
                CurrentEquipment.CreateTime = DateTime.Now;
                CurrentEquipment.CreateBy = App.User.UserCode;
                var a = AppSession.Dal.InsertCommonModel<Equipment>(CurrentEquipment);
                if (a.Success)
                {
                    //TxtEquipmentCode.IsReadOnly = true;
                    MessageBox.Show("新增成功");
                }
                else
                {
                    MessageBox.Show($"新增失败{a.Msg}");
                }
            }
            else
            {
                //更新
                var a = AppSession.Dal.UpdateCommonModel<Equipment>(CurrentEquipment);
                if (a.Success)
                {
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show($"更新失败:{a.Msg}");
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
