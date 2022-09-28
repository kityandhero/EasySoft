using DotNetCore.CAP;
using EasySoft.Core.Data.Interfaces;
using EasySoft.Core.EntityFramework.InterFaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasySoft.Core.EntityFramework.Contexts.Basic;

public abstract class AdvanceContextBase : DbContext, IAdvanceUnitOfWork, IAdvanceTransaction
{
    protected readonly IMediator Mediator;

    protected AdvanceContextBase(
        DbContextOptions options
        // bool debugMode,
    ) : base(options)
    {
        Mediator = this.GetService<IMediator>();
    }

    #region IUnitOfWork

    public abstract Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default);

    #endregion

    #region ITransaction

    protected IDbContextTransaction? CurrentTransaction;
    public IDbContextTransaction GetCurrentTransaction() => CurrentTransaction;
    public bool HasActiveTransaction => CurrentTransaction != null;

    public async Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus)
    {
        if (CurrentTransaction != null)
        {
            return CurrentTransaction;
        }

        CurrentTransaction = BeginTransactionWithPersistenceTarget(capBus, autoCommit: false);

        return await Task.FromResult(CurrentTransaction);
    }

    protected abstract IDbContextTransaction BeginTransactionWithPersistenceTarget(
        ICapPublisher publisher,
        bool autoCommit = false
    );

    public async Task CommitAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != CurrentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            Rollback();
            throw;
        }
        finally
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
                CurrentTransaction = null;
            }
        }
    }

    public void Rollback()
    {
        try
        {
            CurrentTransaction?.Rollback();
        }
        finally
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
                CurrentTransaction = null;
            }
        }
    }

    #endregion
}