using EasySoft.Core.EntityFramework.Contexts.ContextFactories;

namespace EasySoft.Core.EntityFramework.Extensions;

/// <summary>
/// ContainerBuilderExtensions
/// </summary>
public static class ContainerBuilderExtensions
{
    /// <summary>
    /// AddAdvanceTenantContextFactory
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TFactory"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ConfigureHostBuilder AddAdvanceTenantContextFactory<TFactory, T>(
        this ConfigureHostBuilder builder
    )
        where TFactory : AdvanceTenantContextFactory<T>, new()
        where T : TenantBasicContext
    {
        builder.ConfigureServices((_, services) =>
            services.AddAdvanceTenantContextFactory<TFactory, T>()
        );

        return builder;
    }
}