namespace EasySoft.Core.AdvanceTrace.Options;

/// <summary>
/// 链路全局配置
/// </summary>
public class LogRecordTraceOptions
{
    /// <summary>
    /// 全局总开关：是否允许记录入参与返回值。
    /// 常态 false（仅记录链路骨架），应急排查时临时打开。
    /// 优先级高于方法/类级特性。
    /// </summary>
    public bool EnableRecordData { get; set; } = false;

    /// <summary>
    /// 入参/返回值序列化字符串最大长度，超出后截断
    /// </summary>
    public int MaxDataLength { get; set; } = 2048;
}