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

        var type = context.MethodInfo.ReturnType;

        if (type.IsGenericType)
        {
            var arguments = type.GetGenericArguments();

            if (arguments.Length == 1) type = arguments[0];
        }

        var supplementApiResult = false;

        if (typeof(IApiResult).IsAssignableFrom(type))
        {
            var producesResponseTypeAttributes = context.MethodInfo.DeclaringType?
                .GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<ProducesResponseTypeAttribute>();

            if (producesResponseTypeAttributes == null || !producesResponseTypeAttributes.Any())
                supplementApiResult = true;
        }

        var abnormalResponseCollection = SwaggerConfigure.AbnormalResponseCollection;

        if (!abnormalResponseCollection.Keys.Contains(StatusCodes.Status500InternalServerError.ToString()))
            abnormalResponseCollection.Add(new KeyValuePair<string, OpenApiResponse>(
                StatusCodes.Status500InternalServerError.ToString(),
                new OpenApiResponse()
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

            var openApiMediaType = new OpenApiMediaType()
            {
                Schema = new OpenApiSchema()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.Schema,
                        Id = nameof(IApiResult)
                    }
                }
            };

            o.Value.Content.Add(MimeCollection.TextPlain.ContentType, openApiMediaType);

            o.Value.Content.Add(MimeCollection.ApplicationJson.ContentType, openApiMediaType);

            o.Value.Content.Add(MimeCollection.TextJson.ContentType, openApiMediaType);
        });
    }
}