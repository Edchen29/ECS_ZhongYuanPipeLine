using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.View.Win;
using HHECS.ViewModel;
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

namespace HHECS.View.StepTraceTask
{
    /// <summary>
    /// WinStepTrace.xaml 的交互逻辑
    /// </summary>
    public partial class WinStepTrace : HideCloseWindow
    {
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        public WinStepTrace()
        {
            InitializeComponent();
            InitControl();
            AppSession.BllService.CheckPermission(App.MenuOperations, new List<UIElement>() { btn_Delete, btn_Query, btn_SendToWCS, BtnAdd, BtnComplete });
        }

        private void InitControl()
        {
            endTime.SelectedDate = DateTime.Now.AddDays(1);
            beginTime.SelectedDate = DateTime.Now.AddDays(-7);
            //var temp = AppSession.BllService.GetDictWithDetails("TaskStatus");
            //if (temp.Success)
            //{
            //    var dictDetails = temp.Data.DictDetails.ToDictionary(t => t.Value, i => i.Name);
            //    dictDetails.Add("", "全部");
            //    cbx_TaskStatus.ItemsSource = dictDetails;
            //    cbx_TaskStatus.DisplayMemberPath = "Value";
            //    cbx_TaskStatus.SelectedValuePath = "Key";
            //}

            this.DPMain.DataContext = PageInfo;
        }
        private void Query()
        {
            string sql = "where 1=1 ";            
            if (!String.IsNullOrWhiteSpace(txt_TaskNo.Text))
            {
                sql += $" and id ={txt_TaskNo.Text}";
            }
            var status = cbx_TaskStatus.SelectedValue?.ToString();
            if (!String.IsNullOrWhiteSpace(status))
            {
                sql += $" and status= {status} ";
            }
            if (beginTime.SelectedDate != null)
            {
                sql += $" and createTime>='{beginTime.ToString()}'";
            }
            if (endTime.SelectedDate != null)
            {
                sql += $" and createTime<='{endTime.ToString()}'";
            }
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<StepStation>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var result2 = AppSession.Dal.GetCommonModeByPageCondition<StepStation>(PageInfo.PageIndex, PageInfo.PageSize, sql, "id desc");
                if (!result2.Success)
                {
                    MessageBox.Show($"查询失败：{result2.Msg}");
                }
                else
                {
                    var tasks = result2.Data;                  
                    dgv_1.ItemsSource = tasks;
                }
            }
            else
            {
                MessageBox.Show($"查询失败：{result.Msg}");
            }
        }

        private void page_PageChanged(object sender, Model.Controls.PageChangedEventArgs e)
        {

        }

        private void btn_Query_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }
    }
}
