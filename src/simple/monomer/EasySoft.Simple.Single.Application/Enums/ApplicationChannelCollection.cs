using System.ComponentModel;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Simple.Distribute.Common.Enums;

/// <summary>
/// ApplicationChannelCollection
/// </summary>
public enum ApplicationChannelCollection
{
    /// <summary>
    /// TestApplication
    /// </summary>
    [Description("测试应用")]
    TestApplication = 100
}

/// <summary>
/// 
/// </summary>
public static class ApplicationChannelCollectionExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int ToInt(this ApplicationChannelCollection source)
    {
        return (int)source;
    }

    /// <summary>
    /// GetDescription
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string GetDescription(this ApplicationChannelCollection source)
    {
        var descriptionAttribute = source.GetAttribute<DescriptionAttribute>();

        return descriptionAttribute == null ? "" : descriptionAttribute.Description;
    }

    /// <summary>
    /// ToApplicationChannel
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IApplicationChannel ToApplicationChannel(this ApplicationChannelCollection source)
    {
        return new ApplicationChannel().SetChannel(source.ToInt()).SetName(source.GetDescription());
    }
}