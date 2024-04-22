using EasySoft.Core.Sql.Interfaces;
using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.Core.Dapper.Base;

public abstract class BaseEntity<T> : IEntitySelf<T> where T : BaseEntity<T>
{
    [AdvanceColumnInformation("主键标识")]
    [AdvanceColumnMapper(Constants.DefaultTablePrimaryKey)]
    public long Id { get; set; } = 0;

    public abstract Expression<Func<T, object>> GetPrimaryKeyLambda();

    public string GetPrimaryKeyName()
    {
        var lambda = GetPrimaryKeyLambda();

        var advanceColumnMapperAttribute = Tools.GetAdvanceColumnMapperAttribute(
            EntityAssist.GetPropertyInfo(lambda)
        );

        return advanceColumnMapperAttribute?.Name ?? "";
    }

    public virtual string GetSqlSchemaName()
    {
        return ConstantSqlServer.DefaultSchemaName;
    }

    public virtual string GetSqlFieldStringValueDecorateStart()
    {
        return "'";
    }

    public virtual string GetSqlFieldStringValueDecorateEnd()
    {
        return "'";
    }

    public virtual string GetSqlFieldDecorateStart()
    {
        return "[";
    }

    public virtual string GetSqlFieldDecorateEnd()
    {
        return "]";
    }
}