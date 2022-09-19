namespace EasySoft.Core.ConsulConfigCenterClient.Assists;

internal static class ConsulFlagAssist
{
    private static bool _consulInitializeConfigComplete;

    static ConsulFlagAssist()
    {
        _consulInitializeConfigComplete = false;
    }

    internal static bool GetInitializeConfigWhetherComplete()
    {
        return _consulInitializeConfigComplete;
    }

    internal static void SetInitializeConfigComplete()
    {
        _consulInitializeConfigComplete = true;
    }
}