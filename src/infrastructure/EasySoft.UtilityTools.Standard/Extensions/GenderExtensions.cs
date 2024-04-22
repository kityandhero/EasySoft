using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// GenderExtensions
/// </summary>
public static class GenderExtensions
{
    /// <summary>
    /// ToInt
    /// </summary>
    /// <param name="gender"></param>
    /// <returns></returns>
    public static int ToInt(this Gender gender)
    {
        return (int)gender;
    }
}