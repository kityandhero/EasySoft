using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.EntityFramework.Repositories;

/// <summary>
/// Repository  
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class Repository<TEntity> : Repository<TEntity, long>, IRepository<TEntity>
    where TEntity : class, IEntity<long>, new()
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <param name="context"></param>
    public Repository(DbContext context) : base(context)
    {
    }
}

/// <summary>
/// Repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class Repository<TEntity, TKey> : Repository<DbContext, TEntity, TKey>
    where TEntity : class, IEntity<TKey>, new()
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <param name="context"></param>
    protected Repository(DbContext context) : base(context)
    {
    }
}

/// <summary>
/// Repository
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class Repository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
    where TDbContext : DbContext
    where TEntity : class, IEntity<TKey>, new()
{
    /// <summary>
    /// Context
    /// </summary>
    protected virtual TDbContext Context { get; }

    /// <summary>
    /// Repository
    /// </summary>
    /// <param name="context"></param>
    protected Repository(TDbContext context)
    {
        Context = context;
    }

    #region Read

    #region PageList

    /// <inheritdoc />
    public async Task<PageListResult<TEntity>> PageListAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>>? where = null,
        bool isAsc = true,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        return await PageListAsync<TEntity>(
            pageIndex,
            pageSize,
            where,
            null,
            isAsc,
            writeChannel,
            cancellationToken
        );
    }

    /// <inheritdoc />
    public async Task<PageListResult<TEntity>> PageListAsync<TS>(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>>? where = null,
        Expression<Func<TEntity, TS>>? order = null,
        bool isAsc = true,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        var total = await Where(where, writeChannel, true).CountAsync(cancellationToken);

        List<TEntity> list;

        if (isAsc)
            list = order == null
                ? await Where(where, writeChannel, true)
                    .OrderByDescending(o => o.Id)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                : await Where(where, writeChannel, true)
                    .OrderBy(order)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);
        else
            list = order == null
                ? await Where(where, writeChannel, true)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                : await Where(where, writeChannel, true)
                    .OrderByDescending(order)
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

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TEntity>> SingleListAsync(
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        return await GetSet(writeChannel, true).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TEntity>> SingleListAsync(
        Expression<Func<TEntity, bool>> where,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        return await Where(where, writeChannel, true).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TEntity>> SingleListAsync<TTarget>(
        Expression<Func<TEntity, bool>> where,
        Func<TEntity, TTarget> keySelector,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        return descending
            ? await Where(where, writeChannel, true)
                .AsEnumerable()
                .OrderByDescending(keySelector)
                .AsQueryable()
                .ToListAsync(cancellationToken)
            : await Where(where, writeChannel, true)
                .AsEnumerable()
                .OrderBy(keySelector)
                .AsQueryable()
                .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TEntity>> SingleListAsync<TTarget>(
        Expression<Func<TEntity, bool>> where,
        Func<TEntity, TTarget> keySelector,
        IComparer<TTarget>? comparer,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        return descending
            ? await Where(where, writeChannel, true)
                .AsEnumerable()
                .OrderByDescending(keySelector, comparer)
                .AsQueryable()
                .ToListAsync(cancellationToken)
            : await Where(where, writeChannel, true)
                .AsEnumerable()
                .OrderBy(keySelector, comparer)
                .AsQueryable()
                .ToListAsync(cancellationToken);
    }

    #endregion

    #region Get

    /// <inheritdoc />
    public virtual async Task<ExecutiveResult<TEntity>> GetAsync(
        TKey id,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        var entity = writeChannel
            ? await Context.Set<TEntity>()
                .TagWith(RepositoryConst.MaxScaleRouteToMaster)
                .FirstAsync(x => x.Id != null && x.Id.Equals(id), cancellationToken)
            : await Context.Set<TEntity>().FindAsync(id);

        if (entity == null) return new ExecutiveResult<TEntity>(ReturnCode.NoData);

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    /// <inheritdoc />
    public virtual async Task<ExecutiveResult<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        var entity = (await SingleListAsync(filter, writeChannel, cancellationToken)).SingleOrDefault();

        if (entity == null) return new ExecutiveResult<TEntity>(ReturnCode.NoData);

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    /// <inheritdoc />
    public virtual async Task<ExecutiveResult<TEntity>> GetAsync<TTarget>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TTarget> keySelector,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        var entity = (await SingleListAsync(filter, keySelector, descending, writeChannel, cancellationToken))
            .SingleOrDefault();

        if (entity == null) return new ExecutiveResult<TEntity>(ReturnCode.NoData);

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    /// <inheritdoc />
    public virtual async Task<ExecutiveResult<TEntity>> GetAsync<TTarget>(
        Expression<Func<TEntity, bool>> filter,
        Func<TEntity, TTarget> keySelector,
        IComparer<TTarget>? comparer,
        bool descending = false,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        var entity = (await SingleListAsync(
                filter,
                keySelector,
                comparer,
                descending,
                writeChannel,
                cancellationToken
            ))
            .SingleOrDefault();

        if (entity == null) return new ExecutiveResult<TEntity>(ReturnCode.NoData);

        return new ExecutiveResult<TEntity>(ReturnCode.Ok)
        {
            Data = entity
        };
    }

    #endregion

    #region Exists

    /// <inheritdoc />
    public async Task<ExecutiveResult> ExistAsync(
        Expression<Func<TEntity, bool>> where,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        var o = await GetAsync(where, writeChannel, cancellationToken);

        return !o.Success ? new ExecutiveResult(ReturnCode.NoData) : new ExecutiveResult(ReturnCode.Ok);
    }

    #endregion

    #region CountAsync

    /// <inheritdoc />
    public virtual async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> where,
        bool writeChannel = false,
        CancellationToken cancellationToken = default
    )
    {
        var dbSet = GetSet(writeChannel, true);

        return await dbSet.CountAsync(where, cancellationToken);
    }

    #endregion

    #region where

    /// <summary>
    /// GetSet
    /// </summary>
    /// <param name="writeChannel"></param>
    /// <param name="noTracking"></param>
    /// <returns></returns>
    protected virtual IQueryable<TEntity> GetSet(bool writeChannel, bool noTracking)
    {
        switch (noTracking)
        {
            case true when writeChannel:
                return Context.Set<TEntity>().AsNoTracking().TagWith(RepositoryConst.MaxScaleRouteToMaster);

            case true:
                return Context.Set<TEntity>().AsNoTracking();
        }

        if (writeChannel)
            return Context.Set<TEntity>().TagWith(RepositoryConst.MaxScaleRouteToMaster);

        return Context.Set<TEntity>();
    }

    /// <inheritdoc />
    public virtual IQueryable<TEntity> Where(
        Expression<Func<TEntity, bool>>? expression = null,
        bool writeChannel = false,
        bool noTracking = true
    )
    {
        return expression == null
            ? GetSet(writeChannel, noTracking)
            : GetSet(writeChannel, noTracking).Where(expression);
    }

    #endregion

    #endregion

    #region Creeate

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
    public virtual async Task<ExecutiveResult<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        //获取实体状态
        var entry = Context.Entry(entity);

        //如果实体没有被跟踪，必须指定需要更新的列
        if (entry.State == EntityState.Detached)
            Context.Update(entity);

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

    /// <inheritdoc />
    public virtual async Task<int> UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        Context.UpdateRange(entities);

        return await Context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<ExecutiveResult> DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Delete

    /// <summary>
    /// DeleteAsync
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ExecutiveResult> DeleteAsync(
        TKey id,
        CancellationToken cancellationToken = default
    )
    {
        var executiveResult = await GetAsync(id, true, cancellationToken);

        return executiveResult.Success && executiveResult.Data != null
            ? (await DeleteAsync(executiveResult.Data, cancellationToken)).ToExecutiveResult()
            : new ExecutiveResult(ReturnCode.NoData);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
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