using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;

namespace EasySoft.Core.MiniProfiler.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceMiniProfile(
        this WebApplicationBuilder builder
    )
    {
        if (!GeneralConfigAssist.GetMiniProFileSwitch())
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        "MiniProFileSwitch : disable."
                    )
                    .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
            );

            return builder;
        }

        if (FlagAssist.GetMiniProfileSwitch())
        {
            return builder;
        }

        StartupConfigMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    "MiniProFileSwitch : enable."
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
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

        FlagAssist.SetMiniProfileSwitchOpen();

        StartupDescriptionMessageAssist.Add(
            new StartupMessage().SetMessage(
                "You can add \"@using StackExchange.Profiling\" and \"@addTagHelper *, MiniProfiler.AspNetCore.Mvc\" to the _Layout.cshtml and add <mini-profiler /> to the target view to show analysis information."
            )
        );

        return builder;
    }
}