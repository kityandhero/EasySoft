using EasySoft.Core.Domain.Base.Entities.Interfaces;
using EasySoft.Core.EventBus;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.Domain.Base.Entities.implementations;

public class BaseAggregateRoot : DomainEntity, IAggregateRoot
{
    public byte[] RowVersion { get; set; } = { };

    public Lazy<IEventPublisher> EventPublisher => new(() =>
    {
        var httpContext = TreatAssist.Accessor.GetCurrentHttpContext();

        return httpContext is not null
            ? httpContext.RequestServices.GetRequiredService<IEventPublisher>()
            : ServiceAssist.GetServiceProvider().GetRequiredService<IEventPublisher>();
    });
}