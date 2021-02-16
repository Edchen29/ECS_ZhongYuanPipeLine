using HHECS.Model.BllModel;
using HHECS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Bll
{
    public class StationService : BaseService
    {

        /// <summary>
        /// 删除站台数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BllResult StationDeleteId(List<int> ids, string userCode)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate("delete from station where id in @ids", new { ids }, connection, transaction);
                    if (result.Success)
                    {
                        transaction.Commit();
                        AppSession.LogService.LogContent(LogTitle.站台数据删除, $"ID数据：{string.Join(",", ids.Select(t => $"{t}").ToList())}", userCode, LogLevel.Success);
                        return BllResultFactory.Sucess("成功");
                    }
                    else
                    {
                        transaction.Rollback();
                        AppSession.LogService.LogContent(LogTitle.站台数据删除, $"ID数据：{string.Join(",", ids.Select(t => $"{t}").ToList())}", userCode, LogLevel.Error);
                        return BllResultFactory.Error($"失败：{result.Msg}");
                    }

                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    AppSession.LogService.LogContent(LogTitle.站台数据删除, $"ID数据：{string.Join(",", ids.Select(t => $"{t}").ToList())}", userCode, LogLevel.Exception);
                    return BllResultFactory.Error($"异常：{ex.Message}");
                }

            }
        }
    }
}
