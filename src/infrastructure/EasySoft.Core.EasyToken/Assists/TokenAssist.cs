using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyToken.AccessControl;

namespace EasySoft.Core.EasyToken.Assists;

public static class TokenAssist
{
    public static string GenerateEasyToken(string identification)
    {
        var tokenSecret = AutofacAssist.Instance.Resolve<ITokenSecret>();

        return tokenSecret.EncryptWithExpirationTime(
            identification,
            new TimeSpan(TimeSpan.TicksPerSecond * GeneralConfigAssist.GetTokenExpires())
        );
    }
}