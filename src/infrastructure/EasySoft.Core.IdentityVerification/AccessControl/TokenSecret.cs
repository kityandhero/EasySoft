namespace EasySoft.Core.IdentityVerification.AccessControl;

public class TokenSecret : Secret, ITokenSecret
{
    public TokenSecret(ITokenSecretOptions secretOptions) : base(secretOptions)
    {
    }
}