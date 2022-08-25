using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Extensions.Hosting;

namespace EasySoft.Core.NLog.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder UseAdvanceNLog(
        this WebApplicationBuilder builder
    )
    {
        // NLog: Setup NLog for Dependency injection
        builder.Logging.ClearProviders();
        // builder.Host.UseNLog(new NLogProviderOptions().Configure(LogConfigAssist.GetSection("NLog")));
        builder.Host.UseNLog(
            new NLogProviderOptions().Configure(
                Tools.GetNlogDefaultConfig(),
                "NLog"
            )
        );

        return builder;
    }
}