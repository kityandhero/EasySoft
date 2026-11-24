using EasySoft.Core.AdvanceNLog.Assists;
using EasySoft.Core.AdvanceNLog.Renderers;
using EasySoft.Core.AdvanceNLog.Utils;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.AdvanceNLog.ExtensionMethods;

public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入日志服务
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration">配置集</param>
    /// <param name="whetherDevelop">是否开发环境</param>
    /// <returns>服务集</returns>
    public static IServiceCollection AddAdvanceNLog(
        this IServiceCollection services,
        IConfiguration configuration,
        bool whetherDevelop = false
    )
    {
        services.AddSingleton(new LogConfigureAssist(configuration));

        // NLog: Setup NLog for Dependency injection
        // builder.Logging.ClearProviders();

        // 使用如下库实现，更换可能不会读取自定义配置
        // NLog.Extensions.Hosting
        // NLog.Web.AspNetCore

        services.AddLogging(b =>
            {
                b.ClearProviders();

                LogManager.Setup()
                    .SetupExtensions(ext =>
                        {
                            ext.RegisterLayoutRenderer<ShortLevelLayoutRenderer>("shortlevel");

                            ext.RegisterLayoutRenderer<AnsiHighlightLayoutRenderer>("ansi-highlight");
                        }
                    );

                var embedConfig = Tools.GetNlogEmbedConfig(
                    configuration,
                    whetherDevelop
                );

                LogManager.Configuration = new NLogLoggingConfiguration(
                    new ConfigurationBuilder()
                        .AddJsonContent(embedConfig)
                        .Build()
                        .GetSection("NLog")
                );

                b.AddNLogWeb();
            }
        );

        return services;
    }

    /// <summary>
    /// 注入日志服务
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="action">选项配置器</param>
    /// <returns>服务集</returns>
    public static IServiceCollection AddAdvanceNLog(
        this IServiceCollection services,
        Func<LoggingConfiguration> action
    )
    {
        services.AddLogging(b =>
            {
                b.ClearProviders();

                LogManager.Configuration = action();

                b.AddNLogWeb();
            }
        );

        return services;
    }
}