﻿using EasySoft.Core.AccessWayTransmitter.Extensions;
using EasySoft.Core.PermissionVerification.Clients;
using EasySoft.Core.PermissionVerification.Detectors;
using EasySoft.Core.PermissionVerification.Detectors.Implements;
using EasySoft.Core.PermissionVerification.Detectors.Interfaces;
using EasySoft.Core.PermissionVerification.Middlewares;
using EasySoft.Core.PermissionVerification.Observers;
using EasySoft.Core.PermissionVerification.Officers;
using EasySoft.Core.PermissionVerification.Officers.Implements;
using EasySoft.Core.PermissionVerification.Officers.Interfaces;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.PermissionVerification.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddPermissionVerification = "d3dd59e3-0a28-488f-b434-9db031e5c66f";

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
        if (builder.HasRegistered(IdentifierAddPermissionVerification))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddPermissionVerification)}<{typeof(TPermissionObserver).Name}>."
        );

        builder.AddPermissionObserverInjection<TPermissionObserver>()
            .AddAccessWayTransmitter();

        builder.Services.TryAddSingleton(new ProxyGenerator());

        StartupDescriptionMessageAssist.AddPrompt(
            $"Permission server host url is {GeneralConfigAssist.GetPermissionServerHostUrl()}."
        );

        builder.AddAdvanceRefitClient<IPermissionClient>(clientBuilder =>
        {
            clientBuilder.ConfigureHttpClient(client =>
            {
                if (!Uri.TryCreate(
                        GeneralConfigAssist.GetPermissionServerHostUrl(),
                        UriKind.Absolute,
                        out var baseAddress
                    ))
                    throw new UnknownException("PermissionServerHostUrl error");

                client.BaseAddress = baseAddress;
            });
        });

        builder.Services.AddTransient(
            typeof(IAccessWayDetector),
            provider =>
            {
                var permissionClient = provider.GetRequiredService<IPermissionClient>();

                var interceptors = new List<Type> { typeof(LogRecordInterceptor) }
                    .ConvertAll(interceptorType => provider.GetService(interceptorType) as IInterceptor)
                    .ToArray();
                var proxyGenerator = provider.GetService<ProxyGenerator>();

                if (proxyGenerator == null)
                    throw new UnknownException(
                        "provider.GetService<ProxyGenerator>() result is null"
                    );

                var proxy = proxyGenerator.CreateInterfaceProxyWithTargetInterface(
                    typeof(IAccessWayDetector),
                    new AccessWayDetector(permissionClient),
                    interceptors
                );

                return proxy;
            }
        );

        builder.Services.AddTransient(
            typeof(ICompetenceDetector),
            provider =>
            {
                var permissionClient = provider.GetRequiredService<IPermissionClient>();

                var interceptors = new List<Type> { typeof(LogRecordInterceptor) }
                    .ConvertAll(interceptorType => provider.GetService(interceptorType) as IInterceptor)
                    .ToArray();
                var proxyGenerator = provider.GetService<ProxyGenerator>();

                if (proxyGenerator == null)
                    throw new UnknownException(
                        "provider.GetService<ProxyGenerator>() result is null"
                    );

                var proxy = proxyGenerator.CreateInterfaceProxyWithTargetInterface(
                    typeof(ICompetenceDetector),
                    new CompetenceDetector(permissionClient),
                    interceptors
                );

                return proxy;
            }
        );

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

        builder.Services.AddMediatR(typeof(IAccessWayDetector).Assembly);

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(
                    application => { application.UseScanPermission(); }
                )
        );

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