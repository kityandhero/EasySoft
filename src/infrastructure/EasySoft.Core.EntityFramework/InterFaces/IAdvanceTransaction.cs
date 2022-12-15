namespace EasySoft.Core.EntityFramework.InterFaces;

/// <summary>
/// IAdvanceTransaction
/// </summary>
public interface IAdvanceTransaction
{
    /// <summary>
    /// GetCurrentTransaction
    /// </summary>
    /// <returns></returns>
    IDbContextTransaction GetCurrentTransaction();

    /// <summary>
    /// HasActiveTransaction
    /// </summary>
    bool HasActiveTransaction { get; }

    /// <summary>
    /// BeginTransactionAsync
    /// </summary>
    /// <param name="capBus"></param>
    /// <returns></returns>
    Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus);

    /// <summary>
    /// CommitAsync
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    Task CommitAsync(IDbContextTransaction transaction);

    /// <summary>
    /// Rollback
    /// </summary>
    void Rollback();
}