using EasySoft.Core.Swagger.Configures;

namespace EasySoft.Core.Swagger.OperationFilters;

/// <summary>
/// ApiResultResponsesOperationFilter
/// </summary>
public class ApiResultResponsesOperationFilter : IOperationFilter
{
    /// <summary>
    /// Apply
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        SwaggerConfigure.GeneralParameters.ForEach(o => { operation.Parameters.Add(o); });

        var supplementApiResult = false;

        var producesResponseTypeAttributes = context.MethodInfo.DeclaringType?
            .GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<ProducesResponseTypeAttribute>()
            .ToList();

        var useTypeGeneric = false;
        Type? type = null;
        Type? typeGeneric = null;

        if (producesResponseTypeAttributes == null || !producesResponseTypeAttributes.Any())
        {
            type = context.MethodInfo.ReturnType;

            if (typeof(Task).IsAssignableFrom(type) && type.IsGenericType)
            {
                var arguments = type.GetGenericArguments();

                if (arguments.Length == 1) type = arguments[0];
            }

            if (type.IsGenericType)
            {
                typeGeneric = type;
                type = type.GetGenericTypeDefinition();
            }

            if (typeof(IApiResult).IsAssignableFrom(type))
                supplementApiResult = true;

            if (!supplementApiResult && typeof(IApiResult<>).IsAssignableFrom(type))
            {
                useTypeGeneric = true;
                supplementApiResult = true;
            }

            if (!supplementApiResult && typeof(IApiResult<,>).IsAssignableFrom(type))
            {
                useTypeGeneric = true;
                supplementApiResult = true;
            }

            if (typeof(ApiResult).IsAssignableFrom(type))
                supplementApiResult = true;

            if (!supplementApiResult && typeof(ApiResult<>).IsAssignableFrom(type))
            {
                useTypeGeneric = true;
                supplementApiResult = true;
            }

            if (!supplementApiResult && typeof(ApiResult<,>).IsAssignableFrom(type))
            {
                useTypeGeneric = true;
                supplementApiResult = true;
            }

            if (supplementApiResult)
                if (type.FullName != typeof(IApiResult).FullName && type.FullName != typeof(ApiResult).FullName)
                {
                    if (useTypeGeneric)
                        SwaggerConfigure.RuntimeSchemaType.Add(typeGeneric);
                    else
                        SwaggerConfigure.RuntimeSchemaType.Add(type);
                }
        }

        var abnormalResponseCollection = SwaggerConfigure.AbnormalResponseCollection;

        if (!abnormalResponseCollection.Keys.Contains(StatusCodes.Status500InternalServerError.ToString()))
            abnormalResponseCollection.Add(new KeyValuePair<string, OpenApiResponse>(
                StatusCodes.Status500InternalServerError.ToString(),
                new OpenApiResponse
                {
                    Description = "Internal Server Error"
                }
            ));

        abnormalResponseCollection.ForEach(o =>
        {
            if (o.Key == StatusCodes.Status200OK.ToString()) return;

            if (!operation.Responses.Keys.Contains(o.Key))
                operation.Responses.Add(o);
        });

        operation.Responses.ForEach(o =>
        {
            if (o.Key != StatusCodes.Status200OK.ToString()) return;

            SwaggerConfigure.GeneralResponseHeaders.ForEach(one => { o.Value.Headers.Add(one.Key, one.Value); });

            if (!supplementApiResult) return;

            if (type == null) return;

            var openApiMediaType = new OpenApiMediaType
            {
                Schema = context.SchemaGenerator.GenerateSchema(
                    useTypeGeneric ? typeGeneric : type,
                    context.SchemaRepository
                )
            };

            o.Value.Content.Add(MimeCollection.TextPlain.ContentType, openApiMediaType);

            o.Value.Content.Add(MimeCollection.ApplicationJson.ContentType, openApiMediaType);

            o.Value.Content.Add(MimeCollection.TextJson.ContentType, openApiMediaType);
        });
    }
}