using Dapper;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HHECS.DAL
{
    public class DapperDAL : DALBase
    {
        public DapperDAL(string connectStr) : base(connectStr)
        {
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetConnection()
        {
            if (Dapper.SimpleCRUD.GetDialect() == Dapper.SimpleCRUD.Dialect.SQLServer.ToString())
            {
                return new SqlConnection(ConnectStr);
            }
            return new MySqlConnection(ConnectStr);
        }

        #region Data

        public override BllResult<List<T>> GetCommonModelBySql<T>(string sql)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    List<T> temp = connection.Query<T>(sql).ToList();
                    if (temp != null && temp.Count > 0)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp, "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override BllResult<List<T>> GetCommonModelBySqlWithZero<T>(string sql)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    List<T> temp = connection.Query<T>(sql).ToList();
                    if (temp != null)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp, "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override BllResult<List<T>> GetCommonModelByCondition<T>(string v)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    List<T> temp = connection.GetList<T>(v).ToList();
                    if (temp != null && temp.Count > 0)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp, "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }
        public override async Task<BllResult<List<T>>> GetCommonModelByConditionAsync<T>(string v)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    var temp = await connection.GetListAsync<T>(v);
                    if (temp != null && temp.ToList().Count > 0)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp.ToList(), "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override BllResult<List<T>> GetCommonModelByCondition<T>(string v, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                List<T> temp = connection.GetList<T>(v, transaction: tran).ToList();
                if (temp != null && temp.Count > 0)
                {
                    return BllResultFactory<List<T>>.Sucess(temp, "成功");
                }
                else
                {
                    return BllResultFactory<List<T>>.Error("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error("发生异常：" + ex.Message);
            }
        }

        public override async Task<BllResult<List<T>>> GetCommonModelByConditionAsync<T>(string v, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                var temp = await connection.GetListAsync<T>(v, transaction: tran);
                if (temp != null && temp.ToList().Count > 0)
                {
                    return BllResultFactory<List<T>>.Sucess(temp.ToList(), "成功");
                }
                else
                {
                    return BllResultFactory<List<T>>.Error("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
            }
        }

        public override BllResult<List<T>> GetCommonModelByConditionWithZero<T>(string v)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    List<T> temp = connection.GetList<T>(v).ToList();
                    if (temp != null)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp, "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override BllResult<int?> InsertCommonModel<T>(T model)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    return BllResultFactory<int?>.Sucess(connection.Insert<T>(model), "成功");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<int?>.Error("发生异常:" + ex.Message);
                }
            }
        }
        public override BllResult<int?> InsertCommonModel<T>(T model, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                return BllResultFactory<int?>.Sucess(connection.Insert<T>(model, transaction: tran), "成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory<int?>.Error("发生异常:" + ex.Message);
            }
        }

        public override BllResult UpdateCommonModel<T>(T model)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    connection.Update<T>(model);
                    return BllResultFactory.Sucess("成功");
                }
                catch (Exception ex)
                {
                    return BllResultFactory.Error("发生异常：" + ex.Message);
                }
            }
        }

        public override BllResult DeleteCommonModelByIds<T>(List<int> list)
        {
            using (IDbConnection connection = GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    connection.DeleteList<T>("where id in @ids", new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("成功");
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    return BllResultFactory.Error($"删除失败：{ex.Message}");
                }
            }
        }

        public override BllResult DeleteCommonModelByIds<T>(List<int> list,string  x)
        {
            using (IDbConnection connection = GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    connection.DeleteList<T>($"where {x} in @ids",new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("成功");
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    return BllResultFactory.Error($"删除失败：{ex.Message}");
                }
            }
        }

        public override BllResult<List<T>> GetCommonModeByPageCondition<T>(int pageNumber, int pageSize, string condition, string orderBy, object param = null)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    var a = connection.GetListPaged<T>(pageNumber, pageSize, condition, orderBy, param).ToList();
                    return BllResultFactory<List<T>>.Sucess(a, "成功");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error($"未查询到数据:{ex.Message}");
                }
            }
        }

        public override BllResult<int> GetCommonModelCount<T>(string sql)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    int a = connection.RecordCount<T>(sql);
                    return BllResultFactory<int>.Sucess(a, "");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<int>.Error($"未查询到数据:{ex.Message}");
                }
            }
        }

        public override BllResult<int> GetCommonModelCount<T>(string sql, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                int a = connection.RecordCount<T>(sql, transaction: tran);
                return BllResultFactory<int>.Sucess(a, "");
            }
            catch (Exception ex)
            {
                return BllResultFactory<int>.Error($"未查询到数据:{ex.Message}");
            }
        }

        public override BllResult<int> ExcuteCommonSqlForInsertOrUpdate(string sql)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    return BllResultFactory<int>.Sucess(connection.Execute(sql), "成功");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<int>.Error("出现异常：" + ex.Message);
                }
            }
        }

        public override BllResult<int> ExcuteCommonSqlForInsertOrUpdate(string sql, object param)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    return BllResultFactory<int>.Sucess(connection.Execute(sql, param), "成功)");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<int>.Error("出现异常：" + ex.Message);
                }
            }
        }


        public override async Task<BllResult<int?>> InsertCommonModelAsync<T>(T model)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    var a = await connection.InsertAsync<T>(model);
                    return BllResultFactory<int?>.Sucess(a, "成功");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<int?>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override async Task<BllResult<int?>> InsertCommonModelAsync<T>(T model, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                var a = await connection.InsertAsync<T>(model, transaction: tran);
                return BllResultFactory<int?>.Sucess(a, "成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory<int?>.Error("发生异常:" + ex.Message);
            }
        }

        public override async Task<BllResult> UpdateCommonModelAsync<T>(T model)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    await connection.UpdateAsync<T>(model);
                    return BllResultFactory.Sucess("成功");
                }
                catch (Exception ex)
                {
                    return BllResultFactory.Error("发生异常：" + ex.Message);
                }
            }
        }

        public override BllResult UpdateCommonModel<T>(T model, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                connection.Update<T>(model, transaction: tran);
                return BllResultFactory.Sucess("成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error("发生异常：" + ex.Message);
            }
        }

        public override async Task<BllResult> UpdateCommonModelAsync<T>(T model, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                await connection.UpdateAsync<T>(model, tran);
                return BllResultFactory.Sucess("成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error("发生异常：" + ex.Message);
            }
        }

        public override async Task<BllResult> DeleteCommonModelByIdsAsync<T>(List<int> list)
        {
            using (IDbConnection connection = GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    await connection.DeleteListAsync<T>("where id in @ids", new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("成功");
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    return BllResultFactory.Error($"删除失败：{ex.Message}");
                }
            }
        }



        public override BllResult DeleteCommonModelByIds<T>(List<int> list, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                connection.DeleteList<T>("where id in @ids", new { ids = list }, tran);
                return BllResultFactory.Sucess("成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"删除失败：{ex.Message}");
            }
        }

        public override async Task<BllResult> DeleteCommonModelByIdsAsync<T>(List<int> list, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                await connection.DeleteListAsync<T>("where id in @ids", new { ids = list }, tran);
                return BllResultFactory.Sucess("成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory.Error($"删除失败：{ex.Message}");
            }
        }

        public override async Task<BllResult<List<T>>> GetCommonModeByPageConditionAsync<T>(int pageNumber, int pageSize, string condition, string orderBy, object param = null)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    var a = await connection.GetListPagedAsync<T>(pageNumber, pageSize, condition, orderBy, param);
                    return BllResultFactory<List<T>>.Sucess(a.ToList(), "成功");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error($"未查询到数据:{ex.Message}");
                }
            }
        }

        public override BllResult<List<T>> GetCommonModeByPageCondition<T>(IDbConnection connection, IDbTransaction tran, int pageNumber, int pageSize, string condition, string orderBy)
        {
            try
            {
                var a = connection.GetListPaged<T>(pageNumber, pageSize, condition, orderBy);
                return BllResultFactory<List<T>>.Sucess(a.ToList(), "成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error($"未查询到数据:{ex.Message}");
            }
        }

        public override async Task<BllResult<List<T>>> GetCommonModeByPageConditionAsync<T>(IDbConnection connection, IDbTransaction tran, int pageNumber, int pageSize, string condition, string orderBy)
        {
            try
            {
                var a = await connection.GetListPagedAsync<T>(pageNumber, pageSize, condition, orderBy);
                return BllResultFactory<List<T>>.Sucess(a.ToList(), "成功");
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error($"未查询到数据:{ex.Message}");
            }
        }

        public override async Task<BllResult<int>> ExcuteCommonSqlForInsertOrUpdateAsync(string sql)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    return BllResultFactory<int>.Sucess(await connection.ExecuteAsync(sql), "成功)");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<int>.Error("出现异常：" + ex.Message);
                }
            }
        }

        public override BllResult<int> ExcuteCommonSqlForInsertOrUpdate(string sql, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                return BllResultFactory<int>.Sucess(connection.Execute(sql, transaction: tran), "成功)");
            }
            catch (Exception ex)
            {
                return BllResultFactory<int>.Error("出现异常：" + ex.Message);
            }
        }

        public override async Task<BllResult<int>> ExcuteCommonSqlForInsertOrUpdateAsync(string sql, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                return BllResultFactory<int>.Sucess(await connection.ExecuteAsync(sql, transaction: tran), "成功)");
            }
            catch (Exception ex)
            {
                return BllResultFactory<int>.Error("出现异常：" + ex.Message);
            }
        }

        public override async Task<BllResult<int>> ExcuteCommonSqlForInsertOrUpdateAsync(string sql, object param)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    return BllResultFactory<int>.Sucess(await connection.ExecuteAsync(sql, param), "成功)");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<int>.Error("出现异常：" + ex.Message);
                }
            }
        }

        public override BllResult<int> ExcuteCommonSqlForInsertOrUpdate(string sql, object param, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                return BllResultFactory<int>.Sucess(connection.Execute(sql, param, tran), "成功)");
            }
            catch (Exception ex)
            {
                return BllResultFactory<int>.Error("出现异常：" + ex.Message);
            }
        }

        public override async Task<BllResult<int>> ExcuteCommonSqlForInsertOrUpdateAsync(string sql, object param, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                return BllResultFactory<int>.Sucess(await connection.ExecuteAsync(sql, param, tran), "成功)");
            }
            catch (Exception ex)
            {
                return BllResultFactory<int>.Error("出现异常：" + ex.Message);
            }
        }

        public override async Task<BllResult<List<T>>> GetCommonModelByConditionWithZeroAsync<T>(string v)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    List<T> temp = (await connection.GetListAsync<T>(v)).ToList();
                    if (temp != null)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp, "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override BllResult<List<T>> GetCommonModelByConditionWithZero<T>(string v, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                List<T> temp = connection.GetList<T>(v, transaction: tran).ToList();
                if (temp != null)
                {
                    return BllResultFactory<List<T>>.Sucess(temp, "成功");
                }
                else
                {
                    return BllResultFactory<List<T>>.Error("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
            }
        }

        public override async Task<BllResult<List<T>>> GetCommonModelByConditionWithZeroAsync<T>(string v, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                List<T> temp = (await connection.GetListAsync<T>(v, transaction: tran)).ToList();
                if (temp != null)
                {
                    return BllResultFactory<List<T>>.Sucess(temp, "成功");
                }
                else
                {
                    return BllResultFactory<List<T>>.Error("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
            }
        }

        /// <summary>
        /// 删除容器
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override BllResult DeleteContainer(List<int> list)
        {
            using (IDbConnection connection = GetConnection())
            {
                IDbTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    connection.DeleteList<Container>("where id in @ids", new { ids = list }, tran);
                    tran.Commit();
                    return BllResultFactory.Sucess("删除成功");
                }
                catch (Exception ex)
                {
                    tran?.Rollback();
                    return BllResultFactory.Error($"发生异常:{ex.Message}");
                }
            }
        }

        public override BllResult<List<T>> GetCommonModelByCondition<T>(string v, object param)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    List<T> temp = connection.GetList<T>(v, parameters: param).ToList();
                    if (temp != null && temp.Count > 0)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp, "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override async Task<BllResult<List<T>>> GetCommonModelByConditionAsync<T>(string v, object param)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    var temp = await connection.GetListAsync<T>(v, parameters: param);
                    if (temp != null && temp.ToList().Count > 0)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp.ToList(), "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override BllResult<List<T>> GetCommonModelByCondition<T>(string v, object param, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                List<T> temp = connection.GetList<T>(v, parameters: param, transaction: tran).ToList();
                if (temp != null && temp.Count > 0)
                {
                    return BllResultFactory<List<T>>.Sucess(temp, "成功");
                }
                else
                {
                    return BllResultFactory<List<T>>.Error("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error("发生异常：" + ex.Message);
            }
        }

        public override async Task<BllResult<List<T>>> GetCommonModelByConditionAsync<T>(string v, object param, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                var temp = await connection.GetListAsync<T>(v, parameters: param, transaction: tran);
                if (temp != null && temp.ToList().Count > 0)
                {
                    return BllResultFactory<List<T>>.Sucess(temp.ToList(), "成功");
                }
                else
                {
                    return BllResultFactory<List<T>>.Error("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
            }
        }

        public override BllResult<List<T>> GetCommonModelByConditionWithZero<T>(string v, object param)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    List<T> temp = connection.GetList<T>(v, param).ToList();
                    if (temp != null)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp, "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override async Task<BllResult<List<T>>> GetCommonModelByConditionWithZeroAsync<T>(string v, object param)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    List<T> temp = (await connection.GetListAsync<T>(v, param)).ToList();
                    if (temp != null)
                    {
                        return BllResultFactory<List<T>>.Sucess(temp, "成功");
                    }
                    else
                    {
                        return BllResultFactory<List<T>>.Error("未查询到数据");
                    }
                }
                catch (Exception ex)
                {
                    return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
                }
            }
        }

        public override BllResult<List<T>> GetCommonModelByConditionWithZero<T>(string v, object param, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                List<T> temp = connection.GetList<T>(v, parameters: param, transaction: tran).ToList();
                if (temp != null)
                {
                    return BllResultFactory<List<T>>.Sucess(temp, "成功");
                }
                else
                {
                    return BllResultFactory<List<T>>.Error("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
            }
        }

        public override async Task<BllResult<List<T>>> GetCommonModelByConditionWithZeroAsync<T>(string v, object param, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                List<T> temp = (await connection.GetListAsync<T>(v, transaction: tran)).ToList();
                if (temp != null)
                {
                    return BllResultFactory<List<T>>.Sucess(temp, "成功");
                }
                else
                {
                    return BllResultFactory<List<T>>.Error("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<List<T>>.Error("发生异常:" + ex.Message);
            }
        }

        public override BllResult<int> GetCommonModelCount<T>(string sql, object param)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    int a = connection.RecordCount<T>(sql, param);
                    return BllResultFactory<int>.Sucess(a, "");
                }
                catch (Exception ex)
                {
                    return BllResultFactory<int>.Error($"未查询到数据:{ex.Message}");
                }
            }
        }

        public override BllResult<int> GetCommonModelCount<T>(string sql, object param, IDbConnection connection, IDbTransaction tran)
        {
            try
            {
                int a = connection.RecordCount<T>(sql, param, transaction: tran);
                return BllResultFactory<int>.Sucess(a, "");
            }
            catch (Exception ex)
            {
                return BllResultFactory<int>.Error($"未查询到数据:{ex.Message}");
            }
        }

        #endregion
    }
}
