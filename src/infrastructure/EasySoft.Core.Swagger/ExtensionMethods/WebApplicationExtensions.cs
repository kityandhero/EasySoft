using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Entities;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetEnable())
        {
            StartupMessage.StartupMessageCollection.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message = "Swagger: disable."
            });

            return application;
        }

        application.UseSwagger();
        application.UseSwaggerUI();

        StartupMessage.StartupMessageCollection.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message =
                $"swagger: enable, access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]" : FlagAssist.StartupUrls)}/swagger/index.html."
        });

        return application;
    }
}