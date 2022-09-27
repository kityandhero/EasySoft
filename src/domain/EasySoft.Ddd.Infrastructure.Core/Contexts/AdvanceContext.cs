extern alias MySql;
using EasySoft.Core.EntityFramework.Contexts.Basic;
using EasySoft.Domain.Infrastructure.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace EasySoft.Domain.Infrastructure.Core.Contexts;

public class AdvanceContext : AdvanceContextBase
{
    public AdvanceContext(
        DbContextOptions options
    ) : base(options)
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