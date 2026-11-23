namespace EasySoft.Core.AdvanceTrace.Defines;

/// <summary>
/// 追踪源定义，统一供给链路使用
/// </summary>
public static class TraceSourceDefine
{
    public static readonly ActivitySource Source = new(
        "EasySoft.Core.AdvanceTrace"
    );
}