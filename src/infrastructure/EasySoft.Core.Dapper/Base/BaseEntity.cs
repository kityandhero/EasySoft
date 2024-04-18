using EasySoft.Core.Sql.Interfaces;

namespace EasySoft.Core.Dapper.Base;

public abstract class BaseEntity<T> : IEntitySelf<T> where T : BaseEntity<T>
{
    public virtual long Id { get; set; } = 0;

    public abstract Expression<Func<T, object>> GetPrimaryKeyLambda();
}