using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;

namespace EasySoft.Core.ErrorLogTransmitter.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddErrorLogTransmitter = "3991d807-45d5-4153-84f4-14b3919333fc";

    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddErrorLogTransmitter(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddErrorLogTransmitter))
            return builder;

        StartupConfigMessageAssist.AddConfig(
            GeneralConfigAssist.GetRemoteErrorLogSwitch()
                ? "RemoteErrorLogEnable: enable."
                : "RemoteErrorLogEnable: disable.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        builder.Services.AddErrorLogTransmitter();

        return builder;
    }
}