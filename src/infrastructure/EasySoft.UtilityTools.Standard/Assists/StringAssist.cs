namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// StringAssist
/// </summary>
public static class StringAssist
{
    /// <summary>
    /// 集合中全部为空字符串
    /// </summary>
    /// <param name="list"></param>  
    /// <returns></returns>
    public static bool IsAllNullOrWhiteSpace(params string?[] list)
    {
        return !list.Any() || list.All(string.IsNullOrWhiteSpace);
    }

    /// <summary>
    /// 集合中存在任意空字符串
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsAnyNullOrWhiteSpace(params string?[] list)
    {
        return list.Any() && list.Any(string.IsNullOrWhiteSpace);
    }
}