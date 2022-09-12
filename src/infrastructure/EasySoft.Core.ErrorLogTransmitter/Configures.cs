using EasySoft.Core.Cap.Assists;

namespace EasySoft.Core.ErrorLogTransmitter;

public static class Configures
{
    private const string QueryName = "ErrorLog";

    public static string GetQueryName()
    {
        var prefix = CapAssist.GetConfig().Prefix;

        return string.IsNullOrWhiteSpace(prefix) ? QueryName : $"{prefix}.{QueryName}";
    }
}