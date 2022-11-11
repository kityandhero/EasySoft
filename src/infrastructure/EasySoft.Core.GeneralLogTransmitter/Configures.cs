namespace EasySoft.Core.GeneralLogTransmitter;

public static class Configures
{
    private const string QueryName = "GeneralLog";

    public static string GetQueryName()
    {
        var prefix = CapAssist.GetConfig().Prefix;

        return string.IsNullOrWhiteSpace(prefix) ? QueryName : $"{prefix}.{QueryName}";
    }
}