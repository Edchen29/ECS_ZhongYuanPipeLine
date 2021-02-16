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

namespace HHECS.View.CacheInfo
{
    /// <summary>
    /// CacheMaterial.xaml 的交互逻辑
    /// </summary>
    public partial class CacheMaterial : HideCloseWindow
    {
        public StationCache CurrentStationCache { get; set; } = new StationCache();
        public EquipmentType CurrentEquipmentType { get; set; } = new EquipmentType();

        public CacheMaterial()
        {
            InitializeComponent();
            AppSession.BllService.CheckPermission(App.MenuOperations, SPMain.Children);

            //工位列表
            List<Station> stations = new List<Station>();
            stations.Add(new Station() { Code = "LengthMeasuringCache", Name = "测长缓存" });
            //stations.Add(new Station() { Code = "MeasuringLength", Name = "测长缓存" });//cf
            stations.Add(new Station() { Code = "AssembleCache1", Name = "组队区缓存1" });
            stations.Add(new Station() { Code = "AssembleCache2", Name = "组队区缓存2" });
            cbxStation.ItemsSource = stations;
            cbxStation.DisplayMemberPath = "Name";
            cbxStation.SelectedValuePath = "Code";
            this.Closing += CacheMaterial_Closing;//cf
        }
        private void CacheMaterial_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cbxStation.SelectedIndex = -1;
            DGMain.ItemsSource = null;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中一条数据");
                return;
            }
            else
            {
                if (MessageBox.Show("是否确认删除？对应的套料信息和工序监控任务都可能被删除", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<StationCache> list = new List<StationCache>();
                    List<StepTrace> steptrace = new List<StepTrace>();
                    CutPlan cutplan = new CutPlan();
                    foreach (var item in DGMain.SelectedItems)
                    {
                        list.Add((StationCache)item);
                    }
                    var ids = list.Select(t => t.Id.Value).ToList();
                    var stationcachesResult = AppSession.Dal.GetCommonModelByCondition<StationCache>($" where id in ({string.Join(",", ids)})");
                    if (!stationcachesResult.Success)
                    {
                        MessageBox.Show("管材缓存信息不存在！");
                    }
                    var stepTraceIds = stationcachesResult.Data.Where(t => t.stepTraceId != null).Select(t => t.stepTraceId).ToList();

                    using (IDbConnection connection = AppSession.Dal.GetConnection())
                    {
                        IDbTransaction tran = null;
                        try
                        {
                            connection.Open();
                            tran = connection.BeginTransaction();
                            connection.DeleteList<CutPlan>("where stationCacheId in @stationCacheIds", new { stationCacheIds = ids }, tran);
                            if (stepTraceIds.Count > 0)
                            {
                                connection.DeleteList<StepTrace>("where id in @ids", new { ids = stepTraceIds }, tran);
                            }
                            connection.DeleteList<StationCache>("where id in @ids", new { ids = ids }, tran);
                            tran?.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran?.Rollback();
                            Logger.Log($"删除管材缓存的数据候发生异常，原因：{ex.Message}", LogLevel.Exception, ex);
                            MessageBox.Show("删除失败");
                        }
                    }
                    Query();
                }
            }
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            string sql = "where 1=1 ";
            if (!String.IsNullOrWhiteSpace(cbxStation.SelectedValue?.ToString()))
            {
                sql += $" and stationCode = '{cbxStation.SelectedValue?.ToString()}'";
            }
            if (!String.IsNullOrWhiteSpace(txtStepTraceId.Text))
            {
                sql += $" and stepTraceId = {txtStepTraceId.Text}";
            }
            BllResult<List<StationCache>> result = AppSession.Dal.GetCommonModelByCondition<StationCache>(sql);
            var result1 = AppSession.Dal.GetCommonModelByCondition<Station>("where 1=1");
            if (result.Success)
            {
                var tasks = result.Data;
                tasks.ForEach(t =>
                {
                    t.StationCodeVM = result1.Data?.FirstOrDefault(a => a.Id == t.StationId);
                });
                DGMain.ItemsSource = tasks;
            }
            else
            {
                DGMain.ItemsSource = null;
                MessageBox.Show($"查询失败：{result.Msg}");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinCacheMaterialAddOrEdit win = new WinCacheMaterialAddOrEdit(null);
            win.ShowDialog();
            Query();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据！");
                return;
            }
            if (DGMain.SelectedItems.Count > 1)
            {
                MessageBox.Show("每次只能选择一条");
                return;
            }
            else
            {
                StationCache temp = (StationCache)DGMain.SelectedItem;
                WinCacheMaterialAddOrEdit win = new WinCacheMaterialAddOrEdit(temp.Id);
                win.ShowDialog();
                Query();
            }
        }

        private void BtnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStationCache.Id == null)
            {
                MessageBox.Show("请先选择主数据");
                return;
            }
            else
            {
                WinCutPlanAddOrEdit win = new WinCutPlanAddOrEdit(null, CurrentStationCache);
                win.ShowDialog();
                QueryDetail(CurrentStationCache);
            }
        }

        private void BtnEditDetial_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStationCache.Id == null)
            {
                MessageBox.Show("请先选择主数据");
                return;
            }
            if (DGDetail.SelectedItems.Count > 1)
            {
                MessageBox.Show("每次只能选择一条");
                return;
            }
            else if (DGDetail.SelectedItem == null)
            {
                MessageBox.Show("请选择一行明细数据");
                return;
            }
            else
            {
                var a = (CutPlan)DGDetail.SelectedItem;
                WinCutPlanAddOrEdit win = new WinCutPlanAddOrEdit(a.Id, CurrentStationCache);
                win.ShowDialog();
                QueryDetail(CurrentStationCache);
            }
        }

        private void BtnDeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            if (DGDetail.SelectedItems == null || DGDetail.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条属性明细");
                return;
            }
            else
            {
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
                    QueryDetail(CurrentStationCache);
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
                    QueryDetail((StationCache)dgr.Item);
                    //事件不再向上传递，防止设置其他tabitem的isselected属性失败
                    e.Handled = true;
                    DGMain.SelectedItem = dgr.Item;
                }
            }
        }

        private void QueryDetail(StationCache item)
        {
            //重新查询一遍主数据，防止同步删除
            BllResult<List<StationCache>> result = AppSession.Dal.GetCommonModelByCondition<StationCache>($"where id ={item.Id}");
            if (result.Success)
            {
                var temp = result.Data[0];
                CurrentStationCache.Id = temp.Id;
                //CurrentStationCache.Code = temp.Code;
                //CurrentStationCache.Name = temp.Name;
                //CurrentStationCache.Description = temp.Description;
                CurrentStationCache.CreateTime = temp.CreateTime;
                CurrentStationCache.CreateBy = temp.CreateBy;
                CurrentStationCache.CreateTime = temp.CreateTime;
                CurrentStationCache.UpdateBy = temp.UpdateBy;

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
                var a = AppSession.Dal.GetCommonModelByCondition<CutPlan>($"where stationCacheId = {CurrentStationCache.Id}" + sql);
                if (a.Success)
                {
                    DGDetail.ItemsSource = a.Data;
                }
                else
                {
                    DGDetail.ItemsSource = null;
                    MessageBox.Show($"查询当前套料结果模板失败:{a.Msg}");
                }
                TIDetail.IsSelected = true;
            }
            else
            {
                MessageBox.Show("未能查询到主数据，请刷新");
                CacheMaterialPanl.IsSelected = true;
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
                QueryDetail((StationCache)DGMain.SelectedItem);
            }
        }

        private void BtnUpdateDetail_Click(object sender, RoutedEventArgs e)
        {
            QueryDetail(CurrentStationCache);
        }
    }
}
