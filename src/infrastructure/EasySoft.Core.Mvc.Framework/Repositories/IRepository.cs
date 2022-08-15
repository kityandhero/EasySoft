using System.Linq.Expressions;
using EasySoft.UtilityTools.Result;

namespace EasySoft.Core.Mvc.Framework.Repositories;

public interface IRepository
{
}

/// <summary>
/// Repository标记接口
/// </summary>
public interface IRepository<T> : IRepository where T : class, new()
{
    #region PageList

    IEnumerable<T> PageList<TS>(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>> where,
        Expression<Func<T, TS>> orderBy,
        out int total,
        bool isAsc
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

    #region Exists

    ExecutiveResult Exists(Expression<Func<T, bool>> where);

    #endregion

    #region Get

    T? Get(object id);

    T? Get(
        Expression<Func<T, bool>> filter
    );

    T? Get<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        bool descending = false
    );

    T? Get<TKey>(
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

    #region Update

    ExecutiveResult<T> Update(T entity);

    #endregion

    #region Delete

    ExecutiveResult Delete(object id);

    ExecutiveResult<T> Delete(T entity);

    ExecutiveResult BatchDelete(params object[] ids);

    ExecutiveResult BatchDelete(IEnumerable<T> entities);

    #endregion

    void Save();
}