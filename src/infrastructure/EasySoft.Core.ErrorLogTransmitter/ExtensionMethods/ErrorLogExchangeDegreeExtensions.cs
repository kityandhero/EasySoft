using EasySoft.Core.ErrorLogTransmitter.Enums;

namespace EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;

/// <summary>
/// ErrorLogExchangeDegreeExtensions
/// </summary>
public static class ErrorLogExchangeDegreeExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int ToInt(this ErrorLogExchangeDegree source)
    {
        return (int)source;
    }
}