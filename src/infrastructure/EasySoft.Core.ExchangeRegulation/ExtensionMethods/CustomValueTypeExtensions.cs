using EasySoft.Core.ExchangeRegulation.Enums;

namespace EasySoft.Core.ExchangeRegulation.ExtensionMethods;

public static class CustomValueTypeExtensions
{
    public static int ToInt(this CustomValueType source)
    {
        return (int)source;
    }
}