using HHECS.Controls.MonitorProps;
using HHECS.Model.Entities;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HHECS.Controls
{
    /// <summary>
    /// BevelMonitorInfo.xaml 的交互逻辑
    /// 坡口站台监控
    /// </summary>
    public partial class BevelMonitorInfo : UserControl
    {
        public Equipment Self { get; set; }
        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(BevelMonitorInfo), new PropertyMetadata(""));
        public BevelMonitorInfo(int maxW, int maxH)
        {
            InitializeComponent();
            this.Width = maxW;
            this.Height = maxH;
            txt_BevelingName.SetBinding(TextBlock.TextProperty, new Binding("ControlName") { Source = this });
        }


        /// <summary>
        /// 赋值定长切割属性
        /// </summary>
        /// <param name="Beveling"></param>
        public void SetBevelingMonitorProps(Equipment Beveling)
        {
            Self = Beveling;

            #region 固定属性赋值
            var OperationModel = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.OperationModel.ToString());
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

            var TotalError = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.TotalError.ToString()).Value;
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
            var RequestMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.RequestMessage.ToString()).Value;
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
            var RequestNumber = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.RequestNumber.ToString()).Value;
            if (RequestNumber != null)
            {
                txt_RequestNumber.Text = RequestNumber;
            }
            var RequestTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.RequestTaskId.ToString()).Value;
            if (RequestTaskId != null)
            {
                txt_RequestTaskId.Text = RequestTaskId;
            }
            var RequestBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.RequestBarcode.ToString()).Value;
            if (RequestBarcode != null)
            {
                txt_RequestBarcode.Text = RequestBarcode;
            }


            var RequestBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.RequestBackup.ToString()).Value;
            if (RequestBackup != null)
            {
                txt_RequestBackup.Text = RequestBackup;
            }
            var ArriveMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.ArriveMessage.ToString()).Value;
            if (ArriveMessage != null)
            {
                txt_ArriveMessage.Text = ArriveMessage;
            }
            var ArriveResult = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.ArriveResult.ToString()).Value;
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
            var ArriveRealAddress = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.ArriveRealAddress.ToString()).Value;
            if (ArriveRealAddress != null)
            {
                txt_ArriveRealAddress.Text = ArriveRealAddress;
            }
            var ArriveAllcationAddress = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.ArriveAllcationAddress.ToString()).Value;
            if (ArriveAllcationAddress != null)
            {
                txt_ArriveAllcationAddress.Text = ArriveAllcationAddress;
            }
            var ArriveTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.ArriveTaskId.ToString()).Value;
            if (ArriveTaskId != null)
            {
                txt_ArriveTaskId.Text = ArriveTaskId;
            }
            var ArriveBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.ArriveBarcode.ToString()).Value;
            if (ArriveBarcode != null)
            {
                txt_ArriveBarcode.Text = ArriveBarcode;
            }

            var ArriveBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.ArriveBackup.ToString()).Value;
            if (ArriveBackup != null)
            {
                txt_ArriveBackup.Text = ArriveBackup;
            }
            var WCSReplyMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyMessage.ToString()).Value;
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
            var WCSReplyNumber = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyNumber.ToString()).Value;
            if (WCSReplyNumber != null)
            {
                txt_WCSReplyNumber.Text = WCSReplyNumber;
            }
            
            var WCSReplyBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyBarcode.ToString()).Value;
            if (WCSReplyBarcode != null)
            {
                txt_WCSReplyBarcode.Text = WCSReplyBarcode;
            }
            var WCSReplyAddress = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyAddress.ToString()).Value;
            if (WCSReplyAddress != null)
            {
                txt_WCSReplyAddress.Text = WCSReplyAddress;
            }
            var WCSReplyProductId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyProductId.ToString()).Value;
            if (WCSReplyProductId != null)
            {
                txt_WCSReplyProductId.Text = WCSReplyProductId;
            }
            var WCSReplyMaterial = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyMaterial.ToString()).Value;
            if (WCSReplyMaterial != null)
            {
                txt_WCSReplyMaterial.Text = WCSReplyMaterial;
            }
            var WCSReplyLength = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyLength.ToString()).Value;
            if (WCSReplyLength != null)
            {
                txt_WCSReplyLength.Text = WCSReplyLength;
            }
            var WCSReplyDiameter = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyDiameter.ToString()).Value;
            if (WCSReplyDiameter != null)
            {
                txt_WCSReplyDiameter.Text = WCSReplyDiameter;
            }
            var WCSReplyThickness = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyThickness.ToString()).Value;
            if (WCSReplyThickness != null)
            {
                txt_WCSReplyThickness.Text = WCSReplyThickness;
            }
            var WCSReplyTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyTaskId.ToString()).Value;
            if (WCSReplyTaskId != null)
            {
                txt_WCSReplyTaskId.Text = WCSReplyTaskId;
            }

            var WCSReplyBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSReplyBackup.ToString()).Value;
            if (WCSReplyBackup != null)
            {
                txt_WCSReplyBackup.Text = WCSReplyBackup;
            }
            var WCSACKMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKMessage.ToString()).Value;
            if (WCSACKMessage != null)
            {
                txt_WCSACKMessage.Text = WCSACKMessage;
            }
            var WCSACKNumber = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKNumber.ToString()).Value;
            if (WCSACKNumber != null)
            {
                txt_WCSACKNumber.Text = WCSACKNumber;
            }
            var WCSACKBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKBarcode.ToString()).Value;
            if (WCSACKBarcode != null)
            {
                txt_WCSACKBarcode.Text = WCSACKBarcode;
            }

            var WCSACKProductId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKProductId.ToString()).Value;
            if (WCSACKProductId != null)
            {
                txt_WCSACKProductId.Text = WCSACKProductId;
            }
            var WCSACKTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKTaskId.ToString()).Value;
            if (WCSACKTaskId != null)
            {
                txt_WCSACKTaskId.Text = WCSACKTaskId;
            }
            var WCSACKBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKBackup.ToString()).Value;
            if (WCSACKBackup != null)
            {
                txt_WCSACKBackup.Text = WCSACKBackup;
            }
            var WCSACKMaterial = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKMaterial.ToString()).Value;
            if (WCSACKMaterial != null)
            {
                txt_WCSACKMaterial.Text = WCSACKMaterial;
            }
            var WCSACKLength = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKLength.ToString()).Value;
            if (WCSACKLength != null)
            {
                txt_WCSACKLength.Text = WCSACKLength;
            }
            var WCSACKDiameter = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKDiameter.ToString()).Value;
            if (WCSACKDiameter != null)
            {
                txt_WCSACKDiameter.Text = WCSACKDiameter;
            }
            var WCSACKThickness = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSACKThickness.ToString()).Value;
            if (WCSACKThickness != null)
            {
                txt_WCSACKThickness.Text = WCSACKThickness;
            }
            var RequestFlip = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.RequestFlip.ToString()).Value;
            if (RequestFlip != null)
            {
                txt_RequestFlip.Foreground = Brushes.Blue;
                switch (ArriveResult)
                {
                    case "0":
                        txt_RequestFlip.Text = "默认";
                        break;
                    case "1":
                        txt_RequestFlip.Text = "自动请求翻转";
                        break;
                    default:
                        txt_RequestFlip.Text = RequestFlip;
                        break;
                }
            }
            var WCSAllowFlip = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == BevelMonitorProps.WCSAllowFlip.ToString()).Value;
            if (WCSAllowFlip != null)
            {
                txt_WCSAllowFlip.Foreground = Brushes.Blue;
                switch (ArriveResult)
                {
                    case "0":
                        txt_WCSAllowFlip.Text = "默认";
                        break;
                    case "6":
                        txt_WCSAllowFlip.Text = "自动允许翻转";
                        break;
                    default:
                        txt_WCSAllowFlip.Text = WCSAllowFlip;
                        break;
                }
            }
            #endregion
        }
    }
}
