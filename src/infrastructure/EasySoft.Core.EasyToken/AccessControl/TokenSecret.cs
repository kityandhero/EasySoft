namespace EasySoft.Core.EasyToken.AccessControl;

public class TokenSecret : Secret, ITokenSecret
{
    public TokenSecret(ITokenSecretOptions secretOptions) : base(secretOptions)
    {
    }
}