using EasySoft.Core.Swagger.ConfigAssist;
using EasySoft.Core.Swagger.Configures;
using EasySoft.Core.Swagger.Transformers;

namespace EasySoft.Core.Swagger.ExtensionMethods;

/// <summary>
/// ServiceCollectionExtension
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// 启用压缩
    /// </summary>
    /// <returns></returns>
    internal static IServiceCollection AddAdvanceOpenApi(
        this IServiceCollection services
    )
    {
        services.AddOpenApi(options =>
            {
                // 1. 文档基础信息、Server、SecurityScheme
                options.AddDocumentTransformer((document, ctx, ct) =>
                    {
                        document.Components ??= new OpenApiComponents();

                        #region Info / Contact / License

                        var openApiContact = new OpenApiContact
                        {
                            Name = SwaggerConfigAssist.GetContactName(),
                            Email = SwaggerConfigAssist.GetContactEmail()
                        };

                        var contactUrl = SwaggerConfigAssist.GetContactUrl();
                        if (Uri.TryCreate(
                                contactUrl,
                                UriKind.Absolute,
                                out var contactUri
                            ))
                        {
                            openApiContact.Url = contactUri;
                        }

                        var openApiInfo = new OpenApiInfo
                        {
                            Title = SwaggerConfigAssist.GetTitle(),
                            Description = SwaggerConfigAssist.GetDescription(),
                            Version = SwaggerConfigAssist.GetVersion(),
                            Contact = openApiContact
                        };

                        if (Uri.TryCreate(
                                SwaggerConfigAssist.GetLicenseUrl(),
                                UriKind.Absolute,
                                out var licenseUri
                            ))
                        {
                            openApiInfo.License = new OpenApiLicense
                            {
                                Name = SwaggerConfigAssist.GetLicenseName(),
                                Url = licenseUri
                            };
                        }

                        document.Info = openApiInfo;

                        #endregion

                        #region Server

                        var openApiServerUrl = SwaggerConfigAssist.GetOpenApiServerUrl();

                        openApiServerUrl = string.IsNullOrWhiteSpace(openApiServerUrl)
                            ? FlagAssist.StartupDisplayUrls.FirstOrDefault()
                            : openApiServerUrl;

                        document.Servers?.Clear();

                        document.Servers?.Add(
                            new OpenApiServer
                            {
                                Description = SwaggerConfigAssist.GetOpenApiServerDescription(),
                                Url = openApiServerUrl
                            }
                        );

                        #endregion

                        #region SecurityScheme

                        if (!string.IsNullOrWhiteSpace(SwaggerConfigure.SecurityScheme.Key) &&
                            SwaggerConfigure.SecurityScheme.Value != null)
                        {
                            document.Components.SecuritySchemes?[SwaggerConfigure.SecurityScheme.Key] =
                                SwaggerConfigure.SecurityScheme.Value;
                        }

                        #endregion

                        return Task.CompletedTask;
                    }
                );

                // 2. 单独抽离：外部Schema生成（独立类）
                options.AddDocumentTransformer<ExternalSchemaDocumentTransformer>();

                // Operation相关转换器
                options.AddOperationTransformer((op, ctx, ct) =>
                    {
                        if (ctx.Description.ActionDescriptor is ControllerActionDescriptor actionDesc)
                        {
                            op.OperationId = $"{actionDesc.ControllerName}-{actionDesc.ActionName}";
                        }

                        return Task.CompletedTask;
                    }
                );

                if (SwaggerConfigure.GlobalSecuritySwitch &&
                    !string.IsNullOrWhiteSpace(SwaggerConfigure.SecurityScheme.Key) &&
                    SwaggerConfigure.SecurityScheme.Value != null)
                {
                    options.AddOperationTransformer((op, ctx, ct) =>
                        {
                            var schemeRef = new OpenApiSecuritySchemeReference(
                                SwaggerConfigure.SecurityScheme.Key,
                                ctx.Document
                            );

                            op.Security ??= new List<OpenApiSecurityRequirement>();
                            op.Security.Add(
                                new OpenApiSecurityRequirement
                                {
                                    [schemeRef] = new List<string>()
                                }
                            );

                            return Task.CompletedTask;
                        }
                    );
                }

                options.AddOperationTransformer<ApiResultResponsesOperationTransformer>();
                options.AddSchemaTransformer<ApiResultSchemaTransformer>();
            }
        );

        return services;
    }
}