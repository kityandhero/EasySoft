using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Enums;

namespace EasySoft.Core.Sql.Common;

/// <summary>
/// AssignField
/// </summary>
/// <typeparam name="T"></typeparam>
public class AssignField<T> where T : new()
{
    /// <summary>
    /// 指向表达式
    /// </summary>
    public Expression<Func<T, object>> Expression { get; set; } = null!;

    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; set; } = null!;

    /// <summary>
    /// 赋值模式
    /// </summary>
    public AssignFieldType AssignFieldType { get; set; } = AssignFieldType.Eq;

    /// <summary>
    /// TransferExpression
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string TransferExpression(out Type type)
    {
        return TransferAssist.GetTableAndColumnName(Expression, out type);
    }
}