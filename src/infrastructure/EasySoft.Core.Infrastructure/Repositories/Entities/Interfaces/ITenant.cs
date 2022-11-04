namespace EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

public interface ITenant : ITenant<long>
{
}

public interface ITenant<TKey>
{
    TKey TenantId { get; set; }
}