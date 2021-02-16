using HHECS.Bll;
using HHECS.Controls;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Controls;
using HHECS.Model.Entities;
using HHECS.View.Win;
using HHECS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace HHECS.View.UserOperation
{
    /// <summary>
    /// User.xaml 的交互逻辑
    /// </summary>
    public partial class WinUser : HideCloseWindow
    {
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        public WinUser()
        {
            InitializeComponent();
            AppSession.BllService.CheckPermission(App.MenuOperations, SPBtns.Children);
            this.page.DataContext = PageInfo;
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void Query()
        {
            string sql = "where 1=1 ";
            if (!String.IsNullOrWhiteSpace(TxtUserCode.Text))
            {
                sql += $" and userCode like '{TxtUserCode.Text}%'";
            }
            if (!String.IsNullOrWhiteSpace(TxtUserName.Text))
            {
                sql += $" and userName like '{TxtUserName.Text}%'";
            }
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<User>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                var a = AppSession.Dal.GetCommonModeByPageCondition<User>(PageInfo.PageIndex, PageInfo.PageSize, sql, "");
                if (a.Success)
                {
                    DatagridMain.ItemsSource = a.Data;
                }
                else
                {
                    MessageBox.Show($"查询出错：{a.Msg}");
                }
            }
            else
            {
                MessageBox.Show($"查询失败:{result.Msg}");
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            WinUserAddOrEdit user = new WinUserAddOrEdit(null);
            user.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中一条数据");
            }
            else
            {
                User user = (User)DatagridMain.SelectedItem;
                WinUserAddOrEdit userAddOrEdit = new WinUserAddOrEdit(user.Id);
                userAddOrEdit.ShowDialog();
            }
             Query();
        }

        private void BtnDisable_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选中数据");
            }
            else
            {
                var temp = new List<User>();
                foreach (var item in DatagridMain.SelectedItems)
                {
                    temp.Add((User)item);
                }
                BllResult result = AppSession.BllService.SetUserDisable(temp.Select(t => t.Id).ToList(),1);
                if (result.Success)
                {
                    MessageBox.Show("禁用成功");
                }
                else
                {
                    MessageBox.Show("禁用失败");
                }
            }
            Query();
        }

        private void BtnEnable_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选中数据");
            }
            else
            {
                var temp = new List<User>();
                foreach (var item in DatagridMain.SelectedItems)
                {
                    temp.Add((User)item);
                }
                BllResult result = AppSession.BllService.SetUserDisable(temp.Select(t => t.Id).ToList(),0);
                if (result.Success)
                {
                    MessageBox.Show("启用成功");
                }
                else
                {
                    MessageBox.Show("启用失败");
                }
            }
            Query();
        }

        private void page_PageChanged(object sender, PageChangedEventArgs e)
        {
            PageInfo.PageIndex = e.CurrentPageIndex;
            Query();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选中数据");
            }
            else
            {
                List<int> ids=new List<int> { } ;
                var temp = new List<User>();
                foreach (var item in DatagridMain.SelectedItems)
                {
                    User user = (User)item;
                    if (user != null)
                    {
                        ids.Add((int)user.Id);
                    }
                }

                BllResult result = AppSession.Dal.DeleteCommonModelByIds<User>(ids);
                if (result.Success)
                {
                    MessageBox.Show("删除成功");
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
            Query();
        }
    }
}
