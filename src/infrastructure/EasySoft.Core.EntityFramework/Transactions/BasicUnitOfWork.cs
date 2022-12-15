namespace EasySoft.Core.EntityFramework.Transactions;

/// <summary>
/// BasicUnitOfWork
/// </summary>
/// <typeparam name="TContext"></typeparam>
public abstract class BasicUnitOfWork<TContext> : IUnitOfWork where TContext : BasicContext
{
    /// <summary>
    /// BasicUnitOfWork
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="ArgumentNullException"></exception>
    protected BasicUnitOfWork(TContext context)
    {
        AdvanceContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// AdvanceContext
    /// </summary>
    protected TContext AdvanceContext { get; init; }

    /// <summary>
    /// DbTransaction
    /// </summary>
    protected IDbContextTransaction? DbTransaction { get; set; }

    /// <summary>
    /// WhetherStartingUow
    /// </summary>
    public bool WhetherStartingUow => AdvanceContext.Database.CurrentTransaction is not null;

    /// <summary>
    /// BeginTransaction
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <param name="distributed"></param>
    /// <exception cref="ArgumentException"></exception>
    public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, bool distributed = false)
    {
        if (AdvanceContext.Database.CurrentTransaction is not null)
            throw new ArgumentException($"UnitOfWork Error,{AdvanceContext.Database.CurrentTransaction}");

        DbTransaction = GetDbContextTransaction(isolationLevel, distributed);
    }

    /// <summary>
    /// Commit
    /// </summary>
    public void Commit()
    {
        var transaction = CheckDbTransactionExistence();

        transaction.Commit();
    }

    /// <summary>
    /// Rollback
    /// </summary>
    public void Rollback()
    {
        var transaction = CheckDbTransactionExistence();

        transaction.Rollback();
    }

    /// <summary>
    /// CommitAsync
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        var transaction = CheckDbTransactionExistence();

        await transaction.CommitAsync(cancellationToken);
    }

    /// <summary>
    /// RollbackAsync
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        var transaction = CheckDbTransactionExistence();

        await transaction.RollbackAsync(cancellationToken);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     校验事务是否存在
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private IDbContextTransaction CheckDbTransactionExistence()
    {
        if (DbTransaction is null)
            throw new ArgumentNullException(nameof(DbTransaction), "IDbContextTransaction is null");

        return DbTransaction;
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;

        if (DbTransaction is null) return;

        DbTransaction.Dispose();
        DbTransaction = null;
    }

    /// <summary>
    /// GetDbContextTransaction
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <param name="distributed"></param>
    /// <returns></returns>
    protected abstract IDbContextTransaction GetDbContextTransaction(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        bool distributed = false
    );
}