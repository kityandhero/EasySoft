﻿using EasySoft.Core.Hangfire.ConfigAssist;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Hangfire.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceHangfire(this WebApplication application)
    {
        if (!HangfireConfigAssist.GetSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceHangfire)}."
        );

        //启用Hangfire面板 
        application.UseHangfireDashboard();

        StartupDescriptionMessageAssist.AddPrompt(
            $"Young can access hangfire dashboard by {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/hangfire").Join(" ")}."
        );

        if (EnvironmentAssist.IsDevelopment() && AuxiliaryConfigure.PromptConfigFileInfo)
            ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
                new ExtraAction<IEndpointRouteBuilder>()
                    .SetName("")
                    .SetAction(endpoints => { endpoints.MapHangfireConfigFile(); })
            );

        return application;
    }
}