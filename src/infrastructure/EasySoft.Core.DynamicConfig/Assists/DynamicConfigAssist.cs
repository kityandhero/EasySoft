using EasySoft.Core.AgileConfigClient.Assists;
using EasySoft.Core.Config;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.DynamicConfig.Assists;

public static class DynamicConfigAssist
{
    /// <summary>
    /// 过期时间 (秒), 默认7200
    /// </summary> 
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int GetTokenExpires()
    {
        var defaultTokenExpires = GeneralConfigAssist.GetTokenExpires();

        if (!GeneralConfigAssist.GetAgileConfigSwitch())
        {
            return defaultTokenExpires;
        }

        var remoteConfigCache = ConfigClientAssist.GetConfigClient().Data;

        var remoteTokenExpires = remoteConfigCache[ConstCollection.TokenExpiresKey] ?? "";

        if (remoteTokenExpires.IsInt(out var value) && value > 0)
        {
            return value;
        }

        return defaultTokenExpires;
    }
}