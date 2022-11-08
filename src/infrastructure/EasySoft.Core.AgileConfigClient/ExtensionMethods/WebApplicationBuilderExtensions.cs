using AgileConfig.Client;
using EasySoft.Core.AgileConfigClient.Assists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace EasySoft.Core.AgileConfigClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAgileConfigClient(
        this WebApplicationBuilder builder,
        Action<ConfigChangedArg>? action = null
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAgileConfigClient)}()."
        );

        builder.Host.ConfigureHostConfiguration((config) =>
        {
            Task.Run(() =>
            {
                while (!FlagAssist.GetApplicationRunWhetherPerformed()) Thread.Sleep(500);

                try
                {
                    StartupDescriptionMessageAssist.AddDescription(
                        $"AgileConfig env is \"{AgileConfigAssist.GetAgileConfigEnv()}\", node is \"{AgileConfigAssist.GetAgileConfigNodeCollection().Join(",")}\"."
                    );

                    var configClient = new ConfigClient(
                        AgileConfigAssist.GetAgileConfigAppId(),
                        AgileConfigAssist.GetAgileConfigSecret(),
                        AgileConfigAssist.GetAgileConfigNodeCollection().Join(","),
                        AgileConfigAssist.GetAgileConfigEnv(),
                        LogAssist.GetLogger()
                    )
                    {
                        Options =
                        {
                            Name = AgileConfigAssist.GetAgileConfigName(),
                            Tag = AgileConfigAssist.GetAgileConfigTag(),
                            CacheDirectory = AgileConfigAssist.GetAgileConfigCacheDirectory(),
                            HttpTimeout = AgileConfigAssist.GetAgileConfigHttpTimeout()
                        }
                    };

                    if (action != null)
                        //注册配置项修改事件  
                        configClient.ConfigChanged += action;

                    //注册配置项修改事件    
                    configClient.ConfigChanged += AgileConfigClientActionAssist.ActionAgileConfigChanged;

                    //使用AddAgileConfig配置一个新的IConfigurationSource
                    config.AddAgileConfig(configClient);

                    //找一个变量挂载client实例，以便其他地方可以直接使用实例访问配置
                    AgileConfigClientAssist.SetConfigClient(configClient);
                }
                catch (Exception e)
                {
                    LogAssist.Error(e.Message);

                    Console.WriteLine(e);
                }
            });
        });

        return builder;
    }
}