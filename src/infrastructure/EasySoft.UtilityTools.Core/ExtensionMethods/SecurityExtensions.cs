using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Enums;
using EasySoft.UtilityTools.Core.Securities.Interfaces;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

/// <summary>
/// SecurityExtensions
/// </summary>
public static class SecurityExtensions
{
    /// <summary>
    /// get MD5 hashed string
    /// </summary>
    /// <param name="_"></param>
    /// <param name="sourceString">原字符串</param>
    /// <param name="isLower">加密后的字符串是否为小写</param>
    /// <returns>加密后字符串</returns>
    public static string Md5(this ISecurity _, string sourceString, bool isLower = false)
    {
        return string.IsNullOrEmpty(sourceString)
            ? ""
            : TreatAssist.Hash.GetHashedString(HashType.Md5, sourceString, isLower);
    }
}