using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddSwaggerGen = "59a8a837-eb19-4f97-8bcf-832a1370afc8";

    public static WebApplicationBuilder AddAdvanceSwagger(this WebApplicationBuilder builder)
    {
        if (builder.HasRegistered(UniqueIdentifierAddSwaggerGen))
            return builder;

        if (!SwaggerConfigAssist.GetSwitch())
        {
            StartupConfigMessageAssist.AddConfig(
                "Swagger: disable."
            );

            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceSwagger)}()."
        );

        StartupConfigMessageAssist.AddConfig(
            $"swagger: enable, access {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/swagger/index.html" : FlagAssist.StartupUrls.Select(o => $"{o}/swagger/index.html").Join(" "))}."
        );

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access swagger api document by {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/swagger/index.html" : FlagAssist.StartupUrls.Select(o => $"{o}/swagger/index.html").Join(" "))}."
        );

        builder.Services.AddSwaggerGen();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseAdvanceSwagger(); })
        );

        return builder;
    }
}