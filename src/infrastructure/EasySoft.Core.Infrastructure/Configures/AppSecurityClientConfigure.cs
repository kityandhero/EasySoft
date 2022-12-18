namespace EasySoft.Core.Infrastructure.Configures;

/// <summary>
/// AppSecurityClientConfigure
/// </summary>
public class AppSecurityClientConfigure
{
    private static string _publicKey = "";

    /// <summary>
    /// GetPublicKey
    /// </summary>
    public static string GetPublicKey()
    {
        return _publicKey;
    }

    /// <summary>
    /// SetPublicKey
    /// </summary>
    /// <param name="publicKey"></param>
    public static void SetPublicKey(string publicKey)
    {
        _publicKey = publicKey.Trim().Remove(" ");
    }
}