namespace EasySoft.Core.Dapper.Enums;

public enum CollectMode
{
    [Description("未知")]
    Unknown = 0,

    [Description("日记记录")]
    LogRecord = 10,

    [Description("MiniProfiler")]
    MiniProfiler = 20
}

public static class CollectModeExtensionMethods
{
    public static int ToInt(this CollectMode collectMode
    )
    {
        return (int)collectMode;
    }
}