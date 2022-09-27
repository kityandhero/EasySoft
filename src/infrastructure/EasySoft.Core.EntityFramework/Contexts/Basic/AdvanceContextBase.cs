using DotNetCore.CAP;
using EasySoft.Core.Data.Interfaces;
using EasySoft.Core.EntityFramework.Configures;
using EasySoft.Core.EntityFramework.ExtensionMethods;
using EasySoft.Core.EntityFramework.InterFaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.EntityFramework.Contexts.Basic;

public abstract class AdvanceContextBase : DbContext, IAdvanceUnitOfWork, IAdvanceTransaction
{
    protected IMediator _mediator;

    protected AdvanceContextBase(
        DbContextOptions options
        // bool debugMode,
    ) : base(options)
    {
        _mediator = this.GetService<IMediator>();
    }

    #region IUnitOfWork

    public abstract Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default);

    #endregion

    #region ITransaction

    protected IDbContextTransaction _currentTransaction;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus)
    {
        if (_currentTransaction != null)
        {
            return null;
        }

        _currentTransaction = BeginTransactionWithPersistence(capBus, autoCommit: false);

        return Task.FromResult(_currentTransaction);
    }

    protected abstract IDbContextTransaction BeginTransactionWithPersistence(
        ICapPublisher publisher,
        bool autoCommit = false
    );

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