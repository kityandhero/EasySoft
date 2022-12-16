using EasySoft.Core.ExchangeRegulation.Enums;

namespace EasySoft.Core.ExchangeRegulation.ExtensionMethods;

/// <summary>
/// CustomValueTypeExtensions
/// </summary>
public static class CustomValueTypeExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int ToInt(this CustomValueType source)
    {
        return (int)source;
    }
}