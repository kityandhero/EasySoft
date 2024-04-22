using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.GeneralLogTransmitter.Producers;
using EasySoft.Core.Infrastructure.Assists;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.GeneralLogTransmitter.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddGeneralLogTransmitter = "eb34705d-9ab6-4def-a853-41d3ff47c2b8";

    /// <summary>
    /// 配置远程普通日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddGeneralLogTransmitter(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddGeneralLogTransmitter))
            return builder;

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteGeneralLogSwitch()
                ? "RemoteGeneralLogEnable: enable."
                : "RemoteGeneralLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddSingleton<IGeneralLogProducer, GeneralLogProducer>();

        return builder;
    }
}