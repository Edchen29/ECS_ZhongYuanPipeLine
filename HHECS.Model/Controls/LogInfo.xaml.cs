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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HHECS.Model.Enums;

namespace HHECS.Model.Controls
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class LogInfo : UserControl
    {
        public LogInfo(double maxW, double maxH)
        {
            InitializeComponent();
            this.Height = maxH;
            this.Width = maxW;
        }

        public LogInfo(double maxW)
        {
            InitializeComponent();
            this.Width = maxW;
        }


        public LogInfo(int margin = 0)
        {
            InitializeComponent();
            this.Margin = new Thickness(margin);
        }

        public LogInfo()
        {
            InitializeComponent();
            this.Margin = new Thickness(0);
        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="level">1显示绿色，2显示红色,3黄色</param>
        public void AddLogs(string log, LogLevel level)
        {
            TextBlock textBlock = new TextBlock {
                Text = DateTime.Now.ToLongTimeString() + ":" + log,
                Background = Brushes.White
            };
            switch (level)
            {
                case LogLevel.Info:
                    textBlock.Background = Brushes.White;
                    break;
                case LogLevel.Error:
                    textBlock.Background = Brushes.Red;
                    break;
                case LogLevel.Warning:
                    textBlock.Background = Brushes.Yellow;
                    break;
                case LogLevel.Success:
                    textBlock.Background = Brushes.LightGreen;
                    break;
                case LogLevel.Exception:
                    textBlock.Background = Brushes.Red;
                    break;
                default:
                    textBlock.Background = Brushes.White;
                    break;
            }
            textBlock.SetBinding(TextBlock.MaxWidthProperty, new Binding("ActualWidth") { Source = this });
            textBlock.TextWrapping = TextWrapping.Wrap;
            this.list_Log.Items.Add(textBlock);
            this.list_Log.SelectedIndex = this.list_Log.Items.Count - 1;
            this.list_Log.ScrollIntoView(this.list_Log.SelectedItem);
            if (list_Log.Items.Count > 50)
            {
                list_Log.Items.RemoveAt(0);
            }
        }
    }
}
