namespace EasySoft.Core.EasyToken.ExtensionMethods;

public static class LongExtensions
{
    public static string ToEasyToken(long identification)
    {
        return identification.ToString().ToEasyToken();
    }
}