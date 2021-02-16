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
using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using MahApps.Metro.Controls;

namespace HHECS.TimerClient
{
    /// <summary>
    /// WinJobAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinJobAddOrEdit : MetroWindow
    {
        public int? Id { get; set; }

        public WinJobAddOrEdit(int? id)
        {
            InitializeComponent();
            Id = id;
            Init();
            this.Title = id == null ? "新增" : "编辑";
            CBStatus.ItemsSource = CommonHelper.EnumListDic<JobStatus>("");
            CBStatus.DisplayMemberPath = "Key";
            CBStatus.SelectedValuePath = "Value";
        }

        private void Init()
        {
            if (Id == null)
            {
                //新增
                this.GridMain.DataContext = new JobEntity();
            }
            else
            {
                //编辑
                BllResult<List<JobEntity>> result = AppSession.Dal.GetCommonModelByCondition<JobEntity>($"where id ={Id}");
                if (result.Success)
                {
                    this.GridMain.DataContext = result.Data[0];
                }
                else
                {
                    MessageBox.Show($"查询计划任务数据详情失败:{result.Msg}");
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var job = (JobEntity)GridMain.DataContext;
            if (job.Id == null)
            {
                //新增
                job.Created = DateTime.Now;
                job.CreatedBy = App.User.UserCode;
                var result = AppSession.Dal.InsertCommonModel<JobEntity>(job);
                if (result.Success)
                {
                    job.Id = result.Data;
                    MessageBox.Show("新增成功");
                }
                else
                {
                    MessageBox.Show($"新增失败{result.Msg}");
                }
            }
            else
            {
                //编辑
                job.Updated = DateTime.Now;
                job.UpdatedBy = App.User.UserCode;
                var result = AppSession.Dal.UpdateCommonModel<JobEntity>(job);
                if (result.Success)
                {
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show($"更新失败{result.Msg}");
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
