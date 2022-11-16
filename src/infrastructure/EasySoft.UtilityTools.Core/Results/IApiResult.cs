namespace EasySoft.UtilityTools.Core.Results;

/// <summary>
/// IApiResult
/// </summary>
public interface IApiResult : IActionResult
{
    /// <summary>
    /// 返回码
    /// </summary>
    int Code { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    bool Success { get; set; }

    /// <summary>
    /// 消息文本
    /// </summary>
    string Message { get; set; }

    /// <summary>
    /// 主要数据
    /// </summary>
    object? Data { get; set; }

    /// <summary>
    /// 扩展数据
    /// </summary>
    object? ExtraData { get; set; }

    /// <summary>
    /// Set CamelCase
    /// </summary>
    /// <param name="camelCase"></param>
    /// <returns></returns>
    ApiResult SetCamelCase(bool camelCase);

    /// <summary>
    /// Get CamelCase
    /// </summary>
    /// <returns></returns>  
    bool GetCamelCase();
}