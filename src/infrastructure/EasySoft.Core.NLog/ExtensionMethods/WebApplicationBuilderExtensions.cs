using EasySoft.Core.Config.ConfigAssist;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
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
        builder.Host.UseNLog(new NLogProviderOptions().Configure(AppSettingAssist.GetSection("NLog")));

        return builder;
    }

    // void loadNlogConfig()
    // {
    //     IConfiguration config = builder.Configuration;
    //     LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
    //     LogManager.Configuration.Reload();
    // }
}