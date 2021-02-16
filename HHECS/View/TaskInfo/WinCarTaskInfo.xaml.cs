using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Controls;
using HHECS.Model.Entities;
using HHECS.View.Win;
using HHECS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HHECS.View.TaskInfo
{
    /// <summary>
    /// CarTaskInfo.xaml 的交互逻辑
    /// </summary>
    public partial class WinCarTaskInfo : BaseWindow
    {
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        public WinCarTaskInfo()
        {
            InitializeComponent();
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = AppSession.BllService.GetDictWithDetails("CarTaskType");
            if (temp.Success)
            {
                var dictDetails = temp.Data.DictDetails.ToDictionary(t => t.Value, i => i.Name);
                dictDetails.Add("", "全部");
                Cbx_TaskType.ItemsSource = dictDetails;
                Cbx_TaskType.DisplayMemberPath = "Value";
                Cbx_TaskType.SelectedValuePath = "Key";
            }
            else
            {
                MessageBox.Show("加载状态失败");
            }
        }

        private void page_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }

        private void btn_Query_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }
        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条数据！");
            }
            else
            {
                if (MessageBox.Show("是否确认删除？这可能导致程序异常！", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<CarTask> list = new List<CarTask>();
                    foreach (var item in dgv_1.SelectedItems)
                    {
                        list.Add((CarTask)item);
                    }
                    BllResult result = AppSession.Dal.DeleteCommonModelByIds<CarTask>(list.Select(t => t.Id.Value).ToList());
                    if (result.Success)
                    {
                        MessageBox.Show("删除成功");
                    }
                    else
                    {
                        MessageBox.Show("删除失败");
                    }
                    Query();
                }
            }
        }

        
        private void Query()
        {
            string sql = "where 1=1 ";
            if (!String.IsNullOrWhiteSpace(txt_StepTraceId.Text))
            {
                sql += $" and stepTraceId = {txt_StepTraceId.Text}";
            }
            var taskType = Cbx_TaskType.SelectedValue?.ToString();
            if (!String.IsNullOrWhiteSpace(taskType))
            {
                sql += $" and type = {taskType} ";
            }
            if (!String.IsNullOrWhiteSpace(txt_CarNum.Text))
            {
                sql += $" and ";
            }
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<CarTask>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var result2 = AppSession.Dal.GetCommonModeByPageCondition<CarTask>(PageInfo.PageIndex, PageInfo.PageSize, sql, " id desc ");
                if (!result2.Success)
                {
                    MessageBox.Show($"查询错误:{result2.Msg}");
                    return;
                }
                dgv_1.ItemsSource = result2.Data;
            }
            else
            {
                MessageBox.Show($"查询错误:{result.Msg}");
            }

        }
    }
}
