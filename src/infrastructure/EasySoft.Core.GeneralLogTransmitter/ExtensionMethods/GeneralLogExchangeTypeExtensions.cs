using EasySoft.Core.GeneralLogTransmitter.Enums;

namespace EasySoft.Core.GeneralLogTransmitter.ExtensionMethods;

public static class GeneralLogExchangeTypeExtensions
{
    public static int ToInt(this GeneralLogExchangeType source)
    {
        return (int)source;
    }
}