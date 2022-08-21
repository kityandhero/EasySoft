using Autofac;
using EasySoft.Core.AccessWayTransmitter.ExtensionMethods;
using EasySoft.Core.IdentityVerification.AccessControl;
using EasySoft.Core.IdentityVerification.Observers;
using EasySoft.Core.IdentityVerification.Operators;
using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.IdentityVerification.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置操作者验证以及操作权限验证    
    /// </summary> 
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAdvanceIdentityVerification<TTokenSecretOptions, TOperator,
        TPermissionObserver>(
        this WebApplicationBuilder builder
    ) where TOperator : ActualOperator
        where TPermissionObserver : IPermissionObserver
        where TTokenSecretOptions : ITokenSecretOptions
    {
        if (FlagAssist.IdentityVerificationSwitch)
        {
            throw new Exception("UseTokenSecretOptionsInjection<T> disallow inject more than once");
        }

        builder.UseTokenSecretOptionsInjection<TTokenSecretOptions>()
            .UseTokenSecretInjection<TokenSecret>()
            .UseOperatorInjection<TOperator>()
            .UsePermissionObserverInjection<TPermissionObserver>()
            .UseAccessWayTransmitter();

        FlagAssist.IdentityVerificationSwitch = true;

        return builder;
    }

    /// <summary>
    /// 配置操作者验证以及操作权限验证    
    /// </summary> 
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAdvanceIdentityVerification<TTokenSecretOptions, TTokenSecret, TOperator,
        TPermissionObserver>(
        this WebApplicationBuilder builder
    ) where TOperator : ActualOperator
        where TPermissionObserver : IPermissionObserver
        where TTokenSecretOptions : ITokenSecretOptions
        where TTokenSecret : ITokenSecret
    {
        if (FlagAssist.IdentityVerificationSwitch)
        {
            throw new Exception("UseTokenSecretOptionsInjection<T> disallow inject more than once");
        }

        builder.UseTokenSecretOptionsInjection<TTokenSecretOptions>()
            .UseTokenSecretInjection<TTokenSecret>()
            .UseOperatorInjection<TOperator>()
            .UsePermissionObserverInjection<TPermissionObserver>()
            .UseAccessWayTransmitter();

        FlagAssist.IdentityVerificationSwitch = true;

        return builder;
    }

    /// <summary>
    /// 注入操作员
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder UseOperatorInjection<T>(
        this WebApplicationBuilder builder
    ) where T : ActualOperator
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            // https://docs.autofac.org/en/latest/faq/per-request-scope.html
            containerBuilder.RegisterType<T>().As<IActualOperator>().InstancePerLifetimeScope();
        });

        return builder;
    }

    /// <summary>
    /// 注入权限判断器
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder UsePermissionObserverInjection<T>(
        this WebApplicationBuilder builder
    ) where T : IPermissionObserver
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            // https://docs.autofac.org/en/latest/faq/per-request-scope.html
            containerBuilder.RegisterType<T>().As<IPermissionObserver>().InstancePerLifetimeScope();
        });

        return builder;
    }

    /// <summary>
    /// 注入Token密钥配置, 未配置此项将使用内置密钥, 但默认密钥不安全的
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static WebApplicationBuilder UseTokenSecretOptionsInjection<T>(
        this WebApplicationBuilder builder
    ) where T : ITokenSecretOptions
    {
        if (FlagAssist.TokenSecretOptionInjectionComplete)
        {
            throw new Exception("UseTokenSecretOptionsInjection<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<ITokenSecretOptions>().SingleInstance();
        });

        FlagAssist.TokenSecretOptionInjectionComplete = true;

        return builder;
    }

    /// <summary>
    /// 注入Token加解密工具, 未配置此项将使用内置工具
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static WebApplicationBuilder UseTokenSecretInjection<T>(
        this WebApplicationBuilder builder
    ) where T : ITokenSecret
    {
        if (FlagAssist.TokenSecretInjectionComplete)
        {
            throw new Exception("UseTokenSecretInjection<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<ITokenSecret>().SingleInstance();
        });

        FlagAssist.TokenSecretInjectionComplete = true;

        return builder;
    }
}