using Autofac;
using EasySoft.Core.AccessWayTransmitter.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.IdentityVerification.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置远程异常日志传输
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAdvanceIdentityVerification(
        this WebApplicationBuilder builder
    )
    {
        builder.UseAccessWayTransmitter();

        return builder;
    }
}