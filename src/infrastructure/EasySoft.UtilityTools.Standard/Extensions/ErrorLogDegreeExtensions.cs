using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// ErrorLogExchangeDegreeExtensions
/// </summary>
public static class ErrorLogDegreeExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int ToInt(this ErrorLogDegree source)
    {
        return (int)source;
    }
}