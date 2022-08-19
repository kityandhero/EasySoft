using ValueType = EasySoft.Core.ExchangeRegulation.Enums.ValueType;

namespace EasySoft.Core.ExchangeRegulation.ExtensionMethods;

public static class LogValueTypeExtensions
{
    public static int ToInt(this ValueType source)
    {
        return (int)source;
    }
}