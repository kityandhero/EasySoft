using EasySoft.Core.Infrastructure.Assists;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.HealthChecks.ExtensionMethods;

public static class EndpointRouteBuilderExtensions
{
    public static void UseAdvanceHealthChecks(
        this IEndpointRouteBuilder endpointRouteBuilder
    )
    {
        if (!FlagAssist.GetHealthChecksSwitch())
        {
            return;
        }

        endpointRouteBuilder.MapHealthChecks(
            ConstCollection.HealthChecksEndpoint,
            new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            }
        );

        endpointRouteBuilder.MapHealthChecksUI();
    }
}