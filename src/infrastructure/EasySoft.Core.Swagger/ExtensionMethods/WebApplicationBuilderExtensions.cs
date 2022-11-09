using System.Reflection;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.UtilityTools.Core.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddSwaggerGen = "59a8a837-eb19-4f97-8bcf-832a1370afc8";

    public static WebApplicationBuilder AddAdvanceSwagger(this WebApplicationBuilder builder)
    {
        if (builder.HasRegistered(UniqueIdentifierAddSwaggerGen))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceSwagger)}()."
        );

        var swaggerSwitch = SwaggerConfigAssist.GetSwitch();

        StartupConfigMessageAssist.AddConfig(
            $"Swagger: {(!swaggerSwitch ? "disable" : "enable")}.",
            SwaggerConfigAssist.GetConfigFileInfo()
        );

        if (!swaggerSwitch)
        {
            StartupDescriptionMessageAssist.AddPrompt(
                $"Swagger is closed."
            );

            return builder;
        }

        StartupDescriptionMessageAssist.AddPrompt(
            $"You can access swagger api document by {(!FlagAssist.StartupUrls.Any() ? "https://[host]:[port]/swagger/index.html" : FlagAssist.StartupUrls.Select(o => $"{o}/swagger/index.html").Join(" "))}."
        );

        builder.Services.AddSwaggerGen(c =>
        {
            var title = SwaggerConfigAssist.GetTitle();

            title = string.IsNullOrWhiteSpace(title) ? Assembly.GetEntryAssembly()?.GetName().Name : title;

            var openApiContact = new OpenApiContact
            {
                Name = SwaggerConfigAssist.GetContactName(),
                Email = SwaggerConfigAssist.GetContactEmail()
            };

            var contactUrl = SwaggerConfigAssist.GetContactUrl();

            if (Uri.TryCreate(contactUrl, UriKind.Absolute, out var contactUri)) openApiContact.Url = contactUri;

            var openApiLicense = new OpenApiLicense()
            {
                Name = SwaggerConfigAssist.GetLicenseName()
            };

            var licenseUrl = SwaggerConfigAssist.GetLicenseUrl();

            if (Uri.TryCreate(licenseUrl, UriKind.Absolute, out var licenseUri)) openApiLicense.Url = licenseUri;

            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = title,
                    Description = SwaggerConfigAssist.GetDescription(),
                    Version = SwaggerConfigAssist.GetVersion(),
                    Contact = openApiContact,
                    License = openApiLicense
                }
            );

            var openApiServerUrl = SwaggerConfigAssist.GetOpenApiServerUrl();

            openApiServerUrl = string.IsNullOrWhiteSpace(openApiServerUrl)
                ? !FlagAssist.StartupUrls.Any()
                    ? "https://[host]:[port]"
                    : FlagAssist.StartupUrls.FirstOrDefault()
                : openApiServerUrl;

            var openApiServer = new OpenApiServer
            {
                Description = SwaggerConfigAssist.GetOpenApiServerDescription(),
                Url = openApiServerUrl
            };

            c.AddServer(openApiServer);

            c.CustomOperationIds(apiDesc =>
            {
                var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;

                return controllerAction?.ControllerName + "-" + controllerAction?.ActionName;
            });
        });

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseAdvanceSwagger(); })
        );

        return builder;
    }
}