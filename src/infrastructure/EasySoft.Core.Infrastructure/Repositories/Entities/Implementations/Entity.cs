using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

namespace EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;

public abstract class Entity : IEntity<long>
{
    public virtual long Id { get; set; }
}