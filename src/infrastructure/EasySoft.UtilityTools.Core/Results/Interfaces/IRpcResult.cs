namespace EasySoft.UtilityTools.Core.Results.Interfaces;

/// <summary>
/// rpc result
/// </summary>
public interface IRpcResult<T>
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
    /// 
    /// </summary>
    T? Data { get; set; }
}