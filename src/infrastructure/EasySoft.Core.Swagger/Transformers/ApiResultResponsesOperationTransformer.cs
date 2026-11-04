using EasySoft.Core.Swagger.Configures;
using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Core.Results.Interfaces;
using EasySoft.UtilityTools.Standard.Extensions;
using Microsoft.AspNetCore.OpenApi;

namespace EasySoft.Core.Swagger.Transformers;

public class ApiResultResponsesOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken
    )
    {
        // 1.追加全局公共参数
        SwaggerConfigure.GeneralParameters.ForEach(o => { operation.Parameters.Add(o); });

        var supplementApiResult = false;
        var apiDesc = context.Description;
        if (apiDesc.ActionDescriptor is not ControllerActionDescriptor actionDesc)
        {
            return Task.CompletedTask;
        }

        var methodInfo = actionDesc.MethodInfo;

        var producesResponseTypeAttributes = methodInfo.DeclaringType?
            .GetCustomAttributes(true)
            .Union(methodInfo.GetCustomAttributes(true))
            .OfType<ProducesResponseTypeAttribute>()
            .ToList();

        var useTypeGeneric = false;
        Type type = null;
        Type typeGeneric = null;

        if (producesResponseTypeAttributes == null || !producesResponseTypeAttributes.Any())
        {
            type = methodInfo.ReturnType;

            if (typeof(Task).IsAssignableFrom(type) && type.IsGenericType)
            {
                var arguments = type.GetGenericArguments();
                if (arguments.Length == 1)
                {
                    type = arguments[0];
                }
            }

            if (type.IsGenericType)
            {
                typeGeneric = type;
                type = type.GetGenericTypeDefinition();
            }

            if (typeof(IApiResult).IsAssignableFrom(type))
            {
                supplementApiResult = true;
            }

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
            {
                supplementApiResult = true;
            }

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
        }

        var abnormalResponseCollection = SwaggerConfigure.AbnormalResponseCollection;
        if (!abnormalResponseCollection.Keys.Contains(StatusCodes.Status500InternalServerError.ToString()))
        {
            abnormalResponseCollection.Add(
                new KeyValuePair<string, OpenApiResponse>(
                    StatusCodes.Status500InternalServerError.ToString(),
                    new OpenApiResponse
                    {
                        Description = "Internal Server Error"
                    }
                )
            );
        }

        abnormalResponseCollection.ForEach(o =>
            {
                if (o.Key == StatusCodes.Status200OK.ToString())
                {
                    return;
                }

                if (operation.Responses != null && !operation.Responses.Keys.Contains(o.Key))
                {
                    operation.Responses.Add(o.Key, o.Value);
                }
            }
        );

        // 处理200响应头
        var okResponse = operation.Responses.FirstOrDefault(p => p.Key == StatusCodes.Status200OK.ToString());
        if (okResponse.Value != null)
        {
            SwaggerConfigure.GeneralResponseHeaders.ForEach(one => { okResponse.Value.Headers.Add(one.Key, one.Value); }
            );

            // ======================
            // 【重点】原这里调用context.SchemaGenerator.GenerateSchema，微软OpenApi不能在这里调用
            // 这一段需要拆分，Schema交由 SchemaTransformer 处理
            // if (!supplementApiResult) return;
            // ... 生成Schema并填充Content 的逻辑移走
            // ======================
        }

        return Task.CompletedTask;
    }
}