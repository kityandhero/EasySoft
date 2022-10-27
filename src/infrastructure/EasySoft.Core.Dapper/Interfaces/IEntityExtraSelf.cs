using System.Linq.Expressions;
using EasySoft.Core.Sql.Interfaces;

namespace EasySoft.Core.Dapper.Interfaces;

public interface IEntityExtraSelf<T> : IEntityExtra
{
    Expression<Func<T, object>> GetPrimaryKeyLambda();
}