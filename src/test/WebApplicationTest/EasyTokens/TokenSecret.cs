using EasySoft.Core.EasyToken.AccessControl;

namespace WebApplicationTest.EasyTokens;

/// <summary>
/// CustomTokenSecret
/// </summary>
public class CustomTokenSecret : TokenSecret
{
    /// <summary>
    /// CustomTokenSecret
    /// </summary>
    /// <param name="secretOptions"></param>
    public CustomTokenSecret(ITokenSecretOptions secretOptions) : base(secretOptions)
    {
    }
}