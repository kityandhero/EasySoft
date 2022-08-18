namespace EasySoft.Core.Mvc.Framework.AccessControl;

public class TokenSecret : Secret, ITokenSecret
{
    public TokenSecret(ITokenSecretOptions secretOptions) : base(secretOptions)
    {
    }
}