using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Hangfire.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceHangfire(this WebApplication application)
    {
        if (!HangfireConfigAssist.GetEnable())
        {
            StartupConfigMessageAssist.AddConfig(
                "Hangfire: disable."
            );

            return application;
        }

        //启用Hangfire面板 
        application.UseHangfireDashboard();

        StartupConfigMessageAssist.AddConfig(
            $"Hangfire: enable, access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/hangfire" : FlagAssist.StartupUrls.Select(o => $"{o}/hangfire").Join(" "))}."
        );

        return application;
    }
}