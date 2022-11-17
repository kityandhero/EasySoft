namespace EasySoft.Core.Web.Framework.Attributes;

public sealed class ApiResultFilterAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        // ReSharper disable once RedundantAssignment
        // ReSharper disable once EntityNameCapturedOnly.Local
        var apiResult = new ApiResult(ReturnCode.Ok);

        var isApiResult = false;

        var resultType = context.Result.GetType();

        if (resultType.IsGenericType)
        {
            var genericTypeDefinition = resultType.GetGenericTypeDefinition();

            if (typeof(IApiResult<>).IsAssignableFrom(genericTypeDefinition) ||
                typeof(IApiResult<,>).IsAssignableFrom(genericTypeDefinition) ||
                typeof(ApiResult<>).IsAssignableFrom(genericTypeDefinition) ||
                typeof(ApiResult<,>).IsAssignableFrom(genericTypeDefinition))
                isApiResult = true;
        }
        else
        {
            if (typeof(IApiResult).IsAssignableFrom(resultType) || typeof(ApiResult).IsAssignableFrom(resultType))
                isApiResult = true;
        }

        if (isApiResult)
        {
            var code = context.Result.GetProperty<int>(nameof(apiResult.Code));
            var success = context.Result.GetProperty<bool>(nameof(apiResult.Success));
            var message = context.Result.GetProperty<string>(nameof(apiResult.Message));
            var d = context.Result.GetProperty<object>(nameof(apiResult.Data));
            var ed = context.Result.GetProperty<object?>(nameof(apiResult.ExtraData));

            var data = BuildApiData(code, success, message, d, ed);

            var camelCase = context.Result.InvokeMethod<bool>(nameof(apiResult.GetCamelCase), new object[] { });

            context.Result = new JsonResult(
                data,
                JsonConvertAssist.CreateJsonSerializerSettings(camelCase)
            );
        }
        else
        {
            base.OnResultExecuting(context);
        }
    }

    private static object BuildApiData<TData, TExtraData>(
        int code,
        bool success,
        string message,
        TData data,
        TExtraData extraData
    )
    {
        dynamic d = new ExpandoObject();

        d.code = code;
        d.success = success;
        d.message = message;

        if (data != null)
        {
            if (data is JObject jData)
            {
                d.data = jData;
            }
            else
            {
                IEnumerable? list = data as IList;

                if (list != null)
                    d.list = list;
                else
                    d.data = data;
            }
        }

        if (extraData != null) d.extra = extraData;

        return d;
    }
}