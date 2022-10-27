using System.Linq.Expressions;
using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Enums;
using EasySoft.Core.Sql.Interfaces;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Sql.Common;

public class Sort<T>
{
    public Sort()
    {
        SortType = SortType.Asc;
    }

    /// <summary>  
    /// 指向表达式
    /// </summary>
    public Expression<Func<T, object>> Expression { get; set; } = null!;

    /// <summary>
    /// 判断条件
    /// </summary>
    public SortType SortType { get; set; }
}

public static class SortAssist
{
    public static string Build<T>(IEnumerable<Sort<T>> sorts) where T : IEntityExtra, new()
    {
        var list = new List<string>();

        var enumerable = sorts as Sort<T>[] ?? sorts.ToArray();

        if (enumerable.Any())
            foreach (var s in enumerable)
                list.Add(SqlAssist.AndOrderBy("", s.Expression, s.SortType));

        if (list.Count == 0) throw new Exception("没有可以构造的Sql排序");

        return $" ORDER BY {list.Join(" , ")}";
    }

    /// <summary>
    /// 生成条件
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string Build<T>(Sort<T> condition) where T : IEntityExtra, new()
    {
        return Build(new[]
        {
            condition
        });
    }

    public static string TransferSort<T>(Sort<T> sort) where T : IEntityExtra, new()
    {
        return SqlAssist.TransferSort(sort);
    }
}