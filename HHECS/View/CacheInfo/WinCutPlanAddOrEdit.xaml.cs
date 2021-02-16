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

namespace HHECS.View.CacheInfo
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

        public StationCache Head { get; set; }
        public WinCutPlanAddOrEdit(int? id, StationCache head)
        {
            InitializeComponent();
            this.Id = id;
            this.Head = head;
            this.Title = id == null ? "新增" : "编辑";
            Init();
            this.GridMain.DataContext = CurrentCutPlan;
        }



        private void Init()
        {
            ////管材缓存状态列表
            //var StationCacheStatusList = CommonHelper.EnumListDic<StationCacheStatus>("");
            //cbx_Status.ItemsSource = StationCacheStatusList;
            //cbx_Status.DisplayMemberPath = "Value";
            //cbx_Status.SelectedValuePath = "Key";
            //管材缓存状态列表
            var CutPlanStatusList = CommonHelper.EnumListDic<CutPlanStatus>("");
            cbx_Status.ItemsSource = CutPlanStatusList;
            cbx_Status.DisplayMemberPath = "Value";
            cbx_Status.SelectedValuePath = "Key";
            if (Id == null)
            {
                //新增
                CurrentCutPlan.stationCacheId = (int)Head.Id;
            }
            else
            {
                //BtnNew.Visibility = Visibility.Hidden;
                //编辑，编码不能改变
                //TxtEquipmentTypeTemplateCode.IsReadOnly = true;
                var a = AppSession.Dal.GetCommonModelByCondition<CutPlan>($"where id = {Id}");
                if (a.Success)
                {

                    CurrentCutPlan = a.Data[0];
                    //

                    CurrentCutPlan.stationCacheId = (int)Head.Id;
                }
                else
                {
                    MessageBox.Show($"未能获取到id为{Id}的数据，错误消息：{a.Msg}");
                    this.Close();
                }

            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //这里查下主表还存不存在，防止并发删除；
            var result = AppSession.Dal.GetCommonModelByCondition<StationCache>($"where id = {Head.Id}");
            if (!result.Success)
            {
                MessageBox.Show($"ID为{Head.Id}的工单已被删除，请检查，错误信息：{result.Msg}");
            }
            else
            {
                if (CurrentCutPlan.Id == null)
                {
                    //新增
                    CurrentCutPlan.stationCacheId = (int)Head.Id;
                    CurrentCutPlan.CreateTime = DateTime.Now;
                    CurrentCutPlan.CreateBy = App.User.UserCode;
                    BllResult<int?> a = AppSession.Dal.InsertCommonModel<CutPlan>(CurrentCutPlan);
                    if (a.Success)
                    {
                        MessageBox.Show("新增成功");
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"新增失败：{a.Msg}");
                    }
                }
                else
                {
                    //更新
                    CurrentCutPlan.UpdateTime = DateTime.Now;
                    CurrentCutPlan.UpdateBy = App.User.UserCode;
                    BllResult a = AppSession.Dal.UpdateCommonModel<CutPlan>(CurrentCutPlan);
                    if (a.Success)
                    {
                        MessageBox.Show("更新成功");
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"更新失败：{a.Msg}");
                    }

                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
