using HHECS.Bll;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums.PipeLine;
using HHECS.View.Win;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HHECS.View.CacheInfo
{
    /// <summary>
    /// WinCacheMaterialAddOrEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinCacheMaterialAddOrEdit : BaseWindow
    {
        public int? Id { get; set; }

        //工位列表
        List<Station> stations = new List<Station>();

        public StationCache CurrentStationCache
        {
            get { return (StationCache)GetValue(CurrentStationCacheProperty); }
            set { SetValue(CurrentStationCacheProperty, value); }
        }

        public static readonly DependencyProperty CurrentStationCacheProperty =
            DependencyProperty.Register("CurrentStationCache", typeof(StationCache), typeof(WinCacheMaterialAddOrEdit), new PropertyMetadata(new StationCache()));



        public WinCacheMaterialAddOrEdit(int? id)
        {
            InitializeComponent();
            this.Id = id;
            this.Title = id == null ? "新增" : "编辑";
            Init();
            this.GridMain.DataContext = CurrentStationCache;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var temp = AppSession.BllService.GetDictWithDetails("CacheMaterialStatus");
            //if (temp.Success)
            //{
                //var dictDetails = temp.Data.DictDetails.ToDictionary(t => t.Value, i => i.Name);
                //dictDetails.Add("", "全部");
                //CbxCacheMaterialStatus.ItemsSource = dictDetails;
                //CbxCacheMaterialStatus.DisplayMemberPath = "Value";
                //CbxCacheMaterialStatus.SelectedValuePath = "Key";
            //}
            //else
            //{
            //    MessageBox.Show("加载状态失败");
            //}
        }


        private void Init()
        {
            stations.Add(new Station() { Id = 1135, Code = "LengthMeasuringCache", Name = "测长缓存" });
            stations.Add(new Station() { Id = 1131, Code = "AssembleCache1", Name = "组队区缓存1" });
            stations.Add(new Station() { Id = 1132, Code = "AssembleCache2", Name = "组队区缓存2" });
            CbxStation.ItemsSource = stations;
            CbxStation.DisplayMemberPath = "Name";
            CbxStation.SelectedValuePath = "Code";

            //管材缓存状态列表
            var StationCacheStatusList = CommonHelper.EnumListDic<StationCacheStatus>("");
            CbxCacheMaterialStatus.ItemsSource = StationCacheStatusList;
            CbxCacheMaterialStatus.DisplayMemberPath = "Value";
            CbxCacheMaterialStatus.SelectedValuePath =  "Key";

            if (Id == null)
            {
                //新增
            }
            else
            {
                //编辑
                BllResult<List<StationCache>> result = AppSession.Dal.GetCommonModelByCondition<StationCache>($"where id ={Id}");
                if (result.Success)
                {
                    CurrentStationCache = result.Data[0];
                    //CbxCacheMaterialStatus.SelectedValue = StepTraceStatus.设备请求下料.GetIndexInt();
                    //CurrentEquipmentType.Id = temp.Id;
                    //CurrentEquipmentType.Code = temp.Code;
                    //CurrentEquipmentType.Name = temp.Name;
                    //CurrentEquipmentType.Description = temp.Description;
                    //CurrentEquipmentType.Created = temp.Created;
                    //CurrentEquipmentType.CreatedBy = temp.CreatedBy;
                    //TxtEquipmentTypeCode.IsReadOnly = true;
                }
                else
                {
                    MessageBox.Show($"查询设备类型详情失败:{result.Msg}");
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStationCache.Id == null)
            {
                //新增
                var station = stations.FirstOrDefault(t => t.Code == CurrentStationCache.StationCode);
                CurrentStationCache.StationId = station.Id;
                CurrentStationCache.CreateTime = DateTime.Now;
                CurrentStationCache.CreateBy = App.User.UserCode;
                var a = AppSession.Dal.InsertCommonModel<StationCache>(CurrentStationCache);
                if (a.Success)
                {
                    //新增成功后，不允许修改code了
                    //TxtEquipmentTypeCode.IsReadOnly = true;
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
                var station = stations.FirstOrDefault(t => t.Code == CurrentStationCache.StationCode);
                CurrentStationCache.StationId = station.Id;
                CurrentStationCache.CreateTime = DateTime.Now;
                CurrentStationCache.UpdateBy = App.User.UserCode;
                var a = AppSession.Dal.UpdateCommonModel<StationCache>(CurrentStationCache);
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
