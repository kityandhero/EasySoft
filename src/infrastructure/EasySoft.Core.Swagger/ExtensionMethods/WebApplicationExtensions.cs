using EasySoft.Core.Swagger.ConfigAssist;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Swagger.ExtensionMethods;

/// <summary>
/// WebApplicationExtensions
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// UseAdvanceSwagger
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceSwagger)}."
        );

        application.UseSwagger();

        application.UseSwaggerUI();

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access swagger api document by {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/swagger/index.html").Join(" ")}."
        );

        if (EnvironmentAssist.IsDevelopment() && AuxiliaryConfigure.PromptConfigFileInfo)
            ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
                new ExtraAction<IEndpointRouteBuilder>()
                    .SetName("")
                    .SetAction(endpoints => { endpoints.MapSwaggerConfigFile(); })
            );

        return application;
    }

    /// <summary>
    /// UseAdvanceKnife4UI
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static WebApplication UseAdvanceKnife4UI(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceKnife4UI)}."
        );

        const string routePrefix = "knife4";

        application.UseKnife4UI(c =>
        {
            c.RoutePrefix = routePrefix; // serve the UI at root

            c.SwaggerEndpoint($"../swagger/v1/swagger.json", SwaggerConfigAssist.GetTitle());
        });

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access swagger knife4UI by {FlagAssist.StartupDisplayUrls.Select(o => $"{o}/{routePrefix}").Join(" ")}."
        );

        return application;
    }
}