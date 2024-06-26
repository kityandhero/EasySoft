﻿using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.NLog.ExtensionMethods;

public static class EndpointConventionBuilderExtensions
{
    internal static IEndpointConventionBuilder MapNLogInlayConfig(
        this IEndpointRouteBuilder endpoints
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(MapNLogInlayConfig)}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can get nLogInlayConfig template by access {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/NLogInlayConfig").Join(" ")}, it only can access in development mode."
        );

        return endpoints.MapControllerRoute(
            "NLogInlayConfig",
            "{controller=NLogInlayConfig}/{action=Index}"
        ).WithDisplayName("NLogInlayConfig");
    }
}