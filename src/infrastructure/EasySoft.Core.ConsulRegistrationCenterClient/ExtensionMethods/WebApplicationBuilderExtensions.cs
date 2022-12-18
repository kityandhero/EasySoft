using EasySoft.Core.Infrastructure.Configures;

namespace EasySoft.Core.ConsulRegistrationCenterClient.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceConsulRegistrationCenter(
        this WebApplicationBuilder builder
    )
    {
        if (ConsulFlagAssist.GetInitializeRegistrationWhetherComplete()) return builder;

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => application.UseAdvanceConsulRegistrationCenter())
        );

        ApplicationConfigure.AddEndpointRouteBuilderExtraAction(
            new ExtraAction<IEndpointRouteBuilder>()
                .SetName("")
                .SetAction(endpoints => endpoints.MapConsulHealthCheck())
        );

        var serviceHealthCheck = ConsulRegistrationCenterConfigAssist.GetServiceHealthCheck();

        var healthCheckAddress = string.IsNullOrWhiteSpace(serviceHealthCheck)
            ? $"http://{ConsulRegistrationCenterConfigAssist.GetServiceIP()}:{ConsulRegistrationCenterConfigAssist.GetServicePort()}/{ConstCollection.ConsulServiceHealthEndpointName}"
            : serviceHealthCheck;

        StartupDescriptionMessageAssist.AddPrompt(
            $"Consul registration center address is {ConsulRegistrationCenterConfigAssist.GetCenterAddress()} , service name is \"{ConsulRegistrationCenterConfigAssist.GetServiceName()}\", address is \"{ConsulRegistrationCenterConfigAssist.GetServiceIP()}:{ConsulRegistrationCenterConfigAssist.GetServicePort()}\", health check is {healthCheckAddress}.",
            ConsulRegistrationCenterConfigAssist.GetConfigFileInfo()
        );

        ConsulFlagAssist.SetInitializeRegistrationComplete();

        return builder;
    }
}