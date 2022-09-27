namespace EasySoft.Core.EntityFramework.Assists;

public class FlagAssist
{
    private static bool _entityFrameworkSwitch;

    static FlagAssist()
    {
        _entityFrameworkSwitch = false;
    }

    public static void SetEntityFrameworkSwitchOpen()
    {
        _entityFrameworkSwitch = true;
    }

    public static bool GetEntityFrameworkSwitch()
    {
        return _entityFrameworkSwitch;
    }
}