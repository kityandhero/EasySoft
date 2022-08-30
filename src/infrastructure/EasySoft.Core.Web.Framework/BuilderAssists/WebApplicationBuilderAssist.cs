using EasySoft.Configuration.ExtensionMethods;
using EasySoft.Core.AgileConfigClient.ExtensionMethods;
using EasySoft.Core.AutoFac.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.Utils;
using EasySoft.Core.DynamicConfig.Assists;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Mapster.ExtensionMethods;
using EasySoft.Core.NLog.Assists;
using EasySoft.Core.NLog.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;

namespace EasySoft.Core.Web.Framework.BuilderAssists;

public static class WebApplicationBuilderAssist
{
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args
        });

        builder.AddAdvanceAutoFac();

        // builder.AddServerAddressesFeature();

        builder.AddCovertInjection();

        builder.AddAdvanceUrls();

        builder.AddAdvanceMapster();

        if (GeneralConfigAssist.GetAgileConfigSwitch())
        {
            builder.AddAgileConfigClient(_ =>
            {
                var result = DynamicConfigAssist.GetNLogJsonConfig();

                if (!result.Success || (result.Success && string.IsNullOrWhiteSpace(result.Data)))
                {
                    LogManager.Configuration = BuildDefaultConfig();

                    LogManager.Configuration.Reload();

                    return;
                }

                if (!string.IsNullOrWhiteSpace(result.Data))
                {
                    var hasChanged = NLogAssist.CheckChange(result.Data);

                    if (!hasChanged)
                    {
                        return;
                    }

                    try
                    {
                        LogManager.Configuration = new NLogLoggingConfiguration(
                            new ConfigurationBuilder().AddJsonContent(
                                result.Data
                            ).Build().GetSection("NLog")
                        );

                        LogManager.Configuration.Reload();

                        LogAssist.Info("Receive agileConfig changed message, nlog configure reload success.");
                    }
                    catch (Exception e)
                    {
                        LogAssist.Error($"some error occur, message: \"{e.Message}\", will use local config.");

                        LogManager.Configuration = BuildDefaultConfig();

                        LogManager.Configuration.Reload();
                    }
                }
                else
                {
                    LogManager.Configuration = BuildDefaultConfig();

                    LogManager.Configuration.Reload();
                }
            });

            builder.AddAdvanceNLog(BuildDefaultConfig);
        }
        else
        {
            builder.AddAdvanceNLog();
        }

        return builder;
    }

    private static NLogLoggingConfiguration BuildDefaultConfig()
    {
        NLogLoggingConfiguration configuration;

        var configurationSection = LogConfigAssist.GetSection("NLog");

        if (configurationSection.GetChildren().Any())
        {
            configuration = new NLogLoggingConfiguration(configurationSection);
        }
        else
        {
            configuration = new NLogLoggingConfiguration(
                new ConfigurationBuilder().AddJsonContent(
                    Tools.GetNlogDefaultConfig()
                ).Build().GetSection("NLog")
            );
        }

        return configuration;
    }
}