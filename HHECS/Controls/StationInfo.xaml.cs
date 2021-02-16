using HHECS.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HHECS.Controls
{
    /// <summary>
    /// 标准站台信息监控
    /// </summary>
    public partial class StationInfo : UserControl
    {
        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(StationInfo), new PropertyMetadata(""));


        public StationInfo(int maxW,int maxH)
        {
            InitializeComponent();
            lab_Name.SetBinding(Label.ContentProperty, new Binding("ControlName") { Source = this });
            this.MaxHeight = maxH;
            this.MaxWidth = maxW;
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        /// <param name="deviceAddressEntities"></param>
        public void SetProp(Equipment equipment)
        {
            foreach (var item in equipment.EquipmentProps)
            {
                var temp =(Label)this.FindName("lab_" + item.EquipmentTypeTemplateCode);
                if(temp!=null)
                {
                    temp.Content = item.Value;
                }
            }
        }
    }
}
