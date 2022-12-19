using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// GeneralLogExchangeTypeExtensions
/// </summary>
public static class GeneralLogTypeExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int ToInt(this GeneralLogType source)
    {
        return (int)source;
    }
}