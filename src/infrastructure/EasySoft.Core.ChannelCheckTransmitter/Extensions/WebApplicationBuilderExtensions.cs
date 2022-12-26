using EasySoft.UtilityTools.Core.Extensions;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.ChannelCheckTransmitter.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddAccessWayTransmitter = "338ac76c-d8e7-4f93-ac89-0256c49871d7";

    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddChannelCheckTransmitter(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddAccessWayTransmitter))
            return builder;

        builder.Services.AddChannelCheckTransmitter();

        return builder;
    }
}