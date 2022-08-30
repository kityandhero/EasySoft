using AgileConfig.Client;
using EasySoft.Core.AgileConfigClient.Assists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Entities;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.AgileConfigClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAgileConfigClient(
        this WebApplicationBuilder builder,
        Action<ConfigChangedArg>? action = null
    )
    {
        builder.Host.ConfigureHostConfiguration((config) =>
        {
            Task.Run(() =>
            {
                while (!FlagAssist.GetApplicationRunWhetherPerformed())
                {
                    Thread.Sleep(500);
                }

                try
                {
                    StartupMessage.StartupMessageCollection.Add(new StartupMessage
                    {
                        LogLevel = LogLevel.Information,
                        Message =
                            $"AgileConfig env: {GeneralConfigAssist.GetAgileConfigEnv()}, node: {GeneralConfigAssist.GetAgileConfigNodeCollection().Join(",")}."
                    });

                    var configClient = new ConfigClient(
                        GeneralConfigAssist.GetAgileConfigAppId(),
                        GeneralConfigAssist.GetAgileConfigSecret(),
                        GeneralConfigAssist.GetAgileConfigNodeCollection().Join(","),
                        GeneralConfigAssist.GetAgileConfigEnv(),
                        LogAssist.GetLogger()
                    )
                    {
                        Options =
                        {
                            Name = GeneralConfigAssist.GetAgileConfigName(),
                            Tag = GeneralConfigAssist.GetAgileConfigTag(),
                            CacheDirectory = GeneralConfigAssist.GetAgileConfigCacheDirectory(),
                            HttpTimeout = GeneralConfigAssist.GetAgileConfigHttpTimeout()
                        }
                    };

                    if (action != null)
                    {
                        //注册配置项修改事件  
                        configClient.ConfigChanged += action;
                    }

                    //注册配置项修改事件    
                    configClient.ConfigChanged += AgileConfigClientActionAssist.ActionAgileConfigChanged;

                    //使用AddAgileConfig配置一个新的IConfigurationSource
                    config.AddAgileConfig(configClient);

                    //找一个变量挂载client实例，以便其他地方可以直接使用实例访问配置
                    ConfigClientAssist.SetConfigClient(configClient);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        });

        return builder;
    }
}