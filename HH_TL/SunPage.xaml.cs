using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HH_TL
{
    public delegate void ChangeTextHandler();
    /// <summary>
    /// SunPage.xaml 的交互逻辑
    /// </summary>
    public partial class SunPage : Window
    {
        public event ChangeTextHandler ChangeTextEvent;
        
        public SunPage()
        {
            InitializeComponent();
        }

        private void Button_Click_N(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Y(object sender, RoutedEventArgs e)
        {
            int fl = 0;
            if (!int.TryParse(TextBox.Text,out fl))
            {
                MessageBox.Show("请输入小数已保存损耗率");
            }

            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["loss"].Value = fl.ToString();
            cfa.Save();
            App.loss = fl;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            TextBox.Text = App.loss.ToString(); ;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            StrikeEvent();
        }
        //触发事件改变MainWindow的值
        private void StrikeEvent()
        {
            if (ChangeTextEvent != null)
            {
                ChangeTextEvent();
            }
        }

        //isDigit是否是数字
        public static bool isNumberic(string _string)
        {
            if (string.IsNullOrEmpty(_string))
                return false;
            foreach (char c in _string)
            {
                
                if (!char.IsDigit(c) && c != '.')
                    return false;
            }
            return true;
        }

        private void intervalBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void intervalBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isNumberic(e.Text))
            {
                e.Handled = true;
            }
            else
                e.Handled = false;
        }

        private void intervalBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumberic(text))
                { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        
    }
}

