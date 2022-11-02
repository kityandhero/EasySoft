using EasySoft.Core.Domain.Base.Entities.Interfaces;
using EasySoft.Core.EventBus;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.Domain.Base.Entities.Implementations;

/// <summary>
/// 基础聚合根, (Fluent API 配置的优先级高于数据注释属性, 同时配置可考虑兼容 EntityFrameWork Core 之外的 ORM, 若无兼容性需求, 选其一即可, 推荐 Fluent API, 数据注释属性设计上容易引起 DDD Domain 定义混淆, 可考虑继承接口并在接口上定义数据注释属性)
/// </summary>
public class BaseAggregateRoot : DomainEntity, IAggregateRoot
{
    public virtual byte[] RowVersion { get; set; } = { };

    public Lazy<IEventPublisher> EventPublisher => new(() =>
    {
        var httpContext = TreatAssist.Accessor.GetCurrentHttpContext();

        return httpContext is not null
            ? httpContext.RequestServices.GetRequiredService<IEventPublisher>()
            : ServiceAssist.GetServiceProvider().GetRequiredService<IEventPublisher>();
    });
}