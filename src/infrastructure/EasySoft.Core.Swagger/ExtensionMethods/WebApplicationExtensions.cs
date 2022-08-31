using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Entities;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetEnable())
        {
            StartupMessage.Add(new StartupMessage
            {
                LogLevel = LogLevel.Information,
                Message = "Swagger: disable."
            });

            return application;
        }

        application.UseSwagger();
        application.UseSwaggerUI();

        StartupMessage.Add(new StartupMessage
        {
            LogLevel = LogLevel.Information,
            Message =
                $"swagger: enable, access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/swagger/index.html" : FlagAssist.StartupUrls.Select(o => $"{o}/swagger/index.html").Join(" "))}."
        });

        return application;
    }
}