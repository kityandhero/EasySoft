using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// AdvanceSqlBuilderMinFieldExtensions
/// </summary>
public static class AdvanceSqlBuilderMinFieldExtensions
{
    /// <summary>
    /// MinField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <param name="replaceDBNullValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder MinField<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        string columnAlias = "",
        bool replaceDBNullValue = true
    )
    {
        var sqlField = new FieldItem<T>(propertyLambda)
        {
            ColumnAlias = columnAlias,
            ReplaceDBNullValue = replaceDBNullValue
        };

        return builder.MinField(sqlField);
    }

    /// <summary>
    /// MinField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fieldItem"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder MinField<T>(
        this AdvanceSqlBuilder builder,
        FieldItem<T> fieldItem
    )
    {
        var sql = builder.Sql;

        var transferResult = TransferAssist.TransferMinField(fieldItem);

        sql = "{0} {1}".FormatValue(sql, transferResult);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// MinField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <param name="replaceDBNullValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder MinField<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T, object>> propertyLambda,
        string columnAlias = "",
        bool replaceDBNullValue = true
    )
    {
        var fieldItemSpecial = new FieldItemSpecial<T>(propertyLambda)
        {
            ColumnAlias = columnAlias,
            ReplaceDBNullValue = replaceDBNullValue
        };

        return builder.MinField(fieldItemSpecial);
    }

    /// <summary>
    /// MinField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fieldItemSpecial"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder MinField<T>(
        this AdvanceSqlBuilder builder,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var sql = builder.Sql;

        var transferResult = TransferAssist.TransferMinField(fieldItemSpecial);

        sql = "{0} {1}".FormatValue(sql, transferResult);

        builder.Sql = sql;

        return builder;
    }
}