﻿using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    internal static IEndpointConventionBuilder MapSwaggerConfigFile(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapSwaggerConfigFile)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get swaggerConfig template by access {FlagAssist.StartupUrls.Select(o => $"{o}/SwaggerConfigFile").Join(" ")}, it only can access in development mode."
        );

        return endpoints.MapControllerRoute(
            "SwaggerConfigFile",
            "{controller=SwaggerConfigFile}/{action=Index}"
        ).WithDisplayName("SwaggerConfigFile");
    }
}