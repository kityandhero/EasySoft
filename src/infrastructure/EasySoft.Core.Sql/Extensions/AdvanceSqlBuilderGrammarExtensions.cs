using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Enums;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// AdvanceSqlBuilderExtensions
/// </summary>
public static class AdvanceSqlBuilderGrammarExtensions
{
    /// <summary>
    /// Select
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fragment"></param>
    /// <returns></returns>
    public static AdvanceSqlBuilder Select(this AdvanceSqlBuilder builder, string fragment = "")
    {
        var sql = builder.Sql;

        if (string.IsNullOrWhiteSpace(fragment))
        {
            sql += "SELECT ";

            builder.Sql = sql;

            return builder;
        }

        sql += $"SELECT {fragment}";

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fragment"></param>
    /// <returns></returns>
    public static AdvanceSqlBuilder Update(this AdvanceSqlBuilder builder, string fragment = "")
    {
        builder.Sql = string.IsNullOrWhiteSpace(fragment) ? "UPDATE " : $"UPDATE {fragment}";

        return builder;
    }

    /// <summary>
    /// SelectTop
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static AdvanceSqlBuilder Top(this AdvanceSqlBuilder builder, int top)
    {
        if (top <= 0) throw new Exception("top not allow 0");

        var sql = builder.Sql;

        sql += $"SELECT TOP {top} ";

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fragment"></param>
    /// <returns></returns>
    public static AdvanceSqlBuilder Set(this AdvanceSqlBuilder builder, string fragment = "")
    {
        var sql = builder.Sql;

        sql = $" {sql} SET {fragment}";

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// Sum
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="fragment"></param>
    /// <param name="valueWhenNUll"></param>
    /// <returns></returns>
    public static AdvanceSqlBuilder Sum(this AdvanceSqlBuilder builder, string fragment, string valueWhenNUll)
    {
        var sql = builder.Sql;

        sql = "{0} ISNULL(SUM(ISNULL({1},{2})),{2}) ".FormatValue(sql, fragment, valueWhenNUll);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// AppendFragment
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="sqlFragment"></param>
    /// <returns></returns>
    public static AdvanceSqlBuilder AppendFragment(
        this AdvanceSqlBuilder builder,
        string sqlFragment
    )
    {
        var sql = builder.Sql;

        sql = "{0} {1}".FormatValue(sql, sqlFragment);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// From
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder From<T>(
        this AdvanceSqlBuilder builder,
        T model
    ) where T : IEntity
    {
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        tableName = $"{fieldDecorateStart}{tableName}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) tableName = schemaName + "." + tableName;

        var sql = builder.Sql;

        sql = "{0} FROM {1}".FormatValue(sql, tableName);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// FromInnerQuery
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="innerQuery"></param>
    /// <param name="aliasInnerQueryResult"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static AdvanceSqlBuilder FromInnerQuery(
        this AdvanceSqlBuilder builder,
        string innerQuery,
        string aliasInnerQueryResult = "t"
    )
    {
        if (string.IsNullOrWhiteSpace(innerQuery)) throw new Exception("内查询语句不能为空");

        var sql = builder.Sql;

        sql = "{0} FROM ({1}){2}".FormatValue(sql, innerQuery, aliasInnerQueryResult);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// InnerJoin
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder InnerJoin<T>(this AdvanceSqlBuilder builder) where T : IEntity, new()
    {
        var model = new T();

        return builder.InnerJoin(model);
    }

    /// <summary>
    /// InnerJoin
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder InnerJoin<T>(
        this AdvanceSqlBuilder builder,
        T model
    ) where T : IEntity
    {
        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        if (!string.IsNullOrWhiteSpace(schemaName)) tableName = schemaName + "." + tableName;

        var sql = builder.Sql;

        sql = "{0} INNER JOIN {1}".FormatValue(sql, tableName);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// LeftJoin
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder LeftJoin<T>(this AdvanceSqlBuilder builder) where T : IEntity, new()
    {
        var model = new T();

        return builder.LeftJoin(model);
    }

    /// <summary>
    /// LeftJoin
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder LeftJoin<T>(
        this AdvanceSqlBuilder builder,
        T model
    ) where T : IEntity
    {
        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        if (!string.IsNullOrWhiteSpace(schemaName)) tableName = schemaName + "." + tableName;

        var sql = builder.Sql;

        sql = "{0} LEFT JOIN {1}".FormatValue(sql, tableName);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// On
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyLambda2"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder On<T1, T2>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T1>> propertyLambda,
        Expression<Func<T2>> propertyLambda2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type1);

        {
            var m = type1.Create();

            var entity = m as IEntity;

            var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            var t = p1.Split('.');
            p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

            if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";
        }

        var p2 = TransferAssist.GetTableAndColumnName(propertyLambda2, out Type type2);

        {
            var m = type2.Create();

            var entity = m as IEntity;

            var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            var t = p2.Split('.');
            p2 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

            if (!string.IsNullOrWhiteSpace(schemaName)) p2 = $"{schemaName}.{p2}";
        }

        var sql = builder.Sql;

        sql = "{0} ON {1} = {2}".FormatValue(sql, p1, p2);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// On
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder On<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        string p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} ON {1} = '{2}'".FormatValue(sql, p1, p2);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// On
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder On<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        Guid p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} ON {1} = '{2}'".FormatValue(sql, p1, p2.ToString());

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// On
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder On<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        int p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} ON {1} = {2}".FormatValue(sql, p1, p2);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// On
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder On<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        long p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} ON {1} = {2}".FormatValue(sql, p1, p2);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// On
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder On<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        DateTime p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} ON {1} = '{2}'".FormatValue(sql, p1, p2.ToString("yyyy-MM-dd HH:mm:ss"));

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// And
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyLambda2"></param>
    /// <param name="conditionType"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder And<T1, T2>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T1>> propertyLambda,
        Expression<Func<T2>> propertyLambda2,
        ConditionType conditionType = ConditionType.Eq
    ) where T1 : IEntity, new() where T2 : IEntity, new()
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type1);

        {
            var m = type1.Create();

            var entity = m as IEntity;

            var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            var t = p1.Split('.');
            p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

            if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";
        }

        var p2 = TransferAssist.GetTableAndColumnName(propertyLambda2, out Type type2);

        {
            var m = type2.Create();

            var entity = m as IEntity;

            var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            var t = p2.Split('.');
            p2 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

            if (!string.IsNullOrWhiteSpace(schemaName)) p2 = $"{schemaName}.{p2}";
        }

        var sql = builder.Sql;

        sql = "{0} AND {1} {3} {2}".FormatValue(sql, p1, p2, SqlAssist.TranslationConditionType(conditionType));

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// And
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder And<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        string p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} AND {1} = '{2}'".FormatValue(sql, p1, p2);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// And
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder And<T>(this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        Guid p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} AND {1} = '{2}'".FormatValue(sql, p1, p2.ToString());

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// And
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <param name="conditionType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder And<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        int p2,
        ConditionType conditionType = ConditionType.Eq
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} AND {1} {3} {2}".FormatValue(sql, p1, p2, SqlAssist.TranslationConditionType(conditionType));

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// And
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <param name="conditionType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder And<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        long p2,
        ConditionType conditionType = ConditionType.Eq
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} AND {1} {3} {2}".FormatValue(sql, p1, p2, SqlAssist.TranslationConditionType(conditionType));

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// And
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="p2"></param>
    /// <param name="conditionType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder And<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T>> propertyLambda,
        DateTime p2,
        ConditionType conditionType = ConditionType.Eq
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var sql = builder.Sql;

        sql = "{0} AND {1} {3} '{2}'".FormatValue(
            sql,
            p1,
            p2.ToString("yyyy-MM-dd HH:mm:ss"),
            SqlAssist.TranslationConditionType(conditionType)
        );

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// And
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder And<T>(
        this AdvanceSqlBuilder builder,
        Condition<T> condition
    ) where T : IEntity, new()
    {
        var transferCondition = SqlAssist.TransferCondition(condition);

        var sql = builder.Sql;

        sql = "{0} AND {1} ".FormatValue(sql, transferCondition);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// GroupBy
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="group"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder GroupBy<T>(this AdvanceSqlBuilder builder, Group<T> group)
    {
        return builder.GroupBy(group.Expression);
    }

    /// <summary>
    /// GroupBy
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder GroupBy<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T, object>> propertyLambda
    )
    {
        var group = SqlAssist.TransferGroup(propertyLambda);

        var sql = builder.Sql;

        sql = $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql} GROUP BY ")} {group}";

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// AndGroupBy
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="group"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AndGroupBy<T>(
        this AdvanceSqlBuilder builder,
        Group<T> group
    )
    {
        return builder.AndGroupBy(group.Expression);
    }

    /// <summary>
    /// AndGroupBy
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AndGroupBy<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T, object>> propertyLambda
    )
    {
        var group = SqlAssist.TransferGroup(propertyLambda);

        var sql = builder.Sql;

        sql = $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql},")} {group}";

        builder.Sql = sql;

        return builder;
    }
}