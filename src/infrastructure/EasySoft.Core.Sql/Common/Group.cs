using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Interfaces;

namespace EasySoft.Core.Sql.Common;

public class Group<T>
{
    /// <summary>
    /// 指向表达式
    /// </summary>
    public Expression<Func<T, object>> Expression { get; set; } = null!;
}

public static class GroupAssist
{
    public static string Build<T>(IEnumerable<Group<T>> sorts) where T : IEntityExtra, new()
    {
        var list = new List<string>();

        var enumerable = sorts as Group<T>[] ?? sorts.ToArray();

        if (enumerable.Any())
            foreach (var s in enumerable)
                list.Add(SqlAssist.AndGroupBy("", s.Expression));

        if (list.Count == 0) throw new Exception("没有可以构造的Sql排序");

        return $" GROUP BY {list.Join(" , ")}";
    }

    /// <summary>
    /// 生成条件
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string Build<T>(Group<T> condition) where T : IEntityExtra, new()
    {
        return Build(new[]
        {
            condition
        });
    }

    public static string TransferGroup<T>(Group<T> group) where T : IEntityExtra, new()
    {
        return SqlAssist.TransferGroup(group);
    }
}