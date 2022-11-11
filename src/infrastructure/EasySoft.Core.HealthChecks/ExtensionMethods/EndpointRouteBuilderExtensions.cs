namespace EasySoft.Core.HealthChecks.ExtensionMethods;

public static class EndpointRouteBuilderExtensions
{
    internal static void UseAdvanceHealthChecks(
        this IEndpointRouteBuilder endpointRouteBuilder
    )
    {
        endpointRouteBuilder.MapHealthChecks(
            ConstCollection.HealthChecksEndpoint,
            new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }
        );

        endpointRouteBuilder.MapHealthChecksUI();
    }
}