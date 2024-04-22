using EasySoft.Core.Dapper.Elegant.Configure;

namespace EasySoft.Core.Dapper.Elegant.Assists;

public static class EntityAssist
{
    #region EntityCache

    private static string GetEntityCacheKey<T>(long id) where T : IEntitySelf<T>, new()
    {
        return DapperElegantConfigurator.GetCacheOperator()
            ?.BuildKey(
                "entityCache",
                typeof(T).Name.ToLowerFirst(),
                "key",
                id.ToString()
            ) ?? "";
    }

    public static T? RefreshCache<T>(
        long id,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        RemoveEntityCache<T>(id);

        return GetEntity<T>(id, logSql);
    }

    /// <summary>
    /// GetEntityCache 
    /// </summary>
    /// <returns></returns>
    public static T? GetEntity<T>(
        long id,
        bool allowFromCache = true,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        return GetEntity<T>(
            id,
            out _,
            allowFromCache,
            logSql
        );
    }

    /// <summary>
    /// GetEntityCache
    /// </summary>
    /// <returns></returns>
    public static T? GetEntity<T>(
        long id,
        out CacheKeyValue<T?>? cacheValue,
        bool allowFromCache = true,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        cacheValue = null;

        if (id <= 0)
        {
            return default;
        }

        var cacheOperator = DapperElegantConfigurator.GetCacheOperator();

        if (cacheOperator == null)
        {
            throw new UnhandledException("cacheOperator is null");
        }

        cacheValue = cacheOperator.TryGetWithSteadyStorage(
            id,
            logSql,
            (o, _) => GetEntityCacheKey<T>(o),
            GetEntityCore<T>,
            !allowFromCache
        );

        return cacheValue.Value;
    }

    private static T? GetEntityCore<T>(
        long id,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (id <= 0)
        {
            return default;
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.Get(id);
    }

    /// <summary>
    /// 移除缓存
    /// </summary>
    public static ExecutiveResult RemoveEntityCache<T>(long id) where T : IEntitySelf<T>, new()
    {
        var result = new ExecutiveResult(ReturnCode.NoChange);

        if (id <= 0)
        {
            return result;
        }

        var cacheOperator = DapperElegantConfigurator.GetCacheOperator();

        if (cacheOperator == null)
        {
            return result;
        }

        var key = GetEntityCacheKey<T>(id);

        cacheOperator.Remove(key);

        return new ExecutiveResult(ReturnCode.Ok);
    }

    public static ExecutiveResult RemoveEntityCache<T>(T entity) where T : IEntitySelf<T>, new()
    {
        var result = new ExecutiveResult(ReturnCode.NoChange);

        if (entity.GetPrimaryKeyValue().ConvertTo<long>() <= 0)
        {
            return result;
        }

        var id = Convert.ToInt64(entity.GetPrimaryKeyValue());

        result = RemoveEntityCache<T>(id);

        return result;
    }

    #endregion EntityCache

    #region CheckExist

    public static bool CheckExist<T>(
        long id,
        bool enableCache = true,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var entity = GetEntity<T>(
            id,
            enableCache,
            logSql
        );

        return entity != null;
    }

    public static bool CheckExist<T>(
        Condition<T> condition,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var entity = GetEntity(
            condition,
            logSql
        );

        return entity != null;
    }

    public static bool CheckExist<T>(
        ICollection<Condition<T>> conditions,
        bool logSql = true
    )
        where T : IEntitySelf<T>, new()
    {
        var entity = GetEntity(
            conditions,
            logSql
        );

        return entity != null;
    }

    #endregion

    #region SingleListObject

    public static IList<ExpandoObject> SingleListEntity<T>(
        ICollection<FieldItemSpecial<T>> fieldItems,
        Condition<T> condition,
        Sort<T> sort,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
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
        ICollection<Condition<T>> conditions,
        Sort<T> sort,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListObject(
            listPropertyLambda,
            conditions,
            sort
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<FieldItemSpecial<T>> fieldItems,
        Condition<T> condition,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListObject(
            fieldItems,
            condition
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        Condition<T> condition,
        IList<Sort<T>> sorts,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListObject(
            listPropertyLambda,
            condition,
            sorts
        );

        return result;
    }

    public static IList<ExpandoObject> SingleListObject<T>(
        ICollection<FieldItemSpecial<T>> fieldItems,
        Condition<T> condition,
        IList<Sort<T>> sorts,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
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
        ICollection<Condition<T>> conditions,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
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
        IList<Sort<T>> sorts,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
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
        IList<Sort<T>> sorts,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
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

    public static IList<T> SingleListEntity<T>(
        Sort<T> sort,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListEntity(sort);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        ICollection<object> listId,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (!listId.Any())
        {
            return new List<T>();
        }

        var condition = new Condition<T>
        {
            ConditionType = ConditionType.In,
            Expression = new T().GetPrimaryKeyLambda(),
            Value = listId
        };

        return SingleListEntity(condition, logSql);
    }

    public static IList<T> SingleListEntity<T>(
        Condition<T> condition,
        Sort<T> sort,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListEntity(condition, sort);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        ICollection<Condition<T>> conditions,
        Sort<T> sort,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListEntity(conditions, sort);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        Condition<T> condition,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListEntity(condition);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        Condition<T> condition,
        IList<Sort<T>> sorts,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListEntity(condition, sorts);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        ICollection<Condition<T>> conditions,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListEntity(conditions);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        IList<Sort<T>> sorts,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListEntity(sorts);

        return result;
    }

    public static IList<T> SingleListEntity<T>(
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.SingleListEntity(conditions, sorts);

        return result;
    }

    #endregion

    public static PageListResult<T> PageListEntityWithTotalSize<T>(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>>? conditions = null,
        IList<Sort<T>>? sorts = null,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var conditionsAdjust = conditions ?? new List<Condition<T>>();
        var sortsAdjust = sorts ?? new List<Sort<T>>();

        var result = mapper.PageListWithTotalSize(
            pageNo,
            pageSize,
            conditionsAdjust,
            sortsAdjust
        );

        return result;
    }

    public static List<T> PageListEntity<T>(
        int pageNo,
        int pageSize,
        ICollection<Condition<T>>? conditions = null,
        IList<Sort<T>>? sorts = null,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var conditionsAdjust = conditions ?? new List<Condition<T>>();
        var sortsAdjust = sorts ?? new List<Sort<T>>();

        var result = mapper.PageList(
            pageNo,
            pageSize,
            conditionsAdjust,
            sortsAdjust
        );

        return result;
    }

    public static long GetTotalCount<T>(
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.TotalCount();

        return result;
    }

    public static T? GetEntity<T>(
        Condition<T> condition,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.GetBy(condition);

        return result;
    }

    public static T? GetEntity<T>(
        ICollection<Condition<T>> conditions,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.GetBy(conditions);

        return result;
    }

    public static T? GetEntity<T>(
        ICollection<Condition<T>> conditions,
        IList<Sort<T>> sorts,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.GetBy(conditions, sorts);

        return result;
    }

    #region AddEntity

    public static ExecutiveResult<T> Add<T>(
        T entity,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.Add(entity);
    }

    public static ExecutiveResult<T> AddUniquer<T>(
        T entity,
        Condition<T> uniquerCondition,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.AddUniquer(entity, uniquerCondition);
    }

    public static ExecutiveResult<T> AddUniquer<T>(
        T entity,
        ICollection<Condition<T>> uniquerConditions,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.AddUniquer(entity, uniquerConditions);
    }

    #endregion

    #region UpdateEntity

    public static ExecutiveResult<T> Update<T>(
        T entity,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0)
        {
            throw new Exception("执行对象主键数据错误");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.Update(entity);
    }

    public static bool UpdateWithCondition<T>(
        T entity,
        ICollection<Condition<T>> conditions,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0)
        {
            throw new Exception("执行对象主键数据错误");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.UpdateWithCondition(entity, conditions);
    }

    public static ExecutiveResult<T> UpdatesSpecific<T>(
        T entity,
        ICollection<Expression<Func<T>>> listPropertyLambda,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0)
        {
            throw new Exception("执行对象主键数据错误");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.UpdatesSpecific(entity, listPropertyLambda);
    }

    public static ExecutiveResult<T> UpdatesSpecific<T>(
        T entity,
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0)
        {
            throw new Exception("执行对象主键数据错误");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.UpdatesSpecific(entity, listPropertyLambda);
    }

    public static bool UpdateSpecificWithCondition<T>(
        T entity,
        ICollection<Expression<Func<T>>> listPropertyLambda,
        ICollection<Condition<T>> conditions,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0)
        {
            throw new Exception("执行对象主键数据错误");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.UpdateSpecificWithCondition(
            entity,
            listPropertyLambda,
            conditions
        );
    }

    public static bool UpdateSpecificWithCondition<T>(
        T entity,
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) <= 0)
        {
            throw new Exception("执行对象主键数据错误");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        return mapper.UpdateSpecificWithCondition(
            entity,
            listPropertyLambda,
            conditions
        );
    }

    #endregion

    #region SetEntity

    public static ExecutiveResult<T> Set<T>(
        T entity,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("执行对象不能为null");
        }

        if (Convert.ToInt64(entity.GetPrimaryKeyValue()) == 0)
        {
            return Add(entity, logSql);
        }

        return Update(entity, logSql);
    }

    #endregion

    #region DeleteEntity

    public static ExecutiveResult<T> DeleteEntity<T>(
        T entity,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        if (entity == null)
        {
            throw new Exception("can not delete null data");
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.Delete(entity);

        return result;
    }

    public static ExecutiveResult<T> DeleteEntity<T>(
        long id,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var entity = GetEntity<T>(id, false);

        if (entity == null)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData.ToMessage());
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.Delete(entity);

        return result;
    }

    /// <summary>
    /// 为安全起见,条件删除仅删除第一个满足检索条件的数据，批量删除请使用其他方式
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="logSql"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ExecutiveResult<T> DeleteEntity<T>(
        Condition<T> condition,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var entity = GetEntity(condition);

        if (entity == null)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.Delete(entity);

        return result;
    }

    /// <summary>
    /// 为安全起见,条件删除仅删除第一个满足检索条件的数据，批量删除请使用其他方式
    /// </summary>
    /// <param name="conditions"></param>
    /// <param name="logSql"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ExecutiveResult<T> DeleteEntity<T>(
        ICollection<Condition<T>> conditions,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var entity = GetEntity(conditions);

        if (entity == null)
        {
            return new ExecutiveResult<T>(ReturnCode.NoData);
        }

        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.Delete(entity);

        return result;
    }

    #endregion

    #region DeleteMany

    public static ExecutiveResult DeleteMany<T>(
        IEnumerable<long> keys,
        Action? callback = null,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.DeleteMany(keys);

        callback?.Invoke();

        return result;
    }

    public static ExecutiveResult DeleteMany<T>(
        IEnumerable<T> models,
        Action? callback = null,
        bool logSql = true
    ) where T : IEntitySelf<T>, new()
    {
        var mapper = new BaseMapper<T>(
            MapperChannelFactory.GetMainMapperChannel(),
            logSql
        );

        var result = mapper.DeleteMany(models);

        callback?.Invoke();

        return result;
    }

    #endregion
}