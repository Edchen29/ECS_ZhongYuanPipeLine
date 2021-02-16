using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
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

namespace HHECS.View.UserOperation
{
    /// <summary>
    /// Role.xaml 的交互逻辑
    /// </summary>
    public partial class WinRole : HideCloseWindow
    {
        public WinRole()
        {
            InitializeComponent();
            AppSession.BllService.CheckPermission(App.MenuOperations, new List<UIElement>() { BtnDelete, BtnEdit, BtnNew, BtnUpdate });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BllResult<List<Role>> result = AppSession.BllService.GetAllRole();
            if (result.Success)
            {
                DatagridMain.ItemsSource = result.Data;
            }
            else
            {
                MessageBox.Show("获取角色信息失败:" + result.Msg);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(null, null);
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            WinRoleAddOrEdit roleAddOrEdit = new WinRoleAddOrEdit(null);
            roleAddOrEdit.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridMain.SelectedItem == null)
            {
                MessageBox.Show("请先选中数据");
            }
            else
            {
                Role role = (Role)DatagridMain.SelectedItem;
                WinRoleAddOrEdit roleAddOrEdit = new WinRoleAddOrEdit(role.Id);
                roleAddOrEdit.ShowDialog();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DatagridMain.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选中一条数据！");
            }
            else
            {
                List<Role> roles = new List<Role>();
                foreach (var item in DatagridMain.SelectedItems)
                {
                    roles.Add((Role)item);
                }
                BllResult result = AppSession.BllService.DeleteRoleByIds(roles.Select(t => t.Id.Value).ToList());
                if (result.Success)
                {
                    MessageBox.Show("删除成功");
                }
                else
                {
                    MessageBox.Show($"删除失败：{result.Msg}");
                }
                Window_Loaded(null, null);
            }
        }
    }
}
