using Consul;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ConsulConfigClient.Assists;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Winton.Extensions.Configuration.Consul;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace EasySoft.Core.ConsulConfigClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceConsul(
        this WebApplicationBuilder builder
    )
    {
        if (ConsulFlagAssist.GetInitializeRegistrationWhetherComplete())
        {
            return builder;
        }

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => application.UseAdvanceConsul())
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => endpoints.MapConsulHealthCheck())
        );

        var serviceHealthCheck = ConsulConfigAssist.GetServiceHealthCheck();

        var healthCheckAddress = string.IsNullOrWhiteSpace(serviceHealthCheck)
            ? $"http://{ConsulConfigAssist.GetServiceIP()}:{ConsulConfigAssist.GetServicePort()}/{ConstCollection.ConsulServiceHealthEndpointName}"
            : serviceHealthCheck;

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"consul registration center address is {ConsulConfigAssist.GetConsulAddress()} , service name is \"{ConsulConfigAssist.GetServiceName()}\", address is \"{ConsulConfigAssist.GetServiceIP()}:{ConsulConfigAssist.GetServicePort()}\", health check is {healthCheckAddress}."
                )
        );

        ConsulFlagAssist.SetInitializeRegistrationComplete();

        return builder;
    }

    public static WebApplicationBuilder AddAdvanceConsulConfigClient<T>(
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
            var consulUrl = ConsulConfigAssist.GetConsulAddress();

            StartupDescriptionMessageAssist.Add(
                new StartupMessage()
                    .SetLevel(LogLevel.Information)
                    .SetMessage(
                        $"consul config center address is {ConsulConfigAssist.GetConsulAddress()}, key is {applicationChannel.GetChannel()}/config.{env.EnvironmentName}.json, key build by ApplicationChannel and Environment"
                    )
            );

            config.AddConsul(
                $"{applicationChannel.GetChannel()}/config.{env.EnvironmentName}.json",
                options =>
                {
                    // 1、consul地址
                    options.ConsulConfigurationOptions = cco => { cco.Address = new Uri(consulUrl); };
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

            ConsulConfigAssist.SetConfiguration(configure);

            if (action != null)
            {
                ChangeToken.OnChange(
                    () => configure.GetReloadToken(),
                    () => action(ConsulConfigAssist.GetConfiguration())
                );
            }
        });

        ConsulFlagAssist.SetInitializeConfigComplete();

        return builder;
    }
}