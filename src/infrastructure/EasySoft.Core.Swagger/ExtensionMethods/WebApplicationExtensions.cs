using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetEnable())
        {
            LogAssist.Info("swagger: disable.");

            return application;
        }

        application.UseSwagger();
        application.UseSwaggerUI();

        LogAssist.Info("swagger: enable, access https://[host]:[port]/swagger/index.html.");

        return application;
    }
}