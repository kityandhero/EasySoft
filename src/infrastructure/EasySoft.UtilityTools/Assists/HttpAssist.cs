namespace EasySoft.UtilityTools.Assists;

public static class HttpAssist
{
    public static string UrlEncode(string source)
    {
        return System.Web.HttpUtility.UrlEncode(source);
    }

    public static string UrlDecode(string source)
    {
        return System.Web.HttpUtility.UrlDecode(source);
    }
}