using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.View.Win;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace HHECS.View.ContainerInfo
{
    /// <summary>
    /// WinPalletAdd.xaml 的交互逻辑
    /// </summary>
    public partial class WinPalletAdd : HideCloseWindow
    {


        public Container CurrentContainer
        {
            get { return (Container)GetValue(CurrentContainerProperty); }
            set { SetValue(CurrentContainerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentContainer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentContainerProperty =
            DependencyProperty.Register("CurrentContainer", typeof(Container), typeof(WinPalletAdd), new PropertyMetadata(null));


        public Container Temp_containers { get; set; } = new Container();
        //public Container containers { get; set; } = new Container();

        public List<Container> containers = new List<Container>();
        public WinPalletAdd()
        {
            InitializeComponent();
            ints();
            IntAdd();
        }

        private void IntAdd()
        {            //查询出表里不同字段数据，然后赋值给ComboBox。
            //查询托盘类型
            var resultType = AppSession.BllService.GetDictWithDetails("ContainerType");
            if (resultType.Success)
            {
                var data = resultType.Data;
                var temp = resultType.Data.DictDetails.ToDictionary(i => i.Code, i => i.Value);
                Cbx_VolumePalletType.ItemsSource = temp;
                Cbx_VolumePalletType.DisplayMemberPath = "Key";
                Cbx_VolumePalletType.SelectedValuePath = "Value";
                Cbx_VolumePalletType.SelectedIndex = 0;
            }

            //查询托盘状态
            var resultStatus = AppSession.BllService.GetDictWithDetails("ContainerStatus");
            if (resultStatus.Success)
            {
                var data = resultStatus.Data;
                var temp = resultStatus.Data.DictDetails.ToDictionary(i => i.Code, i => i.Value);
                Cbx_VolumeStatus.ItemsSource = temp;
                Cbx_VolumeStatus.DisplayMemberPath = "Key";
                Cbx_VolumeStatus.SelectedValuePath = "Value";
                Cbx_VolumeStatus.SelectedIndex = 0;
            }


            //查询仓库位置
            var resultWarehouseCode = AppSession.Dal.GetCommonModelByCondition<Warehouse>("");
            if (resultWarehouseCode.Success)
            {
                var dictDetails = resultWarehouseCode.Data.ToDictionary(i => i.Code, i => i.Name);
                Cbx_VolumeaWarehouseCode.ItemsSource = dictDetails;
                Cbx_VolumeaWarehouseCode.DisplayMemberPath = "Key";
                Cbx_VolumeaWarehouseCode.SelectedValuePath = "Value";
                Cbx_VolumeaWarehouseCode.SelectedIndex = 0;
            }

            this.Txt_VolumePrefixNum.Text = "6";
        }

        private void ints()
        {
            //查询出表里不同字段数据，然后赋值给ComboBox。
            //查询托盘类型
            var resultType = AppSession.BllService.GetDictWithDetails("ContainerType");
            if (resultType.Success)
            {
                var data = resultType.Data;
                var temp = resultType.Data.DictDetails.ToDictionary(i => i.Code, i => i.Value);
                ComboBox_PalletType.ItemsSource = temp;
                ComboBox_PalletType.DisplayMemberPath = "Key";
                ComboBox_PalletType.SelectedValuePath = "Value";
                ComboBox_PalletType.SelectedIndex = 0;
            }

            //查询托盘状态
            var resultStatus = AppSession.BllService.GetDictWithDetails("ContainerStatus");
            if (resultStatus.Success)
            {
                var data = resultStatus.Data;
                var temp = resultStatus.Data.DictDetails.ToDictionary(i => i.Code, i => i.Value);
                ComboBox_PalletStatus.ItemsSource = temp;
                ComboBox_PalletStatus.DisplayMemberPath = "Key";
                ComboBox_PalletStatus.SelectedValuePath = "Value";
                ComboBox_PalletStatus.SelectedIndex = 0;
            }

            //查询仓库位置
            var resultWarehouseCode = AppSession.Dal.GetCommonModelByCondition<Warehouse>("");
            if (resultWarehouseCode.Success)
            {
                var dictDetails = resultWarehouseCode.Data.ToDictionary(i => i.Code, i => i.Name);
                ComboBox_WarehouseCode.ItemsSource = dictDetails;
                ComboBox_WarehouseCode.DisplayMemberPath = "Key";
                ComboBox_WarehouseCode.SelectedValuePath = "Value";
                ComboBox_WarehouseCode.SelectedIndex = 0;
            }
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Confrilm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Txt_PalletNum.Text))
                {
                    MessageBox.Show("容器号不能为空");
                    return;
                }

                //效验是否已经存在
                string sql = $"where code='{Txt_PalletNum.Text}'";
                var tempCheck = AppSession.Dal.GetCommonModelByCondition<Container>(sql);
                if (tempCheck.Success)
                {
                    var temp = tempCheck.Data[0];
                    var a = temp.Code;
                    if (a == Txt_PalletNum.Text)
                    {
                        MessageBox.Show($"查询到有相同容器编号," + "ID为" + temp.Id);
                        return;
                    }
                }
                else
                {
                    Temp_containers.Code = Txt_PalletNum.Text.Trim();
                    ///查询出来直接获取文本信息。
                    Temp_containers.Type = Convert.ToString(ComboBox_PalletType.Text);
                    Temp_containers.Status = Convert.ToString(ComboBox_PalletStatus.Text);
                    Temp_containers.WarehouseCode = Convert.ToString(ComboBox_WarehouseCode.Text);
                    Temp_containers.CreateTime = DateTime.Now;
                    Temp_containers.CreateBy = App.User.UserCode;
                    var result = AppSession.Dal.InsertCommonModel<Container>(Temp_containers);
                    if (result.Success)
                    {
                        MessageBox.Show("添加成功");
                        Txt_PalletNum.Text = "";
                    }
                    else
                    {
                        MessageBox.Show($"添加失败,失败原因{result.Msg}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"错误" + ex);
            }

        }

        private void Btn_VolumeAdd_Click(object sender, RoutedEventArgs e)
        {

            #region 文本框不能为空，前缀校验格式M，L，S+五位数字。
            if (Txt_VolumePrefix.Text == "")
            {
                MessageBox.Show($"前缀不能为空。");
                return;
            }
            if (Txt_VolumePrefix.Text.StartsWith("M") && Txt_VolumePrefix.Text.StartsWith("L") && Txt_VolumePrefix.Text.StartsWith("S"))
            {
                MessageBox.Show($"前缀开头非法格式");
                return;
            }
            if (Txt_VolumeStartBit.Text == "")
            {
                MessageBox.Show($"起始位不能为空。");
                return;
            }
            if (Txt_VolumeEndBit.Text == "")
            {
                MessageBox.Show($"结束位不能为空。");
                return;
            }
            int temp_Start = Convert.ToInt32(Txt_VolumeStartBit.Text.Trim());
            int temp_End = Convert.ToInt32(Txt_VolumeEndBit.Text.Trim());

            if (temp_End < temp_Start)
            {
                MessageBox.Show($"结束位必须大于起始位。");
                return;
            }
            int num = Convert.ToInt32(Txt_VolumePrefixNum.Text.Trim());
            //if (num > Txt_VolumePrefix.Text.Trim().Length)
            //{
            //    MessageBox.Show($"定义的位数,小于录入地{Txt_VolumePrefix.Text}的长度");
            //    return;
            //}
            int Start = Txt_VolumeStartBit.Text.Length + Txt_VolumePrefix.Text.Length;
            int End = Txt_VolumeEndBit.Text.Length + Txt_VolumePrefix.Text.Length;
            if (Start != num && End != num)
            {
                if (Start > num || Start < num)
                {
                    MessageBox.Show($"起始位+前缀：长度与位数不一致");
                    return;
                }
                if (End > num || End < num)
                {
                    MessageBox.Show($"结束位+前缀：长度与位数不一致");
                    return;
                }
            }
            #endregion
            int VolumeStartBit = Convert.ToInt32(Txt_VolumeStartBit.Text) - 1;
            this.Txt_VolumePrefix.IsReadOnly = true; this.Txt_VolumeStartBit.IsReadOnly = true; this.Txt_VolumeEndBit.IsReadOnly = true;
            string CreatedBy = App.User.UserCode;
            var res = AppSession.ContainerService.CreateContainers(Txt_VolumePrefix.Text, Cbx_VolumePalletType.Text, Cbx_VolumeStatus.Text, VolumeStartBit, Txt_VolumeEndBit.Text, Cbx_VolumeaWarehouseCode.Text, num, CreatedBy);
            if (!res.Success)
            {
                MessageBox.Show($"{res.Msg}");
            }
            else
            {
                this.Txt_VolumePrefix.IsReadOnly = false;this.Txt_VolumeStartBit.IsReadOnly = false;this.Txt_VolumeEndBit.IsReadOnly = false;
                MessageBox.Show($"{res.Msg}");
            }

        }
    }
}
