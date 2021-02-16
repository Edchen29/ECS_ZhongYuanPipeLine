using HHECS.Controls.MonitorProps;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HHECS.Controls
{
    /// <summary>
    /// DownPlatform.xaml 的交互逻辑
    /// </summary>
    public partial class StationForFinishedMonitor : UserControl
    {

        public Equipment Self { get; set; }

        SolidColorBrush Colors_Red = new SolidColorBrush(Colors.Red);
        SolidColorBrush Colors_Green = new SolidColorBrush(Colors.Green);
        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(StationForFinishedMonitor), new PropertyMetadata(""));
        public StationForFinishedMonitor()
        {
            InitializeComponent();
        }


        public StationForFinishedMonitor(int maxW, int maxH)
        {
            InitializeComponent();
            this.Width = maxW;
            this.Height = maxH;
            txt_DownPlatform.SetBinding(TextBlock.TextProperty, new Binding("ControlName") { Source = this });
        }

        public void SetStationForFinishedProps(Equipment StationForFinishedType, short? layer, short? line)
        {

            Self = StationForFinishedType;
            var AGV_Arrive_WCS = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationForFinishedMonitorProps.AGV_Arrive_WCS.ToString());
            if (AGV_Arrive_WCS != null)
            {
                if (AGV_Arrive_WCS.Value == true.ToString())
                {
                    ellipse_arrive.Fill = Colors_Green;
                }
                else
                {
                    ellipse_arrive.Fill = Colors_Red;
                }
                txt_AGV_Arrive_WCS.Foreground = Brushes.Blue;
                txt_AGV_Arrive_WCS.Text = AGV_Arrive_WCS.Value;
            }

            var AGV_Leave_WCS = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == StationForFinishedMonitorProps.AGV_Leave_WCS.ToString());
            if (AGV_Leave_WCS != null)
            {
                if (AGV_Leave_WCS.Value == true.ToString())
                {
                    ellipse_leave.Fill = Colors_Green;
                }
                else
                {
                    ellipse_leave.Fill = Colors_Red;
                }
                txt_AGV_Leave_WCS.Foreground = Brushes.Blue;
                txt_AGV_Leave_WCS.Text = AGV_Leave_WCS.Value;
            }


            if (layer > 2 && line >= 1)
            {
                this.txt_layer_line.Text = $"该{StationForFinishedType.Name}站台已满";
            }
            else
            {
                this.txt_layer_line.Text = $"{layer}层-{line}列";
            }
        }
    }
}
