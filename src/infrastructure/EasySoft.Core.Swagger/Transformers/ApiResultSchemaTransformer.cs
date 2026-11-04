using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Core.Results.Interfaces;
using Microsoft.AspNetCore.OpenApi;

namespace EasySoft.Core.Swagger.Transformers;

public class ApiResultSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var jsonTypeInfo = context.JsonTypeInfo;
        
        
        
        
        if (jsonTypeInfo == null)
        {
            return Task.CompletedTask;  
        }

        var modelType = jsonTypeInfo.Type;

        // 匹配 ApiResult / IApiResult
        if (modelType == typeof(ApiResult) || modelType == typeof(IApiResult))
        {
            schema.Description = "统一返回结果";
        }

        // ApiResult<T> / IApiResult<T>
        if (modelType.IsGenericType)
        {
            var genericDef = modelType.GetGenericTypeDefinition();
            if (genericDef == typeof(ApiResult<>) || genericDef == typeof(IApiResult<>))
            {
                schema.Description = "带数据的统一返回结果";
            }

            if (genericDef == typeof(ApiResult<,>) || genericDef == typeof(IApiResult<,>))
            {
                schema.Description = "双泛型统一返回结果";
            }
        }

        return Task.CompletedTask;
    }
}