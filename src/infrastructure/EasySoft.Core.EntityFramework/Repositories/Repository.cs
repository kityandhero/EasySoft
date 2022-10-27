using System.Linq.Expressions;
using EasySoft.Core.Data.Repositories;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.EntityFramework.Repositories;

public class Repository<TEntity> : Repository<DbContext, TEntity>
    where TEntity : class, new()
{
    public Repository(DbContext context) : base(context)
    {
    }
}

public abstract class Repository<TDbContext, TEntity> : IRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : class, new()
{
    protected virtual TDbContext Context { get; }

    protected Repository(TDbContext context)
    {
        Context = context;
    }

    #region PageList

    public async Task<PageListResult<TEntity>> PageListAsync<TS>(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, TS>> orderBy,
        bool isAsc = true,
        CancellationToken cancellationToken = default
    )
    {
        var total = await Context.Set<TEntity>().Where(where).CountAsync(cancellationToken);

        List<TEntity> list;

        if (isAsc)
            list = await Context.Set<TEntity>().Where(where)
                .OrderBy(orderBy)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        else
            list = await Context.Set<TEntity>().Where(where)
                .OrderByDescending(orderBy)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync(cancellationToken);

        return new PageListResult<TEntity>(ReturnCode.Ok)
        {
            List = list,
            TotalSize = total
        };
    }

    #endregion

    #region SingleList

    public virtual async Task<IEnumerable<TEntity>> SingleListAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    )
    {
        return await Context.Set<TEntity>().Where(filter).ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> SingleListAsync<TKey>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TKey> keySelector,
        bool descending = false,
        CancellationToken cancellationToken = default
    )
    {
        return descending
            ? await Context.Set<TEntity>().Where(filter).AsEnumerable().OrderByDescending(keySelector).AsQueryable()
                .ToListAsync(cancellationToken)
            : await Context.Set<TEntity>().Where(filter).AsEnumerable().OrderBy(keySelector).AsQueryable()
                .ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> SingleListAsync<TKey>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false,
        CancellationToken cancellationToken = default
    )
    {
        return descending
            ? await Context.Set<TEntity>().Where(filter).AsEnumerable().OrderByDescending(keySelector, comparer)
                .AsQueryable()
                .ToListAsync(cancellationToken)
            : await Context.Set<TEntity>().Where(filter).AsEnumerable().OrderBy(keySelector, comparer).AsQueryable()
                .ToListAsync(cancellationToken);
    }

    #endregion

    #region Get

    public virtual async Task<ExecutiveResult<TEntity>> GetAsync(
        object id,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);

        if (entity == null) return new ExecutiveResult<TEntity>(ReturnCode.NoData);

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual async Task<ExecutiveResult<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default
    )
    {
        var entity = (await SingleListAsync(filter, cancellationToken)).SingleOrDefault();

        if (entity == null) return new ExecutiveResult<TEntity>(ReturnCode.NoData);

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual async Task<ExecutiveResult<TEntity>> GetAsync<TKey>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TKey> keySelector,
        bool descending = false,
        CancellationToken cancellationToken = default
    )
    {
        var entity = (await SingleListAsync(filter, keySelector, descending, cancellationToken)).SingleOrDefault();

        if (entity == null) return new ExecutiveResult<TEntity>(ReturnCode.NoData);

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual async Task<ExecutiveResult<TEntity>> GetAsync<TKey>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TKey> keySelector,
        IComparer<TKey>? comparer,
        bool descending = false,
        CancellationToken cancellationToken = default
    )
    {
        var entity = (await SingleListAsync(filter, keySelector, comparer, descending, cancellationToken))
            .SingleOrDefault();

        if (entity == null) return new ExecutiveResult<TEntity>(ReturnCode.NoData);

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    #endregion

    #region Exists

    public async Task<ExecutiveResult> ExistAsync(
        Expression<Func<TEntity, bool>> where,
        CancellationToken cancellationToken = default
    )
    {
        var o = await GetAsync(where, cancellationToken);

        return !o.Success ? new ExecutiveResult(ReturnCode.NoData) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion

    #region CountAsync

    public virtual async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> whereExpression,
        CancellationToken cancellationToken = default
    )
    {
        var dbSet = Context.Set<TEntity>().AsNoTracking();

        return await dbSet.CountAsync(whereExpression, cancellationToken);
    }

    #endregion

    #region Add

    public async Task<ExecutiveResult<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);

        var success = await Context.SaveChangesAsync(cancellationToken) > 0;

        if (!success)
            return new ExecutiveResult<TEntity>(ReturnCode.NoChange)
            {
                Data = entity
            };

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual async Task<ExecutiveResult> AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        await Context.AddRangeAsync(entities, cancellationToken);

        var success = await Context.SaveChangesAsync(cancellationToken) > 0;

        return !success ? new ExecutiveResult(ReturnCode.NoChange) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion

    #region Update

    public virtual async Task<ExecutiveResult<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        //获取实体状态
        var entry = Context.Entry(entity);

        //如果实体没有被跟踪，必须指定需要更新的列
        if (entry.State == EntityState.Detached)
            throw new ArgumentException($"实体没有被跟踪，需要指定更新的列");

        if (entry.State is EntityState.Added or EntityState.Deleted)
            throw new ArgumentException($"{nameof(entity)},实体状态为{nameof(entry.State)}");

        var success = await Context.SaveChangesAsync(cancellationToken) > 0;

        if (!success)
            return new ExecutiveResult<TEntity>(ReturnCode.NoChange)
            {
                Data = entity
            };

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual async Task<int> UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        Context.UpdateRange(entities);

        return await Context.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Delete

    public virtual async Task<ExecutiveResult> DeleteAsync(
        object id,
        CancellationToken cancellationToken = default
    )
    {
        var entityToDelete = await GetAsync(id, cancellationToken);

        return entityToDelete.Success && entityToDelete.Data != null
            ? (await DeleteAsync(entityToDelete.Data, cancellationToken)).ToExecutiveResult()
            : new ExecutiveResult(ReturnCode.NoData);
    }

    public virtual async Task<ExecutiveResult<TEntity>> DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        if (Context.Entry(entity).State == EntityState.Detached) Context.Set<TEntity>().Attach(entity);

        Context.Set<TEntity>().Remove(entity);

        Context.Entry(entity).State = EntityState.Deleted;

        var success = await Context.SaveChangesAsync(cancellationToken) > 0;

        if (!success)
            return new ExecutiveResult<TEntity>(ReturnCode.NoChange)
            {
                Data = entity
            };

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    public virtual async Task<ExecutiveResult> BatchDeleteAsync(
        IEnumerable<object> idCollection,
        CancellationToken cancellationToken = default
    )
    {
        foreach (var item in idCollection)
        {
            var entity = await Context.Set<TEntity>()
                .FindAsync(item, cancellationToken); //如果实体已经在内存中，那么就直接从内存拿，如果内存中跟踪实体没有，那么才查询数据库。

            if (entity != null) Context.Set<TEntity>().Remove(entity);
        }

        var success = await Context.SaveChangesAsync(cancellationToken) > 0;

        return !success ? new ExecutiveResult(ReturnCode.NoChange) : new ExecutiveResult(ReturnCode.Ok);
    }

    public virtual async Task<ExecutiveResult> BatchDeleteAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        Context.Set<TEntity>().RemoveRange(entities);

        var success = await Context.SaveChangesAsync(cancellationToken) > 0;

        return !success ? new ExecutiveResult(ReturnCode.NoChange) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion
}