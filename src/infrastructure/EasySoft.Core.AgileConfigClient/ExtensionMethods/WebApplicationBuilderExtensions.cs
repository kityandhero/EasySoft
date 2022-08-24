using AgileConfig.Client;
using AgileConfig.Protocol;
using EasySoft.Core.AgileConfigClient.Assists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace EasySoft.Core.AgileConfigClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder UseAgileConfigClient(
        this WebApplicationBuilder builder
    )
    {
        builder.Host.ConfigureAppConfiguration((_, config) =>
        {
            try
            {
                var configClient = new ConfigClient(
                    GeneralConfigAssist.GetAgileConfigAppId(),
                    GeneralConfigAssist.GetAgileConfigSecret(),
                    GeneralConfigAssist.GetAgileConfigNodeCollection().Join(","),
                    GeneralConfigAssist.GetAgileConfigEnv()
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

                //注册配置项修改事件  
                configClient.ConfigChanged += ConfigClient_ConfigChanged;

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

        return builder;
    }

    /// <summary>
    /// 此事件会在配置项目发生新增、修改、删除的时候触发
    /// </summary>
    private static void ConfigClient_ConfigChanged(ConfigChangedArg obj)
    {
        Console.WriteLine($"action:{obj.Action} key:{obj.Key}");

        switch (obj.Action)
        {
            case ActionConst.Add:
                break;

            case ActionConst.Update:
                break;

            case ActionConst.Remove:
                break;

            default:
                break;
        }
    }
}