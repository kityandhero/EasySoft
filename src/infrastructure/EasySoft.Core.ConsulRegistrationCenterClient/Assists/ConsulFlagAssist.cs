namespace EasySoft.Core.ConsulRegistrationCenterClient.Assists;

internal static class ConsulFlagAssist
{
    private static bool _consulInitializeRegistrationComplete;

    static ConsulFlagAssist()
    {
        _consulInitializeRegistrationComplete = false;
    }

    internal static bool GetInitializeRegistrationWhetherComplete()
    {
        return _consulInitializeRegistrationComplete;
    }

    internal static void SetInitializeRegistrationComplete()
    {
        _consulInitializeRegistrationComplete = true;
    }
}