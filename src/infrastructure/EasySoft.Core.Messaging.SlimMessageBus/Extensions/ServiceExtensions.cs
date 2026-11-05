using EasySoft.Core.Messaging.Abstractions;
using EasySoft.Core.Messaging.Observers;
using EasySoft.Core.Messaging.SlimMessageBus.Adapters;
using EasySoft.Core.Messaging.SlimMessageBus.Facades;
using EasySoft.Core.Messaging.SlimMessageBus.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasySoft.Core.Messaging.SlimMessageBus.Extensions;

/// <summary>
/// ServiceExtensions
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// 注册拦截器和观测器、门面
    /// </summary>
    public static IServiceCollection AddKernelMessaging(this IServiceCollection services)
    {
        services.AddKernelMessagingCore();
        services.AddKernelMessagingContracts();

        return services;
    }

    /// <summary>
    /// 注册拦截器和观测器。
    /// </summary>
    /// <remarks>
    /// 不包含任何 <c>AddChildBus</c>，也不依赖任何具体基础设施或业务模块。
    /// 各基础设施模块通过各自的扩展方法追加子总线，本方法只补公共能力。
    /// </remarks>
    /// <param name="services">根容器注册集合。</param>
    /// <returns>原 services，便于链式调用。</returns>
    private static IServiceCollection AddKernelMessagingCore(this IServiceCollection services)
    {
        // 开放泛型注册：SlimMessageBus 运行时按消息类型解析，对所有子总线统一织入。
        services.AddTransient(typeof(IPublishInterceptor<>), typeof(TracingPublishInterceptor<>));
        services.AddTransient(typeof(ISendInterceptor<,>), typeof(TracingSendInterceptor<,>));
        services.AddTransient(typeof(IConsumerInterceptor<>), typeof(TracingConsumeInterceptor<>));

        // 默认观测实现。业务可在自己的模块内 TryAdd 前覆盖。
        services.TryAddSingleton<IMessagePipelineObserver, ActivityPipelineObserver>();

        return services;
    }

    /// <summary>
    /// 注册对外契约：业务门面 + Keyed 出站口。
    /// 两个契约在这里集中注册，AddKernelMessagingCore 不再承担门面注册。
    /// </summary>
    private static IServiceCollection AddKernelMessagingContracts(this IServiceCollection services)
    {
        // 业务路由门面：注入复合 IMessageBus，按消息类型自动路由。
        services.TryAddSingleton<IMessageBusRouterFacade, MessageBusRouterFacade>();

        return services;
    }

    /// <summary>
    /// 把根容器的 <see cref="IMessageBus"/> 以 Keyed <see cref="IOutboundMessageBus"/> 暴露。
    /// </summary>
    /// <remarks>
    /// 直接使用公开的 <c>IMessageBus</c>，不调用 <c>GetChildBus</c>。
    /// 消息按类型自动路由，调用方不需要指定子总线名。
    /// </remarks>
    /// <param name="services">根容器注册集合。</param>
    /// <param name="key">Keyed 服务的 Key，供 <c>[FromKeyedServices(key)]</c> 注入。</param>
    public static IServiceCollection AddKeyedOutboundBus(
        this IServiceCollection services,
        string key
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        services.AddKeyedSingleton<IOutboundMessageBus>(
            key,
            (sp, _) =>
            {
                var bus = sp.GetRequiredService<IMessageBus>();

                return new MessageBusOutboundAdapter(bus);
            }
        );

        return services;
    }
}