using Autofac;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.Core.EasyToken.Middlewares;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.EasyToken.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置操作者验证以及操作权限验证    
    /// </summary> 
    /// <param name="builder"></param>
    /// <param name="middlewareMode">使用中间件模式</param>
    /// <returns></returns>
    public static WebApplicationBuilder UseEasyToken<TTokenSecretOptions, TOperator>(
        this WebApplicationBuilder builder,
        bool middlewareMode = true
    ) where TOperator : ActualOperator where TTokenSecretOptions : ITokenSecretOptions
    {
        if (!string.IsNullOrWhiteSpace(FlagAssist.TokenMode))
        {
            throw new Exception("token mode disallow use more than one");
        }

        if (FlagAssist.EasyTokenConfigComplete)
        {
            throw new Exception("UseEasyToken<TTokenSecretOptions, TOperator> disallow inject more than once");
        }

        builder.UseTokenSecretOptionsInjection<TTokenSecretOptions>()
            .UseTokenSecretInjection<TokenSecret>()
            .UseOperatorInjection<TOperator>();

        if (middlewareMode)
        {
            builder.Services.AddTransient<EasyTokenMiddleware>();

            FlagAssist.EasyTokenMiddlewareModeSwitch = true;
        }

        FlagAssist.EasyTokenConfigComplete = true;

        FlagAssist.TokenMode = ConstCollection.EasyToken;

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    "EasyToken already available, you can config it with config file."
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        return builder;
    }

    /// <summary>
    /// 配置操作者验证以及操作权限验证    
    /// </summary> 
    /// <param name="builder"></param>
    /// <param name="middlewareMode"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseEasyToken<TTokenSecretOptions, TTokenSecret, TOperator>(
        this WebApplicationBuilder builder,
        bool middlewareMode = true
    ) where TOperator : ActualOperator
        where TTokenSecretOptions : ITokenSecretOptions
        where TTokenSecret : ITokenSecret
    {
        if (FlagAssist.EasyTokenConfigComplete)
        {
            throw new Exception("UseEasyToken<TTokenSecretOptions, TOperator> disallow inject more than once");
        }

        builder.UseTokenSecretOptionsInjection<TTokenSecretOptions>()
            .UseTokenSecretInjection<TTokenSecret>()
            .UseOperatorInjection<TOperator>();

        if (middlewareMode)
        {
            builder.Services.AddTransient<EasyTokenMiddleware>();

            FlagAssist.EasyTokenMiddlewareModeSwitch = true;
        }

        FlagAssist.EasyTokenConfigComplete = true;

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
        if (FlagAssist.EasyTokenSecretOptionInjectionComplete)
        {
            throw new Exception("UseTokenSecretOptionsInjection<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<ITokenSecretOptions>().SingleInstance();
        });

        FlagAssist.EasyTokenSecretOptionInjectionComplete = true;

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
        if (FlagAssist.EasyTokenSecretInjectionComplete)
        {
            throw new Exception("UseTokenSecretInjection<T> disallow inject more than once");
        }

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<T>().As<ITokenSecret>().SingleInstance();
        });

        FlagAssist.EasyTokenSecretInjectionComplete = true;

        return builder;
    }
}