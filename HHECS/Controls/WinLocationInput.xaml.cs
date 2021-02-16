using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
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

namespace HHECS.Controls
{
    /// <summary>
    /// Frm_LocationInput.xaml 的交互逻辑
    /// </summary>
    public partial class WinLocationInput : Window
    {
        public Equipment DeviceEntity { get; set; }
        public Location NewLocation { get; set; }

        public string LocationCode
        {
            get { return (string)GetValue(locationCodeProperty); }
            set { SetValue(locationCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for locationCode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty locationCodeProperty =
            DependencyProperty.Register("locationCode", typeof(string), typeof(WinLocationInput), new PropertyMetadata(""));



        public WinLocationInput()
        {
            InitializeComponent();
            Txt_Location.SetBinding(TextBox.TextProperty, new Binding("LocationCode") { Source = this, Mode = BindingMode.TwoWay });
            Lab_1.Content = $"请重新录入一个巷道为{DeviceEntity?.RoadWay}的库位";

        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Btn_Verify_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LocationCode))
            {
                MessageBox.Show("请先录入库位");
            }
            else
            {
                BllResult<List<Location>> bllResult = AppSession.LocationService.GetAllLocations(null, null, null, null, null, null, LocationCode,null);
                if (bllResult.Success)
                {
                    Location locationEntity = bllResult.Data[0];
                    if (locationEntity.RoadWay == DeviceEntity.RoadWay)
                    {
                        if (locationEntity.IsLock != (short)LocationLockStatus.工作 && string.IsNullOrWhiteSpace(locationEntity.ContainerCode))
                        {
                            NewLocation = locationEntity;
                            this.DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show("库位状态非空闲");
                        }
                    }
                    else
                    {
                        MessageBox.Show("库位巷道与堆垛机巷道不一致");
                    }
                }
                else
                {
                    MessageBox.Show("未找到库位");
                }
            }
        }
    }
}
