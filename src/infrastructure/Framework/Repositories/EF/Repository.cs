using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UtilityTools.Enums;
using UtilityTools.Result;

namespace Framework.Repositories.EF;

public abstract class Repository<T> : IRepository<T> where T : class, new()
{
    protected readonly DbContext Context;
    protected readonly DbSet<T> DBSet;

    protected Repository(DbContext context)
    {
        Context = context;
        DBSet = context.Set<T>();
    }

    #region PageList

    public IEnumerable<T> PageList<TS>(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>> where,
        Expression<Func<T, TS>> orderBy,
        out int total,
        bool descending = false
    )
    {
        total = Context.Set<T>().Where(where).Count();

        if (!descending)
        {
            return
                Context.Set<T>()
                    .Where(where)
                    .OrderBy(orderBy)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToList();
        }

        return
            Context.Set<T>()
                .Where(where)
                .OrderByDescending(orderBy)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToList();
    }

    #endregion

    #region SingleList

    public virtual IEnumerable<T> SingleList(
        Expression<Func<T, bool>> filter
    )
    {
        return Context.Set<T>().Where(filter).ToList();
    }

    public virtual IEnumerable<T> SingleList<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        bool descending = false
    )
    {
        return descending
            ? Context.Set<T>().Where(filter).AsEnumerable().OrderByDescending(keySelector).ToList()
            : Context.Set<T>().Where(filter).AsEnumerable().OrderBy(keySelector).ToList();
    }

    public virtual IEnumerable<T> SingleList<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false
    )
    {
        return descending
            ? Context.Set<T>().Where(filter).AsEnumerable().OrderByDescending(keySelector, comparer).ToList()
            : Context.Set<T>().Where(filter).AsEnumerable().OrderBy(keySelector, comparer).ToList();
    }

    #endregion

    #region Exists

    public ExecutiveResult Exists(Expression<Func<T, bool>> where)
    {
        var o = Get(where);

        return o == null ? new ExecutiveResult(ReturnCode.NoData) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion

    public virtual T? Get(object id)
    {
        return DBSet.Find(id);
    }

    #region Get

    public virtual T? Get(
        Expression<Func<T, bool>> filter
    )
    {
        return SingleList(filter).SingleOrDefault();
    }

    public virtual T? Get<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        bool descending = false
    )
    {
        return SingleList(filter, keySelector, descending).SingleOrDefault();
    }

    public virtual T? Get<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false
    )
    {
        return SingleList(filter, keySelector, comparer, descending).SingleOrDefault();
    }

    #endregion

    #region Add

    public virtual ExecutiveResult<T> Add(T entity)
    {
        Context.Add(entity);

        var success = Context.SaveChanges() > 0;

        if (!success)
        {
            return new ExecutiveResult<T>(ReturnCode.NoChange)
            {
                Data = entity
            };
        }

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual ExecutiveResult AddRange(IEnumerable<T> entities)
    {
        Context.AddRange(entities);

        var success = Context.SaveChanges() > 0;

        return !success ? new ExecutiveResult(ReturnCode.NoChange) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion

    #region Update

    public virtual ExecutiveResult<T> Update(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;

        var success = Context.SaveChanges() > 0;

        if (!success)
        {
            return new ExecutiveResult<T>(ReturnCode.NoChange)
            {
                Data = entity
            };
        }

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    #endregion

    #region Delete

    public virtual ExecutiveResult Delete(object id)
    {
        var entityToDelete = DBSet.Find(id);

        return entityToDelete != null
            ? Delete(entityToDelete).ToExecutiveResult()
            : new ExecutiveResult(ReturnCode.NoData);
    }

    public virtual ExecutiveResult<T> Delete(T entity)
    {
        if (Context.Entry(entity).State == EntityState.Detached)
        {
            DBSet.Attach(entity);
        }

        DBSet.Remove(entity);

        Context.Entry(entity).State = EntityState.Deleted;

        var success = Context.SaveChanges() > 0;

        if (!success)
        {
            return new ExecutiveResult<T>(ReturnCode.NoChange)
            {
                Data = entity
            };
        }

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual ExecutiveResult BatchDelete(params object[] ids)
    {
        foreach (var item in ids)
        {
            var entity = Context.Set<T>().Find(item); //如果实体已经在内存中，那么就直接从内存拿，如果内存中跟踪实体没有，那么才查询数据库。

            if (entity != null) Context.Set<T>().Remove(entity);
        }

        var success = Context.SaveChanges() > 0;

        return !success ? new ExecutiveResult(ReturnCode.NoChange) : new ExecutiveResult(ReturnCode.Ok);
    }

    public virtual ExecutiveResult BatchDelete(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);

        var success = Context.SaveChanges() > 0;

        return !success ? new ExecutiveResult(ReturnCode.NoChange) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion

    public void Save()
    {
        Context.SaveChanges();
    }
}