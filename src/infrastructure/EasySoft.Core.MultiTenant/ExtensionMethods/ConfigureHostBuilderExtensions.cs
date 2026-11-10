namespace EasySoft.Core.MultiTenant.ExtensionMethods;

internal static class ConfigureHostBuilderExtensions
{
    public static ConfigureHostBuilder AddTenantFactory(
        this ConfigureHostBuilder builder,
        Func<HttpContext, Tenant> tenantBuilder
    )
    {
        builder.ConfigureServices((_, services) =>
            services.AddTenantFactory(tenantBuilder)
        );

        return builder;
    }
}