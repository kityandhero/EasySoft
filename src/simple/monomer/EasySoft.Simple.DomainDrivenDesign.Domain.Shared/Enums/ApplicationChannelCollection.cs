using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Simple.DomainDrivenDesign.Domain.Shared.Enums;

/// <summary>
///     ApplicationChannelCollection
/// </summary>
public abstract class ApplicationChannelCollection
{
    /// <summary>
    /// 账户中心
    /// </summary>
    public static readonly Channel AccountCenter = new(
        "4c1d1b3592cd494d8a4ccba38245c071",
        "AccountCenter",
        "AccountCenter"
    );

    /// <summary>
    /// 客户中心
    /// </summary>
    public static readonly Channel CustomerCenter = new(
        "d8aeca4aa43046c892d7ec43f050703a",
        "CustomerCenter",
        "CustomerCenter"
    );

    /// <summary>
    /// 博客
    /// </summary>
    public static readonly Channel WebBlog = new(
        "7311e5625c674843a1686b8967ab82da",
        "WebBlog",
        "WebBlog"
    );
}