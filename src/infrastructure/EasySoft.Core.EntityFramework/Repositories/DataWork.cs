using DotNetCore.CAP;
using EasySoft.Core.Data.Interfaces;
using EasySoft.Core.EntityFramework.InterFaces;
using LogDashboard.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;

namespace EasySoft.Core.EntityFramework.Repositories;

public class DataWork : DbContext, IAdvanceUnitOfWork, IAdvanceTransaction
{
    protected IMediator _mediator;

    public DataWork(
        DbContextOptions options,
        IMediator mediator
    ) : base(options)
    {
        _mediator = mediator;
    }

    #region IUnitOfWork

    public async Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        //实现领域事件的发送
        await _mediator.DispatchDomainEventsAsync(this);

        return result > 0;
    }

    #endregion

    #region ITransaction

    private IDbContextTransaction _currentTransaction;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus)
    {
        if (_currentTransaction != null) return null;
        _currentTransaction = Database.BeginTransaction(capBus, autoCommit: false);
        return Task.FromResult(_currentTransaction);
    }

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