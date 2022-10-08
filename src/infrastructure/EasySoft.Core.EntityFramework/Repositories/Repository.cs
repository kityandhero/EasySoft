using System.Linq.Expressions;
using EasySoft.Core.Data.Repositories;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.EntityFramework.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class, new()
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    protected Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    #region PageList

    public IEnumerable<T> PageList<TS>(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>> where,
        Expression<Func<T, TS>> orderBy,
        out int total,
        bool isAsc = true
    )
    {
        total = _context.Set<T>().Where(where).Count();

        if (isAsc)
            return
                _context.Set<T>().Where(where)
                    .OrderBy(orderBy)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToList();

        return
            _context.Set<T>().Where(where)
                .OrderByDescending(orderBy)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToList();
    }

    #endregion

    #region Exists

    public ExecutiveResult Exists(Expression<Func<T, bool>> where)
    {
        var o = Get(where);

        return !o.Success ? new ExecutiveResult(ReturnCode.NoData) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion

    #region Update

    public virtual ExecutiveResult<T> Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        var success = _context.SaveChanges() > 0;

        if (!success)
            return new ExecutiveResult<T>(ReturnCode.NoChange)
            {
                Data = entity
            };

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    #endregion

    public void Save()
    {
        _context.SaveChanges();
    }

    public DbSet<T> GetDbSet()
    {
        return _dbSet;
    }

    #region SingleList

    public virtual IEnumerable<T> SingleList(
        Expression<Func<T, bool>> filter
    )
    {
        return _context.Set<T>().Where(filter).ToList();
    }

    public virtual IEnumerable<T> SingleList<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        bool descending = false
    )
    {
        return descending
            ? _context.Set<T>().Where(filter).AsEnumerable().OrderByDescending(keySelector).ToList()
            : _context.Set<T>().Where(filter).AsEnumerable().OrderBy(keySelector).ToList();
    }

    public virtual IEnumerable<T> SingleList<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false
    )
    {
        return descending
            ? _context.Set<T>().Where(filter).AsEnumerable().OrderByDescending(keySelector, comparer)
                .ToList()
            : _context.Set<T>().Where(filter).AsEnumerable().OrderBy(keySelector, comparer).ToList();
    }

    #endregion

    #region Get

    public virtual ExecutiveResult<T> Get(object id)
    {
        var entity = _dbSet.Find(id);

        if (entity == null) return new ExecutiveResult<T>(ReturnCode.NoData);

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual ExecutiveResult<T> Get(
        Expression<Func<T, bool>> filter
    )
    {
        var entity = SingleList(filter).SingleOrDefault();

        if (entity == null) return new ExecutiveResult<T>(ReturnCode.NoData);

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual ExecutiveResult<T> Get<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        bool descending = false
    )
    {
        var entity = SingleList(filter, keySelector, descending).SingleOrDefault();

        if (entity == null) return new ExecutiveResult<T>(ReturnCode.NoData);

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual ExecutiveResult<T> Get<TKey>(
        Expression<Func<T, bool>> filter,
        Func<T, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false
    )
    {
        var entity = SingleList(filter, keySelector, comparer, descending).SingleOrDefault();

        if (entity == null) return new ExecutiveResult<T>(ReturnCode.NoData);

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    #endregion

    #region Add

    public virtual ExecutiveResult<T> Add(T entity)
    {
        _context.Add(entity);

        var success = _context.SaveChanges() > 0;

        if (!success)
            return new ExecutiveResult<T>(ReturnCode.NoChange)
            {
                Data = entity
            };

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public async Task<ExecutiveResult<T>> AddAsync(T entity)
    {
        await _context.AddAsync(entity);

        var success = _context.SaveChanges() > 0;

        if (!success)
            return new ExecutiveResult<T>(ReturnCode.NoChange)
            {
                Data = entity
            };

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual ExecutiveResult AddRange(IEnumerable<T> entities)
    {
        _context.AddRange(entities);

        var success = _context.SaveChanges() > 0;

        return !success ? new ExecutiveResult(ReturnCode.NoChange) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion

    #region Delete

    public virtual ExecutiveResult Delete(object id)
    {
        var entityToDelete = _dbSet.Find(id);

        return entityToDelete != null
            ? Delete(entityToDelete).ToExecutiveResult()
            : new ExecutiveResult(ReturnCode.NoData);
    }

    public virtual ExecutiveResult<T> Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached) _dbSet.Attach(entity);

        _dbSet.Remove(entity);

        _context.Entry(entity).State = EntityState.Deleted;

        var success = _context.SaveChanges() > 0;

        if (!success)
            return new ExecutiveResult<T>(ReturnCode.NoChange)
            {
                Data = entity
            };

        return new ExecutiveResult<T>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual ExecutiveResult BatchDelete(params object[] ids)
    {
        foreach (var item in ids)
        {
            var entity = _context.Set<T>().Find(item); //如果实体已经在内存中，那么就直接从内存拿，如果内存中跟踪实体没有，那么才查询数据库。

            if (entity != null) _context.Set<T>().Remove(entity);
        }

        var success = _context.SaveChanges() > 0;

        return !success ? new ExecutiveResult(ReturnCode.NoChange) : new ExecutiveResult(ReturnCode.Ok);
    }

    public virtual ExecutiveResult BatchDelete(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);

        var success = _context.SaveChanges() > 0;

        return !success ? new ExecutiveResult(ReturnCode.NoChange) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion
}