using EasySoft.Core.Cap.Assists;

namespace EasySoft.Core.AccessWayTransmitter;

public static class Configures
{
    private const string QueryName = "AccessWay";

    public static string GetQueryName()
    {
        var prefix = CapAssist.GetConfig().Prefix;

        return string.IsNullOrWhiteSpace(prefix) ? QueryName : $"{prefix}.{QueryName}";
    }
}