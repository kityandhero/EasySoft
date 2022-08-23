using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.EasyToken.Assists;

public static class TokenAssist
{
    public static string GenerateEasyToken(string identification)
    {
        var tokenSecret = AutofacAssist.Instance.Resolve<ITokenSecret>();

        var token = tokenSecret.EncryptWithExpirationTime(
            identification,
            new TimeSpan(TimeSpan.TicksPerSecond * GeneralConfigAssist.GetTokenExpires())
        );

        if (!GeneralConfigAssist.GetTokenServerDumpSwitch())
        {
            return token;
        }

        var asyncCacheOperator = AutofacAssist.Instance.Resolve<IAsyncCacheOperator>();

        var key = Guid.NewGuid().ToString().Remove("-").ToLower();

        asyncCacheOperator.SetAsync(
            key,
            token,
            new TimeSpan(TimeSpan.TicksPerSecond * GeneralConfigAssist.GetTokenExpires())
        );

        return key;
    }
}