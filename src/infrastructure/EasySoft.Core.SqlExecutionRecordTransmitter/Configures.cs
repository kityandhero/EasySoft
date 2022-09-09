using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.SqlExecutionRecordTransmitter;

public static class Configures
{
    private const string QueryName = "SqlExecutionRecord";

    public static string GetQueryName()
    {
        var prefix = RabbitMQConfigAssist.GetPrefix().Remove(" ").Trim();

        if (string.IsNullOrWhiteSpace(prefix))
        {
            return QueryName;
        }

        return $"{prefix}.{QueryName}";
    }
}