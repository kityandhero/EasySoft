using EasySoft.Core.EntityFramework.Configures;
using EasySoft.Core.EntityFramework.Contexts.Basic;
using EasySoft.Core.EntityFramework.Contexts.ContextFactories;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FlagAssist = EasySoft.Core.EntityFramework.Assists.FlagAssist;

namespace EasySoft.Core.EntityFramework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// If available, use AddAdvanceContextPool first,AddAdvanceContextPool has higher performance.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceContext<T>(
        this WebApplicationBuilder builder,
        Action<DbContextOptionsBuilder> action
    ) where T : AdvanceContextBase
    {
        if (FlagAssist.GetEntityFrameworkSwitch())
        {
            return builder;
        }

        builder.Services.AddDbContext<T>((serviceProvider, optionsBuilder) =>
        {
            if (EnvironmentAssist.GetEnvironment().IsDevelopment())
            {
                optionsBuilder.UseLoggerFactory(serviceProvider.GetService<ILoggerFactory>());

                if (ContextConfigure.EnableSensitiveDataLogging)
                {
                    //敏感数据日志
                    //仅应在开发环境下开启
                    optionsBuilder.EnableSensitiveDataLogging();
                }

                if (ContextConfigure.EnableDetailedErrors)
                {
                    //出于性能原因，EF Core 不会在 try-catch 块中包装每个调用以从数据库提供程序读取值。 但是，这有时会导致难以诊断的异常，尤其是当数据库在模型不允许的情况下返回 NULL 时
                    //仅应在开发环境下开启
                    optionsBuilder.EnableDetailedErrors();
                }
            }

            action(optionsBuilder);
        });

        FlagAssist.SetEntityFrameworkSwitchOpen();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseAdvanceMigrationsEndPoint(); })
        );

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceContextPool<T>(
        this WebApplicationBuilder builder,
        Action<DbContextOptionsBuilder> action
    ) where T : AdvanceContextBase
    {
        if (FlagAssist.GetEntityFrameworkSwitch())
        {
            return builder;
        }

        builder.Services.AddDbContextPool<T>(action);

        FlagAssist.SetEntityFrameworkSwitchOpen();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseAdvanceMigrationsEndPoint(); })
        );

        return builder;
    }

    public static WebApplicationBuilder AddPooledAdvanceTenantContext<TFactory, T>(
        this WebApplicationBuilder builder,
        Action<DbContextOptionsBuilder> action
    ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : AdvanceTenantContextBase
    {
        if (FlagAssist.GetEntityFrameworkSwitch())
        {
            return builder;
        }

        builder.Services.AddPooledDbContextFactory<T>(action);

        builder.AddAdvanceTenantContextFactory<TFactory, T>();

        builder.AddAdvanceTenantContext<TFactory, T>();

        FlagAssist.SetEntityFrameworkSwitchOpen();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseAdvanceMigrationsEndPoint(); })
        );

        return builder;
    }

    private static WebApplicationBuilder AddAdvanceTenantContextFactory<TFactory, T>(
        this WebApplicationBuilder builder
    ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : AdvanceTenantContextBase
    {
        builder.Host.AddAdvanceTenantContextFactory<TFactory, T>();

        return builder;
    }

    private static WebApplicationBuilder AddAdvanceTenantContext<TFactory, T>(
        this WebApplicationBuilder builder
    ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : AdvanceTenantContextBase
    {
        builder.Host.AddAdvanceTenantContext<TFactory, T>();

        return builder;
    }
}