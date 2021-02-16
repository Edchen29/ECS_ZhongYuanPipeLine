using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HHECS.View.Win
{
    /// <summary>
    /// 关闭时自动隐藏的window
    /// </summary>
    public class HideCloseWindow : BaseWindow
    {
        public HideCloseWindow()
        {
            this.Closing += MyWindow_Closing;
        }

        private void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
