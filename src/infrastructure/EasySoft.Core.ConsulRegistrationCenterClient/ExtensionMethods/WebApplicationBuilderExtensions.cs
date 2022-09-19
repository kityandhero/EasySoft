using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ConsulRegistrationCenterClient.Assists;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.ConsulRegistrationCenterClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceConsulRegistrationCenter(
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
                .SetAction(application => application.UseAdvanceConsulRegistrationCenter())
        );

        ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => endpoints.MapConsulHealthCheck())
        );

        var serviceHealthCheck = ConsulServiceConfigAssist.GetServiceHealthCheck();

        var healthCheckAddress = string.IsNullOrWhiteSpace(serviceHealthCheck)
            ? $"http://{ConsulServiceConfigAssist.GetServiceIP()}:{ConsulServiceConfigAssist.GetServicePort()}/{ConstCollection.ConsulServiceHealthEndpointName}"
            : serviceHealthCheck;

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    $"consul registration center address is {ConsulCenterConfigAssist.GetCenterAddress()} , service name is \"{ConsulServiceConfigAssist.GetServiceName()}\", address is \"{ConsulServiceConfigAssist.GetServiceIP()}:{ConsulServiceConfigAssist.GetServicePort()}\", health check is {healthCheckAddress}."
                )
        );

        ConsulFlagAssist.SetInitializeRegistrationComplete();

        return builder;
    }
}