﻿using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.ExtensionMethods;

namespace EasySoft.Core.MiniProfiler.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceMiniProfile = "0087964a-ea50-4f11-9f85-35ee4146de53";

    public static WebApplicationBuilder AddAdvanceMiniProfile(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceMiniProfile))
            return builder;

        if (!GeneralConfigAssist.GetMiniProFileSwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "MiniProFileSwitch : disable.",
                GeneralConfigAssist.GetConfigFileInfo()
            );

            return builder;
        }

        StartupDescriptionMessageAssist.AddTraceDivider();

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceMiniProfile)}."
        );

        StartupConfigMessageAssist.AddConfig(
            "MiniProFileSwitch : enable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddMiniProfiler(options =>
        {
            // 设定弹出窗口的位置是左下角
            options.PopupRenderPosition = RenderPosition.BottomLeft;

            // 设定在弹出的明细窗口里会显式 Time With Children 这列
            options.PopupShowTimeWithChildren = true;

            ApplicationConfigurator.GetMiniProfilerOptionsAction()?.Invoke(options);
        });

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(
                    application => { application.UseMiniProfiler(); }
                )
        );

        StartupDescriptionMessageAssist.AddPrompt(
            "You can add \"@using StackExchange.Profiling\" and \"@addTagHelper *, MiniProfiler.AspNetCore.Mvc\" to the _Layout.cshtml and add <mini-profiler /> to the target view to show analysis information."
        );

        return builder;
    }
}