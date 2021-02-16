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
    /// EquipmentTypeAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinEquipmentTypeAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }

        public EquipmentType CurrentEquipmentType
        {
            get { return (EquipmentType)GetValue(CurrentEquipmentTypeProperty); }
            set { SetValue(CurrentEquipmentTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentEquipmentType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEquipmentTypeProperty =
            DependencyProperty.Register("CurrentEquipmentType", typeof(EquipmentType), typeof(WinEquipmentTypeAddOrEdit), new PropertyMetadata(new EquipmentType()));



        public WinEquipmentTypeAddOrEdit(int? id)
        {
            InitializeComponent();
            this.Id = id;
            this.Title = id == null ? "新增" : "编辑";
            Init();
            this.GridMain.DataContext = CurrentEquipmentType;
        }

        private void Init()
        {
            if (Id == null)
            {
                //新增

            }
            else
            {
                //编辑
                BllResult<List<EquipmentType>> result = AppSession.Dal.GetCommonModelByCondition<EquipmentType>($"where id ={Id}");
                if (result.Success)
                {
                    CurrentEquipmentType = result.Data[0];
                    //CurrentEquipmentType.Id = temp.Id;
                    //CurrentEquipmentType.Code = temp.Code;
                    //CurrentEquipmentType.Name = temp.Name;
                    //CurrentEquipmentType.Description = temp.Description;
                    //CurrentEquipmentType.Created = temp.Created;
                    //CurrentEquipmentType.CreatedBy = temp.CreatedBy;
                    //TxtEquipmentTypeCode.IsReadOnly = true;
                }
                else
                {
                    MessageBox.Show($"查询设备类型详情失败:{result.Msg}");
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEquipmentType.Id == null)
            {
                //新增
                CurrentEquipmentType.CreateTime = DateTime.Now;
                CurrentEquipmentType.CreateBy = App.User.UserCode;
                var a = AppSession.Dal.InsertCommonModel<EquipmentType>(CurrentEquipmentType);
                if (a.Success)
                {
                    //新增成功后，不允许修改code了
                    //TxtEquipmentTypeCode.IsReadOnly = true;
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
                CurrentEquipmentType.CreateTime = DateTime.Now;
                CurrentEquipmentType.UpdateBy = App.User.UserCode;
                var a = AppSession.Dal.UpdateCommonModel<EquipmentType>(CurrentEquipmentType);
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
