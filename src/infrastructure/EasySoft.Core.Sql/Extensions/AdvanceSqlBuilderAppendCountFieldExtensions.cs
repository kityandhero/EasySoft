using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Builders;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// AdvanceSqlBuilderAppendCountFieldExtensions
/// </summary>
public static class AdvanceSqlBuilderAppendCountFieldExtensions
{
    /// <summary>
    /// AppendCountField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AppendCountField<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        string columnAlias = "TotalCount"
    )
    {
        var sql = builder.Sql;

        var transferResult = TransferAssist.TransferCountField(
            propertyLambda,
            columnAlias
        );

        sql = "{0},{1}".FormatValue(sql, transferResult);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// AppendCountField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AppendCountField<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T, object>> propertyLambda,
        string columnAlias = "TotalCount"
    )
    {
        var sql = builder.Sql;

        var transferResult = TransferAssist.TransferCountField(
            propertyLambda,
            columnAlias
        );

        sql = "{0},{1}".FormatValue(sql, transferResult);

        builder.Sql = sql;

        return builder;
    }
}