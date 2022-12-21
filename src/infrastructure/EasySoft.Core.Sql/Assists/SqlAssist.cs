using EasySoft.Core.Infrastructure.Extensions;
using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.Extensions;
using Constants = EasySoft.Core.Sql.Common.Constants;

namespace EasySoft.Core.Sql.Assists;

/// <summary>
/// SqlAssist
/// </summary>
public static class SqlAssist
{
    /// <summary>
    /// create connection
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="relationDatabaseType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IDbConnection CreateConnection(
        string connectionString,
        RelationDatabaseType relationDatabaseType
    )
    {
        if (string.IsNullOrWhiteSpace(connectionString)) throw new Exception("无效的数据库连接");

        DbConnection dbConnection;

        switch (relationDatabaseType)
        {
            case RelationDatabaseType.SqlServer:
                dbConnection = new SqlConnection(connectionString);
                break;

            default:
                throw new Exception("未知的关系型数据库");
        }

        dbConnection.Open();

        var profiledDbConnection = new ProfiledDbConnection(
            dbConnection,
            MiniProfiler.Current
        );

        return profiledDbConnection;
    }

    #region Select

    /// <summary>
    /// SelectCount
    /// </summary>
    /// <param name="fragment"></param>
    /// <param name="columnAlias"></param>
    /// <returns></returns>
    public static string BuildSelectCount(string fragment = "", string columnAlias = "TotalCount")
    {
        if (string.IsNullOrWhiteSpace(fragment))
            return $"SELECT COUNT(*) AS {(string.IsNullOrWhiteSpace(columnAlias) ? "TotalCount" : columnAlias)} ";

        return $"SELECT COUNT(*) AS {(string.IsNullOrWhiteSpace(columnAlias) ? "TotalCount" : columnAlias)} {0}"
            .FormatValue(fragment);
    }

    #region On

    #endregion On

    #region And

    #endregion And

    /// <summary>
    /// TransferCondition
    /// </summary>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferCondition<T>(Condition<T> condition) where T : IEntity, new()
    {
        return TransferAssist.TransferCondition(condition);
    }

    /// <summary>
    /// TransferConditionStrange
    /// </summary>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferConditionStrange<T>(ConditionStrange<T> condition) where T : IEntity, new()
    {
        return TransferStrangeAssist.TransferCondition(condition);
    }

    /// <summary>
    /// OnlyCondition
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string OnlyCondition<T>(
        string sql,
        Condition<T> condition
    ) where T : IEntity, new()
    {
        var resultTransferCondition = TransferCondition(condition);

        return " {0} {1} ".FormatValue(sql, resultTransferCondition);
    }

    /// <summary>
    /// OnlyConditionStrange
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string OnlyConditionStrange<T>(
        string sql,
        ConditionStrange<T> condition
    ) where T : IEntity, new()
    {
        var resultTransferCondition = TransferConditionStrange(condition);

        return " {0} {1} ".FormatValue(sql, resultTransferCondition);
    }

    /// <summary>
    /// WhereCondition
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string WhereCondition<T>(
        string sql,
        Condition<T> condition
    ) where T : IEntity, new()
    {
        var resultTransferCondition = TransferCondition(condition);

        return "{0} WHERE {1}".FormatValue(sql, resultTransferCondition);
    }

    /// <summary>
    /// WhereConditionStrange
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string WhereConditionStrange<T>(
        string sql,
        ConditionStrange<T> condition
    ) where T : IEntity, new()
    {
        var resultTransferCondition = TransferConditionStrange(condition);

        return "{0} WHERE {1}".FormatValue(sql, resultTransferCondition);
    }

    /// <summary>
    /// AndCondition
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string AndCondition<T>(
        string sql,
        Condition<T> condition
    ) where T : IEntity, new()
    {
        var resultTransferCondition = TransferCondition(condition);

        return "{0} AND {1}".FormatValue(sql, resultTransferCondition);
    }

    /// <summary>
    /// AndConditionStrange
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string AndConditionStrange<T>(
        string sql,
        ConditionStrange<T> condition
    ) where T : IEntity, new()
    {
        var resultTransferCondition = TransferConditionStrange(condition);

        return "{0} AND {1}".FormatValue(sql, resultTransferCondition);
    }

    /// <summary>
    /// LinkConditions
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="conditions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string LinkConditions<T>(
        string sql,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        var result = sql;

        foreach (var condition in conditions) result = LinkCondition(result, condition);

        return result;
    }

    /// <summary>
    /// LinkCondition
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string LinkCondition<T>(
        string sql,
        Condition<T> condition
    ) where T : IEntity, new()
    {
        var resultTransferCondition = TransferCondition(condition);

        var connector = "";

        if (!string.IsNullOrWhiteSpace(sql)) connector = GetConditionConnector(sql);

        return "{0} {1} {2}".FormatValue(sql, connector, resultTransferCondition);
    }

    /// <summary>
    /// LinkConditionStrange
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string LinkConditionStrange<T>(
        string sql,
        ConditionStrange<T> condition
    ) where T : IEntity, new()
    {
        var resultTransferCondition = TransferConditionStrange(condition);

        var connector = "";

        if (!string.IsNullOrWhiteSpace(sql)) connector = GetConditionConnector(sql);

        return "{0} {1} {2}".FormatValue(sql, connector, resultTransferCondition);
    }

    /// <summary>
    /// LinkConditions
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="assignUpdates"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string LinkConditions<T>(
        string sql,
        ICollection<AssignField<T>> assignUpdates
    ) where T : IEntity, new()
    {
        var result = sql;

        foreach (var assignUpdate in assignUpdates) result = LinkAssignField(result, assignUpdate);

        return result;
    }

    /// <summary>
    /// LinkAssignField
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="assignField"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string LinkAssignField<T>(string sql, AssignField<T> assignField) where T : IEntity, new()
    {
        var resultTransferAssignUpdate = TransferAssignField(assignField);

        var connector = GetAssignFieldConnector(sql);

        return "{0} {1} {2}".FormatValue(sql, connector, resultTransferAssignUpdate);
    }

    /// <summary>
    /// TransferSort
    /// </summary>
    /// <param name="sort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferSort<T>(Sort<T> sort)
    {
        return TransferSort(sort.Expression, sort.SortType);
    }

    /// <summary>
    /// TransferSort
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="sortType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferSort<T>(Expression<Func<T, object>> propertyLambda, SortType sortType)
    {
        var p = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p.Split('.');

        p =
            $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.{p}" : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        return sortType == SortType.Asc ? $" {p} ASC" : $" {p} DESC";
    }

    /// <summary>
    /// OrderByFragment
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="fragment"></param>
    /// <param name="sortType"></param>
    /// <returns></returns>
    public static string OrderByFragment(string sql, string fragment, SortType sortType)
    {
        return sortType == SortType.Asc
            ? $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql} ORDER BY ")} {fragment} ASC "
            : $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql} ORDER BY ")} {fragment} DESC ";
    }

    /// <summary>
    /// OrderBy
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="sort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string OrderBy<T>(string sql, Sort<T> sort)
    {
        return OrderBy(sql, sort.Expression, sort.SortType);
    }

    /// <summary>
    /// OrderBy
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="sortType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string OrderBy<T>(string sql, Expression<Func<T, object>> propertyLambda, SortType sortType)
    {
        var sort = TransferSort(propertyLambda, sortType);

        return $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql} ORDER BY ")} {sort}";
    }

    /// <summary>
    /// AndOrderByFragment
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="fragment"></param>
    /// <param name="sortType"></param>
    /// <returns></returns>
    public static string AndOrderByFragment(string sql, string fragment, SortType sortType)
    {
        return sortType == SortType.Asc
            ? $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql},")} {fragment} ASC "
            : $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql},")} {fragment} DESC ";
    }

    /// <summary>
    /// AndOrderBy
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="sort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string AndOrderBy<T>(string sql, Sort<T> sort)
    {
        return AndOrderBy(sql, sort.Expression, sort.SortType);
    }

    /// <summary>
    /// AndOrderBy
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="sortType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string AndOrderBy<T>(string sql, Expression<Func<T, object>> propertyLambda, SortType sortType)
    {
        var sort = TransferSort(propertyLambda, sortType);

        return $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql},")} {sort}";
    }

    /// <summary>
    /// TransferGroup
    /// </summary>
    /// <param name="group"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferGroup<T>(Group<T> group)
    {
        return TransferGroup(group.Expression);
    }

    /// <summary>
    /// TransferGroup
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferGroup<T>(Expression<Func<T, object>> propertyLambda)
    {
        var p = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p.Split('.');

        p =
            $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.{p}" : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        return $" {p}";
    }

    /// <summary>
    /// TransferAssignField
    /// </summary>
    /// <param name="assignField"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferAssignField<T>(AssignField<T> assignField) where T : IEntity, new()
    {
        return TransferAssist.TransferAssignField(assignField);
    }

    /// <summary>
    /// BuildListSql
    /// </summary>
    /// <param name="listCondition"></param>
    /// <param name="listSort"></param>
    /// <param name="top"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string BuildListSql<T>(
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort,
        int? top = null
    ) where T : IEntity, new()
    {
        var model = new T();

        string sql;

        var fields = "".AllFields(model);

        var where = "";

        if (listCondition.Count > 0) where = ConditionAssist.Build(listCondition);

        var sort = "";

        if (listSort.Count > 0) sort = SortAssist.Build(listSort);

        if (top.HasValue)
            sql = $@"SELECT {fields}
                FROM {model.GetTableName()} WITH (NOLOCK)
                WHERE {model.GetPrimaryKeyName()} IN
                      (
                          SELECT TOP {top} {model.GetPrimaryKeyName()}
                          FROM {model.GetTableName()} WITH (NOLOCK)
                          {where}
                          {sort}
                      )";
        else
            sql = $@"SELECT {fields}
                FROM {model.GetTableName()} WITH (NOLOCK)
                {where}
                {sort}";

        return sql;
    }

    /// <summary>
    /// BuildListSql
    /// </summary>
    /// <param name="listPropertyLambda"></param>
    /// <param name="listCondition"></param>
    /// <param name="listSort"></param>
    /// <param name="top"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string BuildListSql<T>(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort,
        int? top = null
    ) where T : IEntity, new()
    {
        var fieldItems = FieldItemSpecialFactory.BuildFieldItems(listPropertyLambda.ToArray());

        return BuildListSql(
            fieldItems,
            listCondition,
            listSort,
            top
        );
    }

    /// <summary>
    /// BuildListSql
    /// </summary>
    /// <param name="fieldItems"></param>
    /// <param name="listCondition"></param>
    /// <param name="listSort"></param>
    /// <param name="top"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string BuildListSql<T>(
        ICollection<FieldItemSpecial<T>> fieldItems,
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort,
        int? top = null
    ) where T : IEntity, new()
    {
        var model = new T();

        string sql;

        var fieldList = new List<string>();

        foreach (var fieldItem in fieldItems) fieldList.Add(TransferAssist.TransferField(fieldItem));

        var fields = fieldList.Join(",");

        var where = "";

        if (listCondition.Count > 0) where = ConditionAssist.Build(listCondition);

        var sort = listSort.Count > 0
            ? SortAssist.Build(listSort)
            : $" ORDER BY {model.GetPrimaryKeyValue()} DESC ";

        if (top.HasValue && top <= 100)
            sql = $@"SELECT {fields}
                FROM {model.GetTableName()} WITH (NOLOCK)
                WHERE {model.GetPrimaryKeyName()} IN
                      (
                          SELECT TOP {top} {model.GetPrimaryKeyName()}
                          FROM {model.GetTableName()} WITH (NOLOCK)
                          {where}
                          {sort}
                      )";
        else
            sql = $@"SELECT {fields}
                FROM {model.GetTableName()} WITH (NOLOCK)
                {where}
                {sort}";

        return sql;
    }

    /// <summary>
    /// BuildListSql
    /// </summary>
    /// <param name="fields"></param>
    /// <param name="where"></param>
    /// <param name="tableName"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public static string BuildListSql(
        string fields,
        string where,
        string tableName,
        int? top
    )
    {
        return BuildListSql(fields, where, "", tableName, top);
    }

    /// <summary>
    /// BuildListSql
    /// </summary>
    /// <param name="fields"></param>
    /// <param name="where"></param>
    /// <param name="order"></param>
    /// <param name="tableName"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public static string BuildListSql(
        string fields,
        string where,
        string order,
        string tableName,
        int? top
    )
    {
        return BuildListSql(fields, where, order, "", tableName, top);
    }

    /// <summary>
    /// BuildListSql
    /// </summary>
    /// <param name="fields"></param>
    /// <param name="where"></param>
    /// <param name="order"></param>
    /// <param name="group"></param>
    /// <param name="tableName"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public static string BuildListSql(string fields, string where, string order, string group, string tableName,
        int? top)
    {
        var sql = top.HasValue
            ? $@"SELECT TOP {top} {fields} FROM {tableName} {(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")} {(!string.IsNullOrWhiteSpace(group) ? $" GROUP BY {group} " : "")} {(!string.IsNullOrWhiteSpace(order) ? $" ORDER BY {order} " : "")}"
            : $@"SELECT {fields} FROM {tableName} {(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")} {(!string.IsNullOrWhiteSpace(group) ? $" GROUP BY {group} " : "")} {(!string.IsNullOrWhiteSpace(order) ? $" ORDER BY {order} " : "")}";

        return sql;
    }

    /// <summary>
    /// BuildSingleTableListSql
    /// </summary>
    /// <param name="fields"></param>
    /// <param name="where"></param>
    /// <param name="order"></param>
    /// <param name="tableName"></param>
    /// <param name="primaryKey"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public static string BuildSingleTableListSql(
        string fields,
        string where,
        string order,
        string tableName,
        string primaryKey,
        int? top
    )
    {
        string sql;

        if (top.HasValue)
            sql = $@"SELECT {fields}
                FROM {tableName} WITH (NOLOCK)
                WHERE {primaryKey} IN
                      (
                          SELECT TOP {top} {primaryKey}
                          FROM {tableName} WITH (NOLOCK)
                          {(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")}
                          {(!string.IsNullOrWhiteSpace(order) ? $" ORDER BY {order} " : "")}
                      )";
        else
            sql = $@"SELECT {fields}
                FROM {tableName} WITH (NOLOCK)
                {(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")}
                {(!string.IsNullOrWhiteSpace(order) ? $" ORDER BY {order} " : "")}";

        return sql;
    }

    public static string BuildPageListSql<T>(
        int pageIndex,
        int pageSize,
        ICollection<Condition<T>>? listCondition,
        ICollection<Sort<T>>? listSort
    ) where T : IEntity, new()
    {
        var start = (pageIndex - 1) * pageSize + 1;
        var end = start + pageSize - 1;

        var model = new T();

        var fields = "".AllFields(model);

        var where = "";

        if (listCondition is { Count: > 0 }) where = ConditionAssist.Build(listCondition);

        var sort = listSort is { Count: > 0 }
            ? SortAssist.Build(listSort)
            : $" ORDER BY {model.GetPrimaryKeyValue()} DESC ";

        var sql = $@"SELECT {fields}
                    FROM {model.GetTableName()} WITH (NOLOCK)
                    WHERE {model.GetPrimaryKeyName()} IN
                          (
                              SELECT {model.GetPrimaryKeyName()}
                              FROM
                              (
                                  SELECT {model.GetPrimaryKeyName()},
                                         ROW_NUMBER() OVER ({sort}) AS rowId
                                  FROM {model.GetTableName()} WITH (NOLOCK)
                                  {@where}
                              ) data
                              WHERE rowId between {start} and {end}
                          ) {sort}";

        return sql;
    }

    /// <summary>
    /// BuildPageListSql
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="listPropertyLambda"></param>
    /// <param name="listCondition"></param>
    /// <param name="listSort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string BuildPageListSql<T>(
        int pageIndex,
        int pageSize,
        ICollection<Expression<Func<T>>> listPropertyLambda,
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort
    ) where T : IEntity, new()
    {
        var start = (pageIndex - 1) * pageSize + 1;
        var end = start + pageSize - 1;

        var model = new T();

        var fields = listPropertyLambda.Aggregate(
            "",
            (current, propertyLambda) => current.AppendField(propertyLambda)
        );

        var where = "";

        if (listCondition.Count > 0) where = ConditionAssist.Build(listCondition);

        string sort;

        if (listSort.Count > 0)
            sort = SortAssist.Build(listSort);
        else
            sort = $" ORDER BY {model.GetPrimaryKeyValue()} DESC ";

        var sql =
            $@"SELECT {fields} FROM {model.GetTableName()} WITH (NOLOCK) WHERE {model.GetPrimaryKeyValue()} IN (SELECT {model.GetPrimaryKeyValue()} FROM (SELECT {model.GetPrimaryKeyValue()},ROW_NUMBER() OVER ({sort}) AS rowId FROM {model.GetTableName()} WITH (NOLOCK) {where}) data WHERE rowId between {start} and {end}) {sort}";

        return sql;
    }

    /// <summary>
    /// BuildPageListSqlWithSingleTable
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="fields"></param>
    /// <param name="where"></param>
    /// <param name="order"></param>
    /// <param name="tableName"></param>
    /// <param name="primaryKey"></param>
    /// <returns></returns>
    public static string BuildPageListSqlWithSingleTable(
        int pageIndex,
        int pageSize,
        string fields,
        string where,
        string order,
        string tableName,
        string primaryKey
    )
    {
        return BuildPageListSqlWithSingleTable(
            pageIndex,
            pageSize,
            fields,
            where,
            order,
            "",
            tableName,
            primaryKey
        );
    }

    /// <summary>
    /// BuildPageListSqlWithSingleTable
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="fields"></param>
    /// <param name="where"></param>
    /// <param name="order"></param>
    /// <param name="group"></param>
    /// <param name="tableName"></param>
    /// <param name="primaryKey"></param>
    /// <returns></returns>
    public static string BuildPageListSqlWithSingleTable(
        int pageIndex,
        int pageSize,
        string fields,
        string where,
        string order,
        string group,
        string tableName,
        string primaryKey
    )
    {
        var start = (pageIndex - 1) * pageSize + 1;
        var end = start + pageSize - 1;

        var sql =
            $@"SELECT {fields} FROM {tableName} WITH (NOLOCK) WHERE {primaryKey} IN (SELECT {primaryKey} FROM (SELECT {primaryKey},ROW_NUMBER() OVER (order by {order}) AS rowId FROM {tableName} WITH (NOLOCK) {(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")} {(!string.IsNullOrWhiteSpace(group) ? $" WHERE {group} " : "")}) data WHERE rowId between {start} and {end}) order by {order}";

        return sql;
    }

    /// <summary>
    /// BuildPageListSql
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <param name="fields"></param>
    /// <param name="where"></param>
    /// <param name="order"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static string BuildPageListSql(
        int pageSize,
        int pageIndex,
        string fields,
        string where,
        string order,
        string tableName
    )
    {
        return BuildPageListSql(
            pageIndex,
            pageSize,
            fields,
            where,
            order,
            "",
            tableName
        );
    }

    /// <summary>
    /// BuildPageListSql
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="fields"></param>
    /// <param name="where"></param>
    /// <param name="order"></param>
    /// <param name="group"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static string BuildPageListSql(
        int pageIndex,
        int pageSize,
        string fields,
        string where,
        string order,
        string group,
        string tableName
    )
    {
        var start = (pageIndex - 1) * pageSize + 1;
        var end = start + pageSize - 1;

        return
            $"SELECT * FROM (SELECT row_number() over (ORDER BY {order}) AS rowId, {fields} FROM {tableName} {(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")} {(!string.IsNullOrWhiteSpace(group) ? $" GROUP BY {group} " : "")} ) as t where rowId between {start} and {end}";
    }

    public static string BuildCountSql(string where, string tableName)
    {
        return $"SELECT COUNT(*) FROM {tableName} {(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")}";
    }

    #endregion Select

    #region Insert

    public static string Insert(IEntity model)
    {
        var schemaName = model.GetSqlSchemaName();
        var nameString = new StringBuilder();
        var valueString = new StringBuilder();

        var modelType = model.GetType();

        var tableName = TransferAssist.GetTableName(model);

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        foreach (var p in modelType.GetProperties())
        {
            if (!p.CanWrite) continue;

            var columnMapperAttribute = Tools.GetColumnAttribute(p);

            if (columnMapperAttribute == null) throw new Exception("columnMapperAttribute is null");

            nameString.Append($"{fieldDecorateStart}{columnMapperAttribute.Name}{fieldDecorateEnd},");

            valueString.Append($"@{p.Name},");
        }

        if (!string.IsNullOrWhiteSpace(nameString.ToString())) nameString = nameString.Remove(nameString.Length - 1, 1);

        if (!string.IsNullOrWhiteSpace(valueString.ToString()))
            valueString = valueString.Remove(valueString.Length - 1, 1);

        var result = new StringBuilder();

        result.Append("INSERT INTO");
        result.Append(' ');

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append($" ( {nameString} ) VALUES ( {valueString} )");

        return result.ToString();
    }

    public static string InsertUniquer<T>(
        IEntity model,
        ICollection<Condition<T>> uniquerConditions
    ) where T : IEntity, new()
    {
        var schemaName = model.GetSqlSchemaName();
        var nameString = new StringBuilder();
        var valueString = new StringBuilder();

        var modelType = model.GetType();

        var tableName = TransferAssist.GetTableName(model);

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        foreach (var p in modelType.GetProperties())
        {
            if (!p.CanWrite) continue;

            var columnMapperAttribute = Tools.GetColumnAttribute(p);

            if (columnMapperAttribute == null) throw new Exception("columnMapperAttribute is null");

            nameString.Append($"{fieldDecorateStart}{columnMapperAttribute.Name}{fieldDecorateEnd},");

            valueString.Append($"@{p.Name},");
        }

        if (!string.IsNullOrWhiteSpace(nameString.ToString())) nameString = nameString.Remove(nameString.Length - 1, 1);

        if (!string.IsNullOrWhiteSpace(valueString.ToString()))
            valueString = valueString.Remove(valueString.Length - 1, 1);

        var result = new StringBuilder();

        result.Append("INSERT INTO");
        result.Append(' ');

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append(
            $" ( {nameString} ) SELECT  {valueString} WHERE NOT EXISTS ({Select().AppendFragment($"{model.GetTableName()}.{model.GetPrimaryKeyName()}").From(model).LinkConditions(uniquerConditions)})"
        );

        return result.ToString();
    }

    #endregion Insert

    #region Update

    public static string Update(IEntity model)
    {
        var schemaName = model.GetSqlSchemaName();
        var nameValueString = new StringBuilder();
        var modelType = model.GetType();
        var tableName = TransferAssist.GetTableName(model);
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();

        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();
        foreach (var p in modelType.GetProperties())
        {
            if (!p.CanWrite) continue;

            var columnName = TransferAssist.GetColumnName(p);

            if (columnName == null) throw new Exception("columnMapperAttribute is null");

            if (!columnName.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueString.Append(
                    $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name},"
                );
        }

        if (!string.IsNullOrWhiteSpace(nameValueString.ToString()))
            nameValueString = nameValueString.Remove(nameValueString.Length - 1, 1);
        else
            throw new Exception("更新字段不能空缺！");

        var result = new StringBuilder();

        result.Append("UPDATE ");

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append(
            $" SET {nameValueString} Where {model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        );

        return result.ToString();
    }

    public static string UpdateWithCondition<T>(
        T model,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        var schemaName = model.GetSqlSchemaName();

        if (conditions == null || conditions.Count == 0) throw new Exception("条件更新不能缺少条件语句！");

        var nameValueString = new StringBuilder();
        var modelType = model.GetType();
        var tableName = TransferAssist.GetTableName(model);

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();

        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        foreach (var p in modelType.GetProperties())
        {
            if (!p.CanWrite) continue;

            var columnName = TransferAssist.GetColumnName(p);

            if (columnName == null) throw new Exception("columnMapperAttribute is null");

            if (!p.Name.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueString.Append(
                    $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name},"
                );
        }

        if (!string.IsNullOrWhiteSpace(nameValueString.ToString()))
            nameValueString = nameValueString.Remove(nameValueString.Length - 1, 1);
        else
            throw new Exception("更新字段不能空缺！");

        var result = new StringBuilder();

        result.Append("UPDATE ");

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append(nameValueString);
        result.AppendLine(ConditionAssist.Build(conditions));

        return result.ToString();
    }

    public static string UpdateSpecific<T>(
        T model,
        ICollection<Expression<Func<T>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any()) throw new Exception("缺少指定的更新属性");

        var schemaName = model.GetSqlSchemaName();
        var nameValueString = new StringBuilder();
        var modelType = model.GetType();
        var tableName = TransferAssist.GetTableName(model);
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();

        var listPropertyName = new List<string>();

        foreach (var pExpression in listPropertyLambda)
        {
            var propertyName = ReflectionAssist.GetPropertyName(pExpression);

            listPropertyName.Add(propertyName);
        }

        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        foreach (var p in modelType.GetProperties())
        {
            if (!p.CanWrite) continue;

            if (!listPropertyName.Contains(p.Name)) continue;

            var columnName = TransferAssist.GetColumnName(p);

            if (columnName == null) throw new Exception("columnMapperAttribute is null");

            if (!columnName.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueString.Append(
                    $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name},"
                );
        }

        if (!string.IsNullOrWhiteSpace(nameValueString.ToString()))
            nameValueString = nameValueString.Remove(nameValueString.Length - 1, 1);
        else
            throw new Exception("更新字段不能空缺！");

        var result = new StringBuilder();

        result.Append("UPDATE ");

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append(
            $" SET {nameValueString} WHERE {model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        );

        return result.ToString();
    }

    public static string UpdateSpecific<T>(
        T model,
        ICollection<Expression<Func<T, object>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any()) throw new Exception("缺少指定的更新属性");

        var schemaName = model.GetSqlSchemaName();
        var nameValueString = new StringBuilder();
        var modelType = model.GetType();
        var tableName = TransferAssist.GetTableName(model);
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();

        var listPropertyName = new List<string>();

        foreach (var pExpression in listPropertyLambda)
        {
            var propertyName = ReflectionAssist.GetPropertyName(pExpression);

            listPropertyName.Add(propertyName);
        }

        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        foreach (var p in modelType.GetProperties())
        {
            if (!p.CanWrite) continue;

            if (!listPropertyName.Contains(p.Name)) continue;

            var columnName = TransferAssist.GetColumnName(p);

            if (columnName == null) throw new Exception("columnMapperAttribute is null");

            if (!columnName.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueString.Append(
                    $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name},"
                );
        }

        if (!string.IsNullOrWhiteSpace(nameValueString.ToString()))
            nameValueString = nameValueString.Remove(nameValueString.Length - 1, 1);
        else
            throw new Exception("更新字段不能空缺！");

        var result = new StringBuilder();

        result.Append("UPDATE ");

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append(
            $" SET {nameValueString} Where {model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        );

        return result.ToString();
    }

    public static string UpdateSpecificWithCondition<T>(
        T model,
        ICollection<Expression<Func<T>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any()) throw new Exception("缺少指定的更新属性");

        var schemaName = model.GetSqlSchemaName();

        if (conditions == null || conditions.Count == 0) throw new Exception("条件更新不能缺少条件语句！");

        var nameValueString = new StringBuilder();
        var modelType = model.GetType();
        var tableName = TransferAssist.GetTableName(model);
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        var listPropertyName = new List<string>();

        foreach (var pExpression in listPropertyLambda)
        {
            var propertyName = ReflectionAssist.GetPropertyName(pExpression);

            listPropertyName.Add(propertyName);
        }

        foreach (var p in modelType.GetProperties())
        {
            if (!p.CanWrite) continue;

            if (!listPropertyName.Contains(p.Name)) continue;

            var columnMapperAttribute = Tools.GetColumnAttribute(p);

            if (columnMapperAttribute == null) throw new Exception("columnMapperAttribute is null");

            if (!p.Name.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueString.Append(
                    $"{fieldDecorateStart}{columnMapperAttribute.Name}{fieldDecorateEnd} = @{p.Name},"
                );
        }

        if (!string.IsNullOrWhiteSpace(nameValueString.ToString()))
            nameValueString = nameValueString.Remove(nameValueString.Length - 1, 1);
        else
            throw new Exception("更新字段不能空缺！");

        var result = new StringBuilder();

        result.Append("UPDATE ");

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append(" SET ");
        result.Append(nameValueString);
        result.AppendLine(ConditionAssist.Build(conditions));

        return result.ToString();
    }

    public static string UpdateSpecificWithCondition<T>(
        T model,
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any()) throw new Exception("缺少指定的更新属性");

        var schemaName = model.GetSqlSchemaName();
        if (conditions == null || conditions.Count == 0) throw new Exception("条件更新不能缺少条件语句！");

        var nameValueString = new StringBuilder();
        var modelType = model.GetType();
        var tableName = TransferAssist.GetTableName(model);
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        var listPropertyName = new List<string>();

        foreach (var pExpression in listPropertyLambda)
        {
            var propertyName = ReflectionAssist.GetPropertyName(pExpression);

            listPropertyName.Add(propertyName);
        }

        foreach (var p in modelType.GetProperties())
        {
            if (!p.CanWrite) continue;

            if (!listPropertyName.Contains(p.Name)) continue;

            var columnMapperAttribute = Tools.GetColumnAttribute(p);

            if (columnMapperAttribute == null) throw new Exception("columnMapperAttribute is null");

            if (!p.Name.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueString.Append(
                    $"{fieldDecorateStart}{columnMapperAttribute.Name}{fieldDecorateEnd} = @{p.Name},"
                );
        }

        if (!string.IsNullOrWhiteSpace(nameValueString.ToString()))
            nameValueString = nameValueString.Remove(nameValueString.Length - 1, 1);
        else
            throw new Exception("更新字段不能空缺！");

        var result = new StringBuilder();

        result.Append("UPDATE ");

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append(" SET ");
        result.Append(nameValueString);
        result.AppendLine(ConditionAssist.Build(conditions));

        return result.ToString();
    }

    public static string UpdateAssignField<T>(
        T model,
        ICollection<AssignField<T>>? listAssignField
    ) where T : IEntity, new()
    {
        if (listAssignField == null || !listAssignField.Any()) throw new Exception("缺少指定的更新属性");

        var schemaName = model.GetSqlSchemaName();
        // var modelType = model.GetType();
        var tableName = TransferAssist.GetTableName(model);
        // var fieldDecorateStart = model.GetSqlFieldDecorateStart();

        var nameValueString = AssignFieldAssist.Build(listAssignField);

        if (!string.IsNullOrWhiteSpace(nameValueString))
            nameValueString = nameValueString.Remove(nameValueString.Length - 1, 1);
        else
            throw new Exception("更新字段不能空缺！");

        var result = new StringBuilder();

        result.Append("UPDATE ");

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append(
            $" SET {nameValueString} Where {model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        );

        return result.ToString();
    }

    public static string UpdatesAssignField<T>(
        T model,
        ICollection<Expression<Func<T, object>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any()) throw new Exception("缺少指定的更新属性");

        var schemaName = model.GetSqlSchemaName();
        var nameValueString = new StringBuilder();
        var modelType = model.GetType();
        var tableName = TransferAssist.GetTableName(model);
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();

        var listPropertyName = new List<string>();

        foreach (var pExpression in listPropertyLambda)
        {
            var propertyName = ReflectionAssist.GetPropertyName(pExpression);

            listPropertyName.Add(propertyName);
        }

        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        foreach (var p in modelType.GetProperties())
        {
            if (!p.CanWrite) continue;

            if (!listPropertyName.Contains(p.Name)) continue;

            var columnName = TransferAssist.GetColumnName(p);

            if (columnName == null) throw new Exception("columnMapperAttribute is null");

            if (!columnName.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueString.Append(
                    $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name},"
                );
        }

        if (!string.IsNullOrWhiteSpace(nameValueString.ToString()))
            nameValueString = nameValueString.Remove(nameValueString.Length - 1, 1);
        else
            throw new Exception("更新字段不能空缺！");

        var result = new StringBuilder();

        result.Append("UPDATE ");

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);
        result.Append(
            $" SET {nameValueString} Where {model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        );

        return result.ToString();
    }

    #endregion Update

    #region Delete

    public static string DeleteByPrimaryKey<T>(long key) where T : IEntity, new()
    {
        var model = new T();
        model.SetPrimaryKeyValue(key);
        return Delete(model);
    }

    public static string Delete<T>(T model) where T : IEntity, new()
    {
        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();
        var result = new StringBuilder();

        result.Append("DELETE FROM");
        result.Append(" ");

        if (!string.IsNullOrWhiteSpace(schemaName))
            result.Append($"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.");

        result.Append($"{fieldDecorateStart}{tableName}{fieldDecorateEnd}");
        result.Append(" ");
        result.Append(
            $"Where {fieldDecorateStart}{model.GetPrimaryKeyName()}{fieldDecorateEnd} = {model.TransferPrimaryKeyValueToSql()}"
        );

        return result.ToString();
    }

    public static string Delete<T>(
        T model,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        var schemaName = model.GetSqlSchemaName();

        if (conditions == null || conditions.Count == 0) throw new Exception("条件更新不能缺少条件语句！");

        var tableName = TransferAssist.GetTableName(model);

        var result = new StringBuilder();

        result.Append("DELETE ");
        result.Append("FROM ");

        if (!string.IsNullOrWhiteSpace(schemaName)) result.Append($"{schemaName}.");

        result.Append(tableName);

        result.AppendLine(ConditionAssist.Build(conditions));

        return result.ToString();
    }

    #endregion

    #region DeleteMany

    public static string DeleteManyByPrimaryKey<T>(IEnumerable<long> keys) where T : IEntity, new()
    {
        var list = new List<T>();

        foreach (var key in keys)
        {
            var model = new T();

            model.SetPrimaryKeyValue(key);

            list.Add(model);
        }

        return DeleteMany(list.AsEnumerable());
    }

    public static string DeleteMany<T>(IEnumerable<T> models) where T : IEntity, new()
    {
        var modelList = models.ToList();

        if (!modelList.Any()) throw new Exception("多条件In匹配中未找到任何条件");

        if (modelList.Count == 1) return Delete(modelList[0]);

        var values = modelList.Select(item => item.TransferPrimaryKeyValueToSql()).ToList();

        var model = new T();

        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();
        var result = new StringBuilder();

        result.Append("DELETE FROM");
        result.Append(" ");

        if (!string.IsNullOrWhiteSpace(schemaName))
            result.Append($"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.");

        result.Append($"{fieldDecorateStart}{tableName}{fieldDecorateEnd}");
        result.Append(" ");
        result.Append(
            $"Where {fieldDecorateStart}{model.GetPrimaryKeyName()}{fieldDecorateEnd} IN ({values.Join(",", "'{0}'")})"
        );

        return result.ToString();
    }

    #endregion

    private static string GetConditionConnector(string sql)
    {
        var sqlTrim = sql.Trim();
        var connector = "AND";
        var andLastIndex = sqlTrim.LastIndexOf(" and", StringComparison.OrdinalIgnoreCase);
        var whereLastIndex = sqlTrim.LastIndexOf(" where", StringComparison.OrdinalIgnoreCase);

        var onLastIndex = sqlTrim.LastIndexOf(" on", StringComparison.OrdinalIgnoreCase);

        if (whereLastIndex <= 0 && onLastIndex <= 0)
            if (!string.IsNullOrWhiteSpace(sql))
                connector = "WHERE";

        if (whereLastIndex >= 0 || onLastIndex >= 0)
            if ((whereLastIndex >= 0 && whereLastIndex > andLastIndex) ||
                (onLastIndex >= 0 && onLastIndex > andLastIndex))
                if (andLastIndex >= 0)
                    connector = "AND";

        if (whereLastIndex <= 0) return connector;

        if (connector == "AND" && whereLastIndex == sqlTrim.Length - 6) connector = "";

        return connector;
    }

    private static string GetAssignFieldConnector(string sql)
    {
        var sqlTrim = sql.Trim();
        var setLastIndex = sqlTrim.LastIndexOf(" set", StringComparison.OrdinalIgnoreCase);

        var connector = setLastIndex >= 0 ? "," : "";

        return connector;
    }

    /// <summary>
    /// TranslationConditionType
    /// </summary>
    /// <param name="conditionType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string TranslationConditionType(ConditionType conditionType)
    {
        string condition;

        switch (conditionType)
        {
            case ConditionType.Eq:
                condition = "=";
                break;

            case ConditionType.Gt:
                condition = ">";
                break;

            case ConditionType.Gte:
                condition = ">=";
                break;

            case ConditionType.Lt:
                condition = "<";
                break;

            case ConditionType.Lte:
                condition = "<=";
                break;

            case ConditionType.Ne:
                condition = "!=";
                break;

            default:
                throw new Exception(
                    $"conditionType {conditionType.ToString()} Is not yet supported"
                );
        }

        return condition;
    }
}