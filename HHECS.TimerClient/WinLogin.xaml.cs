using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using HHECS.Bll;
using MahApps.Metro.Controls;

namespace HHECS.TimerClient
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class WinLogin : MetroWindow
    {
        public WinLogin()
        {
            
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen; //运行程序时出现在屏幕中间           
            #region 调用放大动画 传入控件名称 位置 开始大小和最终效果
            ScaleEasingAnimationShow(image_logo, new Point(0, -5), 0, 1);
            #endregion
        }
        #region logo放大动画
        /// <summary>
        /// logo放大动画
        /// </summary>
        /// <param name="element">控件名</param>
        /// <param name="point">元素开始动画的位置</param>
        /// <param name="from">元素开始的大小</param>
        /// <param name="from">元素到达的大小</param>
        public static void ScaleEasingAnimationShow(FrameworkElement element, Point point, double from, double to)
        {
            lock (element)
            {
                ScaleTransform scale = new ScaleTransform();
                element.RenderTransform = scale;
                element.RenderTransformOrigin = point;//定义圆心位置        
                EasingFunctionBase easeFunction = new PowerEase()
                {
                    EasingMode = EasingMode.EaseOut,
                    Power = 5
                };
                DoubleAnimation scaleAnimation = new DoubleAnimation()
                {
                    From = from,                                   //起始值
                    To = to,                                     //结束值
                    EasingFunction = easeFunction,                    //缓动函数
                    Duration = new TimeSpan(0, 0, 0, 2, 5)  //动画播放时间
                };
                AnimationClock clock = scaleAnimation.CreateClock();
                scale.ApplyAnimationClock(ScaleTransform.ScaleXProperty, clock);
                scale.ApplyAnimationClock(ScaleTransform.ScaleYProperty, clock);
            }
        }
        #endregion

        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txt_UserName.Text) || string.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("用户编码或密码不能为空","提示",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                var a = AppSession.BllService.GetUserWithRoles(txt_UserName.Text, Password.Password);
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
                    MessageBox.Show($"登录失败：{a.Msg}","提示",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
