using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Entities;
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
            StartupMessage.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message = "Hangfire: disable."
            });

            return application;
        }

        //启用Hangfire面板 
        application.UseHangfireDashboard();

        

        StartupMessage.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message =
                $"Hangfire: enable, access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/hangfire" : FlagAssist.StartupUrls.Select(o => $"{o}/hangfire").Join(" "))}."
        });

        return application;
    }
}