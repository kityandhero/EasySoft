namespace EasySoft.Core.AdvanceTrace.Configs;

/// <summary>
/// 单个方法解析后的追踪配置缓存项。
/// </summary>
internal sealed class MethodTraceConfig
{
    /// <summary>
    /// 是否被排除追踪（命中 LogRecordExcludeAttribute）。
    /// </summary>
    public bool Excluded { get; set; }

    /// <summary>
    /// 是否需要创建链路 Activity。
    /// </summary>
    public bool ShouldTrace { get; set; }

    /// <summary>
    /// 该方法特性上是否显式请求载荷。
    /// </summary>
    public bool CapturePayload { get; set; }
}