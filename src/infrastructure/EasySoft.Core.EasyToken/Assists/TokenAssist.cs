using EasySoft.Core.DependencyInjection.IocAssists;
using EasySoft.Core.EasyToken.AccessControl;

namespace EasySoft.Core.EasyToken.Assists;

public static class TokenAssist
{
    public static string GenerateEasyToken(string identification)
    {
        var tokenSecret = ServiceProviderAssist.Instance.Resolve<ITokenSecret>();

        var token = tokenSecret.EncryptWithExpirationTime(
            identification,
            new TimeSpan(TimeSpan.TicksPerSecond * DynamicConfigAssist.GetTokenExpires())
        );

        if (!GeneralConfigAssist.GetTokenServerDumpSwitch())
        {
            return token;
        }

        var cacheOperator = ServiceProviderAssist.Instance.Resolve<ICacheOperator>();

        var key = Guid.NewGuid().ToString().Remove("-").ToLower();

        cacheOperator.Set(
            key,
            token,
            new TimeSpan(TimeSpan.TicksPerSecond * DynamicConfigAssist.GetTokenExpires())
        );

        return key;
    }
}