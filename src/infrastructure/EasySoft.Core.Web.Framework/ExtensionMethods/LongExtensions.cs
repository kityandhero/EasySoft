namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class LongExtensions
{
    public static string ToToken(this long identification)
    {
        return identification.ToString().ToToken();
    }
}