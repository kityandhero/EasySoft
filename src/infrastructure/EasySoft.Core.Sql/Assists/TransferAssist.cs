using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.Extensions;
using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.Exceptions;

namespace EasySoft.Core.Sql.Assists;

/// <summary>    
/// TransferAssist
/// </summary>
public static class TransferAssist
{
    #region GetTableName

    /// <summary>
    /// GetTableName
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetTableName<T>()
    {
        var tableName = typeof(T).Name;

        var advanceTableMapperAttribute = Tools.GetAdvanceTableMapperAttribute<T>(
            false
        );

        if (advanceTableMapperAttribute != null)
        {
            tableName = advanceTableMapperAttribute.Name;
        }

        return tableName;
    }

    /// <summary>
    /// GetTableName
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetTableName<T>(T model)
    {
        if (model == null)
        {
            throw new UnhandledException("model not allow null");
        }

        var tableName = model.GetType().Name;

        var advanceTableMapperAttribute = Tools.GetAdvanceTableMapperAttribute(
            model,
            false
        );

        if (advanceTableMapperAttribute != null)
        {
            tableName = advanceTableMapperAttribute.Name;
        }

        return tableName;
    }

    #endregion

    #region GetColumnLabel

    /// <summary>
    /// GetColumnInformation
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    public static AdvanceColumnInformationAttribute GetColumnInformation(PropertyInfo propertyInfo)
    {
        var helperAttribute = propertyInfo.GetCustomAttribute<AdvanceColumnInformationAttribute>();

        return helperAttribute ?? new AdvanceColumnInformationAttribute("");
    }

    #endregion

    #region GetColumnLabel

    /// <summary>
    /// GetColumnLabel
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    public static string GetColumnLabel(PropertyInfo propertyInfo)
    {
        var columnInformation = GetColumnInformation(propertyInfo);

        return columnInformation.Label;
    }

    #endregion

    #region GetColumnHelper

    /// <summary>
    /// GetColumnDescription
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    public static string GetColumnDescription(PropertyInfo propertyInfo)
    {
        var columnInformation = GetColumnInformation(propertyInfo);

        return columnInformation.Description;
    }

    #endregion

    #region GetColumnName

    /// <summary>
    /// GetColumnName
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    public static string GetColumnName(PropertyInfo? propertyInfo)
    {
        if (propertyInfo == null)
        {
            throw new UnhandledException("propertyInfo not allow null");
        }

        var customColumnMapperAttribute = Tools.GetAdvanceColumnMapperAttribute(
            propertyInfo,
            false
        );

        return customColumnMapperAttribute == null ? propertyInfo.Name : customColumnMapperAttribute.Name;
    }

    /// <summary>
    /// GetColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetColumnName<T>(Expression<Func<T>> propertyLambda)
    {
        return GetColumnName(propertyLambda, out Type? _);
    }

    /// <summary>
    /// GetColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out PropertyInfo? propertyInfo
    )
    {
        return GetColumnName(
            propertyLambda,
            out _,
            out propertyInfo
        );
    }

    /// <summary>
    /// GetColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType
    )
    {
        return GetColumnName(
            propertyLambda,
            out entityType,
            out _
        );
    }

    /// <summary>
    /// GetColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo
    )
    {
        return HandlerColumnName(
            propertyLambda,
            out entityType,
            out propertyInfo,
            GetColumnName
        );
    }

    /// <summary>
    /// GetColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetColumnName<T>(
        Expression<Func<T, object>> propertyLambda
    )
    {
        return GetColumnName(propertyLambda, out Type? _);
    }

    /// <summary>
    /// GetColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType
    )
    {
        return GetColumnName(
            propertyLambda,
            out entityType,
            out _
        );
    }

    /// <summary>
    /// GetColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out PropertyInfo? propertyInfo
    )
    {
        return GetColumnName(
            propertyLambda,
            out _,
            out propertyInfo
        );
    }

    /// <summary>
    /// GetColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>`
    public static string GetColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo
    )
    {
        return HandlerColumnName(
            propertyLambda,
            out entityType,
            out propertyInfo,
            GetColumnName
        );
    }

    #endregion

    #region GetTableAndColumnName

    /// <summary>
    /// GetTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableAndColumnName<T>(Expression<Func<T>> propertyLambda)
    {
        return GetTableAndColumnName(
            propertyLambda,
            out _,
            out _
        );
    }

    /// <summary>
    /// GetTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableAndColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out PropertyInfo? propertyInfo
    )
    {
        return GetTableAndColumnName(
            propertyLambda,
            out _,
            out propertyInfo
        );
    }

    /// <summary>
    /// GetTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableAndColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType
    )
    {
        return GetTableAndColumnName(
            propertyLambda,
            out entityType,
            out _
        );
    }

    /// <summary>
    /// GetTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableAndColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo
    )
    {
        return HandlerTableAndColumnName(
            propertyLambda,
            out entityType,
            out propertyInfo,
            (entity, propertyInfo) => $"{GetTableName(entity)}.{GetColumnName(propertyInfo)}"
        );
    }

    /// <summary>
    /// GetTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableAndColumnName<T>(
        Expression<Func<T, object>> propertyLambda
    )
    {
        return GetTableAndColumnName(propertyLambda, out Type? _);
    }

    /// <summary>
    /// GetTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableAndColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType
    )
    {
        return GetTableAndColumnName(
            propertyLambda,
            out entityType,
            out _
        );
    }

    /// <summary>
    /// GetTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableAndColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out PropertyInfo? propertyInfo
    )
    {
        return GetTableAndColumnName(
            propertyLambda,
            out _,
            out propertyInfo
        );
    }

    /// <summary>
    /// GetTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableAndColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo
    )
    {
        return HandlerTableAndColumnName(
            propertyLambda,
            out entityType,
            out propertyInfo,
            (entity, propertyInfo) => $"{GetTableName(entity)}.{GetColumnName(propertyInfo)}"
        );
    }

    #endregion

    #region TransferColumnName

    /// <summary>
    /// TransferColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferColumnName<T>(Expression<Func<T>> propertyLambda)
    {
        return TransferColumnName(
            propertyLambda,
            out _,
            out _
        );
    }

    /// <summary>
    /// TransferColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out PropertyInfo? propertyInfo
    )
    {
        return TransferColumnName(
            propertyLambda,
            out _,
            out propertyInfo
        );
    }

    /// <summary>
    /// TransferColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType
    )
    {
        return TransferColumnName(
            propertyLambda,
            out entityType,
            out _
        );
    }

    /// <summary>
    /// TransferColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo
    )
    {
        var columnName = GetColumnName(
            propertyLambda,
            out entityType,
            out propertyInfo
        );

        var entity = entityType.Create() as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        return string.IsNullOrWhiteSpace(columnName)
            ? columnName
            : $"{fieldDecorateStart}{columnName}{fieldDecorateEnd}";
    }

    /// <summary>
    /// TransferColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferColumnName<T>(Expression<Func<T, object>> propertyLambda)
    {
        return TransferColumnName(
            propertyLambda,
            out _,
            out _
        );
    }

    /// <summary>
    /// TransferColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out PropertyInfo? propertyInfo
    )
    {
        return TransferColumnName(
            propertyLambda,
            out _,
            out propertyInfo
        );
    }

    /// <summary>
    /// TransferColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType
    )
    {
        return TransferColumnName(
            propertyLambda,
            out entityType,
            out _
        );
    }

    /// <summary>
    /// TransferColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo
    )
    {
        var columnName = GetColumnName(
            propertyLambda,
            out entityType,
            out propertyInfo
        );

        var entity = entityType.Create() as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        return string.IsNullOrWhiteSpace(columnName)
            ? columnName
            : $"{fieldDecorateStart}{columnName}{fieldDecorateEnd}";
    }

    #endregion

    #region TransferTableName

    /// <summary>
    /// TransferTableName
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableName<T>()
    {
        var tableName = GetTableName<T>();

        var entity = typeof(T).Create() as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        schemaName = string.IsNullOrWhiteSpace(schemaName)
            ? schemaName
            : $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.";

        return $"{schemaName}{fieldDecorateStart}{tableName}{fieldDecorateEnd}";
    }

    /// <summary>
    /// TransferTableName
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableName<T>(T model)
    {
        var tableName = GetTableName(model);

        var entity = model as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        schemaName = string.IsNullOrWhiteSpace(schemaName)
            ? schemaName
            : $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.";

        return $"{schemaName}{fieldDecorateStart}{tableName}{fieldDecorateEnd}";
    }

    #endregion

    #region TransferSchema

    /// <summary>
    /// TransferSchema
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferSchema<T>() where T : IEntity
    {
        var entity = typeof(T).Create() as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        return string.IsNullOrWhiteSpace(schemaName)
            ? schemaName
            : $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}";
    }

    /// <summary>
    /// TransferSchema
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferSchema<T>(T model) where T : IEntity
    {
        var entity = typeof(T).Create() as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        return string.IsNullOrWhiteSpace(schemaName)
            ? schemaName
            : $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.";
    }

    #endregion

    #region TransferTableAndColumnName

    /// <summary>
    /// TransferTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableAndColumnName<T>(Expression<Func<T>> propertyLambda)
    {
        return TransferTableAndColumnName(
            propertyLambda,
            out _,
            out _
        );
    }

    /// <summary>
    /// TransferTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableAndColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out PropertyInfo? propertyInfo
    )
    {
        return TransferTableAndColumnName(
            propertyLambda,
            out _,
            out propertyInfo
        );
    }

    /// <summary>
    /// TransferTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableAndColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType
    )
    {
        return TransferTableAndColumnName(
            propertyLambda,
            out entityType,
            out _
        );
    }

    /// <summary>
    /// TransferTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableAndColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo
    )
    {
        return HandlerTableAndColumnName(
            propertyLambda,
            out entityType,
            out propertyInfo,
            (entity, _) => $"{TransferTableName(entity)}.{TransferColumnName(propertyLambda)}"
        );
    }

    /// <summary>
    /// TransferTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableAndColumnName<T>(Expression<Func<T, object>> propertyLambda)
    {
        return TransferTableAndColumnName(
            propertyLambda,
            out _,
            out _
        );
    }

    /// <summary>
    /// TransferTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableAndColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out PropertyInfo? propertyInfo
    )
    {
        return TransferTableAndColumnName(
            propertyLambda,
            out _,
            out propertyInfo
        );
    }

    /// <summary>
    /// TransferTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableAndColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType
    )
    {
        return TransferTableAndColumnName(
            propertyLambda,
            out entityType,
            out _
        );
    }

    /// <summary>
    /// TransferTableAndColumnName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferTableAndColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo
    )
    {
        return HandlerTableAndColumnName(
            propertyLambda,
            out entityType,
            out propertyInfo,
            (entity, _) => $"{TransferTableName(entity)}.{TransferColumnName(propertyLambda)}"
        );
    }

    #endregion

    #region TransferCondition

    /// <summary>
    /// TransferCondition
    /// </summary>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferCondition<T>(Condition<T> condition) where T : IEntity, new()
    {
        var transferResult = TransferConditionCore(condition);

        return string.IsNullOrWhiteSpace(condition.CollaborationCondition)
            ? transferResult
            : $"({transferResult} {condition.CollaborationCondition})";
    }

    private static string TransferConditionCore<T>(Condition<T> condition) where T : IEntity, new()
    {
        var p1 = condition.TransferExpression(out var type);

        if (type != null)
        {
            var m = type.Create();

            var entity = m as IEntity;

            var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            schemaName = string.IsNullOrWhiteSpace(schemaName)
                ? schemaName
                : $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}.";

            if (condition.ColumnTransferMode == ColumnTransferMode.ContainTableName)
            {
                var t = p1.Split('.');

                p1 = $"{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";
            }
            else
            {
                p1 = $"{fieldDecorateStart}{p1}{fieldDecorateEnd}";
            }

            if (!string.IsNullOrWhiteSpace(schemaName))
            {
                p1 = $"{schemaName}{p1}";
            }
        }

        var isCollection = condition.Value is ICollection;

        if (isCollection)
        {
            var valueCollection = (ICollection)condition.Value;

            var listValueString = new List<string>();

            const string emptyValue = "";

            foreach (var v in valueCollection)
            {
                var typeCode = Type.GetTypeCode(v.GetType());

                if (typeCode == TypeCode.String)
                {
                    var vv = Convert.ToString(v);

                    if (!string.IsNullOrWhiteSpace(vv))
                    {
                        listValueString.Add(Convert.ToString(vv));
                    }

                    continue;
                }

                if (typeCode == TypeCode.DateTime)
                {
                    listValueString.Add(Convert.ToDateTime(v).ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    continue;
                }

                if (typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 || typeCode == TypeCode.Decimal ||
                    typeCode == TypeCode.Double || typeCode == TypeCode.Int16 || typeCode == TypeCode.Single ||
                    typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
                {
                    var vv = Convert.ToString(v);

                    if (!string.IsNullOrWhiteSpace(vv))
                    {
                        listValueString.Add(Convert.ToString(vv));
                    }

                    continue;
                }

                throw new UnhandledException(
                    $"未提供的Sql构建方式{(type != null ? $"，typeName:{type.Name}" : "")}，typeCode:{typeCode}，condition：{JsonConvertAssist.SerializeObject(condition)}"
                );
            }

            if (!listValueString.Any())
            {
                Condition<T> conditionInToOne;

                switch (condition.ConditionType)
                {
                    case ConditionType.In:
                        conditionInToOne = new Condition<T>(
                            condition.ColumnTransferMode,
                            condition.CollaborationCondition
                        )
                        {
                            ConditionType = ConditionType.Eq,
                            Expression = condition.Expression,
                            Value = emptyValue
                        };
                        break;

                    case ConditionType.NotIn:
                        conditionInToOne = new Condition<T>(
                            condition.ColumnTransferMode,
                            condition.CollaborationCondition
                        )
                        {
                            ConditionType = ConditionType.Ne,
                            Expression = condition.Expression,
                            Value = emptyValue
                        };
                        break;

                    default:
                        throw new UnhandledException($"未提供此条件的构建:{condition.ConditionType.ToString()}");
                }

                return TransferCondition(conditionInToOne);
            }

            if (listValueString.Count == 1)
            {
                Condition<T> conditionInToOne;

                switch (condition.ConditionType)
                {
                    case ConditionType.In:
                        conditionInToOne = new Condition<T>(
                            condition.ColumnTransferMode,
                            condition.CollaborationCondition
                        )
                        {
                            ConditionType = ConditionType.Eq,
                            Expression = condition.Expression,
                            Value = listValueString[0]
                        };
                        break;

                    case ConditionType.NotIn:
                        conditionInToOne = new Condition<T>(
                            condition.ColumnTransferMode,
                            condition.CollaborationCondition
                        )
                        {
                            ConditionType = ConditionType.Ne,
                            Expression = condition.Expression,
                            Value = listValueString[0]
                        };
                        break;

                    default:
                        throw new UnhandledException($"未提供此条件的构建:{condition.ConditionType.ToString()}");
                }

                return TransferCondition(conditionInToOne);
            }

            switch (condition.ConditionType)
            {
                case ConditionType.In:
                    return "{0} IN ({1})".FormatValue(
                        p1,
                        listValueString.Join(",", "'{0}'")
                    );

                case ConditionType.NotIn:
                    return "{0} NOT IN ({1})".FormatValue(
                        p1,
                        listValueString.Join(",", "'{0}'")
                    );

                default:
                    throw new UnhandledException($"未提供此条件的构建:{condition.ConditionType.ToString()}");
            }
        }
        else
        {
            if (condition.ConditionType == ConditionType.IsNull)
            {
                return "{0} IS NULL".FormatValue(p1);
            }

            var valueType = condition.Value.GetType();

            var typeCode = Type.GetTypeCode(valueType);

            if (typeCode == TypeCode.String)
            {
                string result;
                var v = Convert.ToString(condition.Value);

                if (string.IsNullOrWhiteSpace(v))
                {
                    throw new UnhandledException("Transfer condition fail");
                }

                switch (condition.ConditionType)
                {
                    case ConditionType.Eq:
                        result = "{0} = '{1}'".FormatValue(p1, v);
                        break;

                    case ConditionType.Ne:
                        result = "{0} != '{1}'".FormatValue(p1, v);
                        break;

                    case ConditionType.Gt:
                        result = "{0} > '{1}'".FormatValue(p1, v);
                        break;

                    case ConditionType.Gte:
                        result = "{0} >= '{1}'".FormatValue(p1, v);
                        break;

                    case ConditionType.Lt:
                        result = "{0} < '{1}'".FormatValue(p1, v);
                        break;

                    case ConditionType.Lte:
                        result = "{0} <= '{1}'".FormatValue(p1, v);
                        break;

                    case ConditionType.LikeBefore:
                        result = "{0} LIKE '{1}%'".FormatValue(p1, v);
                        break;

                    case ConditionType.LikeAfter:
                        result = "{0} LIKE '%{1}'".FormatValue(p1, v);
                        break;

                    case ConditionType.LikeAny:
                        result = "{0} LIKE '%{1}%'".FormatValue(p1, v);
                        break;

                    default:
                        throw new UnhandledException($"未提供此条件的构建:{condition.ConditionType.ToString()},value:{v}");
                }

                return result;
            }

            if (typeCode == TypeCode.DateTime)
            {
                var v = Convert.ToDateTime(condition.Value).ToString("yyyy-MM-dd HH:mm:ss.fff");

                switch (condition.ConditionType)
                {
                    case ConditionType.Eq:
                        return "{0} = '{1}'".FormatValue(p1, v);

                    case ConditionType.Ne:
                        return "{0} != '{1}'".FormatValue(p1, v);

                    case ConditionType.Gt:
                        return "{0} > '{1}'".FormatValue(p1, v);

                    case ConditionType.Gte:
                        return "{0} >= '{1}'".FormatValue(p1, v);

                    case ConditionType.Lt:
                        return "{0} < '{1}'".FormatValue(p1, v);

                    case ConditionType.Lte:
                        return "{0} <= '{1}'".FormatValue(p1, v);

                    default:
                        throw new UnhandledException($"未提供此条件的构建:{condition.ConditionType.ToString()},value:{v}");
                }
            }

            if (typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 || typeCode == TypeCode.Decimal ||
                typeCode == TypeCode.Double || typeCode == TypeCode.Int16 || typeCode == TypeCode.Single ||
                typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
            {
                switch (condition.ConditionType)
                {
                    case ConditionType.Eq:
                        return "{0} = {1}".FormatValue(p1, condition.Value);

                    case ConditionType.Ne:
                        return "{0} != {1}".FormatValue(p1, condition.Value);

                    case ConditionType.Gt:
                        return "{0} > {1}".FormatValue(p1, condition.Value);

                    case ConditionType.Gte:
                        return "{0} >= {1}".FormatValue(p1, condition.Value);

                    case ConditionType.Lt:
                        return "{0} < {1}".FormatValue(p1, condition.Value);

                    case ConditionType.Lte:
                        return "{0} <= {1}".FormatValue(p1, condition.Value);

                    case ConditionType.LikeBefore:
                        return "{0} <= '{1}%'".FormatValue(p1, condition.Value);

                    case ConditionType.LikeAfter:
                        return "{0} <= '%{1}'".FormatValue(p1, condition.Value);

                    case ConditionType.LikeAny:
                        return "{0} <= '%{1}%'".FormatValue(p1, condition.Value);

                    default:
                        throw new UnhandledException(
                            $"未提供此条件的构建:{condition.ConditionType.ToString()},value:{condition.Value}"
                        );
                }
            }

            throw new UnhandledException(
                $"未提供的Sql构建方式{(type != null ? $"，typeName:{type.Name}" : "")}，typeCode:{typeCode}，condition：{JsonConvertAssist.SerializeObject(condition)}"
            );
        }
    }

    #endregion

    #region TransferAssignUpdate

    /// <summary>
    /// TransferAssignField
    /// </summary>
    /// <param name="assignField"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferAssignField<T>(AssignField<T> assignField) where T : IEntity, new()
    {
        var transferResult = TransferAssignUpdateCore(assignField);

        return transferResult;
    }

    private static string TransferAssignUpdateCore<T>(AssignField<T> assignField) where T : IEntity, new()
    {
        var p1 = assignField.TransferExpression(out var type);

        if (type != null)
        {
            var m = type.Create();

            var entity = m as IEntity;

            var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            var t = p1.Split('.');

            p1 = $"{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

            if (!string.IsNullOrWhiteSpace(schemaName))
            {
                p1 = $"{schemaName}.{p1}";
            }
        }

        var valueType = assignField.Value.GetType();

        var isCollection = assignField.Value is ICollection;

        if (isCollection)
        {
            throw new UnhandledException("更新语句不支持Collection赋值");
        }

        var typeCode = Type.GetTypeCode(valueType);

        if (typeCode == TypeCode.String)
        {
            var v = Convert.ToString(assignField.Value);

            if (string.IsNullOrWhiteSpace(v))
            {
                throw new UnhandledException("Transfer condition fail");
            }

            var result = assignField.AssignFieldType switch
            {
                AssignFieldType.Eq => "{0} = '{1}'".FormatValue(p1, v),
                _ => throw new UnhandledException($"未提供此条件的构建:{assignField.AssignFieldType.ToString()}")
            };

            return result;
        }

        if (typeCode == TypeCode.DateTime)
        {
            var v = Convert.ToDateTime(assignField.Value).ToString("yyyy-MM-dd HH:mm:ss.fff");

            switch (assignField.AssignFieldType)
            {
                case AssignFieldType.Eq:
                    return "{0} = '{1}'".FormatValue(p1, v);

                default:
                    throw new UnhandledException($"未提供此条件的构建:{assignField.AssignFieldType.ToString()}");
            }
        }

        if (typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 || typeCode == TypeCode.Decimal ||
            typeCode == TypeCode.Double || typeCode == TypeCode.Int16 || typeCode == TypeCode.Single ||
            typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
        {
            switch (assignField.AssignFieldType)
            {
                case AssignFieldType.Eq:
                    return "{0} = {1}".FormatValue(p1, assignField.Value);

                case AssignFieldType.Increase:
                    return "{0} = {0} + {1}".FormatValue(p1, assignField.Value);

                case AssignFieldType.Decrease:
                    return "{0} = {0} - {1}".FormatValue(p1, assignField.Value);

                default:
                    throw new UnhandledException($"未提供此条件的构建:{assignField.AssignFieldType.ToString()}");
            }
        }

        throw new UnhandledException(
            $"未提供的Sql构建方式{(type != null ? $"，typeName:{type.Name}" : "")}，typeCode:{typeCode}，condition：{JsonConvertAssist.SerializeObject(assignField)}"
        );
    }

    #endregion

    #region TransferField

    /// <summary>
    /// TransferField
    /// </summary>
    /// <param name="fieldItem"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferField<T>(FieldItem<T> fieldItem)
    {
        var f = TransferTableAndColumnName(
            fieldItem.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        if (type == null)
        {
            throw new UnhandledException("type not allow null");
        }

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        f =
            $"{BuildIsNullFragment(propertyInfo, f, fieldItem.ReplaceDBNullValue)} AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItem.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItem.PropertyLambda) : fieldItem.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    /// <summary>
    /// TransferField
    /// </summary>
    /// <param name="fieldItemSpecial"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferField<T>(FieldItemSpecial<T> fieldItemSpecial)
    {
        var f = TransferTableAndColumnName(
            fieldItemSpecial.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        if (type == null)
        {
            throw new UnhandledException("type not allow null");
        }

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        f =
            $"{BuildIsNullFragment(propertyInfo, f, fieldItemSpecial.ReplaceDBNullValue)} AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItemSpecial.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItemSpecial.PropertyLambda) : fieldItemSpecial.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    #endregion

    #region TransferMaxField

    /// <summary>
    /// TransferMaxField
    /// </summary>
    /// <param name="fieldItem"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferMaxField<T>(FieldItem<T> fieldItem)
    {
        var f = TransferTableAndColumnName(
            fieldItem.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        if (type == null)
        {
            throw new UnhandledException("type not allow null");
        }

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        f =
            $"MAX({BuildIsNullFragment(propertyInfo, f, fieldItem.ReplaceDBNullValue)}) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItem.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItem.PropertyLambda) : fieldItem.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    /// <summary>
    /// TransferMaxField
    /// </summary>
    /// <param name="fieldItemSpecial"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferMaxField<T>(FieldItemSpecial<T> fieldItemSpecial)
    {
        var f = TransferTableAndColumnName(
            fieldItemSpecial.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        if (type == null)
        {
            throw new UnhandledException("type not allow null");
        }

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        f =
            $"MAX({BuildIsNullFragment(propertyInfo, f, fieldItemSpecial.ReplaceDBNullValue)}) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItemSpecial.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItemSpecial.PropertyLambda) : fieldItemSpecial.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    #endregion

    #region TransferMinField

    /// <summary>
    /// TransferMinField
    /// </summary>
    /// <param name="fieldItem"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferMinField<T>(FieldItem<T> fieldItem)
    {
        var f = TransferTableAndColumnName(
            fieldItem.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        if (type == null)
        {
            throw new UnhandledException("type not allow null");
        }

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        f =
            $"MIN({BuildIsNullFragment(propertyInfo, f, fieldItem.ReplaceDBNullValue)}) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItem.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItem.PropertyLambda) : fieldItem.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    /// <summary>
    /// TransferMinField
    /// </summary>
    /// <param name="fieldItemSpecial"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferMinField<T>(FieldItemSpecial<T> fieldItemSpecial)
    {
        var f = TransferTableAndColumnName(
            fieldItemSpecial.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        if (type == null)
        {
            throw new UnhandledException("type not allow null");
        }

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        f =
            $"MIN({BuildIsNullFragment(propertyInfo, f, fieldItemSpecial.ReplaceDBNullValue)}) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItemSpecial.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItemSpecial.PropertyLambda) : fieldItemSpecial.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    #endregion

    #region TransferSumField

    /// <summary>
    /// TransferSumField
    /// </summary>
    /// <param name="fieldItem"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferSumField<T>(FieldItem<T> fieldItem)
    {
        var f = TransferTableAndColumnName(
            fieldItem.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        if (type == null)
        {
            throw new UnhandledException("type not allow null");
        }

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        f =
            $"ISNULL(SUM({BuildIsNullFragment(propertyInfo, f, fieldItem.ReplaceDBNullValue)}),0) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItem.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItem.PropertyLambda) : fieldItem.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    /// <summary>
    /// TransferSumField
    /// </summary>
    /// <param name="fieldItemSpecial"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferSumField<T>(FieldItemSpecial<T> fieldItemSpecial)
    {
        var f = TransferTableAndColumnName(
            fieldItemSpecial.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        if (type == null)
        {
            throw new UnhandledException("type not allow null");
        }

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        f =
            $"ISNULL(SUM({BuildIsNullFragment(propertyInfo, f, fieldItemSpecial.ReplaceDBNullValue)}),0) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItemSpecial.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItemSpecial.PropertyLambda) : fieldItemSpecial.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    #endregion

    #region TransferCountField

    /// <summary>
    /// TransferCountField
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferCountField<T>(
        Expression<Func<T>> propertyLambda,
        string columnAlias = "TotalCount"
    )
    {
        TransferTableAndColumnName(
            propertyLambda,
            out var type,
            out _
        );

        if (type == null)
        {
            throw new UnhandledException("type not allow null");
        }

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var f =
            $"ISNULL(COUNT(*),0) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(columnAlias) ? "TotalCount" : columnAlias)}{fieldDecorateEnd}";

        return f;
    }

    /// <summary>
    /// TransferCountField
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string TransferCountField<T>(
        Expression<Func<T, object>> propertyLambda,
        string columnAlias = "TotalCount"
    )
    {
        GetTableAndColumnName(
            propertyLambda,
            out var type,
            out _
        );

        if (type != null)
        {
            var m = type.Create();

            var entity = m as IEntity;

            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            var f =
                $"ISNULL(COUNT(*),0) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(columnAlias) ? "TotalCount" : columnAlias)}{fieldDecorateEnd}";

            return f;
        }

        throw new UnhandledException("type not allow null");
    }

    #endregion

    /// <summary>
    /// GetColumnDatabaseType
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <param name="fieldDecorateStart"></param>
    /// <param name="fieldDecorateEnd"></param>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    public static string GetColumnDatabaseType(
        PropertyInfo propertyInfo,
        string fieldDecorateStart = "",
        string fieldDecorateEnd = ""
    )
    {
        string result;

        var typeCode = propertyInfo.PropertyType.GetTypeCode();

        switch (typeCode)
        {
            case TypeCode.String:
                var customColumnTypeAttribute = propertyInfo.GetCustomAttribute<AdvanceColumnNationalAttribute>();

                result = customColumnTypeAttribute == null ? "varchar" : "nvarchar";

                var customColumnLengthAttribute = propertyInfo.GetCustomAttribute<AdvanceColumnLengthAttribute>();

                result = customColumnLengthAttribute == null
                    ? $"{result}(MAX)"
                    : $"{result}({customColumnLengthAttribute.Length})";
                break;

            case TypeCode.Int16:
                result = "int";
                break;

            case TypeCode.UInt16:
                result = "int";
                break;

            case TypeCode.Int32:
                result = "int";
                break;

            case TypeCode.UInt32:
                result = "int";
                break;

            case TypeCode.Int64:
                result = "bigint";
                break;

            case TypeCode.UInt64:
                result = "bigint";
                break;

            case TypeCode.Single:
                result = "int";
                break;

            case TypeCode.Double:
                result = "decimal(18, 6)";
                break;

            case TypeCode.Decimal:
                var customColumnAccuracyAttribute =
                    propertyInfo.GetCustomAttribute<AdvanceColumnAccuracyAttribute>();

                result = customColumnAccuracyAttribute == null
                    ? "money"
                    : $"decimal(18, {customColumnAccuracyAttribute.Accuracy})";

                break;

            case TypeCode.DateTime:
                result = "datetime";
                break;

            case TypeCode.Empty:
            case TypeCode.Object:
            case TypeCode.DBNull:
            case TypeCode.Boolean:
            case TypeCode.Char:
            case TypeCode.SByte:
            case TypeCode.Byte:
            default:
                throw new UnhandledException($"do not support type {propertyInfo.PropertyType.Name}");
        }

        return result;
    }

    /// <summary>
    /// BuildIsNullFragment
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <param name="sqlFragment"></param>
    /// <param name="replaceDBNullValue"></param>
    /// <returns></returns>
    public static string BuildIsNullFragment(
        PropertyInfo? propertyInfo,
        string sqlFragment,
        bool replaceDBNullValue = false
    )
    {
        if (propertyInfo == null)
        {
            throw new UnhandledException("propertyInfo not allow null");
        }

        if (!replaceDBNullValue)
        {
            return sqlFragment;
        }

        var propertyType = propertyInfo.PropertyType;

        if (propertyInfo.PropertyType.IsGenericType &&
            propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            propertyType = propertyInfo.PropertyType.GenericTypeArguments[0];
        }

        var propertyInfoTypeCode = Type.GetTypeCode(propertyType);

        switch (propertyInfoTypeCode)
        {
            case TypeCode.Decimal:
                return $" ISNULL({sqlFragment},0) ";

            case TypeCode.Double:
                return $" ISNULL({sqlFragment},0) ";

            case TypeCode.Int16:
                return $" ISNULL({sqlFragment},0) ";

            case TypeCode.Int32:
                return $" ISNULL({sqlFragment},0) ";

            case TypeCode.Int64:
                return $" ISNULL({sqlFragment},0) ";

            case TypeCode.Single:
                return $" ISNULL({sqlFragment},0) ";

            case TypeCode.String:
                return $" ISNULL({sqlFragment},'') ";

            case TypeCode.DateTime:
                return $" ISNULL({sqlFragment},'{ConstCollection.DbDefaultDateTime:yyyy-MM-dd HH:mm:ss}') ";

            case TypeCode.UInt16:
                return $" ISNULL({sqlFragment},0) ";

            case TypeCode.UInt32:
                return $" ISNULL({sqlFragment},0) ";

            case TypeCode.UInt64:
                return $" ISNULL({sqlFragment},0) ";

            default:
                return sqlFragment;
        }
    }

    private static string HandlerColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo,
        Func<PropertyInfo, string> handler
    )
    {
        dynamic? me;
        propertyInfo = null;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );
            }

            var classLam = me.Expression;

            entityType = null;

            if (me.Member != null && me.Member.PropertyType != null)
            {
                entityType = classLam.Type;

                propertyInfo = me.Member as PropertyInfo;

                if (propertyInfo == null)
                {
                    throw new ArgumentException("PropertyInfo is null'");
                }

                return handler(propertyInfo);
            }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert && propertyLambda.Body is UnaryExpression cov)
        {
            me = cov.Operand as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );
            }

            var classLam = me.Expression;

            entityType = null;

            if (me.Member != null && me.Member.PropertyType != null)
            {
                entityType = classLam.Type;

                propertyInfo = me.Member as PropertyInfo;

                if (propertyInfo == null)
                {
                    throw new ArgumentException("PropertyInfo is null'");
                }

                return handler(propertyInfo);
            }
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    private static string HandlerColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo,
        Func<PropertyInfo, string> handler
    )
    {
        dynamic? me;
        propertyInfo = null;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );
            }

            var classLam = me.Expression;

            entityType = null;

            if (me.Member != null && me.Member.PropertyType != null)
            {
                entityType = classLam.Type;

                propertyInfo = me.Member as PropertyInfo;

                if (propertyInfo == null)
                {
                    throw new ArgumentException("PropertyInfo is null'");
                }

                return handler(propertyInfo);
            }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert && propertyLambda.Body is UnaryExpression cov)
        {
            me = cov.Operand as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );
            }

            var classLam = me.Expression;

            entityType = null;

            if (me.Member != null && me.Member.PropertyType != null)
            {
                entityType = classLam.Type;

                propertyInfo = me.Member as PropertyInfo;

                if (propertyInfo == null)
                {
                    throw new ArgumentException("PropertyInfo is null'");
                }

                return handler(propertyInfo);
            }
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    private static string HandlerTableAndColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo,
        Func<object?, PropertyInfo?, string> handler
    )
    {
        dynamic? me;
        propertyInfo = null;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );
            }

            var classLam = me.Expression;

            entityType = null;

            if (me.Member != null && me.Member.PropertyType != null)
            {
                entityType = classLam.Type;

                propertyInfo = me.Member as PropertyInfo;

                if (propertyInfo == null)
                {
                    throw new ArgumentException("PropertyInfo is null'");
                }

                return handler(entityType.Create(), propertyInfo);
            }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert && propertyLambda.Body is UnaryExpression cov)
        {
            me = cov.Operand as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );
            }

            var classLam = me.Expression;

            entityType = null;

            if (me.Member != null && me.Member.PropertyType != null)
            {
                entityType = classLam.Type;

                propertyInfo = me.Member as PropertyInfo;

                if (propertyInfo == null)
                {
                    throw new ArgumentException("PropertyInfo is null'");
                }

                return handler(entityType.Create(), propertyInfo);
            }
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    private static string HandlerTableAndColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? entityType,
        out PropertyInfo? propertyInfo,
        Func<object?, PropertyInfo?, string> handler
    )
    {
        dynamic? me;
        propertyInfo = null;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );
            }

            var classLam = me.Expression;

            entityType = null;

            if (me.Member != null && me.Member.PropertyType != null)
            {
                entityType = classLam.Type;

                propertyInfo = me.Member as PropertyInfo;

                if (propertyInfo == null)
                {
                    throw new ArgumentException("PropertyInfo is null'");
                }

                return handler(entityType.Create(), propertyInfo);
            }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert && propertyLambda.Body is UnaryExpression cov)
        {
            me = cov.Operand as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );
            }

            var classLam = me.Expression;

            entityType = null;

            if (me.Member == null || me.Member.PropertyType == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            entityType = classLam.Type;

            propertyInfo = me.Member as PropertyInfo;

            if (propertyInfo == null)
            {
                throw new ArgumentException("PropertyInfo is null'");
            }

            return handler(entityType.Create(), propertyInfo);
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }
}