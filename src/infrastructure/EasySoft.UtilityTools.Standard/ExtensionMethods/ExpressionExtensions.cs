using System.Linq.Expressions;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

/// <summary>
/// ExpressionExtensions
/// </summary>
public static class ExpressionExtensions
{
    /// <summary>
    /// And条件合并表达式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2
    )
    {
        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(expr1.Body, expr2.Body),
            expr1.Parameters
        );
    }

    /// <summary>
    /// Or条件合并表达式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2
    )
    {
        return Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(expr1.Body, expr2.Body),
            expr1.Parameters
        );
    }
}