using EasySoft.Core.ErrorLogTransmitter.Enums;

namespace EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;

public static class ErrorLogExchangeDegreeExtensions
{
    public static int ToInt(this ErrorLogExchangeDegree source)
    {
        return (int)source;
    }
}