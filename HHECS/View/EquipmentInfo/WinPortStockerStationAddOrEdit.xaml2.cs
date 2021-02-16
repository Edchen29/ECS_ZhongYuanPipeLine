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
    public partial class WinPortStockerStationAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }
        public PortStockerStationRelative CurrentEquipment { get; set; } = new PortStockerStationRelative();

        public WinPortStockerStationAddOrEdit(int? id)
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
                    CurrentEquipment.Id = temp.Id;
                    CurrentEquipment.Code = temp.Code;
                    CurrentEquipment.Name = temp.Name;
                    CurrentEquipment.EquipmentTypeId = temp.EquipmentTypeId;
                    CurrentEquipment.IP = temp.IP;
                    CurrentEquipment.ScanIP = temp.ScanIP;
                    CurrentEquipment.LEDIP = temp.LEDIP;
                    CurrentEquipment.SelfAddress = temp.SelfAddress;
                    CurrentEquipment.BackAddress = temp.BackAddress;
                    CurrentEquipment.GoAddress = temp.GoAddress;

                    CurrentEquipment.RoadWay = temp.RoadWay;
                    CurrentEquipment.ConnectName = temp.ConnectName;
                    CurrentEquipment.BasePlcDB = temp.BasePlcDB;
                    CurrentEquipment.Description = temp.Description;
                    CurrentEquipment.Created = temp.Created;
                    CurrentEquipment.CreatedBy = temp.CreatedBy;
                    CurrentEquipment.Disable = temp.Disable;
                   
                    CurrentEquipment.StationIndex = temp.StationIndex;
                    CurrentEquipment.RowIndex = temp.RowIndex;
                    CurrentEquipment.ColumnIndex = temp.ColumnIndex;


                    CurrentEquipment.WarehouseCode = temp.WarehouseCode;
                    //TxtEquipmentCode.IsReadOnly=true;
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
                CurrentEquipment.Created = DateTime.Now;
                CurrentEquipment.CreatedBy = App.User.UserCode;
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
