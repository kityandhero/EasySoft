﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.ConsulRegistrationCenterClient.ExtensionMethods;

internal static class EndpointConventionBuilderExtensions
{
    internal static IEndpointConventionBuilder MapConsulHealthCheck(
        this IEndpointRouteBuilder endpoints
    )
    {
        return endpoints.MapControllerRoute(
            "ConsulServiceHealth",
            "{controller=ConsulServiceHealth}/{action=Index}"
        ).WithDisplayName(ConstCollection.ConsulServiceHealthEndpointName);
    }
}