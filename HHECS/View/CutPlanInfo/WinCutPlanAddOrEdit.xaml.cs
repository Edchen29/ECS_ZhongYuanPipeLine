using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums.PipeLine;
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

namespace HHECS.View.CutPlanInfo
{
    /// <summary>
    /// WinCutPlanAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinCutPlanAddOrEdit : BaseWindow
    {

        public int? Id { get; set; }

        public CutPlan CurrentCutPlan
        {
            get { return (CutPlan)GetValue(CurrentCutPlanProperty); }
            set { SetValue(CurrentCutPlanProperty, value); }
        }

        public static readonly DependencyProperty CurrentCutPlanProperty =
            DependencyProperty.Register("CurrentCutPlanProperty", typeof(CutPlan), typeof(WinCutPlanAddOrEdit), new PropertyMetadata(new CutPlan()));

        //public CutPlan HHLED { get; set; } = new CutPlan();
        public WinCutPlanAddOrEdit(int? id)
        {
            InitializeComponent();
            this.Id = id;
            this.Title = id == null ? "新增" : "编辑";
            Init();
            this.GridMain.DataContext = CurrentCutPlan;
        }

        private void Init()
        {
            var CutPlanStatusList = CommonHelper.EnumListDic<CutPlanStatus>("");
            cbx_Status.ItemsSource = CutPlanStatusList;
            cbx_Status.DisplayMemberPath = "Value";
            cbx_Status.SelectedValuePath = "Key";
            if (Id == null)
            {
                //新增
            }
            else
            {
                BllResult<List<CutPlan>> result = AppSession.Dal.GetCommonModelByCondition<CutPlan>($"where id ={Id}");
                if (result.Success)
                {
                    CurrentCutPlan = result.Data[0];
                }
                else
                {
                    MessageBox.Show($"未能获取到id为{Id}，错误消息：{result.Msg}");
                }
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCutPlan.Id == null)
            {
                CurrentCutPlan.CreateTime = DateTime.Now;
                CurrentCutPlan.CreateBy = App.User.UserCode;
                var a = AppSession.Dal.InsertCommonModel<CutPlan>(CurrentCutPlan);
                if (a.Success)
                {
                    MessageBox.Show("新增成功");
                }
                else
                {
                    MessageBox.Show($"新增失败：{a.Msg}");
                }

            }
            else
            {
                CurrentCutPlan.CreateTime = DateTime.Now;
                CurrentCutPlan.UpdateBy = App.User.UserCode;
                var a = AppSession.Dal.UpdateCommonModel<CutPlan>(CurrentCutPlan);
                if (a.Success)
                {
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show($"更新失败：{a.Msg}");
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
