namespace EasySoft.UtilityTools.Standard.Enums;

/// <summary>
/// 驻留服务状态
/// </summary>
public enum HostServiceStatus
{
    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")]
    Other = 0,

    /// <summary>
    /// 运行
    /// </summary>
    [Description("运行")]
    Run = 1,

    /// <summary>
    /// 停止
    /// </summary>
    [Description("停止")]
    Stop = 2
}