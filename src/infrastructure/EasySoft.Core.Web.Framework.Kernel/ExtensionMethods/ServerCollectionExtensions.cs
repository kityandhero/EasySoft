using EasySoft.Core.Web.Framework.Kernel.Assists;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Web.Framework.Kernel.ExtensionMethods;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入核心配置
    /// </summary>
    /// <param name="services">服务集</param>
    /// <param name="configuration">配置集</param>
    /// <returns>服务集</returns>
    internal static IServiceCollection AddKernelConfigureAssist(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(new KernelConfigureAssist(configuration));

        return services;
    }

    internal static IServiceCollection AddAdvanceDefaultApplicationChannel(
        this IServiceCollection services
    )
    {
        services.AddAdvanceApplicationChannel(
            ApplicationChannel.DefaultChannel
        );

        return services;
    }

    internal static IServiceCollection AddAdvanceApplicationChannel(
        this IServiceCollection services,
        IChannel channel
    )
    {
        var applicationChannel = new ApplicationChannel().SetChannel(channel);

        services.AddSingleton<IApplicationChannel>(applicationChannel);

        StartupDescriptionMessageAssist.AddPrompt(
            $"ApplicationChannel name is {channel.Name}, identification is {channel.Value}."
        );

        return services;
    }

    /// <summary>
    /// 启用压缩
    /// </summary>
    /// <returns>服务集</returns>
    internal static IServiceCollection AddAdvanceResponseCompression(
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

        ApplicationConfigure.AddWebApplicationExtraAction(
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