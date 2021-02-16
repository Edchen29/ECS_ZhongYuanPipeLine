//using HHECS.DAL;
//using HHECS.Model;
//using HHECS.Model.BllModel;
//using HHECS.Model.Entities;
//using HHECS.Model.Enums;
//using MySql.Data.MySqlClient;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;

//namespace HHECS.Bll
//{
//    public abstract class BaseBll
//    {
//        public DALBase DALBase { get; set; }

//        protected BaseBll(DALBase dALBase)
//        {
//            DALBase = dALBase;
//        }


//        #region 实例方法

//        /// <summary>
//        /// 检查按钮权限
//        /// </summary>
//        /// <param name="menuOperations"></param>
//        /// <param name="controls"></param>
//        public void CheckPermission(List<MenuOperation> menuOperations, UIElementCollection controls)
//        {
//            foreach (var item in controls)
//            {
//                if (item is Button)
//                {
//                    Button temp = item as Button;
//                    if (temp.Tag is String)
//                    {
//                        string perms = (string)temp.Tag;
//                        if (menuOperations.Count(t => t.Perms == perms) > 0)
//                        {
//                            temp.Visibility = Visibility.Visible;
//                        }
//                        else
//                        {
//                            temp.Visibility = Visibility.Collapsed;
//                        }
//                    }
//                    else
//                    {
//                        temp.Visibility = Visibility.Collapsed;
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// 组合一组权限和他的子权限
//        /// </summary>
//        /// <param name="menuOperations"></param>
//        /// <param name="all"></param>
//        public void Combine(List<MenuOperation> menuOperations, List<MenuOperation> all)
//        {
//            foreach (var item in menuOperations)
//            {
//                item.Children.AddRange(all.FindAll(t => t.ParentId == item.Id));
//                if (item.Children.Count > 0)
//                {
//                    Combine(item.Children, all);
//                }
//            }
//        }

//        /// <summary>
//        /// 递归查出一组权限下的所有子权限Id
//        /// </summary>
//        /// <param name="menuOperations"></param>
//        /// <param name="all"></param>
//        /// <param name="ids"></param>
//        public void GetMenuOperationIds(List<MenuOperation> menuOperations, List<MenuOperation> all, List<int> ids)
//        {
//            foreach (var item in menuOperations)
//            {
//                //item.Children.AddRange();
//                ids.AddRange(all.FindAll(t => t.ParentId == item.Id).Select(t => t.Id.Value).ToList());
//                if (all.FindAll(t => t.ParentId == item.Id).Count > 0)
//                {
//                    GetMenuOperationIds(all.FindAll(t => t.ParentId == item.Id), all, ids);
//                }
//            }
//        }

//        /// <summary>
//        /// 发送LED
//        /// </summary>
//        /// <param name="station"></param>
//        /// <param name="task"></param>
//        /// <param name="LEDExcute"></param>
//        public void SendLED(Equipment station, TaskEntity task, LEDExcute LEDExcute)
//        {
//            switch (station.Code)
//            {
//                case "InStation1":
//                    LEDExcute.LeftInfoQueue.Enqueue(AppSession.Bll.GetTaskDetailsForLED(task));
//                    break;
//                case "OutStation1":
//                    LEDExcute.RightInfoQueue.Enqueue(AppSession.Bll.GetTaskDetailsForLED(task));
//                    break;
//                default:
//                    break;
//            }
//        }

//        #endregion

//        #region Requst

//        /// <summary>
//        /// 这里用同步的方式，因为我们是在定时器线程中调用的，不影响UI线程
//        /// </summary>
//        /// <param name="list"></param>
//        /// <param name="url"></param>
//        /// <returns></returns>
//        public BllResult FormPost(List<KeyValuePair<string, string>> list, WMSUrls url, HttpClient client, List<DictDetail> urls)
//        {
//            try
//            {
//                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
//                var ret = client.PostAsync(urls.First(t => t.Code == url.ToString()).Value, GetFormUrlEncodedContent(list)).Result;
//                if (ret.IsSuccessStatusCode)
//                {
//                    ret.EnsureSuccessStatusCode();
//                    string temp = ret.Content.ReadAsStringAsync().Result;
//                    AppSession.LogService.WriteInfoLog($"请求WMS接口{url.ToString()}成功，返回值：{temp}");
//                    var b = JsonConvert.DeserializeObject<ResponseModel<Object>>(temp);
//                    if (b.Code == "200")
//                    {
//                        return BllResultFactory.Sucess(null, b.Msg);
//                    }
//                    else
//                    {
//                        return BllResultFactory.Error(null, b.Msg);
//                    }
//                }
//                else
//                {
//                    return BllResultFactory.Error(null, $"请求WMS失败，请检查网络连接,详情：{ret.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                return BllResultFactory.Error(null, $"请求WMS失败，请检查网络连接或者重启本程序,详情：{ex.ToString()}");
//            }
//        }

//        /// <summary>
//        /// 这里用同步的方式，因为我们是在定时器线程中调用的，不影响UI线程
//        /// </summary>
//        /// <param name="list"></param>
//        /// <param name="url"></param>
//        /// <returns></returns>
//        public BllResult<T> FormPost<T>(List<KeyValuePair<string, string>> list, WMSUrls url, HttpClient client, List<DictDetail> urls)
//        {
//            try
//            {
//                var ret = client.PostAsync(urls.First(t => t.Code == url.ToString()).Value, GetFormUrlEncodedContent(list)).Result;
//                if (ret.IsSuccessStatusCode)
//                {
//                    ret.EnsureSuccessStatusCode();
//                    string temp = ret.Content.ReadAsStringAsync().Result;
//                    var b = JsonConvert.DeserializeObject<ResponseModel<T>>(temp);
//                    if (b.Code == "200")
//                    {
//                        return BllResultFactory<T>.Sucess(b.Data, b.Msg);
//                    }
//                    else
//                    {
//                        return BllResultFactory<T>.Error(default(T), b.Msg);
//                    }
//                }
//                else
//                {
//                    return BllResultFactory<T>.Error(default(T), "请求WMS失败，请检查网络连接");
//                }
//            }
//            catch (Exception ex)
//            {
//                return BllResultFactory<T>.Error(default(T), "请求WMS失败，请检查网络连接或者重启本程序");
//            }
//        }

//        /// <summary>
//        /// 这里用异步的方式，UI线程中请使用这个方法
//        /// </summary>
//        /// <param name="list"></param>
//        /// <param name="url"></param>
//        /// <returns></returns>
//        public async Task<BllResult> FormPostAsync(List<KeyValuePair<string, string>> list, WMSUrls url, HttpClient client, List<DictDetail> urls)
//        {
//            try
//            {
//                var ret = await client.PostAsync(urls.First(t => t.Code == url.ToString()).Value, GetFormUrlEncodedContent(list));
//                if (ret.IsSuccessStatusCode)
//                {
//                    ret.EnsureSuccessStatusCode();
//                    string temp = ret.Content.ReadAsStringAsync().Result;
//                    var b = JsonConvert.DeserializeObject<ResponseModel<Object>>(temp);
//                    if (b.Code == "200")
//                    {
//                        return BllResultFactory.Sucess(null, b.Msg);
//                    }
//                    else
//                    {
//                        return BllResultFactory.Error(null, b.Msg);
//                    }
//                }
//                else
//                {
//                    return BllResultFactory.Error(null, "请求WMS失败，请检查网络连接");
//                }
//            }
//            catch (Exception ex)
//            {
//                return BllResultFactory.Error(null, "请求WMS失败，请检查网络连接或者重启本程序");
//            }
//        }

//        public HttpContent GetFormUrlEncodedContent(List<KeyValuePair<string, string>> keyValuePair)
//        {
//            HttpContent content = new FormUrlEncodedContent(keyValuePair);
//            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
//            return content;
//        }

//        #endregion

//        #region 基础数据

//        public abstract BllResult<User> GetUserWithRoles(string userName, string password, int warehouseId, string warehouseCode, HttpClient client, List<DictDetail> urls);
//        public abstract BllResult<List<MenuOperation>> FindMenuOperation(List<Role> roles);
//        public abstract BllResult<List<MenuOperation>> GetAllMenuOperation();
//        public abstract BllResult<MenuOperation> SaveMenuOperation(MenuOperation currentMenuOperation);
//        public abstract BllResult UpdateMenuOperation(MenuOperation currentMenuOperation);
//        public abstract BllResult DeleteMenuOperationByIds(List<int> ids);
//        public abstract BllResult<List<Role>> GetAllRole();
//        public abstract BllResult<Role> GetRoleById(int value);
//        public abstract BllResult<List<Config>> GetAllConfig();
//        public abstract BllResult<Role> InsertRoleAndMenuOperations(Role currentRole, List<MenuOperation> list);
//        public abstract BllResult<Role> UpdateRoleAndMenuOperations(Role currentRole, List<MenuOperation> list);
//        public abstract BllResult<List<User>> GetUserByCondition(string sql);
//        public abstract BllResult SetUserDisable(List<int?> list, int enable);
//        public abstract BllResult<User> SaveUserWithRoles(User currentUser, List<int> RoleIds);
//        public abstract BllResult<User> UpdateUserWithRoles(User currentUser, List<int> RoleIds);
//        public abstract BllResult<Dict> GetDictWithDetails(string v);
//        public abstract BllResult DeleteConfigByIds(List<int> list);
//        public abstract BllResult DeleteRoleByIds(List<int> list);
//        public abstract BllResult DeleteDictByIds(List<int> list);
//        public abstract BllResult DeleteEuipmentTypeByIds(List<int> list);
//        public abstract BllResult DeleteEquipmentTypeTemplateByIds(List<int> list);
//        public abstract BllResult DeleteEuipmentByIds(List<int> ids);
//        public abstract BllResult SyncEquipmentProp(int? id);
//        public abstract BllResult CopyEquipment(EquipmentType equipment);

//        /// <summary>
//        /// 根据索引和巷道获取堆垛机接出站台
//        /// </summary>
//        /// <param name="station"></param>
//        /// <returns></returns>
//        public abstract BllResult<Equipment> GetOutStationByIndex(int? station, int roadWay);

//        /// <summary>
//        /// 根据索引和巷道获取堆垛机接入站台
//        /// </summary>
//        /// <param name="station"></param>
//        /// <returns></returns>
//        public abstract BllResult<Equipment> GetInStationByIndex(int? station, int roadWay);

//        /// <summary>
//        /// 根据站台的软件索引获取站台
//        /// </summary>
//        /// <param name="stationIndex"></param>
//        /// <returns></returns>
//        public abstract BllResult<Equipment> GetStationByIndex(int? stationIndex);

//        #endregion

//        #region Job

//        /// <summary>
//        /// 执行一次指定的job
//        /// </summary>
//        /// <param name="value"></param>
//        /// <param name="client"></param>
//        /// <returns></returns>
//        public abstract Task<BllResult> ExcuteJobAsync(int value, HttpClient client);

//        #endregion

//        #region Task

//        public abstract BllResult SendTaskToWCS(int id, HttpClient client, List<DictDetail> urls);
//        public abstract BllResult DeleteTask(int id);
//        public abstract BllResult UpdateLocationStatus(string locationCode, int warehouseId, string newStatus);
//        public abstract BllResult HeartBeat(string userCode, string password, int warehouseId, string warehouseCode, HttpClient client, List<DictDetail> urls);
//        public abstract BllResult CompleteTask(string taskId, HttpClient client, List<DictDetail> urls);
//        public abstract BllResult EmptyOutHandle(TaskEntity task, HttpClient client, List<DictDetail> urls);
//        public abstract BllResult TaskDoubleInHandle(string flag, String location, int taskId, int warehouseId);
//        public abstract BllResult<TaskEntity> CreatTask(string palletCode, int station, string sourceLocation, string destinationLocation, int status, int type, int priority, string userCode, int warehouseId, string warehouseCode);
//        public abstract BllResult<TaskEntity> CheckLocationForCreateTaskIn(String code, int warehouseId);
//        public abstract BllResult<List<Location>> GetAllLocations(String containerCode, string row, string column, string layer, string roadway, string status, string code);
//        public abstract BllResult TaskForkErrorHandle(int id, HttpClient client, List<DictDetail> urls);

//        /// <summary>
//        /// 请求WMS分配地址
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="id"></param>
//        /// <param name="height"></param>
//        /// <returns></returns>
//        public abstract BllResult<T> GetDestinationLocation<T>(int id, int height, HttpClient client, List<DictDetail> urls);

//        public abstract string GetTaskDetailsForLED(TaskEntity task);
//        #endregion

//    }
//}
