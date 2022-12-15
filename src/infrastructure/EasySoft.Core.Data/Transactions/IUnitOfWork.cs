namespace EasySoft.Core.Data.Transactions;

/// <summary>
/// IUnitOfWork
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// WhetherStartingUow
    /// </summary>
    bool WhetherStartingUow { get; }

    /// <summary>
    /// BeginTransaction
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <param name="distributed"></param>
    void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, bool distributed = false);

    /// <summary>
    /// Rollback
    /// </summary>
    void Rollback();

    /// <summary>
    /// Commit
    /// </summary>
    void Commit();

    /// <summary>
    /// RollbackAsync
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// CommitAsync
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CommitAsync(CancellationToken cancellationToken = default);
}