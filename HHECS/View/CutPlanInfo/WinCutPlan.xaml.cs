using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Controls;
using HHECS.Model.Entities;
using HHECS.Model.Enums.PipeLine;
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

namespace HHECS.View.CutPlanInfo
{
    /// <summary>
    /// WinCutPlan.xaml 的交互逻辑
    /// </summary>
    public partial class WinCutPlan : HideCloseWindow
    {

        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        public WinCutPlan()
        {
            InitializeComponent();
            //this.GridDetail.DataContext = CurrentPipeOrder;
            AppSession.BllService.CheckPermission(App.MenuOperations, SPDetail.Children);
        }

        private void btn_Query_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            string sql = "where 1=1 ";
            if (!string.IsNullOrWhiteSpace(TxtMainCode.Text))
            {
                sql += $" and WONumber like '{TxtMainCode.Text}%'";
            }
            if (!string.IsNullOrWhiteSpace(TxtMainId.Text))
            {
                sql += $" and stationCacheId like '{TxtMainId.Text}%'";
            }
            
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<CutPlan>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var temp = AppSession.Dal.GetCommonModeByPageCondition<CutPlan>(PageInfo.PageIndex, PageInfo.PageSize, sql, null);
                DGDetail.ItemsSource = temp.Data;
            }
        }

        private void btn_Change_Click(object sender, RoutedEventArgs e)
        {
            if (DGDetail.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
                return;
            }
            if (DGDetail.SelectedItems.Count > 1)
            {
                MessageBox.Show("每次只能选择一条");
                return;
            }
            else
            {
                CutPlan cutplan = (CutPlan)DGDetail.SelectedItem;
                WinCutPlanAddOrEdit winCpAdd = new WinCutPlanAddOrEdit(cutplan.Id);
                winCpAdd.ShowDialog();
                Query();
            }
        }

        private void page_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }

        /// <summary>
        /// todo:新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinCutPlanAddOrEdit winCpAdd = new WinCutPlanAddOrEdit(null);
            winCpAdd.ShowDialog();
            Query();
        }

        /// <summary>
        /// todo:删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGDetail.SelectedItems == null || DGDetail.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条属性明细");
                return;
            }
            else
            {

                //var statusresult = AppSession.Dal.GetCommonModelByConditionWithZero<CutPlan>($"where status >={StationCacheStatus.使用中.GetIndexInt()}").Data;
                //判断原材料中的状态
                var statusresult = AppSession.Dal.GetCommonModelByConditionWithZero<StationCache>($"where status >={StationCacheStatus.使用中.GetIndexInt()}").Data;
                if (MessageBox.Show("删除工单明细可能导致程序异常，是否继续？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<CutPlan> list = new List<CutPlan>();
                    foreach (var item in DGDetail.SelectedItems)
                    {
                        var cutplan = (CutPlan)item;
                        list.Add(cutplan);
                        foreach (var statusid in statusresult)
                        {
                            if (statusid.Id == cutplan.stationCacheId)
                            {
                                MessageBox.Show($"选中的切割计划ID为：{ cutplan.Id}的状态有已经套料过的工单！请重选");
                                return;
                            }

                        }
                    }
                    BllResult result = AppSession.Dal.DeleteCommonModelByIds<CutPlan>(list.Select(t => t.Id.Value).ToList());
                    if (result.Success)
                    {
                        MessageBox.Show("删除成功");
                    }
                    else
                    {
                        MessageBox.Show($"删除失败:{result.Msg}");
                    }
                    Query();
                }
            }
        }
    }
}
