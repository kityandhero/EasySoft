using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.Extensions;

namespace EasySoft.Core.Sql.Assists;

/// <summary>
/// TransferStrangeAssist
/// </summary>
public static class TransferStrangeAssist
{
    /// <summary>
    /// GetPropertyName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetPropertyName<T>(Expression<Func<T, object>> propertyLambda)
    {
        return GetPropertyName(propertyLambda, out Type _);
    }

    /// <summary>
    /// GetPropertyName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetPropertyName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type entityType
    )
    {
        return GetPropertyName(
            propertyLambda,
            out entityType,
            out _
        );
    }

    /// <summary>
    /// GetPropertyName   
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetPropertyName<T>(
        Expression<Func<T, object>> propertyLambda,
        out PropertyInfo propertyInfo
    )
    {
        return GetPropertyName(
            propertyLambda,
            out _,
            out propertyInfo
        );
    }

    /// <summary>
    /// GetPropertyName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="entityType"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetPropertyName<T>(
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
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );
            }

            var classLam = me.Expression;

            if (me.Member != null)
            {
                if (me.Member.PropertyType != null)
                {
                    entityType = classLam.Type;

                    var propertyInfoTemp = me.Member as PropertyInfo;

                    if (propertyInfoTemp == null)
                    {
                        throw new ArgumentException("PropertyInfo is null'");
                    }

                    propertyInfo = propertyInfoTemp;

                    if (propertyInfo == null)
                    {
                        throw new ArgumentException("PropertyInfo is null'");
                    }

                    return propertyInfo.Name;
                }
            }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert)
        {
            if (propertyLambda.Body is UnaryExpression cov)
            {
                me = cov.Operand as MemberExpression;

                if (me == null)
                {
                    throw new ArgumentException(
                        "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                    );
                }

                var classLam = me.Expression;

                if (me.Member == null || me.Member.PropertyType == null)
                {
                    throw new ArgumentException("Cannot analyze type get name ");
                }

                entityType = classLam.Type;

                var propertyInfoTemp = me.Member as PropertyInfo;

                if (propertyInfoTemp == null)
                {
                    throw new ArgumentException("PropertyInfo is null'");
                }

                propertyInfo = propertyInfoTemp;

                return propertyInfo.Name;
            }
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    #region TransferCondition

    /// <summary>
    /// TransferCondition
    /// </summary>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferCondition<T>(ConditionStrange<T> condition) where T : IEntity, new()
    {
        var transferResult = TransferConditionCore(condition);

        if (string.IsNullOrWhiteSpace(condition.CollaborationCondition))
        {
            return transferResult;
        }

        return $"({transferResult} {condition.CollaborationCondition})";
    }

    private static string TransferConditionCore<T>(ConditionStrange<T> condition) where T : IEntity, new()
    {
        var p1 = condition.TransferExpression(out var type);

        {
            var m = type.Create();

            var entity = m as IEntity;

            var schemaName = entity == null ? "" : entity.GetSqlSchemaName();
            var fieldDecorateStart = entity == null ? "" : entity.GetSqlFieldDecorateStart();
            var fieldDecorateEnd = entity == null ? "" : entity.GetSqlFieldDecorateEnd();

            p1 = $"{fieldDecorateStart}{p1}{fieldDecorateEnd}";

            if (!string.IsNullOrWhiteSpace(schemaName))
            {
                p1 = $"{schemaName}.{p1}";
            }
        }

        var valueType = condition.Value.GetType();

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
                    var value = Convert.ToString(v);

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new Exception("value disallow empty");
                    }

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
                    listValueString.Add(Convert.ToString(v) ?? "0");

                    continue;
                }

                throw new Exception(
                    $"未提供的Sql构建方式，typeName:{type.Name}，typeCode:{typeCode}，condition：{JsonConvert.SerializeObject(condition)}"
                );
            }

            if (!listValueString.Any())
            {
                // throw new Exception("IN 语句不存在要查询的集合项");

                ConditionStrange<T> conditionInToOne;

                switch (condition.ConditionType)
                {
                    case ConditionType.In:
                        conditionInToOne = new ConditionStrange<T>(
                            condition.CollaborationCondition
                        )
                        {
                            ConditionType = ConditionType.Eq,
                            Expression = condition.Expression,
                            Value = emptyValue
                        };
                        break;
                    case ConditionType.NotIn:
                        conditionInToOne = new ConditionStrange<T>(
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
                ConditionStrange<T> conditionInToOne;

                switch (condition.ConditionType)
                {
                    case ConditionType.In:
                        conditionInToOne = new ConditionStrange<T>(
                            condition.CollaborationCondition
                        )
                        {
                            ConditionType = ConditionType.Eq,
                            Expression = condition.Expression,
                            Value = listValueString[0]
                        };
                        break;
                    case ConditionType.NotIn:
                        conditionInToOne = new ConditionStrange<T>(
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
                    throw new Exception($"未提供此条件的构建:{condition.ConditionType.ToString()}");
            }
        }
        else
        {
            var typeCode = Type.GetTypeCode(valueType);

            if (typeCode == TypeCode.String)
            {
                string result;

                var v = Convert.ToString(condition.Value);

                if (string.IsNullOrWhiteSpace(v))
                {
                    throw new Exception("value disallow empty");
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
                        throw new Exception(
                            $"未提供此条件的构建:{condition.ConditionType.ToString()},value:{condition.Value}"
                        );
                }
            }

            throw new Exception(
                $"未提供的Sql构建方式，typeName:{type.Name}，typeCode:{typeCode}，condition：{JsonConvert.SerializeObject(condition)}"
            );
        }
    }

    #endregion
}