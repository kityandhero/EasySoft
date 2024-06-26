﻿using EasySoft.Core.HealthChecks.Entities;
using EasySoft.Core.Infrastructure.Configures;

namespace EasySoft.Core.HealthChecks.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceHealthChecks = "53245823-9262-4b3d-a1ae-321f67cf04e0";

    public static WebApplicationBuilder AddAdvanceHealthChecks(
        this WebApplicationBuilder builder,
        IEnumerable<IAdvanceHealthCheck> healthCheckList
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceHealthChecks))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceHealthChecks)}."
        );

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

        builder.Services.AddHealthChecksUI(
                settings =>
                {
                    settings.AddHealthCheckEndpoint("internal", ConstCollection.HealthChecksEndpoint);

                    settings.SetEvaluationTimeInSeconds(10);
                    settings.SetMinimumSecondsBetweenFailureNotifications(60);
                }
            )
            .AddInMemoryStorage();

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpointRouteBuilder => { endpointRouteBuilder.UseAdvanceHealthChecks(); })
        );

        StartupConfigMessageAssist.AddConfig(
            $"HealthChecks: enable, you can access {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/HealthChecks-ui").Join(" ")} to visit it."
        );

        return builder;
    }
}