using EasySoft.Core.AppSecurityVerification.Assists;
using EasySoft.Core.AppSecurityVerification.Clients;
using EasySoft.Core.AppSecurityVerification.Detectors.Implements;
using EasySoft.Core.AppSecurityVerification.Detectors.Interfaces;

namespace EasySoft.Core.AppSecurityVerification.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAppSecurityVerification = "06738c9de5f742c9a1ed562512b23b94";

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
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAppSecurityVerification)}."
        );

        builder.Services.TryAddSingleton(new ProxyGenerator());

        var hostUrlConfigValue = AppSecurityServerConfigureAssist.GetHostUrl(builder.Configuration);

        StartupConfigMessageAssist.AddConfig(
            hostUrlConfigValue.ToArrowDescription()
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"AppSecurity server host url is {hostUrlConfigValue.Value}."
        );

        builder.AddAdvanceRefitClient<IAppSecurityClient>(clientBuilder =>
            {
                clientBuilder.ConfigureHttpClient(client =>
                    {
                        if (!Uri.TryCreate(
                                hostUrlConfigValue.Value,
                                UriKind.Absolute,
                                out var baseAddress
                            ))
                        {
                            throw new UnknownException("AppSecurityServerHostUrl error");
                        }

                        client.BaseAddress = baseAddress;
                    }
                );
            }
        );

        builder.Services.AddTransient<IAppSecurityDetector, AppSecurityDetector>();

        builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<IAppSecurityDetector>(); });

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseAppSecurityFirstVerify(); }
                )
        );

        return builder;
    }
}