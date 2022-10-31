using EasySoft.Core.Infrastructure.Entities.Interfaces;

namespace EasySoft.Core.Infrastructure.Entities.implementations;

public class Entity : IEntity<long>
{
    public virtual long Id { get; set; }
}