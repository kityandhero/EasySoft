using System.Linq.Expressions;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityTools.Standard.Assists;

public static class ReflectionAssist
{
    /// <summary>
    /// 是否布尔类型
    /// </summary>
    /// <param name="member">成员</param>
    public static bool IsBool(MemberInfo member)
    {
        return member.MemberType switch
        {
            MemberTypes.TypeInfo => member.ToString() == "System.Boolean",
            MemberTypes.Property => IsBool((PropertyInfo)member),
            _ => false
        };
    }

    /// <summary>
    /// 是否布尔类型
    /// </summary>
    private static bool IsBool(PropertyInfo property)
    {
        return property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
    }

    /// <summary>
    /// 是否枚举类型
    /// </summary>
    /// <param name="member">成员</param>
    public static bool IsEnum(MemberInfo member)
    {
        return member.MemberType switch
        {
            MemberTypes.TypeInfo => ((TypeInfo)member).IsEnum,
            MemberTypes.Property => IsEnum((PropertyInfo)member),
            _ => false
        };
    }

    /// <summary>
    /// 是否枚举类型
    /// </summary>
    private static bool IsEnum(PropertyInfo property)
    {
        if (property.PropertyType.GetTypeInfo().IsEnum) return true;

        var value = Nullable.GetUnderlyingType(property.PropertyType);

        if (value == null) return false;

        return value.GetTypeInfo().IsEnum;
    }

    /// <summary>
    /// 是否日期类型
    /// </summary>
    /// <param name="member">成员</param>
    public static bool IsDate(MemberInfo member)
    {
        return member.MemberType switch
        {
            MemberTypes.TypeInfo => member.ToString() == "System.DateTime",
            MemberTypes.Property => IsDate((PropertyInfo)member),
            _ => false
        };
    }

    /// <summary>
    /// 是否日期类型
    /// </summary>
    private static bool IsDate(PropertyInfo property)
    {
        if (property.PropertyType == typeof(DateTime)) return true;

        if (property.PropertyType == typeof(DateTime?)) return true;

        return false;
    }

    /// <summary>
    /// 是否整型
    /// </summary>
    /// <param name="member">成员</param>
    public static bool IsInt(MemberInfo member)
    {
        return member.MemberType switch
        {
            MemberTypes.TypeInfo => member.ToString() == "System.Int32" || member.ToString() == "System.Int16" ||
                                    member.ToString() == "System.Int64",
            MemberTypes.Property => IsInt((PropertyInfo)member),
            _ => false
        };
    }

    /// <summary>
    /// 是否整型
    /// </summary>
    private static bool IsInt(PropertyInfo property)
    {
        if (property.PropertyType == typeof(int)) return true;

        if (property.PropertyType == typeof(int?)) return true;

        if (property.PropertyType == typeof(short)) return true;

        if (property.PropertyType == typeof(short?)) return true;

        if (property.PropertyType == typeof(long)) return true;

        if (property.PropertyType == typeof(long?)) return true;

        return false;
    }

    /// <summary>
    /// 是否数值类型
    /// </summary>
    /// <param name="member">成员</param>
    public static bool IsNumber(MemberInfo member)
    {
        if (IsInt(member)) return true;

        return member.MemberType switch
        {
            MemberTypes.TypeInfo => member.ToString() == "System.Double" || member.ToString() == "System.Decimal" ||
                                    member.ToString() == "System.Single",
            MemberTypes.Property => IsNumber((PropertyInfo)member),
            _ => false
        };
    }

    /// <summary>
    /// 是否数值类型
    /// </summary>
    private static bool IsNumber(PropertyInfo property)
    {
        if (property.PropertyType == typeof(double)) return true;

        if (property.PropertyType == typeof(double?)) return true;

        if (property.PropertyType == typeof(decimal)) return true;

        if (property.PropertyType == typeof(decimal?)) return true;

        if (property.PropertyType == typeof(float)) return true;

        if (property.PropertyType == typeof(float?)) return true;

        return false;
    }

    /// <summary>
    /// 是否集合
    /// </summary>
    /// <param name="type">类型</param>
    public static bool IsCollection(Type type)
    {
        return type.IsArray || IsGenericCollection(type);
    }

    /// <summary>
    /// 是否泛型集合
    /// </summary>
    /// <param name="type">类型</param>
    public static bool IsGenericCollection(Type type)
    {
        if (!type.IsGenericType) return false;

        var typeDefinition = type.GetGenericTypeDefinition();

        return typeDefinition == typeof(IEnumerable<>)
               || typeDefinition == typeof(IReadOnlyCollection<>)
               || typeDefinition == typeof(IReadOnlyList<>)
               || typeDefinition == typeof(ICollection<>)
               || typeDefinition == typeof(IList<>)
               || typeDefinition == typeof(List<>);
    }

    public static string GetClassName<T>(T o)
    {
        return o.GetClassName();
    }

    #region GetTypeName

    /// <summary>
    /// Returns the type's name
    /// </summary>
    /// <param name="objectType">object type</param>
    /// <returns>string name of the type</returns>
    public static string GetTypeName(Type objectType)
    {
        var output = new StringBuilder();

        if (objectType.Name == "Void")
        {
            output.Append("void");
        }
        else
        {
            if (objectType.Name.Contains("`"))
            {
                var genericTypes = objectType.GetGenericArguments();

                output.Append(objectType.Name.Remove(objectType.Name.IndexOf("`", StringComparison.Ordinal)))
                    .Append("<");

                var seperator = "";

                foreach (var genericType in genericTypes)
                {
                    output.Append(seperator).Append(GetTypeName(genericType));
                    seperator = ",";
                }

                output.Append(">");
            }
            else
            {
                output.Append(objectType.Name);
            }
        }

        return output.ToString();
    }

    #endregion GetTypeName

    #region GetPropertyName

    public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
    {
        return GetPropertyName(propertyLambda, out _, out _);
    }

    public static string GetPropertyName<T>(Expression<Func<T, object>> propertyLambda)
    {
        return GetPropertyName(propertyLambda, out var _, out var _);
    }

    public static string GetPropertyName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? type,
        out PropertyInfo? propertyInfo
    )
    {
        dynamic? me;
        propertyInfo = null;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );

            var classWithMemberAccess = me.Expression;

            type = null;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    type = classWithMemberAccess.Type;

                    propertyInfo = me.Member as PropertyInfo;

                    if (propertyInfo != null) return propertyInfo.Name;

                    throw new ArgumentException("PropertyInfo is null'");
                }
        }

        if (propertyLambda.Body.NodeType != ExpressionType.Convert)
            throw new ArgumentException("Cannot analyze type get name ");

        if (!(propertyLambda.Body is UnaryExpression cov)) throw new ArgumentException("Cannot analyze type get name ");

        me = cov.Operand as MemberExpression;

        if (me == null)
            throw new ArgumentException(
                "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
            );

        var classLam = me.Expression;

        type = null;

        if (me.Member == null) throw new ArgumentException("Cannot analyze type get name ");

        if (me.Member.PropertyType == null) throw new ArgumentException("Cannot analyze type get name ");

        type = classLam.Type;

        propertyInfo = me.Member as PropertyInfo;

        if (propertyInfo != null) return propertyInfo.Name;

        throw new ArgumentException("PropertyInfo is null'");
    }

    public static string GetPropertyName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? type,
        out PropertyInfo? propertyInfo
    )
    {
        dynamic? me;
        propertyInfo = null;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );

            var classWithMemberAccess = me.Expression;

            type = null;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    type = classWithMemberAccess.Type;

                    propertyInfo = me.Member as PropertyInfo;

                    if (propertyInfo != null) return propertyInfo.Name;

                    throw new ArgumentException("PropertyInfo is null'");
                }
        }

        if (propertyLambda.Body.NodeType != ExpressionType.Convert)
            throw new ArgumentException("Cannot analyze type get name ");

        if (!(propertyLambda.Body is UnaryExpression cov)) throw new ArgumentException("Cannot analyze type get name ");

        me = cov.Operand as MemberExpression;

        if (me == null)
            throw new ArgumentException(
                "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
            );

        var classLam = me.Expression;

        type = null;

        if (me.Member == null) throw new ArgumentException("Cannot analyze type get name ");

        if (me.Member.PropertyType == null) throw new ArgumentException("Cannot analyze type get name ");

        type = classLam.Type;

        propertyInfo = me.Member as PropertyInfo;

        if (propertyInfo != null) return propertyInfo.Name;

        throw new ArgumentException("PropertyInfo is null'");
    }

    #endregion

    #region GetMember(获取成员)

    /// <summary>
    /// 获取成员
    /// </summary>
    /// <param name="expression">表达式,范例：t => t.Name</param>
    public static MemberInfo? GetMember(Expression expression)
    {
        var memberExpression = GetMemberExpression(expression);

        return memberExpression?.Member;
    }

    /// <summary>
    /// 获取成员表达式
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <param name="right">取表达式右侧,(l,r) => l.id == r.id，设置为true,返回r.id表达式</param>
    public static MemberExpression? GetMemberExpression(Expression expression, bool right = false)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.Lambda:
                return GetMemberExpression(((LambdaExpression)expression).Body, right);
            case ExpressionType.Convert:
            case ExpressionType.Not:
                return GetMemberExpression(((UnaryExpression)expression).Operand, right);
            case ExpressionType.MemberAccess:
                return (MemberExpression)expression;
            case ExpressionType.Equal:
            case ExpressionType.NotEqual:
            case ExpressionType.GreaterThan:
            case ExpressionType.LessThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.LessThanOrEqual:
                return GetMemberExpression(
                    right ? ((BinaryExpression)expression).Right : ((BinaryExpression)expression).Left,
                    right
                );
            case ExpressionType.Call:
                return GetMethodCallExpressionName(expression);
        }

        return null;
    }

    /// <summary>
    /// 获取方法调用表达式的成员名称
    /// </summary>
    private static MemberExpression GetMethodCallExpressionName(Expression expression)
    {
        var methodCallExpression = (MethodCallExpression)expression;

        var left = (MemberExpression)methodCallExpression.Object;

        if (!IsGenericCollection(left.Type)) return left;

        var argumentExpression = methodCallExpression.Arguments.FirstOrDefault();

        if (argumentExpression is { NodeType: ExpressionType.MemberAccess })
            return (MemberExpression)argumentExpression;

        return left;
    }

    #endregion

    #region GetName(获取成员名称)

    /// <summary>
    /// 获取成员名称，范例：t => t.A.Name,返回 A.Name
    /// </summary>
    /// <param name="expression">表达式,范例：t => t.Name</param>
    public static string GetName(Expression expression)
    {
        var memberExpression = GetMemberExpression(expression);

        return GetMemberName(memberExpression);
    }

    /// <summary>
    /// 获取成员名称
    /// </summary>
    public static string GetMemberName(MemberExpression? memberExpression)
    {
        var result = memberExpression?.ToString() ?? "";

        return result.Substring(result.IndexOf(".", StringComparison.Ordinal) + 1);
    }

    #endregion

    #region GetClassAndPropertyName

    /// <summary>
    /// GetClassAndPropertyName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetClassAndPropertyName<T>(Expression<Func<T>> propertyLambda)
    {
        return GetClassAndPropertyName(propertyLambda, out _, out _);
    }

    /// <summary>
    /// GetClassAndPropertyName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetClassAndPropertyName<T>(Expression<Func<T, object>> propertyLambda)
    {
        return GetClassAndPropertyName(propertyLambda, out _, out _);
    }

    /// <summary>
    /// GetClassAndPropertyName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="type"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetClassAndPropertyName<T>(
        Expression<Func<T>> propertyLambda,
        out Type? type,
        out PropertyInfo? propertyInfo
    )
    {
        dynamic? me;
        propertyInfo = null;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");

            var classWithMemberAccess = me.Expression;

            type = null;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    type = classWithMemberAccess.Type;

                    propertyInfo = me.Member as PropertyInfo;

                    if (propertyInfo != null) return type.Name + "." + propertyInfo.Name;

                    throw new ArgumentException("PropertyInfo is null'");
                }
        }

        if (propertyLambda.Body.NodeType != ExpressionType.Convert)
            throw new ArgumentException("Cannot analyze type get name ");

        if (!(propertyLambda.Body is UnaryExpression cov)) throw new ArgumentException("Cannot analyze type get name ");

        me = cov.Operand as MemberExpression;

        if (me == null)
            throw new ArgumentException(
                "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'");

        var classLam = me.Expression;

        type = null;

        if (me.Member == null) throw new ArgumentException("Cannot analyze type get name ");

        if (me.Member.PropertyType == null) throw new ArgumentException("Cannot analyze type get name ");

        type = classLam.Type;

        propertyInfo = me.Member as PropertyInfo;

        if (propertyInfo != null) return type.Name + "." + propertyInfo.Name;

        throw new ArgumentException("PropertyInfo is null'");
    }

    /// <summary>
    /// GetClassAndPropertyName
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="type"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetClassAndPropertyName<T>(
        Expression<Func<T, object>> propertyLambda,
        out Type? type,
        out PropertyInfo? propertyInfo
    )
    {
        dynamic? me;
        propertyInfo = null;

        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            me = propertyLambda.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                );

            var classWithMemberAccess = me.Expression;

            type = null;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    type = classWithMemberAccess.Type;

                    propertyInfo = me.Member as PropertyInfo;

                    if (propertyInfo != null) return type.Name + "." + propertyInfo.Name;

                    throw new ArgumentException("PropertyInfo is null'");
                }
        }

        if (propertyLambda.Body.NodeType != ExpressionType.Convert)
            throw new ArgumentException("Cannot analyze type get name ");

        if (propertyLambda.Body is not UnaryExpression cov)
            throw new ArgumentException("Cannot analyze type get name ");

        me = cov.Operand as MemberExpression;

        if (me == null)
            throw new ArgumentException(
                "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
            );

        var classLam = me.Expression;

        type = null;

        if (me.Member == null) throw new ArgumentException("Cannot analyze type get name ");

        if (me.Member.PropertyType == null) throw new ArgumentException("Cannot analyze type get name ");

        type = classLam.Type;

        propertyInfo = me.Member as PropertyInfo;

        if (propertyInfo != null) return type.Name + "." + propertyInfo.Name;

        throw new ArgumentException("PropertyInfo is null'");
    }

    #endregion GetClassAndPropertyName

    #region GetNames(获取名称列表)

    /// <summary>
    /// 获取名称列表，范例：t => new object[] { t.A.B, t.C },返回A.B,C
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="expression">属性集合表达式,范例：t => new object[]{t.A,t.B}</param>
    public static List<string> GetNames<T>(Expression<Func<T, object[]>> expression)
    {
        var result = new List<string>();

        if (expression.Body is not NewArrayExpression arrayExpression) return result;

        result.AddRange(
            arrayExpression.Expressions.Select(GetName)
                .Where(name => !string.IsNullOrWhiteSpace(name))
        );

        return result;
    }

    #endregion
}