using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.MiniProfiler.Assists;

namespace EasySoft.Core.MiniProfiler.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceMiniProfile = "0087964a-ea50-4f11-9f85-35ee4146de53";

    /// <summary>
    /// 注入 MiniProfile
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceMiniProfile(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceMiniProfile))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceMiniProfile)}."
        );

        builder.Services.AddSingleton(new MiniProFileConfigureAssist(builder.Configuration));

        var miniProFileConfigValue = MiniProFileConfigureAssist.GetMiniProFile(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            miniProFileConfigValue.ToArrowDescription()
        );

        if (!miniProFileConfigValue.Value)
        {
            return builder;
        }

        builder.Services
            .AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler";

                    // 设定弹出窗口的位置是左下角
                    options.PopupRenderPosition = RenderPosition.BottomLeft;

                    // 设定在弹出的明细窗口里会显式 Time With Children 这列
                    options.PopupShowTimeWithChildren = true;

                    options.ColorScheme = ColorScheme.Auto;

                    ApplicationConfigure.GetMiniProfilerOptionsAction()?.Invoke(options);
                }
            )
            .AddEntityFramework();

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseMiniProfiler(); }
                )
        );

        StartupDescriptionMessageAssist.AddPrompt(
            "You can add \"@using StackExchange.Profiling\" and \"@addTagHelper *, MiniProfiler.AspNetCore.Mvc\" to the _Layout.cshtml and add <mini-profiler /> to the target view to show analysis information."
        );

        return builder;
    }
}