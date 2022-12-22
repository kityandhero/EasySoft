namespace EasySoft.Core.Sql.Common;

/// <summary>
/// FieldItemSpecial
/// </summary>
/// <typeparam name="T"></typeparam>
public class FieldItemSpecial<T>
{
    /// <summary>
    /// PropertyLambda
    /// </summary>
    public Expression<Func<T, object>> PropertyLambda { get; }

    /// <summary>
    /// ColumnAlias
    /// </summary>
    public string ColumnAlias { get; set; }

    /// <summary>
    /// ReplaceDBNullValue
    /// </summary>
    public bool ReplaceDBNullValue { get; set; }

    /// <summary>
    /// FieldItemSpecial
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <exception cref="Exception"></exception>
    public FieldItemSpecial(Expression<Func<T, object>> propertyLambda)
    {
        PropertyLambda = propertyLambda ?? throw new Exception("propertyLambda not allow null");
        ColumnAlias = "";
        ReplaceDBNullValue = true;
    }
}