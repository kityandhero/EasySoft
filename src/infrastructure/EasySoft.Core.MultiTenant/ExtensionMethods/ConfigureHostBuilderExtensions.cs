using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.MultiTenant.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    public static ConfigureHostBuilder AddTenantFactory(
        this ConfigureHostBuilder builder,
        Func<HttpContext?, Tenant> tenantBuilder
    )
    {
        builder.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.AddTenantFactory(tenantBuilder);
        });

        return builder;
    }
}