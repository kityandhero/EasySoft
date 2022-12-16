using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// ReturnCodeExtensionMethods
/// </summary>
public static class ReturnCodeExtensionMethods
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="returnCode"></param>
    /// <returns></returns>
    public static int ToInt(this ReturnCode returnCode)
    {
        return (int)returnCode;
    }
}