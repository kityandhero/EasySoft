using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Hangfire.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceHangfire(this WebApplication application)
    {
        if (!HangfireConfigAssist.GetEnable())
        {
            StartupNormalMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "Hangfire: disable."
                    )
            );

            return application;
        }

        //启用Hangfire面板 
        application.UseHangfireDashboard();

        StartupNormalMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"Hangfire: enable, access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/hangfire" : FlagAssist.StartupUrls.Select(o => $"{o}/hangfire").Join(" "))}."
                )
        );

        return application;
    }
}