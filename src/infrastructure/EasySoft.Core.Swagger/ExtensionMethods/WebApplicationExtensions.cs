using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetSwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "Swagger: disable."
            );

            return application;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"Execute {nameof(UseAdvanceSwagger)}()."
        );

        application.UseSwagger();
        application.UseSwaggerUI();

        StartupConfigMessageAssist.AddConfig(
            $"swagger: enable, access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/swagger/index.html" : FlagAssist.StartupUrls.Select(o => $"{o}/swagger/index.html").Join(" "))}."
        );

        StartupDescriptionMessageAssist.AddDescription(
            $"You can access swagger api document by {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/swagger/index.html" : FlagAssist.StartupUrls.Select(o => $"{o}/swagger/index.html").Join(" "))}."
        );

        return application;
    }
}