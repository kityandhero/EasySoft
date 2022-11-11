using EasySoft.Core.Dapper.Elegant.Configure;

namespace EasySoft.Core.Dapper.Elegant.Assists;

public static class EntityAssist
{
    #region EntityCache

    private static TimeSpan EntityCacheExpireTime => new(
        TimeSpan.TicksPerSecond * 12 * RandomEx.ThreadSafeNext(36000, 72000)
    );

    private static string GetEntityCacheKey<T>(long id) where T : IEntityExtraSelf<T>, new()
    {
        return DapperElegantConfigurator.GetCacheOperator()?.BuildKey(
            "entityCache",
            typeof(T).Name.ToLowerFirst(),
            "key",
            id.ToString()
        ) ?? "";
    }

    public static T? RefreshCache<T>(long id) where T : IEntityExtraSelf<T>, new()
    {
        RemoveEntityCache<T>(id);

        return GetEntity<T>(id);
    }

    /// <summary>
    /// GetEntityCache
    /// </summary>
    /// <returns></returns>
    public static T? GetEntity<T>(long id, bool enableCache = true) where T : IEntityExtraSelf<T>, new()
    {
        if (id <= 0) return default;

        if (!enableCache) return GetEntityCore<T>(id);

        var cacheOperator = DapperElegantConfigurator.GetCacheOperator();

        if (cacheOperator == null) return GetEntityCore<T>(id);

        var key = GetEntityCacheKey<T>(id);

        if (string.IsNullOrWhiteSpace(key)) return GetEntityCore<T>(id);

        var result = cacheOperator.Get<T>(key);

        if (result.Success) return result.Data;

        var data = GetEntityCore<T>(id);

        if (data != null) cacheOperator.Set(key, data, EntityCacheExpireTime);

        return data;
    }

    private static T? GetEntityCore<T>(long id) where T : IEntityExtraSelf<T>, new()
    {
        if (id <= 0) return default;

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.Get(id);
    }

    /// <summary>
    /// 移除缓存
    /// </summary>
    public static ExecutiveResult RemoveEntityCache<T>(long id) where T : IEntityExtraSelf<T>, new()
    {
        var result = new ExecutiveResult(ReturnCode.NoChange);

        if (id <= 0) return result;

        var cacheOperator = DapperElegantConfigurator.GetCacheOperator();

        if (cacheOperator == null) return result;

        var key = GetEntityCacheKey<T>(id);

        cacheOperator.Remove(key);

        return new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion EntityCache

    public static ExecutiveResult RemoveEntityCache<T>(T entity) where T : IEntityExtraSelf<T>, new()
    {
        var result = new ExecutiveResult(ReturnCode.NoChange);

        if (entity.GetPrimaryKeyValue().ConvertTo<long>() <= 0) return result;

        var id = Convert.ToInt64(entity.GetPrimaryKeyValue());

        result = RemoveEntityCache<T>(id);

        return result;
    }

    #region CheckExist

    public static bool CheckExist<T>(long id, bool enableCache = true) where T : IEntityExtraSelf<T>, new()
    {
        var entity = GetEntity<T>(id, enableCache);

        return entity != null;
    }

    public static bool CheckExist<T>(Condition<T> condition) where T : IEntityExtraSelf<T>, new()
    {
        var entity = GetEntity(condition);

        return entity != null;
    }

    public static bool CheckExist<T>(ICollection<Condition<T>> conditions)
        where T : IEntityExtraSelf<T>, new()
    {
        var entity = GetEntity(conditions);

        return entity != null;
    }

    #endregion

    #region SingleListObject

    public static IList<ExpandoObject> SingleListEntity<T>(
        ICollection<FieldItemSpecial<T>> fieldItems, Condition<T> condition,
        Sort<T> sort
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListObject(
            fieldItems,
            condition,
            sort
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions, Sort<T> sort
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListObject(
            listPropertyLambda,
            conditions,
            sort
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<FieldItemSpecial<T>> fieldItems, Condition<T> condition
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListObject(
            fieldItems,
            condition
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<Expression<Func<T, object>>> listPropertyLambda, Condition<T> condition,
        IList<Sort<T>> sorts
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListObject(
            listPropertyLambda,
            condition,
            sorts
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<FieldItemSpecial<T>> fieldItems, Condition<T> condition,
        IList<Sort<T>> sorts
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListObject(
            fieldItems,
            condition,
            sorts
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListObject(
            listPropertyLambda,
            conditions
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListObject(
            listPropertyLambda,
            conditions,
            sorts
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<FieldItemSpecial<T>> fieldItems,
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListObject(
            fieldItems,
            conditions,
            sorts
        );

        return result;
    }

    #endregion

    #region SingleListEntity

    public static IList<T> SingleListEntity<T>(Sort<T> sort) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListEntity(sort);

        return result;
    }

    public static IList<T> SingleListEntity<T>(ICollection<object> listId) where T : IEntityExtraSelf<T>, new()
    {
        if (!listId.Any()) return new List<T>();

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        IList<T> result = new List<T>();

        if (mapper.GetUseLogSqlExecutionMessage())
        {
            listId.ForEach(o =>
            {
                var data = mapper.Get(o);

                if (data != null) result.Add(data);
            });
        }
        else
        {
            var condition = new Condition<T>
            {
                ConditionType = ConditionType.In,
                Expression = new T().GetPrimaryKeyLambda(),
                Value = listId
            };

            result = SingleListEntity(condition);
        }

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        Condition<T> condition,
        Sort<T> sort
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListEntity(condition, sort);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        ICollection<Condition<T>> conditions,
        Sort<T> sort
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListEntity(conditions, sort);

        return result;
    }

    public static IList<T> SingleListEntity<T>(Condition<T> condition)
        where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListEntity(condition);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        Condition<T> condition,
        IList<Sort<T>> sorts
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListEntity(condition, sorts);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        ICollection<Condition<T>> conditions
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListEntity(conditions);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        IList<Sort<T>> sorts
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListEntity(sorts);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.SingleListEntity(conditions, sorts);

        return result;
    }

    #endregion

    public static PageListResult<T> PageListEntityWithTotalSize<T>(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>>? conditions = null,
        IList<Sort<T>>? sorts = null
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var conditionsAdjust = conditions ?? new List<Condition<T>>();
        var sortsAdjust = sorts ?? new List<Sort<T>>();

        var result = mapper.PageListWithTotalSize(pageNo, pageSize, conditionsAdjust, sortsAdjust);

        return result;
    }

    public static List<T> PageListEntity<T>(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>>? conditions = null,
        IList<Sort<T>>? sorts = null
    ) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var conditionsAdjust = conditions ?? new List<Condition<T>>();
        var sortsAdjust = sorts ?? new List<Sort<T>>();

        var result = mapper.PageList(pageNo, pageSize, conditionsAdjust, sortsAdjust);

        return result;
    }

    public static long GetTotalCount<T>() where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.TotalCount();

        return result;
    }

    public static T? GetEntity<T>(Condition<T> condition) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.GetBy(condition);

        return result;
    }

    public static T? GetEntity<T>(ICollection<Condition<T>> conditions)
        where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.GetBy(conditions);

        return result;
    }

    public static T? GetEntity<T>(ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts)
        where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.GetBy(conditions, sorts);

        return result;
    }

    #region AddEntity

    public static ExecutiveResult<T> Add<T>(T entity) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.Add(entity);
    }

    public static ExecutiveResult<T> AddUniquer<T>(T entity, Condition<T> uniquerCondition)
        where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.AddUniquer(entity, uniquerCondition);
    }

    public static ExecutiveResult<T> AddUniquer<T>(
        T entity,
        ICollection<Condition<T>> uniquerConditions
    ) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.AddUniquer(entity, uniquerConditions);
    }

    #endregion

    #region UpdateEntity

    public static ExecutiveResult<T> Update<T>(T entity) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0) throw new Exception("执行对象主键数据错误");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.Update(entity);
    }

    public static bool UpdateWithCondition<T>(
        T entity,
        ICollection<Condition<T>> conditions
    ) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0) throw new Exception("执行对象主键数据错误");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.UpdateWithCondition(entity, conditions);
    }

    public static ExecutiveResult<T> UpdatesSpecific<T>(
        T entity,
        ICollection<Expression<Func<T>>> listPropertyLambda
    ) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0) throw new Exception("执行对象主键数据错误");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.UpdatesSpecific(entity, listPropertyLambda);
    }

    public static ExecutiveResult<T> UpdatesSpecific<T>(
        T entity,
        ICollection<Expression<Func<T, object>>> listPropertyLambda
    ) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0) throw new Exception("执行对象主键数据错误");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.UpdatesSpecific(entity, listPropertyLambda);
    }

    public static bool UpdateSpecificWithCondition<T>(
        T entity,
        ICollection<Expression<Func<T>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    ) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0) throw new Exception("执行对象主键数据错误");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.UpdateSpecificWithCondition(entity, listPropertyLambda, conditions);
    }

    public static bool UpdateSpecificWithCondition<T>(
        T entity,
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    ) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0) throw new Exception("执行对象主键数据错误");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        return mapper.UpdateSpecificWithCondition(entity, listPropertyLambda, conditions);
    }

    #endregion

    #region SetEntity

    public static ExecutiveResult<T> Set<T>(T entity) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("执行对象不能为null");

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) == 0) return Add(entity);

        return Update(entity);
    }

    #endregion

    #region DeleteEntity

    public static ExecutiveResult<T> DeleteEntity<T>(T entity) where T : IEntityExtraSelf<T>, new()
    {
        if (entity == null) throw new Exception("can not delete null data");

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.Delete(entity);

        return result;
    }

    public static ExecutiveResult<T> DeleteEntity<T>(long id) where T : IEntityExtraSelf<T>, new()
    {
        var entity = GetEntity<T>(id, false);

        if (entity == null) return new ExecutiveResult<T>(ReturnCode.NoData.ToMessage());

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.Delete(entity);

        return result;
    }

    /// <summary>
    /// 为安全起见,条件删除仅删除第一个满足检索条件的数据，批量删除请使用其他方式
    /// </summary>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ExecutiveResult<T> DeleteEntity<T>(
        Condition<T> condition
    ) where T : IEntityExtraSelf<T>, new()
    {
        var entity = GetEntity(condition);

        if (entity == null) return new ExecutiveResult<T>(ReturnCode.NoData);

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.Delete(entity);

        return result;
    }

    /// <summary>
    /// 为安全起见,条件删除仅删除第一个满足检索条件的数据，批量删除请使用其他方式
    /// </summary>
    /// <param name="conditions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ExecutiveResult<T> DeleteEntity<T>(
        ICollection<Condition<T>> conditions
    ) where T : IEntityExtraSelf<T>, new()
    {
        var entity = GetEntity(conditions);

        if (entity == null) return new ExecutiveResult<T>(ReturnCode.NoData);

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.Delete(entity);

        return result;
    }

    #endregion

    #region DeleteMany

    public static ExecutiveResult DeleteMany<T>(IEnumerable<long> keys) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.DeleteMany(keys);

        return result;
    }

    public static ExecutiveResult DeleteMany<T>(IEnumerable<T> models) where T : IEntityExtraSelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            DapperElegantConfigurator.GetSqlLogRecordJudge()
        );

        var result = mapper.DeleteMany(models);

        return result;
    }

    #endregion
}