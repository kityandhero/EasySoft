namespace EasySoft.Core.Sql.Common;

/// <summary>
/// FieldItem
/// </summary>
/// <typeparam name="T"></typeparam>
public class FieldItem<T>
{
    /// <summary>
    /// PropertyLambda
    /// </summary>
    public Expression<Func<T>> PropertyLambda { get; }

    /// <summary>
    /// ColumnAlias
    /// </summary>
    public string ColumnAlias { get; set; }

    /// <summary>
    /// ReplaceDBNullValue
    /// </summary>
    public bool ReplaceDBNullValue { get; set; }

    /// <summary>
    /// FieldItem
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <exception cref="Exception"></exception>
    public FieldItem(Expression<Func<T>> propertyLambda)
    {
        PropertyLambda = propertyLambda ?? throw new Exception("propertyLambda not allow null");
        ColumnAlias = "";
        ReplaceDBNullValue = true;
    }
}