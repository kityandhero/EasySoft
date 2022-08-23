namespace EasySoft.Core.EasyToken.ExtensionMethods;

public static class IntExtensions
{
    public static string ToEasyToken(this int identification)
    {
        return identification.ToString().ToEasyToken();
    }
}