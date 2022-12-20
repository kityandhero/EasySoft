namespace EasySoft.Core.AccessWayTransmitter.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAccessWayTransmitter = "ec2fcae3-47e0-4600-8092-909136316bdc";

    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAccessWayTransmitter(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAccessWayTransmitter))
            return builder;

        builder.Services.AddAccessWayTransmitter();

        return builder;
    }
}