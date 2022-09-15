using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetSwitch())
        {
            StartupConfigMessageAssist.Add(
                new StartupMessage().SetLevel(LogLevel.Information).SetMessage(
                    "Swagger: disable."
                )
            );

            return application;
        }

        application.UseSwagger();
        application.UseSwaggerUI();

        StartupConfigMessageAssist.Add(
            new StartupMessage().SetLevel(LogLevel.Information).SetMessage(
                $"swagger: enable, access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/swagger/index.html" : FlagAssist.StartupUrls.Select(o => $"{o}/swagger/index.html").Join(" "))}."
            )
        );

        StartupDescriptionMessageAssist.Add(
            new StartupMessage().SetLevel(LogLevel.Information).SetMessage(
                $"You can access swagger api document by {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/swagger/index.html" : FlagAssist.StartupUrls.Select(o => $"{o}/swagger/index.html").Join(" "))}."
            )
        );

        return application;
    }
}