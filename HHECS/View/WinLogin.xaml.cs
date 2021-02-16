using HHECS.Bll;
using HHECS.View.Win;
using System;
using System.Windows;

namespace HHECS.View
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class WinLogin : BaseWindow
    {
        public WinLogin()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txt_UserName.Text))
            {
                MessageBox.Show("用户编码不能为空");
            }
            else
            {
                var a = AppSession.BllService.GetUserWithRoles(txt_UserName.Text, Password.Password,App.WarehouseCode,App.Client,App.Url?.DictDetails);
                if (a.Success)
                {
                    App.User = a.Data;
                    App.MenuOperations = AppSession.BllService.FindMenuOperation(App.User.Roles).Data;
                    WinMain main = new WinMain();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(a.Msg);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
