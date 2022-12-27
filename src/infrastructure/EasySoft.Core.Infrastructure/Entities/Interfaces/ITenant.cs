namespace EasySoft.Core.Infrastructure.Entities.Interfaces;

/// <summary>
/// ITenant
/// </summary>
public interface ITenant : ITenant<long>
{
}

/// <summary>
/// ITenant
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface ITenant<TKey>
{
    /// <summary>
    /// TenantId
    /// </summary>
    TKey TenantId { get; set; }
}