using EasySoft.Core.Sql.Common;

namespace EasySoft.Core.Sql.Factories;

/// <summary>
/// FieldItemFactory
/// </summary>
public static class FieldItemFactory
{
    /// <summary>
    /// BuildFieldItem
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <param name="columnAlias"></param>
    /// <param name="replaceDBNullValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static FieldItem<T> BuildFieldItem<T>(
        Expression<Func<T>> propertyLambda,
        string columnAlias = "",
        bool replaceDBNullValue = true
    )
    {
        return new FieldItem<T>(propertyLambda)
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
    public static List<FieldItem<T>> BuildFieldItems<T>(
        params Expression<Func<T>>[] propertyLambdas
    )
    {
        if (propertyLambdas == null || propertyLambdas.Length <= 0)
            throw new Exception("propertyLambdas not allow Null Or Empty");

        var result = new List<FieldItem<T>>();

        foreach (var propertyLambda in propertyLambdas) result.Add(new FieldItem<T>(propertyLambda));

        return result;
    }
}