namespace EasySoft.Core.JsonWebToken.ExtensionMethods;

public static class LongExtensions
{
    public static string ToJsonWebToken(long identification)
    {
        return identification.ToString().ToJsonWebToken();
    }
}