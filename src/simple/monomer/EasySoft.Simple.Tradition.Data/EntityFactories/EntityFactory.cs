using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;
using EasySoft.IdGenerator.Assists;

namespace EasySoft.Simple.Tradition.Data.EntityFactories;

public static class EntityFactory
{
    public static TEntity Create<TEntity>() where TEntity : class, IEntity<long>, new()
    {
        return new TEntity
        {
            Id = IdentifierAssist.Create()
        };
    }
}