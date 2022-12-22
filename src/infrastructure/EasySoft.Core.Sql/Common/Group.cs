namespace EasySoft.Core.Sql.Common;

/// <summary>
/// Group
/// </summary>
/// <typeparam name="T"></typeparam>
public class Group<T>
{
    /// <summary>
    /// 指向表达式
    /// </summary>
    public Expression<Func<T, object>> Expression { get; set; } = null!;
}