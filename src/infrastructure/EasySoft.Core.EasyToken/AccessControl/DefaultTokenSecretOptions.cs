using EasySoft.Core.AuthenticationCore.Assists;

namespace EasySoft.Core.EasyToken.AccessControl;

public class DefaultTokenSecretOptions : ITokenSecretOptions
{
    #region Fields

    public AuthenticationConfigureAssist AuthenticationConfigureAssist { get; }

    #endregion

    public DefaultTokenSecretOptions(AuthenticationConfigureAssist authenticationConfigureAssist)
    {
        AuthenticationConfigureAssist = authenticationConfigureAssist;
    }

    public string GetKey()
    {
        var easyTokenSecretKeyConfigValue = AuthenticationConfigureAssist.GetEasyTokenSecretKey();

        return easyTokenSecretKeyConfigValue.Value;
    }
}