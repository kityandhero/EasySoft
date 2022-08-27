using EasySoft.Core.HealthChecks.Entities;
using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.HealthChecks.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceHealthChecks(
        this WebApplicationBuilder builder,
        IEnumerable<IAdvanceHealthCheck> healthCheckList
    )
    {
        if (FlagAssist.HealthChecksComplete)
        {
            throw new Exception("AddAdvanceHealthChecks disallow inject more than once");
        }

        var healthChecksBuilder = builder.Services.AddHealthChecks();

        foreach (var item in healthCheckList)
        {
            healthChecksBuilder.AddCheck(
                item.GetName(),
                item.GetCheckAction(),
                item.GetTags(),
                item.GetTimeout()
            );
        }

        FlagAssist.HealthChecksSwitch = true;
        FlagAssist.HealthChecksComplete = true;

        builder.Services.AddHealthChecksUI(settings =>
        {
            settings.AddHealthCheckEndpoint("internal", ConstCollection.HealthChecksEndpoint);
        });

        return builder;
    }
}