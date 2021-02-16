using HHECS.Bll;
using HHECS.Model.BllModel;
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

namespace HHECS.View.LocationInfo
{
    /// <summary>
    /// WinLocationAdd.xaml 的交互逻辑
    /// </summary>
    public partial class WinLocationAdd : BaseWindow
    {


        public LocationAddVM LocationVM
        {
            get { return (LocationAddVM)GetValue(LocationVMProperty); }
            set { SetValue(LocationVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LocationVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocationVMProperty =
            DependencyProperty.Register("LocationVM", typeof(LocationAddVM), typeof(WinLocationAdd), new PropertyMetadata(null));


        public WinLocationAdd()
        {
            InitializeComponent();
            LocationVM = new LocationAddVM();
            LocationVM.WarehouseCode = AppSession.WarehouseCode;
            GridMain.DataContext = LocationVM;
            var dictResult = AppSession.BllService.GetDictWithDetails("LocationType");
            if (!dictResult.Success)
            {
                MessageBox.Show($"获取库位类型错误：{dictResult.Msg}");
            }
            else
            {
                CbxLocationTypes.ItemsSource = dictResult.Data.DictDetails.ToDictionary(t => t.Name, t => t.Value);
                CbxLocationTypes.SelectedValuePath = "Value";
                CbxLocationTypes.DisplayMemberPath = "Key";
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!LocationVM.Validate().Success)
            {
                MessageBox.Show("堆垛机标识、行索引、列、层、行必须大于0");
            }
            else
            {
                BllResult result = AppSession.LocationService.CreateLocations(LocationVM.Prefix, LocationVM.Connector, LocationVM.SRMCode, LocationVM.Type, LocationVM.RowIndex1, LocationVM.RowIndex2, LocationVM.TotalRows, LocationVM.TotalColumns, LocationVM.TotalLayers, LocationVM.DestinationArea, LocationVM.WarehouseCode, App.User.UserCode);

                MessageBox.Show(result.Msg);
            }

        }

        private void BtnConfirmSingle_Click(object sender, RoutedEventArgs e)
        {
            if (!LocationVM.Validate().Success)
            {
                MessageBox.Show("堆垛机标识、行索引、列、层、行必须大于0");
            }
            else
            {
                BllResult result = AppSession.LocationService.CreateLocation(LocationVM.Prefix, LocationVM.Connector, LocationVM.SRMCode, LocationVM.Type, LocationVM.RowIndex1, LocationVM.RowIndex2, LocationVM.TotalRows, LocationVM.TotalColumns, LocationVM.TotalLayers, LocationVM.DestinationArea, LocationVM.WarehouseCode,App.User.UserCode);
                MessageBox.Show(result.Msg);
            }
        }
    }
}
