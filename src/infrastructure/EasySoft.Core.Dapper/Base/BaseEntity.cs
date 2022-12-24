using EasySoft.Core.Dapper.Interfaces;
using EasySoft.Core.Sql.Interfaces;

namespace EasySoft.Core.Dapper.Base;

public abstract class BaseEntity<T> : IEntitySelf<T> where T : BaseEntity<T>
{
    public long Id { get; set; }
}