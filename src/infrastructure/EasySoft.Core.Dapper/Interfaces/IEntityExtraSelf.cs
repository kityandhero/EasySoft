namespace EasySoft.Core.Dapper.Interfaces;

public interface IEntityExtraSelf<T> : IEntityExtra
{
    Expression<Func<T, object>> GetPrimaryKeyLambda();
}