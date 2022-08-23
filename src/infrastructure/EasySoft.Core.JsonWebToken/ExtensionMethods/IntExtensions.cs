namespace EasySoft.Core.JsonWebToken.ExtensionMethods;

public static class IntExtensions
{
    public static string ToJsonWebToken(this int identification)
    {
        return identification.ToString().ToJsonWebToken();
    }
}