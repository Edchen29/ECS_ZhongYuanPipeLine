using HHECS.Bll;
using HHECS.Model;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
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

namespace HHECS.View.EquipmentInfo
{
    /// <summary>
    /// EquipmentTypeTemplateAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinEquipmentTypeTemplateAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }
        //public EquipmentTypeTemplate CurrentEquipmentTypeTemplate { get; set; } = new EquipmentTypeTemplate();



        public EquipmentTypeTemplate CurrentEquipmentTypeTemplate
        {
            get { return (EquipmentTypeTemplate)GetValue(CurrentEquipmentTypeTemplateProperty); }
            set { SetValue(CurrentEquipmentTypeTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentEquipmentTypeTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEquipmentTypeTemplateProperty =
            DependencyProperty.Register("CurrentEquipmentTypeTemplate", typeof(EquipmentTypeTemplate), typeof(WinEquipmentTypeTemplateAddOrEdit), new PropertyMetadata(new EquipmentTypeTemplate()));



        public EquipmentType Head { get; set; }
        public WinEquipmentTypeTemplateAddOrEdit(int? id, EquipmentType head)
        {
            InitializeComponent();
            this.Id = id;
            this.Head = head;
            this.Title = id == null ? "新增" : "编辑";
            Init();
            this.GridMain.DataContext = CurrentEquipmentTypeTemplate;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("循环时读PLC", "PLC_Read");
            dict.Add("处理类读PLC", "PLC_DelayRead");
            dict.Add("不主动读PLC", "PLC_NotRead");
            dict.Add("后台读PLC", "PLC_BackgroundRead");
            dict.Add("自身属性", "Self");
            CbxEquipmentTypeTemplatePropType.DisplayMemberPath = "Key";
            CbxEquipmentTypeTemplatePropType.SelectedValuePath = "Value";
            CbxEquipmentTypeTemplatePropType.ItemsSource = dict;
            CbxEquipmentTypeTemplatePropType.SelectedIndex = 0;

            Dictionary<string, string> dict2 = new Dictionary<string, string>();
            dict2.Add("BYTE", "BYTE");
            dict2.Add("INT", "INT");
            dict2.Add("DINT", "DINT");
            dict2.Add("WORD", "WORD");
            dict2.Add("DWORD", "DWORD");
            dict2.Add("CHAR", "CHAR");
            dict2.Add("BOOL", "BOOL");
            CbxEquipmentTypeTemplateDataType.DisplayMemberPath = "Key";
            CbxEquipmentTypeTemplateDataType.SelectedValuePath = "Value";
            CbxEquipmentTypeTemplateDataType.ItemsSource = dict2;
            CbxEquipmentTypeTemplateDataType.SelectedIndex = 0;

        }

        private void Init()
        {
            if (Id == null)
            {
                //新增
                CurrentEquipmentTypeTemplate.HeadCode = Head.Code;
                CurrentEquipmentTypeTemplate.HeadName = Head.Name;
                CurrentEquipmentTypeTemplate.EquipmentTypeId = Head.Id.Value;
            }
            else
            {
                BtnNew.Visibility = Visibility.Hidden;
                //编辑，编码不能改变
                //TxtEquipmentTypeTemplateCode.IsReadOnly = true;
                var a = AppSession.Dal.GetCommonModelByCondition<EquipmentTypeTemplate>($"where id = {Id}");
                if (a.Success)
                {
                    CurrentEquipmentTypeTemplate = a.Data[0];
                    //
                    CurrentEquipmentTypeTemplate.HeadCode = Head.Code;
                    CurrentEquipmentTypeTemplate.HeadName = Head.Name;
                }
                else
                {
                    MessageBox.Show($"未能获取到id为{Id}的数据，错误消息：{a.Msg}");
                    this.Close();
                }

            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //这里查下主表还存不存在，防止并发删除；
            var result = AppSession.Dal.GetCommonModelByCondition<EquipmentType>($"where id = {Head.Id}");
            if (!result.Success)
            {
                MessageBox.Show($"编码为{Head.Code}的设备类型已被删除，请检查，错误信息：{result.Msg}");
            }
            else
            {
                if (CurrentEquipmentTypeTemplate.Id == null)
                {
                    //新增
                    CurrentEquipmentTypeTemplate.EquipmentTypeId = Head.Id.Value;
                    CurrentEquipmentTypeTemplate.CreateTime = DateTime.Now;
                    CurrentEquipmentTypeTemplate.CreateBy = App.User.UserCode;
                    BllResult<int?> a = AppSession.Dal.InsertCommonModel<EquipmentTypeTemplate>(CurrentEquipmentTypeTemplate);
                    if (a.Success)
                    {
                        MessageBox.Show("新增成功");
                        //TxtEquipmentTypeTemplateCode.IsReadOnly = true;
                        CurrentEquipmentTypeTemplate.Id = a.Data.Value;
                    }
                    else
                    {
                        MessageBox.Show($"新增失败：{a.Msg}");
                    }
                }
                else
                {
                    //更新
                    CurrentEquipmentTypeTemplate.UpdateTime = DateTime.Now;
                    CurrentEquipmentTypeTemplate.UpdateBy = App.User.UserCode;
                    BllResult a = AppSession.Dal.UpdateCommonModel<EquipmentTypeTemplate>(CurrentEquipmentTypeTemplate);
                    if (a.Success)
                    {
                        MessageBox.Show("更新成功");
                    }
                    else
                    {
                        MessageBox.Show($"更新失败：{a.Msg}");
                    }

                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 清空，用于继续新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in CurrentEquipmentTypeTemplate.GetType().GetProperties())
            {
                if (item.Name != "HeadCode" && item.Name != "HeadName" && item.Name != "EquipmentTypeId")
                {
                    item.SetValue(CurrentEquipmentTypeTemplate, null);
                }
            }
            TxtEquipmentTypeTemplateCode.IsReadOnly = false;
        }
    }
}
