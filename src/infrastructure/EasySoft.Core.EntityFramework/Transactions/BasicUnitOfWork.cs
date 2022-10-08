using System.Data;
using EasySoft.Core.Data.Transactions;
using EasySoft.Core.EntityFramework.Contexts.Basic;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasySoft.Core.EntityFramework.Transactions;

public abstract class BasicUnitOfWork<TContext> : IUnitOfWork where TContext : BasicContext
{
    protected BasicUnitOfWork(TContext context)
    {
        AdvanceContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected TContext AdvanceContext { get; init; }

    protected IDbContextTransaction? DbTransaction { get; set; }

    public bool IsStartingUow => AdvanceContext.Database.CurrentTransaction is not null;

    public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, bool distributed = false)
    {
        if (AdvanceContext.Database.CurrentTransaction is not null)
            throw new ArgumentException($"UnitOfWork Error,{AdvanceContext.Database.CurrentTransaction}");

        DbTransaction = GetDbContextTransaction(isolationLevel, distributed);
    }

    public void Commit()
    {
        var transaction = CheckDbTransactionExistence();

        transaction.Commit();
    }

    public void Rollback()
    {
        var transaction = CheckDbTransactionExistence();

        transaction.Rollback();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        var transaction = CheckDbTransactionExistence();

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        var transaction = CheckDbTransactionExistence();

        await transaction.RollbackAsync(cancellationToken);
    }

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

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;

        if (DbTransaction is null) return;

        DbTransaction.Dispose();
        DbTransaction = null;
    }

    protected abstract IDbContextTransaction GetDbContextTransaction(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        bool distributed = false
    );
}