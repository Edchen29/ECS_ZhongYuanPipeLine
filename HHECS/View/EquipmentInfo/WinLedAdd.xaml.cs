using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.LEDHelper.LEDComponent.DefaultLEDComponent;
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
    /// WinLedAdd.xaml 的交互逻辑
    /// </summary>
    public partial class WinLedAdd : Window
    {
        public int? Id { get; set; }

        public LEDEntity HHLED { get; set; } = new LEDEntity();

        public WinLedAdd(int? id)
        {
            InitializeComponent();
            this.Id = id;
            Init();
            this.Title = id == null ? "新增" : "编辑";
            this.GridMain.DataContext = HHLED;
        }
        private void Init()
        {

            if (Id != null)
            {
                BllResult<List<LEDEntity>> result = AppSession.Dal.GetCommonModelByCondition<LEDEntity>($"where id ={Id}");
                if (result.Success)
                {
                    var temp = result.Data[0];
                    HHLED.Id = temp.Id;
                    HHLED.Code = temp.Code;
                    HHLED.Name = temp.Name;
                    HHLED.IP = temp.IP;
                    HHLED.Port = temp.Port;
                    HHLED.Remark = temp.Remark;
                    HHLED.Created = temp.Created;
                    HHLED.Createdby = App.User.UserCode;
                    HHLED.Updated = DateTime.Now;
                    HHLED.Updatedby = App.User.UserCode;
                    HHLED.WarehouseCode = temp.WarehouseCode;
                }
                else
                {
                    MessageBox.Show($"查询LED详情失败:{result.Msg}");
                }
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (HHLED.Id == null)
            {
                LEDEntity lEDEntity = new LEDEntity();
                lEDEntity.Code = TxtLEDCode.Text;
                lEDEntity.IP = TxtLEDIp.Text;
                lEDEntity.Name = TxtLEDName.Text;
                lEDEntity.Port = Convert.ToUInt32(TxtLEDPort.Text);
                lEDEntity.Remark = TxtRemark.Text;
                lEDEntity.Updated = DateTime.Now;
                lEDEntity.Updatedby = App.User.UserCode;
                lEDEntity.WarehouseCode = App.WarehouseCode;


                var a = AppSession.Dal.InsertCommonModel<LEDEntity>(lEDEntity);
                if (a.Success)
                {
                    MessageBox.Show("新增成功");
                }
                else
                {
                    MessageBox.Show($"新增失败：{a.Msg}");
                }

            }
            else
            {
                var a = AppSession.Dal.UpdateCommonModel<LEDEntity>(HHLED);
                if (a.Success)
                {
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show($"更新失败：{a.Msg}");
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
