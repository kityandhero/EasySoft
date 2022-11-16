using EasySoft.UtilityTools.Core.Securities.implementations;
using EasySoft.UtilityTools.Core.Securities.Interfaces;

namespace EasySoft.UtilityTools.Core.Assists;

/// <summary>
/// 应用综合辅助
/// </summary>
public static class TreatAssist
{
    /// <summary>
    /// Security
    /// </summary>
    public static ISecurity Security => new Security();

    /// <summary>
    /// Hash
    /// </summary>
    public static IHashGenerator Hash => new HashGenerator();

    /// <summary>
    /// Accessor
    /// </summary>
    public static IAccessor Accessor => new Accessor();
}