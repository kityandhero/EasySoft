using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Extensions;

namespace EasySoft.Core.Sql.Assists;

/// <summary>
/// SortAssist
/// </summary>
public static class SortAssist
{
    /// <summary>
    /// Build
    /// </summary>
    /// <param name="sorts"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string Build<T>(IEnumerable<Sort<T>> sorts) where T : IEntity, new()
    {
        var list = new List<string>();

        var enumerable = sorts as Sort<T>[] ?? sorts.ToArray();

        enumerable.ForEach(s => { list.Add(new AdvanceSqlBuilder().AndOrderBy(s.Expression, s.SortType).Sql); });

        if (list.Count == 0) throw new Exception("没有可以构造的Sql排序");

        return $" ORDER BY {list.Join(" , ")}";
    }

    /// <summary>
    /// 生成条件
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string Build<T>(Sort<T> condition) where T : IEntity, new()
    {
        return Build(new[]
        {
            condition
        });
    }

    /// <summary>
    /// TransferSort
    /// </summary>
    /// <param name="sort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferSort<T>(Sort<T> sort) where T : IEntity, new()
    {
        return SqlAssist.TransferSort(sort);
    }
}