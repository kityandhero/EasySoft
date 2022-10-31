using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.MultiTenant.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddTenantFactory(
        this WebApplicationBuilder builder,
        Func<HttpContext?, Tenant> tenantBuilder
    )
    {
        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Debug)
                .SetMessage(
                    $"Execute {nameof(AddTenantFactory)}()."
                )
        );

        builder.Host.AddTenantFactory(tenantBuilder);

        return builder;
    }
}