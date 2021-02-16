using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.View.Win;
using HHECS.ViewModel;
using System;
using System.Windows;

namespace HHECS.View.SysLogInfo
{
    /// <summary>
    /// todo:日志界面待完善
    /// </summary>
    public partial class WinLogInfo : HideCloseWindow
    {
        public PageInfoVM PageInfo1 { get; set; } = new PageInfoVM();
        public PageInfoVM PageInfo2 { get; set; } = new PageInfoVM();

        public WinLogInfo()
        {
            InitializeComponent();
        }

        private void HideCloseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DTBegin1.SelectedDate = DateTime.Now.AddDays(-1);
            DTEnd1.SelectedDate = DateTime.Now.AddDays(1);

            DTBegin2.SelectedDate = DateTime.Now.AddDays(-1);
            DTEnd2.SelectedDate = DateTime.Now.AddDays(1);

            Page1.DataContext = PageInfo1;
            Page2.DataContext = PageInfo2;

            Cbx_Title.ItemsSource = CommonHelper.EnumListDic<LogTitle>("全部", "All");
            Cbx_Title.DisplayMemberPath = "Key";
            Cbx_Title.SelectedValuePath = "Key";
            Cbx_Title.SelectedIndex = 0;

            Cbx_Falg.ItemsSource = CommonHelper.EnumListDic<LogLevel>("全部", "All");
            Cbx_Falg.DisplayMemberPath = "Key";
            Cbx_Falg.SelectedValuePath = "Key";
            Cbx_Falg.SelectedIndex = 0;
        }

        #region 接口日志

        private void Page1_PageChanged(object sender, Model.Controls.PageChangedEventArgs e)
        {
            Query1();
        }

        private void BtnQuery1_Click(object sender, RoutedEventArgs e)
        {
            Query1();
        }

        public void Query1()
        {
            string sql = "where 1=1 and created >@begin and created <@end";
            if (!string.IsNullOrWhiteSpace(Txt1.Text))
            {
                sql += $" and InterfaceName like '{Txt1.Text}%'";
            }

            BllResult<int> result = AppSession.Dal.GetCommonModelCount<InterfaceLog>(sql, new { begin = DTBegin1.SelectedDate, end = DTEnd1.SelectedDate.Value.AddDays(1) });
            if (result.Success)
            {
                PageInfo1.TotalCount = result.Data;
                var temp = AppSession.Dal.GetCommonModeByPageCondition<InterfaceLog>(PageInfo1.PageIndex, PageInfo1.PageSize, sql, "id desc", new { begin = DTBegin1.SelectedDate, end = DTEnd1.SelectedDate.Value.AddDays(1) });
                DG1.ItemsSource = temp.Data;
            }
        }

        #endregion

        #region 内容日志

        private void Page2_PageChanged(object sender, Model.Controls.PageChangedEventArgs e)
        {
            Query2();
        }

        private void BtnQuery2_Click(object sender, RoutedEventArgs e)
        {
            Query2();
        }

        public void Query2()
        {
            string sql = "where 1=1 and created >@begin and created <@end";
            if (!(Cbx_Title.SelectedValue.ToString() == "全部"))
            {
                sql += $" and Title = '{Cbx_Title.SelectedValue}'";
            }
            if (!string.IsNullOrWhiteSpace(Txt2.Text))
            {
                sql += $" and content like '%{Txt2.Text}%'";
            }
            if (!(Cbx_Falg.SelectedValue.ToString() == "全部"))
            {
                sql += $" and flag = '{Cbx_Falg.SelectedValue}'";
            }
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<ContentLog>(sql, new { begin = DTBegin2.SelectedDate, end = DTEnd2.SelectedDate.Value.AddDays(1) });
            if (result.Success)
            {
                PageInfo2.TotalCount = result.Data;
                var temp = AppSession.Dal.GetCommonModeByPageCondition<ContentLog>(PageInfo2.PageIndex, PageInfo2.PageSize, sql, "id desc", new { begin = DTBegin2.SelectedDate, end = DTEnd2.SelectedDate.Value.AddDays(1) });
                if (temp.Success)
                {
                    DG2.ItemsSource = temp.Data;
                }
                else
                {
                    MessageBox.Show($"查询失败：{temp.Msg}");
                }

            }
            else
            {
                MessageBox.Show($"失败{result.Msg}");
            }
        }

        #endregion




    }
}
