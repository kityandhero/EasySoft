using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Extensions;

namespace EasySoft.Core.Sql.Assists;

/// <summary>
/// GroupAssist
/// </summary>
public static class GroupAssist
{
    /// <summary>
    /// Build
    /// </summary>
    /// <param name="sorts"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string Build<T>(IEnumerable<Group<T>> sorts) where T : IEntity, new()
    {
        var list = new List<string>();

        var enumerable = sorts as Group<T>[] ?? sorts.ToArray();

        if (enumerable.Any())
            foreach (var s in enumerable)
                list.Add(new AdvanceSqlBuilder().AndGroupBy(s.Expression).Sql);

        if (list.Count == 0) throw new Exception("没有可以构造的Sql排序");

        return $" GROUP BY {list.Join(" , ")}";
    }

    /// <summary>
    /// 生成条件
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string Build<T>(Group<T> condition) where T : IEntity, new()
    {
        return Build(new[]
        {
            condition
        });
    }

    /// <summary>
    /// TransferGroup
    /// </summary>
    /// <param name="group"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferGroup<T>(Group<T> group) where T : IEntity, new()
    {
        return SqlAssist.TransferGroup(group);
    }
}