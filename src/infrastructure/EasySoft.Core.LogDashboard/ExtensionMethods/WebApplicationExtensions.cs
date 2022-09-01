﻿using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using LogDashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.LogDashboard.ExtensionMethods;

public static class WebApplicationExtensions
{
    internal static WebApplication UseAdvanceLogDashboard(this WebApplication application)
    {
        if (!FlagAssist.GetLogDashboardSwitch())
        {
            return application;
        }

        if (EnvironmentAssist.GetEnvironment().IsDevelopment())
        {
            application.UseDeveloperExceptionPage();
        }

        application.UseLogDashboard();

        StartupNormalMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"LogDashboard enable, access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/LogDashboard" : FlagAssist.StartupUrls.Select(o => $"{o}/LogDashboard").Join(" "))}."
                )
        );

        return application;
    }
}