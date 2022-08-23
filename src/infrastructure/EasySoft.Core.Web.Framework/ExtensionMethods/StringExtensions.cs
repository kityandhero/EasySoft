using EasySoft.Core.EasyToken.ExtensionMethods;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.JsonWebToken.ExtensionMethods;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class StringExtensions
{
    public static string ToToken(this string identification)
    {
        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.EasyToken)
        {
            return identification.ToEasyToken();
        }

        if (FlagAssist.TokenMode == UtilityTools.Standard.ConstCollection.JsonWebToken)
        {
            return identification.ToJsonWebToken();
        }

        throw new Exception("unknown token mode");
    }
}