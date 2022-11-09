using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceSwagger)}()."
        );

        application.UseSwagger();

        application.UseSwaggerUI();

        return application;
    }

    public static WebApplication UseAdvanceKnife4UI(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetSwitch()) return application;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(UseAdvanceKnife4UI)}()."
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