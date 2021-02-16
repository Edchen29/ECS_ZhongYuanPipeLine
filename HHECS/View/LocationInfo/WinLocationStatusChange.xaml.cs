using HHECS.Bll;
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

namespace HHECS.View.LocationInfo
{
    /// <summary>
    /// Frm_LocationStatusChange.xaml 的交互逻辑
    /// </summary>
    public partial class WinLocationStatusChange : BaseWindow
    {
        public List<string> LocationCodes { get; set; }
        public WinLocationStatusChange(List<string> list)
        {
            InitializeComponent();
            LocationCodes = list;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Verify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql = "update wcslocation set status = '" + cbx_status.SelectedValue.ToString() + "' where code in (" + String.Join(",", LocationCodes.Select(t => "'" + t + "'").ToList()) + ")";
                var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql);
                if(result.Success)
                {
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show("更新失败:"+result.Success);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新出现异常：" + ex.ToString());
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_LocaiontIds.Text = String.Join(",", LocationCodes.ToArray());

            var temp = AppSession.BllService.GetDictWithDetails("LocationStatus");
            if (temp.Success)
            {
                var dictDetails = temp.Data.DictDetails.ToDictionary(t => t.Value, i => i.Name);
                cbx_status.ItemsSource = dictDetails;
                cbx_status.DisplayMemberPath = "Value";
                cbx_status.SelectedValuePath = "Key";
                cbx_status.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("加载状态失败");
            }
        }
    }
}
