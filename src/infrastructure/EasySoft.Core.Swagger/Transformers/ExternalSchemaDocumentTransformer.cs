using EasySoft.Core.Swagger.Configures;
using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Core.Results.Interfaces;
using Microsoft.AspNetCore.OpenApi;

namespace EasySoft.Core.Swagger.Transformers;

public class ExternalSchemaDocumentTransformer : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var schemaTypes = SwaggerConfigure.ExternalSchemaType.ToList();
        schemaTypes.Add(typeof(IApiResult));
        schemaTypes.Add(typeof(ApiResult));

        document.Components ??= new OpenApiComponents();

        foreach (var type in schemaTypes)
        {
            var schemaName = type.Name;
            if (document.Components.Schemas.ContainsKey(schemaName))
                continue;

            // .NET10 新增 GetOrCreateSchemaAsync，用框架原生逻辑生成Schema
            var schema = await context.GetOrCreateSchemaAsync(type, cancellationToken: cancellationToken);
            document.Components.Schemas.Add(schemaName, schema);
        }
    }
}