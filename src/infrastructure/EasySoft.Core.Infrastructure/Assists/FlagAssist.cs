namespace EasySoft.Core.Infrastructure.Assists;

public static class FlagAssist
{
    public static bool CovertInjectionComplete { get; set; }

    public static bool IdentityVerificationSwitch { get; set; }
    public static bool TokenSecretOptionInjectionComplete { get; set; }

    public static bool TokenSecretInjectionComplete { get; set; }

    public static bool ApplicationChannelInjectionComplete { get; set; }

    public static bool ApplicationChannelIsDefault { get; set; }

    static FlagAssist()
    {
        CovertInjectionComplete = false;
        IdentityVerificationSwitch = false;
        TokenSecretOptionInjectionComplete = false;
        TokenSecretInjectionComplete = false;
        ApplicationChannelInjectionComplete = false;
        ApplicationChannelIsDefault = false;
    }
}