namespace EasySoft.Core.Infrastructure.Assists;

public static class FlagAssist
{
    public static bool CovertInjectionComplete { get; set; }

    public static bool EasyTokenSwitch { get; set; }

    public static bool EasyTokenMiddlewareModeSwitch { get; set; }

    public static bool EasyTokenSecretOptionInjectionComplete { get; set; }

    public static bool EasyTokenSecretInjectionComplete { get; set; }

    public static bool JsonWebTokenSwitch { get; set; }

    public static bool JsonWebTokenMiddlewareModeSwitch { get; set; }

    public static bool PermissionVerificationSwitch { get; set; }

    public static bool PermissionVerificationMiddlewareModeSwitch { get; set; }

    public static bool ApplicationChannelInjectionComplete { get; set; }

    public static bool ApplicationChannelIsDefault { get; set; }

    static FlagAssist()
    {
        CovertInjectionComplete = false;
        EasyTokenSwitch = false;
        EasyTokenMiddlewareModeSwitch = false;
        EasyTokenSecretOptionInjectionComplete = false;
        EasyTokenSecretInjectionComplete = false;
        JsonWebTokenSwitch = false;
        JsonWebTokenMiddlewareModeSwitch = false;
        PermissionVerificationSwitch = false;
        PermissionVerificationMiddlewareModeSwitch = false;
        ApplicationChannelInjectionComplete = false;
        ApplicationChannelIsDefault = false;
    }
}