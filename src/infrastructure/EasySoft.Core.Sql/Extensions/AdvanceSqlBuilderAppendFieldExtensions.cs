using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// 
/// </summary>
public static class AdvanceSqlBuilderAppendFieldExtensions
{
    /// <summary>
    /// AppendField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <param name="replaceDBNullValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AppendField<T>(
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

        return builder.AppendField(sqlField);
    }

    /// <summary>
    /// AppendField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fieldItem"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AppendField<T>(
        this AdvanceSqlBuilder builder,
        FieldItem<T> fieldItem
    )
    {
        var sql = builder.Sql;

        var transferResult = TransferAssist.TransferField(fieldItem);

        sql = "{0},{1}".FormatValue(sql, transferResult);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// AppendField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <param name="replaceDBNullValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AppendField<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T, object>> propertyLambda,
        string columnAlias = "",
        bool replaceDBNullValue = true
    )
    {
        var sqlField = new FieldItemSpecial<T>(propertyLambda)
        {
            ColumnAlias = columnAlias,
            ReplaceDBNullValue = replaceDBNullValue
        };

        return builder.AppendField(sqlField);
    }

    /// <summary>
    /// AppendField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fieldItemSpecial"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AppendField<T>(
        this AdvanceSqlBuilder builder,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var sql = builder.Sql;

        var transferResult = TransferAssist.TransferField(fieldItemSpecial);

        sql = "{0},{1}".FormatValue(sql, transferResult);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// LinkAssignField
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assignField"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder LinkAssignField<T>(
        this AdvanceSqlBuilder builder,
        AssignField<T> assignField
    ) where T : IEntity, new()
    {
        var sql = builder.Sql;

        var resultTransferAssignUpdate = TransferAssist.TransferAssignField(assignField);

        var connector = SqlAssist.GetAssignFieldConnector(sql);

        sql = "{0} {1} {2}".FormatValue(sql, connector, resultTransferAssignUpdate);

        builder.Sql = sql;

        return builder;
    }
}