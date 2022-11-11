using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.ConsulRegistrationCenterClient.ExtensionMethods;

internal static class EndpointConventionBuilderExtensions
{
    internal static IEndpointConventionBuilder MapConsulHealthCheck(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            nameof(MapConsulHealthCheck)
        );

        return endpoints.MapControllerRoute(
            "ConsulServiceHealth",
            "{controller=ConsulServiceHealth}/{action=Index}"
        ).WithDisplayName(ConstCollection.ConsulServiceHealthEndpointName);
    }
}