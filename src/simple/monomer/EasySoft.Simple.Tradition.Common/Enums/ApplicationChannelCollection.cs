using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Simple.Tradition.Common.Enums;

/// <summary>
/// ApplicationChannelCollection
/// </summary>
public abstract class ApplicationChannelCollection
{
    /// <summary>
    /// 客户端接口
    /// </summary>
    public static readonly Channel ClientWebApi = new(
        "8de67b08a9c944a48b6260fbfe590173",
        "ClientWebApi",
        "客户端接口"
    );

    /// <summary>
    /// 管理端接口
    /// </summary>
    public static readonly Channel ManagementWebApi = new(
        "43d9abf1dce54c24a998a0411c9b3a7b",
        "ManagementWebApi",
        "管理端接口"
    );
}