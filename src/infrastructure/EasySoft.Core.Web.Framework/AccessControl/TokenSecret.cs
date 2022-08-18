namespace EasySoft.Core.Web.Framework.AccessControl;

public class TokenSecret : Secret, ITokenSecret
{
    public TokenSecret(ITokenSecretOptions secretOptions) : base(secretOptions)
    {
    }
}