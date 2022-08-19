using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceSwagger(this WebApplication application)
    {
        if (!SwaggerConfigAssist.GetEnable())
        {
            application.RecordInformation("swagger: disable.");

            return application;
        }

        application.UseSwagger();
        application.UseSwaggerUI();

        application.RecordInformation("swagger: enable, access https://[host]:[port]/swagger/index.html.");

        return application;
    }
}