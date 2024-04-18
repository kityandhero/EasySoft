using EasySoft.Core.Infrastructure.Entities.Interfaces;

namespace EasySoft.Core.Sql.Interfaces;

/// <summary>
/// IEntitySelf
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntitySelf<T> : IEntity where T : IEntity
{
    /// <summary>
    /// GetPrimaryKeyLambda
    /// </summary>
    /// <returns></returns>
    Expression<Func<T, object>> GetPrimaryKeyLambda();
}