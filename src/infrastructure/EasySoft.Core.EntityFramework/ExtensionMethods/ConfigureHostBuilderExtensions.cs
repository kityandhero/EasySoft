using Autofac;
using EasySoft.Core.EntityFramework.Contexts.Basic;
using EasySoft.Core.EntityFramework.Contexts.ContextFactories;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.EntityFramework.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    public static ConfigureHostBuilder AddAdvanceTenantContextFactory<TFactory, T>(
        this ConfigureHostBuilder builder
    ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : TenantBasicContext
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AddAdvanceTenantContextFactory<TFactory, T>();
        });

        return builder;
    }

    public static ConfigureHostBuilder AddAdvanceTenantContext<TFactory, T>(
        this ConfigureHostBuilder builder
    ) where TFactory : AdvanceTenantContextFactory<T>, new() where T : TenantBasicContext
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AddAdvanceTenantContext<TFactory, T>();
        });

        return builder;
    }
}