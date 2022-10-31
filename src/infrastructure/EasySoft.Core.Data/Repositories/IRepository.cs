using System.Linq.Expressions;
using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.Data.Repositories;

public interface IRepository
{
}

public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : class, IEntity<long>
{
}

/// <summary>  
///     Repository标记接口
/// </summary>
public interface IRepository<TEntity, in TKey> : IRepository where TEntity : class, IEntity<TKey>
{
    #region PageList

    Task<PageListResult<TEntity>> PageListAsync<TS>(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, TS>> orderBy,
        bool isAsc = true,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region SingleList

    Task<IEnumerable<TEntity>> SingleListAsync(
        Expression<Func<TEntity, bool>> filter,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<TEntity>> SingleListAsync<TTarget>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TTarget> keySelector,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

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

    Task<ExecutiveResult<TEntity>> GetAsync(
        TKey id,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    Task<ExecutiveResult<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

    Task<ExecutiveResult<TEntity>> GetAsync<TTarget>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TTarget> keySelector,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    );

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

    /// <summary>
    /// 根据条件查询，返回IQueryable 
    /// </summary>
    /// <param name="expression">查询条件</param>
    /// <param name="writeDb">是否读写库，默认false,可选参数</param>
    /// <param name="noTracking">是否开启跟踪，默认false,可选参数</param>
    /// <returns></returns>
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, bool writeDb = false, bool noTracking = true);

    #region Add

    Task<ExecutiveResult<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    Task<ExecutiveResult> AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region Update

    Task<ExecutiveResult<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Delete

    Task<ExecutiveResult> DeleteAsync(
        object id,
        CancellationToken cancellationToken = default
    );

    Task<ExecutiveResult<TEntity>> DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    Task<ExecutiveResult> BatchDeleteAsync(
        IEnumerable<object> idCollection,
        CancellationToken cancellationToken = default
    );

    Task<ExecutiveResult> BatchDeleteAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    );

    #endregion
}