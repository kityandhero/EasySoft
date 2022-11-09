using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using LogDashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.LogDashboard.ExtensionMethods;

public static class WebApplicationExtensions
{
    internal static WebApplication UseAdvanceLogDashboard(this WebApplication application)
    {
        if (!FlagAssist.GetLogDashboardSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceLogDashboard)}()."
        );

        if (EnvironmentAssist.GetEnvironment().IsDevelopment()) application.UseDeveloperExceptionPage();

        application.UseLogDashboard();

        StartupConfigMessageAssist.AddConfig(
            $"LogDashboard enable."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/LogDashboard" : FlagAssist.StartupUrls.Select(o => $"{o}/LogDashboard").Join(" "))} to visit LogDashboard."
        );

        return application;
    }
}