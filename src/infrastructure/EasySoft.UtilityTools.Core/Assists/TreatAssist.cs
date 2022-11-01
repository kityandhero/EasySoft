using EasySoft.UtilityTools.Core.Securities.implementations;
using EasySoft.UtilityTools.Core.Securities.Interfaces;

namespace EasySoft.UtilityTools.Core.Assists;

/// <summary>
/// 应用综合辅助
/// </summary>
public static class TreatAssist
{
    public static ISecurity Security => new Security();

    public static IHashGenerator Hash => new HashGenerator();

    public static IAccessor Accessor => new Accessor();
}