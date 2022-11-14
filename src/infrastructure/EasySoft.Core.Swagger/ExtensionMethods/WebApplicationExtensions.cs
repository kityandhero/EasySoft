using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Swagger.ConfigAssist;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceSwagger)}."
        );

        application.UseSwagger();

        application.UseSwaggerUI();

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access swagger api document by {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/swagger/index.html" : FlagAssist.StartupUrls.Select(o => $"{o}/swagger/index.html").Join(" "))}."
        );

        if (EnvironmentAssist.IsDevelopment() && AuxiliaryConfigure.PromptConfigFileInfo)
            ApplicationConfigurator.AddEndpointRouteBuilderExtraAction(
                new ExtraAction<IEndpointRouteBuilder>()
                    .SetName("")
                    .SetAction(endpoints => { endpoints.MapSwaggerConfigFile(); })
            );

        return application;
    }

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
            $"You can access swagger knife4UI by {(!FlagAssist.StartupUrls.Any() ? $"https://[host]:[port]/{routePrefix}" : FlagAssist.StartupUrls.Select(o => $"{o}/{routePrefix}").Join(" "))}."
        );

        return application;
    }
}