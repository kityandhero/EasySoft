using System.Data;
using System.Dynamic;
using Dapper;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Dapper.Elegant.Configure;
using EasySoft.Core.Dapper.Enums;
using EasySoft.Core.Dapper.Interfaces;
using EasySoft.Core.ErrorLogTransmitter.Producers;
using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Entities;
using EasySoft.Core.SqlExecutionRecordTransmitter.Producers;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.Dapper.Elegant.Assists;

public class QueryAssist
{
    public static class SqlExecutorAssist
    {
        public static SqlExecutor GetExecutor(IApplicationChannel applicationChannel)
        {
            return new SqlExecutor();
        }
    }

    /// <summary>
    /// Sql执行辅助
    /// </summary>
    public class SqlExecutor
    {
        public DataTable QueryToDataTable(string sql, IMapperChannel mapperChannel)
        {
            try
            {
                LogSql(mapperChannel, sql);

                using var conn = mapperChannel.OpenConnection();

                var reader = conn.ExecuteReader(sql);

                return ConvertAssist.DataReaderToDataTable(reader);
            }
            catch (Exception ex)
            {
                LogError(ex, sql, mapperChannel);

                throw;
            }
        }

        public void ExecuteSql(string sql, IMapperChannel mapperChannel)
        {
            ExecuteSql(new List<string>()
            {
                sql
            }, mapperChannel);
        }

        public void ExecuteSql(
            IMapperChannel mapperChannel,
            params string[] sqlArray
        )
        {
            var listSql = Enumerable.ToList(sqlArray);

            ExecuteSql(listSql, mapperChannel);
        }

        /// <summary>
        /// 内置事务处理，主要用于写处理
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="mapperChannel"></param>
        public void ExecuteSql(List<string> listSql, IMapperChannel mapperChannel)
        {
            using var mapperTransaction = mapperChannel.CreateMapperTransaction();
            var dbTransaction = mapperTransaction.GetTransaction();

            try
            {
                foreach (var sql in listSql)
                {
                    try
                    {
                        LogSql(mapperChannel, sql);

                        dbTransaction.Connection.Execute(sql, null, dbTransaction);
                    }
                    catch (Exception e)
                    {
                        LogError(e, sql, mapperChannel);

                        throw;
                    }
                }

                mapperTransaction.Commit();
            }
            catch (Exception ex)
            {
                LogError(ex);

                mapperTransaction.Rollback();

                throw;
            }
        }

        /// <summary>
        /// 执行sql并返回第一个匹配的结果
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public ExpandoObject? ExecuteObject(
            string sql,
            IMapperChannel mapperChannel
        )
        {
            try
            {
                LogSql(mapperChannel, sql);

                using var conn = mapperChannel.OpenConnection();
                var reader = conn.ExecuteReader(sql);

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                return list.Count > 0 ? list[0] : null;
            }
            catch (Exception ex)
            {
                LogError(ex, sql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// 执行Sql语句并返回结果集合
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public ListResult<ExpandoObject> ExecuteObjectList(
            string sql,
            IMapperChannel mapperChannel
        )
        {
            try
            {
                LogSql(mapperChannel, sql);

                using var conn = mapperChannel.OpenConnection();
                var reader = conn.ExecuteReader(sql);

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                return new ListResult<ExpandoObject>(ReturnCode.Ok, list);
            }
            catch (Exception ex)
            {
                LogError(ex, sql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// 返回指定页码和条目数的符合条件的条目数
        /// </summary>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="tableName">表名</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <param name="enableTotalCache">启用总条目数缓存，具体缓存情况以及有效期依照总量数量动态判断，设为 false 可以关闭</param>
        /// <param name="totalCountCacheThreshold"></param>
        /// <param name="minTotalCacheSustainSecond"></param>
        /// <returns></returns>
        public int ExecuteTotalCount(
            string where,
            string tableName,
            IMapperChannel mapperChannel,
            bool enableTotalCache = true,
            int totalCountCacheThreshold = 2000,
            long minTotalCacheSustainSecond = 30
        )
        {
            var countSql = SqlAssist.BuildCountSql(@where, tableName);

            try
            {
                if (enableTotalCache)
                {
                    return GetTotalCount(
                        countSql,
                        totalCountCacheThreshold,
                        minTotalCacheSustainSecond,
                        mapperChannel,
                        () =>
                        {
                            using var conn = mapperChannel.OpenConnection();
                            var calculateSql = SqlAssist.BuildCountSql(where, tableName);

                            LogSql(mapperChannel, calculateSql);

                            return Convert.ToInt32(conn.ExecuteScalar(calculateSql));
                        }
                    );
                }
                else
                {
                    using var conn = mapperChannel.OpenConnection();
                    LogSql(mapperChannel, countSql);

                    return Convert.ToInt32(conn.ExecuteScalar(countSql));
                }
            }
            catch (Exception ex)
            {
                LogError(ex, countSql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey"></param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public bool ExecuteSingleTableDataExist(
            string where,
            string tableName,
            string primaryKey,
            IMapperChannel mapperChannel
        )
        {
            var sql = "";

            try
            {
                using var conn = mapperChannel.OpenConnection();
                sql = SqlAssist.BuildSingleTableListSql(primaryKey, where, "", tableName, primaryKey, 1);

                var reader = conn.ExecuteReader(sql);

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                LogSql(mapperChannel, sql);

                return list.Count > 0;
            }
            catch (Exception ex)
            {
                LogError(ex, sql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey"></param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public ExpandoObject? ExecuteSingleTableObject(
            string fields,
            string where,
            string tableName,
            string primaryKey,
            IMapperChannel mapperChannel
        )
        {
            return ExecuteSingleTableObject(
                fields,
                where,
                "",
                tableName,
                primaryKey,
                mapperChannel
            );
        }

        /// <summary>
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="sort"></param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey"></param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public ExpandoObject? ExecuteSingleTableObject(
            string fields,
            string where,
            string sort,
            string tableName,
            string primaryKey,
            IMapperChannel mapperChannel
        )
        {
            var sql = "";

            try
            {
                using var conn = mapperChannel.OpenConnection();
                sql =
                    SqlAssist.BuildSingleTableListSql(fields, where, sort, tableName, primaryKey, 1);

                var reader = conn.ExecuteReader(sql);

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                LogSql(mapperChannel, sql);

                return list.Count > 0 ? list[0] : null;
            }
            catch (Exception ex)
            {
                LogError(ex, sql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// 执行Sql语句并返回结果集合，构建适用于单表的高效查询
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="order">排序条件(不需要拼写 ORDER BY)</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey">主键名</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public ListResult<ExpandoObject> ExecuteSingleTableObjectList(
            string fields,
            string where,
            string order,
            string tableName,
            string primaryKey,
            IMapperChannel mapperChannel
        )
        {
            return ExecuteSingleTableObjectList(
                fields,
                where,
                order,
                tableName,
                primaryKey,
                null,
                mapperChannel
            );
        }

        /// <summary>
        /// 执行Sql语句并返回结果集合，构建适用于单表的高效查询
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="order">排序条件(不需要拼写 ORDER BY)</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey">主键名</param>
        /// <param name="top">取得的条目数，不需要请传递 null</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public ListResult<ExpandoObject> ExecuteSingleTableObjectList(
            string fields,
            string where,
            string order,
            string tableName,
            string primaryKey,
            int? top,
            IMapperChannel mapperChannel
        )
        {
            var sql = "";

            try
            {
                using var conn = mapperChannel.OpenConnection();
                sql = SqlAssist.BuildSingleTableListSql(
                    fields,
                    where,
                    order,
                    tableName,
                    primaryKey,
                    top
                );

                var reader = conn.ExecuteReader(sql);

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                LogSql(mapperChannel, sql);

                return new ListResult<ExpandoObject>(ReturnCode.Ok, list);
            }
            catch (Exception ex)
            {
                LogError(ex, sql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// 返回指定页码和条目数的符合条件的条目总数，适用于单表查询
        /// </summary>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="tableName">表名</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <param name="enableTotalCache">启用总条目数缓存，具体缓存情况以及有效期依照总量数量动态判断，可以关闭</param>
        /// <param name="totalCountCacheThreshold"></param>
        /// <param name="minTotalCacheSustainSecond"></param>
        /// <returns></returns>
        public int ExecuteSingleTableTotalCount(
            string where,
            string tableName,
            IMapperChannel mapperChannel,
            bool enableTotalCache = true,
            int totalCountCacheThreshold = 2000,
            long minTotalCacheSustainSecond = 30
        )
        {
            var countSql = SqlAssist.BuildCountSql(@where, tableName);

            try
            {
                if (enableTotalCache)
                {
                    return GetTotalCount(
                        countSql,
                        totalCountCacheThreshold,
                        minTotalCacheSustainSecond,
                        mapperChannel,
                        () =>
                        {
                            using var conn = mapperChannel.OpenConnection();
                            var calculateSql = SqlAssist.BuildCountSql(where, tableName);

                            LogSql(mapperChannel, calculateSql);

                            return Convert.ToInt32(conn.ExecuteScalar(calculateSql));
                        }
                    );
                }
                else
                {
                    using var conn = mapperChannel.OpenConnection();
                    LogSql(mapperChannel, countSql);

                    return Convert.ToInt32(conn.ExecuteScalar(countSql));
                }
            }
            catch (Exception ex)
            {
                LogError(ex, countSql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// 执行Sql(可以是存储过程文本)语句并返回结果集合
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public ListResult<ExpandoObject> ExecuteReturnObjectList(
            string sql,
            IMapperChannel mapperChannel
        )
        {
            try
            {
                LogSql(mapperChannel, sql);

                using var conn = mapperChannel.OpenConnection();

                var reader = conn.ExecuteReader(sql);

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                return new ListResult<ExpandoObject>(ReturnCode.Ok, list);
            }
            catch (Exception ex)
            {
                LogError(ex, sql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// 返回指定页码和条目数的符合条件的结果集合，内部生成适用于单表的高效查询
        /// </summary>
        /// <param name="pageSize">每页条目数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="order">排序条件(不需要拼写 ORDER BY)</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey"></param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public ListResult<ExpandoObject> ExecuteSingleTableObjectListSpecialPage(
            int pageIndex,
            int pageSize,
            string fields,
            string where,
            string order,
            string tableName,
            string primaryKey,
            IMapperChannel mapperChannel
        )
        {
            var pageSql = "";

            try
            {
                using var conn = mapperChannel.OpenConnection();
                pageSql = SqlAssist.BuildPageListSql(
                    pageIndex,
                    pageSize,
                    fields,
                    where,
                    order,
                    tableName,
                    primaryKey
                );

                var reader = conn.ExecuteReader(pageSql);

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                LogSql(mapperChannel, pageSql);

                return new ListResult<ExpandoObject>(ReturnCode.Ok, list);
            }
            catch (Exception ex)
            {
                LogError(ex, pageSql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// 返回指定页码和条目数的符合条件的结果集合和总条目数，内部生成适用于单表的高效查询
        /// </summary>
        /// <param name="pageSize">每页条目数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="order">排序条件(不需要拼写 ORDER BY)</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey"></param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <param name="enableTotalCache">启用总条目数缓存，具体缓存情况以及有效期依照总量数量动态判断，设为 false 可以关闭</param>
        /// <param name="totalCountCacheThreshold"></param>
        /// <param name="minTotalCacheSustainSecond"></param>
        /// <returns></returns>
        public PageListResult<ExpandoObject> ExecuteSingleTableObjectListPage(
            int pageSize,
            int pageIndex,
            string fields,
            string where,
            string order,
            string tableName,
            string primaryKey,
            IMapperChannel mapperChannel,
            bool enableTotalCache = true,
            int totalCountCacheThreshold = 2000,
            long minTotalCacheSustainSecond = 30
        )
        {
            return ExecuteSingleTableObjectListPage(pageSize, pageIndex, fields, where, order, "", tableName,
                primaryKey,
                mapperChannel, enableTotalCache, totalCountCacheThreshold, minTotalCacheSustainSecond);
        }

        /// <summary>
        /// 返回指定页码和条目数的符合条件的结果集合和总条目数，内部生成适用于单表的高效查询
        /// </summary>
        /// <param name="pageSize">每页条目数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="order">排序条件(不需要拼写 ORDER BY)</param>
        /// <param name="group"></param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey"></param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <param name="enableTotalCache">启用总条目数缓存，具体缓存情况以及有效期依照总量数量动态判断，设为 false 可以关闭</param>
        /// <param name="totalCountCacheThreshold"></param>
        /// <param name="minTotalCacheSustainSecond"></param>
        /// <returns></returns>
        public PageListResult<ExpandoObject> ExecuteSingleTableObjectListPage(
            int pageSize,
            int pageIndex,
            string fields,
            string where,
            string order,
            string group,
            string tableName,
            string primaryKey,
            IMapperChannel mapperChannel,
            bool enableTotalCache = true,
            int totalCountCacheThreshold = 2000,
            long minTotalCacheSustainSecond = 30
        )
        {
            var countSql = SqlAssist.BuildCountSql(where, tableName);
            var pageSql = "";

            try
            {
                if (enableTotalCache)
                {
                    var totalCount = GetTotalCount(
                        countSql,
                        totalCountCacheThreshold,
                        minTotalCacheSustainSecond,
                        mapperChannel,
                        () =>
                        {
                            using var conn = mapperChannel.OpenConnection();
                            var calculateSql = SqlAssist.BuildCountSql(where, tableName);

                            LogSql(mapperChannel, calculateSql);

                            return Convert.ToInt32(conn.ExecuteScalar(calculateSql));
                        }
                    );

                    using (var conn = mapperChannel.OpenConnection())
                    {
                        pageSql = SqlAssist.BuildPageListSqlWithSingleTable(
                            pageIndex,
                            pageSize,
                            fields,
                            where,
                            order,
                            group,
                            tableName,
                            primaryKey
                        );

                        var reader = conn.ExecuteReader(pageSql);

                        var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                        LogSql(mapperChannel, pageSql);

                        return new PageListResult<ExpandoObject>(ReturnCode.Ok, list, pageIndex, pageSize,
                            totalCount);
                    }
                }
                else
                {
                    using var conn = mapperChannel.OpenConnection();
                    pageSql = SqlAssist.BuildPageListSqlWithSingleTable(
                        pageIndex,
                        pageSize,
                        fields,
                        where,
                        order,
                        group,
                        tableName,
                        primaryKey
                    );

                    var reader = conn.ExecuteReader(pageSql);

                    var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }

                    var totalCount = Convert.ToInt32(conn.ExecuteScalar(countSql));

                    LogSql(mapperChannel, pageSql, countSql);

                    return new PageListResult<ExpandoObject>(
                        ReturnCode.Ok,
                        list,
                        pageIndex,
                        pageSize,
                        totalCount
                    );
                }
            }
            catch (Exception ex)
            {
                LogError(ex, new List<string> { countSql, pageSql }, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// 返回指定页码和条目数的符合条件的结果集合
        /// </summary>
        /// <param name="pageSize">每页条目数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="order">排序条件(不需要拼写 ORDER BY)</param>
        /// <param name="tableName">表名</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <returns></returns>
        public ListResult<ExpandoObject> ExecuteObjectListSpecialPage(
            int pageIndex,
            int pageSize,
            string fields,
            string where,
            string order,
            string tableName,
            IMapperChannel mapperChannel
        )
        {
            var pageSql = "";

            try
            {
                using var conn = mapperChannel.OpenConnection();

                pageSql = SqlAssist.BuildPageListSql(pageSize, pageIndex, fields, where, order, tableName);

                var reader = conn.ExecuteReader(pageSql);

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                LogSql(mapperChannel, pageSql);

                return new ListResult<ExpandoObject>(ReturnCode.Ok, list);
            }
            catch (Exception ex)
            {
                LogError(ex, pageSql, mapperChannel);

                throw;
            }
        }

        /// <summary>
        /// 返回指定页码和条目数的符合条件的结果集合和总条目数
        /// </summary>
        /// <param name="pageSize">每页条目数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="order">排序条件(不需要拼写 ORDER BY)</param>
        /// <param name="tableName">表名</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <param name="enableTotalCache">启用总条目数缓存，具体缓存情况以及有效期依照总量数量动态判断，设为 false 可以关闭</param>
        /// <param name="totalCountCacheThreshold"></param>
        /// <param name="minTotalCacheSustainSecond"></param>
        /// <returns></returns>
        public PageListResult<ExpandoObject> ExecuteObjectListPage(
            int pageSize,
            int pageIndex,
            string fields,
            string where,
            string order,
            string tableName,
            IMapperChannel mapperChannel,
            bool enableTotalCache = true,
            int totalCountCacheThreshold = 2000,
            long minTotalCacheSustainSecond = 30
        )
        {
            return ExecuteObjectListPage(
                pageSize,
                pageIndex,
                fields,
                where,
                order,
                "",
                tableName,
                mapperChannel,
                enableTotalCache,
                totalCountCacheThreshold,
                minTotalCacheSustainSecond
            );
        }

        /// <summary>
        /// 返回指定页码和条目数的符合条件的结果集合和总条目数
        /// </summary>
        /// <param name="pageSize">每页条目数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="fields">字段集合</param>
        /// <param name="where">筛选条件 (不需要加 WHERE)</param>
        /// <param name="order">排序条件(不需要拼写 ORDER BY)</param>
        /// <param name="group"></param>
        /// <param name="tableName">表名</param>
        /// <param name="mapperChannel">数据库通道</param>
        /// <param name="enableTotalCache">启用总条目数缓存，具体缓存情况以及有效期依照总量数量动态判断，设为 false 可以关闭</param>
        /// <param name="totalCountCacheThreshold"></param>
        /// <param name="minTotalCacheSustainSecond"></param>
        /// <returns></returns>
        public PageListResult<ExpandoObject> ExecuteObjectListPage(
            int pageSize,
            int pageIndex,
            string fields,
            string where,
            string order,
            string group,
            string tableName,
            IMapperChannel mapperChannel,
            bool enableTotalCache = true,
            int totalCountCacheThreshold = 2000,
            long minTotalCacheSustainSecond = 30
        )
        {
            var countSql = SqlAssist.BuildCountSql(where, tableName);
            var pageSql = "";

            try
            {
                if (enableTotalCache)
                {
                    var totalCount = GetTotalCount(
                        countSql,
                        totalCountCacheThreshold,
                        minTotalCacheSustainSecond,
                        mapperChannel,
                        () =>
                        {
                            using var conn = mapperChannel.OpenConnection();
                            var calculateSql = SqlAssist.BuildCountSql(where, tableName);

                            LogSql(mapperChannel, calculateSql);

                            return Convert.ToInt32(conn.ExecuteScalar(calculateSql));
                        }
                    );

                    using (var conn = mapperChannel.OpenConnection())
                    {
                        pageSql = SqlAssist.BuildPageListSql(
                            pageIndex,
                            pageSize,
                            fields,
                            where,
                            order,
                            group,
                            tableName
                        );

                        var reader = conn.ExecuteReader(pageSql);

                        var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                        LogSql(mapperChannel, pageSql);

                        return new PageListResult<ExpandoObject>(
                            ReturnCode.Ok,
                            list,
                            pageIndex,
                            pageSize,
                            totalCount
                        );
                    }
                }
                else
                {
                    using var conn = mapperChannel.OpenConnection();

                    pageSql = SqlAssist.BuildPageListSql(
                        pageIndex,
                        pageSize,
                        fields,
                        where,
                        order,
                        group,
                        tableName
                    );

                    var reader = conn.ExecuteReader(pageSql);

                    var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }

                    var totalCount = Convert.ToInt32(conn.ExecuteScalar(countSql));

                    LogSql(mapperChannel, pageSql, countSql);

                    return new PageListResult<ExpandoObject>(
                        ReturnCode.Ok,
                        list,
                        pageIndex,
                        pageSize,
                        totalCount
                    );
                }
            }
            catch (Exception ex)
            {
                LogError(ex, new List<string> { countSql, pageSql }, mapperChannel);

                throw;
            }
        }

        private static void LogSql(IMapperChannel mapperChannel, params string[] sqlArray)
        {
            if (sqlArray.Length <= 0)
            {
                return;
            }

            if (GeneralConfigAssist.GetRemoteGeneralLogSwitch())
            {
                return;
            }

            var applicationChannel = AutofacAssist.Instance.Resolve<IApplicationChannel>();

            if (!DapperElegantConfigurator.GetSqlLogRecordJudge()(applicationChannel.GetChannel()))
            {
                return;
            }

            var listRecord = (
                from sql in sqlArray
                where !string.IsNullOrWhiteSpace(sql)
                select new SqlExecutionMessage
                {
                    SqlExecutionMessageId = Guid.NewGuid().ToString(),
                    CommandString = sql,
                    ExecuteType = "",
                    StackTraceSnippet = "",
                    StartMilliseconds = 0,
                    DurationMilliseconds = 0,
                    FirstFetchDurationMilliseconds = 0,
                    Errored = 0,
                    TriggerChannel = applicationChannel.GetChannel(),
                    CollectMode = CollectMode.MiniProfiler.ToInt(),
                    DatabaseChannel = mapperChannel.GetChannel()
                }
            ).ToList();

            if (listRecord.Count > 0)
            {
                listRecord.ForEach(LogSqlExecutionMessage);
            }
        }

        /// <summary>
        /// 记录执行的sql
        /// </summary>
        /// <param name="sqlExecutionMessage">sql</param>
        private static void LogSqlExecutionMessage(SqlExecutionMessage sqlExecutionMessage)
        {
            AutofacAssist.Instance.Resolve<ISqlExecutionRecordProducer>().Send(
                sqlExecutionMessage.SqlExecutionMessageId,
                sqlExecutionMessage.CommandString,
                sqlExecutionMessage.ExecuteType,
                sqlExecutionMessage.StackTraceSnippet,
                sqlExecutionMessage.StartMilliseconds,
                sqlExecutionMessage.DurationMilliseconds,
                sqlExecutionMessage.FirstFetchDurationMilliseconds,
                sqlExecutionMessage.Errored,
                sqlExecutionMessage.TriggerChannel,
                sqlExecutionMessage.CollectMode,
                sqlExecutionMessage.DatabaseChannel
            );
        }

        private static void LogError(
            Exception exception
        )
        {
            AutofacAssist.Instance.Resolve<IErrorLogProducer>().Send(
                exception,
                0
            );
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="sql"></param>
        /// <param name="mapperChannel"></param>
        private static void LogError(
            Exception exception,
            string sql,
            IMapperChannel mapperChannel
        )
        {
            LogError(exception, new List<string> { sql }, mapperChannel);
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="sqlCollection"></param>
        /// <param name="mapperChannel"></param>
        private static void LogError(
            Exception exception,
            ICollection<string> sqlCollection,
            IMapperChannel mapperChannel
        )
        {
            if (GeneralConfigAssist.GetRemoteErrorLogSwitch())
            {
                return;
            }

            var sqlList = sqlCollection.ToListFilterNullOrWhiteSpace();

            var count = sqlList.Count;

            if (count > 0)
            {
                if (count > 1)
                {
                    var dic = new Dictionary<string, object>();

                    for (var i = 0; i < count; i++)
                    {
                        var sql = sqlList[i];

                        dic.Add($"sql{i}", sql);
                    }

                    exception.Data.Add("sql", ConvertAssist.DictionaryToExpandoObject(dic));
                }
                else
                {
                    exception.Data.Add("sql", $"[{mapperChannel.GetChannel()}] {sqlCollection.Join("")}");
                }

                exception.Data.Add("mapperChannel", mapperChannel.GetChannel());
            }

            AutofacAssist.Instance.Resolve<IErrorLogProducer>().Send(
                exception,
                0
            );
        }

        private static string BuildTotalCountResultCacheKey(string key, IMapperChannel mapperChannel)
        {
            return DapperElegantConfigurator.GetCacheOperator()?.BuildKey(
                "sql",
                "executor",
                "countSql",
                "result",
                key,
                mapperChannel.GetChannel()
            ) ?? "";
        }

        private static TimeSpan GetRandomTotalCountCacheExpireTime(
            int count,
            int totalCountCacheThreshold,
            long minTotalCacheSustainSecond,
            out bool needSetCatch
        )
        {
            needSetCatch = true;

            if (count <= totalCountCacheThreshold)
            {
                needSetCatch = false;
            }

            if (count > totalCountCacheThreshold && count <= totalCountCacheThreshold + 500)
            {
                var totalSeconds = minTotalCacheSustainSecond + RandomEx.ThreadSafeNext(30, 60);

                return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
            }

            if (count > totalCountCacheThreshold + 500 && count <= totalCountCacheThreshold + 1000)
            {
                var totalSeconds = minTotalCacheSustainSecond + RandomEx.ThreadSafeNext(60, 120);

                return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
            }

            if (count > totalCountCacheThreshold + 1000 && count <= totalCountCacheThreshold + 3000)
            {
                var totalSeconds = minTotalCacheSustainSecond + RandomEx.ThreadSafeNext(60, 120);

                return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
            }

            if (count > totalCountCacheThreshold + 3000)
            {
                var totalSeconds = minTotalCacheSustainSecond + RandomEx.ThreadSafeNext(600, 900);

                return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
            }
            else
            {
                var totalSeconds = minTotalCacheSustainSecond + RandomEx.ThreadSafeNext(30, 60);

                return new TimeSpan(TimeSpan.TicksPerSecond * totalSeconds);
            }
        }

        private int GetTotalCount(
            string countSql,
            int totalCountCacheThreshold,
            long minTotalCacheSustainSecond,
            IMapperChannel mapperChannel,
            Func<int> calculateFunc
        )
        {
            int result;

            var cacheOperator = DapperElegantConfigurator.GetCacheOperator();

            if (cacheOperator == null)
            {
                result = calculateFunc();

                return result;
            }

            var key = BuildTotalCountResultCacheKey(
                countSql.ToMd5(),
                mapperChannel
            );

            var resultCache = cacheOperator.Get<int>(key);

            if (resultCache.Success)
            {
                return resultCache.Data;
            }

            result = calculateFunc();

            var timeSpan = GetRandomTotalCountCacheExpireTime(
                result,
                totalCountCacheThreshold,
                minTotalCacheSustainSecond,
                out var needSetCatch
            );

            if (needSetCatch)
            {
                cacheOperator.Set(key, result, timeSpan);
            }

            return result;
        }
    }
}