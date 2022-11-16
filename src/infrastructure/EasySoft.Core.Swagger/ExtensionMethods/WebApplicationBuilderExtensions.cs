using EasySoft.Core.Swagger.ConfigAssist;
using EasySoft.Core.Swagger.Configures;
using EasySoft.Core.Swagger.DocumentFilters;
using EasySoft.Core.Swagger.OperationFilters;

namespace EasySoft.Core.Swagger.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddSwaggerGen = "59a8a837-eb19-4f97-8bcf-832a1370afc8";

    public static WebApplicationBuilder AddAdvanceSwagger(
        this WebApplicationBuilder builder,
        Action<SwaggerGenOptions>? setupAction = null
    )
    {
        if (builder.HasRegistered(UniqueIdentifierAddSwaggerGen))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceSwagger)}."
        );

        SwaggerConfigAssist.Init();

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

        builder.Services.AddSwaggerGen(c =>
        {
            var openApiContact = new OpenApiContact
            {
                Name = SwaggerConfigAssist.GetContactName(),
                Email = SwaggerConfigAssist.GetContactEmail()
            };

            var contactUrl = SwaggerConfigAssist.GetContactUrl();

            if (Uri.TryCreate(contactUrl, UriKind.Absolute, out var contactUri)) openApiContact.Url = contactUri;

            var openApiInfo = new OpenApiInfo
            {
                Title = SwaggerConfigAssist.GetTitle(),
                Description = SwaggerConfigAssist.GetDescription(),
                Version = SwaggerConfigAssist.GetVersion(),
                Contact = openApiContact
            };

            if (Uri.TryCreate(SwaggerConfigAssist.GetLicenseUrl(), UriKind.Absolute, out var licenseUri))
            {
                var openApiLicense = new OpenApiLicense
                {
                    Name = SwaggerConfigAssist.GetLicenseName(),
                    Url = licenseUri
                };

                openApiInfo.License = openApiLicense;
            }

            c.SwaggerDoc(
                "v1",
                openApiInfo
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

            c.OperationFilter<ApiResultResponsesOperationFilter>();

            if (SwaggerConfigure.DescribeAllParametersInCamelCase) c.EnableAnnotations();

            if (SwaggerConfigure.DescribeAllParametersInCamelCase) c.DescribeAllParametersInCamelCase();

            //
            // c.SchemaGeneratorOptions.CustomTypeMappings.Add(typeof(IApiResult), () =>
            // {
            //     return new OpenApiSchema()
            //     {
            //         Reference = new OpenApiReference()
            //         {
            //             Type = ReferenceType.Schema,
            //             Id = nameof(IApiResult)
            //         }
            //     };
            // });

            c.DocumentFilter<ExternalSchemaGeneratorFilter>();

            #region IncludeXmlComments

            var assemblyNames = new List<string>
            {
                Assembly.GetEntryAssembly()?.GetName().Name ?? ""
            };

            Assembly.GetEntryAssembly()?.GetReferencedAssemblies().ForEach(o => { assemblyNames.Add(o.Name ?? ""); });

            var assemblyNameAdjust = assemblyNames.ToListFilterNullOrWhiteSpace().Distinct().ToList();

            if (assemblyNameAdjust.Any())
                assemblyNameAdjust.ForEach(o =>
                {
                    var filePath = Path.Combine(AppContext.BaseDirectory, $"{o}.xml");

                    if (!filePath.ExistFile()) return;

                    LogAssist.Prompt(
                        $"The swagger include xml comments file: {filePath}."
                    );

                    c.IncludeXmlComments(filePath, true);
                });

            #endregion

            setupAction?.Invoke(c);
        });

        if (SwaggerConfigure.UseNewtonsoft)
            // explicit opt-in - needs to be placed after AddSwaggerGen()
            builder.Services.AddSwaggerGenNewtonsoftSupport();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application =>
                {
                    application.UseAdvanceSwagger();

                    application.UseAdvanceKnife4UI();
                })
        );

        return builder;
    }
}