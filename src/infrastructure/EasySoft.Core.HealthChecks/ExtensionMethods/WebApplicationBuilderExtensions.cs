using EasySoft.Core.HealthChecks.Entities;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.HealthChecks.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceHealthChecks(
        this WebApplicationBuilder builder,
        IEnumerable<IAdvanceHealthCheck> healthCheckList
    )
    {
        if (FlagAssist.GetHealthChecksSwitch())
        {
            return builder;
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

        FlagAssist.SetHealthChecksSwitchOpen();

        builder.Services.AddHealthChecksUI(settings =>
        {
            settings.AddHealthCheckEndpoint("internal", ConstCollection.HealthChecksEndpoint);

            settings.SetEvaluationTimeInSeconds(10);
            settings.SetMinimumSecondsBetweenFailureNotifications(60);
        }).AddInMemoryStorage();

        ApplicationConfigActionAssist.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpointRouteBuilder => { endpointRouteBuilder.UseAdvanceHealthChecks(); })
        );

        StartupNormalMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"HealthChecks: enable{(!FlagAssist.StartupUrls.Any() ? "." : $", you can access {FlagAssist.StartupUrls.Select(o => $"{o}/HealthChecks-ui").Join(" ")}")} to visit it."
                )
        );

        return builder;
    }
}