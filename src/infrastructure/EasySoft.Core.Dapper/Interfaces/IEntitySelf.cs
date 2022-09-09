using System.Linq.Expressions;
using EasySoft.Core.Sql.Interfaces;

namespace EasySoft.Core.Dapper.Interfaces
{
    public interface IEntitySelf<T> : IEntity
    {
        Expression<Func<T, object>> GetPrimaryKeyLambda();
    }
}