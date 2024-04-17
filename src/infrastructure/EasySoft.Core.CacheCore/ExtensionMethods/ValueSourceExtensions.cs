using EasySoft.Core.CacheCore.Enums;

namespace EasySoft.Core.CacheCore.ExtensionMethods;

public static class ValueSourceExtensions
{
    public static int ToInt(this ValueSource valueSource)
    {
        return (int)valueSource;
    }
}