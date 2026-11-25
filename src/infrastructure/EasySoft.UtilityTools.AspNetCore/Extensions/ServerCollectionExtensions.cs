using EasySoft.UtilityTools.AspNetCore.Assists;
using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Startup;

namespace EasySoft.UtilityTools.AspNetCore.Extensions;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入 Web Host 核心配置
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration">配置集</param>
    /// <returns>服务集</returns>
    public static IServiceCollection AddWebApplicationConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new WebApplicationConfigureAssist(configuration));

        return services;
    }

    /// <summary>
    /// 启用压缩
    /// </summary>
    /// <returns>服务集</returns>
    public static IServiceCollection AddAdvanceResponseCompression(
        this IServiceCollection services
    )
    {
        services.AddResponseCompression();

        StartupDescriptionMessageAssist.AddExecute(
            $"AddResponseCompression"
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"Response use compression"
        );

        WebApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application =>
                    {
                        application.UseResponseCompression();

                        StartupDescriptionMessageAssist.AddExecute(
                            $"UseResponseCompression"
                        );
                    }
                )
        );

        return services;
    }
}