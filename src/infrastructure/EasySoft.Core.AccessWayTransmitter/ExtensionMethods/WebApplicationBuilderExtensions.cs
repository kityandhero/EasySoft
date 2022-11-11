using EasySoft.Core.AccessWayTransmitter.Assists;

namespace EasySoft.Core.AccessWayTransmitter.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAccessWayTransmitter(
        this WebApplicationBuilder builder
    )
    {
        if (InitialAssist.InitialComplete)
        {
            return builder;
        }

        builder.Host.AddAccessWayTransmitter();

        InitialAssist.InitialComplete = true;

        return builder;
    }
}