namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// PathAssist
/// </summary>
public static class PathAssist
{
    /// <summary>
    /// Combine
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static string Combine(string source, string target)
    {
        return Path.Combine(source, target).Replace("\\", "/");
    }
}