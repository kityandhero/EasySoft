using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.Data.Repositories;

/// <summary>
/// IRepository
/// </summary>
public interface IRepository
{
}

/// <inheritdoc />
public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : class, IEntity<long>
{
}

/// <summary>  
///     Repository标记接口
/// </summary>
public interface IRepository<TEntity, in TKey> : IRepository where TEntity : class, IEntity<TKey>
{
    #region Read

    #region PageList

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="where"></param>
    /// <param name="isAsc"></param>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>    
    Task<PageListResult<TEntity>> PageListAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>>? where = null,
        bool isAsc = true,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="where"></param>
    /// <param name="order"></param>
    /// <param name="isAsc"></param>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TTarget"></typeparam>
    /// <returns></returns>
    Task<PageListResult<TEntity>> PageListAsync<TTarget>(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>>? where = null,
        Expression<Func<TEntity, TTarget>>? order = null,
        bool isAsc = true,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region SingleList

    /// <summary>
    /// 单页列表
    /// </summary>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> SingleListAsync(
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 单页列表
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> SingleListAsync(
        Expression<Func<TEntity, bool>> filter,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 单页列表
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="keySelector"></param>
    /// <param name="descending"></param>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TTarget"></typeparam>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> SingleListAsync<TTarget>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TTarget> keySelector,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 单页列表
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <param name="descending"></param>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TTarget"></typeparam>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> SingleListAsync<TTarget>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TTarget> keySelector,
        IComparer<TTarget>? comparer,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region Get

    /// <summary>
    /// 获取单项
    /// </summary>
    /// <param name="id"></param>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ExecutiveResult<TEntity>> GetAsync(
        TKey id,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 获取单项
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ExecutiveResult<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 获取单项
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="keySelector"></param>
    /// <param name="descending"></param>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TTarget"></typeparam>
    /// <returns></returns>
    Task<ExecutiveResult<TEntity>> GetAsync<TTarget>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TTarget> keySelector,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 获取单项
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <param name="descending"></param>
    /// <param name="writeChannel"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TTarget"></typeparam>
    /// <returns></returns>
    Task<ExecutiveResult<TEntity>> GetAsync<TTarget>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TTarget> keySelector,
        IComparer<TTarget>? comparer,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region Exists

    /// <summary>
    /// 根据条件查询实体是否存在
    /// </summary>
    /// <param name="where">查询条件</param>
    /// <param name="writeChannel">是否读写库，默认false,可选参数</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task<ExecutiveResult> ExistAsync(
        Expression<Func<TEntity, bool>> where,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region CountAsync

    /// <summary>
    /// 统计符合条件的实体数量
    /// </summary>
    /// <param name="where">查询条件</param>
    /// <param name="writeChannel">是否读写库，默认false,可选参数</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> where,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region Where

    /// <summary>
    /// 根据条件查询，返回IQueryable 
    /// </summary>
    /// <param name="expression">查询条件</param>
    /// <param name="writeDb">是否读写库，默认false,可选参数</param>
    /// <param name="noTracking">是否开启跟踪，默认false,可选参数</param>
    /// <returns></returns>
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, bool writeDb = false, bool noTracking = true);

    #endregion

    #endregion

    #region Add

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ExecutiveResult<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 添加多项
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ExecutiveResult> AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region Update

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ExecutiveResult<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 更新多项
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ExecutiveResult> DeleteAsync(
        object id,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ExecutiveResult<TEntity>> DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idCollection"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ExecutiveResult> BatchDeleteAsync(
        IEnumerable<object> idCollection,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ExecutiveResult> BatchDeleteAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    );

    #endregion
}