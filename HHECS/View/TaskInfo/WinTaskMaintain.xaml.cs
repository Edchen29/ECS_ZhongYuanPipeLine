using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;
using HHECS.View.Win;
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

namespace HHECS.View.TaskInfo
{
    /// <summary>
    /// WinTaskMaintain.xaml 的交互逻辑
    /// </summary>
    public partial class WinTaskMaintain : BaseWindow
    {
        int taskId;

        public StepTrace TaskModel
        {
            get { return (StepTrace)GetValue(TaskModelProperty); }
            set { SetValue(TaskModelProperty, value); }
        }

        public static readonly DependencyProperty TaskModelProperty =
            DependencyProperty.Register("TaskModel", typeof(StepTrace), typeof(WinTaskMaintain), new PropertyMetadata(new StepTrace()));


        public WinTaskMaintain(int taskId)
        {
            InitializeComponent();
            this.taskId = taskId;
            InitControls();

            Dictionary<int, string> layerItems = new Dictionary<int, string>();
            layerItems.Add(1, "1层");
            layerItems.Add(2, "2层");
            Cbx_WCSLayer.ItemsSource = layerItems;
            Cbx_WCSLayer.DisplayMemberPath = "Value";
            Cbx_WCSLayer.SelectedValuePath = "Key";

            Dictionary<int, string> lineItems = new Dictionary<int, string>();
            lineItems.Add(1, "1列");
            lineItems.Add(2, "2列");
            lineItems.Add(3, "3列");
            lineItems.Add(4, "4列");
            Cbx_WCSLine.ItemsSource = lineItems;
            Cbx_WCSLine.DisplayMemberPath = "Value";
            Cbx_WCSLine.SelectedValuePath = "Key";
        }

        private void InitControls()
        {
            var temp = AppSession.BllService.GetDictWithDetails("StepTraceStatus");
            if (temp.Success)
            {
                var dictDetails = temp.Data.DictDetails.ToDictionary(t => t.Value, i => i.Name);
                CbxStatus.ItemsSource = dictDetails;
                CbxStatus.DisplayMemberPath = "Value";
                CbxStatus.SelectedValuePath = "Key";
            }
        }

        private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            var result = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id = {taskId}");
            if (!result.Success)
            {
                Grid1.IsEnabled = false;
                MessageBox.Show($"未找到任务号为：{taskId}的任务：{result.Msg}");
            }
            else
            {
                Grid1.IsEnabled = true;
                TaskModel = result.Data[0];
                Grid1.DataContext = TaskModel;
            }

            ////更改当前工序需要，根据产品编码，取对应的ID
            //var step = AppSession.Dal.GetCommonModelByCondition<Step>($"WHERE productCode ='{TaskModel.ProductCode}'");
            //if (!step.Success)
            //{
            //    Grid1.IsEnabled = false;
            //    MessageBox.Show($"未找到任务号为：{taskId}的任务：{result.Msg}");
            //}
            //else
            //{
            //    var dictDetails = step.Data;
            //    Cbx_StepId.ItemsSource = dictDetails;
            //    Cbx_StepId.DisplayMemberPath = "Name";
            //    Cbx_StepId.SelectedValuePath = "Id";
            //}
            //var station = AppSession.Dal.GetCommonModelByCondition<Station>($"where 1=1");

            //var StepStation = AppSession.Dal.GetCommonModelByCondition<StepStation>($"where 1=1");
        }

        /// <summary>
        /// 重新加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 状态维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditStatus_Click(object sender, RoutedEventArgs e)
        {
            string status = CbxStatus.SelectedValue?.ToString();
            if (string.IsNullOrWhiteSpace(status))
            {
                MessageBox.Show("请先选中需要更改的状态");
                return;
            }

            if (!Enum.TryParse(status, out StepTraceStatus newStatus))
            {
                MessageBox.Show($"状态：{status}无效");
                return;
            }

            if (TaskModel.Status >= StepTraceStatus.任务完成.GetIndexInt())
            {
                MessageBox.Show("任务已经完成，不能修改数据");
                return;
            }

            string messageBoxText = null;
            if (status == StepTraceStatus.响应放货完成.GetIndexString())
            {
                messageBoxText = $"是否要将任务[{taskId}]的状态手动改为[{StepTraceStatus.响应放货完成}]？这会改变任务的当前工位！";
            }
            if (status == StepTraceStatus.任务完成.GetIndexString())
            {
                messageBoxText = $"是否要将任务[{taskId}]的状态手动改为[{StepTraceStatus.任务完成}]？这会导致任务不再执行！";
            }
            if (status == StepTraceStatus.异常结束.GetIndexString())
            {
                messageBoxText = $"是否要将任务[{taskId}]的状态手动改为[{StepTraceStatus.异常结束}]？这会导致任务不再执行，并且会被标记为异常技术！";
            }
            if (!string.IsNullOrWhiteSpace(messageBoxText))
            {
                if (MessageBox.Show(messageBoxText, "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }

            //原來任務狀態
            int oldStatus = TaskModel.Status;
            BllResult result = AppSession.TaskService.ChangStepStatus(TaskModel.Id.Value, (int)newStatus, oldStatus, App.User.UserCode);
            if (result.Success)
            {
                MessageBox.Show($"更改任务{TaskModel.Id}状态成功");
            }
            else
            {
                MessageBox.Show($"更改任务{TaskModel.Id}状态失败：{result.Msg}");
            }            
        }

        #region
        ///// <summary>
        ///// 产品id维护
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void BtnEditProductId_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("");
        //}

        ///// <summary>
        ///// 序列号维护
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void BtnEditSerialNumber_Click(object sender, RoutedEventArgs e)
        //{

        //}

        ///// <summary>
        ///// 当前工序维护
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void BtnEditStepIdVM_Click(object sender, RoutedEventArgs e)
        //{
        //    var oldStepId_Results = AppSession.Dal.GetCommonModelByCondition<StepTrace>($"where id={taskId}");
        //    if (!oldStepId_Results.Success)
        //    {
        //        MessageBox.Show($"查询此任务{taskId}出错");
        //        return;
        //    }

        //    string oldStepId = oldStepId_Results.Data[0].StepId.ToString();      /*原任务老的工序ID*/

        //    string newStepId = Cbx_StepId.SelectedValue?.ToString();        /*任务新的工序ID*/

        //    if (string.IsNullOrWhiteSpace(newStepId))
        //    {
        //        MessageBox.Show($"请选择一道工序");
        //        return;
        //    }

        //    if (oldStepId == newStepId)
        //    {
        //        MessageBox.Show($"选择的工序与原任务工序一致");
        //        return;
        //    }

        //    var res = AppSession.TaskService.ChangeTaskStepId(TaskModel.Id, oldStepId, newStepId, App.User.UserCode);
        //    if (res.Success)
        //    {
        //        MessageBox.Show($"工序维护成功");
        //    }
        //    else
        //    {
        //        MessageBox.Show($"工序维护失败");
        //    }
        //}

        ///// <summary>
        ///// 当前工位维护
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void BtnEditStationIdVM_Click(object sender, RoutedEventArgs e)
        //{

        //}

        ///// <summary>
        ///// 下道工位维护
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void BtnEditNextStationIdVM_Click(object sender, RoutedEventArgs e)
        //{
        //    string str = "0";
        //    if (MessageBox.Show($"任务{taskId}是否清空下道工位？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //    {
        //        BllResult result = AppSession.TaskService.ChangeTaskNextStepId(TaskModel.Id, str, App.User.UserCode);
        //        if (result.Success)
        //        {
        //            MessageBox.Show($"任务{taskId}清空下道工位成功");
        //        }
        //        else
        //        {
        //            MessageBox.Show($"任务{taskId}清空失败:{result.Msg}");
        //        }
        //    }
        //}

        ///// <summary>
        ///// 下道工序维护
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void BtnEditNextStepIdVM_Click(object sender, RoutedEventArgs e)
        //{
        //    string str = "0";
        //    if (MessageBox.Show($"任务{taskId}是否清空下道工序？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //    {
        //        BllResult result = AppSession.TaskService.ChangeTaskNextStationId(TaskModel.Id, str, App.User.UserCode);
        //        if (result.Success)
        //        {
        //            MessageBox.Show($"任务{taskId}清空下道工序成功");
        //        }
        //        else
        //        {
        //            MessageBox.Show($"任务{taskId}清空失败:{result.Msg}");
        //        }
        //    }
        //}
        #endregion

        /// <summary>
        /// 修改wcs列层逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditWCSLine_Click(object sender, RoutedEventArgs e)
        {

            if (Cbx_WCSLayer.SelectedValue?.ToString() == TaskModel.WCSLayer && Cbx_WCSLine.SelectedValue?.ToString() == TaskModel.WCSLine)
            {
                MessageBox.Show("无需修改");
                return;
            }

            if (MessageBox.Show($"任务[{taskId}]是否修改列层？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string newLine = Cbx_WCSLine.SelectedValue?.ToString();
                string newLayer = Cbx_WCSLayer.SelectedValue?.ToString();
                BllResult result = AppSession.TaskService.ChangeTaskWCSLineOrWcsLayer(TaskModel.Id, newLine, newLayer, App.User.UserCode);
                if (result.Success)
                {
                    MessageBox.Show($"任务{taskId}修改列成功");
                }
                else
                {
                    MessageBox.Show($"任务{taskId}修改列失败:{result.Msg}");
                }
            }
        }
    }
}
