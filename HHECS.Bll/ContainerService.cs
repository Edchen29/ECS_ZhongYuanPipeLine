using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHECS.Bll
{
    /// <summary>
    /// 容器处理
    /// </summary>
    public class ContainerService : BaseService
    {
        #region Container

        #region 容器批量新增
        /// <summary>
        /// 容器批量新增
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="volumeStartBit"></param>
        /// <param name="volumeEndBit"></param>
        /// <param name="warehouseCode"></param>
        /// <param name="num"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult CreateContainers(string code, string type, string status, int volumeStartBit, string volumeEndBit, string warehouseCode, int num, string userCode)
        {
            string sql = $"where 1=1";
            var tempCheck = AppSession.Dal.GetCommonModelByCondition<Container>(sql);
            if (!tempCheck.Success)
            {
                //查询不到数据，不应该报错
                //return BllResultFactory.Error($"查询容器表失败：原因{tempCheck.Msg}");
            }
            int CodeLength = code.Length;
            List<Container> containers = new List<Container>();
            //循环次数
            int loop = Convert.ToInt32(volumeEndBit) - Convert.ToInt32(volumeStartBit);
            int k = Convert.ToInt32(volumeStartBit);
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    for (int i = 1; i <= loop; i++)
                    {
                        Container container = new Container();
                        int o = k += 1;
                        //减去Code的长度
                        container.Code = code + o.ToString().PadLeft(num - CodeLength, '0');
                        container.Type = type;
                        container.Status = status;
                        container.WarehouseCode = warehouseCode;
                        container.CreateBy = userCode;
                        container.CreateTime = DateTime.Now;
                        containers.Add(container);

                        var a = tempCheck.Data.Select(t => t.Code == container.Code);
                        if (a == null)
                        {
                            transaction.Rollback();
                            return BllResultFactory.Error($"查询到新增容器中有Code一致数据");
                        }
                    }

                    foreach (var item in containers)
                    {
                        var result = AppSession.Dal.InsertCommonModel<Container>(item, connection, transaction);
                        if (!result.Success)
                        {
                            transaction.Rollback();
                            return BllResultFactory.Error($"新增容器失败:{result.Msg}");
                        }
                    }
                    transaction.Commit();
                    return BllResultFactory.Sucess("新增成功");
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    return BllResultFactory.Error($"异常：{ex.Message}");
                }
            }
        }
        #endregion

        #endregion
    }
}
