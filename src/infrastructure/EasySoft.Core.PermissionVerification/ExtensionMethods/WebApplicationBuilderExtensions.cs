using EasySoft.Core.AccessWayTransmitter.ExtensionMethods;
using EasySoft.Core.PermissionVerification.Detectors;
using EasySoft.Core.PermissionVerification.Middlewares;
using EasySoft.Core.PermissionVerification.Observers;
using EasySoft.Core.PermissionVerification.Officers;

namespace EasySoft.Core.PermissionVerification.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddPermissionVerification = "d3dd59e3-0a28-488f-b434-9db031e5c66f";
    private const string UniqueIdentifierAddAccessWayDetector = "90dd6ccf-55e7-4f38-ba38-a7377cdc826e";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TAccessWayDetector"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder AddAccessWayDetector<TAccessWayDetector>(
        this WebApplicationBuilder builder
    ) where TAccessWayDetector : class, IAccessWayDetector
    {
        if (builder.HasRegistered(UniqueIdentifierAddAccessWayDetector))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAccessWayDetector)}<{typeof(TAccessWayDetector).Name}>."
        );

        builder.Services.AddTransient<IAccessWayDetector, TAccessWayDetector>();

        return builder;
    }

    /// <summary>
    /// 配置操作者验证以及操作权限验证, 需要配置在 UseEasyToken/UseAdvanceJsonWebToken 之后
    /// </summary> 
    /// <param name="builder"></param>
    /// <param name="middlewareMode">使用中间件模式</param>
    /// <returns></returns>
    public static WebApplicationBuilder AddPermissionVerification<TPermissionObserver>(
        this WebApplicationBuilder builder,
        bool middlewareMode = true
    ) where TPermissionObserver : IPermissionObserver
    {
        if (builder.HasRegistered(UniqueIdentifierAddPermissionVerification))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddPermissionVerification)}<{typeof(TPermissionObserver).Name}>."
        );

        builder.AddPermissionObserverInjection<TPermissionObserver>()
            .AddAccessWayTransmitter();

        if (middlewareMode)
        {
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                // https://docs.autofac.org/en/latest/faq/per-request-scope.html
                containerBuilder.RegisterType<OperateOfficer>().As<IOperateOfficer>().InstancePerLifetimeScope();
            });

            builder.Services.AddTransient<PermissionVerificationMiddleware>();

            FlagAssist.PermissionVerificationMiddlewareModeSwitch = true;
        }

        FlagAssist.PermissionVerificationSwitch = true;

        return builder;
    }

    /// <summary>
    /// 注入权限判断器
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPermissionObserverInjection<T>(
        this WebApplicationBuilder builder
    ) where T : IPermissionObserver
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddPermissionObserverInjection)}<{typeof(T).Name}>."
        );

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            // https://docs.autofac.org/en/latest/faq/per-request-scope.html
            containerBuilder.RegisterType<T>().As<IPermissionObserver>().InstancePerLifetimeScope();
        });

        return builder;
    }
}