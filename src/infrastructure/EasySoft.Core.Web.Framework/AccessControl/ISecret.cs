namespace EasySoft.Core.Web.Framework.AccessControl;

public interface ISecret
{
    public string Encrypt(string source);

    public string EncryptWithExpirationTime(string source, TimeSpan timeSpan);

    public string EncryptWithExpirationTime(string source, DateTime expirationTime);

    public string Decrypt(string target);

    public string DecryptWithExpirationTime(string target, out bool expired);

    public string DecryptWithExpirationTime(string target, out bool expired, out DateTime time);
}