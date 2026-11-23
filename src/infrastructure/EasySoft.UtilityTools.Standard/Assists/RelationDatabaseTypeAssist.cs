using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Exceptions;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// RelationDatabaseTypeAssist
/// </summary>
public static class RelationDatabaseTypeAssist
{
    /// <summary>
    /// GetRelationDatabaseType
    /// </summary>
    /// <returns></returns>
    public static RelationDatabaseType GetRelationDatabaseType(string type)
    {
        var value = EnumAssist.GetValueByName<RelationDatabaseType>(type);

        if (value == null)
        {
            throw new UnhandledException("无法将字符串转换为枚举值");
        }

        return (RelationDatabaseType)value;
    }
}