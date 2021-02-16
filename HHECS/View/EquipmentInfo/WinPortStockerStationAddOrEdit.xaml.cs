using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
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

namespace HHECS.View
{
    /// <summary>
    /// WinPortStockerStationAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinPortStockerStationAddOrEdit : BaseWindow
    {


        public PortSRMStationRelative PortSRMStationRelative
        {
            get { return (PortSRMStationRelative)GetValue(PortSRMStationRelativeProperty); }
            set { SetValue(PortSRMStationRelativeProperty, value); }
        }

        public static readonly DependencyProperty PortSRMStationRelativeProperty =
            DependencyProperty.Register("PortSRMStationRelative", typeof(PortSRMStationRelative), typeof(WinPortStockerStationAddOrEdit), new PropertyMetadata(new PortSRMStationRelative()));


        public WinPortStockerStationAddOrEdit(int? id = null)
        {
            InitializeComponent();
            if (id.HasValue)
            {
                var result = AppSession.Dal.GetCommonModelByCondition<PortSRMStationRelative>($"where id = {id.Value}");
                if (!result.Success)
                {
                    MessageBox.Show("没有获取到数据");
                    BtnComfrim.IsEnabled = false;
                }
                this.Title = this.Title + "- 编辑";
                PortSRMStationRelative = result.Data[0];
            }
            else
            {
                this.Title = this.Title + "- 新增";
            }
            var warehouseResult = AppSession.Dal.GetCommonModelByCondition<Warehouse>("");
            if (!warehouseResult.Success)
            {
                MessageBox.Show($"未能获取到仓库列表：{warehouseResult.Msg}");
                BtnComfrim.IsEnabled = false;
            }
            CbxWarehouse.ItemsSource = warehouseResult.Data;
            CbxWarehouse.DisplayMemberPath = "Name";
            CbxWarehouse.SelectedValuePath = "Code";

            var dict = CommonHelper.EnumListDic<InOutFlag>("");
            CbxInOut.ItemsSource = dict;
            CbxInOut.DisplayMemberPath = "Key";
            CbxInOut.SelectedValuePath = "Value";
            GridMain.DataContext = PortSRMStationRelative;
        }

        private void BtnComfrim_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(PortSRMStationRelative.DestinationArea))
            {
                MessageBox.Show("区域不能为空");
                return;
            }
            if (PortSRMStationRelative.Id.HasValue)
            {
                //编辑
                PortSRMStationRelative.Updated = DateTime.Now;
                PortSRMStationRelative.UpdatedBy = App.User.UserCode;
                var result = AppSession.Dal.UpdateCommonModel<PortSRMStationRelative>(PortSRMStationRelative);
                if (result.Success)
                {
                    MessageBox.Show($"更新成功");
                }
                else
                {
                    MessageBox.Show($"更新失败：{result.Msg}");
                }
            }
            else
            {
                //新增
                PortSRMStationRelative.Created = DateTime.Now;
                PortSRMStationRelative.CreatedBy = App.User.UserCode;
                var result = AppSession.Dal.InsertCommonModel<PortSRMStationRelative>(PortSRMStationRelative);
                if (result.Success)
                {
                    MessageBox.Show($"新增成功");
                }
                else
                {
                    MessageBox.Show($"新增失败：{result.Msg}");
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
