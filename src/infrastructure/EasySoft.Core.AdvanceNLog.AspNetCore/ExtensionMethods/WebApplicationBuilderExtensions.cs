using EasySoft.Core.AdvanceNLog.ExtensionMethods;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.AdvanceNLog.AspNetCore.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceNLog = "8c1ba1e4-2745-47e6-8286-afe73de2b7ed";

    /// <summary>
    /// 注入日志框架
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddAdvanceNLog(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceNLog))
        {
            return builder;
        }

        builder.Services.AddAdvanceNLog(
            builder.Configuration,
            HostEnvironmentAssist.GetEnvironment().IsDevelopment()
        );

        return builder;
    }

    /// <summary>
    /// 注入日志框架
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="action">选项配置器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddAdvanceNLog(
        this WebApplicationBuilder builder,
        Func<LoggingConfiguration> action
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAdvanceNLog))
        {
            return builder;
        }

        builder.Services.AddAdvanceNLog(action);

        return builder;
    }
}