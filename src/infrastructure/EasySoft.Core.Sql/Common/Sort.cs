using EasySoft.Core.Sql.Enums;

namespace EasySoft.Core.Sql.Common;

/// <summary>
/// Sort
/// </summary>
/// <typeparam name="T"></typeparam>
public class Sort<T>
{
    /// <summary>  
    /// 指向表达式
    /// </summary>
    public Expression<Func<T, object>> Expression { get; set; } = null!;

    /// <summary>
    /// 判断条件
    /// </summary>
    public SortType SortType { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public Sort()
    {
        SortType = SortType.Asc;
    }
}