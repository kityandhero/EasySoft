using Autofac;
using EasySoft.Core.AccessWayTransmitter.ExtensionMethods;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.PermissionVerification.Middlewares;
using EasySoft.Core.PermissionVerification.Observers;
using EasySoft.Core.PermissionVerification.Officers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.PermissionVerification.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
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
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddPermissionVerification)}<{typeof(TPermissionObserver).Name}>."
        );

        builder.AddPermissionObserverInjection<TPermissionObserver>()
            .UseAccessWayTransmitter();

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