using System.Linq.Expressions;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.Data.Repositories;

public interface IRepository
{
}

/// <summary>
///     Repository标记接口
/// </summary>
public interface IRepository<TEntity> : IRepository where TEntity : class
{
    #region Exists

    Task<ExecutiveResult> ExistAsync(
        Expression<Func<TEntity, bool>> where,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region PageList

    Task<PageListResult<TEntity>> PageListAsync<TS>(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, TS>> orderBy,
        bool isAsc = true,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region SingleList

    Task<IEnumerable<TEntity>> SingleListAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<TEntity>> SingleListAsync<TKey>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TKey> keySelector,
        bool descending = false,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<TEntity>> SingleListAsync<TKey>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region Get

    Task<ExecutiveResult<TEntity>> GetAsync(
        object id,
        CancellationToken cancellationToken = default
    );

    Task<ExecutiveResult<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    );

    Task<ExecutiveResult<TEntity>> GetAsync<TKey>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TKey> keySelector,
        bool descending = false,
        CancellationToken cancellationToken = default
    );

    Task<ExecutiveResult<TEntity>> GetAsync<TKey>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false,
        CancellationToken cancellationToken = default
    );

    #endregion

    #region CountAsync

    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> whereExpression,
        CancellationToken cancellationToken = default
    );

    #endregion

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