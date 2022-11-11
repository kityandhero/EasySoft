namespace EasySoft.Core.AgileConfigClient.Assists;

public static class AgileConfigClientActionAssist
{
    public static Action<ConfigChangedArg> ActionAgileConfigChanged { get; set; } = e => { };
}