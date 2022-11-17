namespace EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

/// <summary>
/// IEntity
/// </summary>
public interface IEntity : IEntity<long>
{
}

/// <summary>
/// IEntity
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IEntity<TKey>
{
    TKey Id { get; set; }
}