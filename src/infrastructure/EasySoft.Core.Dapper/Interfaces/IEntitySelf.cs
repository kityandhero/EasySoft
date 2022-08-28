using System.Linq.Expressions;

namespace EasySoft.Core.Dapper.Interfaces
{
    public interface IEntitySelf<T> : IEntity
    {
        Expression<Func<T, object>> GetTablePrimaryKeyLambda();
    }
}