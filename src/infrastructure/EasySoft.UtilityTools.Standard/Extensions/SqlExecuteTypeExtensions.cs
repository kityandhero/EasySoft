using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// SqlExecuteTypeExtensions
/// </summary>
public static class SqlExecuteTypeExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int ToInt(this SqlExecuteType source)
    {
        return (int)source;
    }
}