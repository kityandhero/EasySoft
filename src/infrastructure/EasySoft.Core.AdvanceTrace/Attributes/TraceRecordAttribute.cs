namespace EasySoft.Core.AdvanceTrace.Attributes;

/// <summary>
/// 链路追踪标记特性。
/// 可标记在【类】或【方法】上；方法标记优先级高于类标记。
/// </summary>
[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Method,
    AllowMultiple = false,
    Inherited = false
)]
public sealed class TraceRecordAttribute : Attribute
{
    /// <summary>
    /// 是否向上继续查找基类。
    /// 仅标记在【类】上时生效；方法标记忽略此字段。
    /// </summary>
    public bool Inherit { get; set; }

    /// <summary>
    /// 是否在该方法/类上覆盖开启载荷采集。
    /// 注意：最终是否序列化还受全局配置 CapturePayloadSwitch 约束；
    /// 全局关闭时，特性上的开启不生效（防止误开造成高开销）。
    /// </summary>
    public bool CapturePayload { get; set; }
}