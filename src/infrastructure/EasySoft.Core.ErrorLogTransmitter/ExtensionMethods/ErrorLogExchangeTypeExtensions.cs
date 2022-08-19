using EasySoft.Core.ErrorLogTransmitter.Enums;

namespace EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;

public static class GeneralLogExchangeTypeExtensions
{
    public static int ToInt(this ErrorLogExchangeType source)
    {
        return (int)source;
    }
}