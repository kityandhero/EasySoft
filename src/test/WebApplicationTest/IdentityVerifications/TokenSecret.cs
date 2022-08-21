using EasySoft.Core.IdentityVerification.AccessControl;

namespace WebApplicationTest.IdentityVerifications;

public class CustomTokenSecret : TokenSecret
{
    public CustomTokenSecret(ITokenSecretOptions secretOptions) : base(secretOptions)
    {
    }
}