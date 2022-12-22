using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.Extensions;
using EasySoft.Core.Sql.Factories;

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
        var list = new List<string>
        {
            "SELECT",
            "COUNT(*)",
            "AS",
            string.IsNullOrWhiteSpace(columnAlias) ? "TotalCount" : columnAlias,
            string.IsNullOrWhiteSpace(fragment) ? "" : fragment
        };

        return list.Join(" ");
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

        var list = new List<string>
        {
            !string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.{p}" : "",
            fieldDecorateStart,
            t[0],
            fieldDecorateEnd,
            ".",
            fieldDecorateStart,
            t[1],
            fieldDecorateEnd
        };

        p = list.Join("");

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

        var list = new List<string>();

        var fields = new AdvanceSqlBuilder().AllFields(model).Sql;

        var where = "";

        if (listCondition.Count > 0) where = ConditionAssist.Build(listCondition);

        var sort = "";

        if (listSort.Count > 0) sort = SortAssist.Build(listSort);

        if (top.HasValue)
        {
            list.Add($"SELECT {fields}");
            list.Add($"FROM {model.GetTableName()} WITH (NOLOCK)");
            list.Add($"WHERE {model.GetPrimaryKeyName()} IN");
            list.Add("(");
            list.Add($"SELECT TOP {top} {model.GetPrimaryKeyName()}");
            list.Add($"FROM {model.GetTableName()} WITH (NOLOCK)");
            list.Add($"{where}");
            list.Add($"{sort}");
            list.Add(")");
        }
        else
        {
            list.Add($"SELECT {fields}");
            list.Add($"FROM {model.GetTableName()} WITH (NOLOCK)");
            list.Add($"{where}");
            list.Add($"{sort}");
        }

        return list.Join(" ");
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
        IEnumerable<FieldItemSpecial<T>> fieldItems,
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort,
        int? top = null
    ) where T : IEntity, new()
    {
        var model = new T();

        var list = new List<string>();

        var fieldList = new List<string>();

        fieldItems.ForEach(fieldItem => { fieldList.Add(TransferAssist.TransferField(fieldItem)); });

        var fields = fieldList.Join(",");

        var where = "";

        if (listCondition.Count > 0) where = ConditionAssist.Build(listCondition);

        var sort = listSort.Count > 0
            ? SortAssist.Build(listSort)
            : $" ORDER BY {model.GetPrimaryKeyValue()} DESC ";

        if (top is <= 100)
        {
            list.Add($"SELECT {fields}");
            list.Add($"FROM {model.GetTableName()} WITH (NOLOCK)");
            list.Add($"WHERE {model.GetPrimaryKeyName()} IN");
            list.Add("(");
            list.Add($"SELECT TOP {top} {model.GetPrimaryKeyName()}");
            list.Add($"FROM {model.GetTableName()} WITH (NOLOCK)");
            list.Add($"{where}");
            list.Add($"{sort}");
            list.Add(")");
        }
        else
        {
            list.Add($"SELECT {fields}");
            list.Add($"FROM {model.GetTableName()} WITH (NOLOCK)");
            list.Add($"{where}");
            list.Add($"{sort}");
        }

        return list.Join(" ");
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
        uint? top
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
        uint? top
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
    public static string BuildListSql(
        string fields,
        string where,
        string order,
        string group,
        string tableName,
        uint? top
    )
    {
        var list = new List<string>
        {
            "SELECT"
        };

        if (top is > 0) list.Add($"TOP {top}");

        list.Add($"{fields}");
        list.Add($"FROM {tableName}");
        list.Add($"{(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")}");
        list.Add($"{(!string.IsNullOrWhiteSpace(group) ? $" GROUP BY {group} " : "")}");
        list.Add($"{(!string.IsNullOrWhiteSpace(order) ? $" ORDER BY {order} " : "")}");

        return list.Join(" ");
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
        var list = new List<string>
        {
            $"SELECT {fields}",
            $"FROM {tableName} WITH (NOLOCK)"
        };

        if (top.HasValue)
        {
            list.Add($"WHERE {primaryKey} IN");
            list.Add("(");
            list.Add($"SELECT TOP {top} {primaryKey}");
            list.Add($"FROM {tableName} WITH (NOLOCK)");
            list.Add($"{(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")}");
            list.Add($"{(!string.IsNullOrWhiteSpace(order) ? $" ORDER BY {order} " : "")}");
            list.Add(")");
        }
        else
        {
            list.Add($"{(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")}");
            list.Add($"{(!string.IsNullOrWhiteSpace(order) ? $" ORDER BY {order} " : "")}");
        }

        return list.Join(" ");
    }

    /// <summary>
    /// BuildPageListSql
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="listCondition"></param>
    /// <param name="listSort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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

        var fields = new AdvanceSqlBuilder().AllFields(model);

        var where = "";

        if (listCondition is { Count: > 0 }) where = ConditionAssist.Build(listCondition);

        var sort = listSort is { Count: > 0 }
            ? SortAssist.Build(listSort)
            : $"ORDER BY {model.GetPrimaryKeyValue()} DESC";

        var list = new List<string>
        {
            $"SELECT {fields}",
            $"FROM {model.GetTableName()} WITH (NOLOCK)",
            $"WHERE {model.GetPrimaryKeyName()} IN",
            "(",
            $"SELECT {model.GetPrimaryKeyName()}",
            "FROM",
            "(",
            $"SELECT {model.GetPrimaryKeyName()},",
            $"ROW_NUMBER() OVER ({sort}) AS rowId",
            $"FROM {model.GetTableName()} WITH (NOLOCK)",
            $"{where}",
            ") data",
            $"WHERE rowId between {start} and {end}",
            ")",
            $"{sort}"
        };

        return list.Join(" ");
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
        IEnumerable<Expression<Func<T>>> listPropertyLambda,
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort
    ) where T : IEntity, new()
    {
        var start = (pageIndex - 1) * pageSize + 1;
        var end = start + pageSize - 1;

        var model = new T();

        var fields = listPropertyLambda.Aggregate(
            "",
            (current, propertyLambda) => new AdvanceSqlBuilder(current).AppendField(propertyLambda).Sql
        );

        var where = "";

        if (listCondition.Count > 0) where = ConditionAssist.Build(listCondition);

        var sort = listSort.Count > 0 ? SortAssist.Build(listSort) : $"ORDER BY {model.GetPrimaryKeyValue()} DESC";

        var list = new List<string>
        {
            $"SELECT {fields}",
            $"FROM {model.GetTableName()} WITH (NOLOCK)",
            $"WHERE {model.GetPrimaryKeyValue()} IN",
            "(",
            $"SELECT {model.GetPrimaryKeyValue()} FROM",
            "(",
            $"SELECT {model.GetPrimaryKeyValue()},ROW_NUMBER() OVER ({sort}) AS rowId",
            $"FROM {model.GetTableName()} WITH (NOLOCK)",
            $"{where}",
            ") data",
            $"WHERE rowId between {start} and {end}",
            ")",
            $"{sort}"
        };

        return list.Join(" ");
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

        var list = new List<string>
        {
            $"SELECT {fields}",
            $"FROM {tableName} WITH (NOLOCK)",
            $"WHERE {primaryKey} IN",
            "(",
            $"SELECT {primaryKey}",
            "FROM",
            "(",
            $"SELECT {primaryKey},ROW_NUMBER() OVER (order by {order}) AS rowId",
            $"FROM {tableName} WITH (NOLOCK)",
            $"{(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")}",
            $"{(!string.IsNullOrWhiteSpace(group) ? $" WHERE {group} " : "")}",
            ") data",
            $"WHERE rowId between {start} and {end}",
            ")",
            $"order by {order}"
        };

        return list.Join(" ");
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

        var list = new List<string>
        {
            "SELECT *",
            "FROM",
            "(",
            $"SELECT row_number() over (ORDER BY {order}) AS rowId, {fields}",
            $"FROM {tableName}",
            $"{(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")}",
            $"{(!string.IsNullOrWhiteSpace(group) ? $" GROUP BY {group} " : "")}",
            ") AS t",
            $"WHERE rowId BETWEEN {start} AND {end}"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// BuildCountSql
    /// </summary>
    /// <param name="where"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static string BuildCountSql(string where, string tableName)
    {
        var list = new List<string>
        {
            "SELECT COUNT(*)",
            $"FROM {tableName}",
            $"{(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")}"
        };

        return list.Join(" ");
    }

    #endregion Select

    #region Insert

    /// <summary>
    /// Insert
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string Insert(IEntity model)
    {
        model.BuildNameAndValueList(out var nameList, out var valueList);

        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        var list = new List<string>
        {
            "INSERT INTO",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "(",
            $"{nameList.Join(",")}",
            ")",
            "VALUES",
            "(",
            $"{valueList.Join(",")}",
            ")"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// InsertUniquer
    /// </summary>
    /// <param name="model"></param>
    /// <param name="uniquerConditions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string InsertUniquer<T>(
        IEntity model,
        ICollection<Condition<T>> uniquerConditions
    ) where T : IEntity, new()
    {
        model.BuildNameAndValueList(out var nameList, out var valueList);

        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        var list = new List<string>
        {
            "INSERT INTO",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "(",
            $"{nameList.Join(",")}",
            ")",
            "SELECT",
            $"{valueList.Join(",")}",
            "WHERE NOT EXISTS",
            "(",
            $"{new AdvanceSqlBuilder().Select().AppendFragment($"{model.GetTableName()}.{model.GetPrimaryKeyName()}").From(model).LinkConditions(uniquerConditions)}",
            ")"
        };

        return list.Join(" ");
    }

    #endregion Insert

    #region Update

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string Update(IEntity model)
    {
        var schemaName = model.GetSqlSchemaName();
        var nameValueList = model.BuildNameValueList();
        var tableName = TransferAssist.GetTableName(model);

        if (nameValueList.Count <= 0)
            throw new Exception("更新字段不能空缺！");

        var list = new List<string>
        {
            "UPDATE",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "SET",
            $"{nameValueList.Join(",")}",
            "Where",
            $"{model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// UpdateWithCondition
    /// </summary>
    /// <param name="model"></param>
    /// <param name="conditions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string UpdateWithCondition<T>(
        T model,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        if (conditions == null || conditions.Count == 0) throw new Exception("条件更新不能缺少条件语句！");

        var nameValueList = model.BuildNameValueList();
        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        if (nameValueList.Count <= 0)
            throw new Exception("更新字段不能空缺！");

        var list = new List<string>
        {
            "UPDATE",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "SET",
            $"{nameValueList.Join(",")}",
            $"{ConditionAssist.Build(conditions)}"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// UpdateSpecific
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listPropertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string UpdateSpecific<T>(
        T model,
        ICollection<Expression<Func<T>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        var nameValueList = model.BuildNameValueList(listPropertyLambda);
        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        if (nameValueList.Count <= 0)
            throw new Exception("更新字段不能空缺！");

        var list = new List<string>
        {
            "UPDATE",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "SET",
            $"{nameValueList.Join(",")}",
            "WHERE",
            $"{model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// UpdateSpecific
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listPropertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string UpdateSpecific<T>(
        T model,
        ICollection<Expression<Func<T, object>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        var nameValueList = model.BuildNameValueList(listPropertyLambda);
        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        if (nameValueList.Count <= 0)
            throw new Exception("更新字段不能空缺！");

        var list = new List<string>
        {
            "UPDATE",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "SET",
            $"{nameValueList.Join(",")}",
            "WHERE",
            $"{model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// UpdateSpecificWithCondition
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listPropertyLambda"></param>
    /// <param name="conditions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string UpdateSpecificWithCondition<T>(
        T model,
        ICollection<Expression<Func<T>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        if (conditions == null || conditions.Count == 0) throw new Exception("条件更新不能缺少条件语句！");

        var schemaName = model.GetSqlSchemaName();
        var nameValueList = model.BuildNameValueList(listPropertyLambda);
        var tableName = TransferAssist.GetTableName(model);

        if (nameValueList.Count <= 0)
            throw new Exception("更新字段不能空缺！");

        var list = new List<string>
        {
            "UPDATE",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "SET",
            $"{nameValueList.Join(",")}",
            $"{ConditionAssist.Build(conditions)}"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// UpdateSpecificWithCondition
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listPropertyLambda"></param>
    /// <param name="conditions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string UpdateSpecificWithCondition<T>(
        T model,
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        if (conditions == null || conditions.Count == 0) throw new Exception("条件更新不能缺少条件语句！");

        var schemaName = model.GetSqlSchemaName();
        var nameValueList = model.BuildNameValueList(listPropertyLambda);
        var tableName = TransferAssist.GetTableName(model);

        if (nameValueList.Count <= 0)
            throw new Exception("更新字段不能空缺！");

        var list = new List<string>
        {
            "UPDATE",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "SET",
            $"{nameValueList.Join(",")}",
            $"{ConditionAssist.Build(conditions)}"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// UpdateAssignField
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listAssignField"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string UpdateAssignField<T>(
        T model,
        ICollection<AssignField<T>>? listAssignField
    ) where T : IEntity, new()
    {
        if (listAssignField == null || !listAssignField.Any()) throw new Exception("缺少指定的更新属性");

        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        var nameValueString = AssignFieldAssist.Build(listAssignField);

        if (string.IsNullOrWhiteSpace(nameValueString))
            throw new Exception("更新字段不能空缺！");

        var list = new List<string>
        {
            "UPDATE",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "SET",
            $"{nameValueString}",
            "WHERE",
            $"{model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// UpdatesAssignField
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listPropertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string UpdatesAssignField<T>(
        T model,
        ICollection<Expression<Func<T, object>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any()) throw new Exception("缺少指定的更新属性");

        var schemaName = model.GetSqlSchemaName();
        var nameValueList = model.BuildNameValueList(listPropertyLambda);
        var tableName = TransferAssist.GetTableName(model);

        if (nameValueList.Count <= 0)
            throw new Exception("更新字段不能空缺！");

        var list = new List<string>
        {
            "UPDATE",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "SET",
            $"{nameValueList}",
            "WHERE",
            $"{model.GetPrimaryKeyName()} = {model.TransferPrimaryKeyValueToSql()}"
        };

        return list.Join(" ");
    }

    #endregion Update

    #region Delete

    /// <summary>
    /// delete by primary value
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string Delete<T>(long key) where T : IEntity, new()
    {
        var model = new T();

        model.SetPrimaryKeyValue(key);

        return Delete(model);
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string Delete<T>(T model) where T : IEntity, new()
    {
        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        var list = new List<string>
        {
            "DELETE FROM",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "WHERE",
            $"{fieldDecorateStart}{model.GetPrimaryKeyName()}{fieldDecorateEnd} = {model.TransferPrimaryKeyValueToSql()}"
        };

        return list.Join(" ");
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="model"></param>
    /// <param name="conditions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string Delete<T>(
        T model,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        if (conditions == null || conditions.Count == 0) throw new Exception("条件更新不能缺少条件语句！");

        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        var list = new List<string>
        {
            "DELETE FROM",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            $"{ConditionAssist.Build(conditions)}"
        };

        return list.Join(" ");
    }

    #endregion

    #region DeleteBatch

    /// <summary>
    /// delete many by primary value
    /// </summary>
    /// <param name="keys"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string DeleteBatch<T>(IEnumerable<long> keys) where T : IEntity, new()
    {
        var list = new List<T>();

        foreach (var key in keys)
        {
            var model = new T();

            model.SetPrimaryKeyValue(key);

            list.Add(model);
        }

        return DeleteBatch(list.AsEnumerable());
    }

    /// <summary>
    /// DeleteBatch
    /// </summary>
    /// <param name="models"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string DeleteBatch<T>(IEnumerable<T> models) where T : IEntity, new()
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

        var list = new List<string>
        {
            "DELETE FROM",
            !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{tableName}" : tableName,
            "WHERE",
            $"{fieldDecorateStart}{model.GetPrimaryKeyName()}{fieldDecorateEnd} IN",
            "(",
            $"{values.Join(",", "'{0}'")}",
            ")"
        };

        return list.Join(" ");
    }

    #endregion

    /// <summary>
    /// GetConditionConnector
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static string GetConditionConnector(string sql)
    {
        var sqlTrim = sql.Trim().ToLower();
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

    /// <summary>
    /// GetAssignFieldConnector
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static string GetAssignFieldConnector(string sql)
    {
        var sqlTrim = sql.Trim().ToLower();
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