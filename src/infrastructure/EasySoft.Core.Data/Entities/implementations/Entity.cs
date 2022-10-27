using EasySoft.Core.Data.Entities.Interfaces;

namespace EasySoft.Core.Data.Entities.implementations;

public class Entity : IEntity<long>
{
    public virtual long Id { get; set; }
}