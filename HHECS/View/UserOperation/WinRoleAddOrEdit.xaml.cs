using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
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

namespace HHECS.View.UserOperation
{
    /// <summary>
    /// RoleAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinRoleAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }
        public Role CurrentRole { get; set; } = new Role()
        {
            RoleName = "",
            Remark = ""
        };
        public List<MenuOperation> AllMenuOperations { get; set; }
        public List<MenuOperation> HasMenuOperations { get; set; }
        public WinRoleAddOrEdit(int? id)
        {
            this.Id = id;
            InitializeComponent();
            if (id != null)
            {
                this.Title = "编辑";
            }
            else
            {
                this.Title = "新增";
            }
            Init();
        }

        public void Init()
        {
            if (Id.HasValue)
            {
                BllResult<Role> result = AppSession.BllService.GetRoleById(Id.Value);
                if (result.Success)
                {
                    CurrentRole.Id = result.Data.Id;
                    CurrentRole.RoleName = result.Data.RoleName;
                    CurrentRole.Remark = result.Data.Remark;
                }
                else
                {
                    MessageBox.Show("未能获取指定角色");
                }
            }
         
            this.GridMain.DataContext = CurrentRole;
            BllResult<List<MenuOperation>> result2 = AppSession.BllService.GetAllMenuOperation();
            if (!result2.Success)
            {
                MessageBox.Show("未能获取权限值");
            }
            else
            {
                AllMenuOperations = result2.Data;
            }
            if (CurrentRole.Id != null)
            {
                result2 = AppSession.BllService.FindMenuOperation(new List<Role>() { CurrentRole });
                if (!result2.Success)
                {
                    MessageBox.Show("未能获取拥有的权限值");
                }
                else
                {
                    HasMenuOperations = result2.Data;
                    AllMenuOperations.ForEach(t => { t.HasPerm = HasMenuOperations.Count(i => i.Id == t.Id) > 0; });
                }
            }
            AppSession.BllService.Combine(AllMenuOperations.FindAll(t => t.ParentId == null).ToList(), AllMenuOperations);
            treeMain.ItemsSource = AllMenuOperations.FindAll(t => t.ParentId == null).ToList();
        }

        private void treeMain_Selected(object sender, RoutedEventArgs e)
        {
            //MenuOperation o = (MenuOperation)treeMain.SelectedItem;
            //if (o != null)
            //{
            //    o.HasPerm = !o.HasPerm;
            //}
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(CurrentRole.RoleName))
            {
                MessageBox.Show("角色名不能为空");
                return;
            }
            if (Id == null)
            {
                //说明是新增
                BllResult<Role> result = AppSession.BllService.InsertRoleAndMenuOperations(CurrentRole, AllMenuOperations.FindAll(t => t.HasPerm == true));
                if (result.Success)
                {
                    CurrentRole.Id = result.Data.Id;
                    Id = result.Data.Id;
                    MessageBox.Show("新增成功");
                }
                else
                {
                    MessageBox.Show("新增失败：" + result.Msg);
                }

            }
            else
            {
                //说明是更新
                BllResult<Role> result = AppSession.BllService.UpdateRoleAndMenuOperations(CurrentRole, AllMenuOperations.FindAll(t => t.HasPerm == true));
                if (result.Success)
                {
                    CurrentRole.Id = result.Data.Id;
                    Id = result.Data.Id;
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show("更新失败：" + result.Msg);
                }
            }
            Init();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int? id = (int?)cb.Tag;
            var temp = AllMenuOperations.Find(t => t.Id == id);
            List<int> ids = new List<int>();
            AppSession.BllService.GetMenuOperationIds(new List<MenuOperation> { temp }, AllMenuOperations, ids);
            AllMenuOperations.ForEach(t =>
            {
                if (ids.Count(i => i == t.Id) > 0)
                {
                    t.HasPerm = !t.HasPerm;
                }
            });
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
