using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// AdvanceSqlBuilderFieldsExtensions
/// </summary>
public static class AdvanceSqlBuilderFieldsExtensions
{
    /// <summary>
    /// Fields
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambdas"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder Fields<T>(
        this AdvanceSqlBuilder builder,
        params Expression<Func<T, object>>[] propertyLambdas
    )
    {
        var list = FieldItemSpecialFactory.BuildFieldItems(propertyLambdas);

        return builder.Fields(list.ToArray());
    }

    /// <summary>
    /// Fields
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fieldItemSpecials"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder Fields<T>(
        this AdvanceSqlBuilder builder,
        params FieldItemSpecial<T>[] fieldItemSpecials
    )
    {
        var sql = builder.Sql;

        var r = new List<string>();

        foreach (var fieldItemSpecial in fieldItemSpecials)
        {
            var transferResult = TransferAssist.TransferSumField(fieldItemSpecial);

            r.Add(transferResult);
        }

        sql = "{0} {1}".FormatValue(sql, r.Join(","));

        builder.Sql = sql;

        return builder;
    }
}