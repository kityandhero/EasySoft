using System.ComponentModel;

namespace EasySoft.Core.CacheCore.Enums;

/// <summary>
/// 数据值的来源
/// </summary>
[Description("数据值的来源")]
public enum ValueSource
{
    /// <summary>
    /// 直接来自数据源
    /// </summary>
    [Description("直接来自数据源")]
    Source = 100,

    /// <summary>
    /// 直接来自缓存
    /// </summary>
    [Description("直接来自缓存")]
    Cache = 200,

    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")]
    Other = 999
}