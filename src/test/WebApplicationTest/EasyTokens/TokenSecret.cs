using EasySoft.Core.EasyToken.AccessControl;

namespace WebApplicationTest.EasyTokens;

public class CustomTokenSecret : TokenSecret
{
    public CustomTokenSecret(ITokenSecretOptions secretOptions) : base(secretOptions)
    {
    }
}