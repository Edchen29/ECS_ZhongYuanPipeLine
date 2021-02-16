using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Dapper;
using HHECS.DAL;
using HHECS.Model.BllModel;
using HHECS.Model.Common;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.LEDHelper;

namespace HHECS.Bll
{
    /// <summary>
    /// 系统相关服务，比如字典查询，设备关系维护等
    /// </summary>
    public class BllService : BaseService
    {

        #region 权限


        /// <summary>
        /// 检查按钮权限
        /// </summary>
        /// <param name="menuOperations"></param>
        /// <param name="controls"></param>
        public void CheckPermission(List<MenuOperation> menuOperations, UIElementCollection controls)
        {
            foreach (var item in controls)
            {
                if (item is Button)
                {
                    Button temp = item as Button;
                    if (temp.Tag is String)
                    {
                        string perms = (string)temp.Tag;
                        if (menuOperations.Count(t => t.Perms == perms) > 0)
                        {
                            temp.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            temp.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        temp.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        /// <summary>
        /// 检查按钮权限
        /// </summary>
        /// <param name="menuOperations"></param>
        /// <param name="controls"></param>
        public void CheckPermission(List<MenuOperation> menuOperations, List<UIElement> controls)
        {
            foreach (var item in controls)
            {
                if (item is Button)
                {
                    Button temp = item as Button;
                    if (temp.Tag is String)
                    {
                        string perms = (string)temp.Tag;
                        if (menuOperations.Count(t => t.Perms == perms) > 0)
                        {
                            temp.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            temp.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        temp.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        /// <summary>
        /// 组合一组权限和他的子权限
        /// </summary>
        /// <param name="menuOperations"></param>
        /// <param name="all"></param>
        public void Combine(List<MenuOperation> menuOperations, List<MenuOperation> all)
        {
            foreach (var item in menuOperations)
            {
                item.Children.AddRange(all.FindAll(t => t.ParentId == item.Id));
                if (item.Children.Count > 0)
                {
                    Combine(item.Children, all);
                }
            }
        }

        /// <summary>
        /// 递归查出一组权限下的所有子权限Id
        /// </summary>
        /// <param name="menuOperations"></param>
        /// <param name="all"></param>
        /// <param name="ids"></param>
        public void GetMenuOperationIds(List<MenuOperation> menuOperations, List<MenuOperation> all, List<int> ids)
        {
            foreach (var item in menuOperations)
            {
                //item.Children.AddRange();
                ids.AddRange(all.FindAll(t => t.ParentId == item.Id).Select(t => t.Id.Value).ToList());
                if (all.FindAll(t => t.ParentId == item.Id).Count > 0)
                {
                    GetMenuOperationIds(all.FindAll(t => t.ParentId == item.Id), all, ids);
                }
            }
        }

        /// <summary>
        /// 发送LED
        /// </summary>
        /// <param name="station"></param>
        /// <param name="task"></param>
        /// <param name="LEDExcute"></param>
        public void SendLED(Equipment station, TaskEntity task)
        {
            AppSession.LEDExcute?.Push(station.LedIp, AppSession.TaskService.GetTaskDetailsForLED(task));
        }

        #endregion

        #region 基础数据相关

        #region UserAndMenuOperation

        /// <summary>
        /// 根据一组角色返回一组权限
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public BllResult<List<MenuOperation>> FindMenuOperation(List<Role> roles)
        {
            try
            {
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    var list = connection.GetList<MenuOperation>("where id in (select menuOperationId from wcsrolemenuoperation where roleId in @roleIds)", new
                    {
                        roleIds = roles.Select(t => t.Id.Value).ToList().ToArray()
                    }).ToList();
                    if (list.Count() == 0)
                    {
                        return BllResultFactory<List<MenuOperation>>.Error("未找到对应权限");
                    }
                    else
                    {
                        return BllResultFactory<List<MenuOperation>>.Sucess(list, "成功");
                    }
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteDBExceptionLog(ex);
                return BllResultFactory<List<MenuOperation>>.Error(null, "发生异常");
            }
        }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public BllResult<List<MenuOperation>> GetAllMenuOperation()
        {
            try
            {
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    var list = connection.GetList<MenuOperation>().ToList();
                    return BllResultFactory<List<MenuOperation>>.Sucess(list, "成功");
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteDBExceptionLog(ex);
                return BllResultFactory<List<MenuOperation>>.Error(null, "发生异常");
            }
        }

        public BllResult<User> GetUserWithRoles(string userCode, string password)
        {
            try
            {
                BllResult result = BllResultFactory.Error();

                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    var lookup = new Dictionary<int?, User>();
                    connection.Query<User, Role, User>("select u.*,rl.* from wcsuser u join wcsuserrole r on u.id = r.userId join wcsrole rl on r.roleId = rl.id where u.userCode=@userTemp and u.password=@passwordTemp;", (user, role) =>
                    {
                        List<Role> roles = new List<Role>();
                        User u;
                        if (!lookup.TryGetValue(user.Id, out u))
                        {
                            u = user;
                            lookup.Add(u.Id, u);
                        }
                        u.Roles.Add(role);
                        return u;
                    }, new { userCode, password });
                    var resultList = lookup.Values.ToList();
                    if (resultList.Count() == 0)
                    {
                        return BllResultFactory<User>.Error("用户名或密码不正确");
                    }
                    else
                    {
                        resultList[0].UserCode = userCode;
                        resultList[0].UserName = userCode;
                        resultList[0].Password = password;
                        return BllResultFactory<User>.Sucess(resultList[0], "登录成功");
                    }
                }

            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteDBExceptionLog(ex);
                return BllResultFactory<User>.Error(null, ex.Message);
            }
        }

        /// <summary>
        /// 根据用户名和密码获取用户实体，包括他的角色
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public BllResult<User> GetUserWithRoles(string userCode, string password, string warehouseCode, HttpClient client, List<DictDetail> urls)
        {
            try
            {
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    var lookup = new Dictionary<int?, User>();
                    connection.Query<User, Role, User>("select u.*,rl.* from wcsuser u join wcsuserrole r on u.id = r.userId join wcsrole rl on r.roleId = rl.id where u.userCode=@userCode and u.password=@password;", (user, role) =>
                    {
                        List<Role> roles = new List<Role>();
                        User u;
                        if (!lookup.TryGetValue(user.Id, out u))
                        {
                            u = user;
                            lookup.Add(u.Id, u);
                        }
                        u.Roles.Add(role);
                        return u;
                    }, new { userCode, password });
                    var resultList = lookup.Values.ToList();
                    if (resultList.Count() == 0)
                    {
                        return BllResultFactory<User>.Error("用户名或密码不正确");
                    }
                    else
                    {
                        resultList[0].UserCode = userCode;
                        resultList[0].UserName = userCode;
                        resultList[0].Password = password;
                        return BllResultFactory<User>.Sucess(resultList[0], "登录成功");
                    }
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteDBExceptionLog(ex);
                return BllResultFactory<User>.Error(null, ex.Message);
            }
        }

        public BllResult<MenuOperation> SaveMenuOperation(MenuOperation currentMenuOperation)
        {
            try
            {
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    var a = connection.Insert<MenuOperation>(currentMenuOperation);
                    currentMenuOperation.Id = a;
                    return BllResultFactory<MenuOperation>.Sucess(currentMenuOperation, "成功");
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteDBExceptionLog(ex);
                return BllResultFactory<MenuOperation>.Error(null, "发生异常");
            }
        }

        public BllResult UpdateMenuOperation(MenuOperation currentMenuOperation)
        {
            try
            {
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    var a = connection.Update<MenuOperation>(currentMenuOperation);
                    if (a != 0)
                    {
                        return BllResultFactory.Sucess(null, "成功");
                    }
                    else
                    {
                        return BllResultFactory.Error(null, "失败");
                    }
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteDBExceptionLog(ex);
                return BllResultFactory.Error(null, "发生异常");
            }
        }

        public BllResult DeleteMenuOperationByIds(List<int> ids)
        {
            try
            {
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    var a = connection.DeleteList<MenuOperation>("where id in @ids", new { ids = ids });
                    if (a != 0)
                    {
                        return BllResultFactory.Sucess(null, "成功");
                    }
                    else
                    {
                        return BllResultFactory.Error(null, "失败");
                    }
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteDBExceptionLog(ex);
                return BllResultFactory.Error(null, "发生异常");
            }
        }

        public BllResult<List<Role>> GetAllRole()
        {
            try
            {
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    var a = connection.GetList<Role>().ToList();
                    return BllResultFactory<List<Role>>.Sucess(a, "成功");
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteDBExceptionLog(ex);
                return BllResultFactory<List<Role>>.Error(null, "发生异常");
            }
        }

        public BllResult<Role> GetRoleById(int id)
        {
            try
            {
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    var a = connection.Get<Role>(id);
                    return BllResultFactory<Role>.Sucess(a, "成功");
                }
            }
            catch (Exception ex)
            {
                AppSession.LogService.WriteDBExceptionLog(ex);
                return BllResultFactory<Role>.Error(null, "发生异常");
            }
        }

        /// <summary>
        /// 保存角色和他的权限值
        /// </summary>
        /// <param name="currentRole"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public BllResult<Role> InsertRoleAndMenuOperations(Role currentRole, List<MenuOperation> list)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    var a = connection.Insert<Role>(currentRole, transaction: tran);
                    currentRole.Id = a;
                    var b = connection.Execute("INSERT INTO wcsrolemenuoperation(roleId,menuOperationId) values (@roleId,@menuOperationId)", list.Select(t => new { roleId = a, menuOperationId = t.Id }), transaction: tran);
                    tran.Commit();
                    return BllResultFactory<Role>.Sucess(currentRole, "成功");

                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<Role>.Error(null, "发生异常");
                }
            }
        }

        public BllResult<Role> UpdateRoleAndMenuOperations(Role currentRole, List<MenuOperation> list)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    int i = connection.Update<Role>(currentRole, transaction: tran);
                    //删掉原来所有的权限，然后再插入新增的所有权限
                    connection.Execute("delete from wcsrolemenuoperation where roleId = @roleId", new { roleId = currentRole.Id }, transaction: tran);
                    var b = connection.Execute("INSERT INTO wcsrolemenuoperation(roleId,menuOperationId) values (@roleId,@menuOperationId)", list.Select(t => new { roleId = currentRole.Id, menuOperationId = t.Id }), transaction: tran);
                    tran.Commit();
                    return BllResultFactory<Role>.Sucess(currentRole, "成功");
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<Role>.Error(null, "发生异常");
                }
            }
        }

        public BllResult<List<User>> GetUserByCondition(string sql)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    var result = connection.GetList<User>(sql).ToList();
                    return BllResultFactory<List<User>>.Sucess(result, "成功");
                }
                catch (Exception ex)
                {
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<List<User>>.Error(null, "发生异常");
                }
            }
        }

        public BllResult SetUserDisable(List<int?> list, int enable)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    var a = connection.Execute("update [wcsuser] set disable = @enalbe where id in @ids;", new { enalbe = enable, ids = list });
                    return BllResultFactory.Sucess("成功");
                }
                catch (Exception ex)
                {
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory.Error("发生异常");
                }
            }
        }

        public BllResult<User> SaveUserWithRoles(User currentUser, List<int> RoleIds)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    currentUser.Id = connection.Insert<User>(currentUser, tran);
                    connection.Execute("insert into wcsuserrole(userId,roleId)  values (@userId,@roleId);", RoleIds.Select(t => new { userId = currentUser.Id.Value, roleId = t }).ToList(), tran);
                    tran.Commit();
                    return BllResultFactory<User>.Sucess(currentUser, "成功");
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<User>.Error("发生异常");
                }
            }
        }

        public BllResult<User> UpdateUserWithRoles(User currentUser, List<int> RoleIds)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    connection.Update<User>(currentUser, transaction);
                    //先删再添加
                    connection.Execute("delete from wcsuserrole where userId=@userId", new { userId = currentUser.Id }, transaction: transaction);
                    //插入
                    connection.Execute("insert into wcsuserrole(userId,roleId) values (@userId,@roleId);", RoleIds.Select(t => new { userId = currentUser.Id, roleId = t }).ToList(), transaction);
                    transaction.Commit();
                    return BllResultFactory<User>.Sucess(currentUser, "成功");
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<User>.Error("发生异常");
                }
            }
        }

        public BllResult DeleteRoleByIds(List<int> list)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (connection.Query<int>("select count(1) from wcsuserrole where roleId = @roleId", new { roleId = item }).ToList()[0] > 0)
                        {
                            throw new BllException($"Id为{item}的角色被赋予过用户，请先取消用户的角色，再行删除");
                        }
                    }
                    connection.DeleteList<Role>("where id in @ids", new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("删除成功");
                }
                catch (BllException bllEx)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(bllEx);
                    return BllResultFactory.Error(bllEx.Message);
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory.Error("发生异常");
                }
            }
        }

        #endregion

        #region System

        public BllResult<List<Config>> GetAllConfig()
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    var a = connection.GetList<Config>().ToList();
                    return BllResultFactory<List<Config>>.Sucess(a, "成功");
                }
                catch (Exception ex)
                {
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<List<Config>>.Error($"发生异常:{ex.Message}");
                }
            }
        }

        public BllResult<Config> GetConfig(string code)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    var a = connection.GetList<Config>($"where code = '{code}'").ToList();
                    if (a.Count == 0)
                    {
                        return BllResultFactory<Config>.Error($"失败,无此{code}配置");

                    }
                    else
                    {
                        return BllResultFactory<Config>.Sucess(a[0], "成功");

                    }
                }
                catch (Exception ex)
                {
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<Config>.Error($"发生异常:{ex.Message}");
                }
            }
        }

        public BllResult DeleteConfigByIds(List<int> list)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    connection.DeleteList<Config>("where id in @ids", new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("删除成功");
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory.Error("发生异常");
                }
            }
        }

        public BllResult DeleteDictByIds(List<int> list)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    connection.DeleteList<Dict>("where id in @ids", new { ids = list }, tran);
                    connection.DeleteList<DictDetail>("where headId in @ids", new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("成功");
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory.Error($"删除失败：{ex.Message}");
                }
            }
        }

        public BllResult<Dict> GetDictWithDetails(string v)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    var lookup = new Dictionary<int?, Dict>();

                    connection.Query<Dict, DictDetail, Dict>("select * from wcsdict a join wcsdictdetail b on a.id = b.headId where a.code = @code", (dict, dictDetail) =>
                    {
                        Dict tempDict;
                        if (!lookup.TryGetValue(dict.Id, out tempDict))
                        {
                            tempDict = dict;
                            lookup.Add(dict.Id, dict);
                        }
                        tempDict.DictDetails.Add(dictDetail);
                        return tempDict;
                    }, new { code = v });
                    var list = lookup.Values.ToList();
                    if (list.Count == 0)
                    {
                        return BllResultFactory<Dict>.Error("未查询到数据");
                    }
                    else
                    {
                        return BllResultFactory<Dict>.Sucess(list[0], "成功");
                    }
                }
                catch (Exception ex)
                {
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<Dict>.Error($"未查询到数据:{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 查询中控系统表的数据
        /// sys_dict_type
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public BllResult<SysDictType> GetSysDictTypeWithDetails(string v)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    var lookup = new Dictionary<int?, SysDictType>();

                    connection.Query<SysDictType, SysDictData, SysDictType>("select * from sys_dict_type a join sys_dict_data b on a.id = b.headerId where a.dictType = @code", (dict, dictDetail) =>
                    {
                        SysDictType tempDict;
                        if (!lookup.TryGetValue(dict.Id, out tempDict))
                        {
                            tempDict = dict;
                            lookup.Add(dict.Id, dict);
                        }
                        tempDict.DictDetails.Add(dictDetail);
                        return tempDict;
                    }, new { code = v });
                    var list = lookup.Values.ToList();
                    if (list.Count == 0)
                    {
                        return BllResultFactory<SysDictType>.Error("未查询到数据");
                    }
                    else
                    {
                        return BllResultFactory<SysDictType>.Sucess(list[0], "成功");
                    }
                }
                catch (Exception ex)
                {
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<SysDictType>.Error($"未查询到数据:{ex.Message}");
                }
            }
        }

        public BllResult<Dict> GetDictWithDetailsAndTrussCode(string trussCode)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    var list = connection.Query<Dict>("select * from station where transportNormal = @trussCode ", new { trussCode = trussCode }).ToList();

                    if (list.Count == 0)
                    {
                        return BllResultFactory<Dict>.Error("未查询到数据");
                    }
                    else
                    {
                        return BllResultFactory<Dict>.Sucess(list[0], "成功");
                    }
                }
                catch (Exception ex)
                {
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory<Dict>.Error($"未查询到数据:{ex.Message}");
                }
            }
        }

        #endregion

        #region Equipment

        public BllResult DeleteEuipmentTypeByIds(List<int> list)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    foreach (var item in list)
                    {
                        if (connection.Query<int>("select count(1) from equipment where equipmentTypeId = @equipmentTypeId", new { equipmentTypeId = item }).ToList()[0] > 0)
                        {
                            throw new BllException($"Id为{item}的设备类型存在对应设备，请先删除设备，再行删除");
                        }
                    }
                    tran = connection.BeginTransaction();
                    connection.DeleteList<EquipmentType>("where id in @ids", new { ids = list }, tran);
                    connection.DeleteList<EquipmentTypeTemplate>("where equipmentTypeId in @ids", new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("成功");
                }
                catch (BllException bllEx)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(bllEx);
                    return BllResultFactory.Error(bllEx.Message);
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory.Error($"删除失败：{ex.Message}");
                }
            }
        }

        public BllResult DeleteEquipmentTypeTemplateByIds(List<int> list)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    foreach (var item in list)
                    {
                        if (connection.Query<int>("select count(1) from equipment_prop where EquipmentTypeTemplateId = @id", new { id = item }).ToList()[0] > 0)
                        {
                            throw new BllException($"Id为{item}的设备类型属性模板存在对应数据，请先删除具体设备属性数据值，再行删除");
                        }
                    }
                    tran = connection.BeginTransaction();
                    connection.DeleteList<EquipmentTypeTemplate>("where id in @ids", new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("成功");
                }
                catch (BllException bllEx)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(bllEx);
                    return BllResultFactory.Error(bllEx.Message);
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory.Error($"删除失败：{ex.Message}");
                }
            }
        }

        public BllResult DeleteEuipmentByIds(List<int> ids)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    connection.DeleteList<EquipmentProp>("where equipmentId in @ids ", new { ids = ids }, tran);
                    connection.DeleteList<Equipment>("where id in @ids", new { ids = ids }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("成功");

                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory.Error($"删除失败：{ex.Message}");
                }
            }
        }

        public BllResult DeleteLEDS(List<int> list)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    connection.DeleteList<LEDEntity>("where id in @ids", new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("删除成功");
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory.Error("发生异常");
                }
            }
        }

        public BllResult SyncEquipmentProp(int? id, string plcDb)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();

                    //现获取这个设备，防止同步删除
                    var a = AppSession.Dal.GetCommonModelByCondition<Equipment>($"where id = {id}");
                    if (!a.Success)
                    {
                        tran.Rollback();
                        return BllResultFactory.Error("同步失败，设备已被同步删除");
                    }
                    var equipment = a.Data[0];
                    //查询对应模板的属性
                    var b = AppSession.Dal.GetCommonModelByCondition<EquipmentTypeTemplate>($"where equipmentTypeId = {equipment.EquipmentTypeId}");
                    if (!b.Success)
                    {
                        tran.Rollback();
                        return BllResultFactory.Error("同步失败，设备无模板属性");
                    }
                    var templateProps = b.Data;
                    var c = AppSession.Dal.GetCommonModelByCondition<EquipmentProp>($"where equipmentId = {equipment.Id}");
                    var props = new List<EquipmentProp>();
                    if (c.Success)
                    {
                        props = c.Data;
                    }
                    var idsFordelete = props.Where(t => !templateProps.Exists(i => i.Id == t.EquipmentTypeTemplateId)).Select(t => t.Id).ToList();
                    if (idsFordelete != null && idsFordelete.Count > 0)
                    {
                        connection.DeleteList<EquipmentProp>("where id in @ids", new { ids = idsFordelete }, tran);
                    }
                    var propForAdd = templateProps.Where(t => !props.Exists(i => i.EquipmentTypeTemplateId == t.Id)).ToList();
                    List<EquipmentProp> propListToAdd = new List<EquipmentProp>();
                    foreach (var item in propForAdd)
                    {
                        EquipmentProp equipmentProp = new EquipmentProp();
                        equipmentProp.EquipmentId = equipment.Id;
                        equipmentProp.EquipmentTypeTemplateId = item.Id;
                        equipmentProp.EquipmentTypeTemplateCode = item.Code;
                        equipmentProp.ServerHandle = 0;
                        if (string.IsNullOrWhiteSpace(plcDb) || !string.IsNullOrWhiteSpace(item.Address))
                        {
                            equipmentProp.Address = item.Address;
                        }
                        else
                        {
                            switch (item.DataType)
                            {
                                case "BOOL": { equipmentProp.Address = plcDb + "X" + item.Offset; break; }
                                case "BYTE": { equipmentProp.Address = plcDb + "B" + item.Offset; break; }
                                case "INT": { equipmentProp.Address = plcDb + "W" + item.Offset; break; }
                                case "DINT": { equipmentProp.Address = plcDb + "D" + item.Offset; break; }
                                case "CHAR": { equipmentProp.Address = plcDb + "CHAR" + item.Offset + ",20"; break; }
                                default: { equipmentProp.Address = item.Address; break; }
                            }
                        }

                        equipmentProp.Value = "";
                        equipmentProp.Remark = item.Name;
                        equipmentProp.CreateTime = DateTime.Now;
                        equipmentProp.CreateBy = "";
                        propListToAdd.Add(equipmentProp);
                    }
                    propListToAdd.ForEach(t => connection.Insert<EquipmentProp>(t, tran));
                    tran.Commit();
                    return BllResultFactory.Sucess("成功");

                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    AppSession.LogService.WriteDBExceptionLog(ex);
                    return BllResultFactory.Error($"删除失败：{ex.Message}");
                }
            }
        }

        public BllResult CopyEquipment(EquipmentType equipmentType)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    var temp = AppSession.Dal.GetCommonModelByCondition<EquipmentType>($"where id = {equipmentType.Id}", connection, transaction);
                    if (temp.Success)
                    {
                        var e = temp.Data[0];
                        var temp2 = AppSession.Dal.GetCommonModelByCondition<EquipmentTypeTemplate>($"where equipmentTypeId = {e.Id}", connection, transaction);
                        if (temp2.Success)
                        {
                            var props = temp2.Data;
                            //e.Id = null;
                            e.Code = e.Code + "2";
                            var temp3 = AppSession.Dal.InsertCommonModel<EquipmentType>(e, connection, transaction);
                            if (temp3.Success)
                            {
                                props.ForEach(t =>
                                {
                                    t.EquipmentTypeId = temp3.Data.Value;

                                });
                                foreach (var item in props)
                                {
                                    var temp4 = AppSession.Dal.InsertCommonModel<EquipmentTypeTemplate>(item, connection, transaction);
                                    if (!temp4.Success)
                                    {
                                        transaction?.Rollback();
                                        return BllResultFactory.Error($"复制出错：{temp4.Msg}");
                                    }
                                }
                                transaction.Commit();
                                return BllResultFactory.Sucess();
                            }
                            else
                            {
                                transaction?.Rollback();
                                return BllResultFactory.Error($"复制出错：{temp3.Msg}");
                            }
                        }
                        else
                        {
                            transaction?.Rollback();
                            return BllResultFactory.Error($"复制出错：{temp2.Msg}");
                        }
                    }
                    else
                    {
                        transaction?.Rollback();
                        return BllResultFactory.Error($"复制出错：{temp.Msg}");
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    return BllResultFactory.Error($"复制出错：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 根据PLC索引获取接出站台
        /// </summary>
        /// <param name="station"></param>
        /// <param name="roadWay"></param>
        /// <returns></returns>
        public BllResult<Equipment> GetOutStationByIndex(int? station, int roadWay)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    string sql = "SELECT a.* from equipment a join equipment_type b on a.equipmentTypeId = b.id where a.PLCStationIndex=@StationIndex and (b.code='StationForStockerOut' or b.code='StationForStockerInOrOut') and a.roadWay = @roadWay";
                    var stations = connection.Query<Equipment>(sql, new { StationIndex = station.Value, roadWay = roadWay }).ToList();
                    if (stations != null && stations.Count > 0)
                    {
                        return BllResultFactory<Equipment>.Sucess(stations[0]);
                    }
                    else
                    {
                        return BllResultFactory<Equipment>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<Equipment>.Error(ex.Message);
                }
            }
        }

        /// <summary>
        /// 根据PLC索引获取接入站台
        /// </summary>
        /// <param name="station"></param>
        /// <param name="roadWay"></param>
        /// <returns></returns>
        public BllResult<Equipment> GetInStationByIndex(int? station, int roadWay)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    string sql = "SELECT a.* from equipment a join equipment_type b on a.equipmentTypeId = b.id where a.PLCStationIndex=@StationIndex  and (b.code='StationForStockerIn' or b.code='StationForStockerInOrOut') and a.roadWay=@roadWay";
                    var stations = connection.Query<Equipment>(sql, new { StationIndex = station.Value, roadWay = roadWay }).ToList();
                    if (stations != null && stations.Count > 0)
                    {
                        return BllResultFactory<Equipment>.Sucess(stations[0]);
                    }
                    else
                    {
                        return BllResultFactory<Equipment>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<Equipment>.Error(ex.Message);
                }
            }
        }

        /// <summary>
        /// 根据站台软件索引获取站台
        /// </summary>
        /// <param name="stationIndex"></param>
        /// <returns></returns>
        public BllResult<Equipment> GetStationByIndex(int? stationIndex)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                try
                {
                    string sql = "SELECT a.* from equipment a join equipment_type b on a.equipmentTypeId = b.id where a.StationIndex=@StationIndex";
                    var stations = connection.Query<Equipment>(sql, new { StationIndex = stationIndex.Value }).ToList();
                    if (stations != null && stations.Count > 0)
                    {
                        return BllResultFactory<Equipment>.Sucess(stations[0]);
                    }
                    else
                    {
                        return BllResultFactory<Equipment>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<Equipment>.Error(ex.Message);
                }
            }
        }

        /// <summary>
        /// 复制一个设备
        /// </summary>
        /// <returns></returns>
        public BllResult CopyEquipment(int id)
        {
            var result = AppSession.Dal.GetCommonModelByCondition<Equipment>($"where id = {id}");
            if (!result.Success)
            {
                return BllResultFactory.Error(result.Msg);
            }
            else
            {
                var equipment = result.Data[0];
                //所有属性
                var result2 = AppSession.Dal.GetCommonModelByCondition<EquipmentProp>($"where EquipmentId = {id}");
                if (!result2.Success)
                {
                    return BllResultFactory.Error(result2.Msg);
                }
                var equipmentProps = result2.Data;
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction transaction = null;
                    try
                    {
                        connection.Open();
                        transaction = connection.BeginTransaction();
                        //equipment.Id = null;
                        equipment.Code = equipment.Code + DateTime.Now.Millisecond;
                        var i = connection.Insert<Equipment>(equipment, transaction);
                        foreach (var item in equipmentProps)
                        {
                            item.EquipmentId = i.Value;
                            //item.Id = null;
                            connection.Insert<EquipmentProp>(item, transaction);
                        }
                        transaction.Commit();
                        return BllResultFactory.Sucess("复制成功");
                    }
                    catch (Exception ex)
                    {
                        transaction?.Rollback();
                        return BllResultFactory.Error($"复制出现异常：{ex.Message}");
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
