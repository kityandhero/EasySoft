using EasySoft.Core.Sql.Common;

namespace EasySoft.Core.Sql.Factories;

/// <summary>
/// FieldItemSpecialFactory
/// </summary>
public static class FieldItemSpecialFactory
{
    /// <summary>
    /// BuildFieldItem
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <param name="replaceDBNullValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static FieldItemSpecial<T> BuildFieldItem<T>(
        Expression<Func<T, object>> propertyLambda,
        string columnAlias = "",
        bool replaceDBNullValue = true
    )
    {
        return new FieldItemSpecial<T>(propertyLambda)
        {
            ColumnAlias = columnAlias,
            ReplaceDBNullValue = replaceDBNullValue
        };
    }

    /// <summary>
    /// BuildFieldItems
    /// </summary>
    /// <param name="propertyLambdas"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<FieldItemSpecial<T>> BuildFieldItems<T>(
        params Expression<Func<T, object>>[] propertyLambdas
    )
    {
        if (propertyLambdas == null || propertyLambdas.Length <= 0)
            throw new Exception("propertyLambdas not allow Null Or Empty");

        var result = new List<FieldItemSpecial<T>>();

        foreach (var propertyLambda in propertyLambdas) result.Add(new FieldItemSpecial<T>(propertyLambda));

        return result;
    }
}