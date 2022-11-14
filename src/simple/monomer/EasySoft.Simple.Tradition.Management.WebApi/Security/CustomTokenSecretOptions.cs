using EasySoft.Core.EasyToken.AccessControl;

namespace EasySoft.Simple.Tradition.Management.WebApi.Security;

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
        return "3535a5cc6cd541e08a1230a76c2c6993";
    }
}