using HHECS.Model.BllModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HHECS.DAL
{
    public abstract class DALBase
    {
        public string ConnectStr { get; set; }

        public abstract IDbConnection GetConnection();

        protected DALBase(string connectStr)
        {
            ConnectStr = connectStr;
        }



        #region Data

        public abstract BllResult<List<T>> GetCommonModelBySql<T>(string sql);
        public abstract BllResult<List<T>> GetCommonModelBySqlWithZero<T>(string sql);        
        public abstract BllResult<List<T>> GetCommonModelByCondition<T>(string v);
        public abstract BllResult<List<T>> GetCommonModelByCondition<T>(string v, object param);
        public abstract Task<BllResult<List<T>>> GetCommonModelByConditionAsync<T>(string v);
        public abstract Task<BllResult<List<T>>> GetCommonModelByConditionAsync<T>(string v, object param);
        public abstract BllResult<List<T>> GetCommonModelByCondition<T>(string v, IDbConnection connection, IDbTransaction tran);
        public abstract BllResult<List<T>> GetCommonModelByCondition<T>(string v, object param, IDbConnection connection, IDbTransaction tran);
        public abstract Task<BllResult<List<T>>> GetCommonModelByConditionAsync<T>(string v, IDbConnection connection, IDbTransaction tran);
        public abstract Task<BllResult<List<T>>> GetCommonModelByConditionAsync<T>(string v, object param, IDbConnection connection, IDbTransaction tran);

        public abstract BllResult<List<T>> GetCommonModelByConditionWithZero<T>(string v);
        public abstract BllResult<List<T>> GetCommonModelByConditionWithZero<T>(string v, object param);
        public abstract Task<BllResult<List<T>>> GetCommonModelByConditionWithZeroAsync<T>(string v);
        public abstract Task<BllResult<List<T>>> GetCommonModelByConditionWithZeroAsync<T>(string v, object param);
        public abstract BllResult<List<T>> GetCommonModelByConditionWithZero<T>(string v, IDbConnection connection, IDbTransaction tran);
        public abstract BllResult<List<T>> GetCommonModelByConditionWithZero<T>(string v, object param, IDbConnection connection, IDbTransaction tran);
        public abstract Task<BllResult<List<T>>> GetCommonModelByConditionWithZeroAsync<T>(string v, IDbConnection connection, IDbTransaction tran);
        public abstract Task<BllResult<List<T>>> GetCommonModelByConditionWithZeroAsync<T>(string v, object param, IDbConnection connection, IDbTransaction tran);

        public abstract BllResult<int> GetCommonModelCount<T>(string sql);
        public abstract BllResult<int> GetCommonModelCount<T>(string sql, IDbConnection connection, IDbTransaction tran);

        public abstract BllResult<int> GetCommonModelCount<T>(string sql, object param);
        public abstract BllResult<int> GetCommonModelCount<T>(string sql, object param, IDbConnection connection, IDbTransaction tran);

        public abstract BllResult<int?> InsertCommonModel<T>(T model);
        public abstract Task<BllResult<int?>> InsertCommonModelAsync<T>(T model);
        public abstract BllResult<int?> InsertCommonModel<T>(T model, IDbConnection connection, IDbTransaction tran);
        public abstract Task<BllResult<int?>> InsertCommonModelAsync<T>(T model, IDbConnection connection, IDbTransaction tran);

        public abstract BllResult UpdateCommonModel<T>(T model);
        public abstract Task<BllResult> UpdateCommonModelAsync<T>(T model);
        public abstract BllResult UpdateCommonModel<T>(T model, IDbConnection connection, IDbTransaction tran);
        public abstract Task<BllResult> UpdateCommonModelAsync<T>(T model, IDbConnection connection, IDbTransaction tran);

        public abstract BllResult DeleteCommonModelByIds<T>(List<int> list);
        public abstract BllResult DeleteCommonModelByIds<T>(List<int> list,string  x);
        public abstract Task<BllResult> DeleteCommonModelByIdsAsync<T>(List<int> list);
        public abstract BllResult DeleteCommonModelByIds<T>(List<int> list, IDbConnection connection, IDbTransaction tran);
        public abstract Task<BllResult> DeleteCommonModelByIdsAsync<T>(List<int> list, IDbConnection connection, IDbTransaction tran);

        public abstract BllResult<List<T>> GetCommonModeByPageCondition<T>(int pageNumber, int pageSize, string condition, string orderBy, object param = null);
        public abstract Task<BllResult<List<T>>> GetCommonModeByPageConditionAsync<T>(int pageNumber, int pageSize, string condition, string orderBy, object param = null);


        public abstract BllResult<List<T>> GetCommonModeByPageCondition<T>(IDbConnection connection, IDbTransaction tran, int pageNumber, int pageSize, string condition, string orderBy);
        public abstract Task<BllResult<List<T>>> GetCommonModeByPageConditionAsync<T>(IDbConnection connection, IDbTransaction tran, int pageNumber, int pageSize, string condition, string orderBy);


        public abstract BllResult<int> ExcuteCommonSqlForInsertOrUpdate(string sql);
        public abstract Task<BllResult<int>> ExcuteCommonSqlForInsertOrUpdateAsync(string sql);
        public abstract BllResult<int> ExcuteCommonSqlForInsertOrUpdate(string sql, IDbConnection connection, IDbTransaction tran);
        public abstract Task<BllResult<int>> ExcuteCommonSqlForInsertOrUpdateAsync(string sql, IDbConnection connection, IDbTransaction tran);

        public abstract BllResult<int> ExcuteCommonSqlForInsertOrUpdate(string sql, object param);
        public abstract Task<BllResult<int>> ExcuteCommonSqlForInsertOrUpdateAsync(string sql, object param);
        public abstract BllResult<int> ExcuteCommonSqlForInsertOrUpdate(string sql, object param, IDbConnection connection, IDbTransaction tran);
        public abstract Task<BllResult<int>> ExcuteCommonSqlForInsertOrUpdateAsync(string sql, object param, IDbConnection connection, IDbTransaction tran);


        public abstract BllResult DeleteContainer(List<int> list);
        #endregion
    }
}
