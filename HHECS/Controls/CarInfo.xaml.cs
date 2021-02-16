using HHECS.Controls.MonitorProps;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HHECS.Controls
{
    /// <summary>
    /// Car.xaml 的交互逻辑
    /// </summary>
    public partial class CarInfo : UserControl
    {
        /// <summary>
        ///  复位、开启、删除、切换、设置间距
        /// </summary>
        public event Func<Equipment, BllResult> CommandReset;
        public event Func<Equipment, BllResult> CommandDelete;
        public event Func<Equipment, BllResult> CommandControlMode;
        public event Func<Equipment, BllResult> CommandSetGrap;
        public event Func<Equipment, BllResult> CommandFabricSpeed;
        public event Func<Equipment, BllResult> CommandFabricDistance;
        public event Func<Equipment, BllResult> CommandUpdateLocation;
        public event Func<Equipment, BllResult> CommandCharge;
        public event Func<Equipment, BllResult> CommandReSend;
        public event Func<Equipment, BllResult> CommandSetPosition;


        /// <summary>
        /// 充电桩socket返回的字符
        /// </summary>
        private string chargingResult = string.Empty;
        /// <summary>
        /// 小车名称
        /// </summary>
        private string carName = string.Empty;
        /// <summary>
        /// 穿梭车任务类型
        /// </summary>
        private Dictionary<string, string> carActionTypeList;
        /// <summary>
        /// 穿梭车位置
        /// </summary>
        private Dictionary<string, string> carPositionList;
        /// <summary>
        /// 小车状态
        /// </summary>
        private Dictionary<string, string> carStatusList;
        /// <summary>
        /// 小车状态
        /// </summary>
        private Dictionary<string, string> controlModeList;

        /// <summary>
        /// 小车PLC数据
        /// </summary>
        public Equipment Self { get; set; }
        ////倒料机PLC数据
        //Equipment feeder = null;
        /// <summary>
        /// 错误提示文本
        /// </summary>
        string alarmText = "";
        /// <summary>
        /// 控件显示名
        /// </summary>
        public string ControlName
        {
            get
            {
                return (string)GetValue(ControlNameProperty);
            }
            set
            {
                SetValue(ControlNameProperty, value);
                this.carName = value;
            }
        }

        // Using a DependencyProperty as the backing store for ControlName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(CarInfo), new PropertyMetadata(""));

        public CarInfo()
        {
            InitializeComponent();
            this.lab_Name.SetBinding(Label.ContentProperty, new Binding("ControlName") { Source = this });

            this.carActionTypeList = CommonHelper.EnumListDic<CarActionType>();
            this.carPositionList = CommonHelper.EnumListDic<CarPosition>();
            this.carStatusList = CommonHelper.EnumListDic<CarStatus>();
            this.controlModeList = CommonHelper.EnumListDic<CarControlMode>();

            //this.cb_position.ItemsSource = carPositionList.Where(t=> t.Key == "2" || t.Key == "7" || t.Key == "8").ToList();
            //this.cb_position.SelectedValuePath = "Key";
            //this.cb_position.DisplayMemberPath = "Value";
            //this.cb_position.SelectedIndex = 1;

        }

        /// <summary>
        /// 赋值属性
        /// </summary>
        /// <param name="deviceEntity"></param>
        /// <param name="props"></param>
        /// <param name="address"></param>
        public void SetProps(IEnumerable<Equipment> equipments)
        {
            Self = equipments.FirstOrDefault(t => t.Code == ControlName);
            if (Self == null)
            {
                //feeder = null;
                return;
            }

            //if (Self.Code == "car1")
            //{
            //    feeder = equipments.FirstOrDefault(x => x.Code == "Feeder2");
            //}
            //else
            //{
            //    feeder = equipments.FirstOrDefault(x => x.Code == "Feeder1");
            //}

            try
            {
                #region 监控文本赋值

                var hasPallet = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CarMonitorProps.hasPallet.ToString()).Value;
                if (hasPallet != null)
                {
                    lab_hasPallet.Foreground = Brushes.Blue;
                    switch (hasPallet)
                    {
                        case "0":
                            lab_hasPallet.Content = "初始";
                            break;
                        case "1":
                            lab_hasPallet.Content = "无货";
                            break;
                        case "2":
                            lab_hasPallet.Content = "有货";
                            break;
                        default:
                            lab_hasPallet.Content = "未获取或不识别";
                            lab_hasPallet.Foreground = Brushes.Red;
                            break;
                    }
                }

                var carNo = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CarMonitorProps.carNo.ToString()).Value;
                if (!string.IsNullOrEmpty(carNo))
                {
                    lab_carNo.Foreground = Brushes.Blue;
                    lab_carNo.Content = carNo;
                }

                var row = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CarMonitorProps.row.ToString()).Value;
                if (row != null)
                {
                    lab_Row.Foreground = Brushes.Blue;
                    lab_Row.Content = row;
                }
                var TaskHeaderID = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CarMonitorProps.TaskHeaderID.ToString()).Value;
                if (TaskHeaderID != null)
                {
                    lab_taskHeaderId.Foreground = Brushes.Blue;
                    lab_taskHeaderId.Content = TaskHeaderID;
                }
                var TaskCarId = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CarMonitorProps.TaskCarId.ToString()).Value;
                if (TaskCarId  != null)
                {
                    lab_taskCarId.Foreground = Brushes.Blue;
                    lab_taskCarId.Content = TaskCarId;
                }
                


                

                //显示任务类型
                var actionType = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CarMonitorProps.actionType.ToString()).Value;
                if (this.carActionTypeList.ContainsKey(actionType))
                {
                    lab_actionType.Content = this.carActionTypeList[actionType];
                }
                //显示设备状态
                var carStatus = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CarMonitorProps.carStatus.ToString()).Value;
                if (this.carStatusList.ContainsKey(carStatus))
                {
                    lab_carStatus.Content = this.carStatusList[carStatus];
                }                   
                //显示控制模式
                var controlMode = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == CarMonitorProps.controlMode.ToString()).Value;
                if (this.controlModeList.ContainsKey(controlMode))
                {
                    lab_controlMode.Content = this.controlModeList[controlMode];
                }

                //错误信息显示
                var tempProps = Self.EquipmentProps.FindAll(t => t.EquipmentTypeTemplate.IsMonitor == true);
                foreach (var item in tempProps)
                {
                    if (item.Value != item.EquipmentTypeTemplate.MonitorCompareValue)
                    {
                        AddAlarm("报警：" + item.EquipmentTypeTemplate.Name + " 信息：" + item.EquipmentTypeTemplate.MonitorFailure, 2);
                    }
                    else
                    {
                        RemoveAlarm("报警：" + item.EquipmentTypeTemplate.Name + " 信息：" + item.EquipmentTypeTemplate.MonitorFailure);
                    }
                }

                #endregion

                #region 清除WCS写入的DB块

                //清除【行列层】设置

                var wcsActionType = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsActionType.ToString());
                if (wcsActionType.Value == CarActionType.ResetLocation.GetIndexString())
                {
                    var wcsRow = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.row .ToString());
                    if (wcsRow.Value != "0" )
                    {
                        var wcsSwitch = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsSwitch.ToString());
                        wcsSwitch.Value = "0";
                        wcsActionType.Value = CarActionType.Init.GetIndexString();
                        wcsRow.Value = "0";
                        CommandUpdateLocation?.Invoke(Self);
                    }
                }
                //清除【位置】设置
                if (wcsActionType.Value == CarActionType.ResetPosition.GetIndexString())
                {
                    var wcsPosition = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "wcsPosition");
                    if (wcsPosition.Value != "0")
                    {
                        var wcsSwitch = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsSwitch.ToString());
                        wcsSwitch.Value = "0";
                        wcsActionType.Value = CarActionType.Init.GetIndexString();
                        wcsPosition.Value = "0";
                        CommandSetPosition?.Invoke(Self);
                    }
                }
                
                //清除【复位】设置
                var wcsResetCommand = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsResetCommand.ToString());
                if (wcsResetCommand.Value != "0")
                {
                    wcsResetCommand.Value = "0";
                    CommandReset?.Invoke(Self);
                }
                //清除【删除任务】设置
                var wcsDeleteCommand = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsDeleteCommand.ToString());
                if (wcsDeleteCommand.Value != "0")
                {
                    wcsDeleteCommand.Value = "0";
                    CommandDelete?.Invoke(Self);
                }
                //清除【切换模式】设置
                var wcsControlMode = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsControlMode.ToString());
                if (wcsControlMode.Value != "0")
                {
                    wcsControlMode.Value = "0";
                    CommandControlMode?.Invoke(Self);
                }
                #endregion

                
            }
            catch (Exception ex)
            {
                Logger.Log($"小车监控界面异常：{ex.Message}", LogLevel.Exception);
            }
        }


        /// <summary>
        /// 删除报警
        /// </summary>
        /// <param name="v"></param>
        private void RemoveAlarm(string v)
        {
            for (int i = list_Alarm.Items.Count - 1; i >= 0; i--)
            {
                var a = (TextBlock)list_Alarm.Items[i];
                if (a.Text.Contains(v))
                {
                    list_Alarm.Items.Remove(list_Alarm.Items[i]);
                }
            }
        }

        /// <summary>
        /// 添加报警
        /// </summary>
        /// <param name="log"></param>
        /// <param name="level">1显示绿色，2显示红色</param>
        public void AddAlarm(string log, int level)
        {
            //先找存不存在
            foreach (var item in list_Alarm.Items)
            {
                var a = (TextBlock)item;
                if (a.Text.Contains(log))
                {
                    //存在就不再次添加
                    return;
                }
            }
            TextBlock textBlock = new TextBlock
            {
                Text = DateTime.Now.ToLongTimeString() + ":" + log
            };
            switch (level)
            {
                case 1:
                    textBlock.Background = Brushes.Green;
                    break;
                case 2:
                    textBlock.Background = Brushes.Red;
                    break;
            }
            textBlock.MaxWidth = this.Width;
            textBlock.TextWrapping = TextWrapping.Wrap;
            this.list_Alarm.Items.Add(textBlock);
            this.list_Alarm.SelectedIndex = this.list_Alarm.Items.Count - 1;
            this.list_Alarm.ScrollIntoView(this.list_Alarm.SelectedItem);
        }

        

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CommandDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"是否确认删除任务？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button button = (Button)sender;
                if (Self != null)
                {
                    //var carStatus = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == "carStatus");
                    //if (carStatus.Value != CarStatus.idle.GetIndexString())
                    //{
                    //    MessageBox.Show("小车状态不是空闲，不能设置！");
                    //    return;
                    //} 
                    var props = Self.EquipmentProps;
                    var wcsDeleteCommand = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsDeleteCommand.ToString());
                    wcsDeleteCommand.Value = "1";
                    var wcsConfirmTaskFinish = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsConfirmTaskFinish.ToString());
                    wcsConfirmTaskFinish.Value = "0";
                    var wcsActionType = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsActionType.ToString());
                    wcsActionType.Value = CarActionType.Init.GetIndexString();
                    var wcsRow = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.row .ToString());
                    wcsRow.Value = "0";
                    
                    var wcsTaskHeaderId = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsTaskHeaderId.ToString());
                    wcsTaskHeaderId.Value = "0";
                    var wcsTaskCarId = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsTaskCarId.ToString());
                    wcsTaskCarId.Value = "0";
                    var wcsSwitch = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsSwitch.ToString());
                    wcsSwitch.Value = "0";
                    var hasPallet = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.hasPallet.ToString());
                    hasPallet.Value = "2";
                    var temp = CommandDelete?.Invoke(Self);
                    if (temp == null)
                    {
                        MessageBox.Show($"未处理{button.Content}事件");
                    }
                    else
                    {
                        if (temp.Success)
                        {
                            //MessageBox.Show("取货错误处理成功");
                        }
                        else
                        {
                            MessageBox.Show($"{button.Content}操作失败：{temp.Msg}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("操作失败，通信中断，请重试！");
                }
            }
        }

        

        /// <summary>
        /// 控制模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_controlMode_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (Self != null)
            {
                //var carStatus = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.carStatus.ToString());
                //if (carStatus.Value != CarStatus.idle.GetIndexString())
                //{
                //    MessageBox.Show("小车状态不是空闲，不能设置！");
                //    return;
                //}
                var wcsControlMode = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsControlMode.ToString());
                if (wcsControlMode != null)
                {
                    wcsControlMode.Value = "1";
                    var temp = CommandControlMode?.Invoke(Self);
                    if (temp == null)
                    {
                        MessageBox.Show($"未处理{button.Content}事件");
                    }
                    else
                    {
                        if (temp.Success)
                        {
                            //MessageBox.Show("取货错误处理成功");
                        }
                        else
                        {
                            MessageBox.Show($"{button.Content}操作失败：{temp.Msg}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("操作失败，通信中断，请重试！");
            }
        }

        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (Self != null)
            {
                //var carStatus = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.carStatus.ToString());
                //if (carStatus.Value != CarStatus.idle.GetIndexString())
                //{
                //    MessageBox.Show("小车状态不是空闲，不能设置！");
                //    return;
                //}
                var wcsResetCommand = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsResetCommand.ToString());
                if (wcsResetCommand != null)
                {
                    wcsResetCommand.Value = "1";
                    var temp = CommandReset?.Invoke(Self);
                    if (temp == null)
                    {
                        MessageBox.Show($"未处理{button.Content}事件");
                    }
                    else
                    {
                        if (temp.Success)
                        {
                            //MessageBox.Show("取货错误处理成功");
                        }
                        else
                        {
                            MessageBox.Show($"{button.Content}操作失败：{temp.Msg}");
                        }
                    }                    
                }
            }
            else
            {
                MessageBox.Show("操作失败，通信中断，请重试！");
            }
        }

        /// <summary>
        /// 任务重发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ReSend_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (Self != null)
            {
                //var carStatus = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.carStatus.ToString());
                //if (carStatus.Value != CarStatus.idle.GetIndexString())
                //{
                //    MessageBox.Show("小车状态不是空闲，不能设置！");
                //    return;
                //}
                var temp = CommandReSend?.Invoke(Self);
                if (temp == null)
                {
                    MessageBox.Show($"未处理{button.Content}事件");
                }
                else
                {
                    if (temp.Success)
                    {
                        MessageBox.Show("重下任务成功！");
                    }
                    else
                    {
                        MessageBox.Show($"{button.Content}操作失败：{temp.Msg}");
                    }
                }
            }
            else
            {
                MessageBox.Show("操作失败，通信中断，请重试！");
            }
        }

        

        /// <summary>
        /// 更新行列层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_updateLocation_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txt_wcsRow.Text, out int row) == false )
            {
                MessageBox.Show("行必须为数字！");
                return;
            }
            if (row <= 0 )
            {
                MessageBox.Show("行必须为大于0的数字！");
                return;
            }
            if (MessageBox.Show($"是否确认小车[{lab_Name.Content}]更新位置到 【{txt_wcsRow.Text}】行", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button button = (Button)sender;
                if (Self != null)
                {
                    var props = Self.EquipmentProps;
                    var carStatus = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.carStatus.ToString());
                    if (carStatus.Value != CarStatus.idle.GetIndexString())
                    {
                        MessageBox.Show("小车状态不是空闲，不能设置行！");
                        return;
                    }
                    var controlMode = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.controlMode.ToString());
                    if (controlMode.Value == CarControlMode.自动.GetIndexString())
                    {
                        MessageBox.Show("小车状态是自动，不能设置行！");
                        return;
                    }
                    var wcsActionType = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsActionType.ToString());
                    wcsActionType.Value = CarActionType.ResetLocation.GetIndexString();
                    var wcsRow = props.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.row.ToString());
                    wcsRow.Value = txt_wcsRow.Text;
                    var wcsSwitch = Self.EquipmentProps.Find(t => t.EquipmentTypeTemplateCode == CarMonitorProps.wcsSwitch.ToString());
                    wcsSwitch.Value = "1";
                    var temp = CommandUpdateLocation?.Invoke(Self);
                    if (temp == null)
                    {
                        MessageBox.Show($"未处理{button.Content}事件");
                    }
                    else
                    {
                        if (temp.Success)
                        {
                            this.txt_wcsRow.Text = "";
                        }
                        else
                        {
                            MessageBox.Show($"{button.Content}操作失败：{temp.Msg}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("操作失败，通信中断，请重试！");
                }
            }
        }







    }
}
