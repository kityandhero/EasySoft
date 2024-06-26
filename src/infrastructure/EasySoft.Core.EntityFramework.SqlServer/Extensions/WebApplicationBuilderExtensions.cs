﻿namespace EasySoft.Core.EntityFramework.SqlServer.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifier = "6909f3a5-aa12-4cb4-8637-c412f4beb9af";

    /// <summary>
    ///     注册基于 SqlServer 并已集成好数据操作上下文、工作单元以及通用仓储的 EntityFramework Core 工具集
    /// </summary>  
    /// <param name="builder"></param>
    /// <param name="connectionString"></param>
    /// <param name="action"></param>
    /// <returns></returns> 
    public static WebApplicationBuilder AddAdvanceSqlServer<TContext>(
        this WebApplicationBuilder builder,
        string connectionString,
        Action<DbContextOptionsBuilder>? action = null
    ) where TContext : SqlServerContext
    {
        if (builder.HasRegistered(UniqueIdentifier))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceSqlServer)}<{typeof(TContext).Name}>."
        );

        builder.Services.AddAdvanceSqlServer<TContext>(opt =>
        {
            opt.UseSqlServer(connectionString);

            opt.UseSnakeCaseNamingConvention();

            action?.Invoke(opt);
        });

        return builder;
    }
}