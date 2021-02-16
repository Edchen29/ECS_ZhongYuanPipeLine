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
    /// MachiningTypeMonitor.xaml 的交互逻辑
    /// 机加设备监控
    /// </summary>
    public partial class MachiningTypeMonitor : UserControl
    {

        public Equipment Self { get; set; }
        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(MachiningTypeMonitor), new PropertyMetadata(""));

        public MachiningTypeMonitor(int maxW, int maxH)
        {
            InitializeComponent();
            this.Width = maxW;
            this.Height = maxH;
            txt_MachiningTypeName.SetBinding(TextBlock.TextProperty, new Binding("ControlName") { Source = this });
        }

        public void SetMachiningTypeProps(Equipment MachiningType)
        {
            Self = MachiningType;

            var Abnormal_1 = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.Abnormal_1.ToString());
            if (Abnormal_1 != null)
            {
                txt_Abnormal_1.Foreground = Brushes.Blue;
                txt_Abnormal_1.Text = Abnormal_1.Value;
            }

            var Abnormal_2 = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.Abnormal_2.ToString());
            if (Abnormal_2 != null)
            {
                txt_Abnormal_2.Foreground = Brushes.Blue;
                txt_Abnormal_2.Text = Abnormal_2.Value;
            }

            var WCS_Step_Trace_Id = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.WCS_Step_Trace_Id.ToString());
            if (WCS_Step_Trace_Id != null)
            {
                txt_WCS_Step_Trace_Id.Foreground = Brushes.Blue;
                txt_WCS_Step_Trace_Id.Text = WCS_Step_Trace_Id.Value;
            }

            var Request_Wroking = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.Request_Wroking.ToString());
            if (Request_Wroking != null)
            {
                txt_Request_Wroking.Foreground = Brushes.Blue;
                switch (Request_Wroking.Value)
                {
                    case "True":
                        txt_Request_Wroking.Text = "True";
                        break;
                    case "False":
                        txt_Request_Wroking.Text = "False";
                        break;
                    default:
                        txt_Request_Wroking.Text = "未知";
                        txt_Request_Wroking.Foreground = Brushes.Red;
                        break;
                }
            }


            var WCS_Allow_Load = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.WCS_Allow_Load.ToString());
            if (WCS_Allow_Load != null)
            {
                txt_WCS_Allow_Load.Foreground = Brushes.Blue;
                switch (WCS_Allow_Load.Value)
                {
                    case "True":
                        txt_WCS_Allow_Load.Text = "True";
                        break;
                    case "False":
                        txt_WCS_Allow_Load.Text = "False";
                        break;
                    default:
                        txt_WCS_Allow_Load.Text = "未知";
                        txt_WCS_Allow_Load.Foreground = Brushes.Red;
                        break;
                }
            }

            var WCS_Wroking = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.WCS_Wroking.ToString());
            if (WCS_Wroking != null)
            {
                txt_WCS_Wroking.Foreground = Brushes.Blue;
                switch (WCS_Wroking.Value)
                {
                    case "True":
                        txt_WCS_Wroking.Text = "True";
                        break;
                    case "False":
                        txt_WCS_Wroking.Text = "False";
                        break;
                    default:
                        txt_WCS_Wroking.Text = "未知";
                        txt_WCS_Wroking.Foreground = Brushes.Red;
                        break;
                }
            }

            var Step_Trace_ID = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.Step_Trace_ID.ToString());
            if (Step_Trace_ID != null)
            {
                txt_Request_Load.Foreground = Brushes.Blue;
                txt_Step_Trace_ID.Text = Step_Trace_ID.Value;
            }

            var Request_Load = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.Request_Load.ToString());
            if (Request_Load != null)
            {
                txt_Request_Load.Foreground = Brushes.Blue;
                switch (Request_Load.Value)
                {
                    case "True":
                        txt_Request_Load.Text = "True";
                        break;
                    case "False":
                        txt_Request_Load.Text = "False";
                        break;
                    default:
                        txt_Request_Load.Text = "未知";
                        txt_Request_Load.Foreground = Brushes.Red;
                        break;
                }
            }

            var Task_OK = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.Task_OK.ToString());
            if (Task_OK != null)
            {
                txt_Task_OK.Foreground = Brushes.Blue;
                txt_Task_OK.Text = Task_OK.Value;
            }

            var Request_Blank = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == MachiningTypeMonitorProps.Request_Blank.ToString());
            if (Request_Blank != null)
            {
                txt_Request_Blank.Foreground = Brushes.Blue;
                switch (Request_Blank.Value)
                {
                    case "True":
                        txt_Request_Blank.Text = "True";
                        break;
                    case "False":
                        txt_Request_Blank.Text = "False";
                        break;
                    default:
                        txt_Request_Blank.Text = "未知";
                        txt_Request_Blank.Foreground = Brushes.Red;
                        break;
                }
            }

        }
    }
}
