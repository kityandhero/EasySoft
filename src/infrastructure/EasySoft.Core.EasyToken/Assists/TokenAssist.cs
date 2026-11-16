using EasySoft.Core.AuthenticationCore.Assists;
using EasySoft.Core.EasyToken.AccessControl;

namespace EasySoft.Core.EasyToken.Assists;

public static class TokenAssist
{
    public static string GenerateEasyToken(string identification)
    {
        var authenticationConfigureAssist = ServiceProviderAssist.Resolve<AuthenticationConfigureAssist>();

        var tokenExpiresConfigValue = authenticationConfigureAssist.GetTokenExpires();

        var tokenSecret = ServiceProviderAssist.Resolve<ITokenSecret>();

        var token = tokenSecret.EncryptWithExpirationTime(
            identification,
            new TimeSpan(TimeSpan.TicksPerSecond * tokenExpiresConfigValue.Value)
        );

        var tokenServerDumpSwitchConfigValue = authenticationConfigureAssist.GetTokenServerDumpSwitch();

        if (!tokenServerDumpSwitchConfigValue.Value)
        {
            return token;
        }

        var cacheOperator = ServiceProviderAssist.Resolve<ICacheOperator>();

        var key = Guid.NewGuid().ToString().Remove("-").ToLower();

        cacheOperator.Set(
            key,
            token,
            new TimeSpan(TimeSpan.TicksPerSecond * tokenExpiresConfigValue.Value)
        );

        return key;
    }
}