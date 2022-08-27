namespace EasySoft.Core.Infrastructure.Assists;

public static class FlagAssist
{
    public static bool CovertInjectionComplete { get; set; }

    public static string TokenMode { get; set; }

    public static bool EasyTokenMiddlewareModeSwitch { get; set; }

    public static bool EasyTokenSecretOptionInjectionComplete { get; set; }

    public static bool EasyTokenSecretInjectionComplete { get; set; }

    public static bool EasyTokenConfigComplete { get; set; }

    public static bool JsonWebTokenConfigComplete { get; set; }

    public static bool JsonWebTokenMiddlewareModeSwitch { get; set; }

    public static bool PermissionVerificationSwitch { get; set; }

    public static bool PermissionVerificationMiddlewareModeSwitch { get; set; }

    public static bool ApplicationChannelInjectionComplete { get; set; }

    public static bool ApplicationChannelIsDefault { get; set; }

    public static bool HealthChecksSwitch { get; set; }

    public static bool HealthChecksComplete { get; set; }

    static FlagAssist()
    {
        CovertInjectionComplete = false;
        TokenMode = "";
        EasyTokenMiddlewareModeSwitch = false;
        EasyTokenSecretOptionInjectionComplete = false;
        EasyTokenSecretInjectionComplete = false;
        EasyTokenConfigComplete = false;
        JsonWebTokenConfigComplete = false;
        JsonWebTokenMiddlewareModeSwitch = false;
        PermissionVerificationSwitch = false;
        PermissionVerificationMiddlewareModeSwitch = false;
        ApplicationChannelInjectionComplete = false;
        ApplicationChannelIsDefault = false;
        HealthChecksSwitch = false;
        HealthChecksSwitch = false;
    }
}