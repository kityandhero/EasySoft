using System.ComponentModel;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace WebApplicationTest.Enums;

public enum ApplicationChannelCollection
{
    [Description("测试应用")]
    TestApplication = 100
}

public static class ApplicationChannelCollectionExtensions
{
    public static int ToInt(this ApplicationChannelCollection source)
    {
        return (int)source;
    }

    public static string GetDescription(this ApplicationChannelCollection source)
    {
        var descriptionAttribute = source.GetAttribute<DescriptionAttribute>();

        return descriptionAttribute.Description;
    }
}