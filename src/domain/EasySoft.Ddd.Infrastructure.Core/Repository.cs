using EasySoft.Domain.Abstractions.Aggregation;
using EasySoft.Domain.Abstractions.Entities;
using EasySoft.Domain.Infrastructure.Core.Contexts;

namespace EasySoft.Domain.Infrastructure.Core;

public class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    where TDbContext : BaseContext
{
    public Repository(TDbContext context)
    {
        DbContext = context;
    }

    protected virtual TDbContext DbContext { get; set; }

    public virtual TEntity Add(TEntity entity)
    {
        return DbContext.Add(entity).Entity;
    }

    public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Add(entity));
    }

    public virtual TEntity Update(TEntity entity)
    {
        return DbContext.Update(entity).Entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Update(entity));
    }

    public virtual bool Remove(Entity entity)
    {
        DbContext.Remove(entity);
        return true;
    }

    public virtual Task<bool> RemoveAsync(Entity entity)
    {
        return Task.FromResult(Remove(entity));
    }
}