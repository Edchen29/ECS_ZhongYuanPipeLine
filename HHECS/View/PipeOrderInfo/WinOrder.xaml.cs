using Dapper;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.PipeLine;
using HHECS.View.Win;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HHECS.View.PipeOrderInfo
{
    /// <summary>
    /// WinOrder.xaml 的交互逻辑
    /// </summary>
    public partial class WinOrder : HideCloseWindow
    {

        public PipeOrder CurrentPipeOrder { get; set; } = new PipeOrder();
        public WinOrder()
        {
            InitializeComponent();
            this.GridDetail.DataContext = CurrentPipeOrder;
            AppSession.BllService.CheckPermission(App.MenuOperations, SPMain.Children);
            AppSession.BllService.CheckPermission(App.MenuOperations, SPDetail.Children);
        }



        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            string sql = "where 1=1 ";
            if (!String.IsNullOrWhiteSpace(TxtCode.Text))
            {
                sql += $" and code like '%{TxtCode.Text}%'";
            }
            //if (!String.IsNullOrWhiteSpace(TxtName.Text))
            //{
            //    sql += $" and productCode like '%{TxtName.Text}%'";
            //}
            BllResult<List<PipeOrder>> result = AppSession.Dal.GetCommonModelByCondition<PipeOrder>(sql);
            if (result.Success)
            {
                DGMain.ItemsSource = result.Data;
                TIMain.IsSelected = true;
            }
            else
            {
                DGMain.ItemsSource = null;
                MessageBox.Show($"查询失败{result.Msg}");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinOrderAddOrEdit win = new WinOrderAddOrEdit(null);
            win.ShowDialog();
            Query();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var statusresult = AppSession.Dal.GetCommonModelByConditionWithZero<PipeOrder>($"where status ={StationCacheStatus.已经套料.GetIndexInt()}").Data;
            if (DGMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
            }
            if (DGMain.SelectedItems.Count > 1)
            {
                MessageBox.Show("每次只能选择一条");
                return;
            }
            else
            {
                
                PipeOrder temp = (PipeOrder)DGMain.SelectedItem;
                foreach (var statusid in statusresult)
                {
                    if (statusid.Id == temp.Id)
                    {
                        MessageBox.Show($"选中的工单ID为：{ temp.Id}的状态有已经套料过的工单！请重选");
                        return;
                    }

                }
                WinOrderAddOrEdit win = new WinOrderAddOrEdit(temp.Id);
                win.ShowDialog();
                Query();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条数据！");
            }
            else
            {
                if (MessageBox.Show("是否确认删除？这可能导致程序异常！", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<PipeOrder> list = new List<PipeOrder>();
                    var statusresult = AppSession.Dal.GetCommonModelByConditionWithZero<PipeOrder>($"where status ={StationCacheStatus.已经套料.GetIndexInt()}").Data;
                    foreach (var item in DGMain.SelectedItems)
                    {
                        var order = (PipeOrder)item;
                        list.Add(order);
                        
                        foreach (var statusid in statusresult)
                        {
                            if (statusid.Id == order.Id)
                            {
                                MessageBox.Show($"选中的工单ID为：{ order.Id}的状态有已经套料过的工单！请重选");
                                return;
                            }
                            
                        }
                        
                    }
                    var ids = list.Select(t => t.Id.Value).ToList();
                    var wonNumber = list.Select(t => t.Code).ToList();//cf
                    using (IDbConnection connection = AppSession.Dal.GetConnection())
                    {
                        IDbTransaction tran = null;
                        try
                        {
                            connection.Open();
                            tran = connection.BeginTransaction();
                            //$"where WONumber = '{CurrentPipeOrder.Code}'"
                           connection.DeleteList<CutPlan>("where WONumber in @WONumbers", new { WONumbers = wonNumber }, tran); 
                            connection.DeleteList<PipeOrder>("where id in @ids", new { ids = ids }, tran);
                            tran?.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran?.Rollback();
                            Logger.Log($"删除工单的数据候发生异常，原因：{ex.Message}", LogLevel.Exception, ex);
                            MessageBox.Show("删除失败");
                        }
                    }
                    
                    Query();
                }
            }
        }

        private void BtnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPipeOrder.Id == null)
            {
                MessageBox.Show("请先选择主数据");
            }
            else
            {
                WinCutPlanAddOrEdit win = new WinCutPlanAddOrEdit(null, CurrentPipeOrder);
                win.ShowDialog();
                QueryDetail(CurrentPipeOrder);
            }

        }

        private void BtnEditDetial_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPipeOrder.Id == null)
            {
                MessageBox.Show("请先选择主数据");
            }
            if (DGDetail.SelectedItems.Count > 1)
            {
                MessageBox.Show("每次只能选择一条");
                return;
            }
            else if (DGDetail.SelectedItem == null)
            {
                MessageBox.Show("请选择一行明细数据");
            }
            else
            {
                var a = (CutPlan)DGDetail.SelectedItem;
                WinCutPlanAddOrEdit win = new WinCutPlanAddOrEdit(a.Id, CurrentPipeOrder);
                win.ShowDialog();
                QueryDetail(CurrentPipeOrder);
            }
        }

        private void BtnDeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            if (DGDetail.SelectedItems == null || DGDetail.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条属性明细");
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
                    QueryDetail(CurrentPipeOrder);
                }
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGridRow dgr = sender as DataGridRow;
                if (!dgr.IsNewItem)
                {
                    QueryDetail((PipeOrder)dgr.Item);
                    //事件不再向上传递，防止设置其他tabitem的isselected属性失败
                    e.Handled = true;
                    DGMain.SelectedItem = dgr.Item;
                }
            }
        }

        private void QueryDetail(PipeOrder item)
        {
            //重新查询一遍主数据，防止同步删除
            BllResult<List<PipeOrder>> result = AppSession.Dal.GetCommonModelByCondition<PipeOrder>($"where id ={item.Id}");
            if (result.Success)
            {
                var temp = result.Data[0];
                CurrentPipeOrder.Id = temp.Id;
                CurrentPipeOrder.Code = temp.Code;
                //CurrentPipeOrder.Name = temp.Name;
                //CurrentPipeOrder.Description = temp.Description;
                CurrentPipeOrder.CreateTime = temp.CreateTime;
                CurrentPipeOrder.CreateBy = temp.CreateBy;
                CurrentPipeOrder.CreateTime = temp.CreateTime;
                CurrentPipeOrder.UpdateBy = temp.UpdateBy;

                string sql = " ";
                if (!String.IsNullOrWhiteSpace(TxtMainCode.Text))
                {
                    sql += $" and WONumber like '%{TxtMainCode.Text}%'";
                }
                if (!String.IsNullOrWhiteSpace(TxtMainId.Text))
                {
                    sql += $" and stationCacheId like '%{TxtMainId.Text}%'";
                }
                
                //查询属性模板
                //var a = AppSession.Dal.GetCommonModelByCondition<CutPlan>(sql );
                var a = AppSession.Dal.GetCommonModelByCondition<CutPlan>($"where WONumber = '{CurrentPipeOrder.Code}'" + sql );               
                if (a.Success)
                {
                    DGDetail.ItemsSource = a.Data;
                }
                else
                {
                    DGDetail.ItemsSource = null;
                    MessageBox.Show($"查询当前工单模板失败:{a.Msg}");

                }
                TIDetail.IsSelected = true;
            }
            else
            {
                MessageBox.Show("未能查询到主数据，请刷新");
                TIMain.IsSelected = true;
            }
        }

        private void BtnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
            }
            else
            {
                QueryDetail((PipeOrder)DGMain.SelectedItem);
            }
        }

        private void BtnUpdateDetail_Click(object sender, RoutedEventArgs e)
        {
            QueryDetail(CurrentPipeOrder);
        }

        
    }
}
