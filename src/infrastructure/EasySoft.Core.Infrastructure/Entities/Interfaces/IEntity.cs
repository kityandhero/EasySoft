namespace EasySoft.Core.Infrastructure.Entities.Interfaces;

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
    /// <summary>
    /// Id
    /// </summary>
    TKey Id { get; set; }
}