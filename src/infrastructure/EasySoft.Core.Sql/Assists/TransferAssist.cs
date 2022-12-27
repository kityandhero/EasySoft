using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.Extensions;
using TypeExtensions = EasySoft.UtilityTools.Standard.Extensions.TypeExtensions;

namespace EasySoft.Core.Sql.Assists;

/// <summary>
/// TransferAssist
/// </summary>
public static class TransferAssist
{
    public static string GetTableName<T>(T entity)
    {
        var tableAttribute = Tools.GetAdvanceTableAttribute(entity);

        if (tableAttribute == null) return typeof(T).Name;

        if (string.IsNullOrWhiteSpace(tableAttribute.Name))
            throw new Exception($"{nameof(TableAttribute)} disallow empty value");

        return tableAttribute.Name;
    }

    public static string GetTableName<T>()
    {
        var tableAttribute = Tools.GetAdvanceTableAttribute<T>();

        if (tableAttribute == null) return typeof(T).Name;

        if (string.IsNullOrWhiteSpace(tableAttribute.Name))
            throw new Exception($"{nameof(TableAttribute)} disallow empty value");

        return tableAttribute.Name;
    }

    #region GetTableAndColumnName

    public static string GetTableAndColumnName<T>(Expression<Func<T, object>> propertyLambda)
    {
        return GetTableAndColumnName(propertyLambda, out Type _);
    }

    public static string GetTableAndColumnName<T>(Expression<Func<T, object>> propertyLambda, out Type entityType)
    {
        return GetTableAndColumnName(propertyLambda, out entityType, out _);
    }

    public static string GetTableAndColumnName<T>(Expression<Func<T, object>> propertyLambda,
        out PropertyInfo propertyInfo)
    {
        return GetTableAndColumnName(propertyLambda, out _, out propertyInfo);
    }

    public static string GetTableAndColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type entityType,
        out PropertyInfo propertyInfo
    )
    {
        dynamic? me;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );

            var classLam = me.Expression;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    entityType = classLam.Type;

                    var propertyInfoTemp = me.Member as PropertyInfo;

                    if (propertyInfoTemp == null) throw new ArgumentException("PropertyInfo is null'");

                    propertyInfo = propertyInfoTemp;

                    var customColumnMapperAttribute = Tools.GetAdvanceColumnAttribute(propertyInfo);

                    if (customColumnMapperAttribute == null)
                        throw new Exception(
                            $"属性${propertyInfo.Name}缺少CustomColumnMapperAttribute特性"
                        );

                    return GetTableName(entityType.Create()) + "." + customColumnMapperAttribute.Name;
                }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert)
        {
            if (propertyLambda.Body is not UnaryExpression cov)
                throw new ArgumentException("Cannot analyze type get name ");

            me = cov.Operand as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );

            var classLam = me.Expression;

            if (me.Member == null) throw new ArgumentException("Cannot analyze type get name ");

            if (me.Member.PropertyType == null) throw new ArgumentException("Cannot analyze type get name ");

            entityType = classLam.Type;

            var propertyInfoTemp = me.Member as PropertyInfo;

            if (propertyInfoTemp == null) throw new ArgumentException("PropertyInfo is null'");

            propertyInfo = propertyInfoTemp;

            var columnAttribute = Tools.GetAdvanceColumnAttribute(propertyInfo);

            string columnName;

            if (columnAttribute == null)
            {
                columnName = propertyInfo.Name;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(columnAttribute.Name))
                    throw new Exception($"{nameof(ColumnAttribute)} disallow empty name");

                columnName = columnAttribute.Name;
            }

            return GetTableName(entityType.Create()) + "." + columnName;
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    #endregion

    #region GetColumnName

    public static string GetColumnName<T>(Expression<Func<T, object>> propertyLambda)
    {
        return GetColumnName(propertyLambda, out Type _);
    }

    public static string GetColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type entityType
    )
    {
        return GetColumnName(propertyLambda, out entityType, out _);
    }

    public static string GetColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out PropertyInfo propertyInfo
    )
    {
        return GetColumnName(propertyLambda, out _, out propertyInfo);
    }

    public static string GetColumnName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type entityType,
        out PropertyInfo propertyInfo
    )
    {
        dynamic? me;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );

            var classLam = me.Expression;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    entityType = classLam.Type;

                    var propertyInfoTemp = me.Member as PropertyInfo;

                    if (propertyInfoTemp == null) throw new ArgumentException("PropertyInfo is null'");

                    propertyInfo = propertyInfoTemp;

                    var columnAttribute = Tools.GetAdvanceColumnAttribute(propertyInfo);

                    string columnName;

                    if (columnAttribute == null)
                    {
                        columnName = propertyInfo.Name;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(columnAttribute.Name))
                            throw new Exception($"{nameof(ColumnAttribute)} disallow empty name");

                        columnName = columnAttribute.Name;
                    }

                    return columnName;
                }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert)
            if (propertyLambda.Body is UnaryExpression cov)
            {
                me = cov.Operand as MemberExpression;

                if (me == null)
                    throw new ArgumentException(
                        "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'");

                var classLam = me.Expression;

                if (me.Member != null)
                    if (me.Member.PropertyType != null)
                    {
                        entityType = classLam.Type;

                        var propertyInfoTemp = me.Member as PropertyInfo;

                        if (propertyInfoTemp == null) throw new ArgumentException("PropertyInfo is null'");

                        propertyInfo = propertyInfoTemp;

                        var columnAttribute = Tools.GetAdvanceColumnAttribute(propertyInfo);

                        string columnName;

                        if (columnAttribute == null)
                        {
                            columnName = propertyInfo.Name;
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(columnAttribute.Name))
                                throw new Exception($"{nameof(ColumnAttribute)} disallow empty name");

                            columnName = columnAttribute.Name;
                        }

                        return columnName;
                    }
            }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    #endregion

    #region GetTableAndColumnName

    public static string GetTableAndColumnName<T>(Expression<Func<T>> propertyLambda)
    {
        return GetTableAndColumnName(propertyLambda, out _, out _);
    }

    public static string GetTableAndColumnName<T>(Expression<Func<T>> propertyLambda, out PropertyInfo propertyInfo)
    {
        return GetTableAndColumnName(propertyLambda, out _, out propertyInfo);
    }

    public static string GetTableAndColumnName<T>(Expression<Func<T>> propertyLambda, out Type entityType)
    {
        return GetTableAndColumnName(propertyLambda, out entityType, out _);
    }

    public static string GetTableAndColumnName<T>(
        Expression<Func<T>> propertyLambda,
        out Type entityType,
        out PropertyInfo propertyInfo
    )
    {
        dynamic? me;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );

            var classLam = me.Expression;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    entityType = classLam.Type;

                    var propertyInfoTemp = me.Member as PropertyInfo;

                    if (propertyInfoTemp == null) throw new ArgumentException("PropertyInfo is null'");

                    propertyInfo = propertyInfoTemp;

                    var columnAttribute = Tools.GetAdvanceColumnAttribute(propertyInfo);

                    string columnName;

                    if (columnAttribute == null)
                    {
                        columnName = propertyInfo.Name;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(columnAttribute.Name))
                            throw new Exception($"{nameof(ColumnAttribute)} disallow empty name");

                        columnName = columnAttribute.Name;
                    }

                    return GetTableName(entityType.Create()) + "." + columnName;
                }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert)
        {
            if (propertyLambda.Body is not UnaryExpression cov)
                throw new ArgumentException("Cannot analyze type get name ");

            me = cov.Operand as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );

            var classLam = me.Expression;

            if (me.Member == null) throw new ArgumentException("Cannot analyze type get name ");

            if (me.Member.PropertyType == null) throw new ArgumentException("Cannot analyze type get name ");

            entityType = classLam.Type;

            var propertyInfoTemp = me.Member as PropertyInfo;

            if (propertyInfoTemp == null) throw new ArgumentException("PropertyInfo is null'");

            propertyInfo = propertyInfoTemp;

            var columnAttribute = Tools.GetAdvanceColumnAttribute(propertyInfo);

            string columnName;

            if (columnAttribute == null)
            {
                columnName = propertyInfo.Name;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(columnAttribute.Name))
                    throw new Exception($"{nameof(ColumnAttribute)} disallow empty name");

                columnName = columnAttribute.Name;
            }

            return GetTableName(entityType.Create()) + "." + columnName;
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    #endregion

    #region GetColumnName

    public static string GetColumnName(PropertyInfo propertyInfo)
    {
        var columnAttribute = TypeExtensions.GetCustomAttribute<ColumnAttribute>(propertyInfo);

        if (columnAttribute == null) return propertyInfo.Name;

        if (string.IsNullOrWhiteSpace(columnAttribute.Name))
            throw new Exception($"{nameof(ColumnAttribute)} disallow empty name");

        return columnAttribute.Name;
    }

    public static string GetColumnName<T>(Expression<Func<T>> propertyLambda)
    {
        return GetColumnName(propertyLambda, out Type _);
    }

    public static string GetColumnName<T>(Expression<Func<T>> propertyLambda, out PropertyInfo propertyInfo)
    {
        return GetColumnName(propertyLambda, out _, out propertyInfo);
    }

    public static string GetColumnName<T>(Expression<Func<T>> propertyLambda, out Type entityType)
    {
        return GetColumnName(propertyLambda, out entityType, out _);
    }

    public static string GetColumnName<T>(
        Expression<Func<T>> propertyLambda, out Type entityType,
        out PropertyInfo propertyInfo
    )
    {
        dynamic? me;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );

            var classLam = me.Expression;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    entityType = classLam.Type;

                    var propertyInfoTemp = me.Member as PropertyInfo;

                    if (propertyInfoTemp == null) throw new ArgumentException("PropertyInfo is null'");

                    propertyInfo = propertyInfoTemp;

                    var columnAttribute = Tools.GetAdvanceColumnAttribute(propertyInfo);

                    if (columnAttribute == null) return propertyInfo.Name;

                    if (string.IsNullOrWhiteSpace(columnAttribute.Name))
                        throw new Exception($"{nameof(ColumnAttribute)} disallow empty name");

                    return columnAttribute.Name;
                }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert)
        {
            if (propertyLambda.Body is not UnaryExpression cov)
                throw new ArgumentException("Cannot analyze type get name ");

            me = cov.Operand as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );

            var classLam = me.Expression;

            if (me.Member == null) throw new ArgumentException("Cannot analyze type get name ");

            if (me.Member.PropertyType == null) throw new ArgumentException("Cannot analyze type get name ");

            entityType = classLam.Type;

            var propertyInfoTemp = me.Member as PropertyInfo;

            if (propertyInfoTemp == null) throw new ArgumentException("PropertyInfo is null'");

            propertyInfo = propertyInfoTemp;

            var columnAttribute = Tools.GetAdvanceColumnAttribute(propertyInfo);

            if (columnAttribute == null) return propertyInfo.Name;

            if (string.IsNullOrWhiteSpace(columnAttribute.Name))
                throw new Exception($"{nameof(ColumnAttribute)} disallow empty name");

            return columnAttribute.Name;
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    #endregion

    #region TransferCondition

    public static string TransferCondition<T>(Condition<T> condition) where T : IEntity, new()
    {
        var transferResult = TransferConditionCore(condition);

        if (string.IsNullOrWhiteSpace(condition.CollaborationCondition)) return transferResult;

        return $"({transferResult} {condition.CollaborationCondition})";
    }

    private static string TransferConditionCore<T>(Condition<T> condition) where T : IEntity, new()
    {
        var p1 = condition.TransferExpression(out var type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        if (condition.ColumnTransferMode == ColumnTransferMode.ContainTableName)
        {
            var t = p1.Split('.');

            p1 = $"{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";
        }
        else
        {
            p1 = $"{fieldDecorateStart}{p1}{fieldDecorateEnd}";
        }

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var isCollection = condition.Value is ICollection;

        if (isCollection)
        {
            var valueCollection = (ICollection)condition.Value;

            var listValueString = new List<string>();

            const string emptyValue = "";

            foreach (var v in valueCollection)
            {
                if (v == null) continue;

                var typeCode = Type.GetTypeCode(v.GetType());

                if (typeCode == TypeCode.String)
                {
                    var value = Convert.ToString(v);

                    if (string.IsNullOrWhiteSpace(value)) throw new Exception("value disallow empty");

                    listValueString.Add(value);

                    continue;
                }

                if (typeCode == TypeCode.DateTime)
                {
                    listValueString.Add(Convert.ToDateTime(v).ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    continue;
                }

                if (typeCode is TypeCode.Int32 or TypeCode.Int64 or TypeCode.Decimal or TypeCode.Double
                    or TypeCode.Int16 or TypeCode.Single or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64)
                {
                    var value = Convert.ToString(v);

                    if (string.IsNullOrWhiteSpace(value)) throw new Exception("value disallow empty");

                    listValueString.Add(value);

                    continue;
                }

                throw new Exception(
                    $"未提供的Sql构建方式, typeName:{type.Name}，typeCode:{typeCode}，condition：{JsonConvert.SerializeObject(condition)}"
                );
            }

            if (!listValueString.Any())
            {
                // throw new Exception("IN 语句不存在要查询的集合项");

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
                        throw new Exception($"未提供此条件的构建:{condition.ConditionType.ToString()}");
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
                        throw new Exception($"未提供此条件的构建:{condition.ConditionType.ToString()}");
                }

                return TransferCondition(conditionInToOne);
            }

            switch (condition.ConditionType)
            {
                case ConditionType.In:
                    return "{0} IN ({1})".FormatValue(p1,
                        listValueString.Join(",", "'{0}'"));

                case ConditionType.NotIn:
                    return "{0} NOT IN ({1})".FormatValue(p1,
                        listValueString.Join(",", "'{0}'"));

                default:
                    throw new Exception($"未提供此条件的构建:{condition.ConditionType.ToString()}");
            }
        }
        else
        {
            if (condition.ConditionType == ConditionType.IsNull)
            {
                return "{0} IS NULL".FormatValue(p1);
            }
            else
            {
                var valueType = condition.Value.GetType();

                var typeCode = Type.GetTypeCode(valueType);

                if (typeCode == TypeCode.String)
                {
                    string result;
                    var v = Convert.ToString(condition.Value);

                    if (string.IsNullOrWhiteSpace(v)) throw new Exception("value disallow empty");

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
                            throw new Exception($"未提供此条件的构建:{condition.ConditionType.ToString()},value:{v}");
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
                            throw new Exception($"未提供此条件的构建:{condition.ConditionType.ToString()},value:{v}");
                    }
                }

                if (typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 || typeCode == TypeCode.Decimal ||
                    typeCode == TypeCode.Double || typeCode == TypeCode.Int16 || typeCode == TypeCode.Single ||
                    typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
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
                            throw new Exception(
                                $"未提供此条件的构建:{condition.ConditionType.ToString()},value:{condition.Value}");
                    }

                throw new Exception(
                    $"未提供的Sql构建方式，typeName:{type.Name}，typeCode:{typeCode}，condition：{JsonConvert.SerializeObject(condition)}"
                );
            }
        }
    }

    #endregion

    #region TransferAssignUpdate

    public static string TransferAssignField<T>(AssignField<T> assignField) where T : IEntity, new()
    {
        var transferResult = TransferAssignUpdateCore(assignField);

        return transferResult;
    }

    private static string TransferAssignUpdateCore<T>(AssignField<T> assignField) where T : IEntity, new()
    {
        var p1 = assignField.TransferExpression(out var type);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = p1.Split('.');

        p1 = $"{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}";

        if (!string.IsNullOrWhiteSpace(schemaName)) p1 = $"{schemaName}.{p1}";

        var valueType = assignField.Value.GetType();

        var isCollection = assignField.Value is ICollection;

        if (isCollection) throw new Exception("更新语句不支持Collection赋值");

        var typeCode = Type.GetTypeCode(valueType);

        if (typeCode == TypeCode.String)
        {
            string result;
            var v = Convert.ToString(assignField.Value);

            if (string.IsNullOrWhiteSpace(v)) throw new Exception("value disallow empty");

            switch (assignField.AssignFieldType)
            {
                case AssignFieldType.Eq:
                    result = "{0} = '{1}'".FormatValue(p1, v);
                    break;

                default:
                    throw new Exception($"未提供此条件的构建:{assignField.AssignFieldType.ToString()}");
            }

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
                    throw new Exception($"未提供此条件的构建:{assignField.AssignFieldType.ToString()}");
            }
        }

        if (typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 || typeCode == TypeCode.Decimal ||
            typeCode == TypeCode.Double || typeCode == TypeCode.Int16 || typeCode == TypeCode.Single ||
            typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
            switch (assignField.AssignFieldType)
            {
                case AssignFieldType.Eq:
                    return "{0} = {1}".FormatValue(p1, assignField.Value);

                case AssignFieldType.Increase:
                    return "{0} = {0} + {1}".FormatValue(p1, assignField.Value);

                case AssignFieldType.Decrease:
                    return "{0} = {0} - {1}".FormatValue(p1, assignField.Value);

                default:
                    throw new Exception($"未提供此条件的构建:{assignField.AssignFieldType.ToString()}");
            }

        throw new Exception(
            $"未提供的Sql构建方式，typeName:{type.Name}，typeCode:{typeCode}，condition：{JsonConvert.SerializeObject(assignField)}");
    }

    #endregion

    /// <summary>
    /// TransferSort
    /// </summary>
    /// <param name="sort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferSort<T>(Sort<T> sort) where T : IEntity, new()
    {
        return SqlAssist.TransferSort(sort);
    }

    #region TransferField

    /// <summary>
    /// TransferField
    /// </summary>
    /// <param name="fieldItem"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferField<T>(FieldItem<T> fieldItem)
    {
        var f = GetTableAndColumnName(fieldItem.PropertyLambda, out var type, out var propertyInfo);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = f.Split('.');

        f =
            $"{BuildIsNullFragment(propertyInfo, $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}." : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}", fieldItem.ReplaceDBNullValue)} AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItem.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItem.PropertyLambda) : fieldItem.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    /// <summary>
    /// TransferField
    /// </summary>
    /// <param name="fieldItemSpecial"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferField<T>(FieldItemSpecial<T> fieldItemSpecial)
    {
        var f = GetTableAndColumnName(fieldItemSpecial.PropertyLambda, out var type, out var propertyInfo);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = f.Split('.');

        f =
            $"{BuildIsNullFragment(propertyInfo, $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}." : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}", fieldItemSpecial.ReplaceDBNullValue)} AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItemSpecial.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItemSpecial.PropertyLambda) : fieldItemSpecial.ColumnAlias)}{fieldDecorateEnd}";

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
    public static string TransferMaxField<T>(FieldItem<T> fieldItem)
    {
        var f = GetTableAndColumnName(
            fieldItem.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = f.Split('.');

        f =
            $"MAX({BuildIsNullFragment(propertyInfo, $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}." : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}", fieldItem.ReplaceDBNullValue)}) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItem.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItem.PropertyLambda) : fieldItem.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    /// <summary>
    /// TransferMaxField
    /// </summary>
    /// <param name="fieldItemSpecial"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferMaxField<T>(FieldItemSpecial<T> fieldItemSpecial)
    {
        var f = GetTableAndColumnName(
            fieldItemSpecial.PropertyLambda,
            out var type,
            out var propertyInfo
        );

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = f.Split('.');

        f =
            $"MAX({BuildIsNullFragment(propertyInfo, $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}." : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}", fieldItemSpecial.ReplaceDBNullValue)}) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItemSpecial.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItemSpecial.PropertyLambda) : fieldItemSpecial.ColumnAlias)}{fieldDecorateEnd}";

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
    public static string TransferMinField<T>(FieldItem<T> fieldItem)
    {
        var f = GetTableAndColumnName(fieldItem.PropertyLambda, out var type, out var propertyInfo);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = f.Split('.');

        f =
            $"MIN({BuildIsNullFragment(propertyInfo, $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}." : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}", fieldItem.ReplaceDBNullValue)}) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItem.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItem.PropertyLambda) : fieldItem.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    public static string TransferMinField<T>(FieldItemSpecial<T> fieldItemSpecial)
    {
        var f = GetTableAndColumnName(fieldItemSpecial.PropertyLambda, out var type, out var propertyInfo);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = f.Split('.');

        f =
            $"MIN({BuildIsNullFragment(propertyInfo, $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}." : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}", fieldItemSpecial.ReplaceDBNullValue)}) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItemSpecial.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItemSpecial.PropertyLambda) : fieldItemSpecial.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    #endregion

    #region TransferSumField

    public static string TransferSumField<T>(FieldItem<T> fieldItem)
    {
        var f = GetTableAndColumnName(fieldItem.PropertyLambda, out var type, out var propertyInfo);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = f.Split('.');

        f =
            $"ISNULL(SUM({BuildIsNullFragment(propertyInfo, $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}." : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}", fieldItem.ReplaceDBNullValue)}),0) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItem.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItem.PropertyLambda) : fieldItem.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    public static string TransferSumField<T>(FieldItemSpecial<T> fieldItemSpecial)
    {
        var f = GetTableAndColumnName(fieldItemSpecial.PropertyLambda, out var type, out var propertyInfo);

        var m = type.Create();

        var entity = m as IEntity;

        var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var t = f.Split('.');

        f =
            $"ISNULL(SUM({BuildIsNullFragment(propertyInfo, $"{(!string.IsNullOrWhiteSpace(schemaName) ? $"{fieldDecorateStart}{schemaName}{fieldDecorateEnd}." : "")}{fieldDecorateStart}{t[0]}{fieldDecorateEnd}.{fieldDecorateStart}{t[1]}{fieldDecorateEnd}", fieldItemSpecial.ReplaceDBNullValue)}),0) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(fieldItemSpecial.ColumnAlias) ? ReflectionAssist.GetPropertyName(fieldItemSpecial.PropertyLambda) : fieldItemSpecial.ColumnAlias)}{fieldDecorateEnd}";

        return f;
    }

    #endregion

    #region TransferCountField

    public static string TransferCountField<T>(
        Expression<Func<T>> propertyLambda,
        string columnAlias = "TotalCount"
    )
    {
        GetTableAndColumnName(propertyLambda, out var type, out _);

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var f =
            $"ISNULL(COUNT(*),0) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(columnAlias) ? "TotalCount" : columnAlias)}{fieldDecorateEnd}";

        return f;
    }

    public static string TransferCountField<T>(
        Expression<Func<T, object>> propertyLambda,
        string columnAlias = "TotalCount"
    )
    {
        GetTableAndColumnName(propertyLambda, out var type, out _);

        var m = type.Create();

        var entity = m as IEntity;

        var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

        var f =
            $"ISNULL(COUNT(*),0) AS {fieldDecorateStart}{(string.IsNullOrWhiteSpace(columnAlias) ? "TotalCount" : columnAlias)}{fieldDecorateEnd}";

        return f;
    }

    #endregion

    public static string BuildIsNullFragment(
        PropertyInfo propertyInfo,
        string sqlFragment,
        bool replaceDBNullValue = false
    )
    {
        if (!replaceDBNullValue) return sqlFragment;

        var propertyType = propertyInfo.PropertyType;
        if (propertyInfo.PropertyType.IsGenericType &&
            propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            propertyType = propertyInfo.PropertyType.GenericTypeArguments[0];

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
}