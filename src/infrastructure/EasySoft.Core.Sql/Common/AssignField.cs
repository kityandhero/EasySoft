using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;
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
    public AssignFieldType AssignFieldType { get; set; }

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
                list.Add(SqlAssist.LinkAssignField("", c));

        if (list.Count == 0) throw new Exception("没有可以构造的Sql条件");

        return list.Join(" , ");
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