using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Extensions;

namespace EasySoft.Core.Sql.Assists;

/// <summary>
/// AssignFieldAssist
/// </summary>
public static class AssignFieldAssist
{
    /// <summary>
    /// Build
    /// </summary>
    /// <param name="assignUpdates"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string Build<T>(IEnumerable<AssignField<T>> assignUpdates) where T : IEntity, new()
    {
        var list = new List<string>();

        var enumerable = assignUpdates as AssignField<T>[] ?? assignUpdates.ToArray();

        if (enumerable.Any())
            foreach (var c in enumerable)
                list.Add(new AdvanceSqlBuilder().LinkAssignField(c).Sql);

        if (list.Count == 0) throw new Exception("没有可以构造的Sql条件");

        return list.Join(",");
    }

    /// <summary>
    /// 生成条件
    /// </summary>
    /// <param name="assignField"></param>
    /// <returns></returns>
    public static string Build<T>(AssignField<T> assignField) where T : IEntity, new()
    {
        return Build(new[]
        {
            assignField
        });
    }

    /// <summary>
    /// TransferAssignUpdate
    /// </summary>
    /// <param name="assignField"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string TransferAssignUpdate<T>(AssignField<T> assignField) where T : IEntity, new()
    {
        return SqlAssist.TransferAssignField(assignField);
    }
}