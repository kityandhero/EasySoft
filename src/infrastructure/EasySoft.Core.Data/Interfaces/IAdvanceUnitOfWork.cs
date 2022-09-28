using System.Data;

namespace EasySoft.Core.Data.Interfaces;

public interface IAdvanceUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default);
}