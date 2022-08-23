namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class IntExtensions
{
    public static string ToToken(this int identification)
    {
        return identification.ToString().ToToken();
    }
}