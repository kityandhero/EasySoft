namespace EasySoft.Core.Config;

public static class ConstCollection
{
    public const string TokenExpiresKey = "TokenExpires";

    public const string NLogJsonConfig = "NLogJsonConfig";

    public static IEnumerable<string> GetDynamicConfigKeyCollection()
    {
        return new[] { TokenExpiresKey, NLogJsonConfig };
    }
}