using EasySoft.Core.MiniProfiler.Assists;
using EasySoft.Core.MiniProfiler.Extensions;
using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Core.Startup;

namespace EasySoft.Core.MiniProfiler.AspNetCore.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceMiniProfile = "0087964a-ea50-4f11-9f85-35ee4146de53";

    /// <summary>
    /// 注入 MiniProfile
    /// </summary>   
    /// <param name="builder">构建器</param>
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

        builder.Services.AddAdvanceMiniProfile(
            builder.Configuration,
            WebApplicationConfigure.GetMiniProfilerOptionsAction(),
            o => { o.AddEntityFramework(); }
        );

        WebApplicationConfigure.AddWebApplicationExtraAction(
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