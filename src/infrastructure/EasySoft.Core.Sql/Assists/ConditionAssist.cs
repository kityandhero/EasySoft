using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Interfaces;

namespace EasySoft.Core.Sql.Assists;

/// <summary>
/// condition assist
/// </summary>
public static class ConditionAssist
{
    /// <summary>
    /// 构建占位常量查询
    /// </summary>
    public static string PlaceholderCondition = " 1=1 ";

    /// <summary>
    /// Build
    /// </summary>
    /// <param name="conditions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string Build<T>(IEnumerable<Condition<T>> conditions) where T : IEntityExtra, new()
    {
        var list = new List<string>();

        var enumerable = conditions as Condition<T>[] ?? conditions.ToArray();

        if (enumerable.Any())
            list.AddRange(enumerable.Select(c => SqlAssist.LinkCondition("", c)));

        if (list.Count == 0) throw new Exception("没有可以构造的Sql条件");

        return $" WHERE {list.Join(" AND ")}";
    }

    /// <summary>
    /// 生成条件
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static string Build<T>(Condition<T> condition) where T : IEntityExtra, new()
    {
        return Build(new[]
        {
            condition
        });
    }

    /// <summary>
    /// transfer condition
    /// </summary>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferCondition<T>(Condition<T> condition) where T : IEntityExtra, new()
    {
        return SqlAssist.TransferCondition(condition);
    }
}