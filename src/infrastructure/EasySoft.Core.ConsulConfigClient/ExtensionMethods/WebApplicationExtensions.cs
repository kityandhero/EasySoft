using Consul;
using EasySoft.Core.Config.ConfigAssist;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.ConsulConfigClient.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceConsul(
        this WebApplication application
    )
    {
        var consulClient = new ConsulClient(x => { x.Address = new Uri(ConsulConfigAssist.GetConsulAddress()); });

        var serviceHealthCheck = ConsulConfigAssist.GetServiceHealthCheck();

        var healthCheckAddress = string.IsNullOrWhiteSpace(serviceHealthCheck)
            ? $"http://{ConsulConfigAssist.GetServiceIP()}:{ConsulConfigAssist.GetServicePort()}/{ConstCollection.ConsulServiceHealthEndpointName}"
            : serviceHealthCheck;

        var registration = new AgentServiceRegistration
        {
            ID = Guid.NewGuid().ToString(),
            // 服务名
            Name = ConsulConfigAssist.GetServiceName(),
            // 服务绑定IP
            Address = ConsulConfigAssist.GetConsulAddress(),
            // 服务绑定端口
            Port = ConsulConfigAssist.GetServicePort(),
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

        // 服务注册
        consulClient.Agent.ServiceRegister(registration).Wait();

        // 应用程序终止时，服务取消注册
        application.Lifetime.ApplicationStopping.Register(() =>
        {
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        });

        return application;
    }
}