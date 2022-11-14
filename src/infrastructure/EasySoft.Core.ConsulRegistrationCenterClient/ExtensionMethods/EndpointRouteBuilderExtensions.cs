namespace EasySoft.Core.ConsulRegistrationCenterClient.ExtensionMethods;

internal static class EndpointConventionBuilderExtensions
{
    internal static IEndpointConventionBuilder MapConsulHealthCheck(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapConsulHealthCheck)}."
        );

        return endpoints.MapControllerRoute(
            "ConsulServiceHealth",
            "{controller=ConsulServiceHealth}/{action=Index}"
        ).WithDisplayName(ConstCollection.ConsulServiceHealthEndpointName);
    }
}