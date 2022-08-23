using EasySoft.Core.JsonWebToken.Assists;

namespace EasySoft.Core.JsonWebToken.ExtensionMethods;

public static class StringExtensions
{
    public static string ToJsonWebToken(this string identification)
    {
        return TokenAssist.GenerateJsonWebToken(identification);
    }
}