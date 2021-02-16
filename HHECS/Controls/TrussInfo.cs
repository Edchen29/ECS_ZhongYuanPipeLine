using HHECS.EquipmentExcute.SRMV130;
using HHECS.EquipmentExcute.Truss;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.SRM;
using HHECS.Model.Enums.Truss;
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
    /// 标准桁车详情监控
    /// </summary>
    public partial class TrussInfo : UserControl
    {
        public event Func<Location, Equipment, BllResult> DoubleInEvent;
        public event Func<Equipment, BllResult> EmptyOutEvent;
        public event Func<Equipment, BllResult> ForkErrorEvent;
        public event Func<Equipment, BllResult> OverrideTask; //重新下发任务
        public event Func<Equipment, BllResult> DeleteTask; //重新下发任务
        public event Func<String, Equipment, BllResult> MoveTruss; //重新下发任务
        public Equipment Self { get; set; }
        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(TrussInfo), new PropertyMetadata(""));


        public TrussInfo(int maxW, int maxH)
        {
            InitializeComponent();
            this.Width = maxW;
            this.Height = maxH;
            lab_Name.SetBinding(TextBlock.TextProperty, new Binding("ControlName") { Source = this });
        }

        public TrussInfo(int maxW)
        {
            InitializeComponent();
            this.Width = maxW;
            //this.Height = maxH;
            lab_Name.SetBinding(TextBlock.TextProperty, new Binding("ControlName") { Source = this });
        }

        /// <summary>
        /// 赋值属性
        /// </summary>
        /// <param name="deviceEntity"></param>
        /// <param name="props"></param>
        /// <param name="address"></param>
        public void SetProps(Equipment truss)
        {
            Self = truss;

            #region 固定属性赋值
            var Fork1ForkHasPallet = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1HasPallet.ToString());
            if (Fork1ForkHasPallet != null)
            {
                lab_Fork1ForkHasPallet.Foreground = Brushes.Blue;
                switch (Fork1ForkHasPallet.Value)
                {
                    case "True":
                        lab_Fork1ForkHasPallet.Text = "有货";
                        break;
                    case "False":
                        lab_Fork1ForkHasPallet.Text = "无货";
                        break;
                    default:
                        lab_Fork1ForkHasPallet.Text = "未知";
                        lab_Fork1ForkHasPallet.Foreground = Brushes.Red;
                        break;
                }
            }

            var Fork1TaskExcuteStatus = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskExcuteStatus.ToString());
            if (Fork1TaskExcuteStatus != null)
            {
                lab_Fork1TaskExcuteStatus.Foreground = Brushes.Blue;
                SRMTaskExcuteStatus temp = (SRMTaskExcuteStatus)(Convert.ToInt32(Fork1TaskExcuteStatus.Value));
                switch (temp)
                {
                    case SRMTaskExcuteStatus.待机:
                    case SRMTaskExcuteStatus.任务执行中:
                    case SRMTaskExcuteStatus.任务完成:
                        lab_Fork1TaskExcuteStatus.Text = temp.ToString();
                        break;
                    case SRMTaskExcuteStatus.任务中断_出错:
                    case SRMTaskExcuteStatus.下发任务错误:
                        lab_Fork1TaskExcuteStatus.Text = temp.ToString();
                        lab_Fork1TaskExcuteStatus.Foreground = Brushes.Red;
                        break;
                    default:
                        lab_Fork1TaskExcuteStatus.Text = "未知";
                        lab_Fork1TaskExcuteStatus.Foreground = Brushes.Red;
                        break;
                }
            }

            var operationModel = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.OperationModel.ToString());
            if (operationModel != null)
            {
                lab_OperationModel.Foreground = Brushes.Blue;
                SRMOperationModel temp = (SRMOperationModel)(Convert.ToInt32(operationModel.Value));
                switch (temp)
                {
                    case SRMOperationModel.维修:
                    case SRMOperationModel.手动:
                    case SRMOperationModel.机载操作:
                    case SRMOperationModel.单机自动:
                    case SRMOperationModel.联机:
                        lab_OperationModel.Text = temp.ToString();
                        break;
                    default:
                        lab_OperationModel.Text = "未知";
                        lab_OperationModel.Foreground = Brushes.Red;
                        break;
                }
            }

            //var model = stocker.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.ExpendMode.ToString());
            //if (model != null)
            //{
            //    lab_Model.Foreground = Brushes.Blue;
            //    if (model.Value == "True")
            //    {
            //        lab_Model.Text = "转轨";
            //    }
            //    else
            //    {
            //        lab_Model.Text = "正常";
            //    }
            //}

            var forkCenter = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1Center.ToString());
            if (forkCenter != null)
            {
                if (forkCenter.Value == "True")
                {
                    lab_ForkCenter.Text = "在高位";
                }
                else
                {
                    lab_ForkCenter.Text = "在低位";
                }
            }

            var maxColumn = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.ManageBigDistance.ToString());
            if (maxColumn != null)
            {
                lab_MaxColumn.Text = maxColumn.Value;
            }

            var minColumn = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.ManageSmallDistance.ToString());
            if (minColumn != null)
            {
                lab_MinColumn.Text = minColumn.Value;
            }

            var Fork1TaskType = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskType.ToString());
            if (Fork1TaskType != null)
            {
                lab_Fork1TaskType.Foreground = Brushes.Blue;
                TrussForTaskFlag temp = (TrussForTaskFlag)Convert.ToInt32(Fork1TaskType.Value);
                switch (temp)
                {
                    case TrussForTaskFlag.无任务:
                    case TrussForTaskFlag.绗车取货:
                    case TrussForTaskFlag.绗车放货:
                    case TrussForTaskFlag.移车任务:
                        lab_Fork1TaskType.Text = temp.ToString();
                        break;
                    default:
                        lab_Fork1TaskType.Text = "未知";
                        lab_Fork1TaskType.Foreground = Brushes.Red;
                        break;
                }
            }

            var Fork1TaskNo = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1TaskNo.ToString());
            if (Fork1TaskNo != null)
            {
                lab_Fork1TaskNo.Text = Fork1TaskNo.Value;
            }

            lab_rowIndex1.Text = truss.RowIndex1.ToString();
            lab_rowIndex2.Text = truss.RowIndex2.ToString();

            var CurrentTongs = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.CurrentTongs.ToString());
            if (CurrentTongs != null)
            {
                lab_CurrentStation.Text = CurrentTongs.Value;
            }

            var horizontalDistance = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == SRMProps.HorizontalDistance.ToString());
            if (horizontalDistance != null)
            {
                lab_HorizontalDistance.Text = horizontalDistance.Value;
            }

            var verticalDistance = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.VerticalDistance.ToString());
            if (verticalDistance != null)
            {
                lab_VerticalDistance.Text = verticalDistance.Value;
            }

            var ForkDistance = truss.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == TrussNormalProps.Fork1Distance.ToString());
            if (ForkDistance != null)
            {
                lab_ForkDistance.Text = ForkDistance.Value;
            }


            #endregion

            //监控属性赋值
            var tempProps = truss.EquipmentProps.FindAll(t => t.EquipmentTypeTemplate.IsMonitor == true);
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
        }

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
        /// 处理重入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DIn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要进行重入处理？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Self != null)
                {
                    var doubleIn = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == SRMProps.Fork1DoubleIn.ToString());
                    if (doubleIn != null)
                    {
                        if (doubleIn.Value == "True")
                        {
                            WinLocationInput frm_LocationInput = new WinLocationInput
                            {
                                DeviceEntity = Self
                            };
                            if (frm_LocationInput.ShowDialog() == true)
                            {
                                Location locationEntity = frm_LocationInput.NewLocation;
                                //需要判断新的库位是否与老库位类型一致，如果不一致则报错
                                BllResult a = DoubleInEvent?.Invoke(locationEntity, Self);
                                if (a == null)
                                {
                                    MessageBox.Show("未处理重入事件");
                                }
                                else
                                {
                                    if (a.Success)
                                    {
                                        MessageBox.Show("重入处理成功");
                                    }
                                    else
                                    {
                                        MessageBox.Show("重入处理失败:" + a.Msg);
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("当前没有重入报警，无需处理");
                        }
                    }
                    else
                    {
                        MessageBox.Show("未找到桁车的重入属性");
                    }
                }
                else
                {
                    MessageBox.Show("无重入需要处理");
                }
            }
        }

        /// <summary>
        /// 空出处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EOut_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要进行空出处理？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Self != null)
                {
                    var emptyOut = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == "Fork1EmptyOut");
                    if (emptyOut != null)
                    {
                        if (emptyOut.Value == "True")
                        {
                            BllResult temp = EmptyOutEvent?.Invoke(Self);
                            if (temp == null)
                            {
                                MessageBox.Show("未处理空出事件");
                            }
                            else
                            {
                                if (temp.Success)
                                {
                                    MessageBox.Show("空出处理成功");
                                }
                                else
                                {
                                    MessageBox.Show("空出处理失败：" + temp.Msg);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("当前没有空出报警，无需处理");
                        }
                    }
                    else
                    {
                        MessageBox.Show("未找到桁车的空出属性");
                    }
                }
                else
                {
                    MessageBox.Show("无需空出处理");
                }
            }
        }

        /// <summary>
        /// 重新写入任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Override_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要进行任务重新下发处理？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Self != null)
                {
                    var temp = OverrideTask?.Invoke(Self);
                    if (temp == null)
                    {
                        MessageBox.Show("未处理重写事件");
                    }
                    else if (temp.Success)
                    {
                        MessageBox.Show("重新下发成功");
                    }
                    else
                    {
                        MessageBox.Show($"重新下发失败:{temp.Msg}");
                    }
                }
            }
        }

        /// <summary>
        /// 处理取货错
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ForkError_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要进行取货错误处理？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Self != null)
                {
                    var forkError = Self.EquipmentProps.FirstOrDefault(t => t.EquipmentTypeTemplateCode == "Fork1PickupTaskError");
                    if (forkError != null)
                    {
                        if (forkError.Value == "True")
                        {
                            var temp = ForkErrorEvent?.Invoke(Self);
                            if (temp == null)
                            {
                                MessageBox.Show("未处理取货错误事件");
                            }
                            if (temp.Success)
                            {
                                MessageBox.Show("取货错误处理成功");
                            }
                            else
                            {
                                MessageBox.Show($"取货错误处理失败：{temp.Msg}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("当前没有取货错误报警，无需处理");
                        }
                    }
                    else
                    {
                        MessageBox.Show("未找到桁车的取货错误属性");
                    }
                }
                else
                {
                    MessageBox.Show("桁车未赋值");
                }
            }
        }

        /// <summary>
        /// hack:删除任务（只在调试中使用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要进行任务删除处理？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Self != null)
                {
                    var temp = DeleteTask?.Invoke(Self);
                    if (temp == null)
                    {
                        MessageBox.Show("未处理任务删除事件");
                    }
                    if (temp.Success)
                    {
                        MessageBox.Show("任务删除处理成功");
                    }
                    else
                    {
                        MessageBox.Show($"任务删除失败：{temp.Msg}");
                    }
                }
            }
        }

        private void Btn_Move_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要进行移车任务处理？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (Self != null)
                {
                    WinMoveTruss winMove = new WinMoveTruss { DeviceEntity = Self };
                    if (winMove.ShowDialog() == true)
                    {
                        string stationId = winMove.stationId;
                        BllResult temp = MoveTruss?.Invoke(stationId, Self);
                        if (temp == null)
                        {
                            MessageBox.Show("未处理移车任务事件");
                        }
                        if (temp.Success)
                        {
                            MessageBox.Show("移车任务处理成功");
                        }
                        else
                        {
                            MessageBox.Show($"移车任务处理失败：{temp.Msg}");
                        }
                    }
                }
            }

        }
    }
}
