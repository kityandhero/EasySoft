using EasySoft.Core.AppSecurityVerification.Clients;
using EasySoft.Core.AppSecurityVerification.Detectors.Implements;
using EasySoft.Core.AppSecurityVerification.Detectors.Interfaces;

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

        builder.Services.AddTransient<IAppSecurityDetector, AppSecurityDetector>();

        builder.AddChannelCheckTransmitter();

        builder.Services.AddMediatR(typeof(IAppSecurityDetector).Assembly);

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(
                    application => { application.UseAppSecurityFirstVerify(); }
                )
        );

        return builder;
    }
}