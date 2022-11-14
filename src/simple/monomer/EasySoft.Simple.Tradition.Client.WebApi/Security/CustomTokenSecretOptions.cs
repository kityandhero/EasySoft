using EasySoft.Core.EasyToken.AccessControl;

namespace EasySoft.Simple.Tradition.Client.WebApi.Security;

/// <summary>
/// CustomTokenSecretOptions
/// </summary>
public class CustomTokenSecretOptions : ITokenSecretOptions
{
    /// <summary>
    /// GetKey
    /// </summary>
    /// <returns></returns>
    public string GetKey()
    {
        return "49ac1bfa54a344fcb744a53d03a3b152";
    }
}