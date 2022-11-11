using EasySoft.Core.EntityFramework.Contexts.ContextFactories;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.EntityFramework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    // private const string UniqueIdentifierAddAdvanceContext = "3964d988-0ba6-44f4-90ba-cc5ae17a0a05";
    //
    // private const string UniqueIdentifierAddAdvanceContextPool = "494c3ace-765a-45f4-b798-7a29d76847b1";
    //
    // private const string UniqueIdentifierAddPooledAdvanceTenantContext = "bb87108a-a604-47e3-9166-60f424451c80";

    // /// <summary>
    // ///     AddAdvanceContext
    // /// </summary>
    // /// <param name="builder"></param>
    // /// <param name="action"></param>
    // /// <typeparam name="T"></typeparam>
    // /// <returns></returns> 
    // public static WebApplicationBuilder AddAdvanceContext<T>(
    //     this WebApplicationBuilder builder,
    //     Action<DbContextOptionsBuilder> action
    // ) where T : BasicContext
    // {
    //     if (builder.HasRegistered(UniqueIdentifierAddAdvanceContextPool))
    //         throw new Exception("AddAdvanceContext and AddAdvanceContextPool do not use at the same time");
    //
    //     if (builder.HasRegistered(UniqueIdentifierAddAdvanceContext))
    //         return builder;
    //
    //     StartupDescriptionMessageAssist.AddExecute(
    //         $"{nameof(AddAdvanceContext)}<{typeof(T).Name}>."
    //     );
    //
    //     builder.Services.AddAdvanceContext<T>(action);
    //
    //     ApplicationConfigurator.AddWebApplicationExtraAction(
    //         new ExtraAction<WebApplication>()
    //             .SetName("")
    //             .SetAction(application => { application.UseAdvanceMigrationsEndPoint(); })
    //     );
    //
    //     return builder;
    // }
    //
    // /// <summary>
    // ///     AddAdvanceContextPool, 与工作单元的协同性未做测试，暂不开放
    // /// </summary>
    // /// <param name="builder"></param>
    // /// <param name="action"></param>
    // /// <typeparam name="T"></typeparam>
    // /// <returns></returns>
    // /// <exception cref="Exception"></exception>
    // internal static WebApplicationBuilder AddAdvanceContextPool<T>(
    //     this WebApplicationBuilder builder,
    //     Action<DbContextOptionsBuilder> action
    // ) where T : BasicContext
    // {
    //     if (builder.HasRegistered(UniqueIdentifierAddAdvanceContext))
    //         throw new Exception("AddAdvanceContext and AddAdvanceContextPool do not use at the same time");
    //
    //     if (builder.HasRegistered(UniqueIdentifierAddAdvanceContextPool))
    //         return builder;
    //
    //     StartupDescriptionMessageAssist.AddExecute(
    //         $"{nameof(AddAdvanceContextPool)}<{typeof(T).Name}>."
    //     );
    //
    //     builder.Services.AddAdvanceContextPool<T>(action);
    //
    //     ApplicationConfigurator.AddWebApplicationExtraAction(
    //         new ExtraAction<WebApplication>()
    //             .SetName("")
    //             .SetAction(application => { application.UseAdvanceMigrationsEndPoint(); })
    //     );
    //
    //     return builder;
    // }
    //
    // /// <summary>
    // ///     AddPooledAdvanceTenantContext, 与工作单元的协同性未做测试，暂不开放
    // /// </summary>
    // /// <param name="builder"></param>
    // /// <param name="action"></param>
    // /// <typeparam name="TFactory"></typeparam>
    // /// <typeparam name="T"></typeparam>
    // /// <returns></returns>
    // internal static WebApplicationBuilder AddPooledAdvanceTenantContext<TFactory, T>(
    //     this WebApplicationBuilder builder,
    //     Action<DbContextOptionsBuilder> action
    // ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : TenantBasicContext
    // {
    //     if (builder.HasRegistered(UniqueIdentifierAddPooledAdvanceTenantContext))
    //         return builder;
    //
    //     StartupDescriptionMessageAssist.AddExecute(
    //         $"{nameof(AddPooledAdvanceTenantContext)}<{typeof(TFactory).Name},{typeof(T).Name}>."
    //     );
    //
    //     builder.Services.AddPooledDbContextFactory<T>(action);
    //
    //     builder.AddAdvanceTenantContextFactory<TFactory, T>()
    //         .AddAdvanceTenantContext<TFactory, T>();
    //
    //     ApplicationConfigurator.AddWebApplicationExtraAction(
    //         new ExtraAction<WebApplication>()
    //             .SetName("")
    //             .SetAction(application => { application.UseAdvanceMigrationsEndPoint(); })
    //     );
    //
    //     return builder;
    // }
    //
    // private static WebApplicationBuilder AddAdvanceTenantContextFactory<TFactory, T>(
    //     this WebApplicationBuilder builder
    // ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : TenantBasicContext
    // {
    //     StartupDescriptionMessageAssist.AddExecute(
    //         $"{nameof(AddAdvanceTenantContextFactory)}<{typeof(TFactory).Name},{typeof(T).Name}>."
    //     );
    //
    //     builder.Host.AddAdvanceTenantContextFactory<TFactory, T>();
    //
    //     return builder;
    // }
    //
    // private static WebApplicationBuilder AddAdvanceTenantContext<TFactory, T>(
    //     this WebApplicationBuilder builder
    // ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : TenantBasicContext
    // {
    //     StartupDescriptionMessageAssist.AddExecute(
    //         $"{nameof(AddAdvanceTenantContext)}<{typeof(TFactory).Name},{typeof(T).Name}>."
    //     );
    //
    //     builder.Host.AddAdvanceTenantContext<TFactory, T>();
    //
    //     return builder;
    // }
}