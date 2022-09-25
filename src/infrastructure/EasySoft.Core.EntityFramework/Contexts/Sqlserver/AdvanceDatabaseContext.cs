using DotNetCore.CAP;
using EasySoft.Core.EntityFramework.Contexts.Basic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasySoft.Core.EntityFramework.Contexts.Sqlserver;

public abstract class AdvanceDatabaseContext : AdvanceDatabaseContextBase
{
    public AdvanceDatabaseContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
    }

    // public override Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus)
    // {
    //     if (_currentTransaction != null) return null;
    //     _currentTransaction = Database.BeginTransaction(capBus, autoCommit: false);
    //     return Task.FromResult(_currentTransaction);
    // }
}