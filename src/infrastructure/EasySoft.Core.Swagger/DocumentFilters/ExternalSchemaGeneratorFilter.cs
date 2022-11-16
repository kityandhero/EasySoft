using System.ComponentModel;
using EasySoft.Core.Swagger.Configures;

namespace EasySoft.Core.Swagger.DocumentFilters;

/// <summary>
/// External Schema Generator Filter, Generator Target Type To OpenApiDocument
/// </summary>
public sealed class ExternalSchemaGeneratorFilter : IDocumentFilter
{
    /// <summary>
    /// Apply
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        SwaggerConfigure.ExternalSchemaType.ForEach(o =>
        {
            if (!swaggerDoc.Components.Schemas.Keys.Contains(o.Name))
                context.SchemaGenerator.GenerateSchema(o, context.SchemaRepository);
        });
    }
}