using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.ExtensionMethods;
using EasySoft.Core.Sql.Interfaces;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Data.SqlClient;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using Constants = EasySoft.Core.Sql.Common.Constants;

namespace EasySoft.Core.Sql.Assists;

public static class SqlAssist
{
    #region Select

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

    public static string Select(string fragment = "")
    {
        if (string.IsNullOrWhiteSpace(fragment)) return "SELECT ";

        return $"SELECT {fragment}";
    }

    public static string SelectAll(string fragment = "")
    {
        if (string.IsNullOrWhiteSpace(fragment)) return "SELECT * ";

        return $"SELECT * {fragment}";
    }

    public static string SelectTopAll(int top)
    {
        return $"SELECT TOP {top} * ";
    }

    public static string Update(string fragment = "")
    {
        if (string.IsNullOrWhiteSpace(fragment)) return "UPDATE ";

        return $"UPDATE {fragment}";
    }

    public static string Set(string sql, string fragment = "")
    {
        return $" {sql} SET {fragment}";
    }

    public static string Sum(string sql, string fragment, string valueWhenNUll)
    {
        return "{0} ISNULL(SUM(ISNULL({1},{2})),{2}) ".FormatValue(sql, fragment, valueWhenNUll);
    }

    //public static string AllField(string sql, object model)
    //{
    //    var f = "{0}.*".FormatValue(Reflection.GetClassName(model));
    //    return "{0} {1}".FormatValue(sql, f);
    //}

    public static string AppendFragment(string sql, string sqlFragment)
    {
        return "{0} {1}".FormatValue(sql, sqlFragment);
    }

    public static string AllField(string sql, params IEntityExtra[] models)
    {
        var list = new List<string>();

        foreach (var model in models)
        {
            var tableName = TransferAssist.GetTableName(model);

            var properties = model.GetType().GetProperties();

            foreach (var property in properties)
            {
                var customColumnMapperAttribute = Tools.GetColumnAttribute(property, false);

                if (customColumnMapperAttribute != null)
                {
                    var v =
                        $"{TransferAssist.BuildIsNullFragment(property, $"{(string.IsNullOrWhiteSpace(model.GetSqlSchemaName()) ? "" : $"{model.GetSqlFieldDecorateStart()}{model.GetSqlSchemaName()}{model.GetSqlFieldDecorateEnd()}" + ".")}{model.GetSqlFieldDecorateStart()}{tableName}{model.GetSqlFieldDecorateEnd()}.{model.GetSqlFieldDecorateStart()}{customColumnMapperAttribute.Name}{model.GetSqlFieldDecorateEnd()}", true)} AS {model.GetSqlFieldDecorateStart()}{property.Name}{model.GetSqlFieldDecorateEnd()}";

                    list.Add(v);
                }
            }
        }

        var f = list.Join(",");

        // var f = models.Select(o =>
        //     o == null
        //         ? ""
        //         : (string.IsNullOrWhiteSpace(o.GetSqlSchemaName()) ? "" : o.GetSqlSchemaName() + ".") +
        //           Tools.GetCustomTableMapperAttribute(o).Name + ".*").Join(",");

        return "{0} {1}".FormatValue(sql, f);
    }

    #region Field

    public static string Field<T>(
        string sql,
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

        return Field(sql, sqlField);
    }

    public static string Field<T>(
        string sql,
        FieldItem<T> fieldItem
    )
    {
        var transferResult = TransferAssist.TransferField(fieldItem);

        return "{0} {1}".FormatValue(sql, transferResult);
    }

    public static string Field<T>(
        string sql,
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

        return Field(sql, sqlField);
    }

    public static string Field<T>(
        string sql,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var transferResult = TransferAssist.TransferField(fieldItemSpecial);

        return "{0} {1}".FormatValue(sql, transferResult);
    }

    #endregion

    #region MinField

    public static string MinField<T>(
        string sql,
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

        return MinField(sql, sqlField);
    }

    public static string MinField<T>(
        string sql,
        FieldItem<T> fieldItem
    )
    {
        var transferResult = TransferAssist.TransferMinField(fieldItem);

        return "{0} {1}".FormatValue(sql, transferResult);
    }

    public static string MinField<T>(
        string sql,
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

        return MinField(sql, fieldItemSpecial);
    }

    public static string MinField<T>(
        string sql,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var transferResult = TransferAssist.TransferMinField(fieldItemSpecial);

        return "{0} {1}".FormatValue(sql, transferResult);
    }

    #endregion

    #region MaxField

    public static string MaxField<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        string columnAlias = "",
        bool replaceDBNullValue = true
    )
    {
        var fieldItem = new FieldItem<T>(propertyLambda)
        {
            ColumnAlias = columnAlias,
            ReplaceDBNullValue = replaceDBNullValue
        };

        return MaxField(sql, fieldItem);
    }

    public static string MaxField<T>(
        string sql,
        FieldItem<T> fieldItem
    )
    {
        var transferResult = TransferAssist.TransferMaxField(fieldItem);

        return "{0} {1}".FormatValue(sql, transferResult);
    }

    public static string MaxField<T>(
        string sql,
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

        return MaxField(sql, sqlField);
    }

    public static string MaxField<T>(
        string sql,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var transferResult = TransferAssist.TransferMaxField(fieldItemSpecial);

        return "{0} {1}".FormatValue(sql, transferResult);
    }

    #endregion

    #region SumField

    public static string SumField<T>(
        string sql,
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

        return SumField(sql, sqlField);
    }

    public static string SumField<T>(
        string sql,
        FieldItem<T> fieldItem
    )
    {
        var transferResult = TransferAssist.TransferSumField(fieldItem);

        return "{0} {1}".FormatValue(sql, transferResult);
    }

    public static string SumField<T>(
        string sql,
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

        return SumField(sql, fieldItemSpecial);
    }

    public static string SumField<T>(
        string sql,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var transferResult = TransferAssist.TransferSumField(fieldItemSpecial);

        return "{0} {1}".FormatValue(sql, transferResult);
    }

    #endregion

    #region AppendField

    public static string AppendField<T>(
        string sql,
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

        return AppendField(sql, sqlField);
    }

    public static string AppendField<T>(
        string sql,
        FieldItem<T> fieldItem
    )
    {
        var transferResult = TransferAssist.TransferField(fieldItem);

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    public static string AppendField<T>(
        string sql,
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

        return AppendField(sql, sqlField);
    }

    public static string AppendField<T>(
        string sql,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var transferResult = TransferAssist.TransferField(fieldItemSpecial);

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    #endregion

    #region AppendMinField

    public static string AppendMinField<T>(
        string sql,
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

        return AppendMinField(sql, sqlField);
    }

    public static string AppendMinField<T>(
        string sql,
        FieldItem<T> fieldItem
    )
    {
        var transferResult = TransferAssist.TransferMinField(fieldItem);

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    public static string AppendMinField<T>(
        string sql,
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

        return AppendMinField(sql, sqlField);
    }

    public static string AppendMinField<T>(
        string sql,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var transferResult = TransferAssist.TransferMinField(fieldItemSpecial);

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    #endregion

    #region AppendMaxField

    public static string AppendMaxField<T>(
        string sql,
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

        return AppendMaxField(sql, sqlField);
    }

    public static string AppendMaxField<T>(
        string sql,
        FieldItem<T> fieldItem
    )
    {
        var transferResult = TransferAssist.TransferMaxField(fieldItem);

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    public static string AppendMaxField<T>(
        string sql,
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

        return AppendMaxField(sql, sqlField);
    }

    public static string AppendMaxField<T>(
        string sql,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var transferResult = TransferAssist.TransferMaxField(fieldItemSpecial);

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    #endregion

    #region AppendSumField

    public static string AppendSumField<T>(
        string sql,
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

        return AppendSumField(sql, sqlField);
    }

    public static string AppendSumField<T>(
        string sql,
        FieldItem<T> fieldItem
    )
    {
        var transferResult = TransferAssist.TransferSumField(fieldItem);

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    public static string AppendSumField<T>(
        string sql,
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

        return AppendSumField(sql, sqlField);
    }

    public static string AppendSumField<T>(
        string sql,
        FieldItemSpecial<T> fieldItemSpecial
    )
    {
        var transferResult = TransferAssist.TransferSumField(fieldItemSpecial);

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    #endregion

    public static string AppendCountField<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        string columnAlias = "TotalCount"
    )
    {
        var transferResult = TransferAssist.TransferCountField(
            propertyLambda,
            columnAlias
        );

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    public static string AppendCountField<T>(
        string sql,
        Expression<Func<T, object>> propertyLambda,
        string columnAlias = "TotalCount"
    )
    {
        var transferResult = TransferAssist.TransferCountField(
            propertyLambda,
            columnAlias
        );

        return "{0},{1}".FormatValue(sql, transferResult);
    }

    #region Fields

    public static string Fields<T>(
        string sql,
        params Expression<Func<T, object>>[] propertyLambdas
    )
    {
        var list = FieldItemSpecialFactory.BuildFieldItems(propertyLambdas);

        return Fields(sql, list.ToArray());
    }

    public static string Fields<T>(
        string sql,
        params FieldItemSpecial<T>[] fieldItemSpecials
    )
    {
        var r = new List<string>();

        foreach (var fieldItemSpecial in fieldItemSpecials)
        {
            var transferResult = TransferAssist.TransferSumField(fieldItemSpecial);

            r.Add(transferResult);
        }

        return "{0} {1}".FormatValue(sql, r.Join(","));
    }

    #endregion

    public static string SelectTop(int top)
    {
        if (top <= 0) throw new Exception("top not allow 0");

        return $"SELECT TOP {top} ";
    }

    public static string SelectCount(string fragment = "", string columnAlias = "TotalCount")
    {
        if (string.IsNullOrWhiteSpace(fragment))
            return $"SELECT COUNT(*) AS {(string.IsNullOrWhiteSpace(columnAlias) ? "TotalCount" : columnAlias)} ";

        return $"SELECT COUNT(*) AS {(string.IsNullOrWhiteSpace(columnAlias) ? "TotalCount" : columnAlias)} {0}"
            .FormatValue(fragment);
    }

    public static string From<T>(string sql, T model) where T : IEntityExtra
    {
        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        tableName = $"{fieldDecorateStart}{tableName}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) tableName = schemaName + "." + tableName;

        return "{0} FROM {1}".FormatValue(sql, tableName);
    }

    public static string FromInnerQuery(
        string sql,
        string innerQuery,
        string aliasInnerQueryResult = "t"
    )
    {
        if (string.IsNullOrWhiteSpace(innerQuery)) throw new Exception("内查询语句不能为空");

        return "{0} FROM ({1}){2}".FormatValue(sql, innerQuery, aliasInnerQueryResult);
    }

    public static string InnerJoin<T>(string sql) where T : IEntityExtra, new()
    {
        var model = new T();

        return InnerJoin(sql, model);
    }

    public static string InnerJoin<T>(string sql, T model) where T : IEntityExtra
    {
        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        if (!string.IsNullOrWhiteSpace(schemaName)) tableName = schemaName + "." + tableName;

        return "{0} INNER JOIN {1}".FormatValue(sql, tableName);
    }

    public static string LeftJoin<T>(string sql) where T : IEntityExtra, new()
    {
        var model = new T();

        return LeftJoin(sql, model);
    }

    public static string LeftJoin<T>(string sql, T model) where T : IEntityExtra
    {
        var schemaName = model.GetSqlSchemaName();
        var tableName = TransferAssist.GetTableName(model);

        if (!string.IsNullOrWhiteSpace(schemaName)) tableName = schemaName + "." + tableName;

        return "{0} LEFT JOIN {1}".FormatValue(sql, tableName);
    }

    #region On

    public static string On<T1, T2>(
        string sql,
        Expression<Func<T1>> propertyLambda,
        Expression<Func<T2>> propertyLambda2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type1);

        {
            var m = type1.Create();

            var entity = m as IEntityExtra;

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

            var entity = m as IEntityExtra;

            var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            var t = p2.Split('.');
            p2 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

            if (!string.IsNullOrWhiteSpace(schemaName)) p2 = $"{schemaName}.{p2}";
        }

        return "{0} ON {1} = {2}".FormatValue(sql, p1, p2);
    }

    public static string On<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        string p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} ON {1} = '{2}'".FormatValue(sql, p1, p2);
    }

    public static string On<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        Guid p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} ON {1} = '{2}'".FormatValue(sql, p1, p2.ToString());
    }

    public static string On<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        int p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} ON {1} = {2}".FormatValue(sql, p1, p2);
    }

    public static string On<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        long p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} ON {1} = {2}".FormatValue(sql, p1, p2);
    }

    public static string On<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        DateTime p2
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} ON {1} = '{2}'".FormatValue(sql, p1, p2.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    #endregion On

    #region And

    public static string And<T1, T2>(
        string sql,
        Expression<Func<T1>> propertyLambda,
        Expression<Func<T2>> propertyLambda2,
        ConditionType conditionType = ConditionType.Eq
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type1);

        {
            var m = type1.Create();

            var entity = m as IEntityExtra;

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

            var entity = m as IEntityExtra;

            var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            var t = p2.Split('.');
            p2 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

            if (!string.IsNullOrWhiteSpace(schemaName)) p2 = $"{schemaName}.{p2}";
        }

        return "{0} AND {1} {3} {2}".FormatValue(sql, p1, p2, TranslationConditionType(conditionType));
    }

    public static string And<T>(string sql, Expression<Func<T>> propertyLambda, string p2)
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} AND {1} = '{2}'".FormatValue(sql, p1, p2);
    }

    public static string And<T>(string sql, Expression<Func<T>> propertyLambda, Guid p2)
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} AND {1} = '{2}'".FormatValue(sql, p1, p2.ToString());
    }

    public static string And<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        int p2,
        ConditionType conditionType = ConditionType.Eq
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} AND {1} {3} {2}".FormatValue(sql, p1, p2, TranslationConditionType(conditionType));
    }

    public static string And<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        long p2,
        ConditionType conditionType = ConditionType.Eq
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} AND {1} {3} {2}".FormatValue(sql, p1, p2, TranslationConditionType(conditionType));
    }

    public static string And<T>(
        string sql,
        Expression<Func<T>> propertyLambda,
        DateTime p2,
        ConditionType conditionType = ConditionType.Eq
    )
    {
        var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');
        p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        return "{0} AND {1} {3} '{2}'".FormatValue(
            sql,
            p1,
            p2.ToString("yyyy-MM-dd HH:mm:ss"),
            TranslationConditionType(conditionType)
        );
    }

    public static string And<T>(string sql, Condition<T> condition) where T : IEntityExtra, new()
    {
        var resultTransferCondition = TransferCondition(condition);

        return "{0} AND {1} ".FormatValue(sql, resultTransferCondition);
    }

    #endregion And

    public static string TransferCondition<T>(Condition<T> condition) where T : IEntityExtra, new()
    {
        return TransferAssist.TransferCondition(condition);
    }

    public static string TransferConditionStrange<T>(ConditionStrange<T> condition) where T : IEntityExtra, new()
    {
        return TransferStrangeAssist.TransferCondition(condition);
    }

    public static string OnlyCondition<T>(
        string sql,
        Condition<T> condition
    ) where T : IEntityExtra, new()
    {
        var resultTransferCondition = TransferCondition(condition);

        return " {0} {1} ".FormatValue(sql, resultTransferCondition);
    }

    public static string OnlyConditionStrange<T>(
        string sql,
        ConditionStrange<T> condition
    ) where T : IEntityExtra, new()
    {
        var resultTransferCondition = TransferConditionStrange(condition);

        return " {0} {1} ".FormatValue(sql, resultTransferCondition);
    }

    public static string WhereCondition<T>(
        string sql,
        Condition<T> condition
    ) where T : IEntityExtra, new()
    {
        var resultTransferCondition = TransferCondition(condition);

        return "{0} WHERE {1}".FormatValue(sql, resultTransferCondition);
    }

    public static string WhereConditionStrange<T>(
        string sql,
        ConditionStrange<T> condition
    ) where T : IEntityExtra, new()
    {
        var resultTransferCondition = TransferConditionStrange(condition);

        return "{0} WHERE {1}".FormatValue(sql, resultTransferCondition);
    }

    public static string AndCondition<T>(
        string sql,
        Condition<T> condition
    ) where T : IEntityExtra, new()
    {
        var resultTransferCondition = TransferCondition(condition);

        return "{0} AND {1}".FormatValue(sql, resultTransferCondition);
    }

    public static string AndConditionStrange<T>(
        string sql,
        ConditionStrange<T> condition
    ) where T : IEntityExtra, new()
    {
        var resultTransferCondition = TransferConditionStrange(condition);

        return "{0} AND {1}".FormatValue(sql, resultTransferCondition);
    }

    public static string LinkConditions<T>(
        string sql,
        ICollection<Condition<T>> conditions
    ) where T : IEntityExtra, new()
    {
        var result = sql;

        foreach (var condition in conditions) result = LinkCondition(result, condition);

        return result;
    }

    public static string LinkCondition<T>(
        string sql,
        Condition<T> condition
    ) where T : IEntityExtra, new()
    {
        var resultTransferCondition = TransferCondition(condition);

        var connector = "";

        if (!string.IsNullOrWhiteSpace(sql)) connector = GetConditionConnector(sql);

        return "{0} {1} {2}".FormatValue(sql, connector, resultTransferCondition);
    }

    public static string LinkConditionStrange<T>(
        string sql,
        ConditionStrange<T> condition
    ) where T : IEntityExtra, new()
    {
        var resultTransferCondition = TransferConditionStrange(condition);

        var connector = "";

        if (!string.IsNullOrWhiteSpace(sql)) connector = GetConditionConnector(sql);

        return "{0} {1} {2}".FormatValue(sql, connector, resultTransferCondition);
    }

    public static string LinkConditions<T>(
        string sql,
        ICollection<AssignField<T>> assignUpdates
    ) where T : IEntityExtra, new()
    {
        var result = sql;

        foreach (var assignUpdate in assignUpdates) result = LinkAssignField(result, assignUpdate);

        return result;
    }

    public static string LinkAssignField<T>(string sql, AssignField<T> assignField) where T : IEntityExtra, new()
    {
        var resultTransferAssignUpdate = TransferAssignField(assignField);

        var connector = GetAssignFieldConnector(sql);

        return "{0} {1} {2}".FormatValue(sql, connector, resultTransferAssignUpdate);
    }

    // public static string LinkCondition<T, TV>(string sql, Expression<Func<T, object>> propertyLambda,
    //     ICollection<TV> p2, ConditionType conditionType)
    // {
    //     var p1 = TransferAssist.GetTableAndColumnName(propertyLambda, out var type);
    //
    //     if (type != null)
    //     {
    //         var m = type.Create();
    //
    //         var entity = (m as IEntity);
    //
    //         var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
    //         var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
    //         var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();
    //
    //         var t = p1.Split('.');
    //         p1 = $"{t[0]}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";
    //
    //         if (!string.IsNullOrWhiteSpace(schemaName))
    //         {
    //             p1 = $"{schemaName}.{p1}";
    //         }
    //     }
    //
    //     var connector = GetConditionConnector(sql);
    //
    //     var typeCode = Type.GetTypeCode(typeof(TV));
    //
    //     if (typeCode == TypeCode.String)
    //     {
    //         var v = ((ICollection<string>)p2).Join(",", "'{0}'");
    //
    //         switch (conditionType)
    //         {
    //             case ConditionType.Eq:
    //                 return "{0} {1} {2} IN {3}".FormatValue(sql, connector, p1, v);
    //
    //             case ConditionType.Ne:
    //                 return "{0} {1} {2} NOT IN {3}".FormatValue(sql, connector, p1, v);
    //
    //             default:
    //                 throw new Exception("未提供此条件的构建");
    //         }
    //     }
    //
    //     if (typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 || typeCode == TypeCode.Decimal ||
    //         typeCode == TypeCode.Double || typeCode == TypeCode.Int16 || typeCode == TypeCode.Single ||
    //         typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
    //     {
    //         var v = ((ICollection<object>)p2).Join(",");
    //
    //         switch (conditionType)
    //         {
    //             case ConditionType.Eq:
    //                 return "{0} {1} {2} IN {3}".FormatValue(sql, connector, p1, v);
    //
    //             case ConditionType.Ne:
    //                 return "{0} {1} {2} NOT IN {3}".FormatValue(sql, connector, p1, v);
    //
    //             default:
    //                 throw new Exception("未提供此条件的构建");
    //         }
    //     }
    //
    //     throw new Exception("未提供的Sql构建方式");
    // }

    public static string TransferSort<T>(Sort<T> sort)
    {
        return TransferSort(sort.Expression, sort.SortType);
    }

    public static string TransferSort<T>(Expression<Func<T, object>> propertyLambda, SortType sortType)
    {
        var p = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p.Split('.');

        p =
            $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.{p}" : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        return sortType == SortType.Asc ? $" {p} ASC" : $" {p} DESC";
    }

    public static string OrderByFragment(string sql, string fragment, SortType sortType)
    {
        return sortType == SortType.Asc
            ? $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql} ORDER BY ")} {fragment} ASC "
            : $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql} ORDER BY ")} {fragment} DESC ";
    }

    public static string OrderBy<T>(string sql, Sort<T> sort)
    {
        return OrderBy(sql, sort.Expression, sort.SortType);
    }

    public static string OrderBy<T>(string sql, Expression<Func<T, object>> propertyLambda, SortType sortType)
    {
        var sort = TransferSort(propertyLambda, sortType);

        return $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql} ORDER BY ")} {sort}";
    }

    public static string AndOrderByFragment(string sql, string fragment, SortType sortType)
    {
        return sortType == SortType.Asc
            ? $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql},")} {fragment} ASC "
            : $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql},")} {fragment} DESC ";
    }

    public static string AndOrderBy<T>(string sql, Sort<T> sort)
    {
        return AndOrderBy(sql, sort.Expression, sort.SortType);
    }

    public static string AndOrderBy<T>(string sql, Expression<Func<T, object>> propertyLambda, SortType sortType)
    {
        var sort = TransferSort(propertyLambda, sortType);

        return $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql},")} {sort}";
    }

    public static string TransferGroup<T>(Group<T> group)
    {
        return TransferGroup(group.Expression);
    }

    public static string TransferGroup<T>(Expression<Func<T, object>> propertyLambda)
    {
        var p = TransferAssist.GetTableAndColumnName(propertyLambda, out Type type);

        var m = type.Create();

        var entity = m as IEntityExtra;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p.Split('.');

        p =
            $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.{p}" : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        return $" {p}";
    }

    public static string GroupBy<T>(string sql, Group<T> group)
    {
        return GroupBy(sql, group.Expression);
    }

    public static string GroupBy<T>(string sql, Expression<Func<T, object>> propertyLambda)
    {
        var group = TransferGroup(propertyLambda);

        return $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql} GROUP BY ")} {group}";
    }

    public static string AndGroupBy<T>(string sql, Group<T> group)
    {
        return AndGroupBy(sql, group.Expression);
    }

    public static string AndGroupBy<T>(string sql, Expression<Func<T, object>> propertyLambda)
    {
        var group = TransferGroup(propertyLambda);

        return $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql},")} {group}";
    }

    public static string TransferAssignField<T>(AssignField<T> assignField) where T : IEntityExtra, new()
    {
        return TransferAssist.TransferAssignField(assignField);
    }

    public static string BuildListSql<T>(
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort,
        int? top = null
    ) where T : IEntityExtra, new()
    {
        var model = new T();

        string sql;

        var fields = "".AllField(model);

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

    public static string BuildListSql<T>(
        ICollection<Expression<Func<T, object>>> listPropertyLambda,
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort,
        int? top = null
    ) where T : IEntityExtra, new()
    {
        var fieldItems = FieldItemSpecialFactory.BuildFieldItems(listPropertyLambda.ToArray());

        return BuildListSql(
            fieldItems,
            listCondition,
            listSort,
            top
        );
    }

    public static string BuildListSql<T>(
        ICollection<FieldItemSpecial<T>> fieldItems,
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort,
        int? top = null
    ) where T : IEntityExtra, new()
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

    public static string BuildListSql(
        string fields,
        string where,
        string tableName,
        int? top
    )
    {
        return BuildListSql(fields, where, "", tableName, top);
    }

    public static string BuildListSql(string fields, string where, string order, string tableName,
        int? top)
    {
        return BuildListSql(fields, where, order, "", tableName, top);
    }

    public static string BuildListSql(string fields, string where, string order, string group, string tableName,
        int? top)
    {
        var sql = top.HasValue
            ? $@"SELECT TOP {top} {fields} FROM {tableName} {(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")} {(!string.IsNullOrWhiteSpace(group) ? $" GROUP BY {group} " : "")} {(!string.IsNullOrWhiteSpace(order) ? $" ORDER BY {order} " : "")}"
            : $@"SELECT {fields} FROM {tableName} {(!string.IsNullOrWhiteSpace(where) ? $" WHERE {where} " : "")} {(!string.IsNullOrWhiteSpace(group) ? $" GROUP BY {group} " : "")} {(!string.IsNullOrWhiteSpace(order) ? $" ORDER BY {order} " : "")}";

        return sql;
    }

    public static string BuildSingleTableListSql(string fields, string where, string order, string tableName,
        string primaryKey, int? top)
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
    ) where T : IEntityExtra, new()
    {
        var start = (pageIndex - 1) * pageSize + 1;
        var end = start + pageSize - 1;

        var model = new T();

        var fields = "".AllField(model);

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

    public static string BuildPageListSql<T>(
        int pageIndex,
        int pageSize,
        ICollection<Expression<Func<T>>> listPropertyLambda,
        ICollection<Condition<T>> listCondition,
        ICollection<Sort<T>> listSort
    ) where T : IEntityExtra, new()
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

    public static string Insert(IEntityExtra model)
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
        IEntityExtra model,
        ICollection<Condition<T>> uniquerConditions
    ) where T : IEntityExtra, new()
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

    public static string Update(IEntityExtra model)
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
    ) where T : IEntityExtra, new()
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
    ) where T : IEntityExtra, new()
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
    ) where T : IEntityExtra, new()
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
    ) where T : IEntityExtra, new()
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
    ) where T : IEntityExtra, new()
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
    ) where T : IEntityExtra, new()
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
    ) where T : IEntityExtra, new()
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

    public static string DeleteByPrimaryKey<T>(long key) where T : IEntityExtra, new()
    {
        var model = new T();
        model.SetPrimaryKeyValue(key);
        return Delete(model);
    }

    public static string Delete<T>(T model) where T : IEntityExtra, new()
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
    ) where T : IEntityExtra, new()
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

    public static string DeleteManyByPrimaryKey<T>(IEnumerable<long> keys) where T : IEntityExtra, new()
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

    public static string DeleteMany<T>(IEnumerable<T> models) where T : IEntityExtra, new()
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

    private static string TranslationConditionType(ConditionType conditionType)
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