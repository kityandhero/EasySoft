using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Hangfire.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceHangfire(this WebApplication application)
    {
        if (!HangfireConfigAssist.GetEnable())
        {
            application.RecordInformation("hangfire: disable."
            );

            return application;
        }

        //启用Hangfire面板 
        application.UseHangfireDashboard();

        application.RecordInformation("hangfire: enable, access:https://[host]:[port]/hangfire.");

        return application;
    }
}