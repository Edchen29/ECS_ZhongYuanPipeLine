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
    /// UserAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinUserAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }
        public User CurrentUser { get; set; } = new User();
        public List<UserEditVM> RoleList { get; set; }
        public WinUserAddOrEdit(int? id)
        {
            InitializeComponent();
            this.Id = id;
            if (id == null)
            {
                this.Title = "新增";
            }
            else
            {
                this.Title = "编辑";
            }
            Init();
            this.GridMain.DataContext = CurrentUser;
        }

        private void Init()
        {
            //获取所有角色
            var result = AppSession.BllService.GetAllRole();
            if (result.Success)
            {
                RoleList = result.Data.Select(t => new UserEditVM()
                {
                    Selected = false,
                    Id = t.Id.Value,
                    RoleName = t.RoleName
                }).ToList();
                if (Id == null)
                {
                    //说明是新增
                    ListViewMain.ItemsSource = RoleList;
                }
                else
                {
                    //说明是更新
                    TxtUserCode.IsReadOnly = true;
                    TxtUserName.IsReadOnly = true;
                    BllResult<List<User>> result2 = AppSession.BllService.GetUserByCondition($"where id = {Id}");
                    if (result2.Success)
                    {
                        User tempUser = result2.Data[0];
                        CurrentUser.Id = tempUser.Id;
                        CurrentUser.UserCode = tempUser.UserCode;
                        CurrentUser.UserName = tempUser.UserName;
                        CurrentUser.Password = tempUser.Password;
                        CurrentUser.Partment = tempUser.Partment;
                        CurrentUser.Address = tempUser.Address;
                        CurrentUser.Phone = tempUser.Phone;
                        CurrentUser.Remark = tempUser.Remark;
                        CurrentUser.Disable = tempUser.Disable;
                        CurrentUser.Created = tempUser.Created;
                        CurrentUser.CreatedBy = tempUser.CreatedBy;
                        CurrentUser.Updated = tempUser.Updated;
                        CurrentUser.UpdatedBy = tempUser.UpdatedBy;
                        //获取他现在所拥有的角色
                        var result3 = AppSession.BllService.GetUserWithRoles(tempUser.UserCode, tempUser.Password,App.WarehouseCode,App.Client,App.Url?.DictDetails);
                        if (result3.Success)
                        {
                            RoleList.ForEach(t =>
                            {
                                t.Selected = result3.Data.Roles.Count(i => i.Id == t.Id) > 0;
                            });
                            ListViewMain.ItemsSource = RoleList;
                        }
                        else
                        {
                            MessageBox.Show($"获取用户角色失败：{result3.Msg}");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"查询用户失败：{result2.Msg}");
                    }
                }
            }
            else
            {
                MessageBox.Show("获取角色信息出错");
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //用户名、编码、密码和角色必要
            if (String.IsNullOrWhiteSpace(CurrentUser.UserName) || String.IsNullOrWhiteSpace(CurrentUser.UserCode) || String.IsNullOrWhiteSpace(CurrentUser.Password) || RoleList.Count(t => t.Selected == true) == 0)
            {
                MessageBox.Show("要求必须提供用户名、用户编码、密码和至少一种角色");
                return;
            }
            BllResult<User> result = null;
            if (CurrentUser.Id == null)
            {
                //新增
                result = AppSession.BllService.SaveUserWithRoles(CurrentUser, RoleList.Where(t => t.Selected == true).Select(t => t.Id).ToList());
            }
            else
            {
                //更新
                result = AppSession.BllService.UpdateUserWithRoles(CurrentUser, RoleList.Where(t => t.Selected == true).Select(t => t.Id).ToList());
            }
            if (result.Success)
            {
                MessageBox.Show("保存成功");
                CurrentUser.Id = result.Data.Id;
            }
            else
            {
                MessageBox.Show($"保存失败:{result.Msg}");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
