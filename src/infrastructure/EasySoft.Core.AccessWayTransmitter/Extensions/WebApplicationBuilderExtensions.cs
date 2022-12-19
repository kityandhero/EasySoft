using EasySoft.Core.AccessWayTransmitter.Assists;

namespace EasySoft.Core.AccessWayTransmitter.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAccessWayTransmitter(
        this WebApplicationBuilder builder
    )
    {
        if (InitialAssist.InitialComplete) return builder;

        builder.Services.AddAccessWayTransmitter();

        InitialAssist.InitialComplete = true;

        return builder;
    }
}