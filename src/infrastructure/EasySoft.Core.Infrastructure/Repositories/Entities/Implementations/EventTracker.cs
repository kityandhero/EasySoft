namespace EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;

/// <summary>
/// 事件跟踪
/// </summary>
[Description("事件跟踪")]
public class EventTracker : Entity
{
    /// <summary>
    /// 事件标识
    /// </summary>
    [Description("事件标识")]
    public long EventId { get; set; }

    /// <summary>
    /// 跟踪名称
    /// </summary>
    [Description("跟踪名称")]
    public string TrackerName { get; set; } = string.Empty;
}