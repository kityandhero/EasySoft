using EasySoft.Core.Cap.Assists;

namespace EasySoft.Core.SqlExecutionRecordTransmitter;

public static class Configures
{
    private const string QueryName = "SqlExecutionRecord";

    public static string GetQueryName()
    {
        var prefix = CapAssist.GetConfig().Prefix;

        return string.IsNullOrWhiteSpace(prefix) ? QueryName : $"{prefix}.{QueryName}";
    }
}