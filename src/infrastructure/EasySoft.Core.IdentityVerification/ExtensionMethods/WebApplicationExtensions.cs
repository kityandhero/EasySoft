using Autofac;
using EasySoft.Core.IdentityVerification.AccessControl;
using EasySoft.Core.Infrastructure.CommonAssists;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.IdentityVerification.ExtensionMethods;

public static class WebApplicationExtensions
{
    /// <summary>
    /// 注入Token密钥配置, 未配置此项将使用内置密钥, 但默认密钥不安全的
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder UseTokenSecretOptionsInjection<T>(
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
        FlagAssist.TokenSecretOptionIsDefault = false;

        return builder;
    }

    /// <summary>
    /// 注入Token密钥配置
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseDefaultTokenSecretOptionsInjection(
        this WebApplicationBuilder builder
    )
    {
        if (FlagAssist.TokenSecretOptionInjectionComplete)
        {
            throw new Exception("UseTokenSecretOptionsInjection<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<DefaultTokenSecretOptions>().As<ITokenSecretOptions>().SingleInstance();
        });

        FlagAssist.TokenSecretOptionInjectionComplete = true;
        FlagAssist.TokenSecretOptionIsDefault = true;

        return builder;
    }

    /// <summary>
    /// 注入Token加解密工具, 未配置此项将使用内置工具
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder UseTokenSecretInjection<T>(
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

    /// <summary>
    /// 注入默认Token密钥配置
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>  
    public static WebApplicationBuilder UseDefaultTokenSecret(
        this WebApplicationBuilder builder
    )
    {
        if (FlagAssist.TokenSecretInjectionComplete)
        {
            throw new Exception("UseDefaultTokenSecret<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<TokenSecret>().As<ITokenSecret>().SingleInstance();
        });

        return builder;
    }
}