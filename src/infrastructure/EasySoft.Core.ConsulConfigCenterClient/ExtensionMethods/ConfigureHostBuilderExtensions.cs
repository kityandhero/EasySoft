using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.ConsulConfigCenterClient.ExtensionMethods;

public static class ConfigureHostBuilderExtensions
{
    public static ConfigureHostBuilder AddAdvanceConsulConfigCenter<T>(
        this ConfigureHostBuilder builder,
        T channel,
        Action<IConfiguration>? action = null
    ) where T : IChannel
    {
        builder.ConfigureAppConfiguration(
            (hostingContext, config) =>
            {
                // 加载默认配置信息到Configuration
                hostingContext.Configuration = config.Build();

                // 动态加载环境信息，主要在于动态获取服务名称和环境名称
                var env = hostingContext.HostingEnvironment;

                // 加载consul配置中心配置
                var consulUrl = ConsulConfigCenterConfigAssist.GetCenterAddress();

                StartupDescriptionMessageAssist.AddPrompt(
                    $"Consul config center address is {ConsulConfigCenterConfigAssist.GetCenterAddress()}, key is {channel.ToValue()}/config.{env.EnvironmentName}.json, key build by ApplicationChannel and Environment"
                );

                config.AddConsul(
                    $"{channel.Name}_{channel.ToValue()}/config.{env.EnvironmentName}.json",
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

                ConsulConfigCenterConfigAssist.SetConfiguration(configure);

                if (action != null)
                {
                    ChangeToken.OnChange(
                        () => configure.GetReloadToken(),
                        () => action(ConsulConfigCenterConfigAssist.GetConfiguration())
                    );
                }
            }
        );

        return builder;
    }
}