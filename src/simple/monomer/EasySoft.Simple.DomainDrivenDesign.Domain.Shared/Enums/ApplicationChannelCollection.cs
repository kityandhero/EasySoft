using System.ComponentModel;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Simple.DomainDrivenDesign.Domain.Shared.Enums;

/// <summary>
///     ApplicationChannelCollection
/// </summary>
public enum ApplicationChannelCollection
{
    /// <summary>
    /// 账户中心
    /// </summary>
    [Description("账户中心")]
    AccountCenter = 100,

    /// <summary>
    /// 客户中心
    /// </summary>
    [Description("客户中心")]
    CustomerCenter = 200,

    /// <summary>
    /// 博客
    /// </summary>
    [Description("博客")]
    WebBlog = 300
}

/// <summary>
/// </summary>
public static class ApplicationChannelCollectionExtensions
{
    /// <summary>
    ///     ToInt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int ToInt(this ApplicationChannelCollection source)
    {
        return (int)source;
    }

    /// <summary>
    ///     GetDescription
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string GetDescription(this ApplicationChannelCollection source)
    {
        var descriptionAttribute = source.GetCustomAttribute<DescriptionAttribute>();

        return descriptionAttribute == null ? "" : descriptionAttribute.Description;
    }

    /// <summary>
    ///     ToApplicationChannel
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IApplicationChannel ToApplicationChannel(this ApplicationChannelCollection source)
    {
        return new ApplicationChannel().SetChannel(source.ToInt()).SetName(source.GetDescription());
    }
}