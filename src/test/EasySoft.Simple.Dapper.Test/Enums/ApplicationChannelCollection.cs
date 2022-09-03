using System.ComponentModel;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Simple.Dapper.Test.Enums;

public enum ApplicationChannelCollection
{
    [Description("Dapper测试应用")]
    DapperTestApplication = 200
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

        return descriptionAttribute == null ? "" : descriptionAttribute.Description;
    }
}