using EasySoft.Core.ErrorLogTransmitter.Enums;

namespace EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;

/// <summary>
/// GeneralLogExchangeTypeExtensions
/// </summary>
public static class GeneralLogExchangeTypeExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int ToInt(this ErrorLogExchangeType source)
    {
        return (int)source;
    }
}