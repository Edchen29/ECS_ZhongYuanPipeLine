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
    /// CuttingMonitorProps.xaml 的交互逻辑
    /// </summary>
    public partial class CuttingMonitorInfo : UserControl
    {
        public Equipment Self { get; set; }
        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(CuttingMonitorInfo), new PropertyMetadata(""));

        public CuttingMonitorInfo(int maxW, int maxH)
        {
            InitializeComponent();
            this.Width = maxW;
            this.Height = maxH;
            txt_CuttingName.SetBinding(TextBlock.TextProperty, new Binding("ControlName") { Source = this });
        }

        /// <summary>
        /// 赋值定长切割属性
        /// </summary>
        /// <param name="Cutting"></param>
        public void SetCuttingMonitorProps(Equipment Cutting)
        {
            Self = Cutting;

            #region 固定属性赋值
            var OperationModel = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.OperationModel.ToString());
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

            var TotalError = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.TotalError.ToString()).Value;
            if (TotalError != null)
            {
                txt_TotalError.Foreground = Brushes.Blue;
                switch (TotalError)
                {
                    case "0":
                        txt_TotalError.Text = "无故障";
                        break;
                    case "":
                        txt_TotalError.Text = "故障";
                        txt_TotalError.Foreground = Brushes.Red;
                        break;
                    default:
                        txt_TotalError.Text = "未知";
                        txt_TotalError.Foreground = Brushes.Red;
                        break;
                }
            }
            var RequestMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.RequestMessage.ToString()).Value;
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
            var RequestNumber = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.RequestNumber.ToString()).Value;
            if (RequestNumber != null)
            {
                txt_RequestNumber.Text = RequestNumber;
            }
            var RequestTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.RequestTaskId.ToString()).Value;
            if (RequestTaskId != null)
            {
                txt_RequestTaskId.Text = RequestTaskId;
            }
            var RequestBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.RequestBarcode.ToString()).Value;
            if (RequestBarcode != null)
            {
                txt_RequestBarcode.Text = RequestBarcode;
            }
            //var RequestProductId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.RequestProductId.ToString()).Value;
            //if (RequestProductId != null)
            //{
            //    txt_RequestProductId.Text = RequestProductId;
            //}

            var RequestBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.RequestBackup.ToString()).Value;
            if (RequestBackup != null)
            {
                txt_RequestBackup.Text = RequestBackup;
            }
            var ArriveMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.ArriveMessage.ToString()).Value;
            if (ArriveMessage != null)
            {
                txt_ArriveMessage.Text = ArriveMessage;
            }
            var ArriveResult = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.ArriveResult.ToString()).Value;
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
            var ArriveRealAddress = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.ArriveRealAddress.ToString()).Value;
            if (ArriveRealAddress != null)
            {
                txt_ArriveRealAddress.Text = ArriveRealAddress;
            }
            var ArriveBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.ArriveBackup.ToString()).Value;
            if (ArriveBackup != null)
            {
                txt_ArriveBackup.Text = ArriveBackup;
            }
            var WCSReplyMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSReplyMessage.ToString()).Value;
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
            var WCSReplyNumber = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSReplyNumber.ToString()).Value;
            if (WCSReplyNumber != null)
            {
                txt_WCSReplyNumber.Text = WCSReplyNumber;
            }
            var WCSReplyBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSReplyBarcode.ToString()).Value;
            if (WCSReplyBarcode != null)
            {
                txt_WCSReplyBarcode.Text = WCSReplyBarcode;
            }
            var WCSReplyAddress = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSReplyAddress.ToString()).Value;
            if (WCSReplyAddress != null)
            {
                txt_WCSReplyAddress.Text = WCSReplyAddress;
            }
            var WCSReplyProductId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSReplyProductId.ToString()).Value;
            if (WCSReplyProductId != null)
            {
                txt_WCSReplyProductId.Text = WCSReplyProductId;
            }
            var WCSReplyTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSReplyTaskId.ToString()).Value;
            if (WCSReplyTaskId != null)
            {
                txt_WCSReplyTaskId.Text = WCSReplyTaskId;
            }

            var WCSReplyBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSReplyBackup.ToString()).Value;
            if (WCSReplyBackup != null)
            {
                txt_WCSReplyBackup.Text = WCSReplyBackup;
            }
            var WCSACKMessage = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSACKMessage.ToString()).Value;
            if (WCSACKMessage != null)
            {
                txt_WCSACKMessage.Text = WCSACKMessage;
            }
            var WCSACKNumber = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSACKNumber.ToString()).Value;
            if (WCSACKNumber != null)
            {
                //.Text = WCSACKNumber;
            }
            var WCSACKBarcode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSACKBarcode.ToString()).Value;
            if (WCSACKBarcode != null)
            {
                txt_WCSACKBarcode.Text = WCSACKBarcode;
            }

            var WCSACKProductId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSACKProductId.ToString()).Value;
            if (WCSACKProductId != null)
            {
                txt_WCSACKProductId.Text = WCSACKProductId;
            }
            var WCSACKTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSACKTaskId.ToString()).Value;
            if (WCSACKTaskId != null)
            {
                txt_WCSACKTaskId.Text = WCSACKTaskId;
            }
            var WCSACKBackup = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSACKBackup.ToString()).Value;
            if (WCSACKBackup != null)
            {
                txt_WCSACKBackup.Text = WCSACKBackup;
            }
            var CutTaskId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSCutTaskId.ToString()).Value;
            if (CutTaskId != null)
            {
                txt_CutTaskId.Text = CutTaskId;
            }
            var RequestCut = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.RequestCut.ToString()).Value;
            if (RequestCut != null)
            {
                txt_Request_Cut.Foreground = Brushes.Blue;
                switch (ArriveResult)
                {
                    case "0":
                        txt_Request_Cut.Text = "默认";
                        break;
                    case "1":
                        txt_Request_Cut.Text = "自动请求切割";
                        break;
                    default:
                        txt_Request_Cut.Text = RequestCut;
                        break;
                }
            }
            var WCSAllowCut = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CuttingMonitorProps.WCSAllowCut.ToString()).Value;
            if (WCSAllowCut != null)
            {
                txt_WCS_Allow_Cut.Foreground = Brushes.Blue;
                switch (ArriveResult)
                {
                    case "0":
                        txt_WCS_Allow_Cut.Text = "默认";
                        break;
                    case "6":
                        txt_WCS_Allow_Cut.Text = "自动允许切割";
                        break;
                    case "7":
                        txt_WCS_Allow_Cut.Text = "回复结束切割";
                        break;
                    default:
                        txt_WCS_Allow_Cut.Text = WCSAllowCut;
                        break;
                }
            }
            #endregion
        }
    }
}
