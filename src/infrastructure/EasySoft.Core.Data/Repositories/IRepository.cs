using System.Linq.Expressions;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.Data.Repositories;

public interface IRepository
{
}

/// <summary>
///     Repository标记接口
/// </summary>
public interface IRepository<T> : IRepository where T : class, new()
{
    #region Exists

    ExecutiveResult Exists(Expression<Func<T, bool>> where);

    #endregion

    #region Update

    ExecutiveResult<T> Update(T entity);

    #endregion

    void Save();

    #region PageList

    IEnumerable<T> PageList<TS>(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>> where,
        Expression<Func<T, TS>> orderBy,
        out int total,
        bool isAsc = true
    );

    #endregion

    #region SingleList

    IEnumerable<T> SingleList(Expression<Func<T, bool>> filter);

    IEnumerable<T> SingleList<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        bool descending = false
    );

    IEnumerable<T> SingleList<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false
    );

    #endregion

    #region Get

    ExecutiveResult<T> Get(object id);

    ExecutiveResult<T> Get(
        Expression<Func<T, bool>> filter
    );

    ExecutiveResult<T> Get<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        bool descending = false
    );

    ExecutiveResult<T> Get<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false
    );

    #endregion

    #region Add

    ExecutiveResult<T> Add(T entity);
    ExecutiveResult AddRange(IEnumerable<T> entities);

    #endregion

    #region Delete

    ExecutiveResult Delete(object id);

    ExecutiveResult<T> Delete(T entity);

    ExecutiveResult BatchDelete(params object[] ids);

    ExecutiveResult BatchDelete(IEnumerable<T> entities);

    #endregion
}