using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

namespace EasySoft.Core.Dapper.Interfaces;

public interface IEntitySelf<T> : IEntity
{
    Expression<Func<T, object>> GetPrimaryKeyLambda();
}