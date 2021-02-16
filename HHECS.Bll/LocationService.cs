using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HHECS.DAL;
using HHECS.Model.BllModel;
using HHECS.Model.Entities;
using HHECS.Model.Enums;
using HHECS.Model.Enums.Task;

namespace HHECS.Bll
{
    public class LocationService : BaseService
    {

        #region Location

        /// <summary>
        /// 更新库位的状态 带仓库维度进行处理
        /// </summary>
        /// <param name="locationCode"></param>
        /// <param name="warehouseCode"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public BllResult UpdateLocationStatus(string locationCode, string warehouseCode, LocationLockStatus newStatus)
        {
            int status = (int)newStatus;
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                string sql = "update location set isLock = @status where code = @code and warehouseCode=@warehouseCode;";
                var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql, new { status = status, code = locationCode, warehouseCode = warehouseCode });
                if (!result.Success)
                {
                    return BllResultFactory.Error("失败");
                }
                else
                {
                    return BllResultFactory.Sucess("成功");
                }
            }
        }

        /// <summary>
        /// 更新库位的状态 带仓库维度进行处理
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="locationCode"></param>
        /// <param name="warehouseCode"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public BllResult UpdateLocationStatus(IDbConnection connection, IDbTransaction transaction, string locationCode, string warehouseCode, LocationLockStatus newStatus)
        {

            string sql = "update location set isLock = @status where code = @code and warehouseCode=@warehouseCode;";
            int status = (int)newStatus;
            var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate(sql, new { status = status, code = locationCode, warehouseCode = warehouseCode }, connection, transaction);
            if (!result.Success)
            {
                return BllResultFactory.Error("失败");
            }
            else
            {
                return BllResultFactory.Sucess("成功");
            }
        }

        /// <summary>
        /// 获取所有库位 根据条件
        /// </summary>
        /// <param name="containerCode"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="layer"></param>
        /// <param name="roadway"></param>
        /// <param name="status"></param>
        /// <param name="code"></param>
        /// <param name="warehouseCode"></param>
        /// <returns></returns>
        public BllResult<List<Location>> GetAllLocations(string containerCode, string row, string column, string layer, string roadway, string status, string code, string warehouseCode)
        {
            try
            {
                string sql = "where 1=1 ";
                if (!String.IsNullOrEmpty(containerCode))
                {
                    sql += " and containerCode = '" + containerCode + "'";
                }
                if (!String.IsNullOrEmpty(row))
                {
                    sql += " and row =" + row;
                }
                if (!String.IsNullOrEmpty(column))
                {
                    sql += " and column =" + column;
                }
                if (!String.IsNullOrEmpty(layer))
                {
                    sql += " and layer =" + layer;
                }
                if (!String.IsNullOrEmpty(roadway))
                {
                    sql += " and roadway =" + roadway;
                }
                if (!String.IsNullOrEmpty(status) && status != "all")
                {
                    sql += " and status = '" + status + "'";
                }
                if (!String.IsNullOrEmpty(code))
                {
                    sql += " and code ='" + code + "'";
                }
                if (!String.IsNullOrEmpty(warehouseCode))
                {
                    sql += $" and warehouseCode = '{warehouseCode}'";
                }

                var resut = AppSession.Dal.GetCommonModelByCondition<Location>(sql);
                if (resut.Success)
                {
                    return BllResultFactory<List<Location>>.Sucess(resut.Data, "成功");
                }
                else
                {
                    return BllResultFactory<List<Location>>.Error("未查询到数据");
                }

            }
            catch (Exception ex)
            {
                return BllResultFactory<List<Location>>.Error("访问异常：" + ex.ToString());
            }
        }
       
        /// <summary>
        /// 获取所有库位 这里设计 转轨堆垛机专用
        /// </summary>
        /// <param name="smallColumn"></param>
        /// <param name="bigColumn"></param>
        /// <param name="roadway"></param>
        /// <returns></returns>
        public BllResult<List<Location>> GetAllLocationsByColumnSpanAndRoadway(int smallColumn, int bigColumn, int roadway, string warehouseCode)
        {
            try
            {
                if (smallColumn == 0 || bigColumn == 0 || smallColumn > bigColumn)
                {
                    return BllResultFactory<List<Location>>.Error("输入参数有误，请核对column的两个值");
                }
                if (roadway == 0)
                {
                    return BllResultFactory<List<Location>>.Error("输入参数有误，请核对roadway值");
                }
                string sql = "where 1=1 ";
                sql += $" and column between {smallColumn} and {bigColumn} and roadway ={roadway} ";
                if (!String.IsNullOrEmpty(warehouseCode))
                {
                    sql += $" and warehouseCode = '{warehouseCode}'";
                }

                var resut = AppSession.Dal.GetCommonModelByCondition<Location>(sql);
                if (resut.Success)
                {
                    return BllResultFactory<List<Location>>.Sucess(resut.Data, "成功");
                }
                else
                {
                    return BllResultFactory<List<Location>>.Error("未查询到数据");
                }

            }
            catch (Exception ex)
            {
                return BllResultFactory<List<Location>>.Error("访问异常：" + ex.ToString());
            }
        }
       
        /// <summary>
        /// 根据库位编码获取库位信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public BllResult<Location> GetLocationByCode(string code)
        {
            try
            {
                var resut = AppSession.Dal.GetCommonModelByCondition<Location>($" where code={code}");
                if (resut.Success)
                {
                    if (resut.Data.Count == 1)
                    {
                        return BllResultFactory<Location>.Sucess(resut.Data[0], "成功");
                    }
                    else
                    {
                        return BllResultFactory<Location>.Error($"查询到多条数据，请核对库位:{code}");
                    }
                }
                else
                {
                    return BllResultFactory<Location>.Error("未查询到数据");
                }
            }
            catch (Exception ex)
            {
                return BllResultFactory<Location>.Error(ex.Message);
            }
        }
        /// <summary>
        /// 创建单个货位
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="connector"></param>
        /// <param name="rowIndex"></param>
        /// <param name="totalRows"></param>
        /// <param name="totalColumns"></param>
        /// <param name="totalLayers"></param>
        /// <param name="destinationArea"></param>
        /// <param name="warehouseCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public BllResult CreateLocation(string prefix, string connector, string srmCode,string type, int rowIndex1, int rowIndex2, int totalRows, int totalColumns, int totalLayers, string destinationArea, string warehouseCode, string userCode)
        {
            if (rowIndex1 <= 0 || rowIndex2 <= 0 || totalColumns <= 0 || totalLayers <= 0 || totalRows <= 0||string.IsNullOrWhiteSpace(srmCode))
            {
                return BllResultFactory.Error("堆垛机标识、行列层和行索引不能小于等于0");
            }
            else
            {
                Location location = new Location();
                location.Code = $"{prefix}{totalRows.ToString().PadLeft(2, '0')}{connector}{totalColumns.ToString().PadLeft(2, '0')}{connector}{totalLayers.ToString().PadLeft(2, '0')}";
                location.RoadWay = totalRows % 2 == 0 ? totalRows / 2 : totalRows / 2 + 1;
                location.Row = (short)totalRows;
                location.Line = (short)totalColumns;
                location.Layer = (short)totalLayers;
                location.RowIndex1 = (short)rowIndex1;
                location.RowIndex2 = (short)rowIndex2;
                location.SrmCode = srmCode;
                location.Type = type;
                location.IsLock = (short)LocationLockStatus.空闲;
                location.ContainerCode = "";
                location.WarehouseCode = warehouseCode;
                location.DestinationArea = destinationArea;
                location.CreateTime = DateTime.Now;
                location.CreateBy = userCode;
                var a = AppSession.Dal.InsertCommonModel<Location>(location);
                if (!a.Success)
                {
                    return BllResultFactory.Error($"新增库位失败:{a.Msg}");
                }
                else
                {
                    return BllResultFactory.Sucess($"新增库位成功");
                }
            }
        }

        /// <summary>
        /// 批量创建货位
        /// </summary>
        /// <param name="rowIndex1"></param>
        /// <param name="totalRows"></param>
        /// <param name="totalColumns"></param>
        /// <param name="totalLayers"></param>
        /// <returns></returns>
        public BllResult CreateLocations(string prefix, string connector, string srmCode,string type, int rowIndex1, int rowIndex2, int totalRows, int totalColumns, int totalLayers, string destinationArea, string warehouseCode,string userCode)
        {
            if (rowIndex1 <= 0 || rowIndex2 <= 0 || totalColumns <= 0 || totalLayers <= 0 || totalRows <= 0 || string.IsNullOrWhiteSpace(srmCode))
            {
                return BllResultFactory.Error("堆垛机标识、行列层和行索引不能小于等于0");
            }
            else
            {
                using (IDbConnection connection = AppSession.Dal.GetConnection())
                {
                    IDbTransaction transaction = null;
                    try
                    {
                        connection.Open();
                        transaction = connection.BeginTransaction();
                        for (int i = 1; i <= totalRows; i++)
                        {
                            for (int j = 1; j <= totalColumns; j++)
                            {
                                for (int k = 1; k <= totalLayers; k++)
                                {
                                    Location location = new Location();
                                    location.Code = $"{prefix}{i.ToString().PadLeft(2, '0')}{connector}{j.ToString().PadLeft(2, '0')}{connector}{k.ToString().PadLeft(2, '0')}";
                                    location.RoadWay = i % 2 == 0 ? i / 2 : i / 2 + 1;
                                    location.Row = (short)i;
                                    location.Line = (short)j;
                                    location.Layer = (short)k;
                                    location.RowIndex1 = (short)rowIndex1;
                                    location.RowIndex2 = (short)rowIndex2;
                                    location.SrmCode = srmCode;
                                    location.Type = type;
                                    location.IsLock = (short)LocationLockStatus.空闲;
                                    location.ContainerCode = "";
                                    location.WarehouseCode = warehouseCode;
                                    location.DestinationArea = destinationArea;
                                    location.CreateTime = DateTime.Now;
                                    location.CreateBy = userCode;
                                    var a = AppSession.Dal.InsertCommonModel<Location>(location, connection, transaction);
                                    if (!a.Success)
                                    {
                                        transaction.Rollback();
                                        return BllResultFactory.Error($"新增库位失败:{a.Msg}");
                                    }
                                }
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
        }

        /// <summary>
        /// 批量删除货位
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public BllResult DeleteLocations(List<string> codes)
        {
            using (IDbConnection connection = AppSession.Dal.GetConnection())
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    var tasks = AppSession.Dal.GetCommonModelByConditionWithZero<TaskEntity>($"where taskStatus < {(int)TaskEntityStatus.任务完成}", connection, transaction).Data;
                    //判断是否库位在任务中
                    if (tasks.Count(t => codes.Exists(a => a == t.FromLocationCode) || codes.Exists(a => a == t.ToLocationCode)) > 0)
                    {
                        transaction.Rollback();
                        return BllResultFactory.Error($"所选库位中存在正在执行的任务，操作中止，请先完成任务再删除库位");
                    }
                    else
                    {
                        var result = AppSession.Dal.ExcuteCommonSqlForInsertOrUpdate("delete from location where code in @codes", new { codes }, connection, transaction);

                        if (result.Success)
                        {
                            transaction.Commit();
                            return BllResultFactory.Sucess("成功");
                        }
                        else
                        {
                            transaction.Rollback();
                            return BllResultFactory.Error($"失败：{result.Msg}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    return BllResultFactory.Error($"异常：{ex.Message}");
                }
            }
        }

        #endregion
    }
}
