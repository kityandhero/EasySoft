using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// GeneralLogExchangeTypeExtensions
/// </summary>
public static class ErrorLogTypeExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int ToInt(this ErrorLogType source)
    {
        return (int)source;
    }
}