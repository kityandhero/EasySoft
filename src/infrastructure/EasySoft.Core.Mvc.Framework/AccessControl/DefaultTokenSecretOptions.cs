namespace EasySoft.Core.Mvc.Framework.AccessControl;

public class DefaultTokenSecretOptions : ITokenSecretOptions
{
    private const string Key = "oqyw7etrkiosj7p4c69s98ed04tzecr7";

    public string GetKey()
    {
        return Key;
    }
}