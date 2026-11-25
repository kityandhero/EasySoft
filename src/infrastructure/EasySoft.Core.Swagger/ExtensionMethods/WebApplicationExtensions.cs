using EasySoft.Core.Swagger.ConfigAssist;
using EasySoft.UtilityTools.AspNetCore.Configures;
using EasySoft.UtilityTools.Core.Startup;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Swagger.ExtensionMethods;

/// <summary>
/// HostExtensions
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// AddAdvanceSwagger
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetSwitch())
        {
            return application;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceSwagger)}."
        );

        application.MapOpenApi(); // 输出 openapi.json
        application.MapScalarApiReference(); // ✅ Scalar UI 在这里，不是Services.AddXXX

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access swagger api document by {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/swagger/index.html").Join(" ")}."
        );

        if (HostEnvironmentAssist.IsDevelopment())
        {
            WebApplicationConfigure.AddEndpointRouteBuilderExtraAction(
                new ExtraAction<IEndpointRouteBuilder>()
                    .SetName("")
                    .SetAction(endpoints => { endpoints.MapSwaggerConfigFile(); })
            );
        }

        return application;
    }
}