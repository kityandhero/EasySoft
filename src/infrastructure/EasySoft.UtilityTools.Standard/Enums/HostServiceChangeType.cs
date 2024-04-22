namespace EasySoft.UtilityTools.Standard.Enums;

/// <summary>
/// 驻留服务变动类型
/// </summary>
public enum HostServiceChangeType
{
    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")]
    Other = 0,

    /// <summary>
    /// 启动
    /// </summary>
    [Description("启动")]
    Start = 1,

    /// <summary>
    /// 停止
    /// </summary>
    [Description("停止")]
    Stop = 2,

    /// <summary>
    /// 重启
    /// </summary>
    [Description("重启")]
    Restart = 3
}