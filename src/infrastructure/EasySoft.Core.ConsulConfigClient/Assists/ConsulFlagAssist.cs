namespace EasySoft.Core.ConsulConfigClient.Assists;

public static class ConsulFlagAssist
{
    private static bool InitializeRegistrationComplete { get; set; }

    private static bool InitializeConfigComplete { get; set; }

    static ConsulFlagAssist()
    {
        InitializeRegistrationComplete = false;
        InitializeConfigComplete = false;
    }

    public static bool GetInitializeRegistrationWhetherComplete()
    {
        return InitializeRegistrationComplete;
    }

    public static void SetInitializeRegistrationComplete()
    {
        InitializeRegistrationComplete = true;
    }

    public static bool GetInitializeConfigWhetherComplete()
    {
        return InitializeConfigComplete;
    }

    public static void SetInitializeConfigComplete()
    {
        InitializeConfigComplete = true;
    }
}