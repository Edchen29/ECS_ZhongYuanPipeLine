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

namespace HHECS.View.SystemInfo
{
    /// <summary>
    /// WinDictAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinDictAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }

        public Dict CurrentDict
        {
            get { return (Dict)GetValue(CurrentDictProperty); }
            set { SetValue(CurrentDictProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDict.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDictProperty =
            DependencyProperty.Register("CurrentDict", typeof(Dict), typeof(WinDictAddOrEdit), new PropertyMetadata(new Dict()));


        public WinDictAddOrEdit(int? id)
        {
            InitializeComponent();
            Id = id;
            this.Title = Id == null ? "新增" : "编辑";
            Init();
            this.GridMain.DataContext = CurrentDict;
        }

        private void Init()
        {
            if (Id == null)
            {
                //新增
                
            }
            else
            {
                //编辑
                BllResult<List<Dict>> result = AppSession.Dal.GetCommonModelByCondition<Dict>($"where id ={Id}");
                if (result.Success)
                {
                    CurrentDict = result.Data[0];
                    TxtDictCode.IsReadOnly = true;
                }
                else
                {
                    MessageBox.Show($"查询字典详情失败:{result.Msg}");
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDict.Id == null)
            {
                //新增
                CurrentDict.Created = DateTime.Now;
                CurrentDict.CreatedBy = App.User.UserCode;
                var a = AppSession.Dal.InsertCommonModel<Dict>(CurrentDict);
                if (a.Success)
                {
                    //新增成功后，不允许修改code了
                    TxtDictCode.IsReadOnly = true;
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
                CurrentDict.Updated = DateTime.Now;
                CurrentDict.UpdatedBy = App.User.UserCode;
                var a = AppSession.Dal.UpdateCommonModel<Dict>(CurrentDict);
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
