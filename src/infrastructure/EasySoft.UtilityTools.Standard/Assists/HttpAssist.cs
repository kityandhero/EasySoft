namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// HttpAssist
/// </summary>
public static class HttpAssist
{
    /// <summary>
    /// UrlEncode
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string UrlEncode(string source)
    {
        return System.Web.HttpUtility.UrlEncode(source);
    }

    /// <summary>
    /// UrlDecode
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string UrlDecode(string source)
    {
        return System.Web.HttpUtility.UrlDecode(source);
    }
}