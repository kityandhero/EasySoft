extern alias MySql;
using EasySoft.Core.EntityFramework.Contexts.Basic;
using EasySoft.Domain.Infrastructure.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasySoft.Domain.Infrastructure.Core;

public class AdvanceDatabaseContext : AdvanceDatabaseContextBase
{
    public AdvanceDatabaseContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
    {
    }

    public override async Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);

        //实现领域事件的发送
        await _mediator.DispatchDomainEventsAsync(this);

        return true;
    }

    protected override IDbContextTransaction BeginTransactionWithPersistence(
        ICapPublisher publisher,
        bool autoCommit = false
    )
    {
        // ReSharper disable once RedundantNameQualifier
        return MySql::DotNetCore.CAP.CapTransactionExtensions.BeginTransaction(
            Database,
            publisher,
            autoCommit
        );
    }
}