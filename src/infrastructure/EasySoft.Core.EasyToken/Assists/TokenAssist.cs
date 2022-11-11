using EasySoft.Core.EasyToken.AccessControl;

namespace EasySoft.Core.EasyToken.Assists;

public static class TokenAssist
{
    public static string GenerateEasyToken(string identification)
    {
        var tokenSecret = AutofacAssist.Instance.Resolve<ITokenSecret>();

        var token = tokenSecret.EncryptWithExpirationTime(
            identification,
            new TimeSpan(TimeSpan.TicksPerSecond * DynamicConfigAssist.GetTokenExpires())
        );

        if (!GeneralConfigAssist.GetTokenServerDumpSwitch()) return token;

        var asyncCacheOperator = AutofacAssist.Instance.Resolve<IAsyncCacheOperator>();

        var key = Guid.NewGuid().ToString().Remove("-").ToLower();

        asyncCacheOperator.SetAsync(
            key,
            token,
            new TimeSpan(TimeSpan.TicksPerSecond * DynamicConfigAssist.GetTokenExpires())
        );

        return key;
    }
}