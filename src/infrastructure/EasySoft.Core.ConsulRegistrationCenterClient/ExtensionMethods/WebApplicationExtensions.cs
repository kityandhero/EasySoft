using Consul;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ConsulRegistrationCenterClient.Assists;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.ConsulRegistrationCenterClient.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceConsulRegistrationCenter(
        this WebApplication application
    )
    {
        var consulClient = ConsulRegistrationCenterClientAssist.GetConfigClient();

        var serviceHealthCheck = ConsulRegistrationCenterConfigAssist.GetServiceHealthCheck();

        var healthCheckAddress = string.IsNullOrWhiteSpace(serviceHealthCheck)
            ? $"http://{ConsulRegistrationCenterConfigAssist.GetServiceIP()}:{ConsulRegistrationCenterConfigAssist.GetServicePort()}/{ConstCollection.ConsulServiceHealthEndpointName}"
            : serviceHealthCheck;

        var registration = new AgentServiceRegistration
        {
            ID = Guid.NewGuid().ToString(),
            // 服务名
            Name = ConsulRegistrationCenterConfigAssist.GetServiceName(),
            // 服务绑定IP
            Address = ConsulRegistrationCenterConfigAssist.GetServiceIP(),
            // 服务绑定端口
            Port = ConsulRegistrationCenterConfigAssist.GetServicePort(),
            Check = new AgentServiceCheck
            {
                //服务启动多久后注册
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                //健康检查时间间隔
                Interval = TimeSpan.FromSeconds(10),
                //健康检查地址
                HTTP = healthCheckAddress,
                Timeout = TimeSpan.FromSeconds(5)
            }
        };

        // 服务注销
        consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
        // 服务注册
        consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

        consulClient.Dispose();

        // 应用程序终止时，服务取消注册
        application.Lifetime.ApplicationStopping.Register(() =>
        {
            ConsulRegistrationCenterClientAssist.GetConfigClient()
                .Agent.ServiceDeregister(registration.ID)
                .ConfigureAwait(true);
        });

        return application;
    }
}