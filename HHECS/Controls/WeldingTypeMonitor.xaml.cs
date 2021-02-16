using HHECS.Controls.MonitorProps;
using HHECS.EquipmentExcute.Robot.Enums;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HHECS.Controls
{
    /// <summary>
    /// WeldingTypeMonitor.xaml 的交互逻辑
    /// 焊接机器人监控界面
    /// </summary>
    public partial class WeldingTypeMonitor : UserControl
    {

        public Equipment Self { get; set; }
        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(WeldingTypeMonitor), new PropertyMetadata(""));

        public WeldingTypeMonitor(int maxW, int maxH)
        {
            InitializeComponent();
            this.Width = maxW;
            this.Height = maxH;
            txt_WeldTypeName.SetBinding(TextBlock.TextProperty, new Binding("ControlName") { Source = this });
        }

        /// <summary>
        /// 赋值属性WeldingType
        /// </summary>
        /// <param name="WeldingType"></param>
        public void SetWeldingTypeProps(Equipment WeldingType)
        {
            Self = WeldingType;

            #region  固定属性赋值
            //可输入信号
            var Cmd_Enabled = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Cmd_Enabled.ToString());
            if (Cmd_Enabled != null)
            {
                txt_Cmd_Enabled.Foreground = Brushes.Blue;
                switch (Cmd_Enabled.Value)
                {
                    case "True":
                        txt_Cmd_Enabled.Text = "True";
                        break;
                    case "False":
                        txt_Cmd_Enabled.Text = "False";
                        break;
                    default:
                        txt_Cmd_Enabled.Text = "未知";
                        txt_Cmd_Enabled.Foreground = Brushes.Red;
                        break;
                }
            }

            //程序允行中
            var Prg_running = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Prg_running.ToString());
            if (Prg_running != null)
            {
                this.txt_Prg_running.Foreground = Brushes.Blue;
                switch (Prg_running.Value)
                {
                    case "True":
                        txt_Prg_running.Text = "True";
                        break;
                    case "False":
                        txt_Prg_running.Text = "False";
                        break;
                    default:
                        txt_Prg_running.Text = "未知";
                        txt_Prg_running.Foreground = Brushes.Red;
                        break;
                }
            }

            //启动信号
            var Start_OK = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Start_OK.ToString());
            if (Start_OK != null)
            {
                this.txt_Start_OK.Foreground = Brushes.Blue;
                switch (Start_OK.Value)
                {
                    case "True":
                        txt_Start_OK.Text = "True";
                        break;
                    case "False":
                        txt_Start_OK.Text = "False";
                        break;
                    default:
                        txt_Start_OK.Text = "未知";
                        txt_Start_OK.Foreground = Brushes.Red;
                        break;
                }
            }

            //准备完成
            var Ready_OK = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Ready_OK.ToString());
            if (Ready_OK != null)
            {
                this.txt_Ready_OK.Foreground = Brushes.Blue;
                switch (Ready_OK.Value)
                {
                    case "True":
                        txt_Ready_OK.Text = "True";
                        break;
                    case "False":
                        txt_Ready_OK.Text = "False";
                        break;
                    default:
                        txt_Ready_OK.Text = "未知";
                        txt_Ready_OK.Foreground = Brushes.Red;
                        break;
                }
            }

            //上料准备完成
            var Ready_Load = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Ready_Load.ToString());
            if (Ready_Load != null)
            {
                txt_Ready_Load.Foreground = Brushes.Blue;
                switch (Ready_Load.Value)
                {
                    case "True":
                        txt_Ready_Load.Text = "True";
                        break;
                    case "False":
                        txt_Ready_Load.Text = "False";
                        break;
                    default:
                        txt_Ready_Load.Text = "未知";
                        txt_Ready_Load.Foreground = Brushes.Red;
                        break;
                }
            }


            //绗架允许上料
            var Load_Ready = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Load_Ready.ToString());
            if (Load_Ready != null)
            {
                txt_Load_Ready.Foreground = Brushes.Blue;
                switch (Load_Ready.Value)
                {
                    case "True":
                        txt_Load_Ready.Text = "True";
                        break;
                    case "False":
                        txt_Load_Ready.Text = "False";
                        txt_Load_Ready.Foreground = Brushes.Red;
                        break;
                    default:
                        txt_Load_Ready.Text = "未知";
                        txt_Load_Ready.Foreground = Brushes.Red;
                        break;
                }
            }

            //行架上料中
            var Allow_Load = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Allow_Load.ToString());
            if (Allow_Load != null)
            {
                txt_Allow_Load.Foreground = Brushes.Blue;
                switch (Allow_Load.Value)
                {
                    case "True":
                        txt_Allow_Load.Text = "True";
                        break;
                    case "False":
                        txt_Allow_Load.Text = "False";
                        txt_Allow_Load.Foreground = Brushes.Red;
                        break;
                    default:
                        txt_Allow_Load.Text = "未知";
                        txt_Allow_Load.Foreground = Brushes.Red;
                        break;
                }
            }

            //上料完成
            var Load_Compelte = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Load_Compelte.ToString());
            if (Load_Compelte != null)
            {
                txt_Load_Compelte.Foreground = Brushes.Blue;
                switch (Load_Compelte.Value)
                {
                    case "True":
                        txt_Load_Compelte.Text = "True";
                        break;
                    case "False":
                        txt_Load_Compelte.Text = "False";
                        break;
                    default:
                        txt_Load_Compelte.Text = "未知";
                        txt_Load_Compelte.Foreground = Brushes.Red;
                        break;
                }
            }

            //请求上料
            var Request_Load = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Request_Load.ToString());
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
                        txt_Request_Load.Foreground = Brushes.Red;
                        break;
                    default:
                        txt_Request_Load.Text = "未知";
                        txt_Request_Load.Foreground = Brushes.Red;
                        break;
                }
            }


            //确认上料完成
            var CompleteLoad_OK = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.CompleteLoad_OK.ToString());
            if (CompleteLoad_OK != null)
            {
                txt_CompleteLoad_OK.Foreground = Brushes.Blue;
                switch (CompleteLoad_OK.Value)
                {
                    case "True":
                        txt_CompleteLoad_OK.Text = "True";
                        break;
                    case "False":
                        txt_CompleteLoad_OK.Text = "False";
                        txt_CompleteLoad_OK.Foreground = Brushes.Red;
                        break;
                    default:
                        txt_CompleteLoad_OK.Text = "未知";
                        txt_CompleteLoad_OK.Foreground = Brushes.Red;
                        break;
                }
            }

            //下料准备完成
            var Ready_Blank = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Ready_Blank.ToString());
            if (Ready_Blank != null)
            {
                txt_Ready_Blank.Foreground = Brushes.Blue;
                switch (Ready_Blank.Value)
                {
                    case "True":
                        txt_Ready_Blank.Text = "True";
                        break;
                    case "False":
                        txt_Ready_Blank.Text = "False";
                        break;
                    default:
                        txt_Ready_Blank.Text = "未知";
                        txt_Ready_Blank.Foreground = Brushes.Red;
                        break;
                }
            }

            //行架允许下料
            var Blank_Ready = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Blank_Ready.ToString());
            if (Blank_Ready != null)
            {
                txt_Blank_Ready.Foreground = Brushes.Blue;
                switch (Blank_Ready.Value)
                {
                    case "True":
                        txt_Blank_Ready.Text = "True";
                        break;
                    case "False":
                        txt_Blank_Ready.Text = "False";
                        break;
                    default:
                        txt_Blank_Ready.Text = "未知";
                        txt_Blank_Ready.Foreground = Brushes.Red;
                        break;
                }
            }

            //行架下料中
            var Allow_Blank = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Allow_Blank.ToString());
            if (Allow_Blank != null)
            {
                txt_Allow_Blank.Foreground = Brushes.Blue;
                switch (Allow_Blank.Value)
                {
                    case "True":
                        txt_Allow_Blank.Text = "True";
                        break;
                    case "False":
                        txt_Allow_Blank.Text = "False";
                        break;
                    default:
                        txt_Allow_Blank.Text = "未知";
                        txt_Allow_Blank.Foreground = Brushes.Red;
                        break;
                }
            }

            //下料完成
            var Blank_Complete = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Blank_Complete.ToString());
            if (Blank_Complete != null)
            {
                txt_Allow_Blank.Foreground = Brushes.Blue;
                switch (Blank_Complete.Value)
                {
                    case "True":
                        txt_Blank_Complete.Text = "True";
                        break;
                    case "False":
                        txt_Blank_Complete.Text = "False";
                        break;
                    default:
                        txt_Blank_Complete.Text = "未知";
                        txt_Blank_Complete.Foreground = Brushes.Red;
                        break;
                }
            }

            //请求下料
            var Request_Blank = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Request_Blank.ToString());
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

            //确认下料完成
            var CompleteBlank_OK = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.CompleteBlank_OK.ToString());
            if (CompleteBlank_OK != null)
            {
                txt_CompleteBlank_OK.Foreground = Brushes.Blue;
                switch (CompleteBlank_OK.Value)
                {
                    case "True":
                        txt_CompleteBlank_OK.Text = "True";
                        break;
                    case "False":
                        txt_CompleteBlank_OK.Text = "False";
                        break;
                    default:
                        txt_CompleteBlank_OK.Text = "未知";
                        txt_CompleteBlank_OK.Foreground = Brushes.Red;
                        break;
                }
            }

            //工序追踪id
            var Step_Trace_Id = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Step_Trace_Id.ToString());
            if (Step_Trace_Id != null)
            {
                this.txt_Step_Trace_Id.Foreground = Brushes.Blue;
                this.txt_Step_Trace_Id.Text = Step_Trace_Id.Value;
            }

            //工件数量
            var Num = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Num.ToString());
            if (Num != null)
            {
                this.txt_Number.Foreground = Brushes.Blue;
                this.txt_Number.Text = Num.Value;
            }

            //任务完成
            var Task_OK = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Task_OK.ToString());
            if (Task_OK != null)
            {
                this.txt_Task_OK.Foreground = Brushes.Blue;
                switch (Task_OK.Value)
                {
                    case "True":
                        txt_Task_OK.Text = "True";
                        break;
                    case "False":
                        txt_Task_OK.Text = "False";
                        txt_Task_OK.Foreground = Brushes.Red;
                        break;
                    default:
                        txt_Task_OK.Text = "未知";
                        txt_Task_OK.Foreground = Brushes.Red;
                        break;
                }
            }


            //暂停中
            var Motion_held = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Motion_held.ToString());
            if (Motion_held != null)
            {
                this.txt_Motion_held.Foreground = Brushes.Blue;
                switch (Motion_held.Value)
                {
                    case "True":
                        txt_Motion_held.Text = "True";
                        break;
                    case "False":
                        txt_Motion_held.Text = "False";
                        break;
                    default:
                        txt_Motion_held.Text = "未知";
                        txt_Motion_held.Foreground = Brushes.Red;
                        break;
                }
            }

            var Fault = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Fault.ToString());
            if (Fault != null)
            {
                this.txt_Fault.Foreground = Brushes.Blue;
                switch (Fault.Value)
                {
                    case "True":
                        txt_Fault.Text = "True";
                        break;
                    case "False":
                        txt_Fault.Text = "False";
                        break;
                    default:
                        txt_Fault.Text = "未知";
                        txt_Fault.Foreground = Brushes.Red;
                        break;
                }
            }

            var Err = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.Err.ToString());
            if (Err != null)
            {
                this.txt_Err.Foreground = Brushes.Blue;
                switch (Err.Value)
                {
                    case "True":
                        txt_Err.Text = "True";
                        break;
                    case "False":
                        txt_Err.Text = "False";
                        txt_Err.Foreground = Brushes.Red;
                        break;
                    default:
                        txt_Err.Text = "未知";
                        txt_Err.Foreground = Brushes.Red;
                        break;
                }
            }

            #endregion

            var Type = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.TYPE_Feedback.ToString());
            if (Type != null)
            {
                txt_TYPE_Feedback.Foreground = Brushes.Blue;
                txt_TYPE_Feedback.Text = Type.Value;
            }

            var ManualSign = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == WeldingTypeMonitorProps.ManualSign.ToString());
            if (ManualSign != null)
            {
                txt_TypeFlag.Foreground = Brushes.Blue;
                if (ManualSign.Value == ((int)CreateTypeFlag.手动任务).ToString() && Step_Trace_Id.Value == "0")
                {
                    txt_TypeFlag.Text = "手动任务";
                }
                else if (ManualSign.Value != ((int)CreateTypeFlag.手动任务).ToString() && Step_Trace_Id.Value != "0")
                {
                    txt_TypeFlag.Text = "自动任务";
                }
                else
                {
                    txt_TypeFlag.Text = "未知任务";
                }
            }
        }
    }
}
