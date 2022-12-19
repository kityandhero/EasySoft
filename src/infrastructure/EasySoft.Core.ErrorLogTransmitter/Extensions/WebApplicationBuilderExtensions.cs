namespace EasySoft.Core.ErrorLogTransmitter.Extensions;

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
    public static WebApplicationBuilder AddErrorLogTransmitter(
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddErrorLogTransmitter();

        return builder;
    }
}