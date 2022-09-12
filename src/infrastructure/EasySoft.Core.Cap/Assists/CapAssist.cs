using EasySoft.Core.Cap.CapConfigure;

namespace EasySoft.Core.Cap.Assists;

public static class CapAssist
{
    private static CapConfig _config = new();

    public static CapConfig GetConfig()
    {
        return _config;
    }
}