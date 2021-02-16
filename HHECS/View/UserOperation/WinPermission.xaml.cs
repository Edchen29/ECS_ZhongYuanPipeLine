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
    /// Permission.xaml 的交互逻辑
    /// </summary>
    public partial class WinPermission : HideCloseWindow
    {
        public List<MenuOperation> MenuOperations { get; set; }
        public MenuOperation CurrentMenuOperation { get; set; } = new MenuOperation();
        public List<DictDetail> DictDetails { get; set; }
        public WinPermission()
        {
            InitializeComponent();
            this.GridDetail.DataContext = CurrentMenuOperation;
            AppSession.BllService.CheckPermission(App.MenuOperations, new List<UIElement>() { BtnCancel, BtnDelete, BtnNew,BtnCancel,BtnSave,BtnUpdate });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var result = AppSession.BllService.GetAllMenuOperation();
            if (result.Success)
            {
                MenuOperations = result.Data.OrderBy(t=>t.OrderNum).ToList();
            }
            else
            {
                MessageBox.Show("获取操作权限和菜单出错");
                return;
            }
            //组合层级菜单
            AppSession.BllService.Combine(MenuOperations.FindAll(t => t.ParentId == null),MenuOperations);
            treeMain.ItemsSource = MenuOperations.FindAll(t => t.ParentId == null);
            BllResult<Dict> a = AppSession.BllService.GetDictWithDetails("menuType");
            if (a.Success)
            {
                CBType.ItemsSource = DictDetails = a.Data.DictDetails;
                CBType.DisplayMemberPath = "Name";
                CBType.SelectedValuePath = "Code";
                CBType.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("获取菜单类型出错");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuOperation o = (MenuOperation)treeMain.SelectedItem;
            if (o?.MenuType == "按钮")
            {
                MessageBox.Show("按钮类型无法增加子节点");
                return;
            }
            CurrentMenuOperation.Id = null;
            CurrentMenuOperation.MenuName = "";
            CurrentMenuOperation.ParentId = o?.Id;
            CurrentMenuOperation.Url = "";
            CurrentMenuOperation.MenuType = "";
            CurrentMenuOperation.Perms = "";
            CurrentMenuOperation.Remark = "";
            CurrentMenuOperation.OrderNum = 0;
            CurrentMenuOperation.Created = DateTime.Now;
            CurrentMenuOperation.CreatedBy = App.User?.UserName;
            CurrentMenuOperation.Updated = null;
            CurrentMenuOperation.UpdatedBy = "";
        }

        private void treeMain_Selected(object sender, RoutedEventArgs e)
        {
            MenuOperation o = (MenuOperation)treeMain.SelectedItem;
            CurrentMenuOperation.Id = o.Id;
            CurrentMenuOperation.MenuName = o.MenuName;
            CurrentMenuOperation.ParentId = o.ParentId;
            CurrentMenuOperation.Url = o.Url;
            CurrentMenuOperation.MenuType = o.MenuType;
            CurrentMenuOperation.Perms = o.Perms;
            CurrentMenuOperation.Remark = o.Remark;
            CurrentMenuOperation.OrderNum = o.OrderNum;
            CurrentMenuOperation.Created = o.Created;
            CurrentMenuOperation.CreatedBy = o.CreatedBy;
            CurrentMenuOperation.Updated = DateTime.Now;
            CurrentMenuOperation.UpdatedBy = App.User?.UserName;
            CBType.SelectedIndex = DictDetails.FindIndex(t => t.Code == CurrentMenuOperation.MenuType);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MenuOperation o = (MenuOperation)treeMain.SelectedItem;
            if (o == null)
            {
                MessageBox.Show("请先选中节点");
            }
            else
            {
                if (o.Id == null)
                {
                    MessageBox.Show("Id不存在，无法删除");
                    return;
                }
                if (MessageBox.Show("是否确认删除？", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //级联查出所有待删的Id;
                    List<int> ids = new List<int>();
                    ids.Add(CurrentMenuOperation.Id.Value);
                    AppSession.BllService.GetMenuOperationIds(new List<MenuOperation>() { CurrentMenuOperation },MenuOperations, ids);
                    BllResult result = AppSession.BllService.DeleteMenuOperationByIds(ids);
                    if (result.Success)
                    {
                        MessageBox.Show("删除成功");
                    }
                    else
                    {
                        MessageBox.Show("删除失败" + result.Msg);
                    }

                }
                Window_Loaded(null, null);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //至少名称和类型不能为空
            if (String.IsNullOrWhiteSpace(CurrentMenuOperation.MenuName) || String.IsNullOrWhiteSpace(CurrentMenuOperation.MenuType))
            {
                MessageBox.Show("名称和类型不能为空");
            }
            switch (CurrentMenuOperation.MenuType)
            {
                case "menu":
                    CurrentMenuOperation.Icon = "/Content/Icon/菜单.png";
                    break;
                case "button":
                    CurrentMenuOperation.Icon = "/Content/Icon/按钮.png";
                    break;
                case "catalog":
                    CurrentMenuOperation.Icon = "/Content/Icon/目录.png";
                    break;
                default:
                    CurrentMenuOperation.Icon = "/Content/Icon/未识别.png";
                    break;
            }
            if (CurrentMenuOperation.Id == null)
            {
                //新增
                BllResult<MenuOperation> result = AppSession.BllService.SaveMenuOperation(CurrentMenuOperation);
                if (result.Success)
                {
                    MessageBox.Show("新增成功");
                    CurrentMenuOperation.Id = result.Data.Id;
                }
                else
                {
                    MessageBox.Show("新增失败" + result.Msg);
                }
            }
            else
            {
                //更新
                BllResult result = AppSession.BllService.UpdateMenuOperation(CurrentMenuOperation);
                if (result.Success)
                {
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show("更新失败" + result.Msg);
                }

            }
            Window_Loaded(null,null);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MenuOperation o = (MenuOperation)treeMain.SelectedItem;
            if (o != null)
            {
                CurrentMenuOperation.Id = o.Id;
                CurrentMenuOperation.MenuName = o.MenuName;
                CurrentMenuOperation.ParentId = o.ParentId;
                CurrentMenuOperation.Url = o.Url;
                CurrentMenuOperation.MenuType = o.MenuType;
                CurrentMenuOperation.Perms = o.Perms;
                CurrentMenuOperation.Remark = o.Remark;
                CurrentMenuOperation.Created = o.Created;
                CurrentMenuOperation.CreatedBy = o.CreatedBy;
                CurrentMenuOperation.Updated = DateTime.Now;
                CurrentMenuOperation.UpdatedBy = App.User?.UserName;
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(null, null);
        }
    }
}
