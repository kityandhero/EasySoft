using DotNetCore.CAP;
using EasySoft.Core.Data.Interfaces;
using EasySoft.Core.EntityFramework.ExtensionMethods;
using EasySoft.Core.EntityFramework.InterFaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasySoft.Core.EntityFramework.Contexts.Basic;

public abstract class AdvanceDatabaseContextBase : DbContext, IAdvanceUnitOfWork, IAdvanceTransaction
{
    protected IMediator _mediator;

    protected AdvanceDatabaseContextBase(
        DbContextOptions options,
        IMediator mediator
    ) : base(options)
    {
        _mediator = mediator;
    }

    #region IUnitOfWork

    public abstract Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default);

    #endregion

    #region ITransaction

    protected IDbContextTransaction _currentTransaction;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public abstract Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus);

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    #endregion
}