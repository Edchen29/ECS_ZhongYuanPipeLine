using HHECS.Bll;
using HHECS.Model.Entities;
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
    /// WinMoveTruss.xaml 的交互逻辑
    /// </summary>
    public partial class WinMoveTruss : Window
    {
        public Equipment DeviceEntity { get; set; }

        public string stationId = string.Empty;
        public WinMoveTruss()
        {
            InitializeComponent();

            InitControl();
        }

        private void InitControl()
        {

        }

        private void btn_Issue_Click(object sender, RoutedEventArgs e)
        {
            string code = null;
            code = DeviceEntity.Code;
            if (cbx_StationId.SelectedValue?.ToString() == null)
            {
                MessageBox.Show("请选择需要到达目的站台");
                return;
            }
            else
            {
                stationId = cbx_StationId.SelectedValue.ToString();
                this.DialogResult = true;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var temp = AppSession.BllService.GetDictWithDetailsAndTrussCode(DeviceEntity.Code);
            var stations = AppSession.Dal.GetCommonModelByCondition<Station>($"where trussPutStationId <> 0 and  transportNormal = '{DeviceEntity.Code}'");
            if (stations.Success)
            {
                //stations.Data.Distinct(t => t.)
                //var dictDetails = stations.Data.ToDictionary(t => t.TrussPutStationId, i => i.Name);
                var dictDetails = stations.Data;
                cbx_StationId.ItemsSource = dictDetails;
                cbx_StationId.DisplayMemberPath = "Name";
                cbx_StationId.SelectedValuePath = "TrussPutStationId";
            }
            else
            {
                MessageBox.Show($"错误{stations.Msg}");
                return;
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
