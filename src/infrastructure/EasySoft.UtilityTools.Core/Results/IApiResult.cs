using System.ComponentModel;

namespace EasySoft.UtilityTools.Core.Results;

/// <summary>
/// IApiResult
/// </summary>
public interface IApiResult : IApiResult<object, object>
{
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TData"></typeparam>
/// <typeparam name="TExtraData"></typeparam>
public interface IApiResult<TData, TExtraData> : IActionResult
{
    /// <summary>
    /// 返回码
    /// </summary>
    [Description("返回码")]
    int Code { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    [Description("是否成功")]
    bool Success { get; set; }

    /// <summary>
    /// 消息文本
    /// </summary>
    [Description("消息文本")]
    string Message { get; set; }

    /// <summary>
    /// 主要数据
    /// </summary>
    [Description("主要数据")]
    TData? Data { get; set; }

    /// <summary>
    /// 扩展数据
    /// </summary>
    [Description("扩展数据")]
    TExtraData? ExtraData { get; set; }

    /// <summary>
    /// Set CamelCase
    /// </summary>
    /// <param name="camelCase"></param>
    /// <returns></returns>
    void SetCamelCase(bool camelCase);

    /// <summary>
    /// Get CamelCase
    /// </summary>
    /// <returns></returns>  
    bool GetCamelCase();
}