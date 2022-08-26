using AgileConfig.Client;

namespace EasySoft.Core.AgileConfigClient.Assists;

public static class ActionAssist
{
    public static Action<ConfigChangedArg> ActionAgileConfigChanged { get; set; } = e => { };
}