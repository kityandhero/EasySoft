using EasySoft.Core.EasyToken.Assists;

namespace EasySoft.Core.EasyToken.ExtensionMethods;

public static class StringExtensions
{
    public static string ToEasyToken(this string identification)
    {
        return TokenAssist.GenerateEasyToken(identification);
    }
}