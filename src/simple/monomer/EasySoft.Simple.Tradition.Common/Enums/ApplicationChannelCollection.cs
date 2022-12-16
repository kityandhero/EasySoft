namespace EasySoft.Simple.Tradition.Common.Enums;

/// <summary>
/// ApplicationChannelCollection
/// </summary>
public enum ApplicationChannelCollection
{
    /// <summary>
    /// 客户端接口
    /// </summary>
    [Description("客户端接口")]
    ClientWebApi = 100,

    /// <summary>
    /// 管理端接口
    /// </summary>
    [Description("管理端接口")]
    ManagementWebApi = 200
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