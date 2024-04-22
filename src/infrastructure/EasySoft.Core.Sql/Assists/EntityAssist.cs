using EasySoft.Core.Infrastructure.Entities.Interfaces;

namespace EasySoft.Core.Sql.Assists;

/// <summary>
/// EntityAssist
/// </summary>
public static class EntityAssist
{
    /// <summary>
    /// GetPropertyName
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetPropertyName<T>(
        Expression<Func<T, object>> expression
    ) where T : IEntity
    {
        return ReflectionAssist.GetPropertyName(expression);
    }

    /// <summary>
    /// GetPropertyInfo
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static PropertyInfo GetPropertyInfo<T>(
        Expression<Func<T, object>> propertyLambda
    ) where T : IEntity
    {
        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            dynamic me = propertyLambda.Body;

            if (me.Member != null)
            {
                if (me.Member.PropertyType != null)
                {
                    var propertyInfo = me.Member as PropertyInfo;

                    if (propertyInfo == null)
                    {
                        throw new ArgumentException("PropertyInfo is null'");
                    }

                    return propertyInfo;
                }
            }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert)
        {
            if (propertyLambda.Body is not UnaryExpression cov)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            dynamic? me = cov.Operand as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );
            }

            if (me.Member == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            if (me.Member.PropertyType == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            var propertyInfo = me.Member as PropertyInfo;

            if (propertyInfo == null)
            {
                throw new ArgumentException("PropertyInfo is null'");
            }

            return propertyInfo;
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }
}