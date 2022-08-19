using EasySoft.Core.Config.ConfigAssist;

namespace EasySoft.Core.Web.Framework.CommonAssists;

internal static class FlagAssist
{
    internal static bool TokenSecretOptionIsDefault { get; set; }
    internal static bool TokenSecretOptionInjectionComplete { get; set; }

    internal static bool TokenSecretInjectionComplete { get; set; }

    internal static bool ApplicationChannelInjectionComplete { get; set; }

    internal static bool ApplicationChannelIsDefault { get; set; }

    static FlagAssist()
    {
        TokenSecretOptionIsDefault = false;
        TokenSecretOptionInjectionComplete = false;
        TokenSecretInjectionComplete = false;
        ApplicationChannelInjectionComplete = false;
        ApplicationChannelIsDefault = false;
    }

    public static bool GetRemoteLogSwitch()
    {
        return GeneralConfigAssist.GetRemoteErrorLogEnable() || GeneralConfigAssist.GetRemoteGeneralLogEnable();
    }
}