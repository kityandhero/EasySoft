using EasySoft.Core.EasyToken.AccessControl;

namespace EasySoft.Simple.Single.Application.EasyTokens;

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