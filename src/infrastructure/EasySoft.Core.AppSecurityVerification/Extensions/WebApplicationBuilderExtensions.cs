using EasySoft.Core.AppSecurityVerification.Clients;
using EasySoft.Core.AppSecurityVerification.Detectors;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.AppSecurityVerification.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAppSecurityVerification = "9930d6ba-2364-44c4-8538-05468e3d0b93";

    /// <summary>
    /// 配置应用调用安全
    /// </summary> 
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAppSecurityVerification(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddAppSecurityVerification))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAppSecurityVerification)}."
        );

        builder.Services.TryAddSingleton(new ProxyGenerator());

        StartupDescriptionMessageAssist.AddPrompt(
            $"AppSecurity server host url is {GeneralConfigAssist.GetAppSecurityServerHostUrl()}."
        );

        builder.AddAdvanceRefitClient<IAppSecurityClient>(clientBuilder =>
        {
            clientBuilder.ConfigureHttpClient(client =>
            {
                if (!Uri.TryCreate(
                        GeneralConfigAssist.GetAppSecurityServerHostUrl(),
                        UriKind.Absolute,
                        out var baseAddress
                    ))
                    throw new UnknownException("AppSecurityServerHostUrl error");

                client.BaseAddress = baseAddress;
            });
        });

        builder.Services.AddTransient(
            typeof(IAppSecurityDetector),
            provider =>
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                var applicationChannel = provider.GetRequiredService<IApplicationChannel>();
                var appSecurityClient = provider.GetRequiredService<IAppSecurityClient>();

                var interceptors = new List<Type> { typeof(LogRecordInterceptor) }
                    .ConvertAll(interceptorType => { return provider.GetService(interceptorType) as IInterceptor; })
                    .ToArray();
                var proxyGenerator = provider.GetService<ProxyGenerator>();

                if (proxyGenerator == null)
                    throw new UnknownException(
                        "provider.GetService<ProxyGenerator>() result is null"
                    );

                var proxy = proxyGenerator.CreateInterfaceProxyWithTargetInterface(
                    typeof(IAppSecurityDetector),
                    new AppSecurityDetector(
                        loggerFactory,
                        applicationChannel,
                        appSecurityClient
                    ),
                    interceptors
                );

                return proxy;
            }
        );

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(
                    application => { application.UseAppSecurityFirstVerify(); }
                )
        );

        return builder;
    }
}