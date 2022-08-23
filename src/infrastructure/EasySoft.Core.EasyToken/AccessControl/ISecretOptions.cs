namespace EasySoft.Core.EasyToken.AccessControl;

public interface ISecretOptions
{
    /// <summary>
    /// key is 32 length string like oqyw7etrkiosj7p4c69s98ed04tzecr7
    /// </summary>
    /// <returns></returns>
    public string GetKey();
}