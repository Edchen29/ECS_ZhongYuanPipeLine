using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.ApiModel;
using HHECS.Model.ApiModel.WCSApiModel;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// TaskAdd.xaml 的交互逻辑
    /// </summary>
    public partial class WinTaskAdd : Window
    {
        public TaskCreateModel TaskCreateModel
        {
            get { return (TaskCreateModel)GetValue(TaskEntityProperty); }
            set { SetValue(TaskEntityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskEntity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskEntityProperty =
            DependencyProperty.Register("TaskCreateModel", typeof(TaskCreateModel), typeof(WinTaskAdd), new PropertyMetadata(new TaskCreateModel()));


        public WinTaskAdd()
        {
            InitializeComponent();
            InitContorl();

        }

        private void InitContorl()
        {
            BllResult<Dict> temp = AppSession.BllService.GetDictWithDetails(SysConst.TaskPriority.ToString());//任务优先级
            cbx_Priority.ItemsSource = temp.Success ? temp.Data.DictDetails : null;
            cbx_Priority.SelectedValuePath = "Value";
            cbx_Priority.DisplayMemberPath = "Name";
            cbx_Priority.SelectedIndex = 0;
            var temp2 = AppSession.BllService.GetDictWithDetails(SysConst.TaskType.ToString());//任务类型
            cbx_TaskType.ItemsSource = temp2.Success ? temp2.Data.DictDetails : null;
            cbx_TaskType.SelectedValuePath = "Value";
            cbx_TaskType.DisplayMemberPath = "Name";
            cbx_TaskType.SelectedIndex = 0;
            var temp3 = AppSession.BllService.GetDictWithDetails(SysConst.Port.ToString());//站台
            cbx_FromPort.ItemsSource = temp3.Success ? temp3.Data.DictDetails : null;
            cbx_FromPort.SelectedValuePath = "Code";
            cbx_FromPort.DisplayMemberPath = "Code";
            cbx_FromPort.SelectedIndex = 0;
            var temp4 = AppSession.BllService.GetDictWithDetails(SysConst.Port.ToString());//站台
            cbx_ToPort.ItemsSource = temp4.Success ? temp4.Data.DictDetails : null;
            cbx_ToPort.SelectedValuePath = "Code";
            cbx_ToPort.DisplayMemberPath = "Code";
            cbx_ToPort.SelectedIndex = 0;

            TaskCreateModel.WarehouseCode = App.WarehouseCode;
            GridMain.DataContext = TaskCreateModel;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    var result = AppSession.TaskService.CreateTask(TaskCreateModel);
                    if (result.Success)
                    {
                        MessageBox.Show("创建成功");
                    }
                    else
                    {
                        MessageBox.Show($"创建失败：{result.Msg}");
                    }

                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"创建任务失败：{ex.Message}");
                }

            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
