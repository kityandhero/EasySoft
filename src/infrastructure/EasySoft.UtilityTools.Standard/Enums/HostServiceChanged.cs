namespace EasySoft.UtilityTools.Standard.Enums;

/// <summary>
/// 服务是否已变更
/// </summary>
public enum HostServiceChanged
{
    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")]
    Other = -1,

    /// <summary>
    /// 未变更
    /// </summary>
    [Description("未变更")]
    No = 0,

    /// <summary>
    /// 已变更
    /// </summary>
    [Description("已变更")]
    Yes = 1,

    /// <summary>
    /// 刷新状态
    /// </summary>
    [Description("刷新状态")]
    RefreshState = 3
}