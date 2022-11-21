using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

/// <summary>
/// 
/// </summary>
public static class WhetherExtensionMethods
{
    /// <summary>
    /// ToBool
    /// </summary>
    /// <param name="whether"></param>
    /// <returns></returns>
    public static bool ToBool(this Whether whether)
    {
        return whether == Whether.Yes;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="whether"></param>
    /// <returns></returns>
    public static int ToInt(this Whether whether)
    {
        return (int)whether;
    }
}