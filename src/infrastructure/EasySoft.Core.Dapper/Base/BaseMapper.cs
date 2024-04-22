using EasySoft.Core.Dapper.Enums;
using EasySoft.Core.Dapper.Interfaces;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Extensions;
using EasySoft.Core.Sql.Factories;
using EasySoft.Core.Sql.Interfaces;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Messages;
using EasySoft.UtilityTools.Standard.Result.Factories;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.Dapper.Base;

public class BaseMapper<T> : IMapper where T : IEntitySelf<T>, new()
{
    private readonly IMapperChannel _mapperChannel;

    private readonly IMapperTransaction? _mapperTransaction;

    private readonly bool _sqlLogRecordSwitch;

    #region Constructor

    public BaseMapper(IMapperChannel mapperChannel, bool logSql = true)
    {
        _mapperChannel = mapperChannel ?? throw new Exception("无效的mapperChannel");

        _sqlLogRecordSwitch = logSql && SqlLogSwitchAssist.GetCurrentSwitch();
    }

    public BaseMapper(IMapperTransaction mapperTransaction, bool logSql = true)
    {
        _mapperTransaction = mapperTransaction ?? throw new Exception("无效的mapperTransaction");

        _mapperChannel = _mapperTransaction.GetMapperChannel();

        _sqlLogRecordSwitch = logSql && SqlLogSwitchAssist.GetCurrentSwitch();
    }

    #endregion Constructor

    #region Method

    #region Get

    /// <summary>
    /// 获取实例
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public T? Get(object id)
    {
        var m = new T();

        ICollection<Condition<T>> conditions = new List<Condition<T>>
        {
            new()
            {
                Expression = m.GetPrimaryKeyLambda(),
                ConditionType = ConditionType.Eq,
                Value = id
            }
        };

        return GetBy(conditions, false);
    }

    /// <summary>
    /// 获取实例并将实例转换为属性名首字母小写的Object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    protected object? GetObject(object id)
    {
        var o = Get(id);

        return o == null ? null : o.ToObject();
    }

    public T? GetBy(Condition<T> conditions, bool transferWrapperQuery = true)
    {
        return GetBy(
            new List<Condition<T>>
            {
                conditions
            },
            new List<Sort<T>>(),
            transferWrapperQuery
        );
    }

    public T? GetBy(Condition<T> conditions, Sort<T> sort, bool transferWrapperQuery = true)
    {
        return GetBy(
            new List<Condition<T>>
            {
                conditions
            },
            new List<Sort<T>>
            {
                sort
            },
            transferWrapperQuery
        );
    }

    public T? GetBy(Condition<T> conditions, IList<Sort<T>> sorts, bool transferWrapperQuery = true)
    {
        return GetBy(
            new List<Condition<T>>
            {
                conditions
            },
            sorts,
            transferWrapperQuery
        );
    }

    public T? GetBy(ICollection<Condition<T>> conditions, bool transferWrapperQuery = true)
    {
        return GetBy(
            conditions,
            new List<Sort<T>>(),
            transferWrapperQuery
        );
    }

    public T? GetBy(ICollection<Condition<T>> conditions, Sort<T> sort, bool transferWrapperQuery = true)
    {
        return GetBy(
            conditions,
            new List<Sort<T>>
            {
                sort
            },
            transferWrapperQuery
        );
    }

    /// <summary>
    /// 按照指定条件获取实例
    /// </summary>
    /// <returns></returns>
    public T? GetBy(ICollection<Condition<T>> conditions, IList<Sort<T>>? sorts, bool transferWrapperQuery = true)
    {
        var model = new T();

        if (conditions == null || conditions.Count < 1)
        {
            throw new Exception("需要设定查询参数");
        }

        AdvanceSqlBuilder builder;

        if (transferWrapperQuery)
        {
            builder = new AdvanceSqlBuilder().Select()
                .AppendFragment($" {(transferWrapperQuery ? " TOP 1 " : "")}{model.GetPrimaryKeyName()} ")
                .From(model);
        }
        else
        {
            builder = new AdvanceSqlBuilder().Select().AppendFragment(" TOP 1 ").AllFields(model).From(model);
        }

        if (conditions.Count > 0)
        {
            builder.AppendFragment(" WHERE ");

            foreach (var c in conditions)
            {
                if (c.ConditionType == ConditionType.In || c.ConditionType == ConditionType.NotIn)
                {
                    builder = builder.LinkCondition(c);
                }
                else
                {
                    builder = builder.LinkCondition(c);
                }
            }
        }

        if (sorts is { Count: > 0 })
        {
            for (var i = 0; i < sorts.Count; i++)
            {
                var s = sorts[i];

                builder = i == 0
                    ? builder.OrderBy(s.Expression, s.SortType)
                    : builder.AndOrderBy(s.Expression, s.SortType);
            }
        }

        if (transferWrapperQuery)
        {
            builder = new AdvanceSqlBuilder().Select()
                .AllFields(model)
                .From(model)
                .AppendFragment($" WHERE {model.GetPrimaryKeyName()} = ({builder})");
        }

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                var data = conn.Query<T>(builder.Sql).SingleOrDefault();

                LogSqlExecutionMessage();

                return data;
            }
            catch (Exception e)
            {
                e.Data.Add("query", builder);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var data = dbTransaction.Connection.Query<T>(
                        builder.Sql,
                        null,
                        dbTransaction
                    )
                    .SingleOrDefault();

                LogSqlExecutionMessage();

                return data;
            }
            catch (Exception e)
            {
                e.Data.Add("query", builder);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    /// <summary>
    /// 获取实例
    /// </summary>
    /// <returns></returns>
    public object? GetByObject(ICollection<Condition<T>> conditions, IList<Sort<T>>? sorts = null)
    {
        var o = GetBy(conditions, sorts);

        return o == null ? null : o.ToObject();
    }

    #region Exist

    public bool Exist(long id)
    {
        var o = Get(id);

        return o != null;
    }

    public bool Exist(Condition<T> conditions)
    {
        var o = GetBy(conditions);

        return o != null;
    }

    public bool Exist(ICollection<Condition<T>> conditions)
    {
        var o = GetBy(conditions);

        return o != null;
    }

    #endregion

    private string AddCore(T model)
    {
        model = PreAdd(model);
        model = PreSave(model);
        model = CheckNullValue(model);

        if (model.Id <= 0)
        {
            model.Id = IdentifierAssist.Create();
        }

        return SqlAssist.Insert(model);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <returns></returns>
    public ExecutiveResult<T> Add(T model)
    {
        var sql = AddCore(model);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();

            try
            {
                conn.ExecuteScalar(sql, model);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.ExecuteScalar(
                    sql,
                    model,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult<T>(ReturnMessageFactory.Ok)
        {
            Data = model
        };
    }

    private string AddUniquerCore(T model, ICollection<Condition<T>> uniquerConditions)
    {
        model = PreAdd(model);
        model = PreSave(model);
        model = CheckNullValue(model);

        if (model.Id <= 0)
        {
            model.Id = IdentifierAssist.Create();
        }

        return SqlAssist.InsertUniquer(model, uniquerConditions);
    }

    public ExecutiveResult<T> AddUniquer(T model, Condition<T> uniquerCondition)
    {
        return AddUniquer(
            model,
            new List<Condition<T>>
            {
                uniquerCondition
            }
        );
    }

    /// <summary>
    /// 唯一性Insert
    /// </summary>
    /// <param name="model"></param>
    /// <param name="uniquerConditions"></param>
    /// <returns></returns>
    public ExecutiveResult<T> AddUniquer(T model, ICollection<Condition<T>> uniquerConditions)
    {
        var sql = AddUniquerCore(model, uniquerConditions);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();

            try
            {
                var result = conn.Execute(sql, model);

                if (result <= 0)
                {
                    return new ExecutiveResult<T>(ReturnCode.NoChange.ToMessage("执行失败"));
                }

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var result = dbTransaction.Connection.Execute(
                    sql,
                    model,
                    dbTransaction
                );

                if (result <= 0)
                {
                    return new ExecutiveResult<T>(ReturnCode.NoChange.ToMessage("执行失败"));
                }

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult<T>(ReturnMessageFactory.Ok)
        {
            Data = model
        };
    }

    public ExecutiveResult<List<T>> Adds(params T[] models)
    {
        var list = new List<T>();

        list.AddRange(models);

        return Adds(list);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <returns></returns>
    public ExecutiveResult<List<T>> Adds(Collection<T> list)
    {
        return Adds(list.ToList());
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <returns></returns>
    public ExecutiveResult<List<T>> Adds(List<T> list)
    {
        foreach (var model in list)
        {
            var sql = AddCore(model);

            if (_mapperTransaction == null)
            {
                using var mapperTransaction = _mapperChannel.CreateMapperTransaction();
                try
                {
                    var dbTransaction = mapperTransaction.GetTransaction();

                    try
                    {
                        dbTransaction.Connection.ExecuteScalar(
                            sql,
                            model,
                            dbTransaction
                        );

                        LogSqlExecutionMessage();

                        mapperTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        e.Data.Add("query", sql);

                        if (MiniProfiler.Current != null)
                        {
                            e.Data.Add(
                                "sqlProfile",
                                new
                                {
                                    channel = _mapperChannel.GetChannel(),
                                    data = JsonConvert.DeserializeObject(
                                        MiniProfiler.Current.Head.CustomTimingsJson
                                    )
                                }
                            );
                        }

                        throw;
                    }
                }
                catch (Exception)
                {
                    mapperTransaction.Rollback();

                    throw;
                }
            }
            else
            {
                var dbTransaction = _mapperTransaction.GetTransaction();

                try
                {
                    dbTransaction.Connection.ExecuteScalar(
                        sql,
                        model,
                        dbTransaction
                    );

                    LogSqlExecutionMessage();
                }
                catch (Exception e)
                {
                    e.Data.Add("query", sql);

                    if (MiniProfiler.Current != null)
                    {
                        e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                    }

                    throw;
                }
            }
        }

        return new ExecutiveResult<List<T>>(ReturnMessageFactory.Ok)
        {
            Data = list
        };
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public ExecutiveResult<T> Update(T model)
    {
        model = PreUpdate(model);
        model = PreSave(model);
        model = CheckNullValue(model);

        var sql = SqlAssist.Update(model);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(sql, model);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    sql,
                    model,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult<T>(ReturnMessageFactory.Ok)
        {
            Data = model
        };
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public bool UpdateWithCondition(T data, ICollection<Condition<T>> conditions)
    {
        var query = SqlAssist.UpdateWithCondition(data, conditions);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(query, data);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    query,
                    data,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return true;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public ExecutiveResult<T> UpdateAssignField(T model, ICollection<AssignField<T>> listAssignField)
    {
        model = PreUpdate(model);
        model = PreSave(model);
        model = CheckNullValue(model);

        var sql = SqlAssist.UpdateAssignField(model, listAssignField);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(sql, model);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    sql,
                    model,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult<T>(ReturnMessageFactory.Ok)
        {
            Data = model
        };
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public ExecutiveResult<T> UpdatesSpecific(T model, ICollection<Expression<Func<T>>> listPropertyLambda)
    {
        model = PreUpdate(model);
        model = PreSave(model);
        model = CheckNullValue(model);

        var sql = SqlAssist.UpdateSpecific(model, listPropertyLambda);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(sql, model);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    sql,
                    model,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult<T>(ReturnMessageFactory.Ok)
        {
            Data = model
        };
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public ExecutiveResult<T> UpdatesSpecific(T model, ICollection<Expression<Func<T, object>>> listPropertyLambda)
    {
        model = PreUpdate(model);
        model = PreSave(model);
        model = CheckNullValue(model);

        var sql = SqlAssist.UpdateSpecific(model, listPropertyLambda);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(sql, model);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    sql,
                    model,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult<T>(ReturnMessageFactory.Ok)
        {
            Data = model
        };
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public bool UpdateSpecificWithCondition(
        T data,
        ICollection<Expression<Func<T>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    )
    {
        var query = SqlAssist.UpdateSpecificWithCondition(
            data,
            listPropertyLambda,
            conditions
        );

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(query, data);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    query,
                    data,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return true;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <returns></returns>
    public bool UpdateSpecificWithCondition(
        T data,
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    )
    {
        var query = SqlAssist.UpdateSpecificWithCondition(
            data,
            listPropertyLambda,
            conditions
        );

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(query, data);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    query,
                    data,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return true;
    }

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public ExecutiveResult<T> DeleteByPrimaryKey(long key)
    {
        var model = new T();

        model.SetPrimaryKeyValue(key);

        var sql = SqlAssist.Delete(model);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(sql, model);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    sql,
                    model,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult<T>(ReturnMessageFactory.Ok)
        {
            Data = model
        };
    }

    public ExecutiveResult<T> Delete(T model)
    {
        var sql = SqlAssist.Delete(model);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(sql, model);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    sql,
                    model,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult<T>(ReturnMessageFactory.Ok)
        {
            Data = model
        };
    }

    // public ExecutiveResult<T> Delete(T model, ICollection<Condition<T>> conditions)
    // {
    //     var sql = SqlAssist.Delete(model, conditions);
    //
    //     if (_mapperTransaction == null)
    //     {
    //         using (var conn = _mapperChannel.OpenConnection())
    //         {
    //             try
    //             {
    //                 conn.Execute(sql, model);
    //
    //                 LogCustomTimingRecord();
    //             }
    //             catch (Exception e)
    //             {
    //                 e.Data.Add("query", sql);
    //
    //                 if (MiniProfiler.Current != null)
    //                 {
    //                     e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
    //                 }
    //
    //                 throw;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         var dbTransaction = _mapperTransaction.GetTransaction();
    //
    //         try
    //         {
    //             dbTransaction.Connection.Execute(sql, model, dbTransaction);
    //
    //             LogCustomTimingRecord();
    //         }
    //         catch (Exception e)
    //         {
    //             e.Data.Add("query", sql);
    //
    //             if (MiniProfiler.Current != null)
    //             {
    //                 e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
    //             }
    //
    //             throw;
    //         }
    //     }
    //
    //     return new ExecutiveResult<T>(ReturnMessage.Ok)
    //     {
    //         Data = model
    //     };
    // }

    #endregion

    #region Delete Many

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public ExecutiveResult DeleteMany(IEnumerable<long> keys)
    {
        var sql = SqlAssist.DeleteBatch<T>(keys);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(sql);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    sql,
                    null,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult(ReturnMessageFactory.Ok);
    }

    public ExecutiveResult DeleteMany(IEnumerable<T> models)
    {
        var sql = SqlAssist.DeleteBatch(models);

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                conn.Execute(sql);

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                dbTransaction.Connection.Execute(
                    sql,
                    null,
                    dbTransaction
                );

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return new ExecutiveResult(ReturnMessageFactory.Ok);
    }

    #endregion

    #region SingleListObject

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<ExpandoObject> SingleListObject(
        ICollection<FieldItemSpecial<T>> fieldItems,
        Condition<T> condition,
        Sort<T> sort
    )
    {
        return SingleListObject(
            fieldItems,
            new List<Condition<T>>
            {
                condition
            },
            new List<Sort<T>>
            {
                sort
            }
        );
    }

    public IList<ExpandoObject> SingleListObject(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions,
        Sort<T> sort
    )
    {
        return SingleListObject(
            listPropertyLambda,
            conditions,
            new List<Sort<T>>
            {
                sort
            }
        );
    }

    public IList<ExpandoObject> SingleListObject(
        ICollection<FieldItemSpecial<T>> fieldItems,
        Condition<T> condition
    )
    {
        return SingleListObject(
            fieldItems,
            condition,
            new List<Sort<T>>()
        );
    }

    public IList<ExpandoObject> SingleListObject(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        Condition<T> condition,
        IList<Sort<T>> sorts
    )
    {
        return SingleListObject(
            listPropertyLambda,
            new List<Condition<T>>
            {
                condition
            },
            sorts
        );
    }

    public IList<ExpandoObject> SingleListObject(
        ICollection<FieldItemSpecial<T>> fieldItems,
        Condition<T> condition,
        IList<Sort<T>> sorts
    )
    {
        return SingleListObject(
            fieldItems,
            new List<Condition<T>>
            {
                condition
            },
            sorts
        );
    }

    public IList<ExpandoObject> SingleListObject(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    )
    {
        return SingleListObject(
            listPropertyLambda,
            conditions,
            new List<Sort<T>>()
        );
    }

    public IList<ExpandoObject> SingleListObject(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts
    )
    {
        var fieldItems = FieldItemSpecialFactory.BuildFieldItems(listPropertyLambda.ToArray());

        return SingleListObject(
            fieldItems,
            conditions,
            sorts
        );
    }

    public IList<ExpandoObject> SingleListObject(
        ICollection<FieldItemSpecial<T>> fieldItems,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts
    )
    {
        var query = SqlAssist.BuildListSql(
            fieldItems,
            conditions,
            sorts
        );

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();

            try
            {
                var reader = conn.ExecuteReader(query);

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                LogSqlExecutionMessage();

                return list;
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var reader = dbTransaction.Connection.ExecuteReader(
                    query,
                    null,
                    dbTransaction
                );

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                LogSqlExecutionMessage();

                return list;
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    #endregion

    #region SingleListEntity

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<T> SingleListEntity()
    {
        return SingleListEntity(new List<Condition<T>>(), new List<Sort<T>>());
    }

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<T> SingleListEntity(Sort<T> sort)
    {
        return SingleListEntity(new List<Condition<T>>(), sort);
    }

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<T> SingleListEntity(Condition<T> condition, Sort<T> sort)
    {
        return SingleListEntity(
            new List<Condition<T>>
            {
                condition
            },
            new List<Sort<T>>
            {
                sort
            }
        );
    }

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<T> SingleListEntity(ICollection<Condition<T>> conditions, Sort<T> sort)
    {
        return SingleListEntity(
            conditions,
            new List<Sort<T>>
            {
                sort
            }
        );
    }

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<T> SingleListEntity(Condition<T> condition)
    {
        return SingleListEntity(condition, new List<Sort<T>>());
    }

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<T> SingleListEntity(Condition<T> condition, IList<Sort<T>> sorts)
    {
        return SingleListEntity(
            new List<Condition<T>>
            {
                condition
            },
            sorts
        );
    }

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<T> SingleListEntity(ICollection<Condition<T>> conditions)
    {
        return SingleListEntity(conditions, new List<Sort<T>>());
    }

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<T> SingleListEntity(IList<Sort<T>> sorts)
    {
        return SingleListEntity(new List<Condition<T>>(), sorts);
    }

    /// <summary>
    /// 获取实例列表
    /// </summary>
    /// <returns></returns>
    public IList<T> SingleListEntity(ICollection<Condition<T>> conditions, IList<Sort<T>> sorts)
    {
        var model = new T();
        var query = new AdvanceSqlBuilder()
            .Select()
            .AllFields(model)
            .From(model);

        if (conditions.Count > 0)
        {
            query = query.AppendFragment(" WHERE ");

            foreach (var c in conditions)
            {
                if (c.ConditionType == ConditionType.In || c.ConditionType == ConditionType.NotIn)
                {
                    c.Value = (ICollection)c.Value;

                    query = query.LinkCondition(c);
                }
                else
                {
                    query = query.LinkCondition(c);
                }
            }
        }

        if (sorts is { Count: > 0 })
        {
            for (var i = 0; i < sorts.Count; i++)
            {
                var s = sorts[i];

                query = i == 0
                    ? query.OrderBy(s.Expression, s.SortType)
                    : query.AndOrderBy(s.Expression, s.SortType);
            }
        }

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();

            try
            {
                var list = conn.Query<T>(query.Sql).ToList();

                LogSqlExecutionMessage();

                return list;
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var list = dbTransaction.Connection.Query<T>(
                        query.Sql,
                        null,
                        dbTransaction
                    )
                    .ToList();

                LogSqlExecutionMessage();

                return list;
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    /// <summary>
    /// 获取实例分页列表
    /// </summary>
    /// <returns></returns>
    public PageListResult<T> PageListWithTotalSize(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>>? conditions = null,
        IList<Sort<T>>? sorts = null
    )
    {
        var total = TotalCount(conditions);

        var list = PageList(
            pageNo,
            pageSize,
            conditions,
            sorts
        );

        return new PageListResult<T>(ReturnCode.Ok)
        {
            List = list,
            PageIndex = pageNo,
            PageSize = pageSize,
            TotalSize = total
        };
    }

    /// <summary>
    /// 获取实例分页列表
    /// </summary>
    /// <param name="pageNo"></param>
    /// <param name="pageSize"></param>
    /// <param name="conditions"></param>
    /// <param name="sorts"></param>
    /// <returns></returns>
    public List<T> PageList(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>>? conditions = null,
        IList<Sort<T>>? sorts = null
    )
    {
        var query = SqlAssist.BuildPageListSql(
            pageNo,
            pageSize,
            conditions,
            sorts
        );

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                var list = conn.Query<T>(query).ToList();

                LogSqlExecutionMessage();

                return list;
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var list = dbTransaction.Connection.Query<T>(
                        query,
                        null,
                        dbTransaction
                    )
                    .ToList();

                LogSqlExecutionMessage();

                return list;
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    /// <summary>
    /// ListBySql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public IList<T> SingleListEntity(string sql)
    {
        IList<T> result;

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                result = conn.Query<T>(sql).ToList();

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                result = dbTransaction.Connection.Query<T>(
                        sql,
                        null,
                        dbTransaction
                    )
                    .ToList();

                LogSqlExecutionMessage();
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }

        return result;
    }

    #endregion List

    #region PageListObject

    /// <summary>
    /// 获取将实例转换为属性名首字母小写的Object的分页列表
    /// </summary>
    /// <returns></returns>
    public PageListResult<object> PageListObjectWithTotalSize(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts
    )
    {
        var pageListResult = PageListWithTotalSize(
            pageNo,
            pageSize,
            conditions,
            sorts
        );

        return new PageListResult<object>(pageListResult.Code)
        {
            List = pageListResult.List.ToListObject(),
            PageIndex = pageListResult.PageIndex,
            PageSize = pageListResult.PageSize,
            TotalSize = pageListResult.TotalSize
        };
    }

    /// <summary>
    /// 获取将实例转换为属性名首字母小写的Object的分页列表
    /// </summary>
    /// <returns></returns>
    public IList<object> PageListObject(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts
    )
    {
        return PageList(
                pageNo,
                pageSize,
                conditions,
                sorts
            )
            .ToListObject();
    }

    #endregion ListObject

    #region ListSimpleObject

    /// <summary>
    /// 获取将实例转换为属性名首字母小写的指定属性的Object的分页列表
    /// </summary>
    /// <returns></returns>
    public PageListResult<object> PageListSimpleObjectWithTotalSize(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts,
        ICollection<Expression<Func<object>>> expressions
    )
    {
        var pageListResult = PageListWithTotalSize(
            pageNo,
            pageSize,
            conditions,
            sorts
        );

        var list = pageListResult.List.ToListSimpleObject(expressions);

        return new PageListResult<object>(pageListResult.Code)
        {
            List = list,
            PageIndex = pageListResult.PageIndex,
            PageSize = pageListResult.PageSize,
            TotalSize = pageListResult.TotalSize
        };
    }

    /// <summary>
    /// 获取将实例转换为属性名首字母小写的指定属性的Object的分页列表
    /// </summary>
    /// <returns></returns>
    public IList<object> ListSimpleObject(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts,
        ICollection<Expression<Func<object>>> expressions
    )
    {
        var list = PageList(
            pageNo,
            pageSize,
            conditions,
            sorts
        );

        return list.ToListSimpleObject(expressions);
    }

    #endregion ListSimpleObject

    #region ListSimpleObjectIgnore

    /// <summary>
    /// 获取将实例转换为属性名首字母小写的忽略指定属性的Object的分页列表
    /// </summary>
    /// <returns></returns>
    public PageListResult<object> PageListSimpleObjectIgnoreWithTotalSize(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts,
        ICollection<Expression<Func<object>>> expressions
    )
    {
        var pageListResult = PageListWithTotalSize(
            pageNo,
            pageSize,
            conditions,
            sorts
        );

        var list = pageListResult.List.ToListSimpleObjectIgnore(expressions);

        return new PageListResult<object>(pageListResult.Code)
        {
            List = list,
            PageIndex = pageListResult.PageIndex,
            PageSize = pageListResult.PageSize,
            TotalSize = pageListResult.TotalSize
        };
    }

    /// <summary>
    /// 获取将实例转换为属性名首字母小写的忽略指定属性的Object的分页列表
    /// </summary>
    /// <returns></returns>
    public IList<object> PageListSimpleObjectIgnore(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts,
        ICollection<Expression<Func<object>>> expressions
    )
    {
        var list = PageList(
            pageNo,
            pageSize,
            conditions,
            sorts
        );

        return list.ToListSimpleObjectIgnore(expressions);
    }

    #endregion ListSimpleObjectIgnore

    /// <summary>
    /// 依据条件获取数据总量
    /// </summary>
    /// <returns></returns>
    public long TotalCount(ICollection<Condition<T>>? conditions = null)
    {
        var model = new T();
        var query = new StringBuilder();

        var builder = new AdvanceSqlBuilder(SqlAssist.BuildSelectCount()).From(model);

        builder.AppendFragment(" WHERE ");

        if (conditions != null)
        {
            foreach (var c in conditions)
            {
                if (c.ConditionType == ConditionType.In || c.ConditionType == ConditionType.NotIn)
                {
                    c.Value = (ICollection<object>)c.Value;

                    builder = builder.LinkCondition(c);
                }
                else
                {
                    builder = builder.LinkCondition(c);
                }
            }
        }

        // query.AppendLine(builder);

        var result = 0L;

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();

            try
            {
                var r = conn.ExecuteReader(query.ToString());

                if (r.Read())
                {
                    result = r[0].ConvertTo<long>();
                }

                LogSqlExecutionMessage();

                return result;
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var r = dbTransaction.Connection.ExecuteReader(
                    query.ToString(),
                    null,
                    dbTransaction
                );

                LogSqlExecutionMessage();

                if (r.Read())
                {
                    result = r[0].ConvertTo<long>();
                }

                return result;
            }
            catch (Exception e)
            {
                e.Data.Add("query", query);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    /// <summary>
    /// ListBySql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public IList<T> ListBySql(string sql)
    {
        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                var list = conn.Query<T>(sql).ToList();

                LogSqlExecutionMessage();

                return list;
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var list = dbTransaction.Connection.Query<T>(sql).ToList();

                LogSqlExecutionMessage();

                return list;
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    /// <summary>
    /// ListBySql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public IList<dynamic> ListCustom(string sql)
    {
        IList<dynamic> result = new List<dynamic>();

        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                var cmd = conn.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                var reader = cmd.ExecuteReader();

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                foreach (var item in list)
                {
                    result.Add(item);
                }
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }

            return result;
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                if (dbTransaction.Connection == null)
                {
                    throw new Exception("dbTransaction.Connection is null");
                }

                var cmd = dbTransaction.Connection.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Transaction = dbTransaction;

                var reader = cmd.ExecuteReader();

                LogSqlExecutionMessage();

                var list = ConvertAssist.DataReaderToExpandoObjectList(reader);

                foreach (var item in list)
                {
                    result.Add(item);
                }

                return result;
            }
            catch (Exception e)
            {
                e.Data.Add("query", sql);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    #region StoredProcedure

    /// <summary>
    /// 执行存储过程
    /// </summary>
    public IList<T> ExecuteToListEntity(string procName, params object[] ps)
    {
        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                var r = conn.ExecuteReader(
                    procName,
                    ps,
                    null,
                    null,
                    CommandType.StoredProcedure
                );

                LogSqlExecutionMessage();

                var dt = ConvertAssist.DataReaderToDataTable(r);

                return ConvertAssist.DataTableToModel<T>(dt, false);
            }
            catch (Exception e)
            {
                e.Data.Add("procName", procName);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            using var r = dbTransaction.Connection.ExecuteReader(
                procName,
                ps,
                dbTransaction,
                null,
                CommandType.StoredProcedure
            );
            try
            {
                LogSqlExecutionMessage();

                var dt = ConvertAssist.DataReaderToDataTable(r);

                return ConvertAssist.DataTableToModel<T>(dt, false);
            }
            catch (Exception e)
            {
                e.Data.Add("procName", procName);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    /// <summary>
    /// 执行存储过程
    /// </summary>
    public IList<ExpandoObject> ExecuteToListObject(string procName, params object[] ps)
    {
        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();

            using var r = conn.ExecuteReader(
                procName,
                ps,
                null,
                null,
                CommandType.StoredProcedure
            );

            try
            {
                LogSqlExecutionMessage();

                return ConvertAssist.DataReaderToExpandoObjectList(r);
            }
            catch (Exception e)
            {
                e.Data.Add("procName", procName);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var r = dbTransaction.Connection.ExecuteReader(
                    procName,
                    ps,
                    dbTransaction,
                    null,
                    CommandType.StoredProcedure
                );

                LogSqlExecutionMessage();

                return ConvertAssist.DataReaderToExpandoObjectList(r);
            }
            catch (Exception e)
            {
                e.Data.Add("procName", procName);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    /// <summary>
    /// 执行存储过程
    /// </summary>
    public object Execute(string procName, params object[] ps)
    {
        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                var result = conn.Execute(
                    procName,
                    ps,
                    null,
                    null,
                    CommandType.StoredProcedure
                );

                LogSqlExecutionMessage();

                return result;
            }
            catch (Exception e)
            {
                e.Data.Add("procName", procName);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var result = dbTransaction.Connection.Execute(
                    procName,
                    ps,
                    dbTransaction,
                    null,
                    CommandType.StoredProcedure
                );

                LogSqlExecutionMessage();

                return result;
            }
            catch (Exception e)
            {
                e.Data.Add("procName", procName);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    /// <summary>
    /// 执行存储过程
    /// </summary>
    public object ExecuteToValue(string procName, params object[] ps)
    {
        if (_mapperTransaction == null)
        {
            using var conn = _mapperChannel.OpenConnection();
            try
            {
                var result = conn.ExecuteScalar(
                    procName,
                    ps,
                    null,
                    null,
                    CommandType.StoredProcedure
                );

                LogSqlExecutionMessage();

                return result;
            }
            catch (Exception e)
            {
                e.Data.Add("procName", procName);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
        else
        {
            var dbTransaction = _mapperTransaction.GetTransaction();

            try
            {
                var result = dbTransaction.Connection.ExecuteScalar(
                    procName,
                    ps,
                    dbTransaction,
                    null,
                    CommandType.StoredProcedure
                );

                LogSqlExecutionMessage();

                return result;
            }
            catch (Exception e)
            {
                e.Data.Add("procName", procName);

                if (MiniProfiler.Current != null)
                {
                    e.Data.Add("sqlProfile", BuildErrorDataValue(MiniProfiler.Current));
                }

                throw;
            }
        }
    }

    #endregion StoredProcedure

    #endregion Get

    protected virtual T PreAdd(T model)
    {
        return model;
    }

    protected virtual T PreUpdate(T model)
    {
        return model;
    }

    protected virtual T PreSave(T model)
    {
        return model;
    }

    private T CheckNullValue(T model)
    {
        var t = model.GetType();
        var plist = t.GetProperties();

        foreach (var p in plist)
        {
            if (!p.CanWrite)
            {
                continue;
            }

            if (p.PropertyType.Name != nameof(DateTime) &&
                p.PropertyType.Name != typeof(DateTime?).Name)
            {
                continue;
            }

            var v = p.GetValue(model);
            var needSetValue = false;

            if (v == null)
            {
                v = ConstCollection.DbDefaultDateTime;
                needSetValue = true;
            }
            else if (v.Equals(ConstCollection.NetDefaultDateTime))
            {
                v = ConstCollection.DbDefaultDateTime;
                needSetValue = true;
            }

            if (needSetValue)
            {
                p.SetValue(model, v);
            }
        }

        return model;
    }

    #endregion Method

    public IMapperChannel GetMapperChannel()
    {
        return _mapperChannel;
    }

    public IMapperTransaction GetDbTransaction()
    {
        if (_mapperTransaction == null)
        {
            throw new Exception("IMapperTransaction is null");
        }

        return _mapperTransaction;
    }

    private object BuildErrorDataValue(MiniProfiler? miniProfiler)
    {
        if (miniProfiler != null)
        {
            return JsonConvert.SerializeObject(
                new
                {
                    channel = _mapperChannel.GetChannel(),
                    data = JsonConvert.DeserializeObject(MiniProfiler.Current.Head.CustomTimingsJson)
                }
            );
        }

        return "";
    }

    /// <summary>
    /// 记录执行的sql
    /// </summary>
    private void LogSqlExecutionMessage()
    {
        if (!_sqlLogRecordSwitch)
        {
            return;
        }

        if (MiniProfiler.Current == null)
        {
            return;
        }

        if (!SqlLogSwitchAssist.GetCurrentSwitch())
        {
            return;
        }

        var listCustomTiming = new List<SqlLogMessage>();

        var dictionary = MiniProfiler.Current.Head.CustomTimings;

        foreach (var item in dictionary)
        {
            listCustomTiming.AddRange(
                item.Value.Select(
                    one => new SqlLogMessage
                    {
                        Flag = one.Id.ToString(),
                        CommandString = one.CommandString,
                        ExecuteType = one.ExecuteType,
                        StackTraceSnippet = one.StackTraceSnippet,
                        StartMilliseconds = one.StartMilliseconds,
                        DurationMilliseconds = one.DurationMilliseconds ?? 0m,
                        FirstFetchDurationMilliseconds = one.FirstFetchDurationMilliseconds ?? 0m,
                        Errored = one.Errored ? 1 : 0,
                        TriggerChannel = ChannelAssist.GetCurrentChannel().ToValue(),
                        CollectMode = CollectMode.MiniProfiler.ToInt(),
                        DatabaseChannel = GetMapperChannel().GetChannel()
                    }
                )
            );
        }

        if (listCustomTiming.Count <= 0)
        {
            return;
        }

        foreach (var item in listCustomTiming)
        {
            LogSqlExecutionMessage(item);
        }
    }

    /// <summary>
    /// 记录执行的sql
    /// </summary>
    /// <param name="sqlExecutionMessage">sql</param>
    private void LogSqlExecutionMessage(ISqlLog sqlExecutionMessage)
    {
        if (!_sqlLogRecordSwitch)
        {
            return;
        }

        AutofacAssist.Instance.Resolve<ISqlLogProducer>()
            .SendAsync(
                sqlExecutionMessage.CommandString,
                sqlExecutionMessage.ExecuteType,
                sqlExecutionMessage.StackTraceSnippet,
                sqlExecutionMessage.StartMilliseconds,
                sqlExecutionMessage.DurationMilliseconds,
                sqlExecutionMessage.FirstFetchDurationMilliseconds,
                sqlExecutionMessage.Errored,
                sqlExecutionMessage.CollectMode,
                sqlExecutionMessage.DatabaseChannel
            );
    }
}