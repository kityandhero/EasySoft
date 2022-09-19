using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ConsulConfigCenterClient.Assists;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Winton.Extensions.Configuration.Consul;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace EasySoft.Core.ConsulConfigCenterClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceConsulConfigCenter<T>(
        this WebApplicationBuilder builder,
        T applicationChannel,
        Action<IConfiguration>? action = null
    ) where T : IApplicationChannel
    {
        if (ConsulFlagAssist.GetInitializeConfigWhetherComplete())
        {
            return builder;
        }

        builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
        {
            // 加载默认配置信息到Configuration
            hostingContext.Configuration = config.Build();

            // 动态加载环境信息，主要在于动态获取服务名称和环境名称
            var env = hostingContext.HostingEnvironment;

            // 加载consul配置中心配置
            var consulUrl = ConsulCenterConfigAssist.GetCenterAddress();

            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"Consul config center address is {ConsulCenterConfigAssist.GetCenterAddress()}, key is {applicationChannel.GetChannel()}/config.{env.EnvironmentName}.json, key build by ApplicationChannel and Environment"
                    )
            );

            config.AddConsul(
                $"{applicationChannel.GetChannel()}/config.{env.EnvironmentName}.json",
                options =>
                {
                    // 1、consul地址
                    options.ConsulConfigurationOptions = consulClientConfiguration =>
                    {
                        consulClientConfiguration.Address = new Uri(consulUrl);
                    };
                    // 2、配置选项
                    options.Optional = true;
                    // 3、配置文件更新后重新加载
                    options.ReloadOnChange = true;
                    // 4、忽略异常
                    options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                }
            );

            // 5、consul中加载的配置信息加载到Configuration对象，然后通过Configuration 对象加载项目中
            var configure = config.Build();

            hostingContext.Configuration = configure;

            ConsulCenterConfigAssist.SetConfiguration(configure);

            if (action != null)
            {
                ChangeToken.OnChange(
                    () => configure.GetReloadToken(),
                    () => action(ConsulCenterConfigAssist.GetConfiguration())
                );
            }
        });

        ConsulFlagAssist.SetInitializeConfigComplete();

        return builder;
    }
}