using HHECS.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HHECS.View.Win
{
    public class BaseWindow : Window
    {
        public BaseWindow()
        {
            this.Loaded += BaseWindow_Loaded;
            //统一字体大小
            this.FontSize = 16;
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Content/Image/favicon.ico", UriKind.RelativeOrAbsolute));
        }

        private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var temp = AppSession.Bll.FindMenuOperation(AppSessitn.User.Roles);
            //AppSession.CheckPermission(temp.Success?temp.Data:new List<MenuOperation>(),)
        }

        ///// <summary>
        ///// 遍历控件及其值
        ///// </summary>
        ///// <param name="uiControls">界面控件</param>
        //private void SetNotEditable(UIElementCollection uiControls)
        //{
        //    foreach (UIElement element in uiControls)
        //    {

        //        if (element is TextBox)
        //        {
        //            TextBox txtBox = (element as TextBox);
        //        }
        //        else if (element is Grid)
        //        {
        //            this.SetNotEditable((element as Grid).Children);
        //        }
        //    }
        //}
    }
}
