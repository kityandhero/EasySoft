namespace EasySoft.Core.AdvanceTrace.Attributes;

/// <summary>
/// 排除追踪特性，仅作用于方法。
/// 标记后该方法完全跳过链路流程：不创建 Activity、不采集任何内容。
/// 适合高频内部工具方法、循环内辅助方法。
/// </summary>
[AttributeUsage(
    AttributeTargets.Method,
    AllowMultiple = false,
    Inherited = false
)]
public sealed class TraceRecordExcludeAttribute : Attribute
{
}