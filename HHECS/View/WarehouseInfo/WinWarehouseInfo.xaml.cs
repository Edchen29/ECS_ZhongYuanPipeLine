using HHECS.Bll;
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

namespace HHECS.View.WarehouseInfo
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class WinWarehouseInfo : HideCloseWindow
    {
        public WinWarehouseInfo()
        {
            InitializeComponent();
        }
        public PageInfoVM PageInfo { get; set; } = new PageInfoVM();
        private void Btn_Query_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }
        private void Query()
        {

            string sql = " t where 1=1 ";
            if (!String.IsNullOrEmpty(txt_Code1.Text))
            {
                sql += " and t.code = '" + txt_Code1.Text + "'";
            }
           
           
            BllResult<int> result = AppSession.Dal.GetCommonModelCount<Warehouse>(sql);
            if (result.Success)
            {
                PageInfo.TotalCount = result.Data;
                dgv_1.ItemsSource = AppSession.Dal.GetCommonModeByPageCondition<Warehouse>(PageInfo.PageIndex, PageInfo.PageSize, sql, null).Data;
            }
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void page_PageChanged(object sender, Model.Controls.PageChangedEventArgs e)
        {

        }
    }
}
