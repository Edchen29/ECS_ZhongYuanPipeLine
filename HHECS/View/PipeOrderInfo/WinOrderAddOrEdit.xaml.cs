using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums.PipeLine;
using HHECS.View.Win;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HHECS.View.PipeOrderInfo
{
    /// <summary>
    /// WinOrderAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinOrderAddOrEdit : BaseWindow
    {

        public int? Id { get; set; }


        public PipeOrder CurrentPipeOrder
        {
            get { return (PipeOrder)GetValue(CurrentPipeOrderProperty); }
            set { SetValue(CurrentPipeOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentEquipmentType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPipeOrderProperty =
            DependencyProperty.Register("CurrentEquipmentType", typeof(PipeOrder), typeof(WinOrderAddOrEdit), new PropertyMetadata(new PipeOrder()));
        public WinOrderAddOrEdit(int? id)
        {
            InitializeComponent();
            this.Id = id;
            this.Title = id == null ? "新增" : "编辑";
            Init();
            this.GridMain.DataContext = CurrentPipeOrder;
        }

        private void Init()
        {
            //管材缓存状态列表
            var StationCacheStatusList = CommonHelper.EnumListDic<OrderStatus>("");
            cbxStatus.ItemsSource = StationCacheStatusList;
            cbxStatus.DisplayMemberPath = "Value";
            cbxStatus.SelectedValuePath = "Key";
            if (Id == null)
            {
                //新增

            }
            else
            {
                //编辑
                BllResult<List<PipeOrder>> result = AppSession.Dal.GetCommonModelByCondition<PipeOrder>($"where id ={Id}");
                if (result.Success)
                {
                    CurrentPipeOrder = result.Data[0];
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
            if (CurrentPipeOrder.Id == null)
            {
                //新增
                CurrentPipeOrder.CreateTime = DateTime.Now;
                CurrentPipeOrder.CreateBy = App.User.UserCode;
                var a = AppSession.Dal.InsertCommonModel<PipeOrder>(CurrentPipeOrder);
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
                CurrentPipeOrder.CreateTime = DateTime.Now;
                CurrentPipeOrder.UpdateBy = App.User.UserCode;
                var a = AppSession.Dal.UpdateCommonModel<PipeOrder>(CurrentPipeOrder);
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
