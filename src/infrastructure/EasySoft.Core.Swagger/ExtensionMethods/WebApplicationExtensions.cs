using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
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
}