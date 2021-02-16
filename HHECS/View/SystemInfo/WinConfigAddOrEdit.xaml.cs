using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.View.Win;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HHECS.View.SystemInfo
{
    /// <summary>
    /// WinConfigAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinConfigAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }
        //public Config CurrentConfig { get; set; } = new Config();


        public Config CurrentConfig
        {
            get { return (Config)GetValue(CurrentConfigProperty); }
            set { SetValue(CurrentConfigProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentConfig.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentConfigProperty =
            DependencyProperty.Register("CurrentConfig", typeof(Config), typeof(WinConfigAddOrEdit), new PropertyMetadata(new Config()));


        public WinConfigAddOrEdit(int? id)
        {

            InitializeComponent();
            this.Id = id;
            this.Title = id == null ? "新增" : "编辑";
            Init();
            this.GridMain.DataContext = CurrentConfig;
        }

        private void Init()
        {
            if (Id != null)
            {
                BllResult<List<Config>> result = AppSession.Dal.GetCommonModelByCondition<Config>($"where id = {Id}");
                if (result.Success)
                {
                    CurrentConfig = result.Data[0];
                }
                else
                {
                    MessageBox.Show($"未查询到Id为{Id}的参数");
                }
                //编辑模式下，设置code不能更改
                TxtConfigCode.IsReadOnly = true;
            }
            else
            {
                Config config = new Config();
                config.WarehouseCode = AppSession.WarehouseCode;
                CurrentConfig = config;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentConfig.Id == null)
            {
                //新增
                CurrentConfig.Created = DateTime.Now;
                CurrentConfig.CreatedBy = App.User.UserCode;
                BllResult<int?> result = AppSession.Dal.InsertCommonModel<Config>(CurrentConfig);
                if (result.Success)
                {
                    MessageBox.Show("新增成功");
                    TxtConfigCode.IsReadOnly = true;
                    CurrentConfig.Id = result.Data;
                }
                else
                {
                    MessageBox.Show($"新增失败：{result.Msg}");
                }
            }
            else
            {
                //更新
                CurrentConfig.Updated = DateTime.Now;
                CurrentConfig.UpdatedBy = App.User.UserCode;
                BllResult result = AppSession.Dal.UpdateCommonModel<Config>(CurrentConfig);
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
