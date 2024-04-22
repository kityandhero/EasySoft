using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Metas;

namespace EasySoft.UtilityTools.Standard.Models;

/// <summary>
/// 渠道定义
/// </summary>
[Description("渠道模型")]
public class Channel : EnumerationString, IChannel
{
    /// <summary>
    /// Unknown
    /// </summary>
    public static readonly Channel Unknown = new(
        "",
        "Unknown",
        "未知来源"
    );

    /// <summary>
    /// Channel
    /// </summary>
    /// <param name="value"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public Channel(string value, string name = "", string description = "") : base(
        value,
        name,
        description
    )
    {
    }
}