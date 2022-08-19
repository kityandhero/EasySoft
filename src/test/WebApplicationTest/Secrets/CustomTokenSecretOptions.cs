using EasySoft.Core.IdentityVerification.AccessControl;

namespace WebApplicationTest.Secrets;

public class CustomTokenSecretOptions : ITokenSecretOptions
{
    public string GetKey()
    {
        return "pqy7854skiosj7p4c74uiyo804tzecr8";
    }
}