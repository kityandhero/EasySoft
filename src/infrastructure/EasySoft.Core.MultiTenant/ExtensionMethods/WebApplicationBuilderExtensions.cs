using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.MultiTenant.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddTenantFactory(
        this WebApplicationBuilder builder,
        Func<HttpContext?, Tenant> tenantBuilder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddTenantFactory)}."
        );

        builder.Host.AddTenantFactory(tenantBuilder);

        return builder;
    }
}