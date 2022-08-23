namespace EasySoft.Core.EasyToken.ExtensionMethods;

public static class IntExtensions
{
    public static string ToEasyToken(int identification)
    {
        return identification.ToString().ToEasyToken();
    }
}