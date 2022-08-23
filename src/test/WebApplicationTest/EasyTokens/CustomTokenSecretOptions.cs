using EasySoft.Core.EasyToken.AccessControl;

namespace WebApplicationTest.EasyTokens;

public class CustomTokenSecretOptions : ITokenSecretOptions
{
    public string GetKey()
    {
        return "pqy7854skiosj7p4c74uiyo804tzecr8";
    }
}