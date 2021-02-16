using HHECS.Controls.Model;
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
    /// RobotForAeesmblyMonitor.xaml 的交互逻辑
    /// 组焊机器人监控界面
    /// </summary>
    public partial class AeesmblyMonitor : UserControl
    {
        public Equipment Self { get; set; }
        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(AeesmblyMonitor), new PropertyMetadata(""));
        public AeesmblyMonitor(int maxW, int maxH)
        {
            InitializeComponent();
            this.Width = maxW;
            this.Height = maxH;
            txt_RobotForAeesmblyName.SetBinding(TextBlock.TextProperty, new Binding("ControlName") { Source = this });
        }

        /// <summary>
        /// 赋值属性RobotForAeesmbly
        /// </summary>
        /// <param name="RobotForAeesmbly"></param>
        public void SetRobotForAeesmblyProps(Equipment RobotForAeesmbly)
        {
            Self = RobotForAeesmbly;
            #region 固定属性赋值
            var OperationModel = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.OperationModel.ToString());
            if (OperationModel != null)
            {
                txt_OperationModel.Foreground = Brushes.Blue;
                switch (OperationModel.Value)
                {
                    case "1":
                        txt_OperationModel.Text = "维修";
                        break;
                    case "2":
                        txt_OperationModel.Text = "手动";
                        break;
                    case "3":
                        txt_OperationModel.Text = "机载操作";
                        break;
                    case "4":
                        txt_OperationModel.Text = "单机自动";
                        break;
                    case "5":
                        txt_OperationModel.Text = "联机";
                        break;
                    default:
                        txt_OperationModel.Text = "未知";
                        txt_OperationModel.Foreground = Brushes.Red;
                        break;
                }
            }

            var TotalError = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.TotalError.ToString()).Value;
            if (TotalError != null)
            {
                txt_TotalError.Foreground = Brushes.Blue;
                switch (TotalError)
                {
                    case "0":
                        txt_ArriveResult.Text = "无故障";
                        break;
                    case "":
                        txt_ArriveResult.Text = "故障";
                        txt_ArriveResult.Foreground = Brushes.Red;
                        break;
                    default:
                        txt_ArriveResult.Text = "未知";
                        txt_ArriveResult.Foreground = Brushes.Red;
                        break;
                }
            }
            var RequestMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.RequestMessage.ToString()).Value;
            if (RequestMessage != null)
            {
                txt_RequestMessage.Foreground = Brushes.Blue;
                switch (RequestMessage)
                {
                    case "1":
                        txt_RequestMessage.Text = "请求下料";
                        break;
                    case "2":
                        txt_RequestMessage.Text = "手动请求";
                        break;
                    case "0":
                        txt_RequestMessage.Text = "无信号";
                        break;
                    default:
                        txt_RequestMessage.Text = "未定义";
                        txt_RequestMessage.Foreground = Brushes.Red;
                        break;
                }
            }
            var RequestNumber = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.RequestNumber.ToString()).Value;
            if (RequestNumber != null)
            {
                txt_RequestNumber.Text = RequestNumber;
            }
            var RequestTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.RequestTaskId.ToString()).Value;
            if (RequestTaskId != null)
            {
                txt_RequestTaskId.Text = RequestTaskId;
            }
            var RequestBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.RequestBarcode.ToString()).Value;
            if (RequestBarcode != null)
            {
                txt_RequestBarcode.Text = RequestBarcode;
            }


            var RequestBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.RequestBackup.ToString()).Value;
            if (RequestBackup != null)
            {
                txt_RequestBackup.Text = RequestBackup;
            }
            var ArriveMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.ArriveMessage.ToString()).Value;
            if (ArriveMessage != null)
            {
                txt_ArriveMessage.Text = ArriveMessage;
            }
            var ArriveResult = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.ArriveResult.ToString()).Value;
            if (ArriveResult != null)
            {
                txt_ArriveResult.Foreground = Brushes.Blue;
                switch (ArriveResult)
                {
                    case "1":
                        txt_ArriveResult.Text = "已到达";
                        break;
                    case "2":
                        txt_ArriveResult.Text = "未到达";
                        break;
                    default:
                        txt_ArriveResult.Text = "未定义";
                        break;
                }
            }
            var ArriveRealAddress = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.ArriveRealAddress.ToString()).Value;
            if (ArriveRealAddress != null)
            {
                txt_ArriveRealAddress.Text = ArriveRealAddress;
            }
            var ArriveAllcationAddress = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.ArriveAllcationAddress.ToString()).Value;
            if (ArriveAllcationAddress != null)
            {
                txt_ArriveAllcationAddress.Text = ArriveAllcationAddress;
            }
            var ArriveTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.ArriveTaskId.ToString()).Value;
            if (ArriveTaskId != null)
            {
                txt_ArriveTaskId.Text = ArriveTaskId;
            }
            var ArriveBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.ArriveBarcode.ToString()).Value;
            if (ArriveBarcode != null)
            {
                txt_ArriveBarcode.Text = ArriveBarcode;
            }

            var ArriveBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.ArriveBackup.ToString()).Value;
            if (ArriveBackup != null)
            {
                txt_ArriveBackup.Text = ArriveBackup;
            }
            var WCSReplyMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyMessage.ToString()).Value;
            if (WCSReplyMessage != null)
            {
                txt_WCSReplyMessage.Foreground = Brushes.Blue;
                switch (WCSReplyMessage)
                {
                    case "6":
                        txt_WCSReplyMessage.Text = "响应请求";
                        break;
                    case "":
                        break;
                    default:
                        txt_WCSReplyMessage.Text = WCSReplyMessage;
                        break;
                }
            }
            var WCSReplyNumber = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyNumber.ToString()).Value;
            if (WCSReplyNumber != null)
            {
                txt_WCSReplyNumber.Text = WCSReplyNumber;
            }

            var WCSReplyBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyBarcode.ToString()).Value;
            if (WCSReplyBarcode != null)
            {
                txt_WCSReplyBarcode.Text = WCSReplyBarcode;
            }
            var WCSReplyAddress = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyAddress.ToString()).Value;
            if (WCSReplyAddress != null)
            {
                txt_WCSReplyAddress.Text = WCSReplyAddress;
            }
            var WCSReplyProductId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyProductId.ToString()).Value;
            if (WCSReplyProductId != null)
            {
                txt_WCSReplyProductId.Text = WCSReplyProductId;
            }
            var WCSReplyMaterial = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyMaterial.ToString()).Value;
            if (WCSReplyMaterial != null)
            {
                txt_WCSReplyMaterial.Text = WCSReplyMaterial;
            }
            var WCSReplyLength = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyLength.ToString()).Value;
            if (WCSReplyLength != null)
            {
                txt_WCSReplyLength.Text = WCSReplyLength;
            }
            var WCSReplyDiameter = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyDiameter.ToString()).Value;
            if (WCSReplyDiameter != null)
            {
                txt_WCSReplyDiameter.Text = WCSReplyDiameter;
            }
            var WCSReplyThickness = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyThickness.ToString()).Value;
            if (WCSReplyThickness != null)
            {
                txt_WCSReplyThickness.Text = WCSReplyThickness;
            }
            var WCSReplyTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyTaskId.ToString()).Value;
            if (WCSReplyTaskId != null)
            {
                txt_WCSReplyTaskId.Text = WCSReplyTaskId;
            }

            var WCSReplyBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSReplyBackup.ToString()).Value;
            if (WCSReplyBackup != null)
            {
                txt_WCSReplyBackup.Text = WCSReplyBackup;
            }
            var WCSACKMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKMessage.ToString()).Value;
            if (WCSACKMessage != null)
            {
                txt_WCSACKMessage.Text = WCSACKMessage;
            }
            var WCSACKNumber = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKNumber.ToString()).Value;
            if (WCSACKNumber != null)
            {
                txt_WCSACKNumber.Text = WCSACKNumber;
            }
            var WCSACKBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKBarcode.ToString()).Value;
            if (WCSACKBarcode != null)
            {
                txt_WCSACKBarcode.Text = WCSACKBarcode;
            }

            var WCSACKProductId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKProductId.ToString()).Value;
            if (WCSACKProductId != null)
            {
                txt_WCSACKProductId.Text = WCSACKProductId;
            }
            var WCSACKTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKTaskId.ToString()).Value;
            if (WCSACKTaskId != null)
            {
                txt_WCSACKTaskId.Text = WCSACKTaskId;
            }
            var WCSACKBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKBackup.ToString()).Value;
            if (WCSACKBackup != null)
            {
                txt_WCSACKBackup.Text = WCSACKBackup;
            }
            var WCSACKMaterial = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKMaterial.ToString()).Value;
            if (WCSACKMaterial != null)
            {
                txt_WCSACKMaterial.Text = WCSACKMaterial;
            }
            var WCSACKLength = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKLength.ToString()).Value;
            if (WCSACKLength != null)
            {
                txt_WCSACKLength.Text = WCSACKLength;
            }
            var WCSACKDiameter = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKDiameter.ToString()).Value;
            if (WCSACKDiameter != null)
            {
                txt_WCSACKDiameter.Text = WCSACKDiameter;
            }
            var WCSACKThickness = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == RobotForAeesmblyMonitorProps.WCSACKThickness.ToString()).Value;
            if (WCSACKThickness != null)
            {
                txt_WCSACKThickness.Text = WCSACKThickness;
            }
            #endregion
        }
    }
}

