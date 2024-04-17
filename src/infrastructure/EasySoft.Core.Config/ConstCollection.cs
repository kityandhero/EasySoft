namespace EasySoft.Core.Config;

/// <summary>
/// 常量集合
/// </summary>
public static class ConstCollection
{
    /// <summary>
    /// TokenExpiresKey
    /// </summary>
    public const string TokenExpiresKey = "TokenExpires";

    /// <summary>
    /// NLogJsonConfig
    /// </summary>
    public const string NLogJsonConfig = "NLogJsonConfig";

    /// <summary>
    /// GetDynamicConfigKeyCollection
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> GetDynamicConfigKeyCollection()
    {
        return new[] { TokenExpiresKey, NLogJsonConfig };
    }
}