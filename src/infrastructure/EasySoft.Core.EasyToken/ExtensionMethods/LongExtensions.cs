namespace EasySoft.Core.EasyToken.ExtensionMethods;

public static class LongExtensions
{
    public static string ToEasyToken(this long identification)
    {
        return identification.ToString().ToEasyToken();
    }
}