namespace EasySoft.Simple.Dapper.Console.Enums;

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
        var descriptionAttribute = source.GetCustomAttribute<DescriptionAttribute>();

        return descriptionAttribute == null ? "" : descriptionAttribute.Description;
    }
}