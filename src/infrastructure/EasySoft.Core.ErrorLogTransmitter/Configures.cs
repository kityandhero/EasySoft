using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.ErrorLogTransmitter;

public static class Configures
{
    private const string QueryName = "ErrorLog";

    public static string GetQueryName()
    {
        var prefix = RabbitMQConfigAssist.GetPrefix().Remove(" ").Trim();

        return string.IsNullOrWhiteSpace(prefix) ? QueryName : $"{prefix}.{QueryName}";
    }
}