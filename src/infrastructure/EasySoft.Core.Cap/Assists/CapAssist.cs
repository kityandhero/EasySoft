using EasySoft.Core.Cap.CapConfigure;

namespace EasySoft.Core.Cap.Assists;

public static class CapAssist
{
    private static readonly CapConfig Config = new();

    public static CapConfig GetConfig()
    {
        return Config;
    }
}