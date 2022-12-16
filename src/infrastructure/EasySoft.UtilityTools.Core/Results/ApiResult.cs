using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Core.Results;

/// <summary>
/// ApiResult
/// </summary>
public class ApiResult : ApiResult<object, object>, IApiResult
{
    /// <summary>
    /// CustomDataResult
    /// </summary>
    /// <param name="code"></param>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="extraData"></param>
    public ApiResult(
        ReturnCode code = ReturnCode.Ok,
        bool success = true,
        string message = "success",
        object? data = default,
        object? extraData = default
    ) : base(code, success, message, data, extraData)
    {
    }

    /// <summary>
    /// CustomDataResult
    /// </summary>
    /// <param name="code"></param>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="extraData"></param>
    public ApiResult(
        int code = 200,
        bool success = true,
        string message = "success",
        object? data = default,
        object? extraData = default
    ) : base(code, success, message, data, extraData)
    {
    }
}

/// <summary>
/// ApiResult
/// </summary>
public class ApiResult<TData> : ApiResult<TData, object>, IApiResult<TData>
{
    /// <summary>
    /// CustomDataResult
    /// </summary>
    /// <param name="code"></param>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="extraData"></param>
    public ApiResult(
        ReturnCode code = ReturnCode.Ok,
        bool success = true,
        string message = "success",
        TData? data = default,
        object? extraData = default
    ) : base(code, success, message, data, extraData)
    {
    }

    /// <summary>
    /// CustomDataResult
    /// </summary>
    /// <param name="code"></param>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="extraData"></param>
    public ApiResult(
        int code = 200,
        bool success = true,
        string message = "success",
        TData? data = default,
        object? extraData = default
    ) : base(code, success, message, data, extraData)
    {
    }
}

/// <summary>
/// CustomDataResult
/// </summary>
public class ApiResult<TData, TExtraData> : ActionResult, IApiResult<TData, TExtraData>
{
    private bool _camelCase = true;

    /// <summary>
    /// 返回码
    /// </summary>
    [Description("返回码")]
    public int Code { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    [Description("是否成功")]
    public bool Success { get; set; }

    /// <summary>
    /// 消息文本
    /// </summary>
    [Description("消息文本")]
    public string Message { get; set; }

    /// <summary>
    /// 主要数据
    /// </summary>
    [Description("主要数据")]
    public TData? Data { get; set; }

    /// <summary>
    /// 扩展数据
    /// </summary>
    [Description("扩展数据")]
    public TExtraData? ExtraData { get; set; }

    /// <summary>
    /// CustomDataResult
    /// </summary>
    /// <param name="code"></param>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="extraData"></param>
    public ApiResult(
        ReturnCode code = ReturnCode.Ok,
        bool success = true,
        string message = "success",
        TData? data = default,
        TExtraData? extraData = default
    )
    {
        Code = code.ToInt();
        Success = success;
        Message = message;
        Data = data;
        ExtraData = extraData;
    }

    /// <summary>
    /// CustomDataResult
    /// </summary>
    /// <param name="code"></param>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="extraData"></param>
    public ApiResult(
        int code = 200,
        bool success = true,
        string message = "success",
        TData? data = default,
        TExtraData? extraData = default
    )
    {
        Code = code;
        Success = success;
        Message = message;
        Data = data;
        ExtraData = extraData;
    }

    /// <summary>
    /// set camelCase
    /// </summary>
    /// <param name="camelCase"></param>
    /// <returns></returns>
    public void SetCamelCase(bool camelCase)
    {
        _camelCase = camelCase;
    }

    /// <summary>
    /// 获取是否使用 camelCase
    /// </summary>
    /// <returns></returns>
    public bool GetCamelCase()
    {
        return _camelCase;
    }
}