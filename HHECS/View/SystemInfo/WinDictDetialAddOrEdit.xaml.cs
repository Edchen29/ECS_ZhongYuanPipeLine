using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.View.Win;
using System;
using System.Windows;

namespace HHECS.View.SystemInfo
{
    /// <summary>
    /// WinDictDetialAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinDictDetialAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }
        public int HeadId { get; set; }
        public DictDetail CurrentDictDetial { get; set; } = new DictDetail();
        public Dict DictMain { get; set; }
        public WinDictDetialAddOrEdit(int? id,int headId)
        {
            InitializeComponent();
            Id = id;
            HeadId = headId;
            this.Title = Id == null ? "新增" : "编辑";
            Init();
            GridMain.DataContext = CurrentDictDetial;
        }

        private void Init()
        {
            var result = AppSession.Dal.GetCommonModelByCondition<Dict>($"where id = {HeadId}");
            if (result.Success)
            {
                DictMain = result.Data[0];
                if (Id == null)
                {
                    //新增
                    CurrentDictDetial.HeadCode = DictMain.Code;
                    CurrentDictDetial.HeadName = DictMain.Name;
                    CurrentDictDetial.HeadId = DictMain.Id.Value;
                }
                else
                {
                    //编辑
                    var a = AppSession.Dal.GetCommonModelByCondition<DictDetail>($"where  id = {Id}");
                    if (a.Success)
                    {
                        CurrentDictDetial = a.Data[0];
                        //主表的
                        CurrentDictDetial.HeadCode = DictMain.Code;
                        CurrentDictDetial.HeadName = DictMain.Name;

                        //
                        //TxtDictDetailCode.IsReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("查询明细失败");
                    }
                }
            }
            else
            {
                MessageBox.Show($"查询字典失败:{result.Msg}");
                this.Close();
            }
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDictDetial.Id == null)
            {
                //新增
                CurrentDictDetial.HeadId = DictMain.Id.Value;
                CurrentDictDetial.Created = DateTime.Now;
                CurrentDictDetial.CreatedBy = App.User.UserCode;
                BllResult<int?> result = AppSession.Dal.InsertCommonModel<DictDetail>(CurrentDictDetial);
                if (result.Success)
                {
                    MessageBox.Show("新增成功");
                    TxtDictDetailCode.IsReadOnly = true;
                    CurrentDictDetial.Id = result.Data;
                }
                else
                {
                    MessageBox.Show($"新增失败:{result.Msg}");
                }
            }
            else
            {
                //更新
                CurrentDictDetial.Updated = DateTime.Now;
                CurrentDictDetial.UpdatedBy = App.User.UserCode;
                BllResult result = AppSession.Dal.UpdateCommonModel<DictDetail>(CurrentDictDetial);
                if (result.Success)
                {
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show($"更新失败：{result.Msg}");
                }

            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
