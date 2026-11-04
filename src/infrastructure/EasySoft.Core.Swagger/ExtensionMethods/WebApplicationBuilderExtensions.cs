using EasySoft.Core.Swagger.ConfigAssist;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Swagger.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierUseAdvanceSwagger = "59a8a837-eb19-4f97-8bcf-832a1370afc8";

    /// <summary>
    /// AddAdvanceSwagger
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceSwagger(
        this WebApplicationBuilder builder,
        Action<OpenApiOptions> setupAction = null
    )
    {
        if (builder.HasRegistered(UniqueIdentifierUseAdvanceSwagger))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(UniqueIdentifierUseAdvanceSwagger)}.");

        SwaggerConfigAssist.Init();
        var swaggerSwitch = SwaggerConfigAssist.GetSwitch();

        StartupConfigMessageAssist.AddConfig(
            $"OpenApi: {(!swaggerSwitch ? "disable" : "enable")}.",
            SwaggerConfigAssist.GetConfigFileInfo()
        );

        if (!swaggerSwitch)
        {
            StartupDescriptionMessageAssist.AddPrompt("OpenApi is closed.");
            return builder;
        }

        builder.Services.AddAdvanceOpenApi();

        ApplicationConfigure.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application =>
                    {
                        if (application.Environment.IsDevelopment())
                        {
                            application.MapOpenApi();
                            application.MapScalarApiReference();
                        }
                    }
                )
        );

        return builder;
    }
}