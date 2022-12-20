using EasySoft.Core.EntityFramework.EntityConfigures.Implementations;
using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.EntityFramework.Repositories;

namespace EasySoft.Core.EntityFramework.Extensions;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
public static class ServiceCollectionExtension
{
    private const string UniqueIdentifierAddAdvanceContext = "b8f6139c-9e5e-41ff-8c4b-2e81ed46548f";

    private const string UniqueIdentifierAddAdvanceContextPool = "3c843d66-f55c-42c8-b2b0-c7f7fe0d2dae";

    private const string UniqueIdentifierAddAdvanceRepository = "fd5288c0-e788-43f8-94a5-86b4a78fbcba";

    private const string UniqueIdentifierAddEntityConfigure = "1d68a6d0-470d-42b9-81df-898649c54c85";

    /// <summary>
    /// AddAdvanceContext
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IServiceCollection AddAdvanceContext<T>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> action
    ) where T : BasicContext
    {
        if (services.HasRegistered(UniqueIdentifierAddAdvanceContextPool))
            throw new Exception("AddAdvanceContext and AddAdvanceContextPool do not use at the same time");

        if (services.HasRegistered(UniqueIdentifierAddAdvanceContext))
            return services;

        services.AddDbContext<DbContext, T>((serviceProvider, optionsBuilder) =>
        {
            var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            if (GeneralConfigAssist.GetRemoteSqlExecutionRecordSwitch())
            {
                var applicationChannel = serviceProvider.GetRequiredService<IApplicationChannel>();

                optionsBuilder.LogTo(message =>
                {
                    SqlLogInnerQueue.Enqueue(
                        new SqlExecutionRecordExchange
                        {
                            CommandString = message,
                            ExecuteType = SqlExecuteType.EntityFramework.ToInt(),
                            Channel = applicationChannel.GetChannel()
                        }
                    );
                });
            }

            if (environment.IsDevelopment())
            {
                optionsBuilder.UseLoggerFactory(serviceProvider.GetService<ILoggerFactory>());

                if (ContextConfigure.EnableSensitiveDataLogging)
                {
                    LogAssist.Hint(
                        $"{typeof(ContextConfigure).FullName}.{nameof(ContextConfigure.EnableSensitiveDataLogging)} is {ContextConfigure.EnableSensitiveDataLogging}."
                    );

                    //敏感数据日志
                    //仅应在开发环境下开启
                    optionsBuilder.EnableSensitiveDataLogging();
                }

                if (ContextConfigure.EnableDetailedErrors)
                {
                    LogAssist.Hint(
                        $"{typeof(ContextConfigure).FullName}.{nameof(ContextConfigure.EnableDetailedErrors)} is {ContextConfigure.EnableDetailedErrors}."
                    );

                    //出于性能原因，EF Core 不会在 try-catch 块中包装每个调用以从数据库提供程序读取值。 但是，这有时会导致难以诊断的异常，尤其是当数据库在模型不允许的情况下返回 NULL 时
                    //仅应在开发环境下开启
                    optionsBuilder.EnableDetailedErrors();
                }

                if (ContextConfigure.EnableSensitiveDataLogging)
                    LogAssist.Prompt(
                        $"{typeof(ContextConfigure).FullName}.{nameof(ContextConfigure.EnableSensitiveDataLogging)} only takes effect in development mode."
                    );

                LogAssist.Hint(
                    $"{typeof(ContextConfigure).FullName}.{nameof(ContextConfigure.GetEntityConfigureAssemblies)} contain {(!ContextConfigure.GetEntityConfigureAssemblies().Any() ? "none" : ContextConfigure.GetEntityConfigureAssemblies().Select(o => o.GetName().Name).Join(","))}."
                );
            }

            action(optionsBuilder);
        });

        return services;
    }

    /// <summary>
    ///     AddAdvanceContextPool, 与工作单元的协同性未做测试，暂不开放
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static IServiceCollection AddAdvanceContextPool<T>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> action
    ) where T : BasicContext
    {
        if (services.HasRegistered(UniqueIdentifierAddAdvanceContext))
            throw new Exception("AddAdvanceContext and AddAdvanceContextPool do not use at the same time");

        if (services.HasRegistered(UniqueIdentifierAddAdvanceContextPool))
            return services;

        services.AddDbContextPool<T>((serviceProvider, optionsBuilder) =>
        {
            if (EnvironmentAssist.GetEnvironment().IsDevelopment())
            {
                optionsBuilder.UseLoggerFactory(serviceProvider.GetService<ILoggerFactory>());

                if (ContextConfigure.EnableSensitiveDataLogging)
                    //敏感数据日志
                    //仅应在开发环境下开启
                    optionsBuilder.EnableSensitiveDataLogging();

                if (ContextConfigure.EnableDetailedErrors)
                    //出于性能原因，EF Core 不会在 try-catch 块中包装每个调用以从数据库提供程序读取值。 但是，这有时会导致难以诊断的异常，尤其是当数据库在模型不允许的情况下返回 NULL 时
                    //仅应在开发环境下开启
                    optionsBuilder.EnableDetailedErrors();
            }

            action(optionsBuilder);
        });

        return services;
    }

    /// <summary>
    /// AddAdvanceRepository
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAdvanceRepository(
        this IServiceCollection services
    )
    {
        if (services.HasRegistered(UniqueIdentifierAddAdvanceRepository))
            return services;

        services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    /// <summary>
    /// AddEntityConfigure
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddEntityConfigure(
        this IServiceCollection services
    )
    {
        if (services.HasRegistered(UniqueIdentifierAddEntityConfigure))
            return services;

        services.TryAddSingleton(
            typeof(IEntityConfigure),
            _ =>
            {
                var entityConfigureAssemblies = ContextConfigure.GetEntityConfigureAssemblies();

                if (!entityConfigureAssemblies.Any())
                    LogAssist.Warning("ContextConfigure.EntityConfigureAssemblies has none Assemblies");

                var entityConfigure = new EntityConfigure().AddRangeAssemblies(
                    entityConfigureAssemblies
                );

                return entityConfigure;
            }
        );

        return services;
    }
}