using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Controls;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using MahApps.Metro.Controls;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;

namespace HHECS.TimerClient
{
    /// <summary>
    /// WinMain.xaml 的交互逻辑
    /// </summary>
    public partial class WinMain : MetroWindow
    {
        public LogInfo Log { get; set; } = new LogInfo();
        IScheduler _scheduler;
        //CancellationTokenSource token = new CancellationTokenSource();

        public WinMain()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            TBUser.Text = $"用户：{App.User.UserName}";
            SPMain.Children.Add(Log);
            Logger.LogWrite += Logger_LogWrite;
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = factory.GetScheduler().Result;
            _scheduler.Start();
            Task.Run(() =>
            {
                JobExcuteListener();
            });
        }

        /// <summary>
        /// 监控job的停止与启用
        /// </summary>
        private void JobExcuteListener()
        {
            while (true)
            {
                Task.Delay(2000);
                try
                {
                    var result = AppSession.Dal.GetCommonModelByCondition<JobEntity>("");
                    if (result.Success)
                    {
                        var jobs = result.Data;
                        //当前所有在执行的jobkey
                        var keys = _scheduler.GetJobKeys(Quartz.Impl.Matchers.GroupMatcher<JobKey>.GroupEquals("Group1")).Result;
                        //查询已经被删除的计划
                        var deleteKeys = keys.Where(t => jobs.Count(a => a.Name == t.Name) == 0 || jobs.First(a => a.Name == t.Name).Status == (int)JobStatus.停止).ToList();
                        if (deleteKeys != null && deleteKeys.Count > 0)
                        {
                            _scheduler.DeleteJobs(deleteKeys);
                        }
                        keys = _scheduler.GetJobKeys(Quartz.Impl.Matchers.GroupMatcher<JobKey>.GroupEquals("Group1")).Result;
                        //遍历
                        foreach (var item in jobs)
                        {
                            var key = keys.FirstOrDefault(t => t.Name == item.Name);
                            if (key == null)
                            {
                                if (item.Status == (int)JobStatus.执行)
                                {
                                    //创建并添加
                                    var jobDetail = JobBuilder.Create<PostJobClient>().WithIdentity(item.Name, "Group1").UsingJobData("Id", item.Id.Value).Build();

                                    var triggerBuild = TriggerBuilder.Create().WithIdentity(item.Name, "Group1").WithCronSchedule(item.Corn);
                                    if (!string.IsNullOrWhiteSpace(item.ExcludesCorn))
                                    {
                                        var calendar = _scheduler.GetCalendar(item.Name).Result;
                                        if (calendar == null)
                                        {
                                            _scheduler.AddCalendar(item.Name, new CronCalendar(item.ExcludesCorn), true, true);
                                        }
                                        triggerBuild.ModifiedByCalendar(item.Name);
                                    }
                                    _scheduler.ScheduleJob(jobDetail, triggerBuild.Build());
                                }
                            }
                            else
                            {
                                var triggerState = _scheduler.GetTriggerState(new TriggerKey(key.Name, key.Group)).Result;
                                string sql = $"update job set updated = '{DateTime.Now.ToString()}',updatedBy = '{App.User.UserCode}',excuteStatus = '{triggerState.ToString()}' where name = '{key.Name}'";
                                //item.Updated = DateTime.Now;
                                //item.UpdatedBy = App.User.UserCode;
                                //item.ExcuteStatus = triggerState.ToString();
                                //AppSession.Bll.UpdateCommonModel<JobEntity>(item);
                                AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                            }
                        }
                    }
                    else
                    {
                        Logger.Log($"获取计划任务失败：{result.Msg}", LogLevel.Error);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log($"监控计划执行出现异常：{ex.ToString()}", LogLevel.Exception, ex);
                }
            }
        }

        private async void Logger_LogWrite(object sender, Model.Common.LogEventArgs args)
        {
            try
            {
                await Dispatcher.InvokeAsync(() =>
                  {
                      Log.AddLogs(args.Content, args.LogLevel);

                  });
            }
            catch (Exception ex)
            {
            }
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            var result = AppSession.Dal.GetCommonModelByCondition<JobEntity>("");
            if (result.Success)
            {
                DGMain.ItemsSource = result.Data;
            }
            else
            {
                MessageBox.Show($"查询失败：{result.Msg}");
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            WinJobAddOrEdit win = new WinJobAddOrEdit(null);
            win.ShowDialog();
            Query();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null || DGMain.SelectedItems.Count > 1)
            {
                MessageBox.Show("请选中一条数据");
            }
            else
            {
                WinJobAddOrEdit win = new WinJobAddOrEdit(((JobEntity)DGMain.SelectedItem).Id);
                win.ShowDialog();
                Query();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中数据");
            }
            else
            {
                if (MessageBox.Show("是否确认删除", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<JobEntity> jobs = new List<JobEntity>();
                    foreach (var item in DGMain.SelectedItems)
                    {
                        jobs.Add((JobEntity)item);
                    }
                    string sql = "delete from job where id in @ids";
                    var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql, new { ids = jobs.Select(t => t.Id).ToList() });
                    if (result.Success)
                    {
                        MessageBox.Show("删除计划成功");
                        Query();
                    }
                    else
                    {
                        MessageBox.Show($"删除计划失败：{result.Msg}");
                    }
                }
            }
        }

        private void BtnExcute_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中数据再进行操作");
            }
            else
            {
                List<JobEntity> jobs = new List<JobEntity>();
                foreach (var item in DGMain.SelectedItems)
                {
                    jobs.Add((JobEntity)item);
                }
                string sql = "update job set status = 1 where id in @ids";
                var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql, new { ids = jobs.Select(t => t.Id).ToList() });
                if (result.Success)
                {
                    MessageBox.Show("启动计划成功");
                    Query();
                }
                else
                {
                    MessageBox.Show($"启动计划失败：{result.Msg}");
                }
            }
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选中数据再进行操作");
            }
            else
            {
                List<JobEntity> jobs = new List<JobEntity>();
                foreach (var item in DGMain.SelectedItems)
                {
                    jobs.Add((JobEntity)item);
                }
                string sql = "update job set status = 0 where id in @ids";
                var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql, new { ids = jobs.Select(t => t.Id).ToList() });
                if (result.Success)
                {
                    MessageBox.Show("停止计划成功");
                    Query();
                }
                else
                {
                    MessageBox.Show($"停止计划失败：{result.Msg}");
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _scheduler.PauseAll();
            _scheduler.Shutdown();
            App.Current.Shutdown();
        }

        private async void BtnExcuteOnce_Click(object sender, RoutedEventArgs e)
        {
            if (DGMain.SelectedItem == null || DGMain.SelectedItems.Count > 1)
            {
                MessageBox.Show("请选中一条数据");
            }
            else
            {
                foreach (var item in DGMain.SelectedItems)
                {
                    BllResult result = await AppSession.JobService.ExcuteJobAsync(((JobEntity)item).Id.Value, App.Client);
                    if (result.Success)
                    {
                        MessageBox.Show($"执行成功,返回值为:{result.Msg}");
                    }
                    else
                    {
                        MessageBox.Show($"执行失败，返回值为：{result.Msg}");
                    }
                }
            }
        }
    }
}
