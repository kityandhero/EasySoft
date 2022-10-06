using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.MultiTenant.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddTenantFactory(
        this WebApplicationBuilder builder,
        Func<HttpContext?, Tenant> tenantBuilder
    )
    {
        builder.Host.AddTenantFactory(tenantBuilder);

        return builder;
    }
}