using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
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

namespace HHECS.View.EquipmentInfo
{
    /// <summary>
    /// WinStationAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinStationAddOrEdit : Window
    {
        public int? Id { get; set; }

        public Station Current
        {
            get { return (Station)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Current.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register("Current", typeof(Station), typeof(WinStationAddOrEdit), new PropertyMetadata(new Station()));
        public WinStationAddOrEdit(int? id)
        {
            InitializeComponent();
            this.Id = id;
            Init();
            this.Title = id == null ? "新增" : "编辑";
            this.GridMain.DataContext = Current;
        }

        private void Init()
        {
            if (Id == null)
            {
                //新增
                //Current.WarehouseCode = App.WarehouseCode;
            }
            else
            {
                //编辑
                BllResult<List<Station>> result = AppSession.Dal.GetCommonModelByCondition<Station>($"where id ={Id}");
                if (result.Success)
                {
                    var temp = result.Data[0];
                    Current = temp;
                }
                else
                {
                    MessageBox.Show($"查询设备详情失败:{result.Msg}");
                }
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (Current.Id == null)
            {
                //新增
                Current.CreateTime = DateTime.Now;
                Current.CreateBy = App.User.UserCode;
                var a = AppSession.Dal.InsertCommonModel<Station>(Current);
                if (a.Success)
                {
                    MessageBox.Show("新增成功");
                }
                else
                {
                    MessageBox.Show($"新增失败{a.Msg}");
                }
            }
            else
            {
                //更新
                Current.UpdateTime = DateTime.Now;
                Current.UpdateBy = App.User.UserCode;
                var a = AppSession.Dal.UpdateCommonModel<Station>(Current);
                if (a.Success)
                {
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show($"更新失败:{a.Msg}");
                }
            }
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
